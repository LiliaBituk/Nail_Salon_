using Xunit;
using Business_Logic;

namespace BusinessLogicTests
{
    public class ServiceDiscountTest
    {
        [Fact]
        public void GetDiscountedPrice_ReturnsDiscountedPrice_WhenIsNewCustomerIsTrue()
        {
            var service = new Service { ServicePrice = 100 };

            var discountedPrice = service.GetDiscountedPrice(true);

            Assert.Equal(70, discountedPrice);
        }

        [Fact]
        public void GetDiscountedPrice_ReturnsOriginalPrice_WhenIsNewCustomerIsFalse()
        {
            var service = new Service { ServicePrice = 100 };

            var discountedPrice = service.GetDiscountedPrice(false);

            Assert.Equal(100, discountedPrice);
        }
    }
}