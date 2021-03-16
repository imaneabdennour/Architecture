using Cds.BusinessCustomer.Tests.ProviderPact.PactSetup;
using PactNet;
using Xunit;
using Xunit.Abstractions;

namespace Cds.BusinessCustomer.Tests.ProviderPact
{
    public class BusinessCustomerProviderTests : ContractTestBase
    {
        private const string Consumer1Contract = "customerweb-customerapi.json";

        public BusinessCustomerProviderTests(ITestOutputHelper output) : base(output) { }

        [Fact]
        public void EnsureAddressBookApiHonorsPactWithConsumer1()
        {
            new PactVerifier(_pactVerifierConfig)
                .ProviderState($"{ProviderUri}/provider-states")
                .ServiceProvider("CustomerApi", ProviderUri)
                .HonoursPactWith("CustomerWeb")
                .PactUri($"..\\..\\..\\..\\pacts\\{Consumer1Contract}")
                .Verify();
        }
    }
}