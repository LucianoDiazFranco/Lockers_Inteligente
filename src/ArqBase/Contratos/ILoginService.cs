using LockersInteligentes.ArqBase.Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockersInteligentes.ArqBase.Contratos
{
    public interface ILoginService
    {
        ResultadoLogin IniciarSesion(string nombreUsuario, string password);
        void CerrarSesion();
    }
}
