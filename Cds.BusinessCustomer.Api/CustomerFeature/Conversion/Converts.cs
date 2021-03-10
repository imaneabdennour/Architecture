using Cds.BusinessCustomer.Api.CustomerFeature.ViewModels;
using Cds.BusinessCustomer.Infrastructure.CustomerRepository.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cds.BusinessCustomer.Api.CustomerFeature.Conversion
{
    /// <summary>
    /// Class for converting types
    /// </summary>
    public class Converts
    {
        /// <summary>
        /// Converts from DTO to ViewModel - for single search
        /// </summary>
        /// <param name="businessCustomer"></param>
        public static SingleCustomerViewModel ToViewModel(CustomerSingleSearchDTO businessCustomer)
        {
            return new SingleCustomerViewModel()
            {
                Name = businessCustomer.Name,
                Adress = businessCustomer.Adress,
                Siret = businessCustomer.Siret,
                NafCode = businessCustomer.NafCode
            };
        }

        /// <summary>
        /// Converts from list of DTO to list of ViewModel - for multiple search
        /// </summary>
        /// <param name="businessCustomers"></param>
        public static List<MultipleCustomersViewModel> ToViewModel(List<CustomerMultipleSearchDTO> businessCustomers)
        {
            // mapping DTO  ViewModel - DTO passed as a param in constructor
            List<MultipleCustomersViewModel> list = businessCustomers.Select(e => ToViewModel(e)).ToList();
            return list;
        }

        /// <summary>
        /// Converts from single DTO to single ViewModel - for multiple search
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private static MultipleCustomersViewModel ToViewModel(CustomerMultipleSearchDTO e)
        {
            return new MultipleCustomersViewModel()
            {
                Id = e.Id,
                Name = e.Name,
                Adress = e.Adress,
                ZipCode = e.ZipCode
            };
        }
    }
}
