using System;
using System.Configuration;
using Microsoft.Data.SqlClient;

namespace LockersInteligentes.DAL
{
    public static class Conexion
    {
        private const string NombreCadena = "LockersDb";
        private static string _cadena;

        public static string Cadena
        {
            get
            {
                if (_cadena == null)
                {
                    ConnectionStringSettings config = ConfigurationManager.ConnectionStrings[NombreCadena];

                    if (config == null || string.IsNullOrWhiteSpace(config.ConnectionString))
                        throw new ConfigurationErrorsException(
                            "No se encontro la cadena de conexion '" + NombreCadena + "'. " +
                            "Verifica que exista en el App.config del proyecto UI y que la UI " +
                            "este marcada como proyecto de inicio.");

                    _cadena = config.ConnectionString;
                }

                return _cadena;
            }
        }
        public static SqlConnection Crear()
        {
            return new SqlConnection(Cadena);
        }
        public static bool Probar(out string mensajeError)
        {
            mensajeError = null;

            try
            {
                using (SqlConnection conexion = Crear())
                {
                    conexion.Open();
                }
                return true;
            }
            catch (Exception ex)
            {
                mensajeError = ex.Message;
                return false;
            }
        }
    }
}