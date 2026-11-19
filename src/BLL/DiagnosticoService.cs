using LockersInteligentes.ArqBase.DataAccess;
using LockersInteligentes.ArqBase.Logica;
using LockersInteligentes.BLL.Contratos;

namespace LockersInteligentes.BLL
{
    public class DiagnosticoService : IDiagnosticoService
    {
        public bool ProbarConexion(out string mensajeError)
        {
            return Conexion.Probar(out mensajeError);
        }
    }
}