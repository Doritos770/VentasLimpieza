using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VentasLimpieza.Api.Responses;
using VentasLimpieza.core.Dtos;
using VentasLimpieza.core.Entities;
using VentasLimpieza.Services.Interfaces;
using VentasLimpieza.Services.Validators;

namespace VentasLimpieza.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioServices;
        private readonly IMapper _mapper;
        private readonly UsuarioDtoValidator _usuarioDtoValidator;
        public UsuarioController(
             IUsuarioService usuarioServices,
             IMapper mapper,
             UsuarioDtoValidator usuarioDtoValidator)
        {
            _usuarioServices = usuarioServices;
            _mapper = mapper;
            _usuarioDtoValidator = usuarioDtoValidator;
        }

        #region sin dtos
        [HttpGet]
        public async Task<IActionResult> GetUsuario()
        {
            var usuarios = await _usuarioServices.GetAllUsersAsync();
            return Ok(usuarios);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuarioById(int id)
        {
            var usuario = await _usuarioServices.GetUsuarioByIdAsync(id);
            return Ok(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> InsertUsuario(Usuario usuario)
        {
            await _usuarioServices.RegistrarUsuario(usuario);
            return Created($"api/usuario/{usuario.Id}", usuario);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUsuario(Usuario usuario)
        {
            await _usuarioServices.UpdateUsuario(usuario);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUsuario(Usuario usuario)
        {
            await _usuarioServices.DeleteUsuario(usuario.Id);
            return NoContent();
        }
        #endregion

        #region conDtos
        [HttpGet("dto")]
        public async Task<IActionResult> GetDtoUsuario()
        {
            var usuarios = await _usuarioServices.GetAllUsersAsync();
            var usuariosDto = usuarios.Select(u => new UsuarioDto
            {
                Id = u.Id,
                Email = u.Email,
                Password = u.Password,
                Nombre = u.Nombre,
                Apellido = u.Apellido,
                Telefono = u.Telefono,
                FechaRegistro = u.FechaRegistro,
                IsActive = u.IsActive
            });
            return Ok(usuariosDto);
        }

        [HttpGet("dto/{id}")]
        public async Task<IActionResult> GetDtoUsuarioById(int id)
        {
            var usuario = await _usuarioServices.GetUsuarioByIdAsync(id);
            var usuarioDto = new UsuarioDto
            {
                Id = usuario.Id,
                Email = usuario.Email,
                Password = usuario.Password,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Telefono = usuario.Telefono,
                FechaRegistro = usuario.FechaRegistro,
                IsActive = usuario.IsActive
            };
            return Ok(usuarioDto);
        }

        [HttpPost("dto")]
        public async Task<IActionResult> InsertDtoUsuario(UsuarioDto usuarioDto)
        {
            var usuario = new Usuario
            {
                Id = usuarioDto.Id,
                Email = usuarioDto.Email,
                Password = usuarioDto.Password,
                Nombre = usuarioDto.Nombre,
                Apellido = usuarioDto.Apellido,
                Telefono = usuarioDto.Telefono,
                FechaRegistro = usuarioDto.FechaRegistro,
                IsActive = usuarioDto.IsActive
            };
            await _usuarioServices.RegistrarUsuario(usuario);
            return Created($"api/usuario/{usuario.Id}", usuario);
        }

        [HttpPut("dto/{id}")]
        public async Task<IActionResult> UpdateDtoUsuario(int id, [FromBody] UsuarioDto usuarioDto)
        {
            if (id != usuarioDto.Id)
                return BadRequest("El ID del usuario no coincide");

            var usuario = await _usuarioServices.GetUsuarioByIdAsync(id);
            if (usuario == null)
                return NotFound("Usuario no encontrado");

            // Mapear valor del DTO a la entidad
            usuario.Email = usuarioDto.Email;
            usuario.Password = usuarioDto.Password;
            usuario.Nombre = usuarioDto.Nombre;
            usuario.Apellido = usuarioDto.Apellido;
            usuario.Telefono = usuarioDto.Telefono;
            usuario.FechaRegistro = usuarioDto.FechaRegistro;
            usuario.IsActive = usuarioDto.IsActive;

            await _usuarioServices.UpdateUsuario(usuario);
            return NoContent();
        }

        [HttpDelete("dto/{id}")]
        public async Task<IActionResult> DeleteDtoUsuario(int id)
        {
            var usuario = await _usuarioServices.GetUsuarioByIdAsync(id);
            if (usuario == null)
                return NotFound("Usuario no encontrado");

            await _usuarioServices.DeleteUsuario(usuario.Id);
            return NoContent();
        }
        #endregion

        #region Mapper
        [HttpGet("dto/mapper")]
        public async Task<IActionResult> GetDtoMapperUsuario()
        {
            var usuarios = await _usuarioServices.GetAllUsersAsync();
            var usuariosDto = _mapper.Map<IEnumerable<UsuarioDto>>(usuarios);

            var response = new ApiResponse<IEnumerable<UsuarioDto>>(usuariosDto);
            return Ok(response);
        }

        [HttpGet("dto/mapper/{id}")]
        public async Task<IActionResult> GetDtoMapperUsuarioById(int id)
        {
            var usuario = await _usuarioServices.GetUsuarioByIdAsync(id);
            if (usuario == null)
                return NotFound(new { mensaje = "Usuario no encontrado" });

            var usuarioDto = _mapper.Map<UsuarioDto>(usuario);
            var response = new ApiResponse<UsuarioDto>(usuarioDto);
            return Ok(response);
        }

        [HttpPost("dto/mapper")]
        public async Task<IActionResult> InsertDtoMapperUsuario(UsuarioDto usuarioDto)
        {
            // Validar el DTO
            var validationResult = await _usuarioDtoValidator.ValidateAsync(usuarioDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(new
                {
                    message = "Error de validación",
                    errors = validationResult.Errors.Select(e => new
                    {
                        field = e.PropertyName,
                        error = e.ErrorMessage
                    })
                });
            }

            try
            {
                var usuario = _mapper.Map<Usuario>(usuarioDto);
                await _usuarioServices.RegistrarUsuario(usuario);

                var response = new ApiResponse<UsuarioDto>(usuarioDto);
                return Created($"api/usuario/{usuario.Id}", response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error al registrar el usuario",
                    error = ex.Message
                });
            }
        }

        [HttpPut("dto/mapper/{id}")]
        public async Task<IActionResult> UpdateDtoMapperUsuario(int id, [FromBody] UsuarioDto usuarioDto)
        {
            if (id != usuarioDto.Id)
                return BadRequest("El ID del usuario no coincide");

            // Validar el DTO
            var validationResult = await _usuarioDtoValidator.ValidateAsync(usuarioDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(new
                {
                    message = "Error de validación",
                    errors = validationResult.Errors.Select(e => new
                    {
                        field = e.PropertyName,
                        error = e.ErrorMessage
                    })
                });
            }

            var usuario = await _usuarioServices.GetUsuarioByIdAsync(id);
            if (usuario == null)
                return NotFound("Usuario no encontrado");

            try
            {
                _mapper.Map(usuarioDto, usuario);
                await _usuarioServices.UpdateUsuario(usuario);

                var response = new ApiResponse<UsuarioDto>(usuarioDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error al actualizar el usuario",
                    error = ex.Message
                });
            }
        }

        [HttpDelete("dto/mapper/{id}")]
        public async Task<IActionResult> DeleteDtoMapperUsuario(int id)
        {
            var usuario = await _usuarioServices.GetUsuarioByIdAsync(id);
            if (usuario == null)
                return NotFound("Usuario no encontrado");

            await _usuarioServices.DeleteUsuario(usuario.Id);
            return NoContent();
        }
        #endregion

        #region Cambiar Contraseña
        [HttpPut("cambiar-contrasena")]
        public async Task<IActionResult> CambiarContraseña([FromBody] UsuarioDto usuarioDto)
        {
            try
            {
                var usuario = _mapper.Map<Usuario>(usuarioDto);
                await _usuarioServices.ActualizarContraseña(usuario);
                return Ok(new
                {
                    success = true,
                    message = "Contraseña actualizada exitosamente"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    error = ex.Message
                });
            }
        }
        #endregion
    }
}