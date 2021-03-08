using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cds.BusinessCustomer.Domain.CustomerAggregate.Abstractions
{
    public interface ICartegieRepository
    {
        List<Customer> GetInfos_MultipleSearch(string socialReason, string zipCode);
        Customer GetInfos_IdSearch(string id);
        Customer GetInfos_SiretSearch(string siret);
    }
}
