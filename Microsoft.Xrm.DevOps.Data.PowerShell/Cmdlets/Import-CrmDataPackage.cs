using System;
using System.IO;
using System.Management.Automation;
using Microsoft.Xrm.Sdk;
using Ionic.Zip;
using System.Xml;

namespace Microsoft.Xrm.DevOps.Data.PowerShell.Cmdlets
{
    [Cmdlet(VerbsData.Import, "CrmDataPackage")]
    [OutputType(typeof(PowerShell.CrmDataPackage))]
    public class ImportCrmDataPackage : PSCmdlet
    {
        [Parameter(Position = 0)]
        public String ZipPath { private get; set; }

        protected override void ProcessRecord()
        {
            try
            {
                CrmDataPackage package = new CrmDataPackage();
                using (ZipFile zip = new ZipFile())
                {
                    var zipData = ZipFile.Read(ZipPath);
                    foreach (ZipEntry entry in zipData.Entries)
                    {
                        XmlDocument Xml = new XmlDocument();
                        using (var ms = new MemoryStream())
                        {
                            entry.Extract(ms);
                            ms.Position = 0;
                            var sr = new StreamReader(ms);
                            var myStr = sr.ReadToEnd();
                            Xml.LoadXml(myStr);
                        }
                        switch(entry.FileName)
                        {
                            case "data.xml":
                                package.Data = Xml;
                                break;
                            case "data_schema.xml":
                                package.Schema = Xml;
                                break;
                            default:
                                package.ContentTypes = Xml;
                                break;
                        }
                    }

                    base.WriteObject(package);
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