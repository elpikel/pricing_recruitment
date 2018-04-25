using Moq;
using Pricing.Gateways;
using Pricing.Gateways.Abstract;
using Pricing.Helpers;
using Pricing.Infrastructure;
using Pricing.Infrastructure.Abstract;
using Pricing.Models;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Pricing.Tests.Gateways
{
    public class PricingApiGatewayTests
    {
        [Fact]
        public async Task GetsPricing()
        {
            var apiGatewayMock = new Mock<IApiGateway>();
            var loggerMock = new Mock<ILogger>();
            apiGatewayMock.Setup(a => a.Get(It.IsAny<string>())).ReturnsAsync(@"{
""productRecords"": [
{
                ""id"": 123456,
""name"": ""Nice Chair"",
""price"": ""$30.25"",
""category"": ""home-furnishings"",
""discontinued"": false
},
{
                ""id"": 234567,
""name"": ""Black & White TV"",
""price"": ""$43.77"",
""category"": ""electronics"",
""discontinued"": true
}
]
}");

            var pricingApiGateway = new PricingApiGateway(loggerMock.Object, apiGatewayMock.Object, new UrlFormatter("http://test.it/"));

            var pricing = await pricingApiGateway.GetPricing(DateRange.LastMonth());

            Assert.Equal(2, pricing.ProductRecords.Count);
            Assert.Equal("123456", pricing.ProductRecords.ElementAt(0).Id);
            Assert.Equal("Nice Chair", pricing.ProductRecords.ElementAt(0).Name);
            Assert.Equal("$30.25", pricing.ProductRecords.ElementAt(0).Price);
            Assert.Equal("home-furnishings", pricing.ProductRecords.ElementAt(0).Category);
            Assert.False(pricing.ProductRecords.ElementAt(0).Discontinued);
            Assert.True(pricing.ProductRecords.ElementAt(1).Discontinued);
        }
    }
}
