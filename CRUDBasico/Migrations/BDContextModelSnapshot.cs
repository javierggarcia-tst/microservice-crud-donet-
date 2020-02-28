﻿// <auto-generated />
using CRUDBasico.Infrastructure.BD;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CRUDBasico.Migrations
{
    [DbContext(typeof(BDContext))]
    partial class BDContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CRUDBasico.Model.Atributo", b =>
                {
                    b.Property<int>("atributoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("idAtributo")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("descripcion")
                        .HasColumnName("vchAtributo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("atributoId");

                    b.ToTable("clips_atributos");
                });
#pragma warning restore 612, 618
        }
    }
}