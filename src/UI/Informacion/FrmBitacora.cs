using LockersInteligentes.ArqBase.Entidades;
using LockersInteligentes.ArqBase.Enums;
using LockersInteligentes.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace LockersInteligentes.UI.Informacion
{
    public partial class FrmBitacora : Form
    {
        private const string TodasLasOperaciones = "(todas)";

        private readonly SistemaLockersFacade _servicio = SistemaLockersFacade.Instancia;
        public FrmBitacora()
        {
            InitializeComponent();
        }
        private void FrmBitacora_Load(object sender, EventArgs e)
        {
            // El combo se arma con los valores del enum más una opción neutra,
            // así no puede quedar desincronizado del dominio.
            List<object> tipos = new List<object> { TodasLasOperaciones };

            foreach (TipoOperacion tipo in Enum.GetValues(typeof(TipoOperacion)))
                tipos.Add(tipo);

            cboTipo.DataSource = tipos;

            dtpDesde.Value = DateTime.Today.AddDays(-7);
            dtpHasta.Value = DateTime.Today;

            Buscar();
        }

        private void chkDesde_CheckedChanged(object sender, EventArgs e)
        {
            dtpDesde.Enabled = chkDesde.Checked;
        }

        private void chkHasta_CheckedChanged(object sender, EventArgs e)
        {
            dtpHasta.Enabled = chkHasta.Checked;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        private void Buscar()
        {
            try
            {
                DateTime? desde = chkDesde.Checked ? dtpDesde.Value.Date : (DateTime?)null;

                // El "hasta" se lleva al final del día: si no, filtrar hasta hoy
                // dejaría afuera todo lo de hoy, porque la hora sería 00:00.
                DateTime? hasta = chkHasta.Checked
                    ? dtpHasta.Value.Date.AddDays(1).AddSeconds(-1)
                    : (DateTime?)null;

                TipoOperacion? tipo = null;

                if (cboTipo.SelectedItem is TipoOperacion)
                    tipo = (TipoOperacion)cboTipo.SelectedItem;

                IList<Historial> registros = _servicio.BuscarBitacora(desde, hasta, txtActor.Text, tipo);

                dgvHistorial.DataSource = registros
                    .Select(h => new
                    {
                        Numero = h.Id,
                        Fecha = h.FechaOperacion.ToString("dd/MM/yyyy HH:mm:ss"),
                        Usuario = h.Actor,
                        Operacion = h.TipoDeOperacion.ToString(),
                        Locker = h.NumeroLocker.HasValue
                            ? h.NumeroLocker.Value.ToString() : "",
                        Orden = h.IdOrdenDeEntrega.HasValue
                            ? "#" + h.IdOrdenDeEntrega.Value : "",
                        Detalle = h.Detalle
                    })
                    .ToList();

                AjustarColumnas();
                PintarPorTipo();

                lblResumen.Text = registros.Count + " registro(s) encontrado(s).";
            }
            catch (Exception ex)
            {
                lblResumen.ForeColor = Color.Firebrick;
                lblResumen.Text = ex.Message;
            }
        }

        private void AjustarColumnas()
        {
            if (dgvHistorial.Columns.Contains("Detalle"))
                dgvHistorial.Columns["Detalle"].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.Fill;

            if (dgvHistorial.Columns.Contains("Numero"))
                dgvHistorial.Columns["Numero"].Width = 60;
        }

        /// <summary>Color por tipo de operación, para poder barrer la lista de un vistazo.</summary>
        private void PintarPorTipo()
        {
            foreach (DataGridViewRow fila in dgvHistorial.Rows)
            {
                if (fila.Cells["Operacion"].Value == null)
                    continue;

                switch (fila.Cells["Operacion"].Value.ToString())
                {
                    case "Login":
                        fila.DefaultCellStyle.ForeColor = Color.SteelBlue;
                        break;
                    case "Reserva":
                        fila.DefaultCellStyle.ForeColor = Color.DarkSlateBlue;
                        break;
                    case "Entrega":
                        fila.DefaultCellStyle.ForeColor = Color.SeaGreen;
                        break;
                    case "Retiro":
                        fila.DefaultCellStyle.ForeColor = Color.DarkGreen;
                        break;
                    case "Baja":
                        fila.DefaultCellStyle.ForeColor = Color.Firebrick;
                        break;
                }
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            chkDesde.Checked = false;
            chkHasta.Checked = false;
            txtActor.Clear();
            cboTipo.SelectedIndex = 0;
            lblResumen.ForeColor = Color.DimGray;
            Buscar();
        }
    }
}
