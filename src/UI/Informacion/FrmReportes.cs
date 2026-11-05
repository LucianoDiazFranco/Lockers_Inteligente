using LockersInteligentes.BLL;
using LockersInteligentes.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace LockersInteligentes.UI.Informacion
{
    public partial class FrmReportes : Form
    {
        private readonly SistemaLockersFacade _servicio = SistemaLockersFacade.Instancia;
        private IList<Reporte> _reportes = new List<Reporte>();
        public FrmReportes()
        {
            InitializeComponent();
        }

        private void FrmReportes_Load(object sender, EventArgs e)
        {
            try
            {
                cboEdificio.DisplayMember = "Nombre";
                cboEdificio.ValueMember = "Id";
                cboEdificio.DataSource = _servicio.ListarEdificios();

                // Por defecto, el mes corriente.
                DateTime hoy = DateTime.Today;
                dtpDesde.Value = new DateTime(hoy.Year, hoy.Month, 1);
                dtpHasta.Value = hoy;

                CargarHistorial();
            }
            catch (Exception ex)
            {
                Mostrar(ex.Message, true);
            }
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            if (cboEdificio.SelectedValue == null)
            {
                Mostrar("Seleccioná un edificio.", true);
                return;
            }

            btnGenerar.Enabled = false;
            Cursor = Cursors.WaitCursor;

            try
            {
                Reporte reporte = _servicio.GenerarReporte(
                    Convert.ToInt32(cboEdificio.SelectedValue),
                    dtpDesde.Value,
                    dtpHasta.Value);

                MostrarReporte(reporte);
                CargarHistorial();
                Mostrar("Reporte N° " + reporte.Id + " generado y guardado.", false);
            }
            catch (Exception ex)
            {
                grpResultado.Visible = false;
                Mostrar(ex.Message, true);
            }
            finally
            {
                btnGenerar.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private void MostrarReporte(Reporte reporte)
        {
            lblEncabezado.Text = reporte.Edificio.Nombre + "   |   Período " + reporte.Periodo +
                                 "   |   Generado el " +
                                 reporte.FechaGeneracion.ToString("dd/MM/yyyy HH:mm");

            lblOcupacion.Text = reporte.PorcentajeOcupacionLocker.ToString("0.##") + "%";
            lblPertenencia.Text = reporte.PorcentajePertenenciaLocker.ToString("0.##") + "%";
            lblVencidos.Text = reporte.PaqueteVencido.ToString();

            lblRankRepartidores.Text = reporte.RankingRepartidores;
            lblRankLockers.Text = reporte.RankingTopLockers;

            lblVencidos.ForeColor = reporte.PaqueteVencido > 0
                ? Color.Red
                : Color.White;

            grpResultado.Visible = true;
        }

        private void CargarHistorial()
        {
            try
            {
                _reportes = _servicio.ListarReportes();

                dgvReportes.DataSource = _reportes
                    .Select(r => new
                    {
                        r.Id,
                        Numero = r.Id,
                        Edificio = r.Edificio.Nombre,
                        r.Periodo,
                        Generado = r.FechaGeneracion.ToString("dd/MM/yyyy HH:mm"),
                        Ocupacion = r.PorcentajeOcupacionLocker.ToString("0.##") + "%",
                        Vencidos = r.PaqueteVencido
                    })
                    .ToList();

                if (dgvReportes.Columns.Contains("Id"))
                    dgvReportes.Columns["Id"].Visible = false;
            }
            catch (Exception ex)
            {
                Mostrar(ex.Message, true);
            }
        }

        /// <summary>
        /// Al seleccionar un reporte viejo se muestra tal como se guardó.
        /// No se recalcula: un reporte es una foto de su momento.
        /// </summary>
        private void dgvReportes_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvReportes.CurrentRow == null || dgvReportes.CurrentRow.Index < 0)
                return;

            object valor = dgvReportes.CurrentRow.Cells["Id"].Value;

            if (valor == null)
                return;

            int id = Convert.ToInt32(valor);
            Reporte reporte = _reportes.FirstOrDefault(r => r.Id == id);

            if (reporte != null)
                MostrarReporte(reporte);
        }

        private void Mostrar(string mensaje, bool esError)
        {
            lblMensaje.ForeColor = esError ? Color.Firebrick : Color.SeaGreen;
            lblMensaje.Text = mensaje;
        }

        private void lblRankRepTitulo_Click(object sender, EventArgs e)
        {

        }
    }
}
