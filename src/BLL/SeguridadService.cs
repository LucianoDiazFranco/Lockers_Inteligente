using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LockersInteligentes.ArqBase.Servicios;

namespace LockersInteligentes.BLL
{
    public class SeguridadService
    {
        public void GenerarCredenciales(string password, out string salt, out string hash)
        {
            salt = EncriptadorService.GenerarSalt();
            hash = EncriptadorService.Encriptar(password, salt);
        }
    }
}
