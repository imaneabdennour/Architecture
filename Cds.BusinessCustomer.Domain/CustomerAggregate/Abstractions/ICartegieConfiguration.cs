using System;
using System.Collections.Generic;
using System.Text;

namespace Cds.BusinessCustomer.Domain.CustomerAggregate.Abstractions
{
    public interface ICartegieConfiguration
    {
        string BaseUrl { get; }
        string ApiKey { get; }

        string BySiret { get; }
        string ByMultiple { get; }
        string ById { get; }

    }
}
