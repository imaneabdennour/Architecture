using Cds.BusinessCustomer.Domain.CustomerAggregate;
using Cds.BusinessCustomer.Domain.CustomerAggregate.Abstractions;
using Cds.BusinessCustomer.Infrastructure.CustomerRepository.Dtos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
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
    public class CartegieRepository :  ICartegieRepository
    {
        ICartegieConfiguration _configuration;
        string baseUrl;
        string apiKey;


        public CartegieRepository(ICartegieConfiguration configuration)
        {
            _configuration = configuration;

            baseUrl = _configuration.BaseUrl;
            apiKey = _configuration.ApiKey;           
        }

        /// <summary>
        /// Get customer infos by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<Customer> GetInfosById(string id)
        {
            CustomerSingleSearchDTO ConsumerInfo = SingleSearch(_configuration.ById).Result;
            if(ConsumerInfo == null)
            {
                return Task.FromResult<Customer>(null);
            }
            return Task.FromResult(new Customer()
            {
                Name = ConsumerInfo.Name,
                Siret = ConsumerInfo.Siret,
                NafCode = ConsumerInfo.NafCode,
                Adress = ConsumerInfo.Adress
            });
        }

        /// <summary>
        /// Get customer infos by siret
        /// </summary>
        /// <param name="siret"></param>
        /// <returns></returns>
        public Task<Customer> GetInfosBySiret(string siret)
        {
            CustomerSingleSearchDTO ConsumerInfo = SingleSearch(_configuration.BySiret).Result;
            if (ConsumerInfo == null)
            {
                return Task.FromResult<Customer>(null);
            }
            return Task.FromResult(new Customer()
            {
                Name = ConsumerInfo.Name,
                Siret = ConsumerInfo.Siret,
                NafCode = ConsumerInfo.NafCode,
                Adress = ConsumerInfo.Adress
            });
        }

        /// <summary>
        /// Get customers infos by social number and zipcode
        /// </summary>
        /// <param name="socialReason"></param>
        /// <param name="zipCode"></param>
        /// <returns></returns>
        public Task<List<Customer>> GetInfosByCriteria(string socialReason, string zipCode)
        {
            var list = MultipleSearch(_configuration.ByMultiple).Result;
            if(list == null || list.Count == 0)
            {
                return Task.FromResult<List<Customer>>(null);
            }
            return Task.FromResult(list);
        }


                    /// Communication with API de cartégie ///


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
                    if (EmpResponse == null)
                    {
                        return await Task.FromResult<CustomerSingleSearchDTO>(null);
                    }
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
        private async Task<List<Customer>> MultipleSearch(string param)
        {
            var ConsumerInfo = new List<CustomerMultipleSearchDTO>();

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
                    if(EmpResponse == null)
                    {
                        return await Task.FromResult<List<Customer>>(null);
                    }
                    //Deserializing the response recieved from web api and storing it
                    ConsumerInfo = JsonConvert.DeserializeObject<List<CustomerMultipleSearchDTO>>(EmpResponse);
                }
            }
            if(ConsumerInfo == null || ConsumerInfo.Count == 0)
            {
                return await Task.FromResult<List<Customer>>(null);
            }
            List<Customer> list = ConsumerInfo.Select(e => new Customer { Id = e.Id, Name = e.Name, Adress = e.Adress }).ToList();
            return list;
        }
    }
}
