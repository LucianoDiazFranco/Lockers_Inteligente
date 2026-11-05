using LockersInteligentes.ArqBase.Composite;
using LockersInteligentes.ArqBase.DataAccess;
using LockersInteligentes.ArqBase.Entidades;
using LockersInteligentes.ArqBaseExcepciones;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LockersInteligentes.ArqBase.Logica
{
    public sealed class GestorSesion
    {
        private static readonly GestorSesion _instancia = new GestorSesion();

        private IList<Componente> _permisos = new List<Componente>();

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

        /// <summary>Componentes asignados al usuario, para mostrarlos si hace falta.</summary>
        public IList<Componente> Permisos
        {
            get { return _permisos; }
        }

        public void IniciarSesion(Usuario usuario)
        {
            if (usuario == null)
                throw new ArgumentNullException("usuario");

            UsuarioActual = usuario;

            // Los permisos se cargan una sola vez: no cambian durante la sesión y
            // consultarlos en cada validación serían decenas de viajes a la base.
            _permisos = RepositorioFactoryArq.Instancia.Componentes.ObtenerDeUsuario(usuario.Id);
        }

        public void CerrarSesion()
        {
            UsuarioActual = null;
            _permisos = new List<Componente>();
        }

        /// <summary>Valor del campo Actor de la bitácora.</summary>
        public string ActorParaBitacora()
        {
            return UsuarioActual != null ? UsuarioActual.NombreUsuario : "(sin sesion)";
        }

        /// <summary>
        /// Pregunta a cada componente asignado si otorga el permiso. Las familias
        /// responden recorriendo su árbol hacia abajo; las patentes, comparando su
        /// propio nombre. Acá no se distingue entre unas y otras: esa uniformidad
        /// es el patrón Composite funcionando.
        /// </summary>
        public bool TienePermiso(string nombrePatente)
        {
            if (UsuarioActual == null || string.IsNullOrWhiteSpace(nombrePatente))
                return false;

            return _permisos.Any(c => c.TienePermiso(nombrePatente));
        }

        /// <summary>Corta la operación si el usuario no tiene la patente.</summary>
        public void ValidarPermiso(string nombrePatente)
        {
            if (!TienePermiso(nombrePatente))
                throw new AccesoDenegadoException(
                    "No tenés permiso para esta operación (" + nombrePatente + ").");
        }

        /// <summary>
        /// Se conserva para compatibilidad y para mostrar el rol en la interfaz,
        /// pero ya NO decide accesos: eso lo resuelve TienePermiso.
        /// </summary>
        public bool EsAdministrador()
        {
            return UsuarioActual != null && UsuarioActual.EsAdministrador();
        }
    }
}