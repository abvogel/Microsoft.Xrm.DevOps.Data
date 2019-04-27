using System;
using System.Management.Automation;
using Microsoft.Xrm.Sdk;
using System.Collections.Generic;
using System.Collections;

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
        public Hashtable Identifiers = new Hashtable();

        [Parameter(Position = 4)]
        public Hashtable DisablePlugins = new Hashtable();

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
                foreach (String key in this.Identifiers.Keys)
                {
                    String[] identifier = Array.ConvertAll<object, string>((Object[])this.Identifiers[key], delegate (object obj) { return (string)obj; });
                    db.SetIdentifier(key, identifier);
                }

            if (this.DisablePlugins.Keys.Count > 0)
                foreach (String key in this.DisablePlugins.Keys)
                    db.SetPluginsDisabled(key, (Boolean)DisablePlugins[key]);

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