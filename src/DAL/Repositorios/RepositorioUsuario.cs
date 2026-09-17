using System;
using LockersInteligentes.Dominio.Arquitectura;
using LockersInteligentes.Dominio.Entidades;
using Microsoft.Data.SqlClient;

namespace LockersInteligentes.DAL.Repositorios
{
   public class RepositorioUsuario : RepositorioBase<Usuario>
    {
        protected override string NombreTabla { get { return "dbo.Usuario"; } }
        protected override string ColumnaId { get { return "IdUsuario"; } }
        protected override string FiltroPorId { get { return "u.IdUsuario = @Id"; } }
        protected override string SqlSelect
        {
            get
            {
                return
                    "SELECT u.IdUsuario, u.NombreUsuario, u.Nombre, u.Apellido, u.Correo, " +
                    "       u.PasswordHash, u.PasswordSalt, u.Activo, " +
                    "       r.IdRol, r.Nombre AS RolNombre, r.Descripcion AS RolDescripcion, " +
                    "       i.NumIdioma, i.Nombre AS IdiomaNombre, i.Codigo AS IdiomaCodigo, " +
                    "       i.EsPredeterminado " +
                    "FROM dbo.Usuario u " +
                    "INNER JOIN dbo.Rol r ON r.IdRol = u.IdRol " +
                    "LEFT JOIN dbo.Idioma i ON i.NumIdioma = u.IdIdioma";
            }
        }
        protected override string SqlInsert
        {
            get
            {
                return
                    "INSERT INTO dbo.Usuario (NombreUsuario, Nombre, Apellido, Correo, PasswordHash, " +
                    "                         PasswordSalt, IdRol, IdIdioma, Activo) " +
                    "VALUES (@NombreUsuario, @Nombre, @Apellido, @Correo, @PasswordHash, " +
                    "        @PasswordSalt, @IdRol, @IdIdioma, @Activo)";
            }
        }
        protected override string SqlUpdate
        {
            get
            {
                return
                    "UPDATE dbo.Usuario SET NombreUsuario = @NombreUsuario, Nombre = @Nombre, " +
                    "       Apellido = @Apellido, Correo = @Correo, PasswordHash = @PasswordHash, " +
                    "       PasswordSalt = @PasswordSalt, IdRol = @IdRol, " +
                    "       IdIdioma = @IdIdioma, Activo = @Activo " +
                    "WHERE IdUsuario = @Id";
            }
        }
        public Usuario ObtenerPorNombreUsuario(string nombreUsuario)
        {
            if (string.IsNullOrWhiteSpace(nombreUsuario))
                return null;

            using (SqlConnection conexion = Conexion.Crear())
            using (SqlCommand comando = new SqlCommand(
                SqlSelect + " WHERE u.NombreUsuario = @NombreUsuario", conexion))
            {
                comando.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);
                conexion.Open();

                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    if (lector.Read())
                        return Mapear(lector);
                }
            }

            return null;
        }
        public override void Eliminar(int id)
        {
            using (SqlConnection conexion = Conexion.Crear())
            using (SqlCommand comando = new SqlCommand(
                "UPDATE dbo.Usuario SET Activo = 0 WHERE IdUsuario = @Id", conexion))
            {
                comando.Parameters.AddWithValue("@Id", id);
                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        protected override Usuario Mapear(SqlDataReader lector)
        {
            Usuario usuario = new Usuario();

            usuario.Id = Convert.ToInt32(lector["IdUsuario"]);
            usuario.NombreUsuario = lector["NombreUsuario"].ToString();
            usuario.Nombre = lector["Nombre"].ToString();
            usuario.Apellido = lector["Apellido"].ToString();
            usuario.Correo = lector["Correo"] == DBNull.Value ? null : lector["Correo"].ToString();
            usuario.PasswordHash = lector["PasswordHash"].ToString();
            usuario.PasswordSalt = lector["PasswordSalt"].ToString();
            usuario.Activo = Convert.ToBoolean(lector["Activo"]);

            usuario.Rol = new Rol();
            usuario.Rol.Id = Convert.ToInt32(lector["IdRol"]);
            usuario.Rol.Nombre = lector["RolNombre"].ToString();
            usuario.Rol.Descripcion = lector["RolDescripcion"] == DBNull.Value
                ? null
                : lector["RolDescripcion"].ToString();

            if (lector["NumIdioma"] != DBNull.Value)
            {
                usuario.IdiomaPreferido = new Idioma();
                usuario.IdiomaPreferido.Id = Convert.ToInt32(lector["NumIdioma"]);
                usuario.IdiomaPreferido.Nombre = lector["IdiomaNombre"].ToString();
                usuario.IdiomaPreferido.Codigo = lector["IdiomaCodigo"].ToString();
                usuario.IdiomaPreferido.EsPredeterminado = Convert.ToBoolean(lector["EsPredeterminado"]);
            }
            return usuario;
        }
        protected override void CargarParametros(SqlCommand comando, Usuario entidad)
        {
            comando.Parameters.AddWithValue("@NombreUsuario", entidad.NombreUsuario);
            comando.Parameters.AddWithValue("@Nombre", entidad.Nombre);
            comando.Parameters.AddWithValue("@Apellido", entidad.Apellido);
            comando.Parameters.AddWithValue("@Correo",entidad.Correo != null ? (object)entidad.Correo : DBNull.Value);
            comando.Parameters.AddWithValue("@PasswordHash", entidad.PasswordHash);
            comando.Parameters.AddWithValue("@PasswordSalt", entidad.PasswordSalt);
            comando.Parameters.AddWithValue("@IdRol", entidad.Rol.Id);
            comando.Parameters.AddWithValue("@Activo", entidad.Activo);

            comando.Parameters.AddWithValue("@IdIdioma",
                entidad.IdiomaPreferido != null ? (object)entidad.IdiomaPreferido.Id : DBNull.Value);
        }
        public bool ExisteNombreUsuario(string nombreUsuario, int idExcluido)
        {
            using (SqlConnection conexion = Conexion.Crear())
            using (SqlCommand comando = new SqlCommand(
                "SELECT COUNT(1) FROM dbo.Usuario " +
                "WHERE NombreUsuario = @NombreUsuario AND IdUsuario <> @IdExcluido", conexion))
            {
                comando.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);
                comando.Parameters.AddWithValue("@IdExcluido", idExcluido);
                conexion.Open();
                return Convert.ToInt32(comando.ExecuteScalar()) > 0;
            }
        }
        public int ContarAdministradoresActivos()
        {
            using (SqlConnection conexion = Conexion.Crear())
            using (SqlCommand comando = new SqlCommand(
                "SELECT COUNT(1) FROM dbo.Usuario u " +
                "INNER JOIN dbo.Rol r ON r.IdRol = u.IdRol " +
                "WHERE u.Activo = 1 AND r.Nombre = N'Administrador'", conexion))
            {
                conexion.Open();
                return Convert.ToInt32(comando.ExecuteScalar());
            }
        }
    }
}