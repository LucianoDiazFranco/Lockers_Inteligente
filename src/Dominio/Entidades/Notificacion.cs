using System;
using LockersInteligentes.Dominio.Enums;

namespace LockersInteligentes.Dominio.Entidades
{

    public class Notificacion : EntidadBase
    {
        public Notificacion()
        {
            Estado = EstadoNotificacion.Pendiente;
        }

        public OrdenDeEntrega OrdenDeEntrega { get; set; }

        public Residente ResidenteEnviado { get; set; }

        public string Mensaje { get; set; }
        public EstadoNotificacion Estado { get; set; }

        public DateTime? FechaEnvio { get; set; }

        public void MarcarEnviada()
        {
            Estado = EstadoNotificacion.Enviada;
            FechaEnvio = DateTime.Now;
        }

        public void MarcarFallida()
        {
            Estado = EstadoNotificacion.Fallida;
            FechaEnvio = DateTime.Now;
        }

        public override string ToString()
        {
            return "Notificacion " + Estado + " a " +
                   (ResidenteEnviado != null ? ResidenteEnviado.Correo : "(sin destinatario)");
        }
    }
}
