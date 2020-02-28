using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Threading.Tasks;
using CRUDBasico.Model;
using System.Collections.Generic;

namespace CRUDBasico.Infrastructure.BD.Repository
{
    class AtributoRepository : IAtributosRepository
    {
        private readonly BDContext _context;

        public AtributoRepository(BDContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        async Task IAtributosRepository.AddAsync(Atributo atributo)
        {
            await this._context.Atributo.AddAsync(atributo);
            await this._context.SaveChangesAsync();
        }

        Atributo IAtributosRepository.GetElement(ISpecification<Atributo> specification)
        {
            return this._context.Atributo.Where(specification.Criteria).FirstOrDefault();
        }

        List<Atributo> IAtributosRepository.GetElements()
        {
            return this._context.Atributo.Select(x => x).ToList();
        }

        async Task<Atributo> IAtributosRepository.ModifyAsync(Atributo atributo)
        {
            EntityEntry<Atributo> update = this._context.Atributo.Update(atributo);
            await this._context.SaveChangesAsync();
            return update.Entity;
        }

        async Task IAtributosRepository.RemoveAsync(Atributo atributo)
        {
            this._context.Remove(atributo);
            await _context.SaveChangesAsync();
        }
    }
}
