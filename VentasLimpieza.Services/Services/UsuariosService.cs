using Microsoft.AspNetCore.Identity;
using VentasLimpieza.core.Entities;
using VentasLimpieza.Core.Interfaces;
using VentasLimpieza.Services.Interfaces;
using VentasLimpieza.Services.Validators;

namespace VentasLimpieza.Services.Services
{
    public class UsuariosService : IUsuarioService
    {
        public readonly IBaseRepository<Usuario> _usuarioRepository;
        public readonly RegistrarUsuarioValidator _usuarioValidator;
        public UsuariosService(IBaseRepository<Usuario> usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
            _usuarioValidator = new RegistrarUsuarioValidator(_usuarioRepository);
        }

        public async Task<IEnumerable<Usuario>> GetAllUsersAsync()
        {
            return await _usuarioRepository.GetAll();
            
        }

        public async Task<Usuario> GetUsuarioByIdAsync(int id)
        {
            return await _usuarioRepository.GetById(id);
        }

        public async Task RegistrarUsuario(Usuario usuario)
        {
            await _usuarioValidator.EmailYaExistente(usuario.Email);
            var user = await _usuarioRepository.GetById(usuario.Id);
            

            if (ContainsFobbidenWord(usuario.Apellido))
            {
                throw new Exception("Apellido no permitido");
            }
            if (ContainsFobbidenWord(usuario.Nombre))
            {
                throw new Exception("Nombre no permitido");
            }

            await _usuarioRepository.Add(usuario);
        }

        private bool ContainsFobbidenWord(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return false;

            foreach (var word in ForbbidenWords)
            {
                if (text.Contains(word, StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            return false;
        }
        public readonly string[] ForbbidenWords =
        {
            "pendejo", "pendeja","maricon", "marica", "culero", 
            "estupido", "estupida", "idiota", "imbecil", 
            "bastardo", "maldito", "maldita", "coño", "gilipollas", "boludo", 
            "pelotudo", "concha", "pichula", "weon", "weona", "culiao", "chucha", "mamaguevo",
            "baboso", "pervertido", "violador", "nazi"
        };

        public async Task UpdateUsuario(Usuario usuario)
        {
            await _usuarioRepository.Update(usuario);
        }
        public async Task DeleteUsuario(int id)
        {
             await _usuarioRepository.Delete(id);
        }


    }

    
}
