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

        public UsuariosService(IBaseRepository<Usuario> usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
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
            await GetUsuarioByEmail(usuario.Email);
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
        public async Task ActualizarContraseña(Usuario usuario)
        {

            var usuarioExistente = await ValidarUsuarioExiste(usuario.Id);


            if (!CompararCampos(usuarioExistente, usuario))
                throw new Exception("Los datos de validación no coinciden");


            usuarioExistente.Password = usuario.Password;
            await _usuarioRepository.Update(usuarioExistente);
        }




        //funciones auxiliares----------------------------------------------------------------------------
        private async Task GetUsuarioByEmail(string email)
        {
            var usuarios = await _usuarioRepository.GetAll();
            var existe = usuarios.Any(usuario => usuario.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
            if (existe == true)
            {
                throw new Exception("Esta cuenta ya fue registrada con este email");
            }
        }


        private async Task<Usuario> ValidarUsuarioExiste(int id)
        {
            var usuario = await GetUsuarioByIdAsync(id);

            if (usuario == null)
                throw new Exception("Usuario no encontrado");

            return usuario;
        }

        private bool CompararCampos(Usuario existente, Usuario nuevo)
        {
            return existente.Nombre == nuevo.Nombre &&
                   existente.Apellido == nuevo.Apellido &&
                   existente.Email == nuevo.Email &&
                   existente.Telefono == nuevo.Telefono;
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
