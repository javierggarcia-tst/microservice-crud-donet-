using CRUDBasico.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace CRUDBasico.Infrastructure.BD
{
    public class BDContext : DbContext
    {

        public DbSet<Atributo> Atributo { get; set; }

        public BDContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BDContext).Assembly);
        }
    }
}
