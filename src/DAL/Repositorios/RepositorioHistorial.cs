using LockersInteligentes.Dominio.Entidades;
using LockersInteligentes.Dominio.Enums;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockersInteligentes.DAL.Repositorios
{
    public class RepositorioHistorial : RepositorioBase<Historial>
    {
        protected override string NombreTabla { get { return "dbo.Historial"; } }
        protected override string ColumnaId { get { return "NumHistorial"; } }
        protected override string FiltroPorId { get { return "h.NumHistorial = @Id"; } }

        protected override string SqlSelect
        {
            get
            {
                return "SELECT h.NumHistorial, h.Actor, h.TipoDeOperacion, h.FechaOperacion, " +
                       "       h.Detalle, " +
                       "       l.IdLocker, l.Numero AS LockerNumero, l.GrupLocker, " +
                       "       o.IdOrden " +
                       "FROM dbo.Historial h " +
                       "LEFT JOIN dbo.Locker l ON l.IdLocker = h.IdLocker " +
                       "LEFT JOIN dbo.OrdenDeEntrega o ON o.IdOrden = h.IdOrden";
            }
        }

        protected override string SqlInsert
        {
            get
            {
                return "INSERT INTO dbo.Historial (Actor, TipoDeOperacion, FechaOperacion, " +
                       "        IdLocker, IdOrden, Detalle) " +
                       "VALUES (@Actor, @TipoDeOperacion, @FechaOperacion, " +
                       "        @IdLocker, @IdOrden, @Detalle)";
            }
        }

        /// <summary>
        /// La bitácora no se modifica. Un registro de auditoría que se puede editar
        /// no sirve como auditoría, así que el método existe solo para cumplir el
        /// contrato de la clase base y corta si alguien lo llama.
        /// </summary>
        protected override string SqlUpdate
        {
            get { throw new InvalidOperationException("La bitácora no admite modificaciones."); }
        }

        public override void Eliminar(int id)
        {
            throw new InvalidOperationException("La bitácora no admite eliminaciones.");
        }

        /// <summary>
        /// Consulta filtrada para CU.Inf.002. Todos los filtros son opcionales:
        /// se arma el WHERE según los que vengan con valor.
        /// </summary>
        public IList<Historial> Buscar(DateTime? desde, DateTime? hasta,
                                       string actor, TipoOperacion? tipo)
        {
            List<Historial> lista = new List<Historial>();
            List<string> condiciones = new List<string>();

            if (desde.HasValue)
                condiciones.Add("h.FechaOperacion >= @Desde");

            if (hasta.HasValue)
                condiciones.Add("h.FechaOperacion <= @Hasta");

            if (!string.IsNullOrWhiteSpace(actor))
                condiciones.Add("h.Actor LIKE @Actor");

            if (tipo.HasValue)
                condiciones.Add("h.TipoDeOperacion = @Tipo");

            string sql = SqlSelect;

            if (condiciones.Count > 0)
                sql += " WHERE " + string.Join(" AND ", condiciones);

            sql += " ORDER BY h.FechaOperacion DESC";

            using (SqlConnection conexion = Conexion.Crear())
            using (SqlCommand comando = new SqlCommand(sql, conexion))
            {
                if (desde.HasValue)
                    comando.Parameters.AddWithValue("@Desde", desde.Value);

                if (hasta.HasValue)
                    comando.Parameters.AddWithValue("@Hasta", hasta.Value);

                if (!string.IsNullOrWhiteSpace(actor))
                    comando.Parameters.AddWithValue("@Actor", "%" + actor.Trim() + "%");

                if (tipo.HasValue)
                    comando.Parameters.AddWithValue("@Tipo", (byte)tipo.Value);

                conexion.Open();

                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                        lista.Add(Mapear(lector));
                }
            }

            return lista;
        }

        protected override Historial Mapear(SqlDataReader lector)
        {
            Historial historial = new Historial();
            historial.Id = Convert.ToInt32(lector["NumHistorial"]);
            historial.Actor = lector["Actor"].ToString();
            historial.TipoDeOperacion = (TipoOperacion)Convert.ToInt32(lector["TipoDeOperacion"]);
            historial.FechaOperacion = Convert.ToDateTime(lector["FechaOperacion"]);
            historial.Detalle = lector["Detalle"] == DBNull.Value ? null : lector["Detalle"].ToString();

            if (lector["IdLocker"] != DBNull.Value)
            {
                historial.LockerUtilizado = new Locker();
                historial.LockerUtilizado.Id = Convert.ToInt32(lector["IdLocker"]);
                historial.LockerUtilizado.Numero = Convert.ToInt32(lector["LockerNumero"]);
                historial.LockerUtilizado.GrupoLocker = lector["GrupLocker"] == DBNull.Value
                    ? null : lector["GrupLocker"].ToString();
            }

            if (lector["IdOrden"] != DBNull.Value)
            {
                historial.OrdenDeEntrega = new OrdenDeEntrega();
                historial.OrdenDeEntrega.Id = Convert.ToInt32(lector["IdOrden"]);
            }

            return historial;
        }

        protected override void CargarParametros(SqlCommand comando, Historial entidad)
        {
            comando.Parameters.AddWithValue("@Actor", entidad.Actor);
            comando.Parameters.AddWithValue("@TipoDeOperacion", (byte)entidad.TipoDeOperacion);
            comando.Parameters.AddWithValue("@FechaOperacion", entidad.FechaOperacion);
            comando.Parameters.AddWithValue("@Detalle",
                entidad.Detalle != null ? (object)entidad.Detalle : DBNull.Value);

            comando.Parameters.AddWithValue("@IdLocker",
                entidad.LockerUtilizado != null ? (object)entidad.LockerUtilizado.Id : DBNull.Value);
            comando.Parameters.AddWithValue("@IdOrden",
                entidad.OrdenDeEntrega != null ? (object)entidad.OrdenDeEntrega.Id : DBNull.Value);
        }
    }
}
