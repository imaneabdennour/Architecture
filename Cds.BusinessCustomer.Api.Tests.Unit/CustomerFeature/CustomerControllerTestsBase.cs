using Cds.BusinessCustomer.Api.CustomerFeature;
using Cds.BusinessCustomer.Api.CustomerFeature.Validation;
using Cds.BusinessCustomer.Domain.CustomerAggregate.Abstractions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cds.BusinessCustomer.Api.Tests.Unit.CustomerFeature
{
    public class CustomerControllerTestsBase
    {
        protected readonly Mock<ILogger<BusinessCustomerController>> _mockLogger;
        protected readonly Mock<IParametersHandler> _mockParametersHandler;
        protected readonly Mock<ICartegieRepository> _mockCartegieRepo;
        protected readonly BusinessCustomerController customerController;

        public CustomerControllerTestsBase()
        {
            _mockLogger = new Mock<ILogger<BusinessCustomerController>>();
            _mockCartegieRepo = new Mock<ICartegieRepository>();
            _mockParametersHandler = new Mock<IParametersHandler>();

            customerController = new BusinessCustomerController(_mockCartegieRepo.Object, _mockLogger.Object, _mockParametersHandler.Object);
        }
    }
}
