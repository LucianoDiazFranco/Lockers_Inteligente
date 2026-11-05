using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using LockersInteligentes.ArqBase.Logica;
using LockersInteligentes.BLL;
using LockersInteligentes.Dominio.Entidades;
using LockersInteligentes.Dominio.Enums;

namespace LockersInteligentes.UI
{
    public partial class FrmReservarLocker : Form, IObservadorIdioma
    {
        private readonly SistemaLockersFacade _sistema = SistemaLockersFacade.Instancia;
        private IList<Residente> _residentes = new List<Residente>();

        public FrmReservarLocker()
        {
            InitializeComponent();
            GestorIdioma.Instancia.Suscribir(this);
        }

        private void FrmReservarLocker_Load(object sender, EventArgs e)
        {
            try
            {
                _residentes = _sistema.ResidentesActivos();

                cboResidente.DisplayMember = "Texto";
                cboResidente.ValueMember = "Id";

                cboResidente.DataSource = _residentes
                    .Select(r => new
                    {
                        r.Id,
                        Texto = r.Apellido + ", " + r.Nombre + "  -  " + r.Piso
                    })
                    .ToList();
                cboTamanio.DataSource = ItemTraducido.DesdeEnum(
                    typeof(TamanioPaquete), "Tamanio");

                MostrarEdificio();
                ActualizarIdioma();
            }
            catch (Exception ex)
            {
                Mostrar(ex.Message, true);
            }
        }

        private void cboResidente_SelectedIndexChanged(object sender, EventArgs e)
        {
            MostrarEdificio();
        }
        private void MostrarEdificio()
        {
            Residente residente = ResidenteSeleccionado();

            lblEdificio.Text = residente != null
             ? string.Format(GestorIdioma.Instancia.Traducir("FrmReservar.lblEdif"),
                    residente.Edificio.Nombre)
            :string.Empty;
        }

        private Residente ResidenteSeleccionado()
        {
            if (cboResidente.SelectedValue == null)
                return null;

            int id = Convert.ToInt32(cboResidente.SelectedValue);
            return _residentes.FirstOrDefault(r => r.Id == id);
        }

        private void btnReservar_Click(object sender, EventArgs e)
        {
            Residente residente = ResidenteSeleccionado();

            Mostrar(GestorIdioma.Instancia.Traducir("Val.Residente"), true);

            lblMensaje.Text = string.Empty;
            btnReservar.Enabled = false;
            Cursor = Cursors.WaitCursor;


            try
            {
                ItemTraducido item = (ItemTraducido)cboTamanio.SelectedItem;
                TamanioPaquete tamanio = (TamanioPaquete)item.Valor;

                ResultadoReserva resultado = _sistema.ReservarLocker(
                    residente.Id, tamanio, txtDescripcion.Text.Trim());

                MostrarResultado(resultado);
            }
            catch (Exception ex)
            {
                grpResultado.Visible = false;
                Mostrar(ex.Message, true);
            }
            finally
            {
                btnReservar.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private void MostrarResultado(ResultadoReserva resultado)
        {
            OrdenDeEntrega orden = resultado.Orden;
            GestorIdioma idioma = GestorIdioma.Instancia;

            lblResultado.Text =
                string.Format(idioma.Traducir("Res.Orden"), orden.Id) +
                    Environment.NewLine + Environment.NewLine +
                idioma.Traducir("Res.Residente") + " " +
                    orden.ResidenteAsignado.NombreCompleto() + Environment.NewLine +
                idioma.Traducir("Res.Edificio") + " " +
                    orden.LockerAsignado.Edificio.Nombre + Environment.NewLine +
                idioma.Traducir("Res.Locker") + " " + orden.LockerAsignado.Numero + " " +
                    string.Format(idioma.Traducir("Res.Modulo"),
                                  orden.LockerAsignado.GrupoLocker) + Environment.NewLine +
                idioma.Traducir("Res.Tamanio") + " " +
                    idioma.TraducirEnum("Tamanio", orden.LockerAsignado.Tamanio) + "   |   " +
                idioma.Traducir("Res.Paquete") + " " +
                    idioma.TraducirEnum("Tamanio", orden.TamanioPaquete) + Environment.NewLine +
                idioma.Traducir("Res.Estado") + " " +
                    idioma.TraducirEnum("EstadoOrden", orden.Estado);

            lblPin.Text = resultado.PinApertura;
            grpResultado.Visible = true;

            Mostrar(idioma.Traducir("Msg.ReservaOk"), false);
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtDescripcion.Clear();
            grpResultado.Visible = false;
            lblMensaje.Text = string.Empty;
            cboResidente.Focus();
        }

        private void Mostrar(string mensaje, bool esError)
        {
            lblMensaje.ForeColor = esError ? Color.Firebrick : Color.SeaGreen;
            lblMensaje.Text = mensaje;
        }

        public void ActualizarIdioma()
        {
            GestorIdioma idioma = GestorIdioma.Instancia;

            Text = idioma.Traducir("FrmReservar.Caption");
            lblTitulo.Text = idioma.Traducir("FrmReservar.Caption");
            lblResidente.Text = idioma.Traducir("Comun.Residente");
            lblTamanio.Text = idioma.Traducir("FrmReservar.lblTam");
            lblDescripcion.Text = idioma.Traducir("FrmReservar.lblDesc");
            btnReservar.Text = idioma.Traducir("FrmReservar.btnRes");
            btnLimpiar.Text = idioma.Traducir("Comun.Limpiar");
            grpResultado.Text = idioma.Traducir("FrmReservar.grpRes");
            lblAvisoPin.Text = idioma.Traducir("FrmReservar.AvisoPin");

            int seleccion = cboTamanio.SelectedIndex;
            cboTamanio.DataSource = ItemTraducido.DesdeEnum(typeof(TamanioPaquete), "Tamanio");

            if (seleccion >= 0 && seleccion < cboTamanio.Items.Count)
                cboTamanio.SelectedIndex = seleccion;

            MostrarEdificio();
        }
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            GestorIdioma.Instancia.Desuscribir(this);
            base.OnFormClosed(e);
        }
    }
}