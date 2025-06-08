using Pedidos_web.Models;
using Microsoft.EntityFrameworkCore;

namespace Pedidos_web.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Vendedor> Vendedores { get; set; }
        public DbSet<Producto> Productos { get; set; }
    }
}
