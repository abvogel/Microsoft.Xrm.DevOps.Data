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
            SetCulture(CultureInfo.InvariantCulture);
        }

        protected void SetCulture(CultureInfo culture)
        {
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }

        /// <summary>
        /// Our goal is to test different kind of number formats ifferent decimal separator (space vs . vs ,) different number group separator (. vs ,)
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<object[]> GetCultures()
        {
            yield return new object[] { CultureInfo.InvariantCulture };
            yield return new object[] { new CultureInfo("en-US") };
            yield return new object[] { new CultureInfo("en-GB") };
            yield return new object[] { new CultureInfo("de-DE") };
            yield return new object[] { new CultureInfo("fr-FR") };
            yield return new object[] { new CultureInfo("ru-RU") };
            yield return new object[] { new CultureInfo("ja-JP") };
        }
    }
}
