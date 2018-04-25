using Pricing.Gateways.Abstract;
using Pricing.Infrastructure.Abstract;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Pricing.Gateways
{
    public class ApiGateway : IApiGateway
    {
        private readonly ILogger _logger;
        private readonly string _apiKey;

        public ApiGateway(ILogger logger, string apiKey)
        {
            _logger = logger;
            _apiKey = apiKey;
        }

        public async Task<string> Get(string url)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
                var response = await client.GetAsync(url); // TODO: add error handling
                var content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return content;
                }

                _logger.LogError($"There was problem with : {url}, status code: {response.StatusCode}, message: {content}");

                // I don't know how to deal with that so I'm closing application
                throw new ApplicationException($"There was problem with : {url}, status code: {response.StatusCode}, message: {content}");
            }
        }
    }
}
