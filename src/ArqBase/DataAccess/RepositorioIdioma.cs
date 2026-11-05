using LockersInteligentes.ArqBase.Entidades;
using Microsoft.Data.SqlClient;
using System;


namespace LockersInteligentes.ArqBase.DataAccess
{
    public class RepositorioIdioma : RepositorioBase<Idioma>
    {
        protected override string NombreTabla { get { return "dbo.Idioma"; } }
        protected override string ColumnaId { get { return "NumIdioma"; } }
        protected override string FiltroPorId { get { return "NumIdioma = @Id"; } }

        protected override string SqlSelect
        {
            get
            {
                return "SELECT NumIdioma, Nombre, Codigo, EsPredeterminado " +
                       "FROM dbo.Idioma";
            }
        }

        protected override string SqlInsert
        {
            get
            {
                return "INSERT INTO dbo.Idioma (Nombre, Codigo, EsPredeterminado) " +
                       "VALUES (@Nombre, @Codigo, @EsPredeterminado)";
            }
        }

        protected override string SqlUpdate
        {
            get
            {
                return "UPDATE dbo.Idioma SET Nombre = @Nombre, Codigo = @Codigo, " +
                       "       EsPredeterminado = @EsPredeterminado " +
                       "WHERE NumIdioma = @Id";
            }
        }

        /// <summary>Idioma que se carga al iniciar la aplicación.</summary>
        public Idioma ObtenerPredeterminado()
        {
            using (SqlConnection conexion = Conexion.Crear())
            using (SqlCommand comando = new SqlCommand(
                SqlSelect + " WHERE EsPredeterminado = 1", conexion))
            {
                conexion.Open();

                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    if (lector.Read())
                        return Mapear(lector);
                }
            }

            return null;
        }

        public Idioma ObtenerPorCodigo(string codigo)
        {
            using (SqlConnection conexion = Conexion.Crear())
            using (SqlCommand comando = new SqlCommand(
                SqlSelect + " WHERE Codigo = @Codigo", conexion))
            {
                comando.Parameters.AddWithValue("@Codigo", codigo);
                conexion.Open();

                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    if (lector.Read())
                        return Mapear(lector);
                }
            }

            return null;
        }

        protected override Idioma Mapear(SqlDataReader lector)
        {
            Idioma idioma = new Idioma();
            idioma.Id = Convert.ToInt32(lector["NumIdioma"]);
            idioma.Nombre = lector["Nombre"].ToString();
            idioma.Codigo = lector["Codigo"].ToString();
            idioma.EsPredeterminado = Convert.ToBoolean(lector["EsPredeterminado"]);
            return idioma;
        }

        protected override void CargarParametros(SqlCommand comando, Idioma entidad)
        {
            comando.Parameters.AddWithValue("@Nombre", entidad.Nombre);
            comando.Parameters.AddWithValue("@Codigo", entidad.Codigo);
            comando.Parameters.AddWithValue("@EsPredeterminado", entidad.EsPredeterminado);
        }
    }
}
