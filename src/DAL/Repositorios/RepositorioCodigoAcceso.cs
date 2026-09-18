using System;
using LockersInteligentes.Dominio.Entidades;
using LockersInteligentes.Dominio.Enums;
using Microsoft.Data.SqlClient;

namespace LockersInteligentes.DAL.Repositorios
{
    public class RepositorioCodigoAcceso : RepositorioBase<CodigoAcceso>
    {
        protected override string NombreTabla { get { return "dbo.CodigoAcceso"; } }
        protected override string ColumnaId { get { return "IdCodigo"; } }
        protected override string FiltroPorId { get { return "IdCodigo = @Id"; } }

        protected override string SqlSelect
        {
            get
            {
                return "SELECT IdCodigo, IdOrden, TipoCodigo, PinHash, PinSalt, " +
                       "       FechaGeneracion, Usado " +
                       "FROM dbo.CodigoAcceso";
            }
        }

        protected override string SqlInsert
        {
            get
            {
                return "INSERT INTO dbo.CodigoAcceso (IdOrden, TipoCodigo, PinHash, PinSalt, " +
                       "        FechaGeneracion, Usado) " +
                       "VALUES (@IdOrden, @TipoCodigo, @PinHash, @PinSalt, @FechaGeneracion, @Usado)";
            }
        }

        protected override string SqlUpdate
        {
            get
            {
                return "UPDATE dbo.CodigoAcceso SET IdOrden = @IdOrden, TipoCodigo = @TipoCodigo, " +
                       "       PinHash = @PinHash, PinSalt = @PinSalt, " +
                       "       FechaGeneracion = @FechaGeneracion, Usado = @Usado " +
                       "WHERE IdCodigo = @Id";
            }
        }
        public CodigoAcceso ObtenerVigente(int idOrden, TipoCodigo tipo)
        {
            using (SqlConnection conexion = Conexion.Crear())
            using (SqlCommand comando = new SqlCommand(
                SqlSelect + " WHERE IdOrden = @IdOrden AND TipoCodigo = @Tipo AND Usado = 0 " +
                            " ORDER BY FechaGeneracion DESC", conexion))
            {
                comando.Parameters.AddWithValue("@IdOrden", idOrden);
                comando.Parameters.AddWithValue("@Tipo", (byte)tipo);
                conexion.Open();

                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    if (lector.Read())
                        return Mapear(lector);
                }
            }

            return null;
        }
        protected override CodigoAcceso Mapear(SqlDataReader lector)
        {
            CodigoAcceso codigo = new CodigoAcceso();
            codigo.Id = Convert.ToInt32(lector["IdCodigo"]);
            codigo.TipoCodigo = (TipoCodigo)Convert.ToInt32(lector["TipoCodigo"]);
            codigo.PinHash = lector["PinHash"].ToString();
            codigo.PinSalt = lector["PinSalt"].ToString();
            codigo.FechaGeneracion = Convert.ToDateTime(lector["FechaGeneracion"]);
            codigo.Usado = Convert.ToBoolean(lector["Usado"]);
            codigo.OrdenDeEntrega = new OrdenDeEntrega();
            codigo.OrdenDeEntrega.Id = Convert.ToInt32(lector["IdOrden"]);

            return codigo;
        }

        protected override void CargarParametros(SqlCommand comando, CodigoAcceso entidad)
        {
            comando.Parameters.AddWithValue("@IdOrden", entidad.OrdenDeEntrega.Id);
            comando.Parameters.AddWithValue("@TipoCodigo", (byte)entidad.TipoCodigo);
            comando.Parameters.AddWithValue("@PinHash", entidad.PinHash);
            comando.Parameters.AddWithValue("@PinSalt", entidad.PinSalt);
            comando.Parameters.AddWithValue("@FechaGeneracion", entidad.FechaGeneracion);
            comando.Parameters.AddWithValue("@Usado", entidad.Usado);
        }
    }
}