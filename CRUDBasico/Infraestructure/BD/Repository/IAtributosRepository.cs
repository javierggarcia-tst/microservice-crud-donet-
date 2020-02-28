using CRUDBasico.Infrastructure;
using CRUDBasico.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRUDBasico.Infrastructure.BD.Repository
{ 
    public interface IAtributosRepository 
    {

        List<Atributo> GetElements();

        Atributo GetElement(ISpecification<Atributo> specification);

        Task AddAsync(Atributo atributo);
        
        Task RemoveAsync(Atributo atributo);

        Task<Atributo> ModifyAsync(Atributo atributo);
    }
}
