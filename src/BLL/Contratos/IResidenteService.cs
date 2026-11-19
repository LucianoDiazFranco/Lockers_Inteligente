using LockersInteligentes.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockersInteligentes.BLL.Contratos
{
    public interface IResidenteService
    {
        IList<Residente> ListarPorEdificio(int idEdificio);
        IList<Residente> ListarActivos();
        Residente Crear(int idEdificio, string nombre, string apellido, string dni,
                        string correo, string piso, string telefono);
        void Modificar(Residente residente);
        void Desactivar(int idResidente);
        void Reactivar(int idResidente);
    }
}
