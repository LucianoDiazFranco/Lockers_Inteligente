using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using LockersInteligentes.BLL;
using LockersInteligentes.Dominio.Entidades;
using LockersInteligentes.Dominio.Enums;

namespace LockersInteligentes.UI
{
    public partial class FrmReservarLocker : Form
    {
        private readonly EntregaService _servicio = new EntregaService();
        private IList<Residente> _residentes = new List<Residente>();

        public FrmReservarLocker()
        {
            InitializeComponent();
        }

        private void FrmReservarLocker_Load(object sender, EventArgs e)
        {
            try
            {
                _residentes = _servicio.ListarResidentes();

                cboResidente.DisplayMember = "Texto";
                cboResidente.ValueMember = "Id";

                cboResidente.DataSource = _residentes
                    .Select(r => new
                    {
                        r.Id,
                        Texto = r.Apellido + ", " + r.Nombre + "  -  " + r.Piso
                    })
                    .ToList();
                cboResidente.DisplayMember = "Texto";
                cboResidente.ValueMember = "Id";
                cboTamanio.DataSource = Enum.GetValues(typeof(TamanioPaquete));

                MostrarEdificio();
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
                ? "Edificio: " + residente.Edificio.Nombre
                : string.Empty;
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

            if (residente == null)
            {
                Mostrar("Seleccioná un residente.", true);
                return;
            }

            lblMensaje.Text = string.Empty;
            btnReservar.Enabled = false;
            Cursor = Cursors.WaitCursor;

            try
            {
                TamanioPaquete tamanio = (TamanioPaquete)cboTamanio.SelectedItem;

                ResultadoReserva resultado = _servicio.ReservarLocker(
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

            lblResultado.Text =
                "Orden N° " + orden.Id + Environment.NewLine + Environment.NewLine +
                "Residente: " + orden.ResidenteAsignado.NombreCompleto() + Environment.NewLine +
                "Edificio:  " + orden.LockerAsignado.Edificio.Nombre + Environment.NewLine +
                "Locker:    " + orden.LockerAsignado.Numero +
                    "  (módulo " + orden.LockerAsignado.GrupoLocker + ")" + Environment.NewLine +
                "Tamaño:    " + orden.LockerAsignado.Tamanio +
                    "   |   Paquete: " + orden.TamanioPaquete + Environment.NewLine +
                "Estado:    " + orden.Estado;

            lblPin.Text = resultado.PinApertura;

            // El PIN se muestra acá porque es la única oportunidad: en la base solo
            // queda su hash. Si el mail no salió, esta pantalla es el único lugar
            // del que se puede recuperar.
            lblAvisoPin.Text = resultado.NotificacionEnviada
                ? "PIN de apertura enviado por correo al residente. Se muestra acá solo como respaldo."
                : "No se pudo enviar el correo (" + resultado.MotivoFalloNotificacion +
                  "). Anotá el PIN: no se puede volver a consultar.";

            lblAvisoPin.ForeColor = resultado.NotificacionEnviada ? Color.Gray : Color.Firebrick;
            grpResultado.Visible = true;

            Mostrar("Reserva registrada correctamente.", false);
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
    }
}