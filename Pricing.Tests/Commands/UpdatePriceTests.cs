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
    public class UpdatePriceTests
    {
        [Fact]
        public void ExecutesNextCommandIfPriceShouldNotBeUpdated()
        {
            var chainedCommandMock = new Mock<IChainedCommand>();
            var newProduct = new UpdatePrice(null, new DollarsToCentsConverter(), null, null, chainedCommandMock.Object);

            newProduct.Execute(new ProductRecord { Id = "1", Discontinued = true, Name = "name", Price = "$22.11" }, null);

            chainedCommandMock.Verify(c => c.Execute(It.IsAny<ProductRecord>(), It.IsAny<Product>()));
        }

        [Fact]
        public void ClosesApplicationWhenPriceCannotBeConverted()
        {
            var loggerMock = new Mock<ILogger>();
            var newProduct = new UpdatePrice(loggerMock.Object, new DollarsToCentsConverter(), null, null, null);

            Assert.Throws<ApplicationException>(() => newProduct.Execute(new ProductRecord { Id = "1", Discontinued = true, Name = "name", Price = "PL22.11" }, null));

            loggerMock.Verify(l => l.LogError(It.IsAny<string>()));
        }

        [Fact]
        public void UpdatesPrice()
        {
            var chainedCommandMock = new Mock<IChainedCommand>();
            var productRepository = new Repository<Product>();
            var priceRepository = new Repository<Price>();
            var product = new Product { Id = "id", Name = "name" };

            productRepository.Create(product);

            var newProduct = new UpdatePrice(null, new DollarsToCentsConverter(), productRepository, priceRepository, chainedCommandMock.Object);

            newProduct.Execute(new ProductRecord { Id = "1", Discontinued = true, Name = "name", Price = "$22.11" }, product);

            var updatedProduct = productRepository.GetById("id");
            var createdPrice = priceRepository.GetAll().Single(p => p.ProductId == product.Id);

            Assert.Equal(2211, updatedProduct.Price);
            Assert.Equal(2211, createdPrice.Cents);
        }
    }
}
