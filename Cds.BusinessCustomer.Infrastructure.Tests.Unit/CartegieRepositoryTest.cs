using System;
using Xunit;

namespace Cds.BusinessCustomer.Infrastructure.Tests.Unit
{
    public class CartegieRepositoryTest
    {
        // check names !!!!!!!!!!
        [Theory]
        [InlineData(4, 3, 7)]
        [InlineData(21, 5.25, 26.25)]
        public void GetInfos_IdSearch_GetCustomerInfos(double x, double y, double expected)
        {
            double actual  = x + y;
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(8, 4, 2)]
        public void Divide_simplevalues(double x, double y, double expected)
        {
            double actual = x / y;
            Assert.Equal(expected, actual);
        }
    }
}
