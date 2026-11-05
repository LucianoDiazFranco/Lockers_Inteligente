using LockersInteligentes.ArqBase.Entidades;
using LockersInteligentes.ArqBase.Logica;
using LockersInteligentes.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace LockersInteligentes.UI
{
    public partial class FrmUsuarios : Form, IObservadorIdioma
    {
        private readonly SistemaLockersFacade _sistema = SistemaLockersFacade.Instancia;
        private IList<Usuario> _usuarios = new List<Usuario>();
        private int _idSeleccionado;

        public FrmUsuarios()
        {
            InitializeComponent();
            GestorIdioma.Instancia.Suscribir(this);
        }
        private void FrmUsuarios_Load(object sender, EventArgs e)
        {
            try
            {
                cboRol.DataSource = _sistema.ListarRoles();
                cboRol.DisplayMember = "Nombre";
                cboRol.ValueMember = "Id";

                CargarGrilla();
                LimpiarCampos();
                ActualizarIdioma();
            }
            catch (Exception ex)
            {
                Mostrar(ex.Message, true);
            }
        }
        private void CargarGrilla()
        {
            _usuarios = _sistema.ListarUsuarios();
            GestorIdioma idioma = GestorIdioma.Instancia;

            dgvUsuarios.DataSource = _usuarios
                .Select(u => new
                {
                    u.Id,
                    Usuario = u.NombreUsuario,
                    Nombre = u.Nombre,
                    Apellido = u.Apellido,
                    Rol = u.Rol.Nombre,
                    u.Activo
                })
                .ToList();

            if (dgvUsuarios.Columns.Contains("Id"))
                dgvUsuarios.Columns["Id"].Visible = false;

            TraducirColumna("Usuario", "Comun.Usuario");
            TraducirColumna("Nombre", "FrmUsuarios.colNombre");
            TraducirColumna("Rol", "Comun.Rol");
            TraducirColumna("Activo", "Comun.Activo");
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
                    _sistema.CrearUsuario(txtNombreUsuario.Text, txtNombre.Text,
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

                    _sistema.ModificarUsuario(usuario);

                    if (!string.IsNullOrWhiteSpace(txtPassword.Text))
                        _sistema.RestablecerPassword(_idSeleccionado, txtPassword.Text);

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
                _sistema.DesactivarUsuario(_idSeleccionado);
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

        private void dgvUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public void ActualizarIdioma()
        {
            GestorIdioma idioma = GestorIdioma.Instancia;

            Text = idioma.Traducir("FrmUsuarios.Caption");
            lblNombreUsuario.Text = idioma.Traducir("Comun.Usuario");
            lblNombre.Text = idioma.Traducir("Comun.Nombre");
            lblApellido.Text = idioma.Traducir("Comun.Apellido");
            lblRol.Text = idioma.Traducir("Comun.Rol");
            lblPassword.Text = idioma.Traducir("Comun.Contrasenia");
            lblAyudaPassword.Text = idioma.Traducir("FrmUsuarios.Ayuda");
            btnNuevo.Text = idioma.Traducir("Comun.Nuevo");
            btnGuardar.Text = idioma.Traducir("Comun.Guardar");
            btnDesactivar.Text = idioma.Traducir("FrmUsuarios.btnDesact");

            CargarGrilla();
        }
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            GestorIdioma.Instancia.Desuscribir(this);
            base.OnFormClosed(e);
        }
        private void TraducirColumna(string nombreColumna, string clave)
        {
            if (dgvUsuarios.Columns.Contains(nombreColumna))
                dgvUsuarios.Columns[nombreColumna].HeaderText =
                    GestorIdioma.Instancia.Traducir(clave);
        }
    }
}
