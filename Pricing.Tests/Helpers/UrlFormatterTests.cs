using Pricing.Helpers;
using Pricing.Models;
using System.Collections.Generic;
using Xunit;

namespace Pricing.Tests.Helpers
{
    public class UrlFormatterTests
    {
        [Fact]
        public void AddsParameters()
        {
            var urlFormatter = new UrlFormatter("https://omegapricinginc.com/pricing/records.json");

            var formatterUrl = urlFormatter.Format(new List<UrlParam> { new UrlParam("key", "value")});

            Assert.Equal("https://omegapricinginc.com/pricing/records.json?key=value", formatterUrl);
        }

        [Fact]
        public void ReturnBaseUrlWhenNoParametersArePassed()
        {
            var urlFormatter = new UrlFormatter("https://omegapricinginc.com/pricing/records.json");

            var formatterUrl = urlFormatter.Format(new List<UrlParam>());

            Assert.Equal("https://omegapricinginc.com/pricing/records.json", formatterUrl);
        }

        [Fact]
        public void ReturnBaseUrlWhenNullIsPassed()
        {
            var urlFormatter = new UrlFormatter("https://omegapricinginc.com/pricing/records.json");

            var formatterUrl = urlFormatter.Format(null);

            Assert.Equal("https://omegapricinginc.com/pricing/records.json", formatterUrl);
        }
    }
}
