using LockersInteligentes.ArqBase.ArqBase;
using LockersInteligentes.ArqBase.Entidades;
using LockersInteligentes.ArqBase.Logica;
using LockersInteligentes.BLL;
using System;
using System.Windows.Forms;

namespace LockersInteligentes.UI
{
    public partial class FrmLogin : Form, IObservadorIdioma
    {
        private readonly SistemaLockersFacade _sistema = SistemaLockersFacade.Instancia;
        public Usuario UsuarioAutenticado { get; private set; }

        public FrmLogin()
        {
            InitializeComponent();
            GestorIdioma.Instancia.Suscribir(this);
            ActualizarIdioma();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            lblMensaje.Text = string.Empty;
            btnIngresar.Enabled = false;
            Cursor = Cursors.WaitCursor;

            try
            {
                ResultadoLogin resultado = _sistema.IniciarSesion(
                    txtUsuario.Text.Trim(), txtContrasenia.Text);

                if (resultado.Exitoso)
                {
                    UsuarioAutenticado = resultado.Usuario;
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    lblMensaje.Text = resultado.Mensaje;
                    txtContrasenia.Clear();
                    txtContrasenia.Focus();
                }
            }
            catch (Exception ex)
            {
                // El detalle técnico va al log; al usuario solo el mensaje general.
                ErrorLogService.Registrar(ex, "FrmLogin.btnIngresar_Click", txtUsuario.Text);
                lblMensaje.Text = "No se pudo conectar con el sistema. " +
                                  "Consultá el log de errores o contactá al administrador.";
            }
            finally
            {
                btnIngresar.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        private void chkMostrarContrasenia_CheckedChanged(object sender, EventArgs e)
        {
            txtContrasenia.UseSystemPasswordChar = !chkMostrarContrasenia.Checked;
        }
        private void lnkRegistrarse_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (FrmRegistro registro = new FrmRegistro())
            {
                if (registro.ShowDialog(this) == DialogResult.OK)
                {
                    txtUsuario.Text = registro.UsuarioCreado;
                    txtContrasenia.Focus();
                }
            }
        }

        private void lblSubtitulo_Click(object sender, EventArgs e)
        {

        }

        public void ActualizarIdioma()
        {
            GestorIdioma idioma = GestorIdioma.Instancia;

            Text = idioma.Traducir("FrmLogin.Caption");
            lblTitulo.Text = idioma.Traducir("FrmLogin.Titulo");
            lblSubtitulo.Text = idioma.Traducir("FrmLogin.Subtitulo");
            lblUsuario.Text = idioma.Traducir("Comun.Usuario");
            lblContrasenia.Text = idioma.Traducir("Comun.Contrasenia");
            chkMostrarContrasenia.Text = idioma.Traducir("FrmLogin.chkMostrar");
            btnIngresar.Text = idioma.Traducir("FrmLogin.btnIngresar");
            btnCancelar.Text = idioma.Traducir("Comun.Cancelar");
            lnkRegistrarse.Text = idioma.Traducir("FrmLogin.lnkRegistrar");
        }
        /// <summary>
        /// Se desuscribe al cerrarse. Es imprescindible: si no, el GestorIdioma
        /// conserva una referencia a una ventana destruida y al cambiar el idioma
        /// intentaría actualizar un formulario que ya no existe.
        /// </summary>
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            GestorIdioma.Instancia.Desuscribir(this);
            base.OnFormClosed(e);
        }
    }
}
