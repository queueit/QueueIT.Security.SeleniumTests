using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QueueIT.Security.SeleniumTests.SiteModel.Extensions.CustomByLocators;
using QueueIT.Security.SeleniumTests.SiteModel.Implementations;

namespace QueueIT.Security.SeleniumTests.SiteModel.Pages
{
    public class QueuePage : PageBase
    {
        private readonly string _eventId;

        public QueuePage(ReferenceImplementation implementation, string eventId) : base(implementation)
        {
            _eventId = eventId;
        }

        public override bool IsAt()
        {
            return base.IsAt(() => Driver.FindElement(new CssLocatorWithWait("body[data-pageid='queue']")));
        }

        public override string Url
        {
            get { return "http://" + this._eventId + "-ticketania.queue-it.net"; }
        }
    }
}
