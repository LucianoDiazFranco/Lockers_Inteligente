using LockersInteligentes.DAL.Repositorios;
using LockersInteligentes.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockersInteligentes.BLL
{
    public class ResidenteService
    {
        public IList<Residente> ListarPorEdificio(int idEdificio)
        {
            GestorSesion.Instancia.ValidarRolAdministrador();
            return RepositorioFactory.Instancia.Residentes.ObtenerPorEdificio(idEdificio);
        }

        /// <summary>
        /// Solo los activos. Lo usa el combo de Reservar Locker: no tiene sentido
        /// ofrecer un residente que ya no vive en el edificio.
        /// </summary>
        public IList<Residente> ListarActivos()
        {
            if (!GestorSesion.Instancia.HaySesionActiva)
                throw new InvalidOperationException("No hay una sesión activa.");

            return RepositorioFactory.Instancia.Residentes
                .ObtenerTodos()
                .Where(r => r.Activo)
                .ToList();
        }

        public Residente Crear(int idEdificio, string nombre, string apellido, string dni,
                               string correo, string piso, string telefono)
        {
            GestorSesion.Instancia.ValidarRolAdministrador();
            Validar(nombre, apellido, dni, correo);

            if (RepositorioFactory.Instancia.Residentes.ExisteDni(dni.Trim(), 0))
                throw new InvalidOperationException("Ya existe un residente con el DNI " + dni.Trim() + ".");

            Edificio edificio = RepositorioFactory.Instancia.Edificios.ObtenerPorId(idEdificio);

            if (edificio == null)
                throw new InvalidOperationException("El edificio seleccionado no existe.");

            Residente residente = new Residente();
            residente.Edificio = edificio;
            residente.Nombre = nombre.Trim();
            residente.Apellido = apellido.Trim();
            residente.Dni = dni.Trim();
            residente.Correo = correo.Trim();
            residente.Piso = string.IsNullOrWhiteSpace(piso) ? null : piso.Trim();
            residente.Telefono = string.IsNullOrWhiteSpace(telefono) ? null : telefono.Trim();
            residente.Activo = true;

            RepositorioFactory.Instancia.Residentes.Insertar(residente);
            return residente;
        }

        public void Modificar(Residente residente)
        {
            GestorSesion.Instancia.ValidarRolAdministrador();

            if (residente == null)
                throw new ArgumentNullException("residente");

            Validar(residente.Nombre, residente.Apellido, residente.Dni, residente.Correo);

            if (RepositorioFactory.Instancia.Residentes.ExisteDni(residente.Dni.Trim(), residente.Id))
                throw new InvalidOperationException(
                    "Ya existe otro residente con el DNI " + residente.Dni.Trim() + ".");

            RepositorioFactory.Instancia.Residentes.Actualizar(residente);
        }

        /// <summary>
        /// Baja lógica, y solo si no tiene paquetes pendientes: desactivar a alguien
        /// que tiene un paquete esperando en un locker dejaría ese locker ocupado
        /// sin nadie habilitado para retirarlo.
        /// </summary>
        public void Desactivar(int idResidente)
        {
            GestorSesion.Instancia.ValidarRolAdministrador();

            int pendientes = RepositorioFactory.Instancia.Ordenes.ContarActivasPorResidente(idResidente);

            if (pendientes > 0)
                throw new InvalidOperationException(
                    "No se puede dar de baja: el residente tiene " + pendientes +
                    " paquete(s) pendiente(s) de entrega o retiro.");

            RepositorioFactory.Instancia.Residentes.Eliminar(idResidente);
        }

        public void Reactivar(int idResidente)
        {
            GestorSesion.Instancia.ValidarRolAdministrador();

            Residente residente = RepositorioFactory.Instancia.Residentes.ObtenerPorId(idResidente);

            if (residente == null)
                throw new InvalidOperationException("El residente no existe.");

            residente.Activo = true;
            RepositorioFactory.Instancia.Residentes.Actualizar(residente);
        }

        private void Validar(string nombre, string apellido, string dni, string correo)
        {
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellido))
                throw new InvalidOperationException("Nombre y apellido son obligatorios.");

            if (string.IsNullOrWhiteSpace(dni))
                throw new InvalidOperationException("El DNI es obligatorio.");

            if (!dni.Trim().All(char.IsDigit))
                throw new InvalidOperationException("El DNI debe contener solo números.");

            // El correo es obligatorio: es el canal por el que viajan los PIN.
            if (string.IsNullOrWhiteSpace(correo) || !correo.Contains("@"))
                throw new InvalidOperationException("Ingresá un correo electrónico válido.");
        }
    }
}
