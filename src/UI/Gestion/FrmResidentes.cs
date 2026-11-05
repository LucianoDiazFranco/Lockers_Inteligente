using LockersInteligentes.ArqBase.Logica;
using LockersInteligentes.BLL;
using LockersInteligentes.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace LockersInteligentes.UI.Gestion
{
    public partial class FrmResidentes : Form, IObservadorIdioma
    {
        private readonly SistemaLockersFacade _sistema = SistemaLockersFacade.Instancia;

        private IList<Residente> _residentes = new List<Residente>();
        private int _idSeleccionado;
        public FrmResidentes()
        {
            InitializeComponent();
            GestorIdioma.Instancia.Suscribir(this);
        }
        private void FrmResidentes_Load(object sender, EventArgs e)
        {
            try
            {
                cboFiltroEdificio.DisplayMember = "Nombre";
                cboFiltroEdificio.ValueMember = "Id";
                cboFiltroEdificio.DataSource = _sistema.EdificiosParaPanel();

                CargarGrilla();
                LimpiarCampos();
                ActualizarIdioma();
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
                _residentes = _sistema.ListarResidentes(
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

                TraducirColumna("Apellido", "Comun.Apellido");
                TraducirColumna("Nombre", "Comun.Nombre");
                TraducirColumna("Dni", "Comun.Dni");
                TraducirColumna("Correo", "Comun.Correo");
                TraducirColumna("Piso", "Comun.Piso");
                TraducirColumna("Telefono", "Comun.Telefono");
                TraducirColumna("Estado", "Comun.Estado");

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
                    _sistema.CrearResidente(idEdificio, txtNombre.Text, txtApellido.Text, txtDni.Text,
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

                    _sistema.ModificarResidente(residente);
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
                _sistema.DesactivarResidente(_idSeleccionado);
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
                _sistema.ReactivarResidente(_idSeleccionado);
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

        public void ActualizarIdioma()
        {
            GestorIdioma idioma = GestorIdioma.Instancia;

            Text = idioma.Traducir("FrmResidentes.Caption");
            lblFiltroEdificio.Text = idioma.Traducir("Comun.Edificio");
            lblNombre.Text = idioma.Traducir("Comun.Nombre");
            lblApellido.Text = idioma.Traducir("Comun.Apellido");
            lblDni.Text = idioma.Traducir("Comun.Dni");
            lblCorreo.Text = idioma.Traducir("Comun.Correo");
            lblPiso.Text = idioma.Traducir("Comun.Piso");
            lblTelefono.Text = idioma.Traducir("Comun.Telefono");
            btnNuevo.Text = idioma.Traducir("Comun.Nuevo");
            btnGuardar.Text = idioma.Traducir("Comun.Guardar");
            btnDesactivar.Text = idioma.Traducir("Comun.DarDeBaja");
            btnReactivar.Text = idioma.Traducir("Comun.Reactivar");

            CargarGrilla();
        }
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            GestorIdioma.Instancia.Desuscribir(this);
            base.OnFormClosed(e);
        }

        private void TraducirColumna(string nombreColumna, string clave)
        {
            if (dgvResidentes.Columns.Contains(nombreColumna))
                dgvResidentes.Columns[nombreColumna].HeaderText =
                    GestorIdioma.Instancia.Traducir(clave);
        }
    }
}
