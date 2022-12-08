namespace SistemaGestion.Models
{
    public class Producto
    {
        public long Id { get; set; }
        public string Descripciones { get; set; }
        public float? Costo { get; set; }
        public float? PrecioVenta { get; set; }
        public int Stock { get; set; }
        public long IdUsuario { get; set; }

        public Producto(long id, string descripciones, float? costo, float? precioVenta, int stock, long idUsuario)
        {
            Id = id;
            Descripciones = descripciones;
            Costo = costo;
            PrecioVenta = precioVenta;
            Stock = stock;
            IdUsuario = idUsuario;
        }

        public Producto()
        {
            Id = 0;
            Descripciones = "";
            Costo = 0;
            PrecioVenta = 0;
            Stock = 0;
            IdUsuario = 0;
        }
    }
}
