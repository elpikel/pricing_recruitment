using System;

namespace Pricing.Models.Abstract
{
    public abstract class Entity
    {
        public string Id { get; set; }
        public TimeSpan InsertedAt { get; set; }
        public TimeSpan UpdatedAt { get; set; }

        public Entity()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}