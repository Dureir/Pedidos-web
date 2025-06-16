// DTO para Pedido que incluye lista de detalles
public class PedidoDto
{
    public int VendedorId { get; set; }
    public List<DetallePedidoDto> Detalles { get; set; } = new();
}