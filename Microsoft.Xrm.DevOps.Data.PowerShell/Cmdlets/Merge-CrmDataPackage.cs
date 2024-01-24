using System;
using System.IO;
using System.Management.Automation;
using Microsoft.Xrm.Sdk;
using System.Xml;
using Ionic.Zip;
using System.Linq;

namespace Microsoft.Xrm.DevOps.Data.PowerShell.Cmdlets
{
    [Cmdlet(VerbsData.Merge, "CrmDataPackage")]
    public class MergeCrmDataPackage : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true)]
        public CrmDataPackage SourcePackage { get; set; }

        [Parameter(Position = 1)]
        public CrmDataPackage AdditionalPackage { get; set; }

        CrmDataPackage mergedPackage = null;

        protected override void ProcessRecord()
        {
            if (AdditionalPackage != null)
            {
                var package = MergePackages(SourcePackage, AdditionalPackage);
                GenerateVerboseMessage("Writing CrmDataPackage to output.");
                WriteObject(package);
            }
            else
            {
                if (mergedPackage == null)
                {
                    //Initialize first package. If this would be the only package piped as SourcePackage, the same package will be returned.
                    mergedPackage = this.SourcePackage;
                }
                else
                {
                    mergedPackage = MergePackages(mergedPackage, SourcePackage);
                }
            }
        }

        private CrmDataPackage MergePackages(CrmDataPackage sourcePackage, CrmDataPackage additionalPackage)
        {
            GenerateVerboseMessage("Generating DataBuilder Instance.");
            DevOps.Data.DataBuilder db = new DevOps.Data.DataBuilder();

            GenerateVerboseMessage("Appending Source CrmDataPackage.");
            db.AppendData(sourcePackage.Data, sourcePackage.Schema);
            GenerateVerboseMessage("Appending Additional CrmDataPackage.");
            db.AppendData(additionalPackage.Data, additionalPackage.Schema);

            try
            {
                GenerateVerboseMessage("Generating CrmDataPackage.");
                var Package = new CrmDataPackage()
                {
                    ContentTypes = db.BuildContentTypesXML(),
                    Data = db.BuildDataXML(),
                    Schema = db.BuildSchemaXML()
                };

                return Package;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected override void EndProcessing()
        {
            base.EndProcessing();
            if (AdditionalPackage == null)
            {
                GenerateVerboseMessage("Writing CrmDataPackage to output.");
                WriteObject(mergedPackage);
            }
        }

        private void GenerateVerboseMessage(string v)
        {
            WriteVerbose(String.Format("{0} {1}: {2}", DateTime.Now.ToShortTimeString(), "Merge-CrmDataPackage", v));
        }
    }
}