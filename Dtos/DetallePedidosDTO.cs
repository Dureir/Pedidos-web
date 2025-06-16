// DTO para DetallePedido que solo espera IDs y cantidad
public class DetallePedidoDto
{
    public int ProductoId { get; set; }
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
}
