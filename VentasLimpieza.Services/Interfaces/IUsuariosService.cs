using System;
using System.Collections.Generic;
using System.Text;
using VentasLimpieza.core.Entities;
using VentasLimpieza.Core.Interfaces;

namespace VentasLimpieza.Services.Interfaces
{
    public interface IUsuarioService
    {
        //services va acciones de negocio
        Task<IEnumerable<Usuario>> GetAllUsersAsync();//a todos
        Task<Usuario> GetUsuarioByIdAsync(int id);//con id
        Task RegistrarUsuario(Usuario usuario);
        Task UpdateUsuario(Usuario usuario);
        Task DeleteUsuario(int id);
        Task ActualizarContraseña(Usuario usuario);
        //Task<Usuario> GetUsuarioByEmail(string email);
        //Task<bool> EmailExists(string email);
    }
}
