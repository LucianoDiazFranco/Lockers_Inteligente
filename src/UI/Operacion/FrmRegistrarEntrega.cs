using LockersInteligentes.ArqBase.ArqBase;
using LockersInteligentes.BLL;
using LockersInteligentes.Dominio.Entidades;
using LockersInteligentes.Dominio.Excepciones;
using LockersInteligentes.ArqBase.Logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LockersInteligentes.ArqBase.Excepciones;

namespace LockersInteligentes.UI.Operacion
{
    /// <summary>CU.Op.002 Registrar Entrega.</summary>
    public partial class FrmRegistrarEntrega : Form
    {
        private readonly EntregaService _servicio = new EntregaService();
        private IList<OrdenDeEntrega> _pendientes = new List<OrdenDeEntrega>();
        public FrmRegistrarEntrega()
        {
            InitializeComponent();
        }
        private void FrmRegistrarEntrega_Load(object sender, EventArgs e)
        {
            try
            {
                cboRepartidor.DisplayMember = "Texto";
                cboRepartidor.ValueMember = "Id";
                cboRepartidor.DataSource = _servicio.ListarRepartidores()
                    .Select(r => new { r.Id, Texto = r.NombreCompleto() })
                    .ToList();

                CargarPendientes();
            }
            catch (Exception ex)
            {
                Mostrar(ex.Message, true);
            }
        }

        private void CargarPendientes()
        {
            try
            {
                _pendientes = _servicio.ListarPendientesDeEntrega();

                dgvPendientes.DataSource = _pendientes
                    .Select(o => new
                    {
                        o.Id,
                        Orden = "#" + o.Id,
                        Residente = o.ResidenteAsignado.NombreCompleto(),
                        Piso = o.ResidenteAsignado.Piso,
                        Edificio = o.LockerAsignado.Edificio.Nombre,
                        Locker = o.LockerAsignado.Numero,
                        Paquete = o.TamanioPaquete.ToString(),
                        Reservada = o.FechaReserva.ToString("dd/MM/yyyy HH:mm"),
                        Estado = o.ReservaExpirada() ? "PIN vencido" : "Vigente"
                    })
                    .ToList();

                if (dgvPendientes.Columns.Contains("Id"))
                    dgvPendientes.Columns["Id"].Visible = false;

                // Las reservas con el PIN vencido se marcan: ese PIN ya no sirve y
                // el intento va a fallar, mejor avisarlo antes.
                foreach (DataGridViewRow fila in dgvPendientes.Rows)
                {
                    if (fila.Cells["Estado"].Value != null &&
                        fila.Cells["Estado"].Value.ToString() == "PIN vencido")
                        fila.DefaultCellStyle.ForeColor = Color.Firebrick;
                }

                if (_pendientes.Count == 0)
                    Mostrar("No hay órdenes esperando entrega.", false);
            }
            catch (Exception ex)
            {
                Mostrar(ex.Message, true);
            }
        }

