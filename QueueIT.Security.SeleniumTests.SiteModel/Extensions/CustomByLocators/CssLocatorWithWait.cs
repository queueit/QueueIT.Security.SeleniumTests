using System;
using System.Collections.ObjectModel;
using OpenQA.Selenium;

namespace QueueIT.Security.SeleniumTests.SiteModel.Extensions.CustomByLocators
{
    public class CssLocatorWithWait : LocatorWithWaitBase
    {
        private readonly string _cssString;
        private readonly TimeSpan _timeout;

        public CssLocatorWithWait(string css)
            : this(css, Config.DefaultTimeout) { }

        public CssLocatorWithWait(string css, TimeSpan timeout)
        {
            this._cssString = css;
            this._timeout = timeout;
        }

        public override IWebElement FindElement(ISearchContext context)
        {
            return base.FindElement(context, CssSelector(_cssString), _timeout);
        }

        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            return base.FindElements(context, CssSelector(_cssString), _timeout);
        }
    }
}
