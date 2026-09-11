namespace LockersInteligentes.Dominio.Entidades
{
    public class Repartidor : EntidadBase
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Dni { get; set; }
        public string Empresa { get; set; }
        public string Telefono { get; set; }

        public string NombreCompleto()
        {
            return Apellido + ", " + Nombre;
        }

        public override string ToString()
        {
            return NombreCompleto() + " - " + Empresa;
        }
    }
}
