using Pricing.Gateways.Models;
using Pricing.Models;

namespace Pricing.Commands.Abstract
{
    public abstract class ChainnedCommand : IChainedCommand
    {
        private readonly IChainedCommand _nextCommand;

        public ChainnedCommand(IChainedCommand nextCommand)
        {
            _nextCommand = nextCommand;
        }

        public void Execute(ProductRecord productRecord, Product product)
        {
            if (!CanBeHandled(productRecord, product) && _nextCommand != null)
            {
                _nextCommand.Execute(productRecord, product);
                return;
            }

            Run(productRecord, product);
        }

        protected abstract bool CanBeHandled(ProductRecord productRecord, Product product);

        protected abstract void Run(ProductRecord productRecord, Product product);
    }
}
