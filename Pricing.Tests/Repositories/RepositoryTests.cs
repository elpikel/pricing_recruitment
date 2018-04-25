using Pricing.Models;
using Pricing.Repositories;
using Xunit;

namespace Pricing.Tests.Repositories
{
    public class RepositoryTests
    {
        [Fact]
        public void CreateNewEntity()
        {
            var repository = new Repository<Product>();

            var product = new Product
            {
                Id = "1"
            };
            repository.Create(product);

            var createdProduct = repository.GetById(product.Id);

            Assert.Equal("1", createdProduct.Id);
        }

        [Fact]
        public void UpdateEntity()
        {
            var repository = new Repository<Product>();

            var product = new Product
            {
                Id = "1",
                Name = "name"
            };

            repository.Create(product);

            product.Name = "newName";
            repository.Update(product);

            var updatedProduct = repository.GetById("1");

            Assert.Equal("newName", updatedProduct.Name);
        }

        [Fact]
        public void ReturnsNullWhenEntityCouldNotBeFound()
        {
            var repository = new Repository<Product>();

            var createdProduct = repository.GetById("non exisitng");

            Assert.Null(createdProduct);
        }
    }
}
