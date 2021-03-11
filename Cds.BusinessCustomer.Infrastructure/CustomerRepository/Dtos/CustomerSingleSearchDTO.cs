using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cds.BusinessCustomer.Infrastructure.CustomerRepository.Dtos
{
    /// <summary>
    /// Solution to not expose my Customer objects 
    /// Exposed when searching by single param
    /// </summary>
    public class CustomerSingleSearchDTO
    {
        /// <summary>
        /// Name
        /// </summary>
        [JsonProperty("adresse1")]
        public string Name { get; set; }

        /// <summary>
        /// Siret
        /// </summary>
        [JsonProperty("siret")]
        [MaxLength(14), MinLength(14)]
        public string Siret { get; set; }

        /// <summary>
        /// Naf code
        /// </summary>
        [JsonProperty("apen700")]
        public string NafCode { get; set; }

        /// <summary>
        /// Adress
        /// </summary>
        [JsonProperty("adresse4")]
        public string Adress { get; set; }

        /// <summary>
        /// Zip code
        /// </summary>
        [JsonProperty("codpos")]
        public string ZipCode { get; set; }

        /// <summary>
        /// City
        /// </summary>
        [JsonProperty("lib_commune")]
        public string City { get; set; }

        /// <summary>
        /// Social reason
        /// </summary>
        [JsonProperty("rs")]
        public string SocialReason { get; set; }

        /// <summary>
        /// Civility
        /// </summary>
        [JsonProperty("civilite")]
        public string Civility { get; set; }

        /// <summary>
        /// Phone
        /// </summary>
        [JsonProperty("tel")]
        public string Phone { get; set; }

    }
}