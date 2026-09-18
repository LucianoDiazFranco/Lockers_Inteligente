using System;
using LockersInteligentes.Dominio.Entidades;
using Microsoft.Data.SqlClient;

namespace LockersInteligentes.DAL.Repositorios
{
    public class RepositorioEdificio : RepositorioBase<Edificio>
    {
        protected override string NombreTabla { get { return "dbo.Edificio"; } }
        protected override string ColumnaId { get { return "IdEdificio"; } }
        protected override string FiltroPorId { get { return "IdEdificio = @Id"; } }

        protected override string SqlSelect
        {
            get
            {
                return "SELECT IdEdificio, Nombre, Direccion, Localidad, " +
                       "       TelefonoContacto, CantLockers " +
                       "FROM dbo.Edificio";
            }
        }

        protected override string SqlInsert
        {
            get
            {
                return "INSERT INTO dbo.Edificio (Nombre, Direccion, Localidad, TelefonoContacto, CantLockers) " +
                       "VALUES (@Nombre, @Direccion, @Localidad, @TelefonoContacto, @CantLockers)";
            }
        }

        protected override string SqlUpdate
        {
            get
            {
                return "UPDATE dbo.Edificio SET Nombre = @Nombre, Direccion = @Direccion, " +
                       "       Localidad = @Localidad, TelefonoContacto = @TelefonoContacto, " +
                       "       CantLockers = @CantLockers " +
                       "WHERE IdEdificio = @Id";
            }
        }

        protected override Edificio Mapear(SqlDataReader lector)
        {
            Edificio edificio = new Edificio();
            edificio.Id = Convert.ToInt32(lector["IdEdificio"]);
            edificio.Nombre = lector["Nombre"].ToString();
            edificio.Direccion = lector["Direccion"].ToString();
            edificio.Localidad = lector["Localidad"].ToString();
            edificio.TelefonoContacto = lector["TelefonoContacto"] == DBNull.Value
                ? null : lector["TelefonoContacto"].ToString();
            edificio.CantLockers = Convert.ToInt32(lector["CantLockers"]);
            return edificio;
        }

        protected override void CargarParametros(SqlCommand comando, Edificio entidad)
        {
            comando.Parameters.AddWithValue("@Nombre", entidad.Nombre);
            comando.Parameters.AddWithValue("@Direccion", entidad.Direccion);
            comando.Parameters.AddWithValue("@Localidad", entidad.Localidad);
            comando.Parameters.AddWithValue("@TelefonoContacto",
                entidad.TelefonoContacto != null ? (object)entidad.TelefonoContacto : DBNull.Value);
            comando.Parameters.AddWithValue("@CantLockers", entidad.CantLockers);
        }
    }
}