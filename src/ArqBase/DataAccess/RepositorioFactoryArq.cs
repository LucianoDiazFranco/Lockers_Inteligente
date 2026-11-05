

namespace LockersInteligentes.ArqBase.DataAccess
{
    public sealed class RepositorioFactoryArq
    {
        private static readonly RepositorioFactoryArq _instancia = new RepositorioFactoryArq();

        private RepositorioUsuario _usuarios;
        private RepositorioRol _roles;
        private RepositorioHistorial _historial;
        private RepositorioComponente _componentes;
        private RepositorioIdioma _idiomas;
        private RepositorioTraduccion _traducciones;

        private RepositorioFactoryArq()
        {
        }

        public static RepositorioFactoryArq Instancia
        {
            get { return _instancia; }
        }

        public RepositorioUsuario Usuarios
        {
            get
            {
                if (_usuarios == null)
                    _usuarios = new RepositorioUsuario();

                return _usuarios;
            }
        }

        public RepositorioRol Roles
        {
            get
            {
                if (_roles == null)
                    _roles = new RepositorioRol();

                return _roles;
            }
        }

        public RepositorioHistorial Historial
        {
            get
            {
                if (_historial == null)
                    _historial = new RepositorioHistorial();

                return _historial;
            }
        }
        public RepositorioComponente Componentes
        {
            get
            {
                if (_componentes == null)
                    _componentes = new RepositorioComponente();

                return _componentes;
            }
        }
        public RepositorioIdioma Idiomas
        {
            get
            {
                if (_idiomas == null)
                    _idiomas = new RepositorioIdioma();

                return _idiomas;
            }
        }

        public RepositorioTraduccion Traducciones
        {
            get
            {
                if (_traducciones == null)
                    _traducciones = new RepositorioTraduccion();

                return _traducciones;
            }
        }
    }
}
