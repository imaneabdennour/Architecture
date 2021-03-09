using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cds.BusinessCustomer.Domain.CustomerAggregate.Abstractions
{
    public interface ICartegieRepository
    {
        Task<List<Customer>> GetInfosByCriteria(string socialReason, string zipCode);
        Task<Customer> GetInfosById(string id);
        Task<Customer> GetInfosBySiret(string siret);
    }
}

///Change les noms voir la PR et ajoute les commentaires sur chaque méthode et classe
