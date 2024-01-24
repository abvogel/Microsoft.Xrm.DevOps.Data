using System;
using System.IO;
using System.Management.Automation;
using Microsoft.Xrm.Sdk;
using System.Xml;
using Ionic.Zip;
using System.Linq;

namespace Microsoft.Xrm.DevOps.Data.PowerShell.Cmdlets
{
    [Cmdlet(VerbsCommon.New, "CrmDataPackage", DefaultParameterSetName = "SourceTargetPackage")]
    public class NewCrmDataPackage : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true)]
        public FileInfo DataFile { get; set; }

        protected override void ProcessRecord()
        {
            var dataPath = DataFile.FullName;
            var schemaPath = Path.Combine(DataFile.DirectoryName, "data_schema.xml");
            if (!File.Exists(schemaPath))
            {
                throw new InvalidOperationException($"Couldn't find data_schema.xml at {schemaPath} - it is expected to be in same folder as data.xml");
            }
            WriteVerbose($"New {nameof(CrmDataPackage)} from {dataPath} and {schemaPath}");

            var dataXml = new XmlDocument();
            dataXml.Load(dataPath);
            var schemaXml = new XmlDocument();
            schemaXml.Load(schemaPath);
            DataBuilder db = new DataBuilder();
            var crmDataPackage = new CrmDataPackage
            {
                ContentTypes = db.BuildContentTypesXML(),
                Data = dataXml,
                Schema = schemaXml
            };

            WriteObject(crmDataPackage);
        }

        private void GenerateVerboseMessage(string v)
        {
            WriteVerbose(String.Format("{0} {1}: {2}", DateTime.Now.ToShortTimeString(), "Merge-CrmDataPackage", v));
        }
    }
}