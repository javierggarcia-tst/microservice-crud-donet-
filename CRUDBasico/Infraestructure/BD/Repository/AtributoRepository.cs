using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Threading.Tasks;
using CRUDBasico.Model;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CRUDBasico.Infrastructure.BD.Repository
{
    public class AtributoRepository : BaseRepository<Atributo>, IAtributosRepository
    {

        public AtributoRepository(BDContext context) : base(context)
        {
        }

        async Task IAtributosRepository.AddAsync(Atributo atributo)
        {
            await this._context.Atributo.AddAsync(atributo);
            await this._context.SaveChangesAsync();
        }

        Atributo IAtributosRepository.GetElement(ISpecification<Atributo> specification)
        {
            return this._context.Atributo.AsNoTracking().Where(specification.Criteria).FirstOrDefault();
        }

        List<Atributo> IAtributosRepository.GetElements()
        {
            return this._context.Atributo.Select(x => x).ToList();
        }

        async Task<Atributo> IAtributosRepository.ModifyAsync(Atributo atributo)
        {
            try
            {
                EntityEntry<Atributo> update = this._context.Atributo.Update(atributo);
                await this._context.SaveChangesAsync();
                return update.Entity;

            }
            catch (Exception e)
            {
                var kk = e;
                return null;
            }

           
        }

        async Task IAtributosRepository.RemoveAsync(Atributo atributo)
        {
            this._context.Remove(atributo);
            await _context.SaveChangesAsync();
        }
    }
}
