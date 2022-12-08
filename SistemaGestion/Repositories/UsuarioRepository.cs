using SistemaGestion.Models;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;

namespace SistemaGestion.Repositories
{
    public class UsuarioRepository
    {
        private SqlConnection? conexion;
        private String cadenaConexion =
                "Server=sql.bsite.net\\MSSQL2016;" +
                "Database=fedecipo_C#Coder;" +
                "User Id=fedecipo_C#Coder;" +
                "Password=Hearmyroar14;";

        public UsuarioRepository()
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

        public List<Usuario> listarUsuario()
        {
            List<Usuario> listaUsuario = new List<Usuario>();
            if (conexion == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Usuario", conexion))
                {
                    conexion.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Usuario usuario = obtenerUsuarioDesdeReader(reader);
                                listaUsuario.Add(usuario);
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
            return listaUsuario;
        }

        public Usuario? obtenerUsuario(long Id)
        {
            if (conexion == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Usuario WHERE Id = @Id", conexion))
                {
                    conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("Id", SqlDbType.BigInt) { Value = Id });
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            Usuario usuario = obtenerUsuarioDesdeReader(reader);
                            return usuario;
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

        public Usuario? actualizarUsuario(long id, Usuario usuarioAActualizar)
        {
            if (conexion == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                Usuario? usuario = obtenerUsuario(id);
                if (usuario == null)
                {
                    return null;
                }
                List<string> camposAActualizar = new List<string>();
                if (usuario.Nombre != usuarioAActualizar.Nombre && !string.IsNullOrEmpty(usuarioAActualizar.Nombre))
                {
                    camposAActualizar.Add("nombre = @nombre");
                    usuario.Nombre = usuarioAActualizar.Nombre;
                }
                if (usuario.Apellido != usuarioAActualizar.Apellido && !string.IsNullOrEmpty(usuarioAActualizar.Apellido))
                {
                    camposAActualizar.Add("apellido = @apellido");
                    usuario.Nombre = usuarioAActualizar.Apellido;
                }
                if (usuario.NombreUsuario != usuarioAActualizar.NombreUsuario && !string.IsNullOrEmpty(usuarioAActualizar.NombreUsuario))
                {
                    camposAActualizar.Add("nombreUsuario = @nombreUsuario");
                    usuario.NombreUsuario = usuarioAActualizar.NombreUsuario;
                }
                if (usuario.Contraseña != usuarioAActualizar.Contraseña && !string.IsNullOrEmpty(usuarioAActualizar.Contraseña))
                {
                    camposAActualizar.Add("contraseña = @contraseña");
                    usuario.Contraseña = usuarioAActualizar.Contraseña;
                }
                if (usuario.Mail != usuarioAActualizar.Mail && !string.IsNullOrEmpty(usuarioAActualizar.Mail))
                {
                    camposAActualizar.Add("mail = @mail");
                    usuario.Mail = usuarioAActualizar.Mail;
                }
                if (camposAActualizar.Count == 0)
                {
                    throw new Exception("No new fields to update");
                }
                using (SqlCommand cmd = new SqlCommand($"UPDATE Usuario SET {String.Join(", ", camposAActualizar)} WHERE id = @id", conexion))
                {
                    cmd.Parameters.Add(new SqlParameter("nombre", SqlDbType.VarChar) { Value = usuarioAActualizar.Nombre });
                    cmd.Parameters.Add(new SqlParameter("apellido", SqlDbType.VarChar) { Value = usuarioAActualizar.Apellido });
                    cmd.Parameters.Add(new SqlParameter("nombreUsuario", SqlDbType.VarChar) { Value = usuarioAActualizar.NombreUsuario });
                    cmd.Parameters.Add(new SqlParameter("contraseña", SqlDbType.VarChar) { Value = usuarioAActualizar.Contraseña });
                    cmd.Parameters.Add(new SqlParameter("mail", SqlDbType.VarChar) { Value = usuarioAActualizar.Mail });
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = id });
                    conexion.Open();
                    cmd.ExecuteNonQuery();
                    return usuario;
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

        public Usuario crearUsuario(Usuario usuario)
        {
            if (conexion == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Usuario(Nombre, Apellido, NombreUsuario, Contraseña, Mail) " +
                    "VALUES(@nombre, @apellido, @nombreUsuario, @contraseña, @mail); SELECT @@Identity", conexion))
                {
                    conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("nombre", SqlDbType.VarChar) { Value = usuario.Nombre });
                    cmd.Parameters.Add(new SqlParameter("apellido", SqlDbType.VarChar) { Value = usuario.Apellido });
                    cmd.Parameters.Add(new SqlParameter("nombreUsuario", SqlDbType.VarChar) { Value = usuario.NombreUsuario });
                    cmd.Parameters.Add(new SqlParameter("contraseña", SqlDbType.VarChar) { Value = usuario.Contraseña });
                    cmd.Parameters.Add(new SqlParameter("mail", SqlDbType.VarChar) { Value = usuario.Mail });
                    usuario.Id = long.Parse(cmd.ExecuteScalar().ToString());
                    return usuario;
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

        public bool eliminarUsusario(long Id)
        {
            if (conexion == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                int filasAfectadas = 0;
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Usuario WHERE Id = @Id", conexion))
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

        private Usuario obtenerUsuarioDesdeReader(SqlDataReader reader)
        {
            Usuario usuario = new Usuario();
            usuario.Id = long.Parse(reader["Id"].ToString());
            usuario.Nombre = reader["Nombre"].ToString();
            usuario.Apellido = reader["Apellido"].ToString();
            usuario.NombreUsuario = reader["NombreUsuario"].ToString();
            usuario.Contraseña = reader["Contraseña"].ToString();
            usuario.Mail = reader["Mail"].ToString();
            return usuario;
        }
    }
}