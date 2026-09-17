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
    public partial class FrmUsuarios : Form
    {
        private readonly UsuarioService _servicio = new UsuarioService();
        private IList<Usuario> _usuarios = new List<Usuario>();
        private int _idSeleccionado;

        public FrmUsuarios()
        {
            InitializeComponent();
        }
        private void FrmUsuarios_Load(object sender, EventArgs e)
        {
            try
            {
                cboRol.DataSource = _servicio.ListarRoles();
                cboRol.DisplayMember = "Nombre";
                cboRol.ValueMember = "Id";

                CargarGrilla();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                Mostrar(ex.Message, true);
            }
        }
        private void CargarGrilla()
        {
            _usuarios = _servicio.Listar();

            dgvUsuarios.DataSource = _usuarios
                .Select(u => new
                {
                    u.Id,
                    Usuario = u.NombreUsuario,
                    Nombre = u.Apellido + ", " + u.Nombre,
                    Rol = u.Rol.Nombre,
                    u.Activo
                })
                .ToList();

            if (dgvUsuarios.Columns.Contains("Id"))
                dgvUsuarios.Columns["Id"].Visible = false;
        }

        private void dgvUsuarios_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvUsuarios.CurrentRow == null || dgvUsuarios.CurrentRow.Index < 0)
                return;

            object valor = dgvUsuarios.CurrentRow.Cells["Id"].Value;

            if (valor == null)
                return;

            _idSeleccionado = Convert.ToInt32(valor);
            Usuario usuario = _usuarios.FirstOrDefault(u => u.Id == _idSeleccionado);

            if (usuario == null)
                return;

            txtNombreUsuario.Text = usuario.NombreUsuario;
            txtNombre.Text = usuario.Nombre;
            txtApellido.Text = usuario.Apellido;
            cboRol.SelectedValue = usuario.Rol.Id;
            txtPassword.Clear();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            txtNombreUsuario.Focus();
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                int idRol = Convert.ToInt32(cboRol.SelectedValue);

                if (_idSeleccionado == 0)
                {
                    _servicio.Crear(txtNombreUsuario.Text, txtNombre.Text,
                                    txtApellido.Text, txtPassword.Text, idRol);
                    Mostrar("Usuario creado correctamente.", false);
                }
                else
                {
                    Usuario usuario = _usuarios.First(u => u.Id == _idSeleccionado);
                    usuario.NombreUsuario = txtNombreUsuario.Text.Trim();
                    usuario.Nombre = txtNombre.Text.Trim();
                    usuario.Apellido = txtApellido.Text.Trim();
                    usuario.Rol = (Rol)cboRol.SelectedItem;

                    _servicio.Modificar(usuario);

                    if (!string.IsNullOrWhiteSpace(txtPassword.Text))
                        _servicio.RestablecerPassword(_idSeleccionado, txtPassword.Text);

                    Mostrar("Usuario actualizado correctamente.", false);
                }

                CargarGrilla();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                Mostrar(ex.Message, true);
            }
        }
        private void btnDesactivar_Click(object sender, EventArgs e)
        {
            if (_idSeleccionado == 0)
            {
                Mostrar("Seleccioná un usuario de la lista.", true);
                return;
            }

            if (MessageBox.Show("¿Desactivar el usuario seleccionado?", "Confirmar",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                _servicio.Desactivar(_idSeleccionado);
                Mostrar("Usuario desactivado.", false);
                CargarGrilla();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                Mostrar(ex.Message, true);
            }
        }
        private void LimpiarCampos()
        {
            _idSeleccionado = 0;
            txtNombreUsuario.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            txtPassword.Clear();
            dgvUsuarios.ClearSelection();

            if (cboRol.Items.Count > 0)
                cboRol.SelectedIndex = 0;
        }
        private void Mostrar(string mensaje, bool esError)
        {
            lblMensaje.ForeColor = esError ? Color.Firebrick : Color.SeaGreen;
            lblMensaje.Text = mensaje;
        }
    }
}
