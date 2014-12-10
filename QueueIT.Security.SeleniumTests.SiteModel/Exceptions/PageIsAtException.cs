using System;

namespace QueueIT.Security.SeleniumTests.SiteModel.Exceptions
{
    public class PageIsAtException : Exception
    {
        public PageIsAtException(string actualUrl)
            : base(string.Format("Actual url was {0}", actualUrl))
        {
            
        }
    }
}