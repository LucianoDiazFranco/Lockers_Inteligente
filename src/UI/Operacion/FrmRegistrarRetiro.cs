using LockersInteligentes.ArqBase.Logica;
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
    public partial class FrmRegistrarRetiro : Form, IObservadorIdioma
    {
        private readonly SistemaLockersFacade _sistema = SistemaLockersFacade.Instancia;
        private IList<OrdenDeEntrega> _pendientes = new List<OrdenDeEntrega>();
        public FrmRegistrarRetiro()
        {
            InitializeComponent();
            GestorIdioma.Instancia.Suscribir(this);
        }
        private void FrmRegistrarRetiro_Load(object sender, EventArgs e)
        {
            CargarPendientes();
            ActualizarIdioma();
        }

        private void CargarPendientes()
        {
            try
            {
                GestorIdioma idioma = GestorIdioma.Instancia;

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

                TraducirColumna("Orden", "Comun.Orden");
                TraducirColumna("Residente", "Comun.Residente");
                TraducirColumna("Piso", "Comun.Piso");
                TraducirColumna("Edificio", "Comun.Edificio");
                TraducirColumna("Locker", "Comun.Locker");
                TraducirColumna("Entregado", "FrmRetiro.colEntreg");
                TraducirColumna("Plazo", "FrmRetiro.colPlazo");

                PintarVencidos();

                if (_pendientes.Count == 0)
                    Mostrar(idioma.Traducir("FrmRetiro.SinPend"), false);

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

            GestorIdioma idioma = GestorIdioma.Instancia;

            return orden.EstaVencida()
                ? string.Format(idioma.Traducir("FrmRetiro.Vencido"), Math.Abs(dias.Value))
                : string.Format(idioma.Traducir("FrmRetiro.Quedan"), dias.Value);
        }
        private void PintarVencidos()
        {
            string marca = GestorIdioma.Instancia.Traducir("FrmRetiro.Vencido");

            // Se compara contra el inicio del texto traducido, antes del marcador,
            // para que funcione en cualquier idioma.
            int corte = marca.IndexOf('{');
            string prefijo = corte > 0 ? marca.Substring(0, corte) : marca;

            foreach (DataGridViewRow fila in dgvPendientes.Rows)
            {
                if (fila.Cells["Plazo"].Value != null &&
                    fila.Cells["Plazo"].Value.ToString().StartsWith(prefijo))
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
                Mostrar(GestorIdioma.Instancia.Traducir("Val.Paquete"), true);
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
            finally{
                btnRegistrar.Enabled = true;
                Cursor = Cursors.Default;
            }
        }
        private void MostrarResultado(OrdenDeEntrega orden)
        {
            GestorIdioma idioma = GestorIdioma.Instancia;

            lblResultado.Text =
                string.Format(idioma.Traducir("Res.OrdenCerrada"), orden.Id) +
                    Environment.NewLine + Environment.NewLine +
                idioma.Traducir("Res.Residente") + " " +
                    orden.ResidenteAsignado.NombreCompleto() + Environment.NewLine +
                idioma.Traducir("Res.Locker") + " " + orden.LockerAsignado.Numero + " " +
                    string.Format(idioma.Traducir("Res.Modulo"),
                                  orden.LockerAsignado.GrupoLocker) + Environment.NewLine +
                idioma.Traducir("Res.Entregado") + " " +
                    orden.FechaEntrega.Value.ToString("dd/MM/yyyy HH:mm") + Environment.NewLine +
                idioma.Traducir("Res.Retirado") + " " +
                    orden.FechaRetiro.Value.ToString("dd/MM/yyyy HH:mm") + Environment.NewLine +
                idioma.Traducir("Res.Estado") + " " +
                    idioma.TraducirEnum("EstadoOrden", orden.Estado) +
                    Environment.NewLine + Environment.NewLine +
                string.Format(idioma.Traducir("Res.LockerLibre"), orden.LockerAsignado.Numero);

            grpResultado.Visible = true;
            Mostrar(idioma.Traducir("Msg.RetiroOk"), false);
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

            Text = idioma.Traducir("FrmRetiro.Caption");
            lblTitulo.Text = idioma.Traducir("FrmRetiro.Caption");
            lblAyuda.Text = idioma.Traducir("FrmRetiro.Ayuda");
            lblPin.Text = idioma.Traducir("FrmRetiro.lblPin");
            btnRegistrar.Text = idioma.Traducir("FrmRetiro.btnReg");
            btnActualizar.Text = idioma.Traducir("Comun.Actualizar");
            grpResultado.Text = idioma.Traducir("FrmRetiro.grpRes");

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
