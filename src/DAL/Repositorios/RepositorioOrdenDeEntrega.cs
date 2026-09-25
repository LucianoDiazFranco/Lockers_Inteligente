using System;
using System.Collections.Generic;
using LockersInteligentes.Dominio.Entidades;
using LockersInteligentes.Dominio.Enums;
using Microsoft.Data.SqlClient;

namespace LockersInteligentes.DAL.Repositorios
{
    public class RepositorioOrdenDeEntrega : RepositorioBase<OrdenDeEntrega>
    {
        protected override string NombreTabla { get { return "dbo.OrdenDeEntrega"; } }
        protected override string ColumnaId { get { return "IdOrden"; } }
        protected override string FiltroPorId { get { return "o.IdOrden = @Id"; } }

        protected override string SqlSelect
        {
            get
            {
                return "SELECT o.IdOrden, o.Descripcion, o.Estado, o.TamanioPaquete, " +
                       "       o.FechaReserva, o.FechaEntrega, o.FechaRetiro, " +
                       "       l.IdLocker, l.Numero AS LockerNumero, l.GrupLocker, " +
                       "       l.Tamanio AS LockerTamanio, l.Estado AS LockerEstado, " +
                       "       res.IdResidente, res.Nombre AS ResNombre, res.Apellido AS ResApellido, " +
                       "       res.Correo AS ResCorreo, res.Piso AS ResPiso, res.Dni AS ResDni, " +
                       "       rep.IdRepartidor, rep.Nombre AS RepNombre, rep.Apellido AS RepApellido, " +
                       "       rep.Empresa AS RepEmpresa, rep.Dni AS RepDni, " +
                       "       e.IdEdificio, e.Nombre AS EdificioNombre " +
                       "FROM dbo.OrdenDeEntrega o " +
                       "INNER JOIN dbo.Locker    l   ON l.IdLocker    = o.IdLocker " +
                       "INNER JOIN dbo.Edificio  e   ON e.IdEdificio  = l.IdEdificio " +
                       "INNER JOIN dbo.Residente res ON res.IdResidente = o.IdResidente " +
                       "LEFT  JOIN dbo.Repartidor rep ON rep.IdRepartidor = o.IdRepartidor";
            }
        }

        protected override string SqlInsert
        {
            get
            {
                return "INSERT INTO dbo.OrdenDeEntrega (Descripcion, Estado, TamanioPaquete, " +
                       "        FechaReserva, FechaEntrega, FechaRetiro, IdLocker, IdResidente, IdRepartidor) " +
                       "VALUES (@Descripcion, @Estado, @TamanioPaquete, @FechaReserva, " +
                       "        @FechaEntrega, @FechaRetiro, @IdLocker, @IdResidente, @IdRepartidor)";
            }
        }

        protected override string SqlUpdate
        {
            get
            {
                return "UPDATE dbo.OrdenDeEntrega SET Descripcion = @Descripcion, Estado = @Estado, " +
                       "       TamanioPaquete = @TamanioPaquete, FechaReserva = @FechaReserva, " +
                       "       FechaEntrega = @FechaEntrega, FechaRetiro = @FechaRetiro, " +
                       "       IdLocker = @IdLocker, IdResidente = @IdResidente, " +
                       "       IdRepartidor = @IdRepartidor " +
                       "WHERE IdOrden = @Id";
            }
        }

        public IList<OrdenDeEntrega> ObtenerPorEstado(EstadoOrden estado)
        {
            List<OrdenDeEntrega> lista = new List<OrdenDeEntrega>();

            using (SqlConnection conexion = Conexion.Crear())
            using (SqlCommand comando = new SqlCommand(
                SqlSelect + " WHERE o.Estado = @Estado ORDER BY o.FechaReserva DESC", conexion))
            {
                comando.Parameters.AddWithValue("@Estado", (byte)estado);
                conexion.Open();

                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                        lista.Add(Mapear(lector));
                }
            }

            return lista;
        }

