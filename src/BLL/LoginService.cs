using LockersInteligentes.ArqBase.Servicios;
using LockersInteligentes.DAL.Repositorios;
using LockersInteligentes.Dominio.Entidades;

namespace LockersInteligentes.BLL
{
    public class LoginService
    {
        private const string MensajeCredencialesInvalidas = "Usuario o contraseña incorrectos.";

        public ResultadoLogin IniciarSesion(string nombreUsuario, string password)
        {
            ResultadoLogin resultado = new ResultadoLogin();
            resultado.Exitoso = false;

            if (string.IsNullOrWhiteSpace(nombreUsuario) || string.IsNullOrWhiteSpace(password))
            {
                resultado.Mensaje = "Complete usuario y contraseña.";
                return resultado;
            }

            Usuario usuario = RepositorioFactory.Instancia.Usuarios
                                                .ObtenerPorNombreUsuario(nombreUsuario);

            if (usuario == null)
            {
                resultado.Mensaje = MensajeCredencialesInvalidas;
                return resultado;
            }

            if (!usuario.Activo)
            {
                resultado.Mensaje = "El usuario esta inactivo. Contacte al administrador.";
                return resultado;
            }

            if (!EncriptadorService.Verificar(password, usuario.PasswordSalt, usuario.PasswordHash))
            {
                resultado.Mensaje = MensajeCredencialesInvalidas;
                return resultado;
            }

            GestorSesion.Instancia.IniciarSesion(usuario);

            resultado.Exitoso = true;
            resultado.Usuario = usuario;
            return resultado;
        }
        public void CerrarSesion()
        {
            GestorSesion.Instancia.CerrarSesion();
        }
    }
}