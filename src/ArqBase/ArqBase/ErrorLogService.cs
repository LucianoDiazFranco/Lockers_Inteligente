using LockersInteligentes.ArqBase.Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockersInteligentes.ArqBase.ArqBase
{
    /// <summary>
    /// Guardar Errores y CU.Arq.007 Ver Errores.
    /// Vive en Arq. Base porque no toca la base de datos, así que la DAL puede
    /// usarlo sin referencia circular.
    /// </summary>
    public static class ErrorLogService
    {
        private const string NombreArchivo = "errores.xml";
        private const int MaximoRegistros = 500;
        public static string RutaArchivo
        {
            get
            {
                string carpeta = ConfigurationManager.AppSettings["CarpetaLogErrores"];

                if (string.IsNullOrWhiteSpace(carpeta))
                    carpeta = "Logs";

                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, carpeta, NombreArchivo);
            }
        }

        /// <summary>
        /// Registra una excepción. NUNCA propaga: si el log falla, la aplicación
        /// tiene que seguir. Un problema al escribir el registro no puede
        /// convertirse en un segundo error encima del primero.
        /// </summary>
        public static void Registrar(Exception ex, string origen, string usuario)
        {
            try
            {
                if (ex == null)
                    return;

                List<RegistroError> registros =
                    new List<RegistroError>(SerializadorService.Leer<RegistroError>(RutaArchivo));

                registros.Add(RegistroError.DesdeExcepcion(ex, origen, usuario));

                // Se conservan los últimos N: sin tope, el archivo crecería sin
                // límite y tardaría cada vez más en leerse.
                if (registros.Count > MaximoRegistros)
                    registros = registros
                        .OrderByDescending(r => r.FechaHora)
                        .Take(MaximoRegistros)
                        .ToList();

                SerializadorService.Guardar(registros, RutaArchivo);
            }
            catch
            {
                // Silencio deliberado: no hay a dónde reportar un fallo del log.
            }
        }

        /// <summary> Devuelve los errores del más reciente al más viejo.</summary>
        public static IList<RegistroError> Listar()
        {
            return SerializadorService.Leer<RegistroError>(RutaArchivo)
                .OrderByDescending(r => r.FechaHora)
                .ToList();
        }

        /// <summary>Vacía el log. Operación de mantenimiento.</summary>
        public static void Limpiar()
        {
            SerializadorService.Guardar(new List<RegistroError>(), RutaArchivo);
        }

        public static bool ExisteArchivo()
        {
            return File.Exists(RutaArchivo);
        }
    }
}
