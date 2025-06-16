namespace Pedidos_web.Dtos
{
    public class PedidoRespuestaDto
    {
        public int Id { get; set; }
        public int VendedorId { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public List<DetalleRespuestaDto> Detalles { get; set; } = new();
    }

    public class DetalleRespuestaDto
    {
        public int ProductoId { get; set; }
        public string NombreProducto { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}
