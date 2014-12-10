using System;
using System.Diagnostics;
using System.Threading;
using OpenQA.Selenium;

namespace QueueIT.Security.SeleniumTests.SiteModel.Extensions
{
    public static class IWebElementExtensions
    {
        public static bool IsElementVisible(this IWebElement el, TimeSpan timeout)
        {
            bool elementIsVisible = false;

            var watch = Stopwatch.StartNew();
            do
            {
                try
                {
                    if (el.Displayed)
                    {
                        elementIsVisible = true;
                        break;
                    }
                }
                catch (Exception) { /*Ignore*/ }

                Thread.Sleep(Config.PollingInterval);

            } while (watch.Elapsed <= timeout);

            return elementIsVisible;
        }

        public static bool IsElementEnabled(this IWebElement el, TimeSpan timeout)
        {
            bool elementIsEnabled = false;

            var watch = Stopwatch.StartNew();
            do
            {
                try
                {
                    if (el.Enabled)
                    {
                        elementIsEnabled = true;
                        break;
                    }
                }
                catch (Exception) { /*Ignore*/ }

                Thread.Sleep(Config.PollingInterval);

            } while (watch.Elapsed <= timeout);

            return elementIsEnabled;
        }

        public static bool IsTextContained(this IWebElement el, string expectedText, TimeSpan timeout)
        {
            bool textContained = false;

            var watch = Stopwatch.StartNew();
            do
            {
                try
                {
                    if (el.Text.Contains(expectedText))
                    {
                        textContained = true;
                        break;
                    }
                }
                catch (Exception) { /*Ignore*/ }

                Thread.Sleep(Config.PollingInterval);

            } while (watch.Elapsed <= timeout);

            return textContained;
        }

        public static void ScrollToVisible(this IWebElement el, IWebDriver driver)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript(String.Format("window.scrollTo(0, {0});", el.Location.Y));
        }
    }
}
