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
    public interface ILockerService
    {
        IList<Edificio> ListarEdificios();
        IList<EstadoLockerVista> ObtenerEstado(int idEdificio);
        IList<string> ListarSectores(int idEdificio);
        ResumenLockers Resumir(IEnumerable<EstadoLockerVista> vista);

        IList<Locker> ListarPorEdificio(int idEdificio);
        Locker Crear(int idEdificio, int numero, string sector, string descripcion,
                     TamanioPaquete tamanio);
        void Modificar(Locker locker);
        void CambiarEstado(int idLocker, EstadoLocker nuevoEstado);
        void Eliminar(int idLocker);
        int GenerarTanda(int idEdificio, int cantidad, int numeroInicial,
                         string sector, TamanioPaquete tamanio);
    }
}
