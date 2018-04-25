using System.Threading.Tasks;

namespace Pricing.Gateways.Abstract
{
    public interface IApiGateway
    {
        Task<string> Get(string url);
    }
}
