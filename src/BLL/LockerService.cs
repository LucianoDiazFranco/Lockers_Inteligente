using System;
using System.Collections.Generic;
using System.Linq;
using LockersInteligentes.DAL.Repositorios;
using LockersInteligentes.Dominio.Entidades;
using LockersInteligentes.Dominio.Enums;

namespace LockersInteligentes.BLL
{
    public class LockerService
    {
        public IList<Edificio> ListarEdificios()
        {
            ValidarSesion();
            return RepositorioFactory.Instancia.Edificios.ObtenerTodos();
        }
        public IList<EstadoLockerVista> ObtenerEstado(int idEdificio)
        {
            ValidarSesion();

            IList<Locker> lockers = RepositorioFactory.Instancia.Lockers
                                                      .ObtenerPorEdificio(idEdificio);

            IDictionary<int, OrdenDeEntrega> ordenes = RepositorioFactory.Instancia.Ordenes
                                                       .ObtenerActivasPorEdificio(idEdificio);

            List<EstadoLockerVista> vista = new List<EstadoLockerVista>();

            foreach (Locker locker in lockers)
            {
                EstadoLockerVista item = new EstadoLockerVista();
                item.Locker = locker;

                OrdenDeEntrega orden;
                item.OrdenActiva = ordenes.TryGetValue(locker.Id, out orden) ? orden : null;

                vista.Add(item);
            }

            return vista;
        }
        public IList<string> ListarSectores(int idEdificio)
        {
            ValidarSesion();

            return RepositorioFactory.Instancia.Lockers
                .ObtenerPorEdificio(idEdificio)
                .Select(l => l.GrupoLocker ?? "(sin sector)")
                .Distinct()
                .OrderBy(s => s)
                .ToList();
        }

        public ResumenLockers Resumir(IEnumerable<EstadoLockerVista> vista)
        {
            ResumenLockers resumen = new ResumenLockers();

            foreach (EstadoLockerVista item in vista)
            {
                resumen.Total++;

                switch (item.Locker.Estado)
                {
                    case EstadoLocker.Libre: resumen.Libres++; break;
                    case EstadoLocker.Reservado: resumen.Reservados++; break;
                    case EstadoLocker.Ocupado: resumen.Ocupados++; break;
                    case EstadoLocker.FueraDeServicio: resumen.FueraDeServicio++; break;
                }

                if (item.Vencido)
                    resumen.Vencidos++;
            }

            return resumen;
        }

        public IList<Locker> ListarPorEdificio(int idEdificio)
        {
            GestorSesion.Instancia.ValidarRolAdministrador();
            return RepositorioFactory.Instancia.Lockers.ObtenerPorEdificio(idEdificio);
        }

        public Locker Crear(int idEdificio, int numero, string sector, string descripcion,
                            TamanioPaquete tamanio)
        {
            GestorSesion.Instancia.ValidarRolAdministrador();
            ValidarDatos(numero);

            if (ExisteNumero(idEdificio, numero, 0))
                throw new InvalidOperationException(
                    "Ya existe el locker N° " + numero + " en ese edificio.");

            Edificio edificio = RepositorioFactory.Instancia.Edificios.ObtenerPorId(idEdificio);

            if (edificio == null)
                throw new InvalidOperationException("El edificio seleccionado no existe.");

            Locker locker = new Locker();
            locker.Edificio = edificio;
            locker.Numero = numero;
            locker.GrupoLocker = string.IsNullOrWhiteSpace(sector) ? null : sector.Trim();
            locker.Descripcion = string.IsNullOrWhiteSpace(descripcion) ? null : descripcion.Trim();
            locker.Tamanio = tamanio;
            locker.Estado = EstadoLocker.Libre;   // un locker nuevo siempre nace libre

            RepositorioFactory.Instancia.Lockers.Insertar(locker);
            return locker;
        }

