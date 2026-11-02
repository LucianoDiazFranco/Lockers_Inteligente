using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockersInteligentes.Dominio.Excepciones
{
    public class AccesoDenegadoException : ExcepcionNegocio
    {
        public AccesoDenegadoException(string mensaje) : base(mensaje)
        {
        }
    }
}
