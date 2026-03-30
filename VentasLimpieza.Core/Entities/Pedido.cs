using System;
using System.Collections.Generic;
using VentasLimpieza.Core.Entities;

namespace VentasLimpieza.core.Entities;

public partial class Pedido : BaseEntity
{
   // public int Id { get; set; }

    public int UsuarioId { get; set; }

    public int ProductoId { get; set; }

    public DateTime FechaPedido { get; set; }

    public string Estado { get; set; } = null!;

    public decimal CostoEnvio { get; set; }

    public string MetodoPago { get; set; } = null!;

    public int CantidadProducto { get; set; }

    public decimal PrecioUnitario { get; set; }

    public decimal Total { get; set; }

    public virtual Producto Producto { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
