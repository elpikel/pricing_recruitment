using Pricing.Models.Abstract;
using Pricing.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pricing.Repositories
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        private IList<T> _db = new List<T>();

        public void Create(T toCreate)
        {
            toCreate.InsertedAt = TimeSpan.FromMilliseconds(DateTime.UtcNow.Millisecond);
            toCreate.UpdatedAt = TimeSpan.FromMilliseconds(DateTime.UtcNow.Millisecond);
            _db.Add(toCreate);
        }

        public T GetById(string id)
        {
            return _db.SingleOrDefault(item => string.Equals(item.Id, id));
        }

        public void Update(T toUpdate)
        {
            toUpdate.UpdatedAt = TimeSpan.FromMilliseconds(DateTime.UtcNow.Millisecond);
            _db[_db.IndexOf(GetById(toUpdate.Id))] = toUpdate;
        }

        public IList<T> GetAll()
        {
            return _db;
        }
    }
}
