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