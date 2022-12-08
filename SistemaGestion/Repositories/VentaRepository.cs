using SistemaGestion.Models;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;

namespace SistemaGestion.Repositories
{
    public class VentaRepository
    {
        private SqlConnection? conexion;
        private String cadenaConexion =
                "Server=sql.bsite.net\\MSSQL2016;" +
                "Database=fedecipo_C#Coder;" +
                "User Id=fedecipo_C#Coder;" +
                "Password=Hearmyroar14;";

        public VentaRepository()
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

        public List<Venta> listarVenta()
        {
            List<Venta> listaVenta = new List<Venta>();
            if (conexion == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Venta", conexion))
                {
                    conexion.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Venta venta = obtenerVentaDesdeReader(reader);
                                listaVenta.Add(venta);
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
            return listaVenta;
        }

        public Venta? obtenerVenta(long Id)
        {
            if (conexion == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Venta WHERE Id = @Id", conexion))
                {
                    conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("Id", SqlDbType.BigInt) { Value = Id });
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            Venta venta = obtenerVentaDesdeReader(reader);
                            return venta;
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

        public Venta? actualizarVenta(long id, Venta ventaAActualizar)
        {
            if (conexion == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                Venta? venta = obtenerVenta(id);
                if (venta == null)
                {
                    return null;
                }
                List<string> camposAActualizar = new List<string>();
                if (venta.Comentarios != ventaAActualizar.Comentarios && !string.IsNullOrEmpty(ventaAActualizar.Comentarios))
                {
                    camposAActualizar.Add("comentarios = @comentarios");
                    venta.Comentarios = ventaAActualizar.Comentarios;
                }
                if (camposAActualizar.Count == 0)
                {
                    throw new Exception("No new fields to update");
                }
                using (SqlCommand cmd = new SqlCommand($"UPDATE Venta SET {String.Join(", ", camposAActualizar)} WHERE id = @id", conexion))
                {
                    cmd.Parameters.Add(new SqlParameter("comentarios", SqlDbType.VarChar) { Value = ventaAActualizar.Comentarios });
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = id });
                    conexion.Open();
                    cmd.ExecuteNonQuery();
                    return venta;
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

        public Venta crearVenta(Venta venta)
        {
            if (conexion == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Venta(Comentarios, IdUsuario) " +
                    "VALUES(@comentarios, @idUsuario); SELECT @@Identity", conexion))
                {
                    conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("comentarios", SqlDbType.VarChar) { Value = venta.Comentarios });
                    cmd.Parameters.Add(new SqlParameter("idUsuario", SqlDbType.BigInt) { Value = venta.IdUsuario });
                    venta.Id = long.Parse(cmd.ExecuteScalar().ToString());
                    return venta;
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

        public bool eliminarVenta(long Id)
        {
            if (conexion == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                int filasAfectadas = 0;
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Venta WHERE Id = @Id", conexion))
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

        private Venta obtenerVentaDesdeReader(SqlDataReader reader)
        {
            Venta venta = new Venta();
            venta.Id = long.Parse(reader["Id"].ToString());
            venta.Comentarios = reader["Comentarios"].ToString();
            venta.IdUsuario = long.Parse(reader["IdUsuario"].ToString());
            return venta;
        }
    }
}
