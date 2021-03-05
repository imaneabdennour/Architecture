using Cds.BusinessCustomer.Domain.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Cds.BusinessCustomer.Api.ViewModels
{
    /// <summary>
    /// The model exposed to my frontend
    /// </summary>
    public class CustomerViewModel
    {

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="businessCustomer"></param>
        public CustomerViewModel(Customer businessCustomer)
        {
            Id = businessCustomer.Id;
            Name = businessCustomer.Name;
            Adress = businessCustomer.Adress;
            Siret = businessCustomer.Siret;
            NafCode = businessCustomer.NafCode;
            Phone = businessCustomer.Phone;
            ZipCode = businessCustomer.ZipCode;
            City = businessCustomer.City;
            SocialReason = businessCustomer.SocialReason;
            Civility = businessCustomer.Civility;
        }


        /// <summary>
        /// Id
        /// </summary>
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
        /// Siret
        /// </summary>
        [MaxLength(14), MinLength(14)]
        public string Siret { get; set; }

        /// <summary>
        /// Naf code
        /// </summary>
        public string NafCode { get; set; }

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
