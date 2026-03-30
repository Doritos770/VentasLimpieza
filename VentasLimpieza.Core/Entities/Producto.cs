using System;
using System.Collections.Generic;
using VentasLimpieza.Core.Entities;

namespace VentasLimpieza.core.Entities;

public partial class Producto : BaseEntity
{
    //public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string? Marca { get; set; }

    public string? UsoEspecifico { get; set; }

    public string? Ingredientes { get; set; }

    public string? Concentracion { get; set; }

    public string? Fragancia { get; set; }

    public string Presentacion { get; set; } = null!;

    public string Unidades { get; set; } = null!;

    public decimal CantidadUnidades { get; set; }

    public decimal Precio { get; set; }

    public int Cantidad { get; set; }

    public DateOnly? FechaCaducidad { get; set; }

    public string? InstruccionesUso { get; set; }

    public string? Precauciones { get; set; }

    public string? ImagenUrl { get; set; }

    public DateOnly FechaCreacion { get; set; }

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

    public virtual ICollection<Resena> Resenas { get; set; } = new List<Resena>();
}
