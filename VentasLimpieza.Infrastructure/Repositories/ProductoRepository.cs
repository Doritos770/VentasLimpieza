using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using VentasLimpieza.core.Entities;
using VentasLimpieza.Core.Interfaces;
using VentasLimpieza.Infrastructure.Data;

namespace VentasLimpieza.Infrastructure.Repositories
{
    public class ProductoRepository : IProductoRepository
    {
        public readonly VentasLimpiezaContext _productos;

        public ProductoRepository(VentasLimpiezaContext productos)
        {
            _productos = productos;
        }
        public async Task<IEnumerable<Producto>> GetProductosAsync()
        {
            var productos = await _productos.Productos.ToListAsync();
            return productos;
        }
        public async Task<Producto> GetProductoByIdAsync(int id)
        {
            var producto = await _productos.Productos.FirstOrDefaultAsync(x => x.Id == id);
            return producto;
        }
        public async Task InsertProducto(Producto producto)
        {
            _productos.Productos.Add(producto);
            await _productos.SaveChangesAsync();
        }

        public async Task UpdateProducto(Producto producto)
        {
            _productos.Productos.Update(producto);
            await _productos.SaveChangesAsync();
        }
        public async Task DeleteProducto(Producto producto)
        {
            _productos.Productos.Remove(producto);
            await _productos.SaveChangesAsync();
        }
    }
}
