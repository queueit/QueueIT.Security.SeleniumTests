using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OpenQA.Selenium;
using QueueIT.Security.SeleniumTests.SiteModel.Pages;

namespace QueueIT.Security.SeleniumTests.SiteModel.Implementations
{
    public class ReferenceImplementation
    {
        public static ReferenceImplementation AspNetWebForms
        {
            get
            {
                return new ReferenceImplementation()
                {
                    Host = "http://knownusertest.queue-it.net/",
                    PageExtension = ".aspx",
                    _readTokenFromCookieFunc = (getCookieFunc) =>
                    {
                        Cookie cookie = getCookieFunc.Invoke("QueueITAccepted-SDFrts345E-ticketania-simple");
                        if (cookie == null)
                            return null;
                        
                        return new KnownUserToken()
                        {
                            QueueId = Guid.Parse(Regex.Match(cookie.Value, "QueueId=([^&]+)").Groups[1].Value),
                            RedirectType = Regex.Match(cookie.Value, "RedirectType=([^&]+)").Groups[1].Value
                        };
                    }
                };
            }
        }

        public static ReferenceImplementation AspNetMvc
        {
            get
            {
                return new ReferenceImplementation()
                {
                    Host = "http://knownusertest.queue-it.net:8081/",
                    PageExtension = string.Empty,
                    _readTokenFromCookieFunc = AspNetWebForms._readTokenFromCookieFunc
                };
            }
        }

        public static ReferenceImplementation JavaEE
        {
            get
            {
                return new ReferenceImplementation()
                {
                    Host = "http://knownusertest.queue-it.net:8080/QueueIT.Security.Examples.Java/",
                    PageExtension = ".jsp",
                    _readTokenFromCookieFunc = (getCookieFunc) =>
                    {
                        Cookie queueIdCookie = getCookieFunc.Invoke("QueueITAccepted-SDFrts345E-ticketania-simple-QueueId");
                        Cookie redirectTypeCookie = getCookieFunc.Invoke("QueueITAccepted-SDFrts345E-ticketania-simple-RedirectType");
                        if (queueIdCookie == null)
                            return null;

                        return new KnownUserToken()
                        {
                            QueueId = Guid.Parse(queueIdCookie.Value),
                            RedirectType = redirectTypeCookie.Value
                        };
                    }
                };
            }
        }

        public static ReferenceImplementation Php
        {
            get
            {
                return new ReferenceImplementation()
                {
                    Host = "http://knownusertest.queue-it.net:8089/",
                    PageExtension = ".php",
                    _readTokenFromCookieFunc = (getCookieFunc) =>
                    {
                        Cookie queueIdCookie = getCookieFunc.Invoke("QueueITAccepted-SDFrts345E-ticketania-simple[QueueId]");
                        Cookie redirectTypeCookie = getCookieFunc.Invoke("QueueITAccepted-SDFrts345E-ticketania-simple[RedirectType]");
                        if (queueIdCookie == null)
                            return null;

                        return new KnownUserToken()
                        {
                            QueueId = Guid.Parse(queueIdCookie.Value),
                            RedirectType = MapPhpRedirectType(redirectTypeCookie.Value)
                        };
                    }
                };
            }
        }


        public string Host { get; private set; }


        private Func<Func<string, Cookie>, KnownUserToken> _readTokenFromCookieFunc;

        public string PageExtension { get; private set; }

        public KnownUserToken ReadTokenFromCookie(Func<string, Cookie> getCookieFunc)
        {
            return this._readTokenFromCookieFunc.Invoke(getCookieFunc);
        }

        private static string MapPhpRedirectType(string intValue)
        {
            if (string.IsNullOrEmpty(intValue))
                return "Unknown";

            switch (intValue)
            {
                case "1" :
                    return "Queue";
                case "2":
                    return "Unknown";
                case "3":
                    return "AfterEvent";
                case "4":
                    return "Disabled";
                case "5":
                    return "DirectLink";
                case "6":
                    return "Idle";
                default:
                    return "Unknown";
            }
        }
    }
}
