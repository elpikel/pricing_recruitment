using Pricing.Commands.Abstract;
using Pricing.Gateways.Models;
using Pricing.Infrastructure.Abstract;
using Pricing.Models;

namespace Pricing.Commands
{
    public class ProductMismatch : ChainnedCommand
    {
        private readonly ILogger _logger;

        public ProductMismatch(ILogger logger) : base(null)
        {
            _logger = logger;
        }

        protected override bool CanBeHandled(ProductRecord productRecord, Product product)
        {
            return product != null && !string.Equals(productRecord.Name, product.Name);
        }

        protected override void Run(ProductRecord productRecord, Product product)
        {
            _logger.LogError($"There was mismatch between: productRecord: {productRecord} and product: {product}");
        }
    }
}
