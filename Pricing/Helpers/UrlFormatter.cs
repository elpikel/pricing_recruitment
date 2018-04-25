using Pricing.Helpers.Abstract;
using Pricing.Models;
using System;
using System.Collections.Generic;
using System.Web;

namespace Pricing.Helpers
{
    public class UrlFormatter : IUrlFormatter
    {
        private readonly Uri _url;

        public UrlFormatter(string baseUrl)
        {
            _url = new Uri(baseUrl);
        }

        public string Format(IReadOnlyCollection<UrlParam> urlParams)
        {
            if(urlParams == null)
            {
                return _url.AbsoluteUri;
            }

            return AddParameters(_url, urlParams).AbsoluteUri;
        }

        private Uri AddParameters(Uri url, IReadOnlyCollection<UrlParam> urlParams)
        {
            var uriBuilder = new UriBuilder(url);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);

            foreach (var urlParam in urlParams)
            {
                query[urlParam.Key] = urlParam.Value;
            }
           
            uriBuilder.Query = query.ToString();

            return uriBuilder.Uri;
        }
    }
}
