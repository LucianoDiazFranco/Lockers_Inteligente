using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LockersInteligentes.ArqBase.ArqBase
{
    /// <summary>
    /// Serializar. Convierte objetos a XML y los recupera.
    /// La clase es genérica para que sirva para cualquier tipo serializable, no
    /// solo para los errores.
    /// </summary>
    public static class SerializadorService
    {
        /// <summary>Guarda una lista completa, reemplazando el archivo.</summary>
        public static void Guardar<T>(IList<T> datos, string rutaArchivo)
        {
            if (datos == null)
                throw new ArgumentNullException("datos");

            string carpeta = Path.GetDirectoryName(rutaArchivo);

            if (!string.IsNullOrEmpty(carpeta) && !Directory.Exists(carpeta))
                Directory.CreateDirectory(carpeta);

            XmlSerializer serializador = new XmlSerializer(typeof(List<T>));

            using (StreamWriter escritor = new StreamWriter(rutaArchivo, false))
            {
                serializador.Serialize(escritor, new List<T>(datos));
            }
        }

        /// <summary>
        /// Lee una lista desde el archivo. Si no existe devuelve una vacía: que
        /// todavía no haya errores registrados no es un problema.
        /// </summary>
        public static IList<T> Leer<T>(string rutaArchivo)
        {
            if (!File.Exists(rutaArchivo))
                return new List<T>();

            XmlSerializer serializador = new XmlSerializer(typeof(List<T>));

            using (StreamReader lector = new StreamReader(rutaArchivo))
            {
                return (List<T>)serializador.Deserialize(lector);
            }
        }
    }
}
