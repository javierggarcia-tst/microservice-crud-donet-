using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRUDBasico.Infrastructure.BD.Repository
{
    public interface IAsyncRepository<T>
    {
        Task<T> GetByIdAsync(int id);

        Task<List<T>> ListAllAsync();

        Task<List<T>> ListAsync(ISpecification<T> spec);

        Task<T> AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);
    }
}
