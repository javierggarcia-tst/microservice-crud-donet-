using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CRUDBasico.Model;

namespace CRUDBasico.Infrastructure.Specification
{
    public class AtributoSpecification : BaseSpecification<Atributo>, IAtributosSpecification
    {

        ISpecification<Atributo> IAtributosSpecification.GetAtributoById(int atributoId)
        {
            var spec = new AtributoSpecification();

            spec.SetCriteria(a => a.atributoId == atributoId);

            return spec;
        }
    }
}
