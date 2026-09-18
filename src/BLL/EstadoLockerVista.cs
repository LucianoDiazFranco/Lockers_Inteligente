using LockersInteligentes.Dominio.Entidades;

namespace LockersInteligentes.BLL
{
    public class EstadoLockerVista
    {
        public Locker Locker { get; set; }
        public OrdenDeEntrega OrdenActiva { get; set; }
        public bool Vencido
        {
            get { return OrdenActiva != null && OrdenActiva.EstaVencida(); }
        }
        public bool ReservaExpirada
        {
            get { return OrdenActiva != null && OrdenActiva.ReservaExpirada(); }
        }
        public string Detalle()
        {
            if (OrdenActiva == null)
                return string.Empty;

            return OrdenActiva.ResidenteAsignado.Apellido;
        }
    }
}