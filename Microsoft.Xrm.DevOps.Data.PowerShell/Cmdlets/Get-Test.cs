//using Microsoft.Xrm.Tooling.PackageDeployment.CrmPackageCore.ImportCode;
//using Microsoft.Xrm.Tooling.PackageDeployment.CrmPackageExtentionBase;
//using Microsoft.Xrm.Tooling.PackageDeployment.Powershell.Cmdlets.Base;
//using Microsoft.Xrm.Tooling.PackageDeployment.Powershell.Resources;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;

namespace Microsoft.Xrm.DevOps.PackageDeployment.Powershell.Cmdlets
{
	[Cmdlet(VerbsCommon.Get, "Test")]
	public class GetCrmPackageCommand : PSCmdlet
	{
		[Parameter(Position = 0, Mandatory = false)]
		public string TestParameter
		{
			get;
			set;
		}

		protected override void BeginProcessing()
		{

		}

		protected override void EndProcessing()
		{

		}
	}
}