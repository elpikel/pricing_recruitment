using System.Collections.Generic;

namespace Pricing.Repositories.Abstract
{
    public interface IRepository<T>
    {
        T GetById(string id);
        void Update(T toUpdate);
        void Create(T toCreate);
        IList<T> GetAll();
    }
}
