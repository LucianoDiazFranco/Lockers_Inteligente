using System;
using LockersInteligentes.ArqBase.Enums;

namespace LockersInteligentes.ArqBase.Entidades
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

        public int? IdLockerUtilizado { get; set; }
        public int? IdOrdenDeEntrega { get; set; }
        public int? NumeroLocker { get; set; }
        public string Detalle { get; set; }

        public override string ToString()
        {
            return FechaOperacion.ToString("dd/MM/yyyy HH:mm") + " - " + Actor + " - " + TipoDeOperacion;
        }
    }
}
