using System;
using System.Collections.Generic;
using LockersInteligentes.ArqBase.Composite;
using LockersInteligentes.ArqBase.Entidades;
using LockersInteligentes.ArqBase.Enums;
using LockersInteligentes.ArqBase.Logica;
using LockersInteligentes.Dominio.Entidades;
using LockersInteligentes.Dominio.Enums;

namespace LockersInteligentes.BLL
{
    /// <summary>
    /// Patrón Facade: punto de entrada único al sistema.
    ///
    /// La UI deja de conocer los ocho services y habla solo con esta clase. Eso
    /// tiene dos efectos: reduce el acoplamiento (un formulario depende de un tipo
    /// en lugar de varios) y centraliza el lugar donde se ajusta cualquier cambio
    /// de firma de un service.
    ///
    /// La fachada NO contiene lógica de negocio: solo delega. Si empezara a decidir
    /// cosas, dejaría de ser una fachada y se convertiría en un service más, con
    /// la diferencia de que acumularía responsabilidades de todos los demás.
    ///
    /// Es Singleton para que la UI no tenga que instanciarla en cada formulario.
    /// </summary>
    public sealed class SistemaLockersFacade
    {
        private static readonly SistemaLockersFacade _instancia = new SistemaLockersFacade();

        private readonly LoginService _login = new LoginService();
        private readonly UsuarioService _usuarios = new UsuarioService();
        private readonly PermisoService _permisos = new PermisoService();
        private readonly HistorialService _historial = new HistorialService();
        private readonly EdificioService _edificios = new EdificioService();
        private readonly LockerService _lockers = new LockerService();
        private readonly ResidenteService _residentes = new ResidenteService();
        private readonly RepartidorService _repartidores = new RepartidorService();
        private readonly EntregaService _entregas = new EntregaService();
        private readonly ReporteService _reportes = new ReporteService();

        private SistemaLockersFacade()
        {
        }

        public static SistemaLockersFacade Instancia
        {
            get { return _instancia; }
        }

        /* ------------------------- Seguridad ------------------------- */

        public ResultadoLogin IniciarSesion(string usuario, string password)
        {
            return _login.IniciarSesion(usuario, password);
        }

        public void CerrarSesion()
        {
            _login.CerrarSesion();
        }

        public Usuario UsuarioActual()
        {
            return GestorSesion.Instancia.UsuarioActual;
        }

        public bool TienePermiso(string patente)
        {
            return GestorSesion.Instancia.TienePermiso(patente);
        }

        public IList<Usuario> ListarUsuarios()
        {
            return _usuarios.Listar();
        }

        public IList<Rol> ListarRoles()
        {
            return _usuarios.ListarRoles();
        }

        public Usuario CrearUsuario(string nombreUsuario, string nombre, string apellido,
                                    string password, int idRol)
        {
            return _usuarios.Crear(nombreUsuario, nombre, apellido, password, idRol);
        }

        public Usuario RegistrarOperador(string nombreUsuario, string nombre, string apellido,
                                         string correo, string password)
        {
            return _usuarios.RegistrarOperador(nombreUsuario, nombre, apellido, correo, password);
        }

        public void ModificarUsuario(Usuario usuario)
        {
            _usuarios.Modificar(usuario);
        }

        public void DesactivarUsuario(int idUsuario)
        {
            _usuarios.Desactivar(idUsuario);
        }

        public void RestablecerPassword(int idUsuario, string passwordNueva)
        {
            _usuarios.RestablecerPassword(idUsuario, passwordNueva);
        }

        /* -------------------------- Permisos -------------------------- */

        public IList<Componente> ListarPermisosRaiz()
        {
            return _permisos.ObtenerRaices();
        }

        public IDictionary<int, Componente> ObtenerArbolPermisos()
        {
            return _permisos.ObtenerArbol();
        }

        public IList<Componente> PermisosDeUsuario(int idUsuario)
        {
            return _permisos.ObtenerDeUsuario(idUsuario);
        }

        public Componente CrearComponente(string nombre, string descripcion, TipoComponente tipo)
        {
            return _permisos.Crear(nombre, descripcion, tipo);
        }

        public void ModificarComponente(int id, string nombre, string descripcion)
        {
            _permisos.Modificar(id, nombre, descripcion);
        }

        public void EliminarComponente(int id)
        {
            _permisos.Eliminar(id);
        }

        public void AnidarComponente(int idPadre, int idHijo)
        {
            _permisos.AgregarHijo(idPadre, idHijo);
        }

        public void DesanidarComponente(int idPadre, int idHijo)
        {
            _permisos.QuitarHijo(idPadre, idHijo);
        }

        public void AsignarPermisoAUsuario(int idUsuario, int idComponente)
        {
            _permisos.AsignarAUsuario(idUsuario, idComponente);
        }

        public void QuitarPermisoAUsuario(int idUsuario, int idComponente)
        {
            _permisos.QuitarDeUsuario(idUsuario, idComponente);
        }

        /* ------------------------- Gestión (ABM) ------------------------- */

        public IList<Edificio> ListarEdificios()
        {
            return _edificios.Listar();
        }

