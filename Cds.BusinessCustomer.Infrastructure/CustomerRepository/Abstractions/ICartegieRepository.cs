using Cds.BusinessCustomer.Infrastructure.CustomerRepository.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cds.BusinessCustomer.Infrastructure.CustomerRepository.Abstractions
{
    public interface ICartegieRepository
    {
        Task<List<CustomerMultipleSearchDTO>> GetInfosByCriteria(string socialReason, string zipCode);
        Task<CustomerSingleSearchDTO> GetInfosById(string id);
        Task<CustomerSingleSearchDTO> GetInfosBySiret(string siret);
    }
}

///Change les noms voir la PR et ajoute les commentaires sur chaque méthode et classe
