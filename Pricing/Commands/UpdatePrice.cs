﻿using Pricing.Commands.Abstract;
using Pricing.Gateways.Models;
using Pricing.Helpers.Abstract;
using Pricing.Infrastructure.Abstract;
using Pricing.Models;
using Pricing.Repositories.Abstract;
using System;

namespace Pricing.Commands
{
    public class UpdatePrice : ChainnedCommand
    {
        private readonly ILogger _logger;
        private readonly IPriceConverter _priceConverter;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Price> _priceRepository;

        public UpdatePrice(
            ILogger logger,
            IPriceConverter priceConverter,
            IRepository<Product> productRepository,
            IRepository<Price> priceRepository,
            IChainedCommand nextUpdater) : base(nextUpdater)
        {
            _logger = logger;
            _priceConverter = priceConverter;
            _productRepository = productRepository;
            _priceRepository = priceRepository;
        }

        protected override bool CanBeHandled(ProductRecord productRecord, Product product)
        {
            return product != null && string.Equals(productRecord.Name, product.Name) && GetPriceInCents(productRecord, product) != product.Price;
        }

        protected override void Run(ProductRecord productRecord, Product product)
        {
            product.UpdatePrice(GetPriceInCents(productRecord, product));
            _productRepository.Update(product);
            _priceRepository.Create(Price.Create(product));
        }

        private int GetPriceInCents(ProductRecord productRecord, Product product)
        {
            if (!_priceConverter.TryConvert(productRecord.Price, out var priceInCents))
            {
                _logger.LogError($"Could not convert price to cents productRecord: {productRecord}, product : {product}");

                // I don't know how to deal with that so I'm closing application
                throw new ApplicationException($"Could not convert price to cents productRecord: {productRecord}, product : {product}");
            }

            return priceInCents;
        }
    }
}
