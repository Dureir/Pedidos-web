using Microsoft.AspNetCore.Mvc;
using Pedidos_web.Data;
using Pedidos_web.Models;
using Microsoft.EntityFrameworkCore;

namespace Pedidos_web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VendedoresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public VendedoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/vendedores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vendedor>>> GetVendedores()
        {
            return await _context.Vendedores.ToListAsync();
        }

        // GET: api/vendedores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vendedor>> GetVendedor(int id)
        {
            var vendedor = await _context.Vendedores.FindAsync(id);
            if (vendedor == null)
                return NotFound();

            return vendedor;
        }

        // POST: api/vendedores
        [HttpPost]
        public async Task<ActionResult<Vendedor>> CreateVendedor(Vendedor vendedor)
        {
            _context.Vendedores.Add(vendedor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVendedor), new { id = vendedor.Id }, vendedor);
        }

        // PUT: api/vendedores/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVendedor(int id, Vendedor vendedor)
        {
            if (id != vendedor.Id)
                return BadRequest();

            _context.Entry(vendedor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Vendedores.Any(e => e.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/vendedores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVendedor(int id)
        {
            var vendedor = await _context.Vendedores.FindAsync(id);
            if (vendedor == null)
                return NotFound();

            _context.Vendedores.Remove(vendedor);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

