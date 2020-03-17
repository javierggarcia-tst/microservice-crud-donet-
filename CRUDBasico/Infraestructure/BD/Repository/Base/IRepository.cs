using System;
using System.Collections.Generic;
using System.Text;

namespace CRUDBasico.Infrastructure.BD.Repository
{
    public interface IRepository<T>
    {
        T GetById(int id);

        T GetSingleBySpec(ISpecification<T> spec);

        IEnumerable<T> ListAll();

        IEnumerable<T> List(ISpecification<T> spec);

        T Add(T entity);

        IEnumerable<T> Add(IEnumerable<T> entities);

        void Update(T entity);

        void Update(IEnumerable<T> entities);

        void Delete(T entity);

    }
}
