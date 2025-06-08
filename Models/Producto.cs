using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pedidos_web.Models
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)] // Ajustado a la longitud real en BD
        public required string Nombre { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")] // Para coincidir con la BD
        [Range(0.01, 1000000)]
        public decimal Precio { get; set; }

        [Required]
        public int Stock { get; set; }
    }
}
