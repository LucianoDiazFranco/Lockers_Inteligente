using LockersInteligentes.ArqBase.ArqBase;
using LockersInteligentes.ArqBase.Entidades;
using LockersInteligentes.ArqBase.Logica;
using LockersInteligentes.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
    }
}
