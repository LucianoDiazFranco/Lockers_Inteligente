using System;
using LockersInteligentes.Dominio.Entidades;
using Microsoft.Data.SqlClient;

namespace LockersInteligentes.DAL.Repositorios
{
    public class RepositorioRol : RepositorioBase<Rol>
    {
        protected override string NombreTabla { get { return "dbo.Rol"; } }
        protected override string ColumnaId { get { return "IdRol"; } }
        protected override string FiltroPorId { get { return "IdRol = @Id"; } }

        protected override string SqlSelect
        {
            get { return "SELECT IdRol, Nombre, Descripcion FROM dbo.Rol"; }
        }
        protected override string SqlInsert
        {
            get { return "INSERT INTO dbo.Rol (Nombre, Descripcion) VALUES (@Nombre, @Descripcion)"; }
        }

        protected override string SqlUpdate
        {
            get
            {
                return "UPDATE dbo.Rol SET Nombre = @Nombre, Descripcion = @Descripcion " +
                       "WHERE IdRol = @Id";
            }
        }
        protected override Rol Mapear(SqlDataReader lector)
        {
            Rol rol = new Rol();
            rol.Id = Convert.ToInt32(lector["IdRol"]);
            rol.Nombre = lector["Nombre"].ToString();
            rol.Descripcion = lector["Descripcion"] == DBNull.Value
                ? null
                : lector["Descripcion"].ToString();
            return rol;
        }
        protected override void CargarParametros(SqlCommand comando, Rol entidad)
        {
            comando.Parameters.AddWithValue("@Nombre", entidad.Nombre);
            comando.Parameters.AddWithValue("@Descripcion",
                entidad.Descripcion != null ? (object)entidad.Descripcion : DBNull.Value);
        }
        public Rol ObtenerPorNombre(string nombre)
        {
            using (SqlConnection conexion = Conexion.Crear())
            using (SqlCommand comando = new SqlCommand(SqlSelect + " WHERE Nombre = @Nombre", conexion))
            {
                comando.Parameters.AddWithValue("@Nombre", nombre);
                conexion.Open();

                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    if (lector.Read())
                        return Mapear(lector);
                }
            }
            return null;
        }
    }
}