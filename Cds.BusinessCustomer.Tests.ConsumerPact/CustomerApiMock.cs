using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PactNet;
using PactNet.Mocks.MockHttpService;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cds.BusinessCustomer.Tests.ConsumerPact
{
    public class CustomerApiMock : IDisposable
    {
        private readonly int _servicePort = 9222;
        private readonly IPactBuilder _pactBuilder;
        public string ServiceUri => $"http://localhost:{_servicePort}";
        public IMockProviderService mockProviderService { get; }

        // create HTTP server
        public CustomerApiMock()
        {
            var pactConfig = new PactConfig
            {
                SpecificationVersion = "2.0.0",
                // location for pact files
                PactDir = @"..\..\..\..\Pacts",    // where to store the PACT
                LogDir = @".\pact_logs"
            };
            _pactBuilder = new PactBuilder(pactConfig)
                .ServiceConsumer("CustomerWeb")                  // a voir
                .HasPactWith("CustomerApi");

            // port for ruby server to use for mocking
            mockProviderService = _pactBuilder.MockService(_servicePort, new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()     // pr la sérialization - json
            }
            );  
        }


        private bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // generate pacts and logs
                    _pactBuilder.Build();
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