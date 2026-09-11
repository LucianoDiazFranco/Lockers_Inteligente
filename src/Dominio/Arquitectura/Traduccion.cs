using LockersInteligentes.Dominio.Entidades;

namespace LockersInteligentes.Dominio.Arquitectura
{
    public class Traduccion : EntidadBase
    {
        public Idioma Idioma { get; set; }

        public string Clave { get; set; }

        public string Texto { get; set; }

        public override string ToString()
        {
            return Clave + " = " + Texto;
        }
    }
}
