using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using Microsoft.Xrm.DevOps.Data;
using Microsoft.Xrm.Sdk;
using System.Xml;
using Ionic.Zip;

namespace Microsoft.Xrm.DevOps.Data.PowerShell.Cmdlets
{
    [Cmdlet(VerbsData.Export, "CrmDataBuilder")]
    public class ExportCrmDataBuilder : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public IOrganizationService Conn
        {
            get;
            set;
        }

        [Parameter(Position = 1, Mandatory = true)]
        public PowerShell.DataBuilder Builder
        {
            private get;
            set;
        }

        [Parameter(Position = 2, Mandatory = true)]
        public String ZipPath
        {
            private get;
            set;
        }

        protected override void ProcessRecord()
        {
            DevOps.Data.DataBuilder db = new DevOps.Data.DataBuilder(this.Conn);

            if (this.Builder.FetchCollection.Length == 0)
            {
                throw new Exception("No fetches provided.");
            }

            // Fetches
            foreach (String fetch in this.Builder.FetchCollection)
            {
                db.AppendData(fetch);
            }

            // Identifiers
            if (this.Builder.Identifiers.Keys.Count > 0)
            {
                foreach (var identifier in this.Builder.Identifiers)
                {
                    db.SetIdentifier(identifier.Key, identifier.Value);
                }
            }

            // Disabled plugins
            if (this.Builder.DisablePlugins.Keys.Count > 0)
            {
                foreach (var disablePlugin in this.Builder.DisablePlugins)
                {
                    db.SetPluginsDisabled(disablePlugin.Key, disablePlugin.Value);
                }
            }

            // Globally disabled plugins
            if (this.Builder.DisablePluginsGlobally)
            {
                db.SetPluginsDisabled(true);
            }

            try
            {
                XmlDocument contentTypes = db.BuildContentTypesXML();
                String contentTypeFileName = "[Content_Types].xml";
                XmlDocument data = db.BuildDataXML();
                String dataFileName = "data.xml";
                XmlDocument schema = db.BuildSchemaXML();
                String schemaFileName = "data_schema.xml";

                using (ZipFile zip = new ZipFile())
                {
                    MemoryStream contentTypeFS = new MemoryStream();
                    contentTypes.Save(contentTypeFS);
                    contentTypeFS.Seek(0, SeekOrigin.Begin);

                    MemoryStream dataFS = new MemoryStream();
                    data.Save(dataFS);
                    dataFS.Seek(0, SeekOrigin.Begin);

                    MemoryStream schemaFS = new MemoryStream();
                    schema.Save(schemaFS);
                    schemaFS.Seek(0, SeekOrigin.Begin);

                    zip.AddEntry(contentTypeFileName, contentTypeFS);
                    zip.AddEntry(dataFileName, dataFS);
                    zip.AddEntry(schemaFileName, schemaFS);

                    zip.Save(ZipPath);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}