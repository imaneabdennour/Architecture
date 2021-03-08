using Cds.BusinessCustomer.Domain.CustomerAggregate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cds.BusinessCustomer.Api.CustomerFeature
{
    /// <summary>
    /// ViewModel for multiple customers
    /// Exposed when searching by multiple params
    /// </summary>
    public class MultipleCustomersViewModel
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="businessCustomer"></param>
        public MultipleCustomersViewModel(Customer businessCustomer)
        {
            Id = businessCustomer.Id;
            Name = businessCustomer.Name;
            Adress = businessCustomer.Adress;           
            ZipCode = businessCustomer.ZipCode;           
        }

        /// <summary>
        /// Id
        /// </summary>
        [Required]
        public string Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Adress
        /// </summary>
        public string Adress { get; set; }

        /// <summary>
        /// Zip code
        /// </summary>
        public string ZipCode { get; set; }
    }
}
