using Pricing.Models.Abstract;

namespace Pricing.Models
{
    public class Price : Entity
    {
        public string ProductId { get; set; }
        public int Cents { get; set; }

        public static Price Create(Product product)
        {
            return new Price
            {
                ProductId = product.Id,
                Cents = product.Price
            };
        }
    }
}
