using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VentasLimpieza.core.Entities;
using VentasLimpieza.Infrastructure.Data.Configurations;

namespace VentasLimpieza.Infrastructure.Data
{
    public class ProductorConfiguration : IEntityTypeConfiguration<Producto>
    {
        public void Configure(EntityTypeBuilder<Producto> entity)
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("producto");

            entity.HasIndex(e => e.FechaCaducidad, "IX_Producto_FechaCaducidad");

            entity.HasIndex(e => e.Marca, "IX_Producto_Marca");

            entity.HasIndex(e => e.Nombre, "IX_Producto_Nombre");

            entity.HasIndex(e => e.Precio, "IX_Producto_Precio");

            entity.HasIndex(e => e.UsoEspecifico, "IX_Producto_UsoEspecifico");

            entity.Property(e => e.CantidadUnidades).HasPrecision(10, 2);
            entity.Property(e => e.Concentracion).HasMaxLength(50);
            entity.Property(e => e.Descripcion).HasColumnType("text");
            entity.Property(e => e.Fragancia).HasMaxLength(100);
            entity.Property(e => e.ImagenUrl).HasMaxLength(500);
            entity.Property(e => e.Ingredientes).HasColumnType("text");
            entity.Property(e => e.InstruccionesUso).HasColumnType("text");
            entity.Property(e => e.Marca).HasMaxLength(100);
            entity.Property(e => e.Nombre).HasMaxLength(200);
            entity.Property(e => e.Precauciones).HasColumnType("text");
            entity.Property(e => e.Precio).HasPrecision(10, 2);
            entity.Property(e => e.Presentacion).HasMaxLength(50);
            entity.Property(e => e.Unidades).HasMaxLength(20);
            entity.Property(e => e.UsoEspecifico).HasColumnType("enum('Limpieza de superficies','Cuidado personal','Mantenimiento de vehículos','Limpieza de textiles','Desinfección de ambientes','Cuidado de la piel','Mantenimiento de equipos','Limpieza de vidrios','Limpieza de suelos','Aromaterapia')");
        }
    }
}