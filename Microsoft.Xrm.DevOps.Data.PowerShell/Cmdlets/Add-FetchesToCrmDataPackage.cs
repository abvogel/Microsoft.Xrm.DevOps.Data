using System;
using System.Management.Automation;
using Microsoft.Xrm.Sdk;
using System.Collections.Generic;

namespace Microsoft.Xrm.DevOps.Data.PowerShell.Cmdlets
{
    [Cmdlet(VerbsCommon.Add, "CrmDataPackage")]
    public class AddCrmDataPackage : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public IOrganizationService Conn { get; set; }

        [Parameter(Position = 1, Mandatory = true, ValueFromPipeline = true)]
        public CrmDataPackage Package { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public String[] Fetches { get; set; }

        [Parameter(Position = 3)]
        public Dictionary<String, String[]> Identifiers = new Dictionary<String, String[]>();

        [Parameter(Position = 4)]
        public Dictionary<String, Boolean> DisablePlugins = new Dictionary<String, Boolean>();

        [Parameter(Position = 5)]
        public Boolean DisablePluginsGlobally = false;

        protected override void ProcessRecord()
        {
            DevOps.Data.DataBuilder db = new DevOps.Data.DataBuilder(this.Conn);

            if (this.Fetches.Length == 0)
                throw new Exception("No fetches provided.");

            db.AppendData(Package.Data, Package.Schema);

            foreach (String fetch in this.Fetches)
                db.AppendData(fetch);

            if (this.Identifiers.Keys.Count > 0)
                foreach (var identifier in this.Identifiers)
                    db.SetIdentifier(identifier.Key, identifier.Value);

            if (this.DisablePlugins.Keys.Count > 0)
                foreach (var disablePlugin in this.DisablePlugins)
                    db.SetPluginsDisabled(disablePlugin.Key, disablePlugin.Value);

            if (this.DisablePluginsGlobally)
                db.SetPluginsDisabled(true);

            try
            {
                var package = new CrmDataPackage()
                {
                    ContentTypes = db.BuildContentTypesXML(),
                    Data = db.BuildDataXML(),
                    Schema = db.BuildSchemaXML()
                };

                base.WriteObject(package);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}