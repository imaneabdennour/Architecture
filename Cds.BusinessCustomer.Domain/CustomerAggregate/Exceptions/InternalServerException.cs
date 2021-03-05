using System;
using System.Collections.Generic;
using System.Text;

namespace Cds.BusinessCustomer.Domain.CustomerAggregate.Exceptions
{
    public class InternalServerException : Exception
    {
        public InternalServerException() : base(){ }
        public InternalServerException(string message) : base(message){
            //logger.LogError("Failed to retreive customer - Internal Server Error");
            //        return StatusCode(500);     //500

        }



    }
}
