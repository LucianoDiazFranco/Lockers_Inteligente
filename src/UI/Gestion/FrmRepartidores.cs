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
    public partial class FrmRepartidores : Form
    {
        private readonly RepartidorService _servicio = new RepartidorService();
        private IList<Repartidor> _repartidores = new List<Repartidor>();
        private int _idSeleccionado;
        public FrmRepartidores()
        {
            InitializeComponent();
        }
        private void FrmRepartidores_Load(object sender, EventArgs e)
        {
            CargarGrilla();
            LimpiarCampos();
        }

        private void CargarGrilla()
        {
            try
            {
                _repartidores = _servicio.Listar();

                dgvRepartidores.DataSource = _repartidores
                    .Select(r => new
                    {
                        r.Id,
                        Apellido = r.Apellido,
                        Nombre = r.Nombre,
                        r.Dni,
                        r.Empresa,
                        r.Telefono,
                        Estado = r.Activo ? "Activo" : "Baja"
                    })
                    .ToList();

                if (dgvRepartidores.Columns.Contains("Id"))
                    dgvRepartidores.Columns["Id"].Visible = false;

                PintarInactivos();
            }
            catch (Exception ex)
            {
                Mostrar(ex.Message, true);
            }
        }

        /// <summary>Los dados de baja se muestran en gris para distinguirlos de un vistazo.</summary>
        private void PintarInactivos()
        {
            foreach (DataGridViewRow fila in dgvRepartidores.Rows)
            {
                if (fila.Cells["Estado"].Value != null &&
                    fila.Cells["Estado"].Value.ToString() == "Baja")
                {
                    fila.DefaultCellStyle.ForeColor = Color.Gray;
                    fila.DefaultCellStyle.BackColor = Color.WhiteSmoke;
                }
            }
        }

        private void dgvRepartidores_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvRepartidores.CurrentRow == null || dgvRepartidores.CurrentRow.Index < 0)
                return;

            object valor = dgvRepartidores.CurrentRow.Cells["Id"].Value;

            if (valor == null)
                return;

            _idSeleccionado = Convert.ToInt32(valor);
            Repartidor repartidor = _repartidores.FirstOrDefault(r => r.Id == _idSeleccionado);

            if (repartidor == null)
                return;

            txtNombre.Text = repartidor.Nombre;
            txtApellido.Text = repartidor.Apellido;
            txtDni.Text = repartidor.Dni;
            txtEmpresa.Text = repartidor.Empresa;
            txtTelefono.Text = repartidor.Telefono;

            // Los botones se habilitan según el estado: no tiene sentido ofrecer
            // "Dar de baja" sobre alguien que ya está de baja.
            btnDesactivar.Enabled = repartidor.Activo;
            btnReactivar.Enabled = !repartidor.Activo;
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
                    _servicio.Crear(txtNombre.Text, txtApellido.Text, txtDni.Text,
                                    txtEmpresa.Text, txtTelefono.Text);
                    Mostrar("Repartidor creado correctamente.", false);
                }
                else
                {
                    Repartidor repartidor = _repartidores.First(r => r.Id == _idSeleccionado);
                    repartidor.Nombre = txtNombre.Text.Trim();
                    repartidor.Apellido = txtApellido.Text.Trim();
                    repartidor.Dni = txtDni.Text.Trim();
                    repartidor.Empresa = string.IsNullOrWhiteSpace(txtEmpresa.Text)
                        ? null : txtEmpresa.Text.Trim();
                    repartidor.Telefono = string.IsNullOrWhiteSpace(txtTelefono.Text)
                        ? null : txtTelefono.Text.Trim();

                    _servicio.Modificar(repartidor);
                    Mostrar("Repartidor actualizado correctamente.", false);
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
                Mostrar("Seleccioná un repartidor de la lista.", true);
                return;
            }

            if (MessageBox.Show("¿Dar de baja al repartidor seleccionado?", "Confirmar",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                _servicio.Desactivar(_idSeleccionado);
                Mostrar("Repartidor dado de baja. Sus entregas siguen en el historial.", false);
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
                Mostrar("Seleccioná un repartidor de la lista.", true);
                return;
            }

            try
            {
                _servicio.Reactivar(_idSeleccionado);
                Mostrar("Repartidor reactivado.", false);
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
            txtEmpresa.Clear();
            txtTelefono.Clear();
            btnDesactivar.Enabled = false;
            btnReactivar.Enabled = false;
            dgvRepartidores.ClearSelection();
        }

        private void Mostrar(string mensaje, bool esError)
        {
            lblMensaje.ForeColor = esError ? Color.Firebrick : Color.SeaGreen;
            lblMensaje.Text = mensaje;
        }
    }
}
