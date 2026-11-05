
namespace LockersInteligentes.DAL.Repositorios
{
    public sealed class RepositorioFactory
    {
        private static readonly RepositorioFactory _instancia = new RepositorioFactory();
        private RepositorioEdificio _edificios;
        private RepositorioLocker _lockers;
        private RepositorioResidente _residentes;
        private RepositorioOrdenDeEntrega _ordenes;
        private RepositorioCodigoAcceso _codigos;
        private RepositorioRepartidor _repartidores;
        private RepositorioReporte _reportes;
        private RepositorioFactory()
        {}
        public static RepositorioFactory Instancia
        {
            get { return _instancia; }
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