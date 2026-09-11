using System;

namespace LockersInteligentes.Dominio.Entidades
{
    public class Reporte : EntidadBase
    {
        public Reporte()
        {
            FechaGeneracion = DateTime.Now;
        }

        public Edificio Edificio { get; set; }

        public DateTime FechaGeneracion { get; set; }

        public string Periodo { get; set; }

        public decimal PorcentajeOcupacionLocker { get; set; }

        public decimal PorcentajePertenenciaLocker { get; set; }

        public int PaqueteVencido { get; set; }

        public string RankingRepartidores { get; set; }

        public string RankingTopLockers { get; set; }

        public override string ToString()
        {
            return "Reporte #" + Id + " - " + Periodo;
        }
    }
}
