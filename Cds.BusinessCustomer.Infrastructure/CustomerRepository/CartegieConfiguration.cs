using System;

namespace Cds.BusinessCustomer.Infrastructure.CustomerRepository
{
    /// <summary>
    /// Configuration POCO
    /// </summary>
    public class CartegieConfiguration
    {
        public string BaseUrl { get; set; }
        public string ApiKey { get; set; }

        public string BySiret{ get; set; }
        public string ByMultiple { get; set; }
        public string ById { get; set; }

       
    }
}
