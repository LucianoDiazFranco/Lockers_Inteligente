using System;
using System.Collections.Generic;
using System.Linq;
using LockersInteligentes.Dominio.Enums;

namespace LockersInteligentes.Dominio.Entidades
{
    public class OrdenDeEntrega : EntidadBase
    {
        public OrdenDeEntrega()
        {
            Codigos = new List<CodigoAcceso>();
            Estado = EstadoOrden.Reservada;
            FechaReserva = DateTime.Now;
        }

        public string Descripcion { get; set; }
        public EstadoOrden Estado { get; set; }
        public TamanioPaquete TamanioPaquete { get; set; }
        public DateTime FechaReserva { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public DateTime? FechaRetiro { get; set; }

        public Locker LockerAsignado { get; set; }
        public Residente ResidenteAsignado { get; set; }
        public Repartidor RepartidorAsignado { get; set; }

        public const int DiasParaRetirar = 7;

        public IList<CodigoAcceso> Codigos { get; private set; }

        public void MarcarEntregada()
        {
            if (Estado != EstadoOrden.Reservada)
                throw new InvalidOperationException(
                    "Solo una orden Reservada puede marcarse como Entregada. Estado actual: " + Estado + ".");

            Estado = EstadoOrden.Entregada;
            FechaEntrega = DateTime.Now;
        }

        public void MarcarRetirada()
        {
            if (Estado != EstadoOrden.Entregada)
                throw new InvalidOperationException(
                    "Solo una orden Entregada puede marcarse como Retirada. Estado actual: " + Estado + ".");

            Estado = EstadoOrden.Retirada;
            FechaRetiro = DateTime.Now;
        }

        public void AgregarCodigo(CodigoAcceso codigo)
        {
            if (codigo == null)
                throw new ArgumentNullException("codigo");

            codigo.OrdenDeEntrega = this;
            Codigos.Add(codigo);
        }

        public CodigoAcceso ObtenerCodigoVigente(TipoCodigo tipo)
        {
            return Codigos.FirstOrDefault(c => c.TipoCodigo == tipo && c.EsValido());
        }

        public override string ToString()
        {
            return "Orden #" + Id + " - " + Estado + " (" + TamanioPaquete + ")";
        }
        public bool EstaVencida()
        {
            if (Estado != EstadoOrden.Entregada || !FechaEntrega.HasValue)
                return false;

            return DateTime.Now > FechaEntrega.Value.AddDays(DiasParaRetirar);
        }
        public int? DiasRestantesParaRetiro()
        {
            if (Estado != EstadoOrden.Entregada || !FechaEntrega.HasValue)
                return null;

            TimeSpan restante = FechaEntrega.Value.AddDays(DiasParaRetirar) - DateTime.Now;
            return (int)Math.Ceiling(restante.TotalDays);
        }
        public bool ReservaExpirada()
        {
            if (Estado != EstadoOrden.Reservada)
                return false;

            return DateTime.Now > FechaReserva.AddHours(CodigoAcceso.HorasDeVigencia);
        }

        public double HorasDesdeReserva()
        {
            return (DateTime.Now - FechaReserva).TotalHours;
        }
    }
}
