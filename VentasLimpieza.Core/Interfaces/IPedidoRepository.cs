using VentasLimpieza.core.Entities;

namespace VentasLimpieza.Core.Interfaces
{
    public interface IPedidoRepository
    {
        Task<IEnumerable<Pedido>> GetPedidosAsync();
        Task<Pedido> GetPedidoByIdAsync(int id);
        Task InsertPedido(Pedido direccion);
        Task UpdatePedido(Pedido direccion);
        Task DeletePedido(Pedido direccion);
    }
}