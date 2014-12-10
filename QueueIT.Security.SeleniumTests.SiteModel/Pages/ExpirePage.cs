using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QueueIT.Security.SeleniumTests.SiteModel.Implementations;

namespace QueueIT.Security.SeleniumTests.SiteModel.Pages
{
    public class ExpirePage : PageBase
    {
        public ExpirePage(ReferenceImplementation implementation)
            : base(implementation)
        {
        }

        public override bool IsAt()
        {
            return base.IsAt(() => GetBodyElement("Expire Validation"));
        }

        public override string Url
        {
            get { return this.Implementation.Host + "Expire" + this.Implementation.PageExtension; }
        }
    }
}
