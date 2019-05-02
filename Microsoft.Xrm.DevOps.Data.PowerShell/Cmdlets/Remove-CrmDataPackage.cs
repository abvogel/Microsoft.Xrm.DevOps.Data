using Microsoft.Xrm.Sdk;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Management.Automation;

namespace Microsoft.Xrm.DevOps.Data.PowerShell.Cmdlets
{
    [Cmdlet(VerbsCommon.Remove, "CrmDataPackage")]
    [OutputType(typeof(CrmDataPackage))]
    public class RemoveCrmDataPackage : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true)]
        public CrmDataPackage SourcePackage { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public CrmDataPackage RemovePackage { get; set; }

        protected override void ProcessRecord()
        {
            GenerateVerboseMessage("Generating DataBuilder Instance.");
            DevOps.Data.DataBuilder db = new DevOps.Data.DataBuilder();

            GenerateVerboseMessage("Appending CrmDataPackage.");
            db.AppendData(SourcePackage.Data, SourcePackage.Schema);

            GenerateVerboseMessage("Removing second CrmDataPackage.");
            db.RemoveData(RemovePackage.Data, RemovePackage.Schema);

            try
            {
                GenerateVerboseMessage("Generating CrmDataPackage.");
                var Package = new CrmDataPackage()
                {
                    ContentTypes = db.BuildContentTypesXML(),
                    Data = db.BuildDataXML(),
                    Schema = db.BuildSchemaXML()
                };

                GenerateVerboseMessage("Writing CrmDataPackage to output.");
                base.WriteObject(Package);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void GenerateVerboseMessage(string v)
        {
            WriteVerbose(String.Format("{0} {1}: {2}", DateTime.Now.ToShortTimeString(), "Remove-CrmDataPackage", v));
        }
    }
}