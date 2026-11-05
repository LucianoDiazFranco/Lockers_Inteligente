using LockersInteligentes.ArqBase.ArqBase;
using LockersInteligentes.BLL;
using LockersInteligentes.Dominio.Entidades;
using LockersInteligentes.Dominio.Excepciones;
using LockersInteligentes.ArqBase.Logica;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using LockersInteligentes.ArqBase.Excepciones;

namespace LockersInteligentes.UI.Operacion
{
    /// <summary>CU.Op.002 Registrar Entrega.</summary>
    public partial class FrmRegistrarEntrega : Form, IObservadorIdioma
    {
        private readonly SistemaLockersFacade _sistema = SistemaLockersFacade.Instancia;
        private IList<OrdenDeEntrega> _pendientes = new List<OrdenDeEntrega>();
        public FrmRegistrarEntrega()
        {
            InitializeComponent();
            GestorIdioma.Instancia.Suscribir(this);
        }
        private void FrmRegistrarEntrega_Load(object sender, EventArgs e)
        {
            try
            {
                cboRepartidor.DisplayMember = "Texto";
                cboRepartidor.ValueMember = "Id";
                cboRepartidor.DataSource = _sistema.RepartidoresActivos()
                    .Select(r => new { r.Id, Texto = r.NombreCompleto() })
                    .ToList();

                CargarPendientes();
                ActualizarIdioma();
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
                GestorIdioma idioma = GestorIdioma.Instancia;
                string vigente = idioma.Traducir("FrmEntrega.Vigente");
                string vencido = idioma.Traducir("FrmEntrega.Vencido");

                _pendientes = _sistema.PendientesDeEntrega();

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
                        Estado = o.ReservaExpirada() ? vencido : vigente
                    })
                    .ToList();

                if (dgvPendientes.Columns.Contains("Id"))
                    dgvPendientes.Columns["Id"].Visible = false;

                TraducirColumna("Orden", "Comun.Orden");
                TraducirColumna("Residente", "Comun.Residente");
                TraducirColumna("Piso", "Comun.Piso");
                TraducirColumna("Edificio", "Comun.Edificio");
                TraducirColumna("Locker", "Comun.Locker");
                TraducirColumna("Paquete", "Comun.Paquete");
                TraducirColumna("Reservada", "FrmEntrega.colReserv");
                TraducirColumna("Estado", "Comun.Estado");

                foreach (DataGridViewRow fila in dgvPendientes.Rows)
                {
                    if (fila.Cells["Estado"].Value != null &&
                        fila.Cells["Estado"].Value.ToString() == vencido)
                        fila.DefaultCellStyle.ForeColor = Color.Firebrick;
                }

                if (_pendientes.Count == 0)
                    Mostrar(idioma.Traducir("FrmEntrega.SinPend"), false);
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
                Mostrar(GestorIdioma.Instancia.Traducir("Val.Orden"), true);
                return;
            }

            if (cboRepartidor.SelectedValue == null)
            {
                Mostrar(GestorIdioma.Instancia.Traducir("Val.Repartidor"), true);
                return;
            }

            lblMensaje.Text = string.Empty;
            btnRegistrar.Enabled = false;
            Cursor = Cursors.WaitCursor;

            try
            {
                ResultadoEntrega resultado = _sistema.RegistrarEntrega(
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

                Mostrar(GestorIdioma.Instancia.Traducir("Val.ErrorGrave"), true);
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
            GestorIdioma idioma = GestorIdioma.Instancia;

            lblResultado.Text =
                string.Format(idioma.Traducir("Res.Orden"), orden.Id) +
                    Environment.NewLine + Environment.NewLine +
                idioma.Traducir("Res.Residente") + " " +
                    orden.ResidenteAsignado.NombreCompleto() + Environment.NewLine +
                idioma.Traducir("Res.Locker") + " " + orden.LockerAsignado.Numero + " " +
                    string.Format(idioma.Traducir("Res.Modulo"),
                                  orden.LockerAsignado.GrupoLocker) + Environment.NewLine +
                idioma.Traducir("Res.Entrego") + " " +
                    orden.RepartidorAsignado.NombreCompleto() + Environment.NewLine +
                idioma.Traducir("Res.Fecha") + " " +
                    orden.FechaEntrega.Value.ToString("dd/MM/yyyy HH:mm") + Environment.NewLine +
                idioma.Traducir("Res.Estado") + " " +
                    idioma.TraducirEnum("EstadoOrden", orden.Estado) +
                    Environment.NewLine + Environment.NewLine +
                string.Format(idioma.Traducir("Res.LockerOcupado"),
                              OrdenDeEntrega.DiasParaRetirar);

            lblPinRetiro.Text = resultado.PinRetiro;
            grpResultado.Visible = true;

            Mostrar(idioma.Traducir("Msg.EntregaOk"), false);
        }
        private void btnLiberar_Click(object sender, EventArgs e)
        {
            OrdenDeEntrega orden = OrdenSeleccionada();

            if (orden == null)
            {
                Mostrar(GestorIdioma.Instancia.Traducir("Val.Orden"), true);
                return;
            }

            if (!orden.ReservaExpirada())
            {
                Mostrar(GestorIdioma.Instancia.Traducir("Val.NoVencida"), true);
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
                _sistema.CancelarReservaVencida(orden.Id);
                Mostrar(string.Format(GestorIdioma.Instancia.Traducir("Msg.LiberadoOk"),
                                      orden.LockerAsignado.Numero), false);
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

        public void ActualizarIdioma()
        {
            GestorIdioma idioma = GestorIdioma.Instancia;

            Text = idioma.Traducir("FrmEntrega.Caption");
            lblTitulo.Text = idioma.Traducir("FrmEntrega.Caption");
            lblAyuda.Text = idioma.Traducir("FrmEntrega.Ayuda");
            lblRepartidor.Text = idioma.Traducir("Comun.Repartidor");
            lblPin.Text = idioma.Traducir("FrmEntrega.lblPin");
            btnRegistrar.Text = idioma.Traducir("FrmEntrega.btnReg");
            btnActualizar.Text = idioma.Traducir("Comun.Actualizar");
            btnLiberar.Text = idioma.Traducir("FrmEntrega.btnLib");
            grpResultado.Text = idioma.Traducir("FrmEntrega.grpRes");
            lblAvisoPin.Text = idioma.Traducir("FrmEntrega.AvisoPin");

            CargarPendientes();
        }
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            GestorIdioma.Instancia.Desuscribir(this);
            base.OnFormClosed(e);
        }

        private void TraducirColumna(string nombreColumna, string clave)
        {
            if (dgvPendientes.Columns.Contains(nombreColumna))
                dgvPendientes.Columns[nombreColumna].HeaderText =
                    GestorIdioma.Instancia.Traducir(clave);
        }
    }
}
