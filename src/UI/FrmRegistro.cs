using System;
using System.Windows.Forms;
using LockersInteligentes.BLL;

namespace LockersInteligentes.UI
{
    /// <summary>
    /// CU.Seg.002 Registrar Operador. Se abre desde el login, sin sesion activa.
    /// El rol no se elige: lo fija el service.
    /// </summary>
    public partial class FrmRegistro : Form
    {
        private readonly UsuarioService _servicio = new UsuarioService();

        /// <summary>Nombre de usuario recien creado, para precargarlo en el login.</summary>
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
                _servicio.RegistrarOperador(
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
    }
}