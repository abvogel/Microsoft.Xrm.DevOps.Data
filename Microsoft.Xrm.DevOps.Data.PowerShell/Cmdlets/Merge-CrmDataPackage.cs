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
            DevOps.Data.DataBuilder db = new DevOps.Data.DataBuilder();

            db.AppendData(SourcePackage.Data, SourcePackage.Schema);
            db.AppendData(AdditionalPackage.Data, AdditionalPackage.Schema);

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