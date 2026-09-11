using System.Collections.Generic;

namespace LockersInteligentes.Dominio.Entidades
{

    public class Edificio : EntidadBase
    {
        public Edificio()
        {
            Lockers = new List<Locker>();
            Residentes = new List<Residente>();
        }

        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Localidad { get; set; }
        public string TelefonoContacto { get; set; }

        public int CantLockers { get; set; }

        public IList<Locker> Lockers { get; private set; }

        public IList<Residente> Residentes { get; private set; }
    }
}
