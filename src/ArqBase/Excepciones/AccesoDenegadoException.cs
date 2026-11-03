using LockersInteligentes.ArqBase.Excepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockersInteligentes.ArqBaseExcepciones
{
    public class AccesoDenegadoException : ExcepcionNegocio
    {
        public AccesoDenegadoException(string mensaje) : base(mensaje)
        {
        }
    }
}
