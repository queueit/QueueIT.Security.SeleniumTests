using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using QueueIT.Security.SeleniumTests.SiteModel.Extensions.CustomByLocators;
using QueueIT.Security.SeleniumTests.SiteModel.Implementations;

namespace QueueIT.Security.SeleniumTests.SiteModel.Pages
{
    public class SimplePage : PageBase
    {
        public SimplePage(ReferenceImplementation implementation)
            : base(implementation)
        {}

        public override bool IsAt()
        {
            return base.IsAt(() => GetBodyElement("Simple Queue Configuration"));
        }

        public override string Url
        {
            get { return this.Implementation.Host + "simple" + this.Implementation.PageExtension; }
        }

        public void CancelToken()
        {
            this.Click("a[id*='Cancel']");
        }

        public void ExpireToken()
        {
            this.Click("a[id*='Expire']");
        }

    }
}
