using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VentasLimpieza.core.Entities;
using VentasLimpieza.Infrastructure.Data.Configurations;

namespace VentasLimpieza.Infrastructure.Data
{
    public class PedidoConjfiguration : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> entity)
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("pedido");

            entity.HasIndex(e => e.Estado, "IX_Pedido_Estado");

            entity.HasIndex(e => e.FechaPedido, "IX_Pedido_FechaPedido");

            entity.HasIndex(e => e.ProductoId, "IX_Pedido_ProductoId");

            entity.HasIndex(e => e.UsuarioId, "IX_Pedido_UsuarioId");

            entity.Property(e => e.CostoEnvio).HasPrecision(10, 2);
            entity.Property(e => e.Estado).HasMaxLength(50);
            entity.Property(e => e.FechaPedido).HasColumnType("datetime");
            entity.Property(e => e.MetodoPago).HasMaxLength(50);
            entity.Property(e => e.PrecioUnitario).HasPrecision(10, 2);
            entity.Property(e => e.Total).HasPrecision(10, 2);

            entity.HasOne(d => d.Producto).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pedido_Producto");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pedido_Usuario");
        }
    }
}
