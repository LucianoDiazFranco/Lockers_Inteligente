using LockersInteligentes.ArqBase.Entidades;
using LockersInteligentes.ArqBase.Enums;
using LockersInteligentes.ArqBase.Logica;
using LockersInteligentes.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace LockersInteligentes.UI.Informacion
{
    public partial class FrmBitacora : Form, IObservadorIdioma
    {
        private const string TodasLasOperaciones = "(todas)";

        private readonly SistemaLockersFacade _servicio = SistemaLockersFacade.Instancia;
        public FrmBitacora()
        {
            InitializeComponent();
            GestorIdioma.Instancia.Suscribir(this);
        }
        private void FrmBitacora_Load(object sender, EventArgs e)
        {
            List<object> tipos = new List<object> { TodasLasOperaciones };

            foreach (TipoOperacion tipo in Enum.GetValues(typeof(TipoOperacion)))
                tipos.Add(new ItemTraducido
                {
                    Valor = tipo,
                    Texto = GestorIdioma.Instancia.TraducirEnum("TipoOperacion", tipo)
                });

            cboTipo.DataSource = tipos;

            dtpDesde.Value = DateTime.Today.AddDays(-7);
            dtpHasta.Value = DateTime.Today;

            Buscar();
            ActualizarIdioma();
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

                ItemTraducido seleccion = cboTipo.SelectedItem as ItemTraducido;

                if (seleccion != null)
                    tipo = (TipoOperacion)seleccion.Valor;

                IList<Historial> registros = _servicio.BuscarBitacora(desde, hasta, txtActor.Text, tipo);

                dgvHistorial.DataSource = registros
                    .Select(h => new
                    {
                        Numero = h.Id,
                        Fecha = h.FechaOperacion.ToString("dd/MM/yyyy HH:mm:ss"),
                        Usuario = h.Actor,
                        Operacion = GestorIdioma.Instancia.TraducirEnum(
                            "TipoOperacion", h.TipoDeOperacion),
                        Locker = h.NumeroLocker.HasValue
                            ? h.NumeroLocker.Value.ToString() : "",
                        Orden = h.IdOrdenDeEntrega.HasValue
                            ? "#" + h.IdOrdenDeEntrega.Value : "",
                        Detalle = h.Detalle
                    })
                    .ToList();

                AjustarColumnas();
                PintarPorTipo(registros);

                TraducirColumna("Numero", "FrmBitacora.colNumero");
                TraducirColumna("Fecha", "Comun.Fecha");
                TraducirColumna("Usuario", "Comun.Usuario");
                TraducirColumna("Operacion", "FrmBitacora.colOper");
                TraducirColumna("Locker", "Comun.Locker");
                TraducirColumna("Orden", "Comun.Orden");
                TraducirColumna("Detalle", "Comun.Detalle");

                lblResumen.Text = string.Format(
                GestorIdioma.Instancia.Traducir("FrmBitacora.Resumen"), registros.Count);
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
        private void PintarPorTipo(IList<Historial> registros)
        {
            for (int i = 0; i < dgvHistorial.Rows.Count && i < registros.Count; i++)
            {
                switch (registros[i].TipoDeOperacion)
                {
                    case TipoOperacion.Login:
                        dgvHistorial.Rows[i].DefaultCellStyle.ForeColor = Color.SteelBlue;
                        break;
                    case TipoOperacion.Reserva:
                        dgvHistorial.Rows[i].DefaultCellStyle.ForeColor = Color.DarkSlateBlue;
                        break;
                    case TipoOperacion.Entrega:
                        dgvHistorial.Rows[i].DefaultCellStyle.ForeColor = Color.SeaGreen;
                        break;
                    case TipoOperacion.Retiro:
                        dgvHistorial.Rows[i].DefaultCellStyle.ForeColor = Color.DarkGreen;
                        break;
                    case TipoOperacion.Baja:
                        dgvHistorial.Rows[i].DefaultCellStyle.ForeColor = Color.Firebrick;
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

        public void ActualizarIdioma()
        {
            GestorIdioma idioma = GestorIdioma.Instancia;

            Text = idioma.Traducir("FrmBitacora.Caption");
            lblTitulo.Text = idioma.Traducir("FrmBitacora.Titulo");
            grpFiltros.Text = idioma.Traducir("FrmBitacora.grpFiltros");
            chkDesde.Text = idioma.Traducir("Comun.Desde");
            chkHasta.Text = idioma.Traducir("Comun.Hasta");
            lblActor.Text = idioma.Traducir("Comun.Usuario");
            lblTipo.Text = idioma.Traducir("FrmBitacora.lblOper");
            btnBuscar.Text = idioma.Traducir("Comun.Buscar");
            btnLimpiar.Text = idioma.Traducir("Comun.Limpiar");

            // El combo se rearma porque sus textos vienen traducidos.
            int seleccion = cboTipo.SelectedIndex;

            List<object> tipos = new List<object> { idioma.Traducir("Comun.Todos") };

            foreach (TipoOperacion tipo in Enum.GetValues(typeof(TipoOperacion)))
                tipos.Add(new ItemTraducido
                {
                    Valor = tipo,
                    Texto = idioma.TraducirEnum("TipoOperacion", tipo)
                });

            cboTipo.DataSource = tipos;

            if (seleccion >= 0 && seleccion < cboTipo.Items.Count)
                cboTipo.SelectedIndex = seleccion;

            Buscar();
        }
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            GestorIdioma.Instancia.Desuscribir(this);
            base.OnFormClosed(e);
        }

        private void TraducirColumna(string nombreColumna, string clave)
        {
            if (dgvHistorial.Columns.Contains(nombreColumna))
                dgvHistorial.Columns[nombreColumna].HeaderText =
                    GestorIdioma.Instancia.Traducir(clave);
        }
    }
}
