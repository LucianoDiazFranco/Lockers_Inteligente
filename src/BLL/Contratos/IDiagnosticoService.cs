using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockersInteligentes.BLL.Contratos
{
    public interface IDiagnosticoService
    {
        bool ProbarConexion(out string mensajeError);
    }
}
