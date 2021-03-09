using Cds.BusinessCustomer.Domain.CustomerAggregate.Abstractions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cds.BusinessCustomer.Infrastructure.CustomerRepository
{
    public class CartegieConfiguration : ICartegieConfiguration
    {
        public string BaseUrl { get; }
        public string ApiKey { get; }

        public string BySiret{ get; }
        public string ByMultiple { get; }
        public string ById { get; }

        private readonly IConfiguration _configuration;

        public CartegieConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;

            BaseUrl = _configuration.GetValue<string>("CartegieConfiguration:baseUrl");
            BySiret = _configuration.GetValue<string>("CartegieConfiguation:BySiret");
            ByMultiple = _configuration.GetValue<string>("CartegieConfiguation:ByMultiple");
            ById = _configuration.GetValue<string>("CartegieConfiguation:ById");
        }

    }
}
