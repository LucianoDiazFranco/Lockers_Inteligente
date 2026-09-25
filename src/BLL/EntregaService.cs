using LockersInteligentes.ArqBase.Servicios;
using LockersInteligentes.DAL.Repositorios;
using LockersInteligentes.Dominio.Entidades;
using LockersInteligentes.Dominio.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LockersInteligentes.BLL
{
    /// <summary>
    /// Orquestador del ciclo de vida del paquete: CU.Op.001 Reservar Locker,
    /// CU.Op.002 Registrar Entrega y CU.Op.003 Registrar Retiro.
    ///
    /// El service coordina, pero NO decide qué transiciones son válidas: eso lo
    /// custodian las entidades. Si el locker no está libre, Locker.Reservar() lanza
    /// la excepción, no un if de acá.
    /// </summary>
    public class EntregaService
    {
        private readonly EmailService _email = new EmailService();

        /// <summary>
        /// CU.Op.001. Busca un locker libre del tamaño adecuado, lo reserva, crea la
        /// orden, genera el PIN de apertura y notifica al residente.
        /// </summary>
        public ResultadoReserva ReservarLocker(int idResidente, TamanioPaquete tamanio,
                                               string descripcion)
        {
            ValidarSesion();

            Residente residente = RepositorioFactory.Instancia.Residentes.ObtenerPorId(idResidente);

            if (residente == null)
                throw new InvalidOperationException("El residente seleccionado no existe.");

            // Se busca en el edificio del residente: un paquete no puede ir a un
            // locker de otro edificio.
            Locker locker = RepositorioFactory.Instancia.Lockers
                                              .ObtenerLibreParaTamanio(residente.Edificio.Id, tamanio);

            if (locker == null)
                throw new InvalidOperationException(
                    "No hay lockers libres de tamaño " + tamanio + " o superior en " +
                    residente.Edificio.Nombre + ".");

            // La entidad valida la transición Libre -> Reservado.
            locker.Reservar();
            RepositorioFactory.Instancia.Lockers.Actualizar(locker);

            OrdenDeEntrega orden = new OrdenDeEntrega();
            orden.Descripcion = descripcion;
            orden.TamanioPaquete = tamanio;
            orden.Estado = EstadoOrden.Reservada;
            orden.FechaReserva = DateTime.Now;
            orden.LockerAsignado = locker;
            orden.ResidenteAsignado = residente;
            orden.RepartidorAsignado = null;   // se conoce al registrar la entrega

            RepositorioFactory.Instancia.Ordenes.Insertar(orden);

            CodigoAccesoFactory.ResultadoPin pin =
                CodigoAccesoFactory.Generar(orden, TipoCodigo.Apertura);

            RepositorioFactory.Instancia.Codigos.Insertar(pin.Codigo);
            orden.AgregarCodigo(pin.Codigo);

            ResultadoReserva resultado = new ResultadoReserva();
            resultado.Orden = orden;
            resultado.PinApertura = pin.PinEnClaro;

            string error;
            resultado.NotificacionEnviada = NotificarReserva(orden, pin.PinEnClaro, out error);
            resultado.MotivoFalloNotificacion = error;

            // PENDIENTE: grabar en bitácora (TipoOperacion.Reserva, locker y orden).
            // No va acá adentro de un if: la reserva se registra siempre, haya o no
            // salido el mail.

            return resultado;
        }

        public IList<Residente> ListarResidentes()
        {
            ValidarSesion();

            return RepositorioFactory.Instancia.Residentes
                .ObtenerTodos()
                .Where(r => r.Activo)
                .ToList();
        }

        public IList<OrdenDeEntrega> ListarPorEstado(EstadoOrden estado)
        {
            ValidarSesion();
            return RepositorioFactory.Instancia.Ordenes.ObtenerPorEstado(estado);
        }

        /// <summary>
        /// CU.Not.001. Un fallo NO revierte la reserva: el diseño establece que la
        /// operación sigue su curso y la notificación queda registrada como fallida.
        /// Por eso devuelve bool en vez de lanzar.
        /// </summary>
        private bool NotificarReserva(OrdenDeEntrega orden, string pin, out string error)
        {
            string cuerpo =
                "Hola " + orden.ResidenteAsignado.Nombre + "," + Environment.NewLine + Environment.NewLine +
                "Se reservó un locker para un paquete a tu nombre." + Environment.NewLine + Environment.NewLine +
                "Edificio: " + orden.LockerAsignado.Edificio.Nombre + Environment.NewLine +
                "Locker:   " + orden.LockerAsignado.Numero +
                    " (módulo " + orden.LockerAsignado.GrupoLocker + ")" + Environment.NewLine +
                "Tamaño:   " + orden.TamanioPaquete + Environment.NewLine + Environment.NewLine +
                "PIN de apertura para el repartidor: " + pin + Environment.NewLine + Environment.NewLine +
                "Este código es de un solo uso y vence en " +
                    CodigoAcceso.HorasDeVigencia + " horas." + Environment.NewLine + Environment.NewLine +
                "Lockers Inteligentes";

            return _email.Enviar(orden.ResidenteAsignado.Correo,
                                 "Locker reservado - Orden #" + orden.Id,
                                 cuerpo, out error);
        }

        /// <summary>
        /// El ciclo del paquete lo pueden operar Administrador y Operador, así que
        /// alcanza con exigir sesión iniciada. No se valida rol específico.
        /// </summary>
        private void ValidarSesion()
        {
            if (!GestorSesion.Instancia.HaySesionActiva)
                throw new InvalidOperationException("No hay una sesión activa.");
        }
    }
}