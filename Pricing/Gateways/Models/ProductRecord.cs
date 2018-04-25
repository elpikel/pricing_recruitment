namespace Pricing.Gateways.Models
{
    public class ProductRecord
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Price { get; set; }
        public bool Discontinued { get; set; }
    }
}
