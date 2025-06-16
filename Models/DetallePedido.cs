using Pedidos_web.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

[Table("DetallePedidos")] // ← si ese es el nombre real de la tabla
public class DetallePedido
{
    [Key]
    [Column("id_detalle")]
    public int Id { get; set; }

    [Required]
    [Column("id_pedido")]
    public int PedidoId { get; set; }

    [ForeignKey("PedidoId")]
    public Pedido Pedido { get; set; }  

    [Required]
    [Column("id_producto")]
    public int ProductoId { get; set; }

    [ForeignKey("ProductoId")]
    public Producto Producto { get; set; }

    [Required]
    [Column("cantidad")]
    public int Cantidad { get; set; }

    [Required]
    [Column("precio_unitario")]
    public decimal PrecioUnitario { get; set; }

}
