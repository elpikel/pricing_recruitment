using Pricing.Models;

namespace Pricing.Commands.Abstract
{
    public interface IChainedCommand
    {
        void Execute(Gateways.Models.ProductRecord productRecord, Product product);
    }
}