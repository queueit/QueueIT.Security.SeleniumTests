using System;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using QueueIT.Security.SeleniumTests.SiteModel.DriverControl;
using QueueIT.Security.SeleniumTests.SiteModel.Extensions;
using QueueIT.Security.SeleniumTests.SiteModel.Extensions.CustomByLocators;

namespace QueueIT.Security.SeleniumTests.SiteModel.Context
{
    public abstract class DriverContext
    {
        protected IWebDriver Driver;

        protected DriverContext()
        {
            this.Driver = ((DefaultDriverController)DriverControllerFactory.GetController()).GetDriver();
        }

        protected DriverContext(IWebDriver driver)
        {
            this.Driver = driver;
        }

        /// <summary>
        /// This method will stall until the page has been reloaded. Call it when button or control triggers a postback (i.e. not using normal page.Goto() call).
        /// </summary>
        /// <param name="maxWaitTimeInSeconds"></param>
        public void WaitForPageLoad(int maxWaitTimeInSeconds = 10)
        {
            // Ressource : http://automationoverflow.wordpress.com/2013/07/27/waiting-for-page-load-to-complete/
            
            string state = string.Empty;
            
            try
            {
                WebDriverWait wait = new WebDriverWait(this.Driver, TimeSpan.FromSeconds(maxWaitTimeInSeconds));

                //Checks every 500 ms whether predicate returns true if returns exit otherwise keep trying till it returns ture
                wait.Until(d =>
                {
                    try
                    {
                        state = ((IJavaScriptExecutor)this.Driver).ExecuteScript(@"return document.readyState").ToString();
                    }
                    catch (InvalidOperationException)
                    {
                        //Ignore
                    }
                    catch (NoSuchWindowException)
                    {
                        //when popup is closed, switch to last windows
                        this.Driver.SwitchTo().Window(this.Driver.WindowHandles.Last());
                    }
                    //In IE7 there are chances we may get state as loaded instead of complete
                    return (state.Equals("complete", StringComparison.InvariantCultureIgnoreCase) || state.Equals("loaded", StringComparison.InvariantCultureIgnoreCase));

                });
            }
            catch (TimeoutException)
            {
                //sometimes Page remains in Interactive mode and never becomes Complete, then we can still try to access the controls
                if (!state.Equals("interactive", StringComparison.InvariantCultureIgnoreCase))
                    throw;
            }
            catch (NullReferenceException)
            {
                //sometimes Page remains in Interactive mode and never becomes Complete, then we can still try to access the controls
                if (!state.Equals("interactive", StringComparison.InvariantCultureIgnoreCase))
                    throw;
            }
            catch (WebDriverException)
            {
                if (this.Driver.WindowHandles.Count == 1)
                {
                    this.Driver.SwitchTo().Window(this.Driver.WindowHandles[0]);
                }
                state = ((IJavaScriptExecutor)this.Driver).ExecuteScript(@"return document.readyState").ToString();
                if (!(state.Equals("complete", StringComparison.InvariantCultureIgnoreCase) || state.Equals("loaded", StringComparison.InvariantCultureIgnoreCase)))
                    throw;
            }
        }

        /// <summary>
        /// This method will stall until all ajax has finished. Call it on pages where data is loaded async.
        /// </summary>
        /// <param name="maxWaitTimeInSeconds">Default 5 secs</param>
        public void WaitForAjaxToComplete(int maxWaitTimeInSeconds = 10)
        {
            WebDriverWait wait = new WebDriverWait(this.Driver, TimeSpan.FromSeconds(maxWaitTimeInSeconds));

            wait.Until(d =>
            {
                return (bool)(this.Driver as IJavaScriptExecutor).ExecuteScript("return jQuery.active == 0");
            });
        }

        protected IWebElement GetElement(string selector, double visibleSecs = 5)
        {
            Driver.FindElement(new CssLocatorWithWait(selector)).IsElementVisible(TimeSpan.FromSeconds(visibleSecs));

            return Driver.FindElement(new CssLocatorWithWait(selector));
        }

        public void Click(string selector)
        {
            IWebElement link = GetElement(selector);
            link.Click();
        }

        public void ClickButton(string selector)
        {
            Click(selector);
        }
    }
    public enum CheckBoxValue
    {
        Unchecked,
        Checked,
    }
}
