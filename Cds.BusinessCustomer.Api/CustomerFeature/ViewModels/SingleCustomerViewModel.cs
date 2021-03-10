using Cds.BusinessCustomer.Domain.CustomerAggregate;
using Cds.BusinessCustomer.Infrastructure.CustomerRepository.Dtos;
using System;
using System.ComponentModel.DataAnnotations;

namespace Cds.BusinessCustomer.Api.CustomerFeature.ViewModels
{
    /// <summary>
    /// ViewModel for single customer
    /// Exposed when searching by single param
    /// </summary>
    public class SingleCustomerViewModel
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public SingleCustomerViewModel() { }
        
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Siret
        /// </summary>
        [MaxLength(14), MinLength(14)]
        public string Siret { get; set; }

        /// <summary>
        /// Naf code
        /// </summary>
        public string NafCode { get; set; }

        /// <summary>
        /// Adress
        /// </summary>
        public string Adress { get; set; }

        /// <summary>
        /// Phone Number
        /// </summary>       
        public string Phone { get; set; }

        /// <summary>
        /// Zip Code
        /// </summary>      
        public string ZipCode { get; set; }

        /// <summary>
        /// City
        /// </summary>      
        public string City { get; set; }

        /// <summary>
        /// Social Reason
        /// </summary>
        public string SocialReason { get; set; }

        /// <summary>
        /// Civility 
        /// </summary>
        public string Civility { get; set; }

    }
}
