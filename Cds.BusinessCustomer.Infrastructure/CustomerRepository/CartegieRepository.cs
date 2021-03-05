using Cds.BusinessCustomer.Domain.Models;
using Cds.BusinessCustomer.Domain.Services;
using Cds.BusinessCustomer.Infrastructure.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Cds.BusinessCustomer.Infrastructure.Http
{
    public class CartegieRepository : ICartegieRepository
    {
        private string baseUrl = "https://6037a3775435040017722f92.mockapi.io/api/v1/Company/";
        public async Task<Customer> GetInfos_IdSearch(string id)
        {
            CustomerSingleSearchDTO ConsumerInfo = new CustomerSingleSearchDTO();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // status code and data :
                HttpResponseMessage res = await client.GetAsync("ResearchById");

                if (res.IsSuccessStatusCode)
                {
                    //Storing the response details received from web api   
                    var EmpResponse = res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing it  
                    ConsumerInfo = JsonConvert.DeserializeObject<CustomerSingleSearchDTO>(EmpResponse.Substring(1, EmpResponse.Length - 2));
                }

                return new Customer()
                {
                    Name = ConsumerInfo.Name,
                    Siret = ConsumerInfo.Siret,
                    NafCode = ConsumerInfo.NafCode,
                    Adress = ConsumerInfo.Adress
                };

            }

        }
        public async Task<List<Customer>> GetInfos_MultipleSearch(string socialReason, string zipCode)
        {
            List<CustomerMultipleSearchDTO> ConsumerInfo = new List<CustomerMultipleSearchDTO>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage res = await client.GetAsync("RechercheMultiple");

                if (res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var EmpResponse = res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing it
                    ConsumerInfo = JsonConvert.DeserializeObject<List<CustomerMultipleSearchDTO>>(EmpResponse);
                }

                List<Customer> list = ConsumerInfo.Select(e => new Customer { Id = e.Id, Name = e.Name, Adress = e.Adress }).ToList();
                return list;
            }

        }

        public async Task<Customer> GetInfos_SiretSearch(string siret)
        {
            CustomerSingleSearchDTO ConsumerInfo = new CustomerSingleSearchDTO();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // status code and data :
                HttpResponseMessage res = await client.GetAsync("ResearchBySiret");

                if (res.IsSuccessStatusCode)
                {
                    //Storing the response details received from web api   
                    var EmpResponse = res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing it  
                    ConsumerInfo = JsonConvert.DeserializeObject<CustomerSingleSearchDTO>(EmpResponse.Substring(1, EmpResponse.Length - 2));
                }

                return new Customer()
                {
                    Name = ConsumerInfo.Name,
                    Siret = ConsumerInfo.Siret,
                    NafCode = ConsumerInfo.NafCode,
                    Adress = ConsumerInfo.Adress
                };

            }
        }

    }
}
