using LockersInteligentes.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockersInteligentes.BLL.Contratos
{
    public interface IRepartidorService
    {
        IList<Repartidor> Listar();
        IList<Repartidor> ListarActivos();
        Repartidor Crear(string nombre, string apellido, string dni,
                         string empresa, string telefono);
        void Modificar(Repartidor repartidor);
        void Desactivar(int idRepartidor);
        void Reactivar(int idRepartidor);
    }
}
