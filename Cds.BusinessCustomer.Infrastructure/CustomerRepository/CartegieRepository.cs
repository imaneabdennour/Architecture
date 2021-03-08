using Cds.BusinessCustomer.Domain.CustomerAggregate;
using Cds.BusinessCustomer.Domain.CustomerAggregate.Abstractions;
using Cds.BusinessCustomer.Infrastructure.CustomerRepository.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Cds.BusinessCustomer.Infrastructure.CustomerRepository
{
    public class CartegieRepository : ICartegieRepository
    {
        private string baseUrl = "https://6037a3775435040017722f92.mockapi.io/api/v1/Company/";
        /// <summary>
        /// Get customer infos by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Customer GetInfos_IdSearch(string id)
        {
            CustomerSingleSearchDTO ConsumerInfo = SingleSearch("ResearchById").Result;
            return new Customer()
            {
                Name = ConsumerInfo.Name,
                Siret = ConsumerInfo.Siret,
                NafCode = ConsumerInfo.NafCode,
                Adress = ConsumerInfo.Adress
            };
        }

        /// <summary>
        /// Get customer infos by siret
        /// </summary>
        /// <param name="siret"></param>
        /// <returns></returns>
        public Customer GetInfos_SiretSearch(string siret)
        {
            CustomerSingleSearchDTO ConsumerInfo = SingleSearch("ResearchBySiret").Result;

            return new Customer()
            {
                Name = ConsumerInfo.Name,
                Siret = ConsumerInfo.Siret,
                NafCode = ConsumerInfo.NafCode,
                Adress = ConsumerInfo.Adress
            };
        }

        /// <summary>
        /// Get customers infos by social number and zipcode
        /// </summary>
        /// <param name="socialReason"></param>
        /// <param name="zipCode"></param>
        /// <returns></returns>
        public List<Customer> GetInfos_MultipleSearch(string socialReason, string zipCode)
        {
            List<CustomerMultipleSearchDTO> ConsumerInfo = MultipleSearch("RechercheMultiple").Result;

            List<Customer> list = ConsumerInfo.Select(e => new Customer { Id = e.Id, Name = e.Name, Adress = e.Adress }).ToList();
            return list;

        }


                    /// API cartégie ///


        /// <summary>
        /// Method to handle Cartégie api - searching by id or siret
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private async Task<CustomerSingleSearchDTO> SingleSearch(string param)
        {
            CustomerSingleSearchDTO ConsumerInfo = new CustomerSingleSearchDTO();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // status code and data :
                HttpResponseMessage res = await client.GetAsync(param);

                if (res.IsSuccessStatusCode)
                {
                    //Storing the response details received from web api   
                    var EmpResponse = res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing it  
                    ConsumerInfo = JsonConvert.DeserializeObject<CustomerSingleSearchDTO>(EmpResponse.Substring(1, EmpResponse.Length - 2));
                }

            }
            return ConsumerInfo;
        }

        /// <summary>
        /// Method to handle Cartégie api - searching by social number and zip code
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private async Task<List<CustomerMultipleSearchDTO>> MultipleSearch(string param)
        {
            List<CustomerMultipleSearchDTO> ConsumerInfo = new List<CustomerMultipleSearchDTO>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // status code and data :
                HttpResponseMessage res = await client.GetAsync(param);

                if (res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var EmpResponse = res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing it
                    ConsumerInfo = JsonConvert.DeserializeObject<List<CustomerMultipleSearchDTO>>(EmpResponse);
                }
            }
            return ConsumerInfo;
        }
    }
}
