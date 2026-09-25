using LockersInteligentes.Dominio.Entidades;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockersInteligentes.DAL.Repositorios
{
    public class RepositorioRepartidor : RepositorioBase<Repartidor>
    {
        protected override string NombreTabla { get { return "dbo.Repartidor"; } }
        protected override string ColumnaId { get { return "IdRepartidor"; } }
        protected override string FiltroPorId { get { return "IdRepartidor = @Id"; } }

        protected override string SqlSelect
        {
            get
            {
                return "SELECT IdRepartidor, Nombre, Apellido, Dni, Empresa, Telefono, Activo " +
                       "FROM dbo.Repartidor";
            }
        }

        protected override string SqlInsert
        {
            get
            {
                return "INSERT INTO dbo.Repartidor (Nombre, Apellido, Dni, Empresa, Telefono, Activo) " +
                       "VALUES (@Nombre, @Apellido, @Dni, @Empresa, @Telefono, @Activo)";
            }
        }

        protected override string SqlUpdate
        {
            get
            {
                return "UPDATE dbo.Repartidor SET Nombre = @Nombre, Apellido = @Apellido, " +
                       "       Dni = @Dni, Empresa = @Empresa, Telefono = @Telefono, Activo = @Activo " +
                       "WHERE IdRepartidor = @Id";
            }
        }

        /// <summary>Baja lógica: el repartidor queda inactivo pero la fila permanece.</summary>
        public override void Eliminar(int id)
        {
            using (SqlConnection conexion = Conexion.Crear())
            using (SqlCommand comando = new SqlCommand(
                "UPDATE dbo.Repartidor SET Activo = 0 WHERE IdRepartidor = @Id", conexion))
            {
                comando.Parameters.AddWithValue("@Id", id);
                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        public bool ExisteDni(string dni, int idExcluido)
        {
            using (SqlConnection conexion = Conexion.Crear())
            using (SqlCommand comando = new SqlCommand(
                "SELECT COUNT(1) FROM dbo.Repartidor WHERE Dni = @Dni AND IdRepartidor <> @IdExcluido",
                conexion))
            {
                comando.Parameters.AddWithValue("@Dni", dni);
                comando.Parameters.AddWithValue("@IdExcluido", idExcluido);
                conexion.Open();
                return Convert.ToInt32(comando.ExecuteScalar()) > 0;
            }
        }

        protected override Repartidor Mapear(SqlDataReader lector)
        {
            Repartidor repartidor = new Repartidor();
            repartidor.Id = Convert.ToInt32(lector["IdRepartidor"]);
            repartidor.Nombre = lector["Nombre"].ToString();
            repartidor.Apellido = lector["Apellido"].ToString();
            repartidor.Dni = lector["Dni"].ToString();
            repartidor.Empresa = lector["Empresa"] == DBNull.Value ? null : lector["Empresa"].ToString();
            repartidor.Telefono = lector["Telefono"] == DBNull.Value ? null : lector["Telefono"].ToString();
            repartidor.Activo = Convert.ToBoolean(lector["Activo"]);
            return repartidor;
        }

        protected override void CargarParametros(SqlCommand comando, Repartidor entidad)
        {
            comando.Parameters.AddWithValue("@Nombre", entidad.Nombre);
            comando.Parameters.AddWithValue("@Apellido", entidad.Apellido);
            comando.Parameters.AddWithValue("@Dni", entidad.Dni);
            comando.Parameters.AddWithValue("@Empresa",
                entidad.Empresa != null ? (object)entidad.Empresa : DBNull.Value);
            comando.Parameters.AddWithValue("@Telefono",
                entidad.Telefono != null ? (object)entidad.Telefono : DBNull.Value);
            comando.Parameters.AddWithValue("@Activo", entidad.Activo);
        }
    }
}
