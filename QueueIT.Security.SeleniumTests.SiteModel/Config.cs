using System;
using System.Configuration;
using System.Reflection;

namespace QueueIT.Security.SeleniumTests.SiteModel
{
    public static class Config
    {
        // App.Config timeout conigs
        private static TimeSpan _pollingInterval;
        public static TimeSpan PollingInterval
        {
            get
            {
                if (_pollingInterval == TimeSpan.Zero)
                {
                    int milliseconds;
                    bool result = Int32.TryParse(ConfigurationManager.AppSettings["PollingInterval"], out milliseconds);

                    if (!result)
                    {
                        throw new Exception(MethodBase.GetCurrentMethod() + " not set");
                    }

                    _pollingInterval = TimeSpan.FromMilliseconds(milliseconds);
                }

                return _pollingInterval;
            }
        }

        private static TimeSpan _shortTimeout;
        public static TimeSpan ShortTimeout
        {
            get
            {
                if (_pollingInterval == TimeSpan.Zero)
                {
                    int milliseconds;
                    bool result = Int32.TryParse(ConfigurationManager.AppSettings["ShortTimeout"], out milliseconds);

                    if (!result)
                    {
                        throw new Exception(MethodBase.GetCurrentMethod() + " not set");
                    }

                    _shortTimeout = TimeSpan.FromMilliseconds(milliseconds);
                }

                return _shortTimeout;
            }
        }

        private static TimeSpan _defaultTimeout;
        public static TimeSpan DefaultTimeout
        {
            get
            {
                if (_defaultTimeout == TimeSpan.Zero)
                {
                    int milliseconds;
                    bool result = Int32.TryParse(ConfigurationManager.AppSettings["DefaultTimeout"], out milliseconds);

                    if (!result)
                    {
                        throw new Exception(MethodBase.GetCurrentMethod() + " not set");
                    }

                    _defaultTimeout = TimeSpan.FromMilliseconds(milliseconds);
                }

                return _defaultTimeout;
            }
        }

        private static TimeSpan _longTimeout;
        public static TimeSpan LongTimeout
        {
            get
            {
                if (_longTimeout == TimeSpan.Zero)
                {
                    int milliseconds;
                    bool result = Int32.TryParse(ConfigurationManager.AppSettings["LongTimeout"], out milliseconds);

                    if (!result)
                    {
                        throw new Exception(MethodBase.GetCurrentMethod() + " not set");
                    }

                    _longTimeout = TimeSpan.FromMilliseconds(milliseconds);
                }

                return _longTimeout;
            }
        }

        private static TimeSpan _queueTimeOut;
        public static TimeSpan QueueTimeout
        {
            get
            {
                if (_queueTimeOut == TimeSpan.Zero)
                {
                    int milliseconds;
                    bool result = Int32.TryParse(ConfigurationManager.AppSettings["QueueTimeout"], out milliseconds);

                    if (!result)
                    {
                        throw new Exception(MethodBase.GetCurrentMethod() + " not set");
                    }

                    _queueTimeOut = TimeSpan.FromMilliseconds(milliseconds);
                }

                return _queueTimeOut;
            }
        } 

    }
}
