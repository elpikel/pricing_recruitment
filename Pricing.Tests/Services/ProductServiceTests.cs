using Moq;
using Pricing.Commands.Abstract;
using Pricing.Gateways.Abstract;
using Pricing.Gateways.Models;
using Pricing.Models;
using Pricing.Repositories;
using Pricing.Services;
using Pricing.Services.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Pricing.Tests.Services
{
    public class ProductServiceTests
    {
        [Fact]
        public async Task ExecutesCommandForEachProductRecord()
        {
            var pricingApiGatewayMock = new Mock<IPricingApiGateway>();
            var executionServiceMock = new Mock<IExecutionService>();
            var chainedCommandMock = new Mock<IChainedCommand>();

            pricingApiGatewayMock
                .Setup(p => p.GetPricing(It.IsAny<DateRange>()))
                .ReturnsAsync(new Pricing.Gateways.Models.Pricing
                {
                    ProductRecords = new List<ProductRecord>
                    {
                        new ProductRecord(),
                        new ProductRecord()
                    }
                });

            var productService = new ProductService(
                pricingApiGatewayMock.Object,
                new Repository<Product>(),
                executionServiceMock.Object,
                chainedCommandMock.Object);

            await productService.Update();

            chainedCommandMock
                .Verify(c => c.Execute(
                    It.IsAny<ProductRecord>(), 
                    It.IsAny<Product>()), Times.Exactly(2));
        }
    }
}
