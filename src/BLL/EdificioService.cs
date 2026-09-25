using LockersInteligentes.DAL.Repositorios;
using LockersInteligentes.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockersInteligentes.BLL
{
    public class EdificioService
    {
        public IList<Edificio> Listar()
        {
            GestorSesion.Instancia.ValidarRolAdministrador();
            return RepositorioFactory.Instancia.Edificios.ObtenerTodos();
        }

        public Edificio Crear(string nombre, string direccion, string localidad,
                              string telefono, int cantLockers)
        {
            GestorSesion.Instancia.ValidarRolAdministrador();
            Validar(nombre, direccion, localidad, cantLockers);

            if (ExisteNombre(nombre, 0))
                throw new InvalidOperationException(
                    "Ya existe un edificio llamado '" + nombre.Trim() + "'.");

            Edificio edificio = new Edificio();
            edificio.Nombre = nombre.Trim();
            edificio.Direccion = direccion.Trim();
            edificio.Localidad = localidad.Trim();
            edificio.TelefonoContacto = string.IsNullOrWhiteSpace(telefono) ? null : telefono.Trim();
            edificio.CantLockers = cantLockers;

            RepositorioFactory.Instancia.Edificios.Insertar(edificio);
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
        }

        /// <summary>
        /// Baja física, pero solo si el edificio está vacío.
        ///
        /// La relación con Locker es composición y la FK tiene ON DELETE CASCADE, así
        /// que borrar el edificio se llevaría puestos todos sus lockers, y con ellos
        /// las órdenes que los referencian. Por eso se verifica antes: es preferible
        /// un mensaje claro a una cascada silenciosa que destruye historial.
        /// </summary>
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

            RepositorioFactory.Instancia.Edificios.Eliminar(idEdificio);
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
