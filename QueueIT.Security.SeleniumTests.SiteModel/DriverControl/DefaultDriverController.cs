using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace QueueIT.Security.SeleniumTests.SiteModel.DriverControl
{
    public class DefaultDriverController : IDriverController
    {
        private ThreadLocal<IWebDriver> threadDriverStore = new ThreadLocal<IWebDriver>();

        public virtual void SetupDriver(DriverSelector driverSelection)
        {
            switch (driverSelection)
            {
                case DriverSelector.Chrome:
                    threadDriverStore.Value = CreateChromeDriver();
                    break;

                default:
                    throw new Exception("Selection not supported: " + driverSelection);
            }
        }

        public virtual void TestCleanup()
        {
            IWebDriver driver = this.GetDriver();
            this.ClearDriver();
            driver.Quit();
        }

        internal IWebDriver GetDriver()
        {
            IWebDriver d = threadDriverStore.Value;

            if (d == null)
            {
                throw new NullReferenceException("Attempt to get NULL driver");
            }
            else
            {
                return d;
            }
        }

        internal void ClearDriver()
        {
            threadDriverStore.Value = null;
        }

        private IWebDriver CreateChromeDriver()
        {
            IWebDriver driver = new ChromeDriver();
            
            driver.Manage().Timeouts().SetScriptTimeout(Config.DefaultTimeout);
            //driver.Manage().Window.Maximize();
            driver.Manage().Window.Position = new System.Drawing.Point(0, 0);
            driver.Manage().Window.Size = new System.Drawing.Size(1280, 1024);

            return driver;
        }
    }
}
