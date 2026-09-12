using System;

namespace LockersInteligentes.Dominio.Entidades
{
   public class Rol : EntidadBase
    {
        public const string NombreAdministrador = "Administrador";
        public const string NombreOperador = "Operador";

        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public bool EsAdministrador()
        {
            return string.Equals(Nombre, NombreAdministrador, StringComparison.OrdinalIgnoreCase);
        }

        public override string ToString()
        {
            return Nombre;
        }
    }
}