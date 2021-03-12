using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using PactNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace Cds.BusinessCustomer.Tests.ProviderPact
{
    public class BusinessCustomerProviderTests : IDisposable
    {
        private readonly string _serviceUri;
        private readonly IWebHost _webHost;
        public BusinessCustomerProviderTests()
        {
            // uri for pact and service url
            _serviceUri = "http://localhost:5005";

            //// run the app
            //_webHost = WebHost.CreateDefaultBuilder()
            //    .UseUrls(_serviceUri)

        }

        [Fact]
        public  void Pact_Should_Be_Verified()
        {
            new PactVerifier(new PactVerifierConfig())
                .ServiceProvider("CustomerApi", _serviceUri)
                .HonoursPactWith("CustomerWeb")
                .PactUri(@"..\..\..\..\Pacts\customerweb-customerapi.json")
                .Verify();
        }


        private bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                   
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
