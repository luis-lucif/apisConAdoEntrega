namespace SistemaGestion.Models
{
    public class Venta
    {
        public long Id { get; set; }
        public string Comentarios { get; set; }
        public long IdUsuario { get; set; }

        public Venta(long id, string comentarios, long idUsuario)
        {
            Id = id;
            Comentarios = comentarios;
            IdUsuario = idUsuario;
        }

        public Venta()
        {
            Id = 0;
            Comentarios = "";
            IdUsuario = 0;
        }
    }
}
