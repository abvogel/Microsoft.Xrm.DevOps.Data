using System;
using System.IO;
using System.Management.Automation;
using Microsoft.Xrm.Sdk;
using Ionic.Zip;

namespace Microsoft.Xrm.DevOps.Data.PowerShell.Cmdlets
{
    [Cmdlet(VerbsData.Export, "CrmDataPackage")]
    [OutputType(typeof(PowerShell.CrmDataPackage))]
    public class ExportCrmDataPackage : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true)]
        public CrmDataPackage Package { get; set; }

        [Parameter(Position = 1)]
        public String ZipPath { private get; set; }

        protected override void ProcessRecord()
        {
            try
            {
                using (ZipFile zip = new ZipFile())
                {
                    GenerateVerboseMessage("Streaming [Content_Types].xml to memory.");
                    MemoryStream contentTypeFS = new MemoryStream();
                    Package.ContentTypes.Save(contentTypeFS);
                    contentTypeFS.Seek(0, SeekOrigin.Begin);

                    GenerateVerboseMessage("Streaming data.xml to memory.");
                    MemoryStream dataFS = new MemoryStream();
                    Package.Data.Save(dataFS);
                    dataFS.Seek(0, SeekOrigin.Begin);

                    GenerateVerboseMessage("Streaming data_schema.xml to memory.");
                    MemoryStream schemaFS = new MemoryStream();
                    Package.Schema.Save(schemaFS);
                    schemaFS.Seek(0, SeekOrigin.Begin);

                    GenerateVerboseMessage("Crossing the streams.");
                    zip.AddEntry("[Content_Types].xml", contentTypeFS);
                    zip.AddEntry("data.xml", dataFS);
                    zip.AddEntry("data_schema.xml", schemaFS);

                    GenerateVerboseMessage(String.Format("Zipping streams to {0}.", ZipPath));
                    zip.Save(ZipPath);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void GenerateVerboseMessage(string v)
        {
            WriteVerbose(String.Format("{0} {1}: {2}", DateTime.Now.ToShortTimeString(), "Export-CrmDataPackage", v));
        }
    }
}