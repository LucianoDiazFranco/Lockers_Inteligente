using LockersInteligentes.ArqBase.Servicios;
using LockersInteligentes.DAL.Repositorios;
using LockersInteligentes.Dominio.Entidades;
using LockersInteligentes.Dominio.Enums;
using LockersInteligentes.ArqBase.Enums;
using LockersInteligentes.ArqBase.Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using LockersInteligentes.Dominio.Excepciones;
using LockersInteligentes.ArqBaseExcepciones;

namespace LockersInteligentes.BLL
{
    public class EntregaService
    {
        private readonly EmailService _email = new EmailService();
        private readonly HistorialService _historial = new HistorialService();

        public ResultadoReserva ReservarLocker(int idResidente, TamanioPaquete tamanio,
                                               string descripcion)
        {
            GestorSesion.Instancia.ValidarPermiso(PermisoService.Patentes.Reservar);
            ValidarSesion();

            Residente residente = RepositorioFactory.Instancia.Residentes.ObtenerPorId(idResidente);

            if (residente == null)
                throw new InvalidOperationException("El residente seleccionado no existe.");

            // Se busca en el edificio del residente: un paquete no puede ir a un
            // locker de otro edificio.
            Locker locker = RepositorioFactory.Instancia.Lockers
                                              .ObtenerLibreParaTamanio(residente.Edificio.Id, tamanio);

            if (locker == null)
                throw new LockerNoDisponibleException(tamanio, residente.Edificio.Nombre);

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

            _historial.Grabar(TipoOperacion.Reserva,
               "Reserva del locker " + locker.Numero + " para " +
               residente.NombreCompleto() + " (paquete " + tamanio + ")",
               locker.Id, orden.Id);

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

        private void ValidarSesion()
        {
            if (!GestorSesion.Instancia.HaySesionActiva)
                throw new AccesoDenegadoException("No hay una sesión activa.");
        }
        public ResultadoEntrega RegistrarEntrega(int idOrden, string pinIngresado, int idRepartidor)
        {
            GestorSesion.Instancia.ValidarPermiso(PermisoService.Patentes.Reservar);
            ValidarSesion();

            if (string.IsNullOrWhiteSpace(pinIngresado))
                throw new InvalidOperationException("Ingresá el PIN de apertura.");

            OrdenDeEntrega orden = RepositorioFactory.Instancia.Ordenes.ObtenerPorId(idOrden);

            if (orden == null)
                throw new InvalidOperationException("La orden no existe.");

            if (orden.Estado != EstadoOrden.Reservada)
                throw new InvalidOperationException(
                    "La orden está " + orden.Estado + ": solo se puede registrar la entrega " +
                    "de una orden Reservada.");

            Repartidor repartidor = RepositorioFactory.Instancia.Repartidores.ObtenerPorId(idRepartidor);

            if (repartidor == null)
                throw new InvalidOperationException("Seleccioná un repartidor.");

            CodigoAcceso codigo = RepositorioFactory.Instancia.Codigos
                                   .ObtenerVigente(idOrden, TipoCodigo.Apertura);

            if (codigo == null)
            {
                CodigoAcceso ultimo = RepositorioFactory.Instancia.Codigos
                                       .ObtenerUltimo(idOrden, TipoCodigo.Apertura);

                if (ultimo != null && ultimo.Usado)
                    throw new PinInvalidoException(
                        PinInvalidoException.Motivo.YaUtilizado, TipoCodigo.Apertura,
                        "Ese PIN de apertura ya fue utilizado: la entrega de esta orden " +
                        "ya se registró. Actualizá la lista.");

                throw new PinInvalidoException(
                    PinInvalidoException.Motivo.NoExiste, TipoCodigo.Apertura,
                    "No hay un PIN de apertura vigente para esta orden.");
            }

            if (codigo.EstaVencido())
                throw new PinInvalidoException(
                    PinInvalidoException.Motivo.Vencido, TipoCodigo.Apertura,
                    "El PIN de apertura venció el " +
                    codigo.FechaExpiracion.ToString("dd/MM/yyyy HH:mm") +
                    ". Hay que generar una reserva nueva.");

            string hashIngresado = EncriptadorService.Encriptar(pinIngresado.Trim(), codigo.PinSalt);

            if (!codigo.CoincideCon(hashIngresado))
                throw new PinInvalidoException(
                    PinInvalidoException.Motivo.Incorrecto, TipoCodigo.Apertura,
                    "El PIN de apertura es incorrecto.");

            codigo.MarcarUsado();
            RepositorioFactory.Instancia.Codigos.Actualizar(codigo);

            orden.MarcarEntregada();
            orden.RepartidorAsignado = repartidor;
            RepositorioFactory.Instancia.Ordenes.Actualizar(orden);

            orden.LockerAsignado.Ocupar();
            RepositorioFactory.Instancia.Lockers.Actualizar(orden.LockerAsignado);

            CodigoAccesoFactory.ResultadoPin pinRetiro =
                CodigoAccesoFactory.Generar(orden, TipoCodigo.Retiro);

            RepositorioFactory.Instancia.Codigos.Insertar(pinRetiro.Codigo);

            ResultadoEntrega resultado = new ResultadoEntrega();
            resultado.Orden = orden;
            resultado.PinRetiro = pinRetiro.PinEnClaro;

            string error;
            resultado.NotificacionEnviada = NotificarEntrega(orden, pinRetiro.PinEnClaro, out error);
            resultado.MotivoFalloNotificacion = error;

            _historial.Grabar(TipoOperacion.Entrega,
                "Entrega en locker " + orden.LockerAsignado.Numero + " por " +
                repartidor.NombreCompleto(),
                orden.LockerAsignado.Id, orden.Id);

            return resultado;
        }

        /// <summary>Órdenes esperando que el repartidor deposite el paquete.</summary>
        public IList<OrdenDeEntrega> ListarPendientesDeEntrega()
        {
            ValidarSesion();
            return RepositorioFactory.Instancia.Ordenes.ObtenerPorEstado(EstadoOrden.Reservada);
        }

        public IList<Repartidor> ListarRepartidores()
        {
            ValidarSesion();

            return RepositorioFactory.Instancia.Repartidores
                .ObtenerTodos()
                .Where(r => r.Activo)
                .ToList();
        }
        public void CancelarReservaVencida(int idOrden)
        {
            ValidarSesion();

            OrdenDeEntrega orden = RepositorioFactory.Instancia.Ordenes.ObtenerPorId(idOrden);

            if (orden == null)
                throw new InvalidOperationException("La orden no existe.");

            if (orden.Estado != EstadoOrden.Reservada)
                throw new InvalidOperationException(
                    "Solo se pueden cancelar órdenes en estado Reservada. Esta está " +
                    orden.Estado + ".");

            if (!orden.ReservaExpirada())
                throw new InvalidOperationException(
                    "La reserva todavía está vigente: el PIN de apertura vence el " +
                    orden.FechaReserva.AddHours(CodigoAcceso.HorasDeVigencia)
                         .ToString("dd/MM/yyyy HH:mm") + ".");

            // Se anula el PIN pendiente para que no pueda usarse después.
            CodigoAcceso codigo = RepositorioFactory.Instancia.Codigos
                                   .ObtenerVigente(idOrden, TipoCodigo.Apertura);

            if (codigo != null)
            {
                codigo.Usado = true;
                RepositorioFactory.Instancia.Codigos.Actualizar(codigo);
            }

            orden.Estado = EstadoOrden.Retirada;
            orden.FechaRetiro = DateTime.Now;
            orden.Descripcion = (orden.Descripcion ?? string.Empty) +
                                " [Cancelada: reserva vencida sin entrega]";
            RepositorioFactory.Instancia.Ordenes.Actualizar(orden);

            orden.LockerAsignado.Liberar();
            RepositorioFactory.Instancia.Lockers.Actualizar(orden.LockerAsignado);

            _historial.Grabar(TipoOperacion.Baja,
                "Cancelación de reserva vencida. Locker " + orden.LockerAsignado.Numero + " liberado.",
                orden.LockerAsignado.Id, orden.Id);

        }

        /// <summary>Reservas con el PIN de apertura vencido, candidatas a liberar.</summary>
        public IList<OrdenDeEntrega> ListarReservasVencidas()
        {
            ValidarSesion();

            return RepositorioFactory.Instancia.Ordenes
                .ObtenerPorEstado(EstadoOrden.Reservada)
                .Where(o => o.ReservaExpirada())
                .ToList();
        }

        private bool NotificarEntrega(OrdenDeEntrega orden, string pin, out string error)
        {
            string cuerpo =
                "Hola " + orden.ResidenteAsignado.Nombre + "," + Environment.NewLine + Environment.NewLine +
                "Tu paquete ya fue depositado y te está esperando." + Environment.NewLine + Environment.NewLine +
                "Edificio: " + orden.LockerAsignado.Edificio.Nombre + Environment.NewLine +
                "Locker:   " + orden.LockerAsignado.Numero +
                    " (módulo " + orden.LockerAsignado.GrupoLocker + ")" + Environment.NewLine +
                "Entregado por: " + orden.RepartidorAsignado.NombreCompleto() + Environment.NewLine +
                Environment.NewLine +
                "PIN de retiro: " + pin + Environment.NewLine + Environment.NewLine +
                "Tenés " + OrdenDeEntrega.DiasParaRetirar + " días para retirarlo." +
                Environment.NewLine + Environment.NewLine +
                "Lockers Inteligentes";

            return _email.Enviar(orden.ResidenteAsignado.Correo,
                                 "Paquete depositado - Orden #" + orden.Id,
                                 cuerpo, out error);
        }

        public OrdenDeEntrega RegistrarRetiro(int idOrden, string pinIngresado)
        {
            GestorSesion.Instancia.ValidarPermiso(PermisoService.Patentes.Reservar);
            ValidarSesion();

            if (string.IsNullOrWhiteSpace(pinIngresado))
                throw new InvalidOperationException("Ingresá el PIN de retiro.");

            OrdenDeEntrega orden = RepositorioFactory.Instancia.Ordenes.ObtenerPorId(idOrden);

            if (orden == null)
                throw new InvalidOperationException("La orden no existe.");

            if (orden.Estado != EstadoOrden.Entregada)
                throw new InvalidOperationException(
                    "La orden está " + orden.Estado + ": solo se puede registrar el retiro " +
                    "de una orden Entregada.");

            CodigoAcceso codigo = RepositorioFactory.Instancia.Codigos
                                   .ObtenerVigente(idOrden, TipoCodigo.Retiro);

            if (codigo == null)
            {
                CodigoAcceso ultimo = RepositorioFactory.Instancia.Codigos
                                       .ObtenerUltimo(idOrden, TipoCodigo.Retiro);

                if (ultimo != null && ultimo.Usado)
                    throw new PinInvalidoException(
                        PinInvalidoException.Motivo.YaUtilizado, TipoCodigo.Retiro,
                        "Ese PIN de retiro ya fue utilizado: el paquete ya se retiró. " +
                        "Actualizá la lista.");

                throw new PinInvalidoException(
                    PinInvalidoException.Motivo.NoExiste, TipoCodigo.Retiro,
                    "No hay un PIN de retiro vigente para esta orden.");
            }

            string hashIngresado = EncriptadorService.Encriptar(pinIngresado.Trim(), codigo.PinSalt);

            if (!string.Equals(codigo.PinHash, hashIngresado, StringComparison.Ordinal))
                throw new PinInvalidoException(
                    PinInvalidoException.Motivo.Incorrecto, TipoCodigo.Retiro,
                    "El PIN de retiro es incorrecto.");

            if (codigo.Usado)
                throw new InvalidOperationException("Ese PIN de retiro ya fue utilizado.");

            codigo.Usado = true;
            RepositorioFactory.Instancia.Codigos.Actualizar(codigo);

            orden.MarcarRetirada();
            RepositorioFactory.Instancia.Ordenes.Actualizar(orden);

            orden.LockerAsignado.Liberar();
            RepositorioFactory.Instancia.Lockers.Actualizar(orden.LockerAsignado);

            _historial.Grabar(TipoOperacion.Retiro,
                 "Retiro del locker " + orden.LockerAsignado.Numero + " por " +
                 orden.ResidenteAsignado.NombreCompleto(),
                 orden.LockerAsignado.Id, orden.Id);

            return orden;
        }
        public IList<OrdenDeEntrega> ListarPendientesDeRetiro()
        {
            ValidarSesion();
            return RepositorioFactory.Instancia.Ordenes.ObtenerPorEstado(EstadoOrden.Entregada);
        }
    }
}