using LockersInteligentes.ArqBase.ArqBase;
using LockersInteligentes.ArqBase.DataAccess;
using LockersInteligentes.ArqBase.Entidades;
using LockersInteligentes.ArqBase.Logica;
using LockersInteligentes.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace LockersInteligentes.UI.Informacion
{
    public partial class FrmErrores : Form, IObservadorIdioma
    {
        private IList<RegistroError> _errores = new List<RegistroError>();
        public FrmErrores()
        {
            InitializeComponent();
            GestorIdioma.Instancia.Suscribir(this);
        }
        private void FrmErrores_Load(object sender, EventArgs e)
        {
            Cargar();
            ActualizarIdioma();
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

                TraducirColumna("Fecha", "FrmErrores.colFecha");
                TraducirColumna("Usuario", "Comun.Usuario");
                TraducirColumna("Tipo", "Comun.Tipo");
                TraducirColumna("Origen", "FrmErrores.colOrigen");
                TraducirColumna("Mensaje", "FrmErrores.colMensaje");

                txtDetalle.Clear();

                GestorIdioma idioma = GestorIdioma.Instancia;
                lblMensaje.ForeColor = Color.DimGray;
                lblMensaje.Text = _errores.Count == 0
                    ? idioma.Traducir("FrmErrores.SinErrores")
                    : string.Format(idioma.Traducir("FrmErrores.Cantidad"), _errores.Count);
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
            GestorSesion.Instancia.ValidarPermiso(PermisoService.Patentes.EjecutarBackup);
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

        public void ActualizarIdioma()
        {
            GestorIdioma idioma = GestorIdioma.Instancia;

            Text = idioma.Traducir("FrmErrores.Caption");
            lblTitulo.Text = idioma.Traducir("FrmErrores.Caption");
            lblRuta.Text = string.Format(
                idioma.Traducir("FrmErrores.lblRuta"), ErrorLogService.RutaArchivo);
            lblDetalleTitulo.Text = idioma.Traducir("FrmErrores.lblDetalle");
            btnActualizar.Text = idioma.Traducir("Comun.Actualizar");
            btnAbrirCarpeta.Text = idioma.Traducir("FrmErrores.btnCarpeta");
            btnLimpiar.Text = idioma.Traducir("FrmErrores.btnVaciar");
            btnBackup.Text = idioma.Traducir("FrmErrores.btnBackup");

            Cargar();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            GestorIdioma.Instancia.Desuscribir(this);
            base.OnFormClosed(e);
        }

        private void TraducirColumna(string nombreColumna, string clave)
        {
            if (dgvErrores.Columns.Contains(nombreColumna))
                dgvErrores.Columns[nombreColumna].HeaderText =
                    GestorIdioma.Instancia.Traducir(clave);
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            GestorSesion.Instancia.ValidarPermiso(PermisoService.Patentes.EjecutarBackup);

            using (OpenFileDialog dialogo = new OpenFileDialog())
            {
                dialogo.Filter = "Script SQL (*.sql)|*.sql";
                dialogo.Title = "Seleccionar backup a restaurar";

                if (dialogo.ShowDialog() != DialogResult.OK)
                    return;

                string aviso =
                    "ATENCIÓN: esta operación BORRA la base actual y la reemplaza " +
                    "por el contenido del backup." + Environment.NewLine + Environment.NewLine +
                    "Archivo: " + dialogo.FileName + Environment.NewLine + Environment.NewLine +
                    "Se va a generar una copia de seguridad del estado actual antes " +
                    "de continuar." + Environment.NewLine + Environment.NewLine +
                    "Al terminar vas a tener que volver a iniciar sesión." +
                    Environment.NewLine + Environment.NewLine +
                    "¿Continuar?";

                if (MessageBox.Show(aviso, "Confirmar restauración",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    return;

                btnRestore.Enabled = false;
                Cursor = Cursors.WaitCursor;

                try
                {
                    string copia = Path.Combine(
                        Path.GetDirectoryName(ErrorLogService.RutaArchivo),
                        "antes_de_restaurar_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".sql");

                    BackupService.Generar(Conexion.Cadena, UbicarEsquema(), copia);

                    int lotes = BackupService.Restaurar(Conexion.Cadena, dialogo.FileName);

                    MessageBox.Show(
                        "Base restaurada correctamente." + Environment.NewLine + Environment.NewLine +
                        "Lotes ejecutados: " + lotes + Environment.NewLine +
                        "Copia previa guardada en: " + copia + Environment.NewLine + Environment.NewLine +
                        "La aplicación se va a cerrar. Volvé a abrirla para usar la base restaurada.",
                        "Restauración", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Se cierra porque la sesión y los permisos en memoria
                    // corresponden a la base anterior, que ya no existe.
                    Application.Exit();
                }
                catch (Exception ex)
                {
                    ErrorLogService.Registrar(ex, "FrmErrores.btnRestore_Click",
                        GestorSesion.Instancia.ActorParaBitacora());

                    lblMensaje.ForeColor = Color.Firebrick;
                    lblMensaje.Text = "No se pudo restaurar: " + ex.Message;
                }
                finally
                {
                    btnRestore.Enabled = true;
                    Cursor = Cursors.Default;
                }
            }
        }

        private void btnVerificarIntegridad_Click(object sender, EventArgs e)
        {
            try
            {
                IList<Usuario> alterados = SistemaLockersFacade.Instancia.VerificarIntegridad();

                if (alterados.Count == 0)
                {
                    lblMensaje.ForeColor = Color.SeaGreen;
                    lblMensaje.Text = "Integridad correcta.";

                    MessageBox.Show(
                        "Todos los usuarios tienen su dígito verificador correcto." +
                        Environment.NewLine + Environment.NewLine +
                        "Ningún registro fue modificado fuera del sistema.",
                        "Verificación de integridad",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string detalle = string.Empty;

                foreach (Usuario usuario in alterados)
                    detalle += "  - " + usuario.NombreUsuario +
                               " (" + usuario.NombreCompleto() + ")" + Environment.NewLine;

                lblMensaje.ForeColor = Color.Firebrick;
                lblMensaje.Text = alterados.Count + " registro(s) con integridad comprometida.";

                MessageBox.Show(
                    "Se detectaron " + alterados.Count + " usuario(s) cuyo dígito " +
                    "verificador no coincide:" + Environment.NewLine + Environment.NewLine +
                    detalle + Environment.NewLine +
                    "Esto significa que esos registros fueron modificados " +
                    "directamente en la base de datos, sin pasar por el sistema." +
                    Environment.NewLine + Environment.NewLine +
                    "Si el cambio fue intencional, usá 'Recalcular dígitos'.",
                    "Integridad comprometida",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                ErrorLogService.Registrar(ex, "FrmErrores.btnVerificarIntegridad_Click",
                    GestorSesion.Instancia.ActorParaBitacora());

                lblMensaje.ForeColor = Color.Firebrick;
                lblMensaje.Text = ex.Message;
            }
        }

        private void btnRecalcularDvh_Click(object sender, EventArgs e)
        {
            string aviso =
                "Esta operación recalcula el dígito verificador de todos los usuarios, " +
                "tomando como válido el contenido actual de la base." +
                Environment.NewLine + Environment.NewLine +
                "Si había registros manipulados, pasan a considerarse correctos y " +
                "se pierde esa evidencia." + Environment.NewLine + Environment.NewLine +
                "¿Continuar?";

            if (MessageBox.Show(aviso, "Recalcular dígitos",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            try
            {
                int actualizados = SistemaLockersFacade.Instancia.RecalcularDigitos();

                lblMensaje.ForeColor = Color.SeaGreen;
                lblMensaje.Text = "Dígitos recalculados: " + actualizados + " usuario(s).";
            }
            catch (Exception ex)
            {
                ErrorLogService.Registrar(ex, "FrmErrores.btnRecalcularDvh_Click",
                    GestorSesion.Instancia.ActorParaBitacora());

                lblMensaje.ForeColor = Color.Firebrick;
                lblMensaje.Text = ex.Message;
            }
        }


    }
}
