using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using LockersInteligentes.BLL;
using LockersInteligentes.Dominio.Entidades;
using LockersInteligentes.Dominio.Enums;

namespace LockersInteligentes.UI
{
    public partial class FrmEdificios : Form
    {
        private readonly EdificioService _servicio = new EdificioService();
        private readonly LockerService _lockerService = new LockerService();

        private IList<Edificio> _edificios = new List<Edificio>();
        private int _idSeleccionado;

        public FrmEdificios()
        {
            InitializeComponent();
        }

        private void FrmEdificios_Load(object sender, EventArgs e)
        {
            cboTamanioGenerar.DataSource = Enum.GetValues(typeof(TamanioPaquete));

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
            lblCantidadReal.Text = edificio.CantLockers.ToString();
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
                    _servicio.Crear(txtNombre.Text, txtDireccion.Text,
                                    txtLocalidad.Text, txtTelefono.Text);

                    Mostrar("Edificio creado. Seleccionalo en la lista y generá sus lockers.", false);
                }
                else
                {
                    Edificio edificio = _edificios.First(ed => ed.Id == _idSeleccionado);
                    edificio.Nombre = txtNombre.Text.Trim();
                    edificio.Direccion = txtDireccion.Text.Trim();
                    edificio.Localidad = txtLocalidad.Text.Trim();
                    edificio.TelefonoContacto = string.IsNullOrWhiteSpace(txtTelefono.Text)
                        ? null : txtTelefono.Text.Trim();

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

        /// <summary>
        /// Crea una tanda de lockers correlativos, todos del mismo tamaño y sector.
        /// Después se ajustan individualmente desde Gestión > Lockers.
        /// </summary>
        private void btnGenerar_Click(object sender, EventArgs e)
        {
            if (_idSeleccionado == 0)
            {
                Mostrar("Seleccioná primero un edificio de la lista.", true);
                return;
            }

            int cantidad = (int)nudCantidadGenerar.Value;
            Edificio edificio = _edificios.First(ed => ed.Id == _idSeleccionado);

            if (MessageBox.Show(
                    "¿Generar " + cantidad + " lockers en " + edificio.Nombre + "?",
                    "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                _lockerService.GenerarTanda(
                    _idSeleccionado,
                    cantidad,
                    (int)nudNumeroInicial.Value,
                    txtSectorGenerar.Text,
                    (TamanioPaquete)cboTamanioGenerar.SelectedItem);

                Mostrar("Se generaron " + cantidad + " lockers. Ajustá tamaños o sectores " +
                        "desde Gestión > Lockers si hace falta.", false);

                // Se recarga la grilla para que el contador refleje los nuevos, pero
                // se conserva la selección: es habitual generar dos tandas seguidas
                // de distinto tamaño en el mismo edificio.
                int seleccion = _idSeleccionado;
                CargarGrilla();
                _idSeleccionado = seleccion;
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
            lblCantidadReal.Text = "0";
            dgvEdificios.ClearSelection();
        }

        private void Mostrar(string mensaje, bool esError)
        {
            lblMensaje.ForeColor = esError ? Color.Firebrick : Color.SeaGreen;
            lblMensaje.Text = mensaje;
        }
    }
}