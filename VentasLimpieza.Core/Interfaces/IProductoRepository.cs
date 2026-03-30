using VentasLimpieza.core.Entities;

namespace VentasLimpieza.Core.Interfaces
{
    public interface IProductoRepository
    {
        Task<IEnumerable<Producto>> GetProductosAsync();
        Task<Producto> GetProductoByIdAsync(int id);
        Task InsertProducto(Producto producto);
        Task UpdateProducto(Producto producto);
        Task DeleteProducto(Producto producto);
    }
}