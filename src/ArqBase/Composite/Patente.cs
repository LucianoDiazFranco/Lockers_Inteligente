using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockersInteligentes.ArqBase.Composite
{
    public class Patente : Componente
    {
        public override bool TienePermiso(string nombrePatente)
        {
            return string.Equals(Nombre, nombrePatente, StringComparison.OrdinalIgnoreCase);
        }

        public override IEnumerable<Componente> ObtenerHijos()
        {
            return new List<Componente>();
        }
    }
}
