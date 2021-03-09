using Microsoft.Extensions.Logging;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cds.BusinessCustomer.Api.CustomerFeature.Validation
{
    public class ParametersHandler : IParametersHandler
    {
        private readonly ILogger<ParametersHandler> _logger;

        public ParametersHandler(ILogger<ParametersHandler> logger)
        {
            _logger = logger;
        }
        
        /// <summary>
        /// Validation for parameter : siret
        /// </summary>
        /// <param name="siret"></param>
        /// <returns></returns>
        public (bool, string)  Validate(string siret)
        {
            if (siret.Length != 14)
            {
                _logger.LogError($"Failed to retreive customer with siret = {siret}, Siret string should be of length 14");
                
                return (false, "Invalid Siret - should be of length 14");
            }
            return (true, null);
        }

        /// <summary>
        /// Validation for parameters : socialreason and zipcode
        /// </summary>
        /// <param name="socialreason"></param>
        /// <param name="zipcode"></param>
        /// <returns></returns>
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
