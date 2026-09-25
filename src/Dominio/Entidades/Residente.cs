namespace LockersInteligentes.Dominio.Entidades
{

    public class Residente : EntidadBase
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Dni { get; set; }
        public string Correo { get; set; }
        public string Piso { get; set; }
        public string Telefono { get; set; }

        public bool Activo { get; set; }

        public Edificio Edificio { get; set; }

        public string NombreCompleto()
        {
            return Apellido + ", " + Nombre;
        }

        public override string ToString()
        {
            return NombreCompleto() + " (Piso " + Piso + ")";
        }
        public Residente()
        {
            Activo = true;
        }
    }
}
