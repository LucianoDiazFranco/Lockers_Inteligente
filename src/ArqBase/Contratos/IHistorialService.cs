using LockersInteligentes.ArqBase.Entidades;
using LockersInteligentes.ArqBase.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockersInteligentes.ArqBase.Contratos
{
    public interface IHistorialService
    {
        void Grabar(TipoOperacion tipo, string detalle, int? idLocker = null, int? idOrden = null);
        IList<Historial> Buscar(DateTime? desde, DateTime? hasta, string actor, TipoOperacion? tipo);
    }
}
