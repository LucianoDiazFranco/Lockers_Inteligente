

namespace LockersInteligentes.ArqBase.Entidades
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
