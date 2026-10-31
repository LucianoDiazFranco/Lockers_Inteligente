namespace LockersInteligentes.DAL.Repositorios
{
    public sealed class RepositorioFactory
    {
        private static readonly RepositorioFactory _instancia = new RepositorioFactory();
        private RepositorioUsuario _usuarios;
        private RepositorioRol _roles;
        private RepositorioEdificio _edificios;
        private RepositorioLocker _lockers;
        private RepositorioResidente _residentes;
        private RepositorioOrdenDeEntrega _ordenes;
        private RepositorioCodigoAcceso _codigos;
        private RepositorioRepartidor _repartidores;
        private RepositorioHistorial _historial;
        private RepositorioReporte _reportes;
        private RepositorioFactory()
        {}
        public static RepositorioFactory Instancia
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
        public RepositorioEdificio Edificios
        {
            get
            {
                if (_edificios == null)
                    _edificios = new RepositorioEdificio();

                return _edificios;
            }
        }

        public RepositorioLocker Lockers
        {
            get
            {
                if (_lockers == null)
                    _lockers = new RepositorioLocker();

                return _lockers;
            }
        }

        public RepositorioResidente Residentes
        {
            get
            {
                if (_residentes == null)
                    _residentes = new RepositorioResidente();

                return _residentes;
            }
        }
        public RepositorioOrdenDeEntrega Ordenes
        {
            get
            {
                if (_ordenes == null)
                    _ordenes = new RepositorioOrdenDeEntrega();

                return _ordenes;
            }
        }

        public RepositorioCodigoAcceso Codigos
        {
            get
            {
                if (_codigos == null)
                    _codigos = new RepositorioCodigoAcceso();

                return _codigos;
            }
        }
        public RepositorioRepartidor Repartidores
        {
            get
            {
                if (_repartidores == null)
                    _repartidores = new RepositorioRepartidor();

                return _repartidores;
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
        public RepositorioReporte Reportes
        {
            get
            {
                if (_reportes == null)
                    _reportes = new RepositorioReporte();

                return _reportes;
            }
        }

    }
}