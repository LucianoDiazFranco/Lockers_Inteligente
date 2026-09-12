using LockersInteligentes.DAL;

namespace LockersInteligentes.BLL
{
    public class DiagnosticoService
    {
        public bool ProbarConexion(out string mensajeError)
        {
            return Conexion.Probar(out mensajeError);
        }
    }
}