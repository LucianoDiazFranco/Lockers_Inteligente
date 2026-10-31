using LockersInteligentes.DAL.Repositorios;
using LockersInteligentes.Dominio.Entidades;
using LockersInteligentes.Dominio.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockersInteligentes.BLL
{
    public class EdificioService
    {
        private readonly HistorialService _historial = new HistorialService();
        public IList<Edificio> Listar()
        {
            GestorSesion.Instancia.ValidarRolAdministrador();

            IList<Edificio> edificios = RepositorioFactory.Instancia.Edificios.ObtenerTodos();
            IDictionary<int, int> conteo = RepositorioFactory.Instancia.Lockers.ContarPorEdificio();

            foreach (Edificio edificio in edificios)
            {
                int cantidad;
                edificio.CantLockers = conteo.TryGetValue(edificio.Id, out cantidad) ? cantidad : 0;
            }

            return edificios;
        }
        public void SincronizarCantidad(int idEdificio)
        {
            Edificio edificio = RepositorioFactory.Instancia.Edificios.ObtenerPorId(idEdificio);

            if (edificio == null)
                return;

            edificio.CantLockers = RepositorioFactory.Instancia.Lockers.ContarPorEdificio(idEdificio);
            RepositorioFactory.Instancia.Edificios.Actualizar(edificio);
        }

        public Edificio Crear(string nombre, string direccion, string localidad,
                              string telefono)
        {
            GestorSesion.Instancia.ValidarRolAdministrador();
            Validar(nombre, direccion, localidad, 0);

            if (ExisteNombre(nombre, 0))
                throw new InvalidOperationException(
                    "Ya existe un edificio llamado '" + nombre.Trim() + "'.");

            Edificio edificio = new Edificio();
            edificio.Nombre = nombre.Trim();
            edificio.Direccion = direccion.Trim();
            edificio.Localidad = localidad.Trim();
            edificio.TelefonoContacto = string.IsNullOrWhiteSpace(telefono) ? null : telefono.Trim();
            edificio.CantLockers = 0; // un edificio nuevo arranca sin lockers

            RepositorioFactory.Instancia.Edificios.Insertar(edificio);

            _historial.Grabar(TipoOperacion.Alta,
                "Alta de edificio '" + edificio.Nombre + "' (" + edificio.Localidad + ")");

            return edificio;
        }

        public void Modificar(Edificio edificio)
        {
            GestorSesion.Instancia.ValidarRolAdministrador();

            if (edificio == null)
                throw new ArgumentNullException("edificio");

            Validar(edificio.Nombre, edificio.Direccion, edificio.Localidad, edificio.CantLockers);

            if (ExisteNombre(edificio.Nombre, edificio.Id))
                throw new InvalidOperationException(
                    "Ya existe otro edificio llamado '" + edificio.Nombre.Trim() + "'.");

            RepositorioFactory.Instancia.Edificios.Actualizar(edificio);

            _historial.Grabar(TipoOperacion.Modificacion,
                "Modificación del edificio '" + edificio.Nombre + "'");
        }

        public void Eliminar(int idEdificio)
        {
            GestorSesion.Instancia.ValidarRolAdministrador();

            int lockers = RepositorioFactory.Instancia.Lockers.ObtenerPorEdificio(idEdificio).Count;

            if (lockers > 0)
                throw new InvalidOperationException(
                    "No se puede eliminar: el edificio tiene " + lockers +
                    " locker(s) asociados. Eliminalos primero.");

            int residentes = RepositorioFactory.Instancia.Residentes.ObtenerPorEdificio(idEdificio).Count;

            if (residentes > 0)
                throw new InvalidOperationException(
                    "No se puede eliminar: el edificio tiene " + residentes +
                    " residente(s) asociados.");

            Edificio edificio = RepositorioFactory.Instancia.Edificios.ObtenerPorId(idEdificio);

            RepositorioFactory.Instancia.Edificios.Eliminar(idEdificio);

            _historial.Grabar(TipoOperacion.Baja,
               "Eliminación del edificio '" + (edificio != null ? edificio.Nombre : "?") + "'");
        }

        private bool ExisteNombre(string nombre, int idExcluido)
        {
            return RepositorioFactory.Instancia.Edificios
                .ObtenerTodos()
                .Any(e => e.Id != idExcluido &&
                          string.Equals(e.Nombre, nombre.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        private void Validar(string nombre, string direccion, string localidad, int cantLockers)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new InvalidOperationException("El nombre del edificio es obligatorio.");

            if (string.IsNullOrWhiteSpace(direccion))
                throw new InvalidOperationException("La dirección es obligatoria.");

            if (string.IsNullOrWhiteSpace(localidad))
                throw new InvalidOperationException("La localidad es obligatoria.");

            if (cantLockers < 0)
                throw new InvalidOperationException("La cantidad de lockers no puede ser negativa.");
        }
    }
}
