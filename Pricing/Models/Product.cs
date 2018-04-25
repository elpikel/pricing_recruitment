using Pricing.Models.Abstract;

namespace Pricing.Models
{
    public class Product : Entity
    {
        public string ExternalProductId { get; set; }
        public int Price { get; set; }
        public string Name { get; set; }

        public void UpdatePrice(int productRecordPriceInCents)
        {
            Price = productRecordPriceInCents;
        }
    }
}
