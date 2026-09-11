using System;

namespace LockersInteligentes.ArqBase.Entidades
{
    [Serializable]
    public class RegistroError
    {
        public RegistroError()
        {
            FechaHora = DateTime.Now;
        }

        public DateTime FechaHora { get; set; }

        public string Usuario { get; set; }

        public string Origen { get; set; }

        public string TipoExcepcion { get; set; }

        public string Mensaje { get; set; }

        public string DetalleInterno { get; set; }

        public string StackTrace { get; set; }

        public static RegistroError DesdeExcepcion(Exception ex, string origen, string usuario)
        {
            if (ex == null)
                throw new ArgumentNullException("ex");

            return new RegistroError
            {
                FechaHora = DateTime.Now,
                Usuario = string.IsNullOrWhiteSpace(usuario) ? "(sin sesion)" : usuario,
                Origen = origen,
                TipoExcepcion = ex.GetType().Name,
                Mensaje = ex.Message,
                DetalleInterno = ex.InnerException != null ? ex.InnerException.Message : null,
                StackTrace = ex.StackTrace
            };
        }

        public override string ToString()
        {
            return FechaHora.ToString("dd/MM/yyyy HH:mm:ss") + " [" + TipoExcepcion + "] " +
                   Origen + " - " + Mensaje;
        }
    }
}