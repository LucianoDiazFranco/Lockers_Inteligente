using LockersInteligentes.BLL;
using LockersInteligentes.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LockersInteligentes.UI.Gestion
{
    public partial class FrmResidentes : Form
    {
        private readonly ResidenteService _servicio = new ResidenteService();
        private readonly LockerService _lockerService = new LockerService();

        private IList<Residente> _residentes = new List<Residente>();
        private int _idSeleccionado;
        public FrmResidentes()
        {
            InitializeComponent();
        }
        private void FrmResidentes_Load(object sender, EventArgs e)
        {
            try
            {
                cboFiltroEdificio.DisplayMember = "Nombre";
                cboFiltroEdificio.ValueMember = "Id";
                cboFiltroEdificio.DataSource = _lockerService.ListarEdificios();

                CargarGrilla();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                Mostrar(ex.Message, true);
            }
        }

        private void cboFiltroEdificio_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarGrilla();
            LimpiarCampos();
        }

        private void CargarGrilla()
        {
            if (cboFiltroEdificio.SelectedValue == null)
                return;

            try
            {
                _residentes = _servicio.ListarPorEdificio(
                    Convert.ToInt32(cboFiltroEdificio.SelectedValue));

                dgvResidentes.DataSource = _residentes
                    .Select(r => new
                    {
                        r.Id,
                        Apellido = r.Apellido,
                        Nombre = r.Nombre,
                        r.Dni,
                        r.Correo,
                        r.Piso,
                        r.Telefono,
                        Estado = r.Activo ? "Activo" : "Baja"
                    })
                    .ToList();

                if (dgvResidentes.Columns.Contains("Id"))
                    dgvResidentes.Columns["Id"].Visible = false;

                PintarInactivos();
            }
            catch (Exception ex)
            {
                Mostrar(ex.Message, true);
            }
        }

        /// <summary>Los dados de baja en gris, para distinguirlos de un vistazo.</summary>
        private void PintarInactivos()
        {
            foreach (DataGridViewRow fila in dgvResidentes.Rows)
            {
                if (fila.Cells["Estado"].Value != null &&
                    fila.Cells["Estado"].Value.ToString() == "Baja")
                {
                    fila.DefaultCellStyle.ForeColor = Color.Gray;
                    fila.DefaultCellStyle.BackColor = Color.WhiteSmoke;
                }
            }
        }

        private void dgvResidentes_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvResidentes.CurrentRow == null || dgvResidentes.CurrentRow.Index < 0)
                return;

            object valor = dgvResidentes.CurrentRow.Cells["Id"].Value;

            if (valor == null)
                return;

            _idSeleccionado = Convert.ToInt32(valor);
            Residente residente = _residentes.FirstOrDefault(r => r.Id == _idSeleccionado);

            if (residente == null)
                return;

            txtNombre.Text = residente.Nombre;
            txtApellido.Text = residente.Apellido;
            txtDni.Text = residente.Dni;
            txtCorreo.Text = residente.Correo;
            txtPiso.Text = residente.Piso;
            txtTelefono.Text = residente.Telefono;

            btnDesactivar.Enabled = residente.Activo;
            btnReactivar.Enabled = !residente.Activo;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            txtNombre.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                int idEdificio = Convert.ToInt32(cboFiltroEdificio.SelectedValue);

                if (_idSeleccionado == 0)
                {
                    _servicio.Crear(idEdificio, txtNombre.Text, txtApellido.Text, txtDni.Text,
                                    txtCorreo.Text, txtPiso.Text, txtTelefono.Text);
                    Mostrar("Residente creado correctamente.", false);
                }
                else
                {
                    Residente residente = _residentes.First(r => r.Id == _idSeleccionado);
                    residente.Nombre = txtNombre.Text.Trim();
                    residente.Apellido = txtApellido.Text.Trim();
                    residente.Dni = txtDni.Text.Trim();
                    residente.Correo = txtCorreo.Text.Trim();
                    residente.Piso = string.IsNullOrWhiteSpace(txtPiso.Text)
                        ? null : txtPiso.Text.Trim();
                    residente.Telefono = string.IsNullOrWhiteSpace(txtTelefono.Text)
                        ? null : txtTelefono.Text.Trim();

                    _servicio.Modificar(residente);
                    Mostrar("Residente actualizado correctamente.", false);
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
                Mostrar("Seleccioná un residente de la lista.", true);
                return;
            }

            if (MessageBox.Show("¿Dar de baja al residente seleccionado?", "Confirmar",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                // Si tiene paquetes pendientes, el service corta acá con el motivo.
                _servicio.Desactivar(_idSeleccionado);
                Mostrar("Residente dado de baja. Su historial de paquetes se conserva.", false);
                CargarGrilla();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                Mostrar(ex.Message, true);
            }
        }

        private void btnReactivar_Click(object sender, EventArgs e)
        {
            if (_idSeleccionado == 0)
            {
                Mostrar("Seleccioná un residente de la lista.", true);
                return;
            }

            try
            {
                _servicio.Reactivar(_idSeleccionado);
                Mostrar("Residente reactivado.", false);
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
            txtNombre.Clear();
            txtApellido.Clear();
            txtDni.Clear();
            txtCorreo.Clear();
            txtPiso.Clear();
            txtTelefono.Clear();
            btnDesactivar.Enabled = false;
            btnReactivar.Enabled = false;
            dgvResidentes.ClearSelection();
        }

        private void Mostrar(string mensaje, bool esError)
        {
            lblMensaje.ForeColor = esError ? Color.Firebrick : Color.SeaGreen;
            lblMensaje.Text = mensaje;
        }
    }
}
