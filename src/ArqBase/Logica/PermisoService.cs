using LockersInteligentes.ArqBase.Composite;
using LockersInteligentes.ArqBase.DataAccess;
using LockersInteligentes.ArqBase.Enums;
using LockersInteligentes.ArqBase.Excepciones;
using System.Collections.Generic;
using System.Linq;


namespace LockersInteligentes.ArqBase.Logica
{
    public class PermisoService
    {
        /// <summary>Nombres de las patentes del sistema, para no escribir literales sueltos.</summary>
        public static class Patentes
        {
            public const string GestionarUsuarios = "Usuarios.Gestionar";
            public const string GestionarPermisos = "Permisos.Gestionar";
            public const string GestionarEdificios = "Edificios.Gestionar";
            public const string GestionarLockers = "Lockers.Gestionar";
            public const string GestionarResidentes = "Residentes.Gestionar";
            public const string GestionarRepartidores = "Repartidores.Gestionar";
            public const string VerEstadoLockers = "Lockers.VerEstado";
            public const string Reservar = "Paquetes.Reservar";
            public const string RegistrarEntrega = "Paquetes.RegistrarEntrega";
            public const string RegistrarRetiro = "Paquetes.RegistrarRetiro";
            public const string VerReportes = "Reportes.Ver";
            public const string VerBitacora = "Bitacora.Ver";
            public const string VerErrores = "Errores.Ver";
            public const string EjecutarBackup = "Backup.Ejecutar";
        }

        public IList<Componente> ObtenerDeUsuario(int idUsuario)
        {
            return RepositorioFactoryArq.Instancia.Componentes.ObtenerDeUsuario(idUsuario);
        }

        public IList<Componente> ObtenerRaices()
        {
            return RepositorioFactoryArq.Instancia.Componentes.ObtenerRaices();
        }

        public IDictionary<int, Componente> ObtenerArbol()
        {
            return RepositorioFactoryArq.Instancia.Componentes.ObtenerArbolCompleto();
        }

        public Componente Crear(string nombre, string descripcion, TipoComponente tipo)
        {
            ValidarGestion();

            if (string.IsNullOrWhiteSpace(nombre))
                throw new ExcepcionNegocio("El nombre es obligatorio.");

            return RepositorioFactoryArq.Instancia.Componentes
                .Insertar(nombre.Trim(), descripcion, tipo);
        }

        public void Modificar(int idComponente, string nombre, string descripcion)
        {
            ValidarGestion();

            if (string.IsNullOrWhiteSpace(nombre))
                throw new ExcepcionNegocio("El nombre es obligatorio.");

            RepositorioFactoryArq.Instancia.Componentes
                .Actualizar(idComponente, nombre.Trim(), descripcion);
        }

        public void Eliminar(int idComponente)
        {
            ValidarGestion();
            RepositorioFactoryArq.Instancia.Componentes.Eliminar(idComponente);
        }

        /// <summary>
        /// Agrega un componente dentro de una familia. Verifica el ciclo ANTES de
        /// tocar la base: si se guardara un ciclo, cualquier consulta de permisos
        /// entraría en recursión infinita y la aplicación se colgaría al siguiente
        /// inicio de sesión.
        /// </summary>
        public void AgregarHijo(int idPadre, int idHijo)
        {
            ValidarGestion();

            if (idPadre == idHijo)
                throw new ExcepcionNegocio("Un componente no puede contenerse a sí mismo.");

            IDictionary<int, Componente> arbol =
                RepositorioFactoryArq.Instancia.Componentes.ObtenerArbolCompleto();

            Componente padre;
            Componente hijo;

            if (!arbol.TryGetValue(idPadre, out padre) || !arbol.TryGetValue(idHijo, out hijo))
                throw new ExcepcionNegocio("El componente no existe.");

            if (!(padre is Familia))
                throw new ExcepcionNegocio(
                    "Solo una familia puede contener otros componentes. " +
                    "'" + padre.Nombre + "' es una patente.");

            if (hijo.ContieneA(padre))
                throw new ExcepcionNegocio(
                    "No se puede agregar '" + hijo.Nombre + "' a '" + padre.Nombre +
                    "': generaría un ciclo en el árbol de permisos.");

            if (padre.ObtenerHijos().Any(h => h.Id == idHijo))
                throw new ExcepcionNegocio("'" + hijo.Nombre + "' ya está dentro de '" +
                                           padre.Nombre + "'.");

            RepositorioFactoryArq.Instancia.Componentes.AgregarHijo(idPadre, idHijo);
        }

        public void QuitarHijo(int idPadre, int idHijo)
        {
            ValidarGestion();
            RepositorioFactoryArq.Instancia.Componentes.QuitarHijo(idPadre, idHijo);
        }

        public void AsignarAUsuario(int idUsuario, int idComponente)
        {
            ValidarGestion();

            if (ObtenerDeUsuario(idUsuario).Any(c => c.Id == idComponente))
                throw new ExcepcionNegocio("El usuario ya tiene asignado ese componente.");

            RepositorioFactoryArq.Instancia.Componentes.AsignarAUsuario(idUsuario, idComponente);
        }

        public void QuitarDeUsuario(int idUsuario, int idComponente)
        {
            ValidarGestion();

            // Impide que alguien se quite a sí mismo el permiso de administrar
            // permisos y deje al sistema sin forma de recuperarse.
            if (GestorSesion.Instancia.UsuarioActual != null &&
                GestorSesion.Instancia.UsuarioActual.Id == idUsuario)
            {
                IList<Componente> restantes = ObtenerDeUsuario(idUsuario)
                    .Where(c => c.Id != idComponente).ToList();

                if (!restantes.Any(c => c.TienePermiso(Patentes.GestionarPermisos)))
                    throw new ExcepcionNegocio(
                        "No podés quitarte el permiso de administrar permisos: " +
                        "quedarías sin acceso a esta pantalla.");
            }

            RepositorioFactoryArq.Instancia.Componentes.QuitarDeUsuario(idUsuario, idComponente);
        }

        private void ValidarGestion()
        {
            GestorSesion.Instancia.ValidarPermiso(Patentes.GestionarPermisos);
        }
    }

}
