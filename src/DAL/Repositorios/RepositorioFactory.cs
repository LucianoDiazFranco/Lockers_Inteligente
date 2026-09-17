namespace LockersInteligentes.DAL.Repositorios
{
    public sealed class RepositorioFactory
    {
        private static readonly RepositorioFactory _instancia = new RepositorioFactory();
        private RepositorioUsuario _usuarios;
        private RepositorioRol _roles;
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
    }
}