using System;
using System.Collections.Generic;

namespace VentasLimpieza.core.Dtos;

public partial class ProductoDto
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string? Marca { get; set; }

    public string? UsoEspecifico { get; set; } 

    public string? Ingredientes { get; set; }

    public string? Concentracion { get; set; }

    public string? Fragancia { get; set; }

    public string Presentacion { get; set; } = null!;
    public int Cantidad { get; set; }

    public string Unidades { get; set; } = null!;

    public decimal CantidadUnidades { get; set; }

    public decimal Precio { get; set; }

    public DateOnly FechaCreacion { get; set; }

    public DateOnly? FechaCaducidad { get; set; }

    public string? InstruccionesUso { get; set; }

    public string? Precauciones { get; set; }

    public string? ImagenUrl { get; set; }

    
}
