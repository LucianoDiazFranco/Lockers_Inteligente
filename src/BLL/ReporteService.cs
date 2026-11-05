using LockersInteligentes.DAL.Repositorios;
using LockersInteligentes.Dominio.Entidades;
using LockersInteligentes.Dominio.Enums;
using LockersInteligentes.ArqBase.Enums;
using LockersInteligentes.ArqBase.Logica;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LockersInteligentes.BLL
{
    public class ReporteService
    {
        private readonly HistorialService _historial = new HistorialService();

        public IList<Edificio> ListarEdificios()
        {
            GestorSesion.Instancia.ValidarPermiso(PermisoService.Patentes.VerReportes);
            return RepositorioFactory.Instancia.Edificios.ObtenerTodos();
        }

        public IList<Reporte> ListarGenerados()
        {
            GestorSesion.Instancia.ValidarPermiso(PermisoService.Patentes.VerReportes);

            return RepositorioFactory.Instancia.Reportes
                .ObtenerTodos()
                .OrderByDescending(r => r.FechaGeneracion)
                .ToList();
        }

        /// <summary>Calcula las métricas y persiste el reporte.</summary>
        public Reporte Generar(int idEdificio, DateTime desde, DateTime hasta)
        {
            GestorSesion.Instancia.ValidarPermiso(PermisoService.Patentes.VerReportes);

            if (hasta < desde)
                throw new InvalidOperationException("La fecha final no puede ser anterior a la inicial.");

            Edificio edificio = RepositorioFactory.Instancia.Edificios.ObtenerPorId(idEdificio);

            if (edificio == null)
                throw new InvalidOperationException("El edificio seleccionado no existe.");

            DateTime finDelDia = hasta.Date.AddDays(1).AddSeconds(-1);

            IList<Locker> lockers = RepositorioFactory.Instancia.Lockers.ObtenerPorEdificio(idEdificio);
            IList<OrdenDeEntrega> ordenes = RepositorioFactory.Instancia.Ordenes
                .ObtenerPorEdificioYPeriodo(idEdificio, desde.Date, finDelDia);

            Reporte reporte = new Reporte();
            reporte.Edificio = edificio;
            reporte.FechaGeneracion = DateTime.Now;
            reporte.Periodo = desde.ToString("dd/MM/yyyy") + " - " + hasta.ToString("dd/MM/yyyy");
            reporte.PorcentajeOcupacionLocker = CalcularOcupacion(lockers);
            reporte.PorcentajePertenenciaLocker = CalcularPertenencia(lockers, ordenes);
            reporte.PaqueteVencido = ordenes.Count(o => o.EstaVencida());
            reporte.RankingRepartidores = ArmarRankingRepartidores(ordenes);
            reporte.RankingTopLockers = ArmarRankingLockers(ordenes);

            RepositorioFactory.Instancia.Reportes.Insertar(reporte);

            _historial.Grabar(TipoOperacion.Consulta,
                "Generación de reporte de " + edificio.Nombre + " (" + reporte.Periodo + ")");

            return reporte;
        }

        /// <summary>
        /// Ocupación actual: lockers reservados u ocupados sobre el total.
        /// Es una medición del momento, no del período: refleja cómo está el
        /// edificio hoy, que es lo que interesa para decidir si hacen falta más.
        /// </summary>
        private decimal CalcularOcupacion(IList<Locker> lockers)
        {
            if (lockers.Count == 0)
                return 0;

            int usados = lockers.Count(l => l.Estado == EstadoLocker.Reservado ||
                                            l.Estado == EstadoLocker.Ocupado);

            return Math.Round(usados * 100m / lockers.Count, 2);
        }

        /// <summary>
        /// Porcentaje de lockers que tuvieron al menos un uso en el período.
        /// Un valor bajo con ocupación alta indica que siempre se usan los mismos
        /// casilleros y el resto está desaprovechado.
        /// </summary>
        private decimal CalcularPertenencia(IList<Locker> lockers, IList<OrdenDeEntrega> ordenes)
        {
            if (lockers.Count == 0)
                return 0;

            int distintos = ordenes.Select(o => o.LockerAsignado.Id).Distinct().Count();

            return Math.Round(distintos * 100m / lockers.Count, 2);
        }

        private string ArmarRankingRepartidores(IList<OrdenDeEntrega> ordenes)
        {
            var ranking = ordenes
                .Where(o => o.RepartidorAsignado != null)
                .GroupBy(o => o.RepartidorAsignado.NombreCompleto())
                .Select(g => new { Nombre = g.Key, Cantidad = g.Count() })
                .OrderByDescending(x => x.Cantidad)
                .Take(5)
                .ToList();

            if (ranking.Count == 0)
                return "Sin entregas registradas en el período.";

            return string.Join(Environment.NewLine,
                ranking.Select((x, i) => (i + 1) + ". " + x.Nombre + " - " + x.Cantidad + " entrega(s)"));
        }

        private string ArmarRankingLockers(IList<OrdenDeEntrega> ordenes)
        {
            var ranking = ordenes
                .GroupBy(o => o.LockerAsignado.Numero + " (" + o.LockerAsignado.GrupoLocker + ")")
                .Select(g => new { Locker = g.Key, Cantidad = g.Count() })
                .OrderByDescending(x => x.Cantidad)
                .Take(5)
                .ToList();

            if (ranking.Count == 0)
                return "Sin uso registrado en el período.";

            return string.Join(Environment.NewLine,
                ranking.Select((x, i) => (i + 1) + ". Locker " + x.Locker + " - " + x.Cantidad + " uso(s)"));
        }


    }
}
