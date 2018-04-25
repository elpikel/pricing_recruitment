using Pricing.Models;
using System.Threading.Tasks;

namespace Pricing.Gateways.Abstract
{
    public interface IPricingApiGateway
    {
        Task<Models.Pricing> GetPricing(DateRange dateRange);
    }
}