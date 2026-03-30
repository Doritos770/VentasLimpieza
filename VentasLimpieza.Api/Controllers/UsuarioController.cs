using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VentasLimpieza.core.Dtos;
using VentasLimpieza.core.Entities;
using VentasLimpieza.core.Interfaces;
using VentasLimpieza.Services.Interfaces;
using VentasLimpieza.Services.Services;

namespace VentasLimpieza.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        public UsuarioController(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        #region sin dtos
        [HttpGet]
        public async Task<IActionResult> GetUsuario()
        {
            var usuarios = await _usuarioRepository.GetUsuariosAsync();
            return Ok(usuarios);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuarioById(int id)
        {
            var usuario = await _usuarioRepository.GetUsuarioByIdAsync(id);
            return Ok(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> InsertUsuario(Usuario usuario)
        {
            await _usuarioRepository.InsertUsuario(usuario);
            return Created($"api/usuario/{usuario.Id}", usuario);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUsuario(Usuario usuario)
        {
            await _usuarioRepository.UpdateUsuario(usuario);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUsuario(Usuario usuario)
        {
            await _usuarioRepository.DeleteUsuario(usuario);
            return NoContent();
        }
        #endregion
        #region conDtos
        [HttpGet("dto")]
        public async Task<IActionResult> GetDtoUsuario()
        {
            var usuarios = await _usuarioRepository.GetUsuariosAsync();
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
            var usuario = await _usuarioRepository.GetUsuarioByIdAsync(id);
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
            await _usuarioRepository.InsertUsuario(usuario);
            return Created($"api/usuario/{usuario.Id}", usuario);
        }

        [HttpPut("dto/{id}")]
        public async Task<IActionResult> UpdateDtoUsuario(int id, [FromBody] UsuarioDto usuarioDto)
        {
            if (id != usuarioDto.Id)
                return BadRequest("El ID del usuario no coincide");

            var usuario = await _usuarioRepository.GetUsuarioByIdAsync(id);
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

            await _usuarioRepository.UpdateUsuario(usuario);
            return NoContent();
        }

        [HttpDelete("dto/{id}")]
        public async Task<IActionResult> DeleteDtoUsuario(int id)
        {
            var usuario = await _usuarioRepository.GetUsuarioByIdAsync(id);
            if (usuario == null)
                return NotFound("Usuario no encontrado");

            await _usuarioRepository.DeleteUsuario(usuario);
            return NoContent();
        }
        #endregion

        #region Mapper
        [HttpGet("dto/mapper")]
        public async Task<IActionResult> GetDtoMapperUsuario()
        {
            var usuarios = await _usuarioRepository.GetUsuariosAsync();
            var usuariosDto = _mapper.Map<IEnumerable<UsuarioDto>>(usuarios);
            return Ok(usuariosDto);
        }

        [HttpGet("dto/mapper/{id}")]
        public async Task<IActionResult> GetDtoMapperUsuarioById(int id)
        {
            var usuario = await _usuarioRepository.GetUsuarioByIdAsync(id);
            var usuarioDto = _mapper.Map<UsuarioDto>(usuario);
            return Ok(usuarioDto);
        }


        [HttpPost("dto/mapper")]
        public async Task<IActionResult> InsertDtoMapperUsuario(UsuarioDto usuarioDto)
        {
            var usuario = _mapper.Map<Usuario>(usuarioDto);
            await _usuarioRepository.InsertUsuario(usuario);
            return Created($"api/usuario/{usuario.Id}", usuario);
        }

        [HttpPut("dto/mapper/{id}")]
        public async Task<IActionResult> UpdateDtoMapperUsuario(int id, [FromBody] UsuarioDto usuarioDto)
        {
            if (id != usuarioDto.Id)
                return BadRequest("El ID del usuario no coincide");

            var usuario = await _usuarioRepository.GetUsuarioByIdAsync(id);
            if (usuario == null)
                return NotFound("Usuario no encontrado");

            // Mapear valores del DTO a la entidad existente
            _mapper.Map(usuarioDto, usuario);

            await _usuarioRepository.UpdateUsuario(usuario);
            return NoContent();
        }

        [HttpDelete("dto/mapper/{id}")]
        public async Task<IActionResult> DeleteDtoMapperUsuario(int id)
        {
            var usuario = await _usuarioRepository.GetUsuarioByIdAsync(id);
            if (usuario == null)
                return NotFound("Usuario no encontrado");

            await _usuarioRepository.DeleteUsuario(usuario);
            return NoContent();
        }
        #endregion

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] UsuarioDto usuarioDto)
        {
            try
            {
                var usuario = _mapper.Map<Usuario>(usuarioDto);
                await _usuarioService.RegistrarUsuario(usuario); //aca es por dependencia pero me tontie jodido aaaaaaaaaaaaa
                return Ok(new { success = true, message = "Usuario registrado exitosamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, error = ex.Message });
            }
        }
    }
}
