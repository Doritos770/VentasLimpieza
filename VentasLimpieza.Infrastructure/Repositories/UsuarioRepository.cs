using Microsoft.EntityFrameworkCore;
using VentasLimpieza.core.Entities;
using VentasLimpieza.core.Interfaces;
using VentasLimpieza.Infrastructure.Data;

namespace VentasLimpieza.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        public readonly VentasLimpiezaContext _usuario;

        public UsuarioRepository(VentasLimpiezaContext usuario)
        {
            _usuario = usuario;
        }
        public async Task<IEnumerable<Usuario>> GetUsuariosAsync()
        {
            var usuarios = await _usuario.Usuarios.ToListAsync();
            return usuarios;
        }
        public async Task<Usuario> GetUsuarioByIdAsync(int id)
        {
            var usuario = await _usuario.Usuarios.FirstOrDefaultAsync(x=> x.Id == id);
            return usuario;
        }
        public async Task InsertUsuario(Usuario usuario)
        {
            _usuario.Usuarios.Add(usuario);
            await _usuario.SaveChangesAsync();
        }
        public async Task UpdateUsuario(Usuario usuario)
        {
            _usuario.Usuarios.Update(usuario);
            await _usuario.SaveChangesAsync();
        }
        public async Task DeleteUsuario(Usuario usuario)
        {
            _usuario.Usuarios.Remove(usuario);
            await _usuario.SaveChangesAsync();
        }
        public async Task ModifiqueDireccionUsuario(int id ,Direccion direccion)
        {

        }
    }
}
