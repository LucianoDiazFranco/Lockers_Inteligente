using LockersInteligentes.DAL.Repositorios;
using LockersInteligentes.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockersInteligentes.BLL
{
    public class RepartidorService
    {
        public IList<Repartidor> Listar()
        {
            GestorSesion.Instancia.ValidarRolAdministrador();
            return RepositorioFactory.Instancia.Repartidores.ObtenerTodos();
        }

        /// <summary>Solo los activos. Lo usa el combo de Registrar Entrega.</summary>
        public IList<Repartidor> ListarActivos()
        {
            if (!GestorSesion.Instancia.HaySesionActiva)
                throw new InvalidOperationException("No hay una sesión activa.");

            return RepositorioFactory.Instancia.Repartidores
                .ObtenerTodos()
                .Where(r => r.Activo)
                .ToList();
        }

        public Repartidor Crear(string nombre, string apellido, string dni,
                                string empresa, string telefono)
        {
            GestorSesion.Instancia.ValidarRolAdministrador();
            Validar(nombre, apellido, dni);

            if (RepositorioFactory.Instancia.Repartidores.ExisteDni(dni.Trim(), 0))
                throw new InvalidOperationException("Ya existe un repartidor con el DNI " + dni.Trim() + ".");

            Repartidor repartidor = new Repartidor();
            repartidor.Nombre = nombre.Trim();
            repartidor.Apellido = apellido.Trim();
            repartidor.Dni = dni.Trim();
            repartidor.Empresa = string.IsNullOrWhiteSpace(empresa) ? null : empresa.Trim();
            repartidor.Telefono = string.IsNullOrWhiteSpace(telefono) ? null : telefono.Trim();
            repartidor.Activo = true;

            RepositorioFactory.Instancia.Repartidores.Insertar(repartidor);
            return repartidor;
        }

        public void Modificar(Repartidor repartidor)
        {
            GestorSesion.Instancia.ValidarRolAdministrador();

            if (repartidor == null)
                throw new ArgumentNullException("repartidor");

            Validar(repartidor.Nombre, repartidor.Apellido, repartidor.Dni);

            if (RepositorioFactory.Instancia.Repartidores.ExisteDni(repartidor.Dni.Trim(), repartidor.Id))
                throw new InvalidOperationException(
                    "Ya existe otro repartidor con el DNI " + repartidor.Dni.Trim() + ".");

            RepositorioFactory.Instancia.Repartidores.Actualizar(repartidor);
        }

        /// <summary>
        /// Baja lógica. No se elimina la fila porque el repartidor figura como
        /// responsable de las entregas que registró.
        /// </summary>
        public void Desactivar(int idRepartidor)
        {
            GestorSesion.Instancia.ValidarRolAdministrador();
            RepositorioFactory.Instancia.Repartidores.Eliminar(idRepartidor);
        }

        public void Reactivar(int idRepartidor)
        {
            GestorSesion.Instancia.ValidarRolAdministrador();

            Repartidor repartidor = RepositorioFactory.Instancia.Repartidores.ObtenerPorId(idRepartidor);

            if (repartidor == null)
                throw new InvalidOperationException("El repartidor no existe.");

            repartidor.Activo = true;
            RepositorioFactory.Instancia.Repartidores.Actualizar(repartidor);
        }

        private void Validar(string nombre, string apellido, string dni)
        {
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellido))
                throw new InvalidOperationException("Nombre y apellido son obligatorios.");

            if (string.IsNullOrWhiteSpace(dni))
                throw new InvalidOperationException("El DNI es obligatorio.");

            if (!dni.Trim().All(char.IsDigit))
                throw new InvalidOperationException("El DNI debe contener solo números.");
        }
    }
}
