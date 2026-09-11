using System.Collections.Generic;
using LockersInteligentes.Dominio.Entidades;

namespace LockersInteligentes.Dominio.Arquitectura
{
    public class Idioma : EntidadBase
    {
        public Idioma()
        {
            Traducciones = new List<Traduccion>();
            EsPredeterminado = false;
        }

        public string Nombre { get; set; }
        public string Codigo { get; set; }

        public bool EsPredeterminado { get; set; }

        public IList<Traduccion> Traducciones { get; private set; }

        public override string ToString()
        {
            return Nombre + " (" + Codigo + ")";
        }
    }
}