        /// <summary>
        /// Modifica los datos del locker. El tamaño NO se puede cambiar si el locker
        /// está ocupado: adentro hay un paquete que se aceptó bajo esa medida.
        /// </summary>
        public void Modificar(Locker locker)
        {
            GestorSesion.Instancia.ValidarRolAdministrador();

            if (locker == null)
                throw new ArgumentNullException("locker");

            ValidarDatos(locker.Numero);

            if (ExisteNumero(locker.Edificio.Id, locker.Numero, locker.Id))
                throw new InvalidOperationException(
                    "Ya existe otro locker N° " + locker.Numero + " en ese edificio.");

            RepositorioFactory.Instancia.Lockers.Actualizar(locker);
        }

        /// <summary>
        /// Cambia el estado manualmente. Solo permite pasar de Libre a FueraDeServicio
        /// y viceversa: los estados Reservado y Ocupado los maneja el ciclo del
        /// paquete, y tocarlos a mano dejaría órdenes apuntando a lockers liberados.
        /// </summary>
        public void CambiarEstado(int idLocker, EstadoLocker nuevoEstado)
        {
            GestorSesion.Instancia.ValidarRolAdministrador();

            Locker locker = RepositorioFactory.Instancia.Lockers.ObtenerPorId(idLocker);

            if (locker == null)
                throw new InvalidOperationException("El locker no existe.");

            bool origenValido = locker.Estado == EstadoLocker.Libre ||
                                locker.Estado == EstadoLocker.FueraDeServicio;

            bool destinoValido = nuevoEstado == EstadoLocker.Libre ||
                                 nuevoEstado == EstadoLocker.FueraDeServicio;

            if (!origenValido || !destinoValido)
                throw new InvalidOperationException(
                    "Solo se puede alternar entre Libre y Fuera de servicio. " +
                    "El locker está " + locker.Estado + ": su estado lo controla el ciclo del paquete.");

            locker.Estado = nuevoEstado;
            RepositorioFactory.Instancia.Lockers.Actualizar(locker);
        }

        /// <summary>
        /// Baja física. Solo si el locker está libre y nunca tuvo órdenes: la FK
        /// desde OrdenDeEntrega no tiene cascada, así que borrarlo con historial
        /// fallaría en la base con un error poco claro.
        /// </summary>
        public void Eliminar(int idLocker)
        {
            GestorSesion.Instancia.ValidarRolAdministrador();

            Locker locker = RepositorioFactory.Instancia.Lockers.ObtenerPorId(idLocker);

            if (locker == null)
                throw new InvalidOperationException("El locker no existe.");

            if (locker.Estado != EstadoLocker.Libre && locker.Estado != EstadoLocker.FueraDeServicio)
                throw new InvalidOperationException(
                    "No se puede eliminar un locker " + locker.Estado + ".");

            if (RepositorioFactory.Instancia.Lockers.TieneOrdenes(idLocker))
                throw new InvalidOperationException(
                    "No se puede eliminar: el locker tiene órdenes registradas en el historial. " +
                    "Ponelo Fuera de servicio en lugar de eliminarlo.");

            RepositorioFactory.Instancia.Lockers.Eliminar(idLocker);
        }

        private bool ExisteNumero(int idEdificio, int numero, int idExcluido)
        {
            return RepositorioFactory.Instancia.Lockers
                .ObtenerPorEdificio(idEdificio)
                .Any(l => l.Numero == numero && l.Id != idExcluido);
        }

        private void ValidarDatos(int numero)
        {
            if (numero <= 0)
                throw new InvalidOperationException("El número de locker debe ser mayor a cero.");
        }

        private void ValidarSesion()
        {
            if (!GestorSesion.Instancia.HaySesionActiva)
                throw new InvalidOperationException("No hay una sesión activa.");
        }
    }
    public class ResumenLockers
    {
        public int Total { get; set; }
        public int Libres { get; set; }
        public int Reservados { get; set; }
        public int Ocupados { get; set; }
        public int FueraDeServicio { get; set; }
        public int Vencidos { get; set; }

        public int PorcentajeOcupacion()
        {
            if (Total == 0)
                return 0;

            return (int)Math.Round((Reservados + Ocupados) * 100.0 / Total);
        }
    }
}