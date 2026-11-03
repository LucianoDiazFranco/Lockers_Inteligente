using LockersInteligentes.ArqBase.DataAccess;
using LockersInteligentes.ArqBase.Entidades;
using LockersInteligentes.ArqBase.Enums;
using LockersInteligentes.ArqBase.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockersInteligentes.ArqBase.Logica
{
    public class HistorialService
    {
        public void Grabar(TipoOperacion tipo, string detalle,
                           int? idLocker = null, int? idOrden = null)
        {
            try
            {
                Historial registro = new Historial();
                registro.Actor = GestorSesion.Instancia.ActorParaBitacora();
                registro.TipoDeOperacion = tipo;
                registro.FechaOperacion = DateTime.Now;
                registro.Detalle = detalle;
                registro.IdLockerUtilizado = idLocker;
                registro.IdOrdenDeEntrega = idOrden;

                RepositorioFactoryArq.Instancia.Historial.Insertar(registro);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Fallo al grabar bitácora: " + ex.Message);
            }
        }

        /// <summary>CU.Arq.002 / CU.Inf.002. Consulta con filtros opcionales.</summary>
        public IList<Historial> Buscar(DateTime? desde, DateTime? hasta,
                                       string actor, TipoOperacion? tipo)
        {
            GestorSesion.Instancia.ValidarRolAdministrador();
            return RepositorioFactoryArq.Instancia.Historial.Buscar(desde, hasta, actor, tipo);
        }
    }
}
