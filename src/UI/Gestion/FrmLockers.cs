using LockersInteligentes.ArqBase.Logica;
using LockersInteligentes.BLL;
using LockersInteligentes.Dominio.Entidades;
using LockersInteligentes.Dominio.Enums;
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
    public partial class FrmLockers : Form, IObservadorIdioma
    {
        private readonly SistemaLockersFacade _sistema = SistemaLockersFacade.Instancia;
        private IList<Locker> _lockers = new List<Locker>();
        private int _idSeleccionado;
        public FrmLockers()
        {
            InitializeComponent();
            GestorIdioma.Instancia.Suscribir(this);
        }
        private void FrmLockers_Load(object sender, EventArgs e)
        {
            try
            {
                cboFiltroEdificio.DisplayMember = "Nombre";
                cboFiltroEdificio.ValueMember = "Id";
                cboFiltroEdificio.DataSource = _sistema.EdificiosParaPanel();

                cboTamanio.DataSource = Enum.GetValues(typeof(TamanioPaquete));

                // Solo los dos estados que el administrador puede fijar a mano.
                // Reservado y Ocupado los gobierna el ciclo del paquete.
                cboEstado.DataSource = new List<EstadoLocker>
                {
                    EstadoLocker.Libre,
                    EstadoLocker.FueraDeServicio
                };

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
                _lockers = _sistema.ListarLockers(Convert.ToInt32(cboFiltroEdificio.SelectedValue));

                dgvLockers.DataSource = _lockers
                    .Select(l => new
                    {
                        l.Id,
                        Numero = l.Numero,
                        Sector = l.GrupoLocker,
                        Tamanio = l.Tamanio.ToString(),
                        Estado = l.Estado.ToString(),
                        l.Descripcion
                    })
                    .ToList();

                if (dgvLockers.Columns.Contains("Id"))
                    dgvLockers.Columns["Id"].Visible = false;

                TraducirColumna("Numero", "Comun.Numero");
                TraducirColumna("Sector", "Comun.Sector");
                TraducirColumna("Tamanio", "Comun.Tamanio");
                TraducirColumna("Estado", "Comun.Estado");
                TraducirColumna("Descripcion", "Comun.Descripcion");

            }
            catch (Exception ex)
            {
                Mostrar(ex.Message, true);
            }
        }

        private void dgvLockers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvLockers.CurrentRow == null || dgvLockers.CurrentRow.Index < 0)
                return;

            object valor = dgvLockers.CurrentRow.Cells["Id"].Value;

            if (valor == null)
                return;

            _idSeleccionado = Convert.ToInt32(valor);
            Locker locker = _lockers.FirstOrDefault(l => l.Id == _idSeleccionado);

            if (locker == null)
                return;

            nudNumero.Value = Math.Min(locker.Numero, nudNumero.Maximum);
            txtSector.Text = locker.GrupoLocker;
            txtDescripcion.Text = locker.Descripcion;
            cboTamanio.SelectedItem = locker.Tamanio;

            // Si está Reservado u Ocupado, el combo no puede mostrar ese estado
            // porque solo tiene los dos manuales. Se deshabilita y se avisa.
            bool estadoManual = locker.Estado == EstadoLocker.Libre ||
                                locker.Estado == EstadoLocker.FueraDeServicio;

            cboEstado.Enabled = estadoManual;

            if (estadoManual)
            {
                cboEstado.SelectedItem = locker.Estado;
                Mostrar(string.Empty, false);
            }
            else
            {
                Mostrar("El locker está " + locker.Estado +
                        ": su estado lo controla el ciclo del paquete y no se edita acá.", false);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            nudNumero.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                int idEdificio = Convert.ToInt32(cboFiltroEdificio.SelectedValue);
                TamanioPaquete tamanio = (TamanioPaquete)cboTamanio.SelectedItem;

                if (_idSeleccionado == 0)
                {
                    _sistema.CrearLocker(idEdificio, (int)nudNumero.Value, txtSector.Text,
                                    txtDescripcion.Text, tamanio);
                    Mostrar("Locker creado correctamente.", false);
                }
                else
                {
                    Locker locker = _lockers.First(l => l.Id == _idSeleccionado);
                    locker.Numero = (int)nudNumero.Value;
                    locker.GrupoLocker = string.IsNullOrWhiteSpace(txtSector.Text)
                        ? null : txtSector.Text.Trim();
                    locker.Descripcion = string.IsNullOrWhiteSpace(txtDescripcion.Text)
                        ? null : txtDescripcion.Text.Trim();
                    locker.Tamanio = tamanio;

                    _sistema.ModificarLocker(locker);

                    // El estado va por su propio método, que valida las transiciones.
                    if (cboEstado.Enabled && cboEstado.SelectedItem != null)
                    {
                        EstadoLocker nuevo = (EstadoLocker)cboEstado.SelectedItem;

                        if (nuevo != locker.Estado)
                            _sistema.CambiarEstadoLocker(locker.Id, nuevo);
                    }

                    Mostrar("Locker actualizado correctamente.", false);
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
                Mostrar("Seleccioná un locker de la lista.", true);
                return;
            }

            if (MessageBox.Show("¿Eliminar el locker seleccionado?", "Confirmar",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                _sistema.EliminarLocker(_idSeleccionado);
                Mostrar("Locker eliminado.", false);
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
            nudNumero.Value = 0;
            txtSector.Clear();
            txtDescripcion.Clear();
            cboEstado.Enabled = true;

            if (cboTamanio.Items.Count > 0)
                cboTamanio.SelectedIndex = 0;

            if (cboEstado.Items.Count > 0)
                cboEstado.SelectedIndex = 0;

            dgvLockers.ClearSelection();
        }

        private void Mostrar(string mensaje, bool esError)
        {
            lblMensaje.ForeColor = esError ? Color.Firebrick : Color.SeaGreen;
            lblMensaje.Text = mensaje;
        }

        public void ActualizarIdioma()
        {
            GestorIdioma idioma = GestorIdioma.Instancia;

            Text = idioma.Traducir("FrmLockers.Caption");
            lblFiltroEdificio.Text = idioma.Traducir("Comun.Edificio");
            lblNumero.Text = idioma.Traducir("Comun.Numero");
            lblSector.Text = idioma.Traducir("Comun.Sector");
            lblTamanio.Text = idioma.Traducir("Comun.Tamanio");
            lblEstado.Text = idioma.Traducir("Comun.Estado");
            lblDescripcion.Text = idioma.Traducir("Comun.Descripcion");
            btnNuevo.Text = idioma.Traducir("Comun.Nuevo");
            btnGuardar.Text = idioma.Traducir("Comun.Guardar");
            btnEliminar.Text = idioma.Traducir("Comun.Eliminar");

            CargarGrilla();
        }
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            GestorIdioma.Instancia.Desuscribir(this);
            base.OnFormClosed(e);
        }

        private void TraducirColumna(string nombreColumna, string clave)
        {
            if (dgvLockers.Columns.Contains(nombreColumna))
                dgvLockers.Columns[nombreColumna].HeaderText =
                    GestorIdioma.Instancia.Traducir(clave);
        }
    }
}
