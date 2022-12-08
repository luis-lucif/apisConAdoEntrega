using SistemaGestion.Models;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;

namespace SistemaGestion.Repositories
{
    public class ProductoVendidoRepository
    {
        private SqlConnection? conexion;
        private String cadenaConexion =
                "Server=sql.bsite.net\\MSSQL2016;" +
                "Database=fedecipo_C#Coder;" +
                "User Id=fedecipo_C#Coder;" +
                "Password=Hearmyroar14;";

        public ProductoVendidoRepository()
        {
            try
            {
                conexion = new SqlConnection(cadenaConexion);
            }
            catch
            {
                throw;
            }
        }

        public List<ProductoVendido> listarProductoVendido()
        {
            List<ProductoVendido> listaProductoVendido = new List<ProductoVendido>();
            if (conexion == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM ProductoVendido", conexion))
                {
                    conexion.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ProductoVendido productoVendido = obtenerProductoVendidoDesdeReader(reader);
                                listaProductoVendido.Add(productoVendido);
                            }
                        }
                    }
                }
                conexion.Close();
            }
            catch
            {
                throw;
            }
            return listaProductoVendido;
        }

        public ProductoVendido? obtenerProductoVendido(long Id)
        {
            if (conexion == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM ProductoVendido WHERE Id = @Id", conexion))
                {
                    conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("Id", SqlDbType.BigInt) { Value = Id });
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            ProductoVendido productoVendido = obtenerProductoVendidoDesdeReader(reader);
                            return productoVendido;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                conexion.Close();
            }
        }

        public ProductoVendido crearProductoVendido(ProductoVendido productoVendido)
        {
            if (conexion == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO ProductoVendido(Stock, IdProducto, IdVenta) " +
                    "VALUES(@stock, @idProducto, @idVenta); SELECT @@Identity", conexion))
                {
                    conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("stock", SqlDbType.Int) { Value = productoVendido.Stock });
                    cmd.Parameters.Add(new SqlParameter("idProducto", SqlDbType.Int) { Value = productoVendido.IdProducto });
                    cmd.Parameters.Add(new SqlParameter("idVenta", SqlDbType.Int) { Value = productoVendido.IdVenta });
                    productoVendido.Id = long.Parse(cmd.ExecuteScalar().ToString());
                    return productoVendido;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                conexion.Close();
            }
        }
       
        public ProductoVendido? actualizarProductoVendido(long id, ProductoVendido productoVendidoAActualizar)
        {
            if (conexion == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                ProductoVendido? productoVendido = obtenerProductoVendido(id);
                if (productoVendido == null)
                {
                    return null;
                }
                List<string> camposAActualizar = new List<string>();
                if (productoVendido.Stock != productoVendidoAActualizar.Stock && productoVendidoAActualizar.Stock > 0)
                {
                    camposAActualizar.Add("stock = @stock");
                    productoVendido.Stock = productoVendidoAActualizar.Stock;
                }
                if (productoVendido.IdProducto != productoVendidoAActualizar.IdProducto && productoVendidoAActualizar.IdProducto > 0)
                {
                    camposAActualizar.Add("idProducto = @idProducto");
                    productoVendido.IdProducto = productoVendidoAActualizar.IdProducto;
                }
                if (productoVendido.IdVenta != productoVendidoAActualizar.IdVenta && productoVendidoAActualizar.IdVenta > 0)
                {
                    camposAActualizar.Add("idVenta = @idVenta");
                    productoVendido.IdVenta = productoVendidoAActualizar.IdVenta;
                }
                if (camposAActualizar.Count == 0)
                {
                    throw new Exception("No new fields to update");
                }
                using (SqlCommand cmd = new SqlCommand($"UPDATE Producto SET {String.Join(", ", camposAActualizar)} WHERE id = @id", conexion))
                {
                    cmd.Parameters.Add(new SqlParameter("stock", SqlDbType.Int) { Value = productoVendidoAActualizar.Stock });
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = productoVendidoAActualizar.IdProducto});
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = productoVendidoAActualizar.IdVenta });
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = id });
                    conexion.Open();
                    cmd.ExecuteNonQuery();
                    return productoVendido;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                conexion.Close();
            }
        }

        public bool eliminarProductoVendido(long Id)
        {
            if (conexion == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                int filasAfectadas = 0;
                using (SqlCommand cmd = new SqlCommand("DELETE FROM ProductoVendido WHERE Id = @Id", conexion))
                {
                    conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("Id", System.Data.SqlDbType.BigInt) { Value = Id });
                    filasAfectadas = cmd.ExecuteNonQuery();
                }
                conexion.Close();
                return filasAfectadas > 0;
            }
            catch
            {
                throw;
            }
        }

        private ProductoVendido obtenerProductoVendidoDesdeReader(SqlDataReader reader)
        {
            ProductoVendido productoVendido = new ProductoVendido();
            productoVendido.Id = long.Parse(reader["Id"].ToString());
            productoVendido.Stock = int.Parse(reader["Stock"].ToString());
            productoVendido.IdProducto = long.Parse(reader["IdProducto"].ToString());
            productoVendido.IdVenta = long.Parse(reader["IdVenta"].ToString());
            return productoVendido;
        }
    }
}