using VentasLimpieza.core.Entities;
using VentasLimpieza.Core.EntidadesAux;
using VentasLimpieza.Core.Interfaces;
using VentasLimpieza.Services.Interfaces;

namespace VentasLimpieza.Services.Services
{
    public class UsuariosService : IUsuarioService
    {
        public readonly IBaseRepository<Usuario> _usuarioRepository;
        private static Dictionary<string, CodigoRecuperacion> _codigosRecuperacion = new();
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
            await ValidateNoDuplicate(usuario);
            await ValidateTelefonoYaExistente(usuario.Telefono);
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

        public async Task<SolicitarRecuperacionResponse> SolicitarCodigoRecuperacion(string email, string telefono)
        {

            var usuarios = await _usuarioRepository.GetAll();
            var usuario = usuarios.FirstOrDefault(u =>
                u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));


            if (usuario == null)
                throw new Exception("No existe una cuenta con ese email");

            if (!string.IsNullOrEmpty(telefono))
            {
                if (usuario.Telefono != telefono)
                    throw new Exception("El número de teléfono no coincide con el registrado");
            }


            var codigo = new Random().Next(100000, 999999).ToString();


            _codigosRecuperacion[email.ToLower()] = new CodigoRecuperacion
            {
                Codigo = codigo,
                Expiracion = DateTime.Now.AddMinutes(10),
                Intentos = 0
            };


            return new SolicitarRecuperacionResponse
            {
                Success = true,
                Message = "Código enviado a tu correo electrónico",
                Email = usuario.Email,
                Codigo = codigo,
                TieneTelefono = !string.IsNullOrEmpty(usuario.Telefono)
            };
        }

        public async Task VerificarCodigoYCambiarContraseña(string email, string codigo, string nuevaContraseña)
        {
            
            if (!_codigosRecuperacion.TryGetValue(email.ToLower(), out var codigoGuardado))
                throw new Exception("No se ha solicitado recuperación para este email");

            // Verificar expiración
            if (codigoGuardado.Expiracion < DateTime.Now)
            {
                _codigosRecuperacion.Remove(email.ToLower());
                throw new Exception("El código ha expirado. Solicita uno nuevo");
            }

            
            if (codigoGuardado.Intentos >= 3)
            {
                _codigosRecuperacion.Remove(email.ToLower());
                throw new Exception("Demasiados intentos fallidos. Solicita un nuevo código");
            }

           
            if (codigoGuardado.Codigo != codigo)
            {
                codigoGuardado.Intentos++;
                throw new Exception($"Código incorrecto. Te quedan {3 - codigoGuardado.Intentos} intentos");
            }


            var usuarios = await _usuarioRepository.GetAll();
            var usuario = usuarios.FirstOrDefault(u =>
             u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

            if (usuario == null)
                throw new Exception("Usuario no encontrado");


            usuario.Password = nuevaContraseña;
            await _usuarioRepository.Update(usuario);


            _codigosRecuperacion.Remove(email.ToLower());
        }



        public async Task UpdateUsuario(Usuario usuario)
        {
            await _usuarioRepository.Update(usuario);
        }
        public async Task DeleteUsuario(int id)
        {
            await _usuarioRepository.Delete(id);
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

        private async Task ValidateNoDuplicate(Usuario usuario)
        {
            var usuarios = await _usuarioRepository.GetAll();

            // de aca es
            var existeMismaPersona = usuarios.Any(u =>
                u.Nombre.Equals(usuario.Nombre, StringComparison.OrdinalIgnoreCase) &&
                u.Apellido.Equals(usuario.Apellido, StringComparison.OrdinalIgnoreCase));

            if (existeMismaPersona)
                throw new Exception("Ya existe una cuenta registrada con este nombre y apellido");
        }
        public async Task ValidateTelefonoYaExistente(string telefono)
        {
            if (string.IsNullOrWhiteSpace(telefono))
                return;

            var usuarios = await _usuarioRepository.GetAll();
            var existe = usuarios.Any(u => u.Telefono == telefono);

            if (existe)
                throw new Exception("Este número de telefono ya esta registrado");
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




    }

    
}
