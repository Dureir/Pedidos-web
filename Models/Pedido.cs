using Pedidos_web.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

[Table("pedidos")]
public class Pedido
{
    [Key]
    [Column("id_pedido")] // ← o el nombre exacto en tu tabla
    public int Id { get; set; }

    [Required]
    [Column("fecha_pedido")]
    public DateTime Fecha { get; set; } = DateTime.Now;

    [Required]
    [Column("id_vendedor")]
    public int VendedorId { get; set; }

    [ForeignKey("VendedorId")]
    public  Vendedor Vendedor { get; set; }

    [Required]
    public decimal Total { get; set; }


    public required ICollection<DetallePedido> Detalles { get; set; } = new List<DetallePedido>();

}
