using Cds.BusinessCustomer.Domain.CustomerAggregate;
using Cds.BusinessCustomer.Infrastructure.CustomerRepository.Abstractions;
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
    public class CartegieApi :  ICartegieApi
    {
        CartegieConfiguration _configuration;
        string baseUrl;
        string apiKey;


        public CartegieApi(CartegieConfiguration myConfiguration)
        {
            _configuration = myConfiguration;

            baseUrl = _configuration.BaseUrl;
            apiKey = _configuration.ApiKey;
        }

        /// <summary>
        /// Get customer infos by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DTO
        public Task<CustomerSingleSearchDTO> GetInfosById(string id)
        {
            if(id.Equals(null))
                return Task.FromResult<CustomerSingleSearchDTO>(null);

            CustomerSingleSearchDTO consumerInfo = IdSearch(id).Result;
            if(consumerInfo == null)
            {
                return Task.FromResult<CustomerSingleSearchDTO>(null);
            }
            return Task.FromResult(consumerInfo);
        }

        /// <summary>
        /// Get customer infos by siret
        /// </summary>
        /// <param name="siret"></param>
        /// <returns></returns>
        public Task<CustomerSingleSearchDTO> GetInfosBySiret(string siret)
        {
            CustomerSingleSearchDTO consumerInfo = IdSearch(siret).Result;
            return Task.FromResult(consumerInfo);
        }

        /// <summary>
        /// Get customers infos by social number and zipcode
        /// </summary>
        /// <param name="socialReason"></param>
        /// <param name="zipCode"></param>
        /// <returns></returns>
        public Task<List<CustomerMultipleSearchDTO>> GetInfosByCriteria(string socialReason, string zipCode)
        {
            List<CustomerMultipleSearchDTO> list = MultipleSearch(socialReason, zipCode).Result;
            if(list == null || list.Count == 0)
            {
                return Task.FromResult<List<CustomerMultipleSearchDTO>>(null);
            }
            return Task.FromResult(list);
        }


                    /// Communication with API de cartégie ///


        /// <summary>
        /// Method to handle Cartégie api - searching by id
        /// </summary>
        /// <param name="param"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task<CustomerSingleSearchDTO> IdSearch(string id)
        {
            if (id.Equals(null))
                return await Task.FromResult<CustomerSingleSearchDTO>(null);
            
            CustomerSingleSearchDTO ConsumerInfo = new CustomerSingleSearchDTO();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // status code and data :
                HttpResponseMessage res = await client.GetAsync(_configuration.ById);

                if (res.IsSuccessStatusCode)
                {
                    ConsumerInfo = FromResponseToDto(res);
                    if (ConsumerInfo == null)
                        return await Task.FromResult<CustomerSingleSearchDTO>(null);
                }
            }
            return ConsumerInfo;
        }

        /// <summary>
        /// Method to handle Cartégie api - searching by id
        /// </summary>
        /// <param name="param"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task<CustomerSingleSearchDTO> SiretSearch(string siret)
        {
            if (siret.Equals(null))
                return await Task.FromResult<CustomerSingleSearchDTO>(null);

            CustomerSingleSearchDTO ConsumerInfo = new CustomerSingleSearchDTO();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // status code and data :
                HttpResponseMessage res = await client.GetAsync(_configuration.BySiret);

                if (res.IsSuccessStatusCode)
                {
                    ConsumerInfo = FromResponseToDto(res);
                    if(ConsumerInfo == null) 
                        return await Task.FromResult<CustomerSingleSearchDTO>(null);
                }
            }
            return ConsumerInfo;
        }

        public CustomerSingleSearchDTO FromResponseToDto(HttpResponseMessage res)
        {
            // Storing the response details received from web api   
            var EmpResponse = res.Content.ReadAsStringAsync().Result;
            if (EmpResponse == null)
            {
                return null;
            }
            // Deserializing the response received from web api and storing it  
            return JsonConvert.DeserializeObject<CustomerSingleSearchDTO>(EmpResponse.Substring(1, EmpResponse.Length - 2));
        }

        /// <summary>
        /// Method to handle Cartégie api - searching by social number and zip code
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private async Task<List<CustomerMultipleSearchDTO>> MultipleSearch(string socialReason, string zipcode)
        {
            if (socialReason.Equals(null) || zipcode.Equals(null))
                return await Task.FromResult<List<CustomerMultipleSearchDTO>>(null);

            var consumerInfo = new List<CustomerMultipleSearchDTO>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // status code and data :
                HttpResponseMessage res = await client.GetAsync(_configuration.ByMultiple);

                if (res.IsSuccessStatusCode)
                {
                    // Storing the response details recieved from web api   
                    var EmpResponse = res.Content.ReadAsStringAsync().Result;
                    if(EmpResponse == null)
                    {
                        return await Task.FromResult<List<CustomerMultipleSearchDTO>>(null);
                    }
                    // Deserializing the response recieved from web api and storing it
                    consumerInfo = JsonConvert.DeserializeObject<List<CustomerMultipleSearchDTO>>(EmpResponse);
                }
            }
            
            return consumerInfo;
        }
    }
}
