using System;
using System.Windows.Forms;
using LockersInteligentes.BLL;
using LockersInteligentes.ArqBase.Entidades;
using LockersInteligentes.ArqBase.Logica;

namespace LockersInteligentes.UI
{
    public partial class FrmRegistro : Form
    {
        private readonly SistemaLockersFacade _sistema = SistemaLockersFacade.Instancia;
        public string UsuarioCreado { get; private set; }

        public FrmRegistro()
        {
            InitializeComponent();
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
    }
}