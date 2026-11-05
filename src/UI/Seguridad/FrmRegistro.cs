using System;
using System.Windows.Forms;
using LockersInteligentes.BLL;
using LockersInteligentes.ArqBase.Entidades;
using LockersInteligentes.ArqBase.Logica;

namespace LockersInteligentes.UI
{
    public partial class FrmRegistro : Form, IObservadorIdioma
    {
        private readonly SistemaLockersFacade _sistema = SistemaLockersFacade.Instancia;
        public string UsuarioCreado { get; private set; }

        public FrmRegistro()
        {
            InitializeComponent();
            GestorIdioma.Instancia.Suscribir(this);
            ActualizarIdioma();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            lblMensaje.Text = string.Empty;

            if (txtPassword.Text != txtPasswordRepetida.Text)
            {
                lblMensaje.Text = "Las contraseñas no coinciden.";
                txtPasswordRepetida.Clear();
                txtPasswordRepetida.Focus();
                return;
            }

            btnRegistrar.Enabled = false;
            Cursor = Cursors.WaitCursor;

            try
            {
                _sistema.RegistrarOperador(
                    txtNombreUsuario.Text.Trim(),
                    txtNombre.Text.Trim(),
                    txtApellido.Text.Trim(),
                    txtCorreo.Text.Trim(),
                    txtPassword.Text);

                UsuarioCreado = txtNombreUsuario.Text.Trim();

                MessageBox.Show("Cuenta creada correctamente. Ya podés iniciar sesión.",
                    "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
            }
            finally
            {
                btnRegistrar.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {

        }

        public void ActualizarIdioma()
        {
            GestorIdioma idioma = GestorIdioma.Instancia;

            Text = idioma.Traducir("FrmRegistro.Caption");
            lblTitulo.Text = idioma.Traducir("FrmRegistro.Titulo");
            lblSubtitulo.Text = idioma.Traducir("FrmRegistro.Subtitulo");
            lblNombre.Text = idioma.Traducir("Comun.Nombre");
            lblApellido.Text = idioma.Traducir("Comun.Apellido");
            lblCorreo.Text = idioma.Traducir("Comun.Correo");
            lblNombreUsuario.Text = idioma.Traducir("FrmRegistro.lblUsuario");
            lblPassword.Text = idioma.Traducir("Comun.Contrasenia");
            lblPasswordRepetida.Text = idioma.Traducir("FrmRegistro.lblRepetir");
            lblAyuda.Text = idioma.Traducir("FrmRegistro.Ayuda");
            btnRegistrar.Text = idioma.Traducir("FrmRegistro.btnCrear");
            btnCancelar.Text = idioma.Traducir("Comun.Cancelar");
        }
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            GestorIdioma.Instancia.Desuscribir(this);
            base.OnFormClosed(e);
        }
    }
}