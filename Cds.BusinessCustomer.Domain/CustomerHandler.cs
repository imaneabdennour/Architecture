using Microsoft.Extensions.Logging;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Cds.BusinessCustomer.Domain.CustomerAggregate.Abstractions;

namespace Cds.BusinessCustomer.Domain
{
    public class CustomerHandler : ICustomerHandler
    {
        private readonly ILogger<CustomerHandler> _logger;

        public CustomerHandler(ILogger<CustomerHandler> logger)
        {
            _logger = logger;
        }

        public (bool, string)  Validate(string siret)
        {
            if (siret.Length != 14)
            {
                _logger.LogError($"Failed to retreive customer with siret = {siret}, Siret string should be of length 14");
                //throw new InvalidArgumentException(nameof(siret));
                //    return BadRequest(new { code = "400", message = "Invalid Siret - should be of length 14" });
                return (false, "Invalid Siret - should be of length 14");
            }
            return (true, null);
        }

        public (bool, string) Validate(string socialreason, string zipcode)
        {
            if (socialreason == null && zipcode == null)
            {
                _logger.LogError("Failed to retreive customers - You should specify Siret OR SocialReason and ZipCode");
                return (false, "You should enter Siret OR SocialReason and ZipCode");
            }
            if (socialreason == null || zipcode == null)
            {
                _logger.LogError("Failed to retreive customers - You should specify both SocialReason and ZipCode");
                return (false, "You should enter both SocialReason and ZipCode" );
            }
            return (true, null);
        }
    }
}
