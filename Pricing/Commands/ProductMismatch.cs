using Pricing.Commands.Abstract;
using Pricing.Gateways.Models;
using Pricing.Infrastructure.Abstract;
using Pricing.Models;

namespace Pricing.Commands
{
    public class ProductMismatch : IChainedCommand
    {
        private readonly ILogger _logger;

        public ProductMismatch(ILogger logger)
        {
            _logger = logger;
        }

        public void Execute(ProductRecord productRecord, Product product)
        {
            if (ThereIsMismatchInNames(productRecord, product))
            {
                _logger.LogError($"There was mismatch between: productRecord: {productRecord} and product: {product}");
            }
        }

        private static bool ThereIsMismatchInNames(ProductRecord productRecord, Product product)
        {
            return product != null && !string.Equals(productRecord.Name, product.Name);
        }
    }
}
