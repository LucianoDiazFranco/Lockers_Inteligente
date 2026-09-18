using LockersInteligentes.Dominio.Entidades;

namespace LockersInteligentes.BLL
{
    /// <summary>
    /// Resultado de CU.Op.001. Devuelve el PIN en claro porque es el único momento
    /// en que existe: en la base solo queda su hash. La UI lo muestra por si falla
    /// el email, y después se pierde para siempre.
    /// </summary>
    public class ResultadoReserva
    {
        public OrdenDeEntrega Orden { get; set; }
        public string PinApertura { get; set; }

        /// <summary>False si el mail no se pudo enviar. La reserva vale igual.</summary>
        public bool NotificacionEnviada { get; set; }

        public string MotivoFalloNotificacion { get; set; }
    }
}