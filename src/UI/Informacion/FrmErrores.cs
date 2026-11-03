using LockersInteligentes.ArqBase.ArqBase;
using LockersInteligentes.ArqBase.DataAccess;
using LockersInteligentes.ArqBase.Entidades;
using LockersInteligentes.ArqBase.Logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LockersInteligentes.UI.Informacion
{
    public partial class FrmErrores : Form
    {
        private IList<RegistroError> _errores = new List<RegistroError>();
        public FrmErrores()
        {
            InitializeComponent();
        }
        private void FrmErrores_Load(object sender, EventArgs e)
        {
            lblRuta.Text = "Archivo: " + ErrorLogService.RutaArchivo;
            Cargar();
        }

        private void Cargar()
        {
            try
            {
                _errores = ErrorLogService.Listar();

                dgvErrores.DataSource = _errores
                    .Select((r, i) => new
                    {
                        Indice = i,
                        Fecha = r.FechaHora.ToString("dd/MM/yyyy HH:mm:ss"),
                        Usuario = r.Usuario,
                        Tipo = r.TipoExcepcion,
                        Origen = r.Origen,
                        Mensaje = r.Mensaje
                    })
                    .ToList();

                if (dgvErrores.Columns.Contains("Indice"))
                    dgvErrores.Columns["Indice"].Visible = false;

                if (dgvErrores.Columns.Contains("Mensaje"))
                    dgvErrores.Columns["Mensaje"].AutoSizeMode =
                        DataGridViewAutoSizeColumnMode.Fill;

                txtDetalle.Clear();

                lblMensaje.ForeColor = Color.DimGray;
                lblMensaje.Text = _errores.Count == 0
                    ? "No hay errores registrados."
                    : _errores.Count + " error(es) registrado(s).";
            }
            catch (Exception ex)
            {
                lblMensaje.ForeColor = Color.Firebrick;
                lblMensaje.Text = "No se pudo leer el log: " + ex.Message;
            }
        }

        /// <summary>
        /// Muestra el detalle completo del error seleccionado, incluido el stack
        /// trace. Esta información es la que NO se le muestra al usuario común
        /// cuando ocurre el error: expone la estructura interna del sistema.
        /// </summary>
        private void dgvErrores_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvErrores.CurrentRow == null || dgvErrores.CurrentRow.Index < 0)
                return;

            object valor = dgvErrores.CurrentRow.Cells["Indice"].Value;

            if (valor == null)
                return;

            int indice = Convert.ToInt32(valor);

            if (indice < 0 || indice >= _errores.Count)
                return;

            RegistroError error = _errores[indice];

            txtDetalle.Text =
                "Fecha:     " + error.FechaHora.ToString("dd/MM/yyyy HH:mm:ss") + Environment.NewLine +
                "Usuario:   " + error.Usuario + Environment.NewLine +
                "Origen:    " + error.Origen + Environment.NewLine +
                "Tipo:      " + error.TipoExcepcion + Environment.NewLine +
                Environment.NewLine +
                "Mensaje:" + Environment.NewLine + error.Mensaje + Environment.NewLine +
                Environment.NewLine +
                (string.IsNullOrEmpty(error.DetalleInterno)
                    ? ""
                    : "Causa interna:" + Environment.NewLine + error.DetalleInterno +
                      Environment.NewLine + Environment.NewLine) +
                "Stack trace:" + Environment.NewLine + error.StackTrace;
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            Cargar();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Vaciar el log de errores? Esta acción no se puede deshacer.",
                    "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            try
            {
                ErrorLogService.Limpiar();
                Cargar();
                lblMensaje.ForeColor = Color.SeaGreen;
                lblMensaje.Text = "Log vaciado.";
            }
            catch (Exception ex)
            {
                lblMensaje.ForeColor = Color.Firebrick;
                lblMensaje.Text = ex.Message;
            }
        }

        /// <summary>Abre la carpeta en el Explorador, para poder adjuntar el XML.</summary>
        private void btnAbrirCarpeta_Click(object sender, EventArgs e)
        {
            try
            {
                string carpeta = Path.GetDirectoryName(ErrorLogService.RutaArchivo);

                if (!Directory.Exists(carpeta))
                {
                    lblMensaje.ForeColor = Color.Firebrick;
                    lblMensaje.Text = "La carpeta todavía no existe: no hubo errores aún.";
                    return;
                }

                System.Diagnostics.Process.Start("explorer.exe", carpeta);
            }
            catch (Exception ex)
            {
                lblMensaje.ForeColor = Color.Firebrick;
                lblMensaje.Text = ex.Message;
            }
        }
        /// <summary>
        /// Backup. Genera un script autosuficiente con esquema y datos.
        /// El esquema se toma del 01_Esquema.sql del proyecto, así el backup y la
        /// estructura versionada nunca se contradicen.
        /// </summary>
        private void btnBackup_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dialogo = new SaveFileDialog())
            {
                dialogo.Filter = "Script SQL (*.sql)|*.sql";
                dialogo.FileName = "backup_lockers_" +
                                   DateTime.Now.ToString("yyyyMMdd_HHmm") + ".sql";
                dialogo.Title = "Guardar backup";

                if (dialogo.ShowDialog() != DialogResult.OK)
                    return;

                btnBackup.Enabled = false;
                Cursor = Cursors.WaitCursor;

                try
                {
                    string rutaEsquema = UbicarEsquema();

                    int filas = BackupService.Generar(
                        Conexion.Cadena, rutaEsquema, dialogo.FileName);

                    lblMensaje.ForeColor = Color.SeaGreen;
                    lblMensaje.Text = "Backup generado: " + filas + " fila(s).";

                    MessageBox.Show(
                        "Backup generado correctamente." + Environment.NewLine + Environment.NewLine +
                        "Filas exportadas: " + filas + Environment.NewLine +
                        "Archivo: " + dialogo.FileName + Environment.NewLine + Environment.NewLine +
                        "Para restaurarlo en otra máquina, abrilo en SSMS y ejecutalo con F5." +
                        Environment.NewLine + Environment.NewLine +
                        "Contiene las credenciales hasheadas de los usuarios: guardalo en un " +
                        "lugar seguro y no lo subas a repositorios públicos.",
                        "Backup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    ErrorLogService.Registrar(ex, "FrmErrores.btnBackup_Click",
                        GestorSesion.Instancia.ActorParaBitacora());

                    lblMensaje.ForeColor = Color.Firebrick;
                    lblMensaje.Text = "No se pudo generar: " + ex.Message;
                }
                finally
                {
                    btnBackup.Enabled = true;
                    Cursor = Cursors.Default;
                }
            }
        }

        /// <summary>
        /// Busca el 01_Esquema.sql subiendo desde el ejecutable hasta la raíz del proyecto
        /// </summary>
        private string UbicarEsquema()
        {
            string actual = AppDomain.CurrentDomain.BaseDirectory;

            for (int i = 0; i < 8 && actual != null; i++)
            {
                string candidato = Path.Combine(actual, "DAL", "db", "01_Esquema.sql");

                if (File.Exists(candidato))
                    return candidato;

                DirectoryInfo padre = Directory.GetParent(actual.TrimEnd(Path.DirectorySeparatorChar));

                if (padre == null)
                    break;

                actual = padre.FullName;
            }

            throw new FileNotFoundException(
                "No se encontró 01_Esquema.sql. Debería estar en src\\DAL\\db.");
        }


    }
}
