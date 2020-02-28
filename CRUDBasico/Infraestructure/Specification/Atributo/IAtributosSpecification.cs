using CRUDBasico.Infrastructure;
using CRUDBasico.Model;

namespace CRUDBasico.Infrastructure.Specification
{
    public interface IAtributosSpecification
    {
        ISpecification<Atributo> GetAtributoById(int atributoId);
    }
}
