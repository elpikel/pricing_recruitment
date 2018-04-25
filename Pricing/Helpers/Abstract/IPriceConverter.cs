namespace Pricing.Helpers.Abstract
{
    public interface IPriceConverter
    {
        bool TryConvert(string price, out int convertedPrice);
    }
}