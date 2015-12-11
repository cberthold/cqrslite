using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public interface IReadRepository<T>
        where T : IEntity
    {
        T GetById(Guid id);
        IQueryable<T> GetCollection();
        void Insert(T document);
        void Update(T document);
    }
}
