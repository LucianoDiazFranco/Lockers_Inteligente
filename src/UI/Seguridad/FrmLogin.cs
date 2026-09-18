using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LockersInteligentes.BLL;
using LockersInteligentes.Dominio.Entidades;

namespace LockersInteligentes.UI
{
    public partial class FrmLogin : Form
    {
        private readonly LoginService _loginService = new LoginService();
        public Usuario UsuarioAutenticado { get; private set; }

        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            lblMensaje.Text = string.Empty;
            btnIngresar.Enabled = false;
            Cursor = Cursors.WaitCursor;

            try
            {
                ResultadoLogin resultado = _loginService.IniciarSesion(
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
                lblMensaje.Text = "Ocurrio un error inesperado.";
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}
