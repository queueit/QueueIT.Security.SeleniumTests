namespace QueueIT.Security.SeleniumTests.SiteModel.DriverControl
{
    public interface IDriverController
    {
        /// <summary>
        /// Relate a driver instance to current thread
        /// </summary>
        /// <param name="driver"></param>
        void SetupDriver(DriverSelector driver);

        /// <summary>
        /// Cleanup of driver after test has finished
        /// </summary>
        void TestCleanup();
    }
}
