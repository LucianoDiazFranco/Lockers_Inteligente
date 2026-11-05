using LockersInteligentes.ArqBase.Logica;
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
    public partial class FrmReportes : Form, IObservadorIdioma
    {
        private readonly SistemaLockersFacade _servicio = SistemaLockersFacade.Instancia;
        private IList<Reporte> _reportes = new List<Reporte>();
        public FrmReportes()
        {
            InitializeComponent();
            GestorIdioma.Instancia.Suscribir(this);
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
                ActualizarIdioma();
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
                Mostrar(GestorIdioma.Instancia.Traducir("FrmReportes.ValEdif"), true);
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
                Mostrar(string.Format(
                GestorIdioma.Instancia.Traducir("FrmReportes.OkGenerado"), reporte.Id), false);
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
            lblEncabezado.Text = string.Format(
                GestorIdioma.Instancia.Traducir("FrmReportes.Encabezado"),
                reporte.Edificio.Nombre,
                reporte.Periodo,
                reporte.FechaGeneracion.ToString("dd/MM/yyyy HH:mm"));
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

                TraducirColumna("Numero", "FrmReportes.colNumero");
                TraducirColumna("Edificio", "Comun.Edificio");
                TraducirColumna("Periodo", "Comun.Periodo");
                TraducirColumna("Generado", "FrmReportes.colGenerado");
                TraducirColumna("Ocupacion", "FrmReportes.colOcup");
                TraducirColumna("Vencidos", "FrmReportes.colVenc");

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

        public void ActualizarIdioma()
        {
            GestorIdioma idioma = GestorIdioma.Instancia;

            Text = idioma.Traducir("FrmReportes.Caption");
            lblTitulo.Text = idioma.Traducir("FrmReportes.Caption");
            grpGenerar.Text = idioma.Traducir("FrmReportes.grpGen");
            lblEdificio.Text = idioma.Traducir("Comun.Edificio");
            lblDesde.Text = idioma.Traducir("Comun.Desde");
            lblHasta.Text = idioma.Traducir("Comun.Hasta");
            btnGenerar.Text = idioma.Traducir("FrmReportes.btnGen");
            grpResultado.Text = idioma.Traducir("FrmReportes.grpRes");
            lblOcupacionTitulo.Text = idioma.Traducir("FrmReportes.lblOcup");
            lblPertenenciaTitulo.Text = idioma.Traducir("FrmReportes.lblPert");
            lblVencidosTitulo.Text = idioma.Traducir("FrmReportes.lblVenc");
            lblRankRepTitulo.Text = idioma.Traducir("FrmReportes.lblRankRep");
            lblRankLockTitulo.Text = idioma.Traducir("FrmReportes.lblRankLoc");
            lblHistorialTitulo.Text = idioma.Traducir("FrmReportes.lblHist");

            CargarHistorial();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            GestorIdioma.Instancia.Desuscribir(this);
            base.OnFormClosed(e);
        }

        private void TraducirColumna(string nombreColumna, string clave)
        {
            if (dgvReportes.Columns.Contains(nombreColumna))
                dgvReportes.Columns[nombreColumna].HeaderText =
                    GestorIdioma.Instancia.Traducir(clave);
        }
    }
}
