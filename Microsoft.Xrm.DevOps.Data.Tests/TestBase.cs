using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Xrm.DevOps.Data.Tests
{
    public abstract class TestBase
    {
        [TestInitialize]
        public void InitializeCulture()
        {
            ///Ensures that data is serialized in a cinsistent way across different devices which use different regional settings.
            ///InvariantCulture will use dot for decimal separator when parsing or converting decimals to string.
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
        }
    }
}
