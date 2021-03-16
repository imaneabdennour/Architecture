using System;
using System.Collections.Generic;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using PactNet;
using PactNet.Infrastructure.Outputters;
using Xunit.Abstractions;

namespace Cds.BusinessCustomer.Tests.ProviderPact.PactSetup
{
    public abstract class ContractTestBase : IDisposable
    {
        protected const string ProviderUri = "https://127.0.0.1:9310";
        protected readonly PactVerifierConfig _pactVerifierConfig;
        private bool _disposedValue;
        private readonly ITestOutputHelper _output;
        private readonly IWebHost _webHost;

        protected ContractTestBase(ITestOutputHelper output)
        {
            _output = output;
            _webHost = WebHost.CreateDefaultBuilder()
                .UseStartup<TestStartup>()
                .UseKestrel()
                .UseUrls(ProviderUri)
                .Build();
            _webHost.Start();

            _pactVerifierConfig = new PactVerifierConfig
            {
                // We default to using a ConsoleOutput, but xUnit 2 does not capture the console output, so a custom outputter is required.
                Outputters =
                    new List<IOutput> 
                        {
                            new XUnitOutput(_output)
                        },               
                Verbose = true //Output verbose verification logs to the test output
            };
        }

        private void Dispose(bool disposing)
        {
            if (_disposedValue) return;
            if (disposing)
            {
                _webHost.StopAsync().GetAwaiter().GetResult();
                _webHost.Dispose();
            }

            _disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
