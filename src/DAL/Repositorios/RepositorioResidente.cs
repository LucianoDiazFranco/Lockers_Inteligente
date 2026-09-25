using System;
using System.Collections.Generic;
using LockersInteligentes.Dominio.Entidades;
using Microsoft.Data.SqlClient;

namespace LockersInteligentes.DAL.Repositorios
{
    public class RepositorioResidente : RepositorioBase<Residente>
    {
        protected override string NombreTabla { get { return "dbo.Residente"; } }
        protected override string ColumnaId { get { return "IdResidente"; } }
        protected override string FiltroPorId { get { return "r.IdResidente = @Id"; } }

        protected override string SqlSelect
        {
            get
            {
                return "SELECT r.IdResidente, r.Nombre, r.Apellido, r.Dni, r.Correo, " +
                       "       r.Piso, r.Telefono, r.Activo, " +
                       "       e.IdEdificio, e.Nombre AS EdificioNombre, e.Direccion, " +
                       "       e.Localidad, e.CantLockers " +
                       "FROM dbo.Residente r " +
                       "INNER JOIN dbo.Edificio e ON e.IdEdificio = r.IdEdificio";
            }
        }

        protected override string SqlInsert
        {
            get
            {
                return "INSERT INTO dbo.Residente (IdEdificio, Nombre, Apellido, Dni, Correo, Piso, Telefono, Activo) " +
                       "VALUES (@IdEdificio, @Nombre, @Apellido, @Dni, @Correo, @Piso, @Telefono, @Activo)";
            }
        }

        protected override string SqlUpdate
        {
            get
            {
                return "UPDATE dbo.Residente SET IdEdificio = @IdEdificio, Nombre = @Nombre, " +
                       "       Apellido = @Apellido, Dni = @Dni, Correo = @Correo, " +
                       "       Piso = @Piso, Telefono = @Telefono, Activo = @Activo " +
                       "WHERE IdResidente = @Id";
            }
        }

        public IList<Residente> ObtenerPorEdificio(int idEdificio)
        {
            List<Residente> lista = new List<Residente>();

            using (SqlConnection conexion = Conexion.Crear())
            using (SqlCommand comando = new SqlCommand(
                SqlSelect + " WHERE r.IdEdificio = @IdEdificio ORDER BY r.Apellido, r.Nombre", conexion))
            {
                comando.Parameters.AddWithValue("@IdEdificio", idEdificio);
                conexion.Open();

                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                        lista.Add(Mapear(lector));
                }
            }

            return lista;
        }

        protected override Residente Mapear(SqlDataReader lector)
        {
            Residente residente = new Residente();
            residente.Id = Convert.ToInt32(lector["IdResidente"]);
            residente.Nombre = lector["Nombre"].ToString();
            residente.Apellido = lector["Apellido"].ToString();
            residente.Dni = lector["Dni"].ToString();
            residente.Correo = lector["Correo"].ToString();
            residente.Piso = lector["Piso"] == DBNull.Value ? null : lector["Piso"].ToString();
            residente.Telefono = lector["Telefono"] == DBNull.Value ? null : lector["Telefono"].ToString();
            residente.Activo = Convert.ToBoolean(lector["Activo"]);

            residente.Edificio = new Edificio();
            residente.Edificio.Id = Convert.ToInt32(lector["IdEdificio"]);
            residente.Edificio.Nombre = lector["EdificioNombre"].ToString();
            residente.Edificio.Direccion = lector["Direccion"].ToString();
            residente.Edificio.Localidad = lector["Localidad"].ToString();
            residente.Edificio.CantLockers = Convert.ToInt32(lector["CantLockers"]);

            return residente;
        }

        protected override void CargarParametros(SqlCommand comando, Residente entidad)
        {
            comando.Parameters.AddWithValue("@IdEdificio", entidad.Edificio.Id);
            comando.Parameters.AddWithValue("@Nombre", entidad.Nombre);
            comando.Parameters.AddWithValue("@Apellido", entidad.Apellido);
            comando.Parameters.AddWithValue("@Dni", entidad.Dni);
            comando.Parameters.AddWithValue("@Correo", entidad.Correo);
            comando.Parameters.AddWithValue("@Piso",
            entidad.Piso != null ? (object)entidad.Piso : DBNull.Value);
            comando.Parameters.AddWithValue("@Telefono",
            entidad.Telefono != null ? (object)entidad.Telefono : DBNull.Value);
            comando.Parameters.AddWithValue("@Activo", entidad.Activo);
        }
        public override void Eliminar(int id)
        {
            using (SqlConnection conexion = Conexion.Crear())
            using (SqlCommand comando = new SqlCommand(
                "UPDATE dbo.Residente SET Activo = 0 WHERE IdResidente = @Id", conexion))
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
                "SELECT COUNT(1) FROM dbo.Residente WHERE Dni = @Dni AND IdResidente <> @IdExcluido",
                conexion))
            {
                comando.Parameters.AddWithValue("@Dni", dni);
                comando.Parameters.AddWithValue("@IdExcluido", idExcluido);
                conexion.Open();
                return Convert.ToInt32(comando.ExecuteScalar()) > 0;
            }
        }
    }
}