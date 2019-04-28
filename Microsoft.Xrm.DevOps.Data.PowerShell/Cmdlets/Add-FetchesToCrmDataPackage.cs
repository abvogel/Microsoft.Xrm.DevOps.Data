using System;
using System.Management.Automation;
using Microsoft.Xrm.Sdk;
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
            GenerateVerboseMessage("Generating DataBuilder Instance.");
            DevOps.Data.DataBuilder db = new DevOps.Data.DataBuilder(this.Conn);

            if (this.Fetches.Length == 0)
                throw new Exception("No fetches provided.");

            GenerateVerboseMessage("Appending CrmDataPackage.");
            db.AppendData(Package.Data, Package.Schema);

            foreach (String fetch in this.Fetches)
            {
                GenerateVerboseMessage(String.Format("Appending fetch: {0}", fetch));
                db.AppendData(fetch);
            }

            if (this.Identifiers.Keys.Count > 0)
            {
                GenerateVerboseMessage("Setting identifiers.");
                foreach (String key in this.Identifiers.Keys)
                {
                    String[] identifier = Array.ConvertAll<object, string>((Object[])this.Identifiers[key], delegate (object obj) { return (string)obj; });
                    GenerateVerboseMessage(String.Format("Setting entity/identifier: {0}/{1}", key, String.Join(",", identifier)));
                    db.SetIdentifier(key, identifier);
                }
            }

            if (this.DisablePlugins.Keys.Count > 0)
            {
                GenerateVerboseMessage("Disabling plugins.");
                foreach (String key in this.DisablePlugins.Keys)
                {
                    GenerateVerboseMessage(String.Format("Setting entity/disable: {0}/{1}", key, DisablePlugins[key].ToString()));
                    db.SetPluginsDisabled(key, (Boolean)DisablePlugins[key]);
                }
            }

            if (this.DisablePluginsGlobally)
            {
                GenerateVerboseMessage("Disabling plugins globally.");
                db.SetPluginsDisabled(true);
            }

            try
            {
                GenerateVerboseMessage("Generating CrmDataPackage.");
                var package = new CrmDataPackage()
                {
                    ContentTypes = db.BuildContentTypesXML(),
                    Data = db.BuildDataXML(),
                    Schema = db.BuildSchemaXML()
                };

                GenerateVerboseMessage("Writing CrmDataPackage to output.");
                base.WriteObject(package);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void GenerateVerboseMessage(string v)
        {
            WriteVerbose(String.Format("{0} {1}: {2}", DateTime.Now.ToShortTimeString(), "Add-FetchesToCrmDataPackage", v));
        }
    }
}