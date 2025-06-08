using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pedidos_web.Models
{
    public class Vendedor
    {
        [Key]
        [Column("id_vendedor")]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public required string Nombre { get; set; }

        [Required]
        [StringLength(200)]
        public required string Correo { get; set; }

        [StringLength(40)]
        public string? Telefono { get; set; } // puede ser null
    }
}
