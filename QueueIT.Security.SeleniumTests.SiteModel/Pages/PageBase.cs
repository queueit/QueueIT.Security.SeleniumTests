using System;
using System.Linq;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using QueueIT.Security.SeleniumTests.SiteModel.Context;
using QueueIT.Security.SeleniumTests.SiteModel.Exceptions;
using QueueIT.Security.SeleniumTests.SiteModel.Extensions;
using QueueIT.Security.SeleniumTests.SiteModel.Extensions.CustomByLocators;
using QueueIT.Security.SeleniumTests.SiteModel.Implementations;

namespace QueueIT.Security.SeleniumTests.SiteModel.Pages
{
    public abstract class PageBase : DriverContext
    {
        public ReferenceImplementation Implementation { get; private set; }

        public string CurrentUrl
        {
            get { return this.Driver.Url; }
        }

        protected PageBase(ReferenceImplementation implementation)
        {
            this.Implementation = implementation;
        }

        public abstract bool IsAt();

        protected bool IsAt(Func<IWebElement> elementLocator)
        {
            bool expectedUrl = Driver.Url.ToLower().StartsWith(Url.ToLower());

            if (!expectedUrl)
                return false;

            IWebElement el = elementLocator.Invoke();

            return (el != null) && el.IsElementVisible(Config.DefaultTimeout);
        }

        public abstract string Url { get; }

        public virtual void Goto()
        {
            this.Goto(Config.DefaultTimeout);
        }

        public virtual void Goto(string queryString = null, params string[] expectedUrls)
        {
            this.Goto(Config.DefaultTimeout, queryString, expectedUrls);
        }

        public virtual void Goto(TimeSpan timeout, string queryString = null, params string[] expectedUrls)
        {
            this.Goto(timeout, this.Url, queryString, expectedUrls);
        }

        private void Goto(TimeSpan timeout, string url, string queryString = null, params string[] expectedUrls)
        {
            if (url == null)
                url = this.Url;

            Driver.Navigate().GoToUrl(queryString != null ? (url + "?" + queryString) : url);

            WebDriverWait wait = new WebDriverWait(Driver, timeout);
            wait.Until(a => expectedUrls != null && expectedUrls.Length > 0
                ? expectedUrls.Any(expectedUrl => Driver.Url.ToLower().StartsWith(expectedUrl.ToLower()))
                : Driver.Url.ToLower().StartsWith(url.ToLower()));
        }

        public void AssertIsAt()
        {
            if (!this.IsAt())
                throw new PageIsAtException(Driver.Url);
        }

        public KnownUserToken GetKnownUserToken(bool readCookie = false)
        {
            KnownUserToken token = GetKnownUserTokenFromQueryString();

            if (token == null && readCookie)
                return GetKnownUserTokenFromCookie();

            return token;
        }

        private KnownUserToken GetKnownUserTokenFromCookie()
        {
            return this.Implementation.ReadTokenFromCookie(this.GetCookie);
        }

        private KnownUserToken GetKnownUserTokenFromQueryString()
        {
            Match queueIdMatch = Regex.Match(this.Driver.Url, "[?&]q=([^&]+)");
            Match redirectTypeMatch = Regex.Match(this.Driver.Url, "[?&]rt=([^&]+)");

            if (!queueIdMatch.Success || !redirectTypeMatch.Success)
                return null;

            return new KnownUserToken()
            {
                QueueId = Guid.Parse(queueIdMatch.Groups[1].Value),
                RedirectType = redirectTypeMatch.Groups[1].Value
            };
        }

        public Cookie GetCookie(string name)
        {
            IOptions options = this.Driver.Manage();
            return options.Cookies.GetCookieNamed(name);
        }

        protected IWebElement GetBodyElement(string pageName)
        {
            return Driver.FindElement(new CssLocatorWithWait("body[id='" + pageName + "']"));
        }
    }

    public class KnownUserToken
    {
        public Guid QueueId { get; set; }
        public string RedirectType { get; set; }
    }
}
