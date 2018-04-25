using Pricing.Helpers;
using Xunit;

namespace Pricing.Tests.Helpers
{
    public class DollarsToCentsConverterTests
    {
        [Fact]
        public void ConvertsDollarsToCents()
        {
            var converter = new DollarsToCentsConverter();

            converter.TryConvert("$11.22", out var cents);

            Assert.Equal(1122, cents);
        }

        [Fact]
        public void ShouldNotConvertPlns()
        {
            var converter = new DollarsToCentsConverter();

            var isConverted = converter.TryConvert("PLN11.22", out var cents);

            Assert.False(isConverted);
        }
    }
}
