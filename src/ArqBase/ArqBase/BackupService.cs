using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockersInteligentes.ArqBase.ArqBase
{
    public static class BackupService
    {
        private static readonly string[] OrdenTablas =
        {
            "Idioma",
            "Traduccion",
            "Rol",
            "Usuario",
            "Edificio",
            "Locker",
            "Residente",
            "Repartidor",
            "OrdenDeEntrega",
            "CodigoAcceso",
            "Notificacion",
            "Historial",
            "Reporte"
        };

        /// <summary>
        /// Genera el script completo en la ruta indicada y devuelve la cantidad de
        /// filas exportadas.
        /// </summary>
        public static int Generar(string cadenaConexion, string rutaEsquema, string rutaDestino)
        {
            if (!File.Exists(rutaEsquema))
                throw new FileNotFoundException(
                    "No se encontró el script de esquema en: " + rutaEsquema);

            StringBuilder script = new StringBuilder();
            int totalFilas = 0;

            EscribirEncabezado(script);

            // El esquema se toma del 01_Esquema.sql que ya existe en el proyecto:
            // es la fuente de verdad de la estructura y no tiene sentido duplicarla.
            script.AppendLine("/* ===================== ESQUEMA ===================== */");
            script.AppendLine();
            script.AppendLine(File.ReadAllText(rutaEsquema));
            script.AppendLine();
            script.AppendLine("/* ====================== DATOS ====================== */");
            script.AppendLine();
            script.AppendLine("USE LockersInteligentes;");
            script.AppendLine("GO");
            script.AppendLine();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                foreach (string tabla in OrdenTablas)
                    totalFilas += EscribirTabla(conexion, script, tabla);
            }

            script.AppendLine();
            script.AppendLine("/* Fin del backup. */");

            string carpeta = Path.GetDirectoryName(rutaDestino);

            if (!string.IsNullOrEmpty(carpeta) && !Directory.Exists(carpeta))
                Directory.CreateDirectory(carpeta);

            File.WriteAllText(rutaDestino, script.ToString(), Encoding.UTF8);

            return totalFilas;
        }

        private static void EscribirEncabezado(StringBuilder script)
        {
            script.AppendLine("/* =====================================================================");
            script.AppendLine("   LOCKERS INTELIGENTES - Backup completo");
            script.AppendLine("   Generado: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            script.AppendLine();
            script.AppendLine("   Para restaurar en otra máquina:");
            script.AppendLine("     1. Abrir este archivo en SSMS conectado a la instancia destino.");
            script.AppendLine("     2. Ejecutarlo completo con F5.");
            script.AppendLine();
            script.AppendLine("   ATENCIÓN: contiene las credenciales (hasheadas) de todos los");
            script.AppendLine("   usuarios. No subir a repositorios públicos ni compartir.");
            script.AppendLine("   ===================================================================== */");
            script.AppendLine();
        }

        /// <summary>Exporta una tabla como INSERTs, conservando los Id originales.</summary>
        private static int EscribirTabla(SqlConnection conexion, StringBuilder script, string tabla)
        {
            List<string> columnas = ObtenerColumnas(conexion, tabla);
            bool tieneIdentidad = TieneIdentidad(conexion, tabla);
            int filas = 0;

            StringBuilder cuerpo = new StringBuilder();

            using (SqlCommand comando = new SqlCommand("SELECT * FROM dbo." + tabla, conexion))
            using (SqlDataReader lector = comando.ExecuteReader())
            {
                while (lector.Read())
                {
                    List<string> valores = new List<string>();

                    foreach (string columna in columnas)
                        valores.Add(FormatearValor(lector[columna]));

                    cuerpo.AppendLine("INSERT INTO dbo." + tabla +
                                      " ([" + string.Join("], [", columnas) + "]) VALUES (" +
                                      string.Join(", ", valores) + ");");
                    filas++;
                }
            }

            script.AppendLine("/* ---- " + tabla + " (" + filas + " fila/s) ---- */");

            if (filas == 0)
            {
                script.AppendLine("-- Sin datos.");
                script.AppendLine();
                return 0;
            }

            if (tieneIdentidad)
                script.AppendLine("SET IDENTITY_INSERT dbo." + tabla + " ON;");

            script.Append(cuerpo.ToString());

            if (tieneIdentidad)
                script.AppendLine("SET IDENTITY_INSERT dbo." + tabla + " OFF;");

            script.AppendLine("GO");
            script.AppendLine();

            return filas;
        }

        private static List<string> ObtenerColumnas(SqlConnection conexion, string tabla)
        {
            List<string> columnas = new List<string>();

            using (SqlCommand comando = new SqlCommand(
                "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS " +
                "WHERE TABLE_NAME = @Tabla ORDER BY ORDINAL_POSITION", conexion))
            {
                comando.Parameters.AddWithValue("@Tabla", tabla);

                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                        columnas.Add(lector["COLUMN_NAME"].ToString());
                }
            }

            return columnas;
        }

        private static bool TieneIdentidad(SqlConnection conexion, string tabla)
        {
            using (SqlCommand comando = new SqlCommand(
                "SELECT COUNT(1) FROM sys.identity_columns " +
                "WHERE OBJECT_NAME(object_id) = @Tabla", conexion))
            {
                comando.Parameters.AddWithValue("@Tabla", tabla);
                return Convert.ToInt32(comando.ExecuteScalar()) > 0;
            }
        }

        private static string FormatearValor(object valor)
        {
            if (valor == null || valor == DBNull.Value)
                return "NULL";

            if (valor is bool)
                return ((bool)valor) ? "1" : "0";

            if (valor is DateTime)
                return "'" + ((DateTime)valor).ToString("yyyy-MM-ddTHH:mm:ss") + "'";

            if (valor is int || valor is long || valor is short || valor is byte)
                return valor.ToString();

            if (valor is decimal || valor is double || valor is float)
                return Convert.ToDecimal(valor).ToString(
                    System.Globalization.CultureInfo.InvariantCulture);

            return "N'" + valor.ToString().Replace("'", "''") + "'";
        }
    }
}
