using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;


namespace LockersInteligentes.ArqBase.DataAccess
{
    public class RepositorioTraduccion{
    
        public IDictionary<string, string> ObtenerDiccionario(int idIdioma)
        {
            Dictionary<string, string> diccionario =
                new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            using (SqlConnection conexion = Conexion.Crear())
            using (SqlCommand comando = new SqlCommand(
                "SELECT Clave, Texto FROM dbo.Traduccion WHERE IdIdioma = @IdIdioma", conexion))
            {
                comando.Parameters.AddWithValue("@IdIdioma", idIdioma);
                conexion.Open();

                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                        diccionario[lector["Clave"].ToString()] = lector["Texto"].ToString();
                }
            }

            return diccionario;
        }
    }
}
