using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using VentasLimpieza.core.Entities;
using VentasLimpieza.Core.Interfaces;
using VentasLimpieza.Infrastructure.Data;

namespace VentasLimpieza.Infrastructure.Repositories
{
    public class PedidoRepository //: IPedidoRepository
    {
        public readonly VentasLimpiezaContext _pedido;

        public PedidoRepository(VentasLimpiezaContext pedido)
        {
            _pedido = pedido;
        }

        public async Task<IEnumerable<Pedido>> GetPedidosAsync()
        {
            var pedidos = await _pedido.Pedidos.ToListAsync();
            return pedidos;
        }
        public async Task<Pedido> GetPedidoByIdAsync(int id)
        {
            var pedido = await _pedido.Pedidos.FirstOrDefaultAsync(x => x.Id == id);
            return pedido;                
        }
        public async Task InsertPedido(Pedido direccion)
        {
            _pedido.Pedidos.Add(direccion);
            await _pedido.SaveChangesAsync();
        }

        public async Task UpdatePedido(Pedido direccion)
        {
            _pedido.Pedidos.Update(direccion);
            await _pedido.SaveChangesAsync();
        }
        public async Task DeletePedido(Pedido direccion)
        {
            _pedido.Pedidos.Remove(direccion);
            await _pedido.SaveChangesAsync();
        }
    }
}
