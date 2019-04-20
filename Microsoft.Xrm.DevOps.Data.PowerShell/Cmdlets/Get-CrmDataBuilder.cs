using System.Management.Automation;

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