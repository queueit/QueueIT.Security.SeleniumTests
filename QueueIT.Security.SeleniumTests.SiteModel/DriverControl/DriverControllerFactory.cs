using System;

namespace QueueIT.Security.SeleniumTests.SiteModel.DriverControl
{
    public class DriverControllerFactory
    {
        private static volatile IDriverController _instance;
        private static readonly object s_syncRoot = new Object();

        public static IDriverController GetController()
        {
            if (_instance == null)
            {
                lock (s_syncRoot)
                {
                    if (_instance == null)
                    {
                        _instance = new DefaultDriverController();
                    }
                }
            }

            return _instance;
        }
    }
}
