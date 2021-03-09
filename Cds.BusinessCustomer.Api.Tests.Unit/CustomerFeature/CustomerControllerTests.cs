using Cds.BusinessCustomer.Api.CustomerFeature;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Linq;
using Cds.BusinessCustomer.Domain.CustomerAggregate;

namespace Cds.BusinessCustomer.Api.Tests.Unit.CustomerFeature
{
    public class CustomerControllerTests : CustomerControllerTestsBase
    {
        [Fact]
        public async Task BusinessCustomerInformation_MultipleSearch_WithNoSocialReason_ReturnsBadRequest()
        {
            //_mockParametersHandler.Setup(x => x.GetCompetingOfferChangesAsync(It.IsAny<long>())).ReturnsAsync(competingOfferChanges);
            var result = await customerController.SearchByMultipleCriteria(null, "454", null).ConfigureAwait(false);

            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task BusinessCustomerInformation_MultipleSearch_WithShortSiret_ReturnsBadRequest()
        {
            //_mockParametersHandler.Setup(x => x.GetCompetingOfferChangesAsync(It.IsAny<long>())).ReturnsAsync(competingOfferChanges);
            var result = await customerController.SearchByMultipleCriteria(null, "454", "12345678945632").ConfigureAwait(false);

            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task BusinessCustomerInformation_MultipleSearch_WithN_ReturnsBadRequest()
        {
            //valid sr and zc

            List<Customer> competingOfferChanges = new List<Customer>() { new Customer() };
            string socialReason = "123";
            string zipCode = "456";
            _mockCartegieRepo.Setup(x => x.GetInfosByCriteria(socialReason, zipCode))
               .ReturnsAsync(competingOfferChanges);

           // _mockCartegieRepo.Setup(x => x.GetInfos_MultipleSearch(It .IsAny<string>())).ReturnsAsync(competingOfferChanges);
            //_mockParametersHandler.Setup(x => x.GetCompetingOfferChangesAsync(It.IsAny<long>())).ReturnsAsync(competingOfferChanges);
            var result = await customerController.SearchByMultipleCriteria("12345", "52", null).ConfigureAwait(false);

            ObjectResult objectReponse = Assert.IsType<ObjectResult>(result);
            Assert.Equal(200, objectReponse.StatusCode);
        }
    }
}
