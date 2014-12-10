using System;
using System.Collections.Generic;
using QueueIT.Security.SeleniumTests.SiteModel.DriverControl;
using QueueIT.Security.SeleniumTests.SiteModel.Implementations;

namespace QueueIT.Security.SeleniumTests.Tests
{
    public abstract class TestBase : IDisposable
    {
        public TestBase()
        {
            DriverControllerFactory.GetController().SetupDriver(DriverSelector.Chrome);
        }

        public void Dispose()
        {
            DriverControllerFactory.GetController().TestCleanup();
        }

        public static IEnumerable<object[]> AllReferenceImplementations
        {
            get { return new[]
            {
                new object[] { ReferenceImplementation.AspNetWebForms },
                new object[] { ReferenceImplementation.AspNetMvc },
                new object[] { ReferenceImplementation.JavaEE },
                new object[] { ReferenceImplementation.Php },
            };
            }
        }

        public static IEnumerable<object[]> AspNetWebFormsReferenceImplementations
        {
            get
            {
                return new[]
                {
                    new object[] { ReferenceImplementation.AspNetWebForms },
                };
            }
        }

        public static IEnumerable<object[]> AspNetMvcReferenceImplementations
        {
            get
            {
                return new[]
                {
                    new object[] { ReferenceImplementation.AspNetMvc },
                };
            }
        }

        public static IEnumerable<object[]> JaveEEReferenceImplementations
        {
            get
            {
                return new[]
            {
                new object[] { ReferenceImplementation.JavaEE },
            };
            }
        }

        public static IEnumerable<object[]> PhpReferenceImplementations
        {
            get
            {
                return new[]
            {
                new object[] { ReferenceImplementation.Php },
            };
            }
        }
    }
}
