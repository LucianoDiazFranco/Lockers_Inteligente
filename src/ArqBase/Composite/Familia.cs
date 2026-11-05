using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockersInteligentes.ArqBase.Composite
{
    public class Familia : Componente
    {
        private readonly List<Componente> _hijos = new List<Componente>();

        public override bool TienePermiso(string nombrePatente)
        {
            // Delega en los hijos sin preguntar de qué tipo son: si el hijo es una
            // patente responde por sí misma, y si es otra familia vuelve a delegar.
            // Esa recursividad es el corazón del patrón.
            foreach (Componente hijo in _hijos)
            {
                if (hijo.TienePermiso(nombrePatente))
                    return true;
            }

            return false;
        }

        public override IEnumerable<Componente> ObtenerHijos()
        {
            return _hijos;
        }

        /// <summary>
        /// Agrega un hijo impidiendo ciclos. Si una familia pudiera contenerse a
        /// sí misma, directa o indirectamente, TienePermiso entraría en recursión
        /// infinita y la aplicación se colgaría. Es el error clásico al implementar
        /// Composite con estructuras que edita el usuario.
        /// </summary>
        public void Agregar(Componente componente)
        {
            if (componente == null)
                throw new ArgumentNullException("componente");

            if (componente.ContieneA(this))
                throw new InvalidOperationException(
                    "No se puede agregar '" + componente.Nombre + "' a '" + Nombre +
                    "': generaría un ciclo en el árbol de permisos.");

            if (!_hijos.Contains(componente))
                _hijos.Add(componente);
        }

        public void Quitar(Componente componente)
        {
            _hijos.Remove(componente);
        }

        public void LimpiarHijos()
        {
            _hijos.Clear();
        }
    }
}
