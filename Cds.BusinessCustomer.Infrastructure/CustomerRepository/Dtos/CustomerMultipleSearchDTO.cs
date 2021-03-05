using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cds.BusinessCustomer.Infrastructure.CustomerRepository.Dtos
{
    /// <summary>
    /// Exposed when searching by multiple params
    /// </summary>
    public class CustomerMultipleSearchDTO
    {
        /// <summary>
        /// Id
        /// </summary>
        [Required]
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [JsonProperty("nomen")]
        public string Name { get; set; }

        /// <summary>
        /// Adress
        /// </summary>
        [JsonProperty("adress4")]
        public string Adress { get; set; }

        /// <summary>
        /// Zip code
        /// </summary>
        [JsonProperty("codpos")]
        public string ZipCode { get; set; }


    }
}
