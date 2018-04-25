using System.Collections.Generic;

namespace Pricing.Gateways.Models
{
    public class Pricing
    {
        public IReadOnlyCollection<ProductRecord> ProductRecords { get; set; }
    }
}
