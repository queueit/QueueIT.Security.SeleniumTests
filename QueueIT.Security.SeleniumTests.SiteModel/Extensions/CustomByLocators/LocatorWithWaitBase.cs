using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;
using OpenQA.Selenium;

namespace QueueIT.Security.SeleniumTests.SiteModel.Extensions.CustomByLocators
{
    public abstract class LocatorWithWaitBase : By
    {
        public IWebElement FindElement(ISearchContext context, By by, TimeSpan timeout)
        {
            IWebElement el = null;

            var stopwatch = Stopwatch.StartNew();
            do
            {
                try
                {
                    el = context.FindElement(by);

                    if (el != null)
                    {
                        return el;
                    }
                }
                catch (Exception) { /* Ignore */ }
                
                Thread.Sleep(Config.PollingInterval);

            } while (stopwatch.Elapsed <= timeout);

            throw new NotFoundException(String.Format("Element not found [{0}], Timeout: {1}", by, timeout));
        }

        public ReadOnlyCollection<IWebElement> FindElements(ISearchContext context, By by, TimeSpan timeout)
        {
            ReadOnlyCollection<IWebElement> elements = null;

            var stopwatch = Stopwatch.StartNew();
            do
            {
                try
                {
                    elements = context.FindElements(by);

                    if (elements.Count > 0)
                    {
                        return elements;
                    }
                }
                catch (Exception) { /* Ignore */ }

                Thread.Sleep(Config.PollingInterval);

            } while (stopwatch.Elapsed <= timeout);

            throw new NotFoundException(String.Format("Elements not found [{0}], Timeout: {1}", by, timeout));
        }
    }
}
