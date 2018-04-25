using Pricing.Models;
using Xunit;

namespace Pricing.Tests.Models
{
    public class ProductTests
    {
        [Fact]
        public void UpdatesPrice()
        {
            var product = new Product
            {
                Price = 1
            };

            product.UpdatePrice(2);

            Assert.Equal(2, product.Price);
        }
    }
}
