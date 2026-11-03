using LockersInteligentes.Dominio.Entidades;
using LockersInteligentes.ArqBase.DataAccess;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockersInteligentes.DAL.Repositorios
{
    public class RepositorioReporte : RepositorioBase<Reporte>
    {
        protected override string NombreTabla { get { return "dbo.Reporte"; } }
        protected override string ColumnaId { get { return "NumReporte"; } }
        protected override string FiltroPorId { get { return "r.NumReporte = @Id"; } }

        protected override string SqlSelect
        {
            get
            {
                return "SELECT r.NumReporte, r.FechaGeneracion, r.Periodo, " +
                       "       r.PorcentajeOcupacionLocker, r.PorcentajePertenenciaLocker, " +
                       "       r.PaqueteVencido, r.RankingRepartidores, r.RankingTopLockers, " +
                       "       e.IdEdificio, e.Nombre AS EdificioNombre " +
                       "FROM dbo.Reporte r " +
                       "INNER JOIN dbo.Edificio e ON e.IdEdificio = r.IdEdificio";
            }
        }

        protected override string SqlInsert
        {
            get
            {
                return "INSERT INTO dbo.Reporte (IdEdificio, FechaGeneracion, Periodo, " +
                       "        PorcentajeOcupacionLocker, PorcentajePertenenciaLocker, " +
                       "        PaqueteVencido, RankingRepartidores, RankingTopLockers) " +
                       "VALUES (@IdEdificio, @FechaGeneracion, @Periodo, " +
                       "        @PorcentajeOcupacion, @PorcentajePertenencia, " +
                       "        @PaqueteVencido, @RankingRepartidores, @RankingTopLockers)";
            }
        }

        protected override string SqlUpdate
        {
            get { throw new InvalidOperationException("Un reporte generado no se modifica."); }
        }

        protected override Reporte Mapear(SqlDataReader lector)
        {
            Reporte reporte = new Reporte();
            reporte.Id = Convert.ToInt32(lector["NumReporte"]);
            reporte.FechaGeneracion = Convert.ToDateTime(lector["FechaGeneracion"]);
            reporte.Periodo = lector["Periodo"].ToString();
            reporte.PorcentajeOcupacionLocker = Convert.ToDecimal(lector["PorcentajeOcupacionLocker"]);
            reporte.PorcentajePertenenciaLocker = Convert.ToDecimal(lector["PorcentajePertenenciaLocker"]);
            reporte.PaqueteVencido = Convert.ToInt32(lector["PaqueteVencido"]);
            reporte.RankingRepartidores = lector["RankingRepartidores"] == DBNull.Value
                ? null : lector["RankingRepartidores"].ToString();
            reporte.RankingTopLockers = lector["RankingTopLockers"] == DBNull.Value
                ? null : lector["RankingTopLockers"].ToString();

            reporte.Edificio = new Edificio();
            reporte.Edificio.Id = Convert.ToInt32(lector["IdEdificio"]);
            reporte.Edificio.Nombre = lector["EdificioNombre"].ToString();

            return reporte;
        }

        protected override void CargarParametros(SqlCommand comando, Reporte entidad)
        {
            comando.Parameters.AddWithValue("@IdEdificio", entidad.Edificio.Id);
            comando.Parameters.AddWithValue("@FechaGeneracion", entidad.FechaGeneracion);
            comando.Parameters.AddWithValue("@Periodo", entidad.Periodo);
            comando.Parameters.AddWithValue("@PorcentajeOcupacion", entidad.PorcentajeOcupacionLocker);
            comando.Parameters.AddWithValue("@PorcentajePertenencia", entidad.PorcentajePertenenciaLocker);
            comando.Parameters.AddWithValue("@PaqueteVencido", entidad.PaqueteVencido);
            comando.Parameters.AddWithValue("@RankingRepartidores",
                entidad.RankingRepartidores != null ? (object)entidad.RankingRepartidores : DBNull.Value);
            comando.Parameters.AddWithValue("@RankingTopLockers",
                entidad.RankingTopLockers != null ? (object)entidad.RankingTopLockers : DBNull.Value);
        }

    }
}
