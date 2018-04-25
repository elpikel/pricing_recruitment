using Moq;
using Pricing.Commands;
using Pricing.Commands.Abstract;
using Pricing.Gateways.Models;
using Pricing.Helpers;
using Pricing.Infrastructure.Abstract;
using Pricing.Models;
using Pricing.Repositories;
using System;
using System.Linq;
using Xunit;

namespace Pricing.Tests.Commands
{
    public class NewProductTests
    {
        [Fact]
        public void CreatesNewProduct()
        {
            var productRepository = new Repository<Product>();
            var loggerMock = new Mock<ILogger>();
            var newProduct = new NewProduct(loggerMock.Object, productRepository, new DollarsToCentsConverter(), null);

            newProduct.Execute(new ProductRecord { Id = "1", Discontinued = false, Name = "name", Price = "$22.11" }, null);

            var createdProduct = productRepository.GetAll().Single(p => p.ExternalProductId == "1");

            Assert.Equal("name", createdProduct.Name);
            Assert.Equal(2211, createdProduct.Price);

            loggerMock.Verify(l => l.LogInfo(It.IsAny<string>()));
        }

        [Fact]
        public void ExecutesNextCommandIfNewProductShouldNotBeCreated()
        {
            var chainedCommandMock = new Mock<IChainedCommand>();
            var newProduct = new NewProduct(null, null, null, chainedCommandMock.Object);

            newProduct.Execute(new ProductRecord { Id = "1", Discontinued = true, Name = "name", Price = "$22.11" }, null);

            chainedCommandMock.Verify(c => c.Execute(It.IsAny<ProductRecord>(), It.IsAny<Product>()));
        }

        [Fact]
        public void ClosesApplicationWhenPriceCannotBeConverted()
        {
            var loggerMock = new Mock<ILogger>();
            var newProduct = new NewProduct(loggerMock.Object, null, new DollarsToCentsConverter(), null);

            Assert.Throws<ApplicationException>(() => newProduct.Execute(new ProductRecord { Id = "1", Discontinued = false, Name = "name", Price = "PLN22.11" }, null));

            loggerMock.Verify(l => l.LogError(It.IsAny<string>()));
        }
    }
}
