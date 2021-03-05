using System;
using System.Collections.Generic;
using System.Text;

namespace Cds.BusinessCustomer.Domain.CustomerAggregate.Abstractions
{
    public interface ICustomerHandler
    {
        public (bool, string) Validate(string siret);
        public (bool, string) Validate(string socialreason, string zipcode);

    }
}
