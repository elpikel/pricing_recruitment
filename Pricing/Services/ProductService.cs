using Pricing.Commands.Abstract;
using Pricing.Gateways.Abstract;
using Pricing.Infrastructure.Abstract;
using Pricing.Models;
using Pricing.Repositories.Abstract;
using Pricing.Services.Abstract;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Pricing.Services
{
    public class ProductService
    {
        private readonly IPricingApiGateway _pricingApiGateway;
        private readonly IChainedCommand _command;
        private readonly IRepository<Product> _productRepository;
        private readonly IExecutionService _executionService;
        private readonly IRepository<Execution> _executionRepository;

        public ProductService(
            IPricingApiGateway pricingApiGateway, 
            IRepository<Product> productRepository,
            IExecutionService executionService,
            IChainedCommand command)
        {
            _pricingApiGateway = pricingApiGateway;
            _productRepository = productRepository;
            _executionService = executionService;
            _command = command;
        }

        public async Task Update()
        {
            if(_executionService.AlreadyDoneThisMonth())
            {
                return;
            }

            var pricing = await _pricingApiGateway.GetPricing(DateRange.LastMonth());

            foreach (var productRecord in pricing.ProductRecords)
            {
                var product = _productRepository.GetById(productRecord.Id);
                _command.Execute(productRecord, product);
            }

            _executionService.MarkAsDoneThisMonth();
        }
    }
}
