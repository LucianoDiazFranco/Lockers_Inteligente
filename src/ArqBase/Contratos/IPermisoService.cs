using LockersInteligentes.ArqBase.Composite;
using LockersInteligentes.ArqBase.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockersInteligentes.ArqBase.Contratos
{
    public interface IPermisoService
    {
        IList<Componente> ObtenerDeUsuario(int idUsuario);
        IList<Componente> ObtenerRaices();
        IDictionary<int, Componente> ObtenerArbol();
        Componente Crear(string nombre, string descripcion, TipoComponente tipo);
        void Modificar(int idComponente, string nombre, string descripcion);
        void Eliminar(int idComponente);
        void AgregarHijo(int idPadre, int idHijo);
        void QuitarHijo(int idPadre, int idHijo);
        void AsignarAUsuario(int idUsuario, int idComponente);
        void QuitarDeUsuario(int idUsuario, int idComponente);
    }
}
