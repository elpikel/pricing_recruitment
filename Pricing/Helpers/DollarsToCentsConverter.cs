using Pricing.Helpers.Abstract;

namespace Pricing.Helpers
{
    public class DollarsToCentsConverter : IPriceConverter
    {
        public bool TryConvert(string priceInDollars, out int convertedPrice)
        {
            convertedPrice = 0;
            var priceWithoutCurrency = priceInDollars.Replace("$", "");

            if (decimal.TryParse(priceWithoutCurrency, out var price))
            {
                convertedPrice = (int)(price * 100);
                return true;
            }

            return false;
        }
    }
}