        protected override OrdenDeEntrega Mapear(SqlDataReader lector)
        {
            OrdenDeEntrega orden = new OrdenDeEntrega();
            orden.Id = Convert.ToInt32(lector["IdOrden"]);
            orden.Descripcion = lector["Descripcion"] == DBNull.Value
                ? null : lector["Descripcion"].ToString();
            orden.Estado = (EstadoOrden)Convert.ToInt32(lector["Estado"]);
            orden.TamanioPaquete = (TamanioPaquete)Convert.ToInt32(lector["TamanioPaquete"]);
            orden.FechaReserva = Convert.ToDateTime(lector["FechaReserva"]);

            orden.FechaEntrega = lector["FechaEntrega"] == DBNull.Value
                ? (DateTime?)null : Convert.ToDateTime(lector["FechaEntrega"]);
            orden.FechaRetiro = lector["FechaRetiro"] == DBNull.Value
                ? (DateTime?)null : Convert.ToDateTime(lector["FechaRetiro"]);

            Edificio edificio = new Edificio();
            edificio.Id = Convert.ToInt32(lector["IdEdificio"]);
            edificio.Nombre = lector["EdificioNombre"].ToString();

            orden.LockerAsignado = new Locker();
            orden.LockerAsignado.Id = Convert.ToInt32(lector["IdLocker"]);
            orden.LockerAsignado.Numero = Convert.ToInt32(lector["LockerNumero"]);
            orden.LockerAsignado.GrupoLocker = lector["GrupLocker"] == DBNull.Value
                ? null : lector["GrupLocker"].ToString();
            orden.LockerAsignado.Tamanio = (TamanioPaquete)Convert.ToInt32(lector["LockerTamanio"]);
            orden.LockerAsignado.Estado = (EstadoLocker)Convert.ToInt32(lector["LockerEstado"]);
            orden.LockerAsignado.Edificio = edificio;

            orden.ResidenteAsignado = new Residente();
            orden.ResidenteAsignado.Id = Convert.ToInt32(lector["IdResidente"]);
            orden.ResidenteAsignado.Nombre = lector["ResNombre"].ToString();
            orden.ResidenteAsignado.Apellido = lector["ResApellido"].ToString();
            orden.ResidenteAsignado.Dni = lector["ResDni"].ToString();
            orden.ResidenteAsignado.Correo = lector["ResCorreo"].ToString();
            orden.ResidenteAsignado.Piso = lector["ResPiso"] == DBNull.Value
                ? null : lector["ResPiso"].ToString();
            orden.ResidenteAsignado.Edificio = edificio;

            if (lector["IdRepartidor"] != DBNull.Value)
            {
                orden.RepartidorAsignado = new Repartidor();
                orden.RepartidorAsignado.Id = Convert.ToInt32(lector["IdRepartidor"]);
                orden.RepartidorAsignado.Nombre = lector["RepNombre"].ToString();
                orden.RepartidorAsignado.Apellido = lector["RepApellido"].ToString();
                orden.RepartidorAsignado.Dni = lector["RepDni"].ToString();
                orden.RepartidorAsignado.Empresa = lector["RepEmpresa"] == DBNull.Value
                    ? null : lector["RepEmpresa"].ToString();
            }

            return orden;
        }

        protected override void CargarParametros(SqlCommand comando, OrdenDeEntrega entidad)
        {
            comando.Parameters.AddWithValue("@Descripcion",
                entidad.Descripcion != null ? (object)entidad.Descripcion : DBNull.Value);
            comando.Parameters.AddWithValue("@Estado", (byte)entidad.Estado);
            comando.Parameters.AddWithValue("@TamanioPaquete", (byte)entidad.TamanioPaquete);
            comando.Parameters.AddWithValue("@FechaReserva", entidad.FechaReserva);

            comando.Parameters.AddWithValue("@FechaEntrega",
                entidad.FechaEntrega.HasValue ? (object)entidad.FechaEntrega.Value : DBNull.Value);
            comando.Parameters.AddWithValue("@FechaRetiro",
                entidad.FechaRetiro.HasValue ? (object)entidad.FechaRetiro.Value : DBNull.Value);

            comando.Parameters.AddWithValue("@IdLocker", entidad.LockerAsignado.Id);
            comando.Parameters.AddWithValue("@IdResidente", entidad.ResidenteAsignado.Id);
            comando.Parameters.AddWithValue("@IdRepartidor",
                entidad.RepartidorAsignado != null ? (object)entidad.RepartidorAsignado.Id : DBNull.Value);
        }
        public IDictionary<int, OrdenDeEntrega> ObtenerActivasPorEdificio(int idEdificio)
        {
            Dictionary<int, OrdenDeEntrega> mapa = new Dictionary<int, OrdenDeEntrega>();

            using (SqlConnection conexion = Conexion.Crear())
            using (SqlCommand comando = new SqlCommand(
                SqlSelect + " WHERE e.IdEdificio = @IdEdificio AND o.Estado IN (1, 2)", conexion))
            {
                comando.Parameters.AddWithValue("@IdEdificio", idEdificio);
                conexion.Open();

                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        OrdenDeEntrega orden = Mapear(lector);
                        mapa[orden.LockerAsignado.Id] = orden;
                    }
                }
            }

            return mapa;
        }
        public int ContarActivasPorResidente(int idResidente)
        {
            using (SqlConnection conexion = Conexion.Crear())
            using (SqlCommand comando = new SqlCommand(
                "SELECT COUNT(1) FROM dbo.OrdenDeEntrega " +
                "WHERE IdResidente = @IdResidente AND Estado IN (1, 2)", conexion))
            {
                comando.Parameters.AddWithValue("@IdResidente", idResidente);
                conexion.Open();
                return Convert.ToInt32(comando.ExecuteScalar());
            }
        }
    }
}