using System;
using LockersInteligentes.Dominio.Enums;

namespace LockersInteligentes.Dominio.Entidades
{
    public class Historial : EntidadBase
    {
        public Historial()
        {
            FechaOperacion = DateTime.Now;
        }
        public string Actor { get; set; }

        public TipoOperacion TipoDeOperacion { get; set; }
        public DateTime FechaOperacion { get; set; }

        public Locker LockerUtilizado { get; set; }
        public OrdenDeEntrega OrdenDeEntrega { get; set; }

        public string Detalle { get; set; }

        public override string ToString()
        {
            return FechaOperacion.ToString("dd/MM/yyyy HH:mm") + " - " + Actor + " - " + TipoDeOperacion;
        }
    }
}
