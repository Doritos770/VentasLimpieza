using System;
using System.Collections.Generic;
using VentasLimpieza.Core.Entities;

namespace VentasLimpieza.core.Entities;

public partial class Resena : BaseEntity
{
    //public int Id { get; set; }

    public int UsuarioId { get; set; }

    public int ProductoId { get; set; }

    public int Calificacion { get; set; }

    public string? Comentario { get; set; }

    public DateOnly Fecha { get; set; }

    public virtual Producto Producto { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
