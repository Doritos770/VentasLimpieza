using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using VentasLimpieza.core.Entities;
using VentasLimpieza.Core.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace VentasLimpieza.Services.Validators
{
    public class RegistrarUsuarioValidator
    {
        private readonly IBaseRepository<Usuario> _usuarioRepository;

        public RegistrarUsuarioValidator(IBaseRepository<Usuario> usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task EmailYaExistente(string email)
        {
            var usuarios = await _usuarioRepository.GetAll();
            var existe = usuarios.Any(usuario => usuario.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
            if( existe == true)
            {
                throw new Exception("Esta cuenta ya fue registrada con este email");
            }
        }
        
    }
}
