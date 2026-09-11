namespace LockersInteligentes.Dominio.Entidades
{
    public class Administrador : EntidadBase
    {
        public string Usuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string Idioma { get; set; }

        public string NombreCompleto()
        {
            return Apellido + ", " + Nombre;
        }
    }
}
