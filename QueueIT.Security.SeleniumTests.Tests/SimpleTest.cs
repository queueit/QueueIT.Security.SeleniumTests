using System.Threading;
using QueueIT.Security.SeleniumTests.SiteModel;
using QueueIT.Security.SeleniumTests.SiteModel.Implementations;
using QueueIT.Security.SeleniumTests.SiteModel.Pages;
using Xunit;
using Xunit.Extensions;

namespace QueueIT.Security.SeleniumTests.Tests
{
    public class SimpleTest : TestBase
    {
        [Theory]
        [PropertyData("AllReferenceImplementations")]
        public void Simple_VerifyKnownUserToken_Test(ReferenceImplementation referenceImplementation)
        {
            var simplePage = new SimplePage(referenceImplementation);

            simplePage.Goto(Config.QueueTimeout);

            simplePage.AssertIsAt();

            // expect to find known user token in querystring
            var token = simplePage.GetKnownUserToken();

            Assert.NotNull(token);
            Assert.Equal("Queue", token.RedirectType);

            // reload page and expect not to be enqueued
            simplePage.Goto(Config.ShortTimeout);
            simplePage.AssertIsAt();

            // expect not to have token in querystring
            Assert.Null(simplePage.GetKnownUserToken());

            // expect token in cookie
            Assert.NotNull(simplePage.GetKnownUserToken(true));

            // reload page  again to verify cookie reset (new hash and timestamp) and expect not to be enqueued
            simplePage.Goto(Config.ShortTimeout);
            simplePage.AssertIsAt();

            // expect not to have token in querystring
            Assert.Null(simplePage.GetKnownUserToken());

            // reload page  again to verify cookie reset (new hash and timestamp) and expect not to be enqueued
            simplePage.Goto(Config.ShortTimeout);
            simplePage.AssertIsAt();

            // expect not to have token in querystring
            Assert.Null(simplePage.GetKnownUserToken());

            // reload page  again to verify cookie reset (new hash and timestamp) and expect not to be enqueued
            simplePage.Goto(Config.ShortTimeout);
            simplePage.AssertIsAt();

            // expect not to have token in querystring
            Assert.Null(simplePage.GetKnownUserToken());
        }

        [Theory]
        [PropertyData("AllReferenceImplementations")]
        public void Simple_QueryStringWithInvalidChars_Test(ReferenceImplementation referenceImplementation)
        {
            var invalidQueryString = "invalidchars=asd|rte&x?dfgdfg{dfg}";

            var simplePage = new SimplePage(referenceImplementation);

            simplePage.Goto(Config.QueueTimeout, invalidQueryString);

            simplePage.AssertIsAt();

            Assert.True(simplePage.CurrentUrl.Contains(invalidQueryString));

            // reload page and expect not to be enqueued
            simplePage.Goto(Config.ShortTimeout);
            simplePage.AssertIsAt();

            // expect not to have token in querystring
            Assert.Null(simplePage.GetKnownUserToken());

            // expect token in cookie
            Assert.NotNull(simplePage.GetKnownUserToken(true));

        }

        [Theory]
        [PropertyData("AspNetWebFormsReferenceImplementations")]
        public void Simple_CancelKnownUserToken_Test(ReferenceImplementation referenceImplementation)
        {
            var simplePage = new SimplePage(referenceImplementation);
            var queuePage = new QueuePage(referenceImplementation, "simple");
            var cancelPage = new CancelPage(referenceImplementation);

            simplePage.Goto(Config.QueueTimeout);

            simplePage.AssertIsAt();

            // expect to find known user token in querystring
            var token = simplePage.GetKnownUserToken();

            Assert.NotNull(token);
            Assert.Equal("Queue", token.RedirectType);

            simplePage.CancelToken();

            cancelPage.AssertIsAt();

            // do not expect token in cookie
            Assert.Null(simplePage.GetKnownUserToken(true));

            // reload page and expect to be enqueued
            simplePage.Goto(Config.DefaultTimeout, null, queuePage.Url);
            queuePage.AssertIsAt();

            // expect not to have token in querystring
            Assert.Null(simplePage.GetKnownUserToken());

            // expect token not in cookie
            Assert.Null(simplePage.GetKnownUserToken(true));
        }

        [Theory]
        [PropertyData("AspNetWebFormsReferenceImplementations")]
        public void Simple_ExpireKnownUserToken_Test(ReferenceImplementation referenceImplementation)
        {
            var simplePage = new SimplePage(referenceImplementation);
            var queuePage = new QueuePage(referenceImplementation, "simple");
            var expirePage = new ExpirePage(referenceImplementation);

            simplePage.Goto(Config.QueueTimeout);

            simplePage.AssertIsAt();

            // expect to find known user token in querystring
            var token = simplePage.GetKnownUserToken();

            Assert.NotNull(token);
            Assert.Equal("Queue", token.RedirectType);

            simplePage.ExpireToken();

            expirePage.AssertIsAt();

            // expect token in cookie
            Assert.NotNull(simplePage.GetKnownUserToken(true));

            // reload page and expect not to be enqueued
            simplePage.Goto();
            simplePage.AssertIsAt();

            simplePage.ExpireToken();

            expirePage.AssertIsAt();

            //Wait for token to expire
            Thread.Sleep(16000);

            // do not expect token in cookie
            Assert.Null(simplePage.GetKnownUserToken(true));
        }

        [Theory]
        [PropertyData("AllReferenceImplementations")]
        public void Simple_InvalidKnownUserToken_Test(ReferenceImplementation referenceImplementation)
        {
            InvalidKnownUserTokenTest(
               referenceImplementation,
               "q=0376e8fd-15f7-46dd-aa27-5d06a446af6d&p=05104ca2-3002-48f3-9022-d468cb047eda&ts=1414744732&c=ticketania&e=simple&rt=Queue&h=bcf9b9287be19e30ad640e6c4b747e65");
        }

        [Fact]
        public void Simple_ExpiredKnownUserToken_WebForms_Test()
        {
            InvalidKnownUserTokenTest(
                ReferenceImplementation.AspNetWebForms,
                "q=0376e8fd-15f7-46dd-aa27-5d06a446af6d&p=05104ca2-3002-48f3-9022-d468cb047eda&ts=1414744732&c=ticketania&e=simple&rt=Queue&h=acf9b9287be19e30ad640e6c4b747e63");
        }

        [Fact]
        public void Simple_ExpiredKnownUserToken_Mvc_Test()
        {
            InvalidKnownUserTokenTest(
                ReferenceImplementation.AspNetMvc,
                "q=07ff3c87-a015-49e8-a052-b2929e5777de&p=add0cc62-410b-4bdf-a08b-a46172080a13&ts=1414745120&c=ticketania&e=simple&rt=Queue&h=488b4368e4d7e0e5e9c608f380eba5cc");
        }

        private void InvalidKnownUserTokenTest(ReferenceImplementation referenceImplementation, string queryString)
        {
            var simplePage = new SimplePage(referenceImplementation);
            var errorPage = new ErrorPage(referenceImplementation);

            simplePage.Goto(
                Config.DefaultTimeout,
                queryString,
                simplePage.Url,
                errorPage.Url);

            errorPage.AssertIsAt();
        }
    }
}
