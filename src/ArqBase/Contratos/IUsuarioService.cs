using LockersInteligentes.ArqBase.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockersInteligentes.ArqBase.Contratos
{
    public interface IUsuarioService
    {
        IList<Usuario> Listar();
        IList<Rol> ListarRoles();
        Usuario Crear(string nombreUsuario, string nombre, string apellido,
                      string password, int idRol);
        Usuario RegistrarOperador(string nombreUsuario, string nombre, string apellido,
                                  string correo, string password);
        void Modificar(Usuario usuario);
        void Desactivar(int idUsuario);
        void RestablecerPassword(int idUsuario, string passwordNueva);
        void CambiarPasswordPropia(string passwordActual, string passwordNueva);
        IList<Usuario> VerificarIntegridad();
        int RecalcularDigitos();
    }
}
