using System;
using System.IO;
using System.Management.Automation;
using Microsoft.Xrm.Sdk;
using Ionic.Zip;
using System.Xml;
using System.Text;

namespace Microsoft.Xrm.DevOps.Data.PowerShell.Cmdlets
{
    [Cmdlet(VerbsData.Export, "CrmDataPackage", DefaultParameterSetName = "SaveAsZip", SupportsShouldProcess = true)]
    [OutputType(typeof(PowerShell.CrmDataPackage))]
    public class ExportCrmDataPackage : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true)]
        public CrmDataPackage Package { get; set; }

        [Parameter(Position = 1, ParameterSetName = "SaveAsZip")]
        public String ZipPath { private get; set; }

        /// <summary>
        /// Saves data.xml, data_schema.xml and [Content_Types].xml within specified folder. If files exist, use Force to overwrite.
        /// </summary>
        [Parameter(Position = 1, ParameterSetName = "SaveInDirectory")]
        public String DirectoryPath { private get; set; }

        /// <summary>
        /// Overwrites existing data.xml, data_schema.xml and [Content_Types].xml files in target directory
        /// </summary>
        [Parameter(ParameterSetName = "SaveInDirectory")]
        public SwitchParameter Force { get; set; }

        bool OverwriteYesToAll = false;
        bool OverwriteNoToAll = false;

        protected override void ProcessRecord()
        {
            try
            {
                if (!string.IsNullOrEmpty(ZipPath))
                {
                    SaveAsZip();
                }
                if (!string.IsNullOrEmpty(DirectoryPath))
                {
                    SaveInDirectory();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void SaveAsZip()
        {
            //Why no force there? To maintain compatbility - originally there was no Force parameter and default behavior was: zip overwrites
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

                if (ShouldProcess(ZipPath, "Save"))
                {
                    zip.Save(ZipPath);
                }
            }
        }

        private void SaveInDirectory()
        {
            OverwriteYesToAll = Force;
            string contentTypesPath = Path.Combine(DirectoryPath, "[Content_Types].xml");
            string dataPath = Path.Combine(DirectoryPath, "data.xml");
            string schemaPath = Path.Combine(DirectoryPath, "data_schema.xml");
            Directory.CreateDirectory(DirectoryPath);
            if (ConfirmOverwrite(contentTypesPath) && ShouldProcess(contentTypesPath, "Save"))
            {
                SaveXmlDocument(Package.ContentTypes, contentTypesPath);
            }
            if (ConfirmOverwrite(dataPath) && ShouldProcess(dataPath, "Save"))
            {
                SaveXmlDocument(Package.Data, dataPath);
            }
            if (ConfirmOverwrite(schemaPath) && ShouldProcess(schemaPath, "Save"))
            {
                
                SaveXmlDocument(Package.Schema, schemaPath);
            }
        }

        private void SaveXmlDocument(XmlDocument document, string output)
        {
            GenerateVerboseMessage($"Saving {output}");
            using (var sw = new StreamWriter(output, append: false, encoding: Encoding.UTF8))
            {
                document.Save(sw);
            }
        }

        private bool ConfirmOverwrite(string path)
        {
            if (OverwriteNoToAll) { return false; }
            if (OverwriteYesToAll) { return true; }
            if (File.Exists(path))
            {
                return ShouldContinue($"File at path '{path}' already exists. Use {nameof(Force)} parameter to overwrite", "Overwrite", ref OverwriteYesToAll, ref OverwriteNoToAll);
            }
            return true;
        }

        private void GenerateVerboseMessage(string v)
        {
            WriteVerbose(String.Format("{0} {1}: {2}", DateTime.Now.ToShortTimeString(), "Export-CrmDataPackage", v));
        }
    }
}