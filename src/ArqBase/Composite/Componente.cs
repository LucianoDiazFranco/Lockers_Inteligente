using LockersInteligentes.ArqBase.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockersInteligentes.ArqBase.Composite
{
    public abstract class Componente : EntidadBase
    {
        /// <summary>Identificador del permiso o nombre de la familia. Único.</summary>
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        /// <summary>Resuelve si este componente otorga el permiso pedido.</summary>
        public abstract bool TienePermiso(string nombrePatente);

        /// <summary>Hijos directos. Una hoja devuelve una colección vacía.</summary>
        public abstract IEnumerable<Componente> ObtenerHijos();

        /// <summary>
        /// Indica si este componente es, o contiene en cualquier nivel, al otro.
        /// Se usa para impedir ciclos al armar el árbol.
        /// </summary>
        public bool ContieneA(Componente componente)
        {
            if (componente == null)
                return false;

            if (Id != 0 && Id == componente.Id)
                return true;

            foreach (Componente hijo in ObtenerHijos())
            {
                if (hijo.ContieneA(componente))
                    return true;
            }

            return false;
        }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
