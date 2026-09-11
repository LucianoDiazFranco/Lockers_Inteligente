using System;
using LockersInteligentes.Dominio.Enums;

namespace LockersInteligentes.Dominio.Entidades
{
    public class Locker : EntidadBase
    {
        public Locker()
        {
            Estado = EstadoLocker.Libre;
        }

        public int Numero { get; set; }
        public string Descripcion { get; set; }

        public string GrupLocker { get; set; }

        public TamanioPaquete Tamanio { get; set; }
        public EstadoLocker Estado { get; set; }

        public Edificio Edificio { get; set; }

        public bool EstaDisponible()
        {
            return Estado == EstadoLocker.Libre;
        }

        public bool PuedeAlojar(TamanioPaquete tamanioPaquete)
        {
            return EstaDisponible() && Tamanio >= tamanioPaquete;
        }

        public void Reservar()
        {
            if (Estado != EstadoLocker.Libre)
                throw new InvalidOperationException(
                    "Solo se puede reservar un locker Libre. Estado actual: " + Estado + ".");

            Estado = EstadoLocker.Reservado;
        }

        public void Ocupar()
        {
            if (Estado != EstadoLocker.Reservado)
                throw new InvalidOperationException(
                    "Solo se puede ocupar un locker Reservado. Estado actual: " + Estado + ".");

            Estado = EstadoLocker.Ocupado;
        }

        public void Liberar()
        {
            if (Estado != EstadoLocker.Ocupado && Estado != EstadoLocker.Reservado)
                throw new InvalidOperationException(
                    "Solo se puede liberar un locker Ocupado o Reservado. Estado actual: " + Estado + ".");

            Estado = EstadoLocker.Libre;
        }

        public void PonerFueraDeServicio()
        {
            if (Estado == EstadoLocker.Ocupado)
                throw new InvalidOperationException(
                    "No se puede dar de baja un locker con un paquete adentro.");

            Estado = EstadoLocker.FueraDeServicio;
        }

        public override string ToString()
        {
            return "Locker " + Numero + " (" + Tamanio + ")";
        }
    }
}