        public Edificio CrearEdificio(string nombre, string direccion,
                                      string localidad, string telefono)
        {
            return _edificios.Crear(nombre, direccion, localidad, telefono);
        }

        public void ModificarEdificio(Edificio edificio)
        {
            _edificios.Modificar(edificio);
        }

        public void EliminarEdificio(int idEdificio)
        {
            _edificios.Eliminar(idEdificio);
        }

        public IList<Locker> ListarLockers(int idEdificio)
        {
            return _lockers.ListarPorEdificio(idEdificio);
        }

        public Locker CrearLocker(int idEdificio, int numero, string sector,
                                  string descripcion, TamanioPaquete tamanio)
        {
            return _lockers.Crear(idEdificio, numero, sector, descripcion, tamanio);
        }

        public void ModificarLocker(Locker locker)
        {
            _lockers.Modificar(locker);
        }

        public void CambiarEstadoLocker(int idLocker, EstadoLocker nuevoEstado)
        {
            _lockers.CambiarEstado(idLocker, nuevoEstado);
        }

        public void EliminarLocker(int idLocker)
        {
            _lockers.Eliminar(idLocker);
        }

        public int GenerarLockers(int idEdificio, int cantidad, int numeroInicial,
                                  string sector, TamanioPaquete tamanio)
        {
            return _lockers.GenerarTanda(idEdificio, cantidad, numeroInicial, sector, tamanio);
        }

        public IList<Residente> ListarResidentes(int idEdificio)
        {
            return _residentes.ListarPorEdificio(idEdificio);
        }

        public Residente CrearResidente(int idEdificio, string nombre, string apellido,
                                        string dni, string correo, string piso, string telefono)
        {
            return _residentes.Crear(idEdificio, nombre, apellido, dni, correo, piso, telefono);
        }

        public void ModificarResidente(Residente residente)
        {
            _residentes.Modificar(residente);
        }

        public void DesactivarResidente(int idResidente)
        {
            _residentes.Desactivar(idResidente);
        }

        public void ReactivarResidente(int idResidente)
        {
            _residentes.Reactivar(idResidente);
        }

        public IList<Repartidor> ListarRepartidores()
        {
            return _repartidores.Listar();
        }

        public Repartidor CrearRepartidor(string nombre, string apellido, string dni,
                                          string empresa, string telefono)
        {
            return _repartidores.Crear(nombre, apellido, dni, empresa, telefono);
        }

        public void ModificarRepartidor(Repartidor repartidor)
        {
            _repartidores.Modificar(repartidor);
        }

        public void DesactivarRepartidor(int idRepartidor)
        {
            _repartidores.Desactivar(idRepartidor);
        }

        public void ReactivarRepartidor(int idRepartidor)
        {
            _repartidores.Reactivar(idRepartidor);
        }

        /* --------------------- Ciclo del paquete --------------------- */

        public ResultadoReserva ReservarLocker(int idResidente, TamanioPaquete tamanio,
                                               string descripcion)
        {
            return _entregas.ReservarLocker(idResidente, tamanio, descripcion);
        }

        public ResultadoEntrega RegistrarEntrega(int idOrden, string pin, int idRepartidor)
        {
            return _entregas.RegistrarEntrega(idOrden, pin, idRepartidor);
        }

        public OrdenDeEntrega RegistrarRetiro(int idOrden, string pin)
        {
            return _entregas.RegistrarRetiro(idOrden, pin);
        }

        public void CancelarReservaVencida(int idOrden)
        {
            _entregas.CancelarReservaVencida(idOrden);
        }

        public IList<OrdenDeEntrega> PendientesDeEntrega()
        {
            return _entregas.ListarPendientesDeEntrega();
        }

        public IList<OrdenDeEntrega> PendientesDeRetiro()
        {
            return _entregas.ListarPendientesDeRetiro();
        }

        public IList<Residente> ResidentesActivos()
        {
            return _entregas.ListarResidentes();
        }

        public IList<Repartidor> RepartidoresActivos()
        {
            return _entregas.ListarRepartidores();
        }

        /* ----------------------- Estado de lockers ----------------------- */

        public IList<Edificio> EdificiosParaPanel()
        {
            return _lockers.ListarEdificios();
        }

        public IList<EstadoLockerVista> EstadoDeLockers(int idEdificio)
        {
            return _lockers.ObtenerEstado(idEdificio);
        }

        public IList<string> SectoresDeEdificio(int idEdificio)
        {
            return _lockers.ListarSectores(idEdificio);
        }

        public ResumenLockers ResumirLockers(IEnumerable<EstadoLockerVista> vista)
        {
            return _lockers.Resumir(vista);
        }

        /* -------------------------- Información -------------------------- */

        public IList<Historial> BuscarBitacora(DateTime? desde, DateTime? hasta,
                                               string actor, TipoOperacion? tipo)
        {
            return _historial.Buscar(desde, hasta, actor, tipo);
        }

        public IList<Reporte> ListarReportes()
        {
            return _reportes.ListarGenerados();
        }

        public Reporte GenerarReporte(int idEdificio, DateTime desde, DateTime hasta)
        {
            return _reportes.Generar(idEdificio, desde, hasta);
        }
    }
}