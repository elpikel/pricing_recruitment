using Pricing.Commands.Abstract;
using Pricing.Gateways.Models;
using Pricing.Helpers.Abstract;
using Pricing.Infrastructure.Abstract;
using Pricing.Models;
using Pricing.Repositories.Abstract;
using System;

namespace Pricing.Commands
{
    public class NewProduct : ChainnedCommand
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IPriceConverter _priceConverter;
        private readonly ILogger _logger;

        public NewProduct(
            ILogger logger,
            IRepository<Product> productRepository,
            IPriceConverter priceConverter,
            IChainedCommand nextCommand) : base(nextCommand)
        {
            _logger = logger;
            _productRepository = productRepository;
            _priceConverter = priceConverter;
        }

        protected override bool CanBeHandled(ProductRecord productRecord, Product product)
        {
            return product == null && !productRecord.Discontinued;
        }

        protected override void Run(ProductRecord productRecord, Product product)
        {
            if (!_priceConverter.TryConvert(productRecord.Price, out var priceInCents))
            {
                _logger.LogError($"Could not convert price to cents productRecord: {productRecord}, product : {product}");

                // I don't know how to deal with that so I'm closing application
                throw new ApplicationException($"Could not convert price to cents productRecord: {productRecord}, product : {product}");
            }

            var newProduct = ToProduct(productRecord, priceInCents);
            _productRepository.Create(newProduct);
            _logger.LogInfo($"New product was created: { newProduct }");
        }

        private static Product ToProduct(ProductRecord productRecord, int priceInCents)
        {
            return new Product
            {
                ExternalProductId = productRecord.Id,
                Name = productRecord.Name,
                Price = priceInCents
            };
        }
    }
}
