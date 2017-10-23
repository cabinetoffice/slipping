using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Triad.CabinetOffice.PAWS.API.Repositories
{
    interface IRepository<T>
    {
        IEnumerable<T> List();
        T Get(int Id);
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
