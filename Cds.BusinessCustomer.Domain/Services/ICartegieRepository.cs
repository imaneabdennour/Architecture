using Cds.BusinessCustomer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cds.BusinessCustomer.Domain.Services
{
    public interface ICartegieRepository
    {
        Task<List<BusinessCustomerModel>> GetInfos_MultipleSearch(string socialReason, string zipCode);
        Task<BusinessCustomerModel> GetInfos_IdSearch(string id);
        Task<BusinessCustomerModel> GetInfos_SiretSearch(string siret);
    }
}
