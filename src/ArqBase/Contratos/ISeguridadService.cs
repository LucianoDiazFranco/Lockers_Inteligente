using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockersInteligentes.ArqBase.Contratos
{
    public interface ISeguridadService
    {
        void GenerarCredenciales(string password, out string salt, out string hash);
    }
}
