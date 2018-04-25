using Moq;
using Pricing.Commands;
using Pricing.Gateways.Models;
using Pricing.Infrastructure.Abstract;
using Pricing.Models;
using Xunit;

namespace Pricing.Tests.Commands
{
    public class ProductMismatchTests
    {
        [Fact]
        public void LogsErrorWhenThereIsProductMismatch()
        {
            var loggerMock = new Mock<ILogger>();
            var productMismatch = new ProductMismatch(loggerMock.Object);

            productMismatch.Execute(new ProductRecord { Name = "name" }, new Product { Name = "otherName" });

            loggerMock.Verify(l => l.LogError(It.IsAny<string>()));
        }
    }
}
