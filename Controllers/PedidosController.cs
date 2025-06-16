using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pedidos_web.Data;
using Pedidos_web.Models;
using Pedidos_web.Dtos;
using Microsoft.AspNetCore.Authorization;


namespace Pedidos_web.Controllers
{

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PedidosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CrearPedido([FromBody] PedidoDto pedidoDto)
        {
            if (pedidoDto == null || pedidoDto.Detalles == null || !pedidoDto.Detalles.Any())
            {
                return BadRequest("Datos del pedido incompletos.");
            }

            // Iniciar transacción
            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var pedido = new Pedido
                {
                    VendedorId = pedidoDto.VendedorId,
                    Fecha = DateTime.Now,
                    Detalles = new List<DetallePedido>()
                };

                decimal total = 0m;

                foreach (var detalleDto in pedidoDto.Detalles)
                {
                    var producto = await _context.Productos.FindAsync(detalleDto.ProductoId);
                    if (producto == null)
                    {
                        return BadRequest($"El producto con ID {detalleDto.ProductoId} no existe.");
                    }

                    if (producto.Stock < detalleDto.Cantidad)
                    {
                        return BadRequest($"No hay suficiente stock para el producto ID {producto.Id}. Stock disponible: {producto.Stock}");
                    }

                    // Restar stock
                    producto.Stock -= detalleDto.Cantidad;

                    var detalle = new DetallePedido
                    {
                        ProductoId = producto.Id,
                        Cantidad = detalleDto.Cantidad,
                        PrecioUnitario = producto.Precio
                    };

                    pedido.Detalles.Add(detalle);
                    total += detalle.Cantidad * detalle.PrecioUnitario;
                }

                pedido.Total = total;

                _context.Pedidos.Add(pedido);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return CreatedAtAction(nameof(GetPedido), new { id = pedido.Id }, pedido);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, $"Error al crear el pedido: {ex.Message}");
            }
        }




        // GET: api/pedidos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> GetPedido(int id)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.Detalles)
                .ThenInclude(d => d.Producto)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedido == null)
            {
                return NotFound();
            }

            return pedido;
        }

    }
}
