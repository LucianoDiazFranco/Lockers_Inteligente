using LockersInteligentes.BLL;
using LockersInteligentes.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace LockersInteligentes.UI.Operacion
{
    public partial class FrmRegistrarRetiro : Form
    {
        private readonly SistemaLockersFacade _sistema = SistemaLockersFacade.Instancia;
        private IList<OrdenDeEntrega> _pendientes = new List<OrdenDeEntrega>();
        public FrmRegistrarRetiro()
        {
            InitializeComponent();
        }
        private void FrmRegistrarRetiro_Load(object sender, EventArgs e)
        {
            CargarPendientes();
        }

        private void CargarPendientes()
        {
            try
            {
                _pendientes = _sistema.PendientesDeRetiro();

                dgvPendientes.DataSource = _pendientes
                    .Select(o => new
                    {
                        o.Id,
                        Orden = "#" + o.Id,
                        Residente = o.ResidenteAsignado.NombreCompleto(),
                        Piso = o.ResidenteAsignado.Piso,
                        Edificio = o.LockerAsignado.Edificio.Nombre,
                        Locker = o.LockerAsignado.Numero,
                        Entregado = o.FechaEntrega.HasValue
                            ? o.FechaEntrega.Value.ToString("dd/MM/yyyy HH:mm") : "",
                        Plazo = TextoPlazo(o)
                    })
                    .ToList();

                if (dgvPendientes.Columns.Contains("Id"))
                    dgvPendientes.Columns["Id"].Visible = false;

                PintarVencidos();

                if (_pendientes.Count == 0)
                    Mostrar("No hay paquetes esperando retiro.", false);
            }
            catch (Exception ex)
            {
                Mostrar(ex.Message, true);
            }
        }

        private string TextoPlazo(OrdenDeEntrega orden)
        {
            int? dias = orden.DiasRestantesParaRetiro();

            if (!dias.HasValue)
                return "";

            if (orden.EstaVencida())
                return "VENCIDO hace " + Math.Abs(dias.Value) + " d";

            return "Quedan " + dias.Value + " d";
        }
        private void PintarVencidos()
        {
            foreach (DataGridViewRow fila in dgvPendientes.Rows)
            {
                if (fila.Cells["Plazo"].Value != null &&
                    fila.Cells["Plazo"].Value.ToString().StartsWith("VENCIDO"))
                {
                    fila.DefaultCellStyle.ForeColor = Color.FromArgb(160, 110, 10);
                    fila.DefaultCellStyle.BackColor = Color.FromArgb(255, 248, 225);
                }
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
            OrdenDeEntrega seleccionada = OrdenSeleccionada();

            if (seleccionada == null)
            {
                Mostrar("Seleccioná un paquete de la lista.", true);
                return;
            }

            lblMensaje.Text = string.Empty;
            btnRegistrar.Enabled = false;
            Cursor = Cursors.WaitCursor;

            try
            {
                OrdenDeEntrega orden = _sistema.RegistrarRetiro(seleccionada.Id, txtPin.Text);

                MostrarResultado(orden);
                txtPin.Clear();
                CargarPendientes();
            }
            catch (Exception ex)
            {
                grpResultado.Visible = false;
                Mostrar(ex.Message, true);
                txtPin.SelectAll();
                txtPin.Focus();
            }
            finally
            {
                btnRegistrar.Enabled = true;
                Cursor = Cursors.Default;
            }
        }
        private void MostrarResultado(OrdenDeEntrega orden)
        {
            lblResultado.Text =
                "Orden N° " + orden.Id + " cerrada." + Environment.NewLine + Environment.NewLine +
                "Residente: " + orden.ResidenteAsignado.NombreCompleto() + Environment.NewLine +
                "Locker:    " + orden.LockerAsignado.Numero +
                    "  (módulo " + orden.LockerAsignado.GrupoLocker + ")" + Environment.NewLine +
                "Entregado: " + orden.FechaEntrega.Value.ToString("dd/MM/yyyy HH:mm") + Environment.NewLine +
                "Retirado:  " + orden.FechaRetiro.Value.ToString("dd/MM/yyyy HH:mm") + Environment.NewLine +
                "Estado:    " + orden.Estado + Environment.NewLine + Environment.NewLine +
                "El locker " + orden.LockerAsignado.Numero + " quedó libre.";

            grpResultado.Visible = true;
            Mostrar("Retiro registrado. Ciclo del paquete completo.", false);
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
