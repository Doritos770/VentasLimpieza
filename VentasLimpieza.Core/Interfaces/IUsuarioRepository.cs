using VentasLimpieza.core.Entities;

namespace VentasLimpieza.core.Interfaces; 

public interface IUsuarioRepository
{
    Task<IEnumerable<Usuario>> GetUsuariosAsync();
    Task<Usuario> GetUsuarioByIdAsync(int id);
    Task InsertUsuario(Usuario usuario);
    Task UpdateUsuario(Usuario usuario);
    Task DeleteUsuario(Usuario usuario);

    //proximanete implementado
//    Task ModifiqueDireccionUsuario(int id, Direccion direccion);
}