using System;
using LockersInteligentes.Dominio.Enums;

namespace LockersInteligentes.Dominio.Entidades
{

    public class CodigoAcceso : EntidadBase
    {
        public const int HorasDeVigencia = 48;

        public CodigoAcceso()
        {
            FechaGeneracion = DateTime.Now;
            Usado = false;
        }

        public OrdenDeEntrega OrdenDeEntrega { get; set; }
        public TipoCodigo TipoCodigo { get; set; }

        public string PinHash { get; set; }

        public string PinSalt { get; set; }

        public DateTime FechaGeneracion { get; set; }
        public bool Usado { get; set; }

        public DateTime FechaExpiracion
        {
            get { return FechaGeneracion.AddHours(HorasDeVigencia); }
        }

        public bool EstaVencido()
        {
            return DateTime.Now > FechaExpiracion;
        }

        public bool EsValido()
        {
            return !Usado && !EstaVencido();
        }

        public bool CoincideCon(string hashIngresado)
        {
            if (!EsValido() || string.IsNullOrWhiteSpace(hashIngresado))
                return false;

            return string.Equals(PinHash, hashIngresado, StringComparison.Ordinal);
        }

        public void MarcarUsado()
        {
            if (Usado)
                throw new InvalidOperationException(
                    "El codigo de " + TipoCodigo + " ya fue utilizado.");

            if (EstaVencido())
                throw new InvalidOperationException(
                    "El codigo de " + TipoCodigo + " esta vencido desde " + FechaExpiracion + ".");

            Usado = true;
        }

        public override string ToString()
        {
            return "Codigo " + TipoCodigo + " (orden #" + (OrdenDeEntrega != null ? OrdenDeEntrega.Id : 0) + ")";
        }
    }
}
