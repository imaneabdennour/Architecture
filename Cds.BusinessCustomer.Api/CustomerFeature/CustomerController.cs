using Cds.BusinessCustomer.Domain.CustomerAggregate;
using Cds.BusinessCustomer.Domain.CustomerAggregate.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Cds.BusinessCustomer.Api.CustomerFeature
{
    /// <summary>
    /// Customer Controller
    /// </summary>
    public class CustomerController : Controller
    {
        private readonly ICartegieRepository _service;
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerHandler _handler;

        /// <summary>
        /// Constructor for BusinessCustomerController
        /// </summary>
        /// <param name="service"></param>
        /// <param name="logger"></param>
        /// <param name="handler"></param>
        public CustomerController(ICartegieRepository service, ILogger<CustomerController> logger, ICustomerHandler handler)
        {
            _service = service;
            _logger = logger;
            _handler = handler;
        }

        /// <summary>
        ///  Research by Social Reason and Zip Code OR Siret - of the information about the business customer
        /// </summary>
        /// <param name="socialReason"></param>
        /// <param name="zipCode"></param>
        /// <param name="siret"></param>
        /// <returns></returns>
        [HttpGet("business-customer-information")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Customer>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> BusinessCustomerInformation_MultipleSearch([FromQuery] string socialReason, [FromQuery] string zipCode, [FromQuery] string siret)
        {
            try
            {
                if (siret != null)
                {
                    (bool, string) res = _handler.Validate(siret);
                    if (! res.Item1)
                        return BadRequest(new { code = "400", message = res.Item2 });                                   

                    var response = await _service.GetInfos_SiretSearch(siret);

                    if (response == null)
                    {
                        return NotFound();      //404      
                    }

                    return Ok(new CustomerViewModel(response));    //200
                }

                else
                {
                    (bool, string) res = _handler.Validate(socialReason, zipCode);

                    if (! res.Item1)
                     return BadRequest(new { code = "400", message = res.Item2});
                    

                    var response = await _service.GetInfos_MultipleSearch(socialReason, zipCode);

                    // converting from Model to ViewModel
                    List<CustomerViewModel> list = response.Select(e => new CustomerViewModel(e)).ToList();

                    return Ok(list);    //200
                }
            }
            catch (Exception)
            {
                _logger.LogError("Failed to retreive customers - Internal Server Error");
                return StatusCode(500);     //500
            }
        }

        /// <summary>
        /// Research by Siret - of the information about the business customer
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("business-customer-information/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Customer))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> BusinessCustomerInformation_IdSearch([FromRoute] string Id)
        {
            try
            {
                var response = await _service.GetInfos_IdSearch(Id);
                if (response == null)
                {
                    return NotFound();      //404
                }
                return Ok(new CustomerViewModel(response));
            }
            catch (Exception)
            {
                _logger.LogError("Failed to retreive customer - Internal Server Error");
                return StatusCode(500);     //500
            }
        }


        /// <summary>
        /// Health check for : HTTP
        /// </summary>
        /// <returns></returns>
        [HttpGet("health")]
        public async Task<bool> GetHealthCheck()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44383/");

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // status code and data :
                HttpResponseMessage res = await client.GetAsync("healthCheck");

                if (res.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                    return false;
            }
        }

    }
}