        private OrdenDeEntrega OrdenSeleccionada()
        {
            if (dgvPendientes.CurrentRow == null || dgvPendientes.CurrentRow.Index < 0)
                return null;

            object valor = dgvPendientes.CurrentRow.Cells["Id"].Value;

            if (valor == null)
                return null;

            int id = Convert.ToInt32(valor);
            return _pendientes.FirstOrDefault(o => o.Id == id);
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            OrdenDeEntrega orden = OrdenSeleccionada();

            if (orden == null)
            {
                Mostrar("Seleccioná una orden de la lista.", true);
                return;
            }

            if (cboRepartidor.SelectedValue == null)
            {
                Mostrar("Seleccioná el repartidor que trae el paquete.", true);
                return;
            }

            lblMensaje.Text = string.Empty;
            btnRegistrar.Enabled = false;
            Cursor = Cursors.WaitCursor;

            try
            {
                ResultadoEntrega resultado = _servicio.RegistrarEntrega(
                    orden.Id, txtPin.Text, Convert.ToInt32(cboRepartidor.SelectedValue));

                MostrarResultado(resultado);
                txtPin.Clear();
                CargarPendientes();
            }
            catch (PinInvalidoException ex)
            {
                grpResultado.Visible = false;
                Mostrar(ex.Message, true);

                // Según el motivo, la pantalla reacciona distinto. Eso es lo que
                // una excepción genérica no permitía hacer.
                if (ex.MotivoFalla == PinInvalidoException.Motivo.YaUtilizado)
                {
                    CargarPendientes();
                }
                else
                {
                    txtPin.SelectAll();
                    txtPin.Focus();
                }
            }
            catch (ExcepcionNegocio ex)
            {
                grpResultado.Visible = false;
                Mostrar(ex.Message, true);
            }
            catch (Exception ex)
            {
                // Cualquier cosa que NO sea de negocio es un fallo real del sistema:
                // va al log con el detalle técnico, y al usuario se le muestra un
                // mensaje comprensible sin exponer la estructura interna.
                grpResultado.Visible = false;

                ErrorLogService.Registrar(ex, "FrmRegistrarEntrega.btnRegistrar_Click",
                    GestorSesion.Instancia.ActorParaBitacora());

                Mostrar("Ocurrió un error inesperado. Consultá el log de errores.", true);
            }
            finally
            {
                btnRegistrar.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private void MostrarResultado(ResultadoEntrega resultado)
        {
            OrdenDeEntrega orden = resultado.Orden;

            lblResultado.Text =
                "Orden N° " + orden.Id + Environment.NewLine + Environment.NewLine +
                "Residente: " + orden.ResidenteAsignado.NombreCompleto() + Environment.NewLine +
                "Locker:    " + orden.LockerAsignado.Numero +
                    "  (módulo " + orden.LockerAsignado.GrupoLocker + ")" + Environment.NewLine +
                "Entregó:   " + orden.RepartidorAsignado.NombreCompleto() + Environment.NewLine +
                "Fecha:     " + orden.FechaEntrega.Value.ToString("dd/MM/yyyy HH:mm") + Environment.NewLine +
                "Estado:    " + orden.Estado + Environment.NewLine + Environment.NewLine +
                "El locker quedó Ocupado. Vence en " +
                    OrdenDeEntrega.DiasParaRetirar + " días.";

            lblPinRetiro.Text = resultado.PinRetiro;

            lblAvisoPin.Text = "Comunicale este PIN al residente. No se puede volver a consultar.";
            lblAvisoPin.ForeColor = Color.Gray;
            grpResultado.Visible = true;

            Mostrar("Entrega registrada correctamente.", false);
        }
        private void btnLiberar_Click(object sender, EventArgs e)
        {
            OrdenDeEntrega orden = OrdenSeleccionada();

            if (orden == null)
            {
                Mostrar("Seleccioná una orden de la lista.", true);
                return;
            }

            if (!orden.ReservaExpirada())
            {
                Mostrar("Esa reserva sigue vigente. Solo se pueden liberar las vencidas.", true);
                return;
            }

            string texto =
                "¿Liberar el locker " + orden.LockerAsignado.Numero + " de la orden #" + orden.Id + "?" +
                Environment.NewLine + Environment.NewLine +
                "Residente: " + orden.ResidenteAsignado.NombreCompleto() + Environment.NewLine +
                "Reservado el " + orden.FechaReserva.ToString("dd/MM/yyyy HH:mm") + Environment.NewLine +
                Environment.NewLine +
                "El PIN se anula y el locker vuelve a quedar libre.";

            if (MessageBox.Show(texto, "Confirmar", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                _servicio.CancelarReservaVencida(orden.Id);
                Mostrar("Locker " + orden.LockerAsignado.Numero + " liberado.", false);
                grpResultado.Visible = false;
                CargarPendientes();
            }
            catch (Exception ex)
            {
                Mostrar(ex.Message, true);
            }
        }
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            grpResultado.Visible = false;
            CargarPendientes();
        }

        private void Mostrar(string mensaje, bool esError)
        {
            lblMensaje.ForeColor = esError ? Color.Firebrick : Color.SeaGreen;
            lblMensaje.Text = mensaje;
        }

    }
}
