using CRUDBasico.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRUDBasico.Infrastructure.BD
{
    class AtributoEntityTypeConfiguration : IEntityTypeConfiguration<Atributo>
    {
        void IEntityTypeConfiguration<Atributo>.Configure(EntityTypeBuilder<Atributo> builder)
        {
            builder.ToTable("clips_atributos");
            builder.HasKey(b => b.atributoId);
            builder.Property(p => p.atributoId).HasColumnName("idAtributo");
            builder.Property(p => p.descripcion).HasColumnName("vchAtributo");
        }
    }
}
