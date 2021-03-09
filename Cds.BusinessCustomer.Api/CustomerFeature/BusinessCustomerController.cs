using Cds.BusinessCustomer.Api.CustomerFeature.Validation;
using Cds.BusinessCustomer.Domain.CustomerAggregate;
using Cds.BusinessCustomer.Domain.CustomerAggregate.Abstractions;
using Cds.BusinessCustomer.Domain.CustomerAggregate.Exceptions;
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
    public class BusinessCustomerController : Controller
    {
        private readonly ICartegieRepository _service;
        private readonly ILogger<BusinessCustomerController> _logger;
        private readonly IParametersHandler _handler;

        /// <summary>
        /// Constructor for BusinessCustomerController
        /// </summary>
        /// <param name="service"></param>
        /// <param name="logger"></param>
        /// <param name="handler"></param>
        public BusinessCustomerController(ICartegieRepository service, ILogger<BusinessCustomerController> logger, IParametersHandler handler)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service)); ;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger)); ;
            _handler = handler ?? throw new ArgumentNullException(nameof(handler)); ;
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
        public async Task<ActionResult> SearchByMultipleCriteria([FromQuery] string socialReason, [FromQuery] string zipCode, [FromQuery] string siret)
        {
            try
            {
                // recherche par siret :
                if (siret != null)
                {
                    (bool, string) res = _handler.Validate(siret);
                    if (! res.Item1)
                        return BadRequest(new { code = "400", message = res.Item2 });                                   

                    var response = await _service.GetInfosBySiret(siret);

                    if (response == null)
                    {
                        return NotFound("There is no business customer with such siret");      //404      
                    }

                    return Ok(new SingleCustomerViewModel(response));    //200
                }

                // recherche par raison sociale et code postal :
                else
                {
                    (bool, string) res = _handler.Validate(socialReason, zipCode);
                     if (! res.Item1)
                        return BadRequest(new { code = "400", message = res.Item2});
                    

                    List<Customer> response = await _service.GetInfosByCriteria(socialReason, zipCode);
                    if (response == null)
                    {
                        return NotFound("There is no business customer with such social reason and zipcode");
                    }
                    // converting from Model to ViewModel :o :o 
                    List<MultipleCustomersViewModel> list = response.Select(e => new MultipleCustomersViewModel(e)).ToList();

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
        public async Task<ActionResult<SingleCustomerViewModel>> SearchById([FromRoute] string Id)
        {
            try
            {
#nullable enable
                var response = await _service.GetInfosById(Id);
                if (response == null)
                {
                    return NotFound("There is no such business customer with such id ");      //404
                }
                return Ok(new SingleCustomerViewModel(response));
            }
            catch (InternalServerException ex)
            {
                //throw new InternalServerException();
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

                return res.IsSuccessStatusCode;
            }
        }

    }
}
