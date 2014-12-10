using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using QueueIT.Security.SeleniumTests.SiteModel.Extensions;
using QueueIT.Security.SeleniumTests.SiteModel.Implementations;

namespace QueueIT.Security.SeleniumTests.SiteModel.Pages
{
    public class ErrorPage : PageBase
    {
        public ErrorPage(ReferenceImplementation implementation) : base(implementation)
        {
        }

        public override bool IsAt()
        {
            IWebElement el = GetBodyElement("Error Page");

            return (el != null) && el.IsElementVisible(Config.DefaultTimeout);
        }

        public override string Url
        {
            get { return this.Implementation.Host + "error" + this.Implementation.PageExtension; }
        }
    }
}
