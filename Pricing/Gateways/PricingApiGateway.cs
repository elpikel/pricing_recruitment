using Newtonsoft.Json;
using Pricing.Gateways.Abstract;
using Pricing.Helpers.Abstract;
using Pricing.Infrastructure.Abstract;
using Pricing.Models;
using System;
using System.Threading.Tasks;

namespace Pricing.Gateways
{
    public class PricingApiGateway : IPricingApiGateway
    {
        private readonly ILogger _logger;
        private readonly IApiGateway _apiGateway;
        private readonly IUrlFormatter _urlFormatter;
        private const string StartDate = "start_date";
        private const string EndDate = "end_date";

        public PricingApiGateway(ILogger logger, IApiGateway apiGateway, IUrlFormatter urlFormatter)
        {
            _logger = logger;
            _apiGateway = apiGateway;
            _urlFormatter = urlFormatter;
        }

        public async Task<Models.Pricing> GetPricing(DateRange dateRange)
        {
            var formattedUrl = _urlFormatter.Format(new[] {
                new UrlParam(StartDate, dateRange.StartDate.ToShortDateString()),
                new UrlParam(EndDate, dateRange.EndDate.ToShortDateString())
            });

            var pricingJson = await _apiGateway.Get(formattedUrl);

            var pricing = JsonConvert.DeserializeObject<Models.Pricing>(pricingJson);

            if (pricing == null)
            {
                _logger.LogError($"could not deserialize pricing json: {pricingJson}");

                // I don't know how to deal with that so I'm closing application
                throw new ApplicationException($"could not deserialize pricing json: {pricingJson}");
            }

            return pricing;
        }
    }
}
