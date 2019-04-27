using Microsoft.Xrm.Sdk;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Management.Automation;

namespace Microsoft.Xrm.DevOps.Data.PowerShell.Cmdlets
{
    [Cmdlet(VerbsCommon.Get, "CrmDataPackage")]
    [OutputType(typeof(CrmDataPackage))]
    public class GetCrmDataPackage : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public IOrganizationService Conn { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public String[] Fetches { get; set; }

        [Parameter(Position = 2)]
        public Hashtable Identifiers = new Hashtable();

        [Parameter(Position = 3)]
        public Hashtable DisablePlugins = new Hashtable();

        [Parameter(Position = 4)]
        public Boolean DisablePluginsGlobally = false;

        protected override void ProcessRecord()
        {
            DevOps.Data.DataBuilder db = new DevOps.Data.DataBuilder(this.Conn);

            if (this.Fetches.Length == 0)
                throw new Exception("No fetches provided.");

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
                var Package = new CrmDataPackage()
                {
                    ContentTypes = db.BuildContentTypesXML(),
                    Data = db.BuildDataXML(),
                    Schema = db.BuildSchemaXML()
                };

                base.WriteObject(Package);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}