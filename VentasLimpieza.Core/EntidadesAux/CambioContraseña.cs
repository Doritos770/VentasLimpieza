using System;
using System.Collections.Generic;
using System.Text;

namespace VentasLimpieza.Core.EntidadesAux
{

    public class SolicitarRecuperacionDto
    {
        public string Email { get; set; } = null!;
        public string? Telefono { get; set; }
    }

    public class VerificarCodigoDto
    {
        public string Email { get; set; } = null!;
        public string Codigo { get; set; } = null!;
        public string NuevaContraseña { get; set; } = null!;
    }
    public class CodigoRecuperacion
    {
        public string Codigo { get; set; }
        public DateTime Expiracion { get; set; }
        public int Intentos { get; set; }
    }
    public class SolicitarRecuperacionResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Email { get; set; }
        public string Codigo { get; set; }
        public bool TieneTelefono { get; set; }
    }
}
