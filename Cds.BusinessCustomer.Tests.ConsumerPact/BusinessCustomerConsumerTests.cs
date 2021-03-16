using Cds.BusinessCustomer.Api.CustomerFeature.ViewModels;
using Newtonsoft.Json;
using PactNet.Mocks.MockHttpService;
using PactNet.Mocks.MockHttpService.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Cds.BusinessCustomer.Tests.ConsumerPact
{
    public class BusinessCustomerConsumerTests : IClassFixture<CustomerApiMock>
    {
        private readonly IMockProviderService _mockProviderService;
        private readonly string _serviceUri;
        public BusinessCustomerConsumerTests(CustomerApiMock fixture)
        {
            _mockProviderService = fixture.mockProviderService;
            _serviceUri = fixture.ServiceUri;
            _mockProviderService.ClearInteractions();
        }

        // what are we going to test

        [Fact]
        public async Task Given_Valid_Id_Customer_Should_Be_Returned()
        {
            var customerId = "a40354012";
            
            // ARRANGE 
            _mockProviderService
                .Given("Customer")
                .UponReceiving("A GET request to retreive business customer details")
                .With(new ProviderServiceRequest
                {
                    Method = HttpVerb.Get,
                    Path = $"/business-customer-information/{customerId}"
                })
                .WillRespondWith(new ProviderServiceResponse
                {
                    Status = 200,
                    Headers = new Dictionary<string, object>
                    {
                        { "Content-Type","application/json; charset=utf-8"},
                    },
                    Body = new SingleCustomerViewModel
                    {
                        Name = "UBER PARTNER SUPPORT FRANCE SAS",
                        Siret = "81999478100022",
                        NafCode = "8299Z",
                        ZipCode = "33000",
                        Adress = "",
                        Phone = null,
                        City = "UBER PARTNER SUPPORT FRANCE SAS",
                        SocialReason = "UBER PARTNER SUPPORT FRANCE SAS",
                        Civility = null
                    }
                });


            // Create HTTP call 
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"{_serviceUri}/business-customer-information/{customerId}");
            var json = await response.Content.ReadAsStringAsync();
            var customerDetails = JsonConvert.DeserializeObject<SingleCustomerViewModel>(json);

            string expected = "UBER PARTNER SUPPORT FRANCE SAS";
            Assert.Equal(expected, customerDetails.Name);
        }
    }
}