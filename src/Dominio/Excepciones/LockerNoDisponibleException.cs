using LockersInteligentes.Dominio.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockersInteligentes.Dominio.Excepciones
{
    public class LockerNoDisponibleException : ExcepcionNegocio
    {
        public TamanioPaquete TamanioSolicitado { get; private set; }
        public string NombreEdificio { get; private set; }

        public LockerNoDisponibleException(TamanioPaquete tamanio, string edificio)
            : base("No hay lockers libres de tamaño " + tamanio + " o superior en " + edificio + ".")
        {
            TamanioSolicitado = tamanio;
            NombreEdificio = edificio;
        }
    }
}
