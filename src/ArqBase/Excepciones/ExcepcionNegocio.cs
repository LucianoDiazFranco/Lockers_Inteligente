using System;
using LockersInteligentes.ArqBase.Excepciones;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockersInteligentes.ArqBase.Excepciones
{
    public class ExcepcionNegocio : Exception
    {
        public ExcepcionNegocio(string mensaje) : base(mensaje)
        {
        }

        public ExcepcionNegocio(string mensaje, Exception interna) : base(mensaje, interna)
        {
        }
    }
}
