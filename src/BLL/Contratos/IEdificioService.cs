using LockersInteligentes.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockersInteligentes.BLL.Contratos
{
    public interface IEdificioService
    {
        IList<Edificio> Listar();
        Edificio Crear(string nombre, string direccion, string localidad, string telefono);
        void Modificar(Edificio edificio);
        void Eliminar(int idEdificio);
        void SincronizarCantidad(int idEdificio);
    }
}
