using LockersInteligentes.DAL.Repositorios;
using LockersInteligentes.Dominio.Entidades;
using LockersInteligentes.Dominio.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockersInteligentes.BLL
{
    public class HistorialService
    {
        public void Grabar(TipoOperacion tipo, string detalle,
                          Locker locker = null, OrdenDeEntrega orden = null)
        {
            try
            {
                Historial registro = new Historial();
                registro.Actor = GestorSesion.Instancia.ActorParaBitacora();
                registro.TipoDeOperacion = tipo;
                registro.FechaOperacion = DateTime.Now;
                registro.Detalle = detalle;
                registro.LockerUtilizado = locker;
                registro.OrdenDeEntrega = orden;

                RepositorioFactory.Instancia.Historial.Insertar(registro);
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
            return RepositorioFactory.Instancia.Historial.Buscar(desde, hasta, actor, tipo);
        }
    }
}
