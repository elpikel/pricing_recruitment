using Pricing.Models;
using Xunit;

namespace Pricing.Tests.Models
{
    public class PriceTests
    {
        [Fact]
        public void CreatesPrice()
        {
            var product = new Product
            {
                Id = "1",
                Price = 10
            };

            var price = Price.Create(product);

            Assert.Equal(10, price.Cents);
            Assert.Equal("1", price.ProductId);
        }
    }
}
