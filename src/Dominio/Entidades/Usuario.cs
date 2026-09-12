using LockersInteligentes.Dominio.Arquitectura;

namespace LockersInteligentes.Dominio.Entidades
{
    public class Usuario : EntidadBase
    {
        public Usuario()
        {
            Activo = true;
        }

        public string NombreUsuario { get; set; }

        public string Nombre { get; set; }
        public string Apellido { get; set; }

        public string PasswordHash { get; set; }

        public string PasswordSalt { get; set; }

        public Rol Rol { get; set; }

        public Idioma IdiomaPreferido { get; set; }

        public bool Activo { get; set; }

        public bool EsAdministrador()
        {
            return Rol != null && Rol.EsAdministrador();
        }

        public string NombreCompleto()
        {
            return Apellido + ", " + Nombre;
        }

        public override string ToString()
        {
            return NombreUsuario + " (" + (Rol != null ? Rol.Nombre : "sin rol") + ")";
        }
    }
}