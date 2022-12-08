namespace SistemaGestion.Models
{
    public class ProductoVendido
    {
        public long Id { get; set; }
        public int Stock { get; set; }
        public long IdProducto { get; set; }
        public long IdVenta { get; set; }

        public ProductoVendido(long id, int stock, long idProducto, long idVenta)
        {
            Id = id;
            Stock = stock;
            IdProducto = idProducto;
            IdVenta = idVenta;
        }

        public ProductoVendido()
        {
            Id = 0;
            Stock = 0;
            IdProducto = 0;
            IdVenta = 0;
        }
    }
}
