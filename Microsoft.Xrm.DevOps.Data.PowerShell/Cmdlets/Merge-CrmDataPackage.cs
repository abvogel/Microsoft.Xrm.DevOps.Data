using System;
using System.IO;
using System.Management.Automation;
using Microsoft.Xrm.Sdk;
using System.Xml;
using Ionic.Zip;

namespace Microsoft.Xrm.DevOps.Data.PowerShell.Cmdlets
{
    [Cmdlet(VerbsData.Merge, "CrmDataPackage")]
    public class MergeCrmDataPackage : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true)]
        public CrmDataPackage SourcePackage { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public CrmDataPackage AdditionalPackage { get; set; }

        protected override void ProcessRecord()
        {
            GenerateVerboseMessage("Generating DataBuilder Instance.");
            DevOps.Data.DataBuilder db = new DevOps.Data.DataBuilder();

            GenerateVerboseMessage("Appending Source CrmDataPackage.");
            db.AppendData(SourcePackage.Data, SourcePackage.Schema);
            GenerateVerboseMessage("Appending Additional CrmDataPackage.");
            db.AppendData(AdditionalPackage.Data, AdditionalPackage.Schema);

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
            WriteVerbose(String.Format("{0} {1}: {2}", DateTime.Now.ToShortTimeString(), "Merge-CrmDataPackage", v));
        }
    }
}