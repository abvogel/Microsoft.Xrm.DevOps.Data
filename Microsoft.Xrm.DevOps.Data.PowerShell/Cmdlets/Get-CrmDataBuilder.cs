using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using Microsoft.Xrm.Sdk;

namespace Microsoft.Xrm.DevOps.Data.PowerShell.Cmdlets
{
	[Cmdlet(VerbsCommon.Get, "CrmDataBuilder")]
    [OutputType(typeof(PowerShell.DataBuilder))]
    public class GetCrmDataBuilder : PSCmdlet
	{
        private PowerShell.DataBuilder crmDataBuilder = new PowerShell.DataBuilder();

        protected override void ProcessRecord()
        {
            base.WriteObject(this.crmDataBuilder);
        }
	}
}