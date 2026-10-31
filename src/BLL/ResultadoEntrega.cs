using LockersInteligentes.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockersInteligentes.BLL
{
    /// <summary>
    /// Resultado de CU.Op.002. Devuelve el PIN de retiro en claro porque es el único
    /// momento en que existe: en la base solo queda su hash.
    /// </summary>

    public class ResultadoEntrega
    {
        public OrdenDeEntrega Orden { get; set; }
        public string PinRetiro { get; set; }
        public bool NotificacionEnviada { get; set; }
        public string MotivoFalloNotificacion { get; set; }
    }
}
