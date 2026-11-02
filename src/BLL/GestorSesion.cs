using System;
using LockersInteligentes.Dominio.Entidades;
using LockersInteligentes.Dominio.Excepciones;

namespace LockersInteligentes.BLL
{
    public sealed class GestorSesion
    {
        private static readonly GestorSesion _instancia = new GestorSesion();

        private GestorSesion()
        {
        }
        public static GestorSesion Instancia
        {
            get { return _instancia; }
        }
        public Usuario UsuarioActual { get; private set; }

        public bool HaySesionActiva
        {
            get { return UsuarioActual != null; }
        }
        public void IniciarSesion(Usuario usuario)
        {
            if (usuario == null)
                throw new ArgumentNullException("usuario");

            UsuarioActual = usuario;
        }
        public void CerrarSesion()
        {
            UsuarioActual = null;
        }

        public string ActorParaBitacora()
        {
            return UsuarioActual != null ? UsuarioActual.NombreUsuario : "(sin sesion)";
        }
        public bool EsAdministrador()
        {
            return UsuarioActual != null && UsuarioActual.EsAdministrador();
        }
        public void ValidarRolAdministrador()
        {
            if (!EsAdministrador())
                throw new AccesoDenegadoException("Esta operación requiere el rol Administrador.");
        }
    }
}