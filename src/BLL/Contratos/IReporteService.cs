using LockersInteligentes.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockersInteligentes.BLL.Contratos
{
    public interface IReporteService
    {
        IList<Edificio> ListarEdificios();
        IList<Reporte> ListarGenerados();
        Reporte Generar(int idEdificio, DateTime desde, DateTime hasta);
    }
}
