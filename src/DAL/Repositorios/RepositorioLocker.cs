using System;
using System.Collections.Generic;
using LockersInteligentes.Dominio.Entidades;
using LockersInteligentes.Dominio.Enums;
using Microsoft.Data.SqlClient;

namespace LockersInteligentes.DAL.Repositorios
{
    public class RepositorioLocker : RepositorioBase<Locker>
    {
        protected override string NombreTabla { get { return "dbo.Locker"; } }
        protected override string ColumnaId { get { return "IdLocker"; } }
        protected override string FiltroPorId { get { return "l.IdLocker = @Id"; } }

        protected override string SqlSelect
        {
            get
            {
                return "SELECT l.IdLocker, l.Numero, l.GrupLocker, l.Descripcion, " +
                       "       l.Tamanio, l.Estado, " +
                       "       e.IdEdificio, e.Nombre AS EdificioNombre, e.Direccion, " +
                       "       e.Localidad, e.TelefonoContacto, e.CantLockers " +
                       "FROM dbo.Locker l " +
                       "INNER JOIN dbo.Edificio e ON e.IdEdificio = l.IdEdificio";
            }
        }

        protected override string SqlInsert
        {
            get
            {
                return "INSERT INTO dbo.Locker (IdEdificio, Numero, GrupLocker, Descripcion, Tamanio, Estado) " +
                       "VALUES (@IdEdificio, @Numero, @GrupLocker, @Descripcion, @Tamanio, @Estado)";
            }
        }

        protected override string SqlUpdate
        {
            get
            {
                return "UPDATE dbo.Locker SET IdEdificio = @IdEdificio, Numero = @Numero, " +
                       "       GrupLocker = @GrupLocker, Descripcion = @Descripcion, " +
                       "       Tamanio = @Tamanio, Estado = @Estado " +
                       "WHERE IdLocker = @Id";
            }
        }

        public Locker ObtenerLibreParaTamanio(int idEdificio, TamanioPaquete tamanio)
        {
            using (SqlConnection conexion = Conexion.Crear())
            using (SqlCommand comando = new SqlCommand(
                SqlSelect +
                " WHERE l.IdEdificio = @IdEdificio AND l.Estado = 1 AND l.Tamanio >= @Tamanio " +
                " ORDER BY l.Tamanio ASC, l.Numero ASC", conexion))
            {
                comando.Parameters.AddWithValue("@IdEdificio", idEdificio);
                comando.Parameters.AddWithValue("@Tamanio", (byte)tamanio);
                conexion.Open();

                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    if (lector.Read())
                        return Mapear(lector);
                }
            }

            return null;
        }

        public IList<Locker> ObtenerPorEdificio(int idEdificio)
        {
            List<Locker> lista = new List<Locker>();

            using (SqlConnection conexion = Conexion.Crear())
            using (SqlCommand comando = new SqlCommand(
                SqlSelect + " WHERE l.IdEdificio = @IdEdificio ORDER BY l.Numero", conexion))
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

        protected override Locker Mapear(SqlDataReader lector)
        {
            Locker locker = new Locker();
            locker.Id = Convert.ToInt32(lector["IdLocker"]);
            locker.Numero = Convert.ToInt32(lector["Numero"]);
            locker.GrupoLocker = lector["GrupLocker"] == DBNull.Value
                ? null : lector["GrupLocker"].ToString();
            locker.Descripcion = lector["Descripcion"] == DBNull.Value
                ? null : lector["Descripcion"].ToString();
            locker.Tamanio = (TamanioPaquete)Convert.ToInt32(lector["Tamanio"]);
            locker.Estado = (EstadoLocker)Convert.ToInt32(lector["Estado"]);

            locker.Edificio = new Edificio();
            locker.Edificio.Id = Convert.ToInt32(lector["IdEdificio"]);
            locker.Edificio.Nombre = lector["EdificioNombre"].ToString();
            locker.Edificio.Direccion = lector["Direccion"].ToString();
            locker.Edificio.Localidad = lector["Localidad"].ToString();
            locker.Edificio.CantLockers = Convert.ToInt32(lector["CantLockers"]);

            return locker;
        }

        protected override void CargarParametros(SqlCommand comando, Locker entidad)
        {
            comando.Parameters.AddWithValue("@IdEdificio", entidad.Edificio.Id);
            comando.Parameters.AddWithValue("@Numero", entidad.Numero);
            comando.Parameters.AddWithValue("@GrupLocker",
                entidad.GrupoLocker != null ? (object)entidad.GrupoLocker : DBNull.Value);
            comando.Parameters.AddWithValue("@Descripcion",
                entidad.Descripcion != null ? (object)entidad.Descripcion : DBNull.Value);
            comando.Parameters.AddWithValue("@Tamanio", (byte)entidad.Tamanio);
            comando.Parameters.AddWithValue("@Estado", (byte)entidad.Estado);
        }

        public bool TieneOrdenes(int idLocker)
        {
            using (SqlConnection conexion = Conexion.Crear())
            using (SqlCommand comando = new SqlCommand(
                "SELECT COUNT(1) FROM dbo.OrdenDeEntrega WHERE IdLocker = @IdLocker", conexion))
            {
                comando.Parameters.AddWithValue("@IdLocker", idLocker);
                conexion.Open();
                return Convert.ToInt32(comando.ExecuteScalar()) > 0;
            }
        }
    }
}