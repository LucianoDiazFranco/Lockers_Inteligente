using LockersInteligentes.BLL.Modelos;
using LockersInteligentes.Dominio.Entidades;
using LockersInteligentes.Dominio.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockersInteligentes.BLL.Contratos
{
    public interface IEntregaService
    {
        ResultadoReserva ReservarLocker(int idResidente, TamanioPaquete tamanio, string descripcion);
        ResultadoEntrega RegistrarEntrega(int idOrden, string pinIngresado, int idRepartidor);
        OrdenDeEntrega RegistrarRetiro(int idOrden, string pinIngresado);
        void CancelarReservaVencida(int idOrden);

        IList<OrdenDeEntrega> ListarPendientesDeEntrega();
        IList<OrdenDeEntrega> ListarPendientesDeRetiro();
        IList<OrdenDeEntrega> ListarReservasVencidas();
        IList<Residente> ListarResidentes();
        IList<Repartidor> ListarRepartidores();
    }
}
