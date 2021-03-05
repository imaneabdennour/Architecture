using Cds.BusinessCustomer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cds.BusinessCustomer.Domain.Services
{
    public interface ICartegieRepository
    {
        Task<List<Customer>> GetInfos_MultipleSearch(string socialReason, string zipCode);
        Task<Customer> GetInfos_IdSearch(string id);
        Task<Customer> GetInfos_SiretSearch(string siret);
    }
}
