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
    public partial class FrmEdificios : Form
    {
        private readonly EdificioService _servicio = new EdificioService();
        private IList<Edificio> _edificios = new List<Edificio>();
        private int _idSeleccionado;
        public FrmEdificios()
        {
            InitializeComponent();
        }
        private void FrmEdificios_Load(object sender, EventArgs e)
        {
            CargarGrilla();
            LimpiarCampos();
        }

        private void CargarGrilla()
        {
            try
            {
                _edificios = _servicio.Listar();

                dgvEdificios.DataSource = _edificios
                    .Select(ed => new
                    {
                        ed.Id,
                        ed.Nombre,
                        ed.Direccion,
                        ed.Localidad,
                        Telefono = ed.TelefonoContacto,
                        Lockers = ed.CantLockers
                    })
                    .ToList();

                if (dgvEdificios.Columns.Contains("Id"))
                    dgvEdificios.Columns["Id"].Visible = false;
            }
            catch (Exception ex)
            {
                Mostrar(ex.Message, true);
            }
        }

        private void dgvEdificios_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvEdificios.CurrentRow == null || dgvEdificios.CurrentRow.Index < 0)
                return;

            object valor = dgvEdificios.CurrentRow.Cells["Id"].Value;

            if (valor == null)
                return;

            _idSeleccionado = Convert.ToInt32(valor);
            Edificio edificio = _edificios.FirstOrDefault(ed => ed.Id == _idSeleccionado);

            if (edificio == null)
                return;

            txtNombre.Text = edificio.Nombre;
            txtDireccion.Text = edificio.Direccion;
            txtLocalidad.Text = edificio.Localidad;
            txtTelefono.Text = edificio.TelefonoContacto;
            nudCantLockers.Value = Math.Min(edificio.CantLockers, nudCantLockers.Maximum);
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
                if (_idSeleccionado == 0)
                {
                    _servicio.Crear(txtNombre.Text, txtDireccion.Text, txtLocalidad.Text,
                                    txtTelefono.Text, (int)nudCantLockers.Value);
                    Mostrar("Edificio creado correctamente.", false);
                }
                else
                {
                    Edificio edificio = _edificios.First(ed => ed.Id == _idSeleccionado);
                    edificio.Nombre = txtNombre.Text.Trim();
                    edificio.Direccion = txtDireccion.Text.Trim();
                    edificio.Localidad = txtLocalidad.Text.Trim();
                    edificio.TelefonoContacto = string.IsNullOrWhiteSpace(txtTelefono.Text)
                        ? null : txtTelefono.Text.Trim();
                    edificio.CantLockers = (int)nudCantLockers.Value;

                    _servicio.Modificar(edificio);
                    Mostrar("Edificio actualizado correctamente.", false);
                }

                CargarGrilla();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                Mostrar(ex.Message, true);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (_idSeleccionado == 0)
            {
                Mostrar("Seleccioná un edificio de la lista.", true);
                return;
            }

            if (MessageBox.Show("¿Eliminar el edificio seleccionado?", "Confirmar",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                _servicio.Eliminar(_idSeleccionado);
                Mostrar("Edificio eliminado.", false);
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
            txtDireccion.Clear();
            txtLocalidad.Clear();
            txtTelefono.Clear();
            nudCantLockers.Value = 0;
            dgvEdificios.ClearSelection();
        }

        private void Mostrar(string mensaje, bool esError)
        {
            lblMensaje.ForeColor = esError ? Color.Firebrick : Color.SeaGreen;
            lblMensaje.Text = mensaje;
        }
    }
}
