using System.Windows.Forms;

namespace LockersInteligentes.UI
{
    partial class FrmPrincipal
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            menuPrincipal = new MenuStrip();
            mnuArchivo = new ToolStripMenuItem();
            mnuCerrarSesion = new ToolStripMenuItem();
            mnuSalir = new ToolStripMenuItem();
            mnuSeguridad = new ToolStripMenuItem();
            mnuUsuarios = new ToolStripMenuItem();
            mnuGestion = new ToolStripMenuItem();
            mnuEdificios = new ToolStripMenuItem();
            mnuLockers = new ToolStripMenuItem();
            mnuResidentes = new ToolStripMenuItem();
            mnuRepartidores = new ToolStripMenuItem();
            mnuOperacion = new ToolStripMenuItem();
            mnuReservar = new ToolStripMenuItem();
            mnuEntrega = new ToolStripMenuItem();
            mnuRetiro = new ToolStripMenuItem();
            mnuInformacion = new ToolStripMenuItem();
            mnuReportes = new ToolStripMenuItem();
            mnuBitacora = new ToolStripMenuItem();
            barraEstado = new StatusStrip();
            lblEstadoUsuario = new ToolStripStatusLabel();
            mnuIdioma = new ToolStripMenuItem();
            mnuEspaniol = new ToolStripMenuItem();
            mnuIngles = new ToolStripMenuItem();
            menuPrincipal.SuspendLayout();
            barraEstado.SuspendLayout();
            SuspendLayout();
            // 
            // menuPrincipal
            // 
            menuPrincipal.ImageScalingSize = new System.Drawing.Size(20, 20);
            menuPrincipal.Items.AddRange(new ToolStripItem[] { mnuArchivo, mnuSeguridad, mnuGestion, mnuOperacion, mnuInformacion, mnuIdioma});
            menuPrincipal.Location = new System.Drawing.Point(0, 0);
            menuPrincipal.Name = "menuPrincipal";
            menuPrincipal.Size = new System.Drawing.Size(900, 28);
            menuPrincipal.TabIndex = 1;
            // 
            // mnuArchivo
            // 
            mnuArchivo.DropDownItems.AddRange(new ToolStripItem[] { mnuCerrarSesion, mnuSalir });
            mnuArchivo.Name = "mnuArchivo";
            mnuArchivo.Size = new System.Drawing.Size(73, 24);
            mnuArchivo.Text = "&Archivo";
            // 
            // mnuCerrarSesion
            // 
            mnuCerrarSesion.Name = "mnuCerrarSesion";
            mnuCerrarSesion.Size = new System.Drawing.Size(224, 26);
            mnuCerrarSesion.Text = "Cerrar sesión";
            mnuCerrarSesion.Click += mnuCerrarSesion_Click;
            // 
            // mnuSalir
            // 
            mnuSalir.Name = "mnuSalir";
            mnuSalir.Size = new System.Drawing.Size(224, 26);
            mnuSalir.Text = "Salir";
            mnuSalir.Click += mnuSalir_Click;
            // 
            // mnuSeguridad
            // 
            mnuSeguridad.DropDownItems.AddRange(new ToolStripItem[] { mnuUsuarios });
            mnuSeguridad.Name = "mnuSeguridad";
            mnuSeguridad.Size = new System.Drawing.Size(91, 24);
            mnuSeguridad.Text = "&Seguridad";
            // 
            // mnuUsuarios
            // 
            mnuUsuarios.Name = "mnuUsuarios";
            mnuUsuarios.Size = new System.Drawing.Size(224, 26);
            mnuUsuarios.Text = "Usuarios";
            mnuUsuarios.Click += mnuUsuarios_Click;
            // 
            // mnuGestion
            // 
            mnuGestion.DropDownItems.AddRange(new ToolStripItem[] { mnuEdificios, mnuLockers, mnuResidentes, mnuRepartidores });
            mnuGestion.Name = "mnuGestion";
            mnuGestion.Size = new System.Drawing.Size(73, 24);
            mnuGestion.Text = "&Gestión";
            // 
            // mnuEdificios
            // 
            mnuEdificios.Enabled = false;
            mnuEdificios.Name = "mnuEdificios";
            mnuEdificios.Size = new System.Drawing.Size(224, 26);
            mnuEdificios.Text = "Edificios";
            // 
            // mnuLockers
            // 
            mnuLockers.Enabled = false;
            mnuLockers.Name = "mnuLockers";
            mnuLockers.Size = new System.Drawing.Size(224, 26);
            mnuLockers.Text = "Lockers";
            // 
            // mnuResidentes
            // 
            mnuResidentes.Enabled = false;
            mnuResidentes.Name = "mnuResidentes";
            mnuResidentes.Size = new System.Drawing.Size(224, 26);
            mnuResidentes.Text = "Residentes";
            // 
            // mnuRepartidores
            // 
            mnuRepartidores.Enabled = false;
            mnuRepartidores.Name = "mnuRepartidores";
            mnuRepartidores.Size = new System.Drawing.Size(224, 26);
            mnuRepartidores.Text = "Repartidores";
            // 
            // mnuOperacion
            // 
            mnuOperacion.DropDownItems.AddRange(new ToolStripItem[] { mnuReservar, mnuEntrega, mnuRetiro });
            mnuOperacion.Name = "mnuOperacion";
            mnuOperacion.Size = new System.Drawing.Size(92, 24);
            mnuOperacion.Text = "&Operación";
            // 
            // mnuReservar
            // 
            mnuReservar.Name = "mnuReservar";
            mnuReservar.Size = new System.Drawing.Size(224, 26);
            mnuReservar.Text = "Reservar locker";
            mnuReservar.Click += mnuReservar_Click;
            // 
            // mnuEntrega
            // 
            mnuEntrega.Enabled = false;
            mnuEntrega.Name = "mnuEntrega";
            mnuEntrega.Size = new System.Drawing.Size(224, 26);
            mnuEntrega.Text = "Registrar entrega";
            // 
            // mnuRetiro
            // 
            mnuRetiro.Enabled = false;
            mnuRetiro.Name = "mnuRetiro";
            mnuRetiro.Size = new System.Drawing.Size(224, 26);
            mnuRetiro.Text = "Registrar retiro";
            // 
            // mnuInformacion
            // 
            mnuInformacion.DropDownItems.AddRange(new ToolStripItem[] { mnuReportes, mnuBitacora });
            mnuInformacion.Name = "mnuInformacion";
            mnuInformacion.Size = new System.Drawing.Size(103, 24);
            mnuInformacion.Text = "&Información";
            // 
            // mnuReportes
            // 
            mnuReportes.Enabled = false;
            mnuReportes.Name = "mnuReportes";
            mnuReportes.Size = new System.Drawing.Size(224, 26);
            mnuReportes.Text = "Reportes";
            // 
            // mnuBitacora
            // 
            mnuBitacora.Enabled = false;
            mnuBitacora.Name = "mnuBitacora";
            mnuBitacora.Size = new System.Drawing.Size(224, 26);
            mnuBitacora.Text = "Bitácora";
            // 
            // barraEstado
            // 
            barraEstado.ImageScalingSize = new System.Drawing.Size(20, 20);
            barraEstado.Items.AddRange(new ToolStripItem[] { lblEstadoUsuario });
            barraEstado.Location = new System.Drawing.Point(0, 528);
            barraEstado.Name = "barraEstado";
            barraEstado.Size = new System.Drawing.Size(900, 22);
            barraEstado.TabIndex = 0;
            // 
            // lblEstadoUsuario
            // 
            lblEstadoUsuario.Name = "lblEstadoUsuario";
            lblEstadoUsuario.Size = new System.Drawing.Size(0, 16);
            // 
            // mnuIdioma
            // 
            mnuIdioma.DropDownItems.AddRange(new ToolStripItem[] { mnuEspaniol, mnuIngles });
            mnuIdioma.Name = "mnuIdioma";
            mnuIdioma.Size = new System.Drawing.Size(32, 19);
            mnuIdioma.Text = "&Idioma";
            // 
            // mnuEspaniol
            // 
            mnuEspaniol.Name = "mnuEspaniol";
            mnuEspaniol.Size = new System.Drawing.Size(144, 26);
            mnuEspaniol.Text = "Español";
            // 
            // mnuIngles
            // 
            mnuIngles.Name = "mnuIngles";
            mnuIngles.Size = new System.Drawing.Size(144, 26);
            mnuIngles.Text = "English";
            // 
            // FrmPrincipal
            // 
            ClientSize = new System.Drawing.Size(900, 550);
            Controls.Add(barraEstado);
            Controls.Add(menuPrincipal);
            Font = new System.Drawing.Font("Segoe UI", 9F);
            IsMdiContainer = true;
            MainMenuStrip = menuPrincipal;
            Name = "FrmPrincipal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Lockers Inteligentes";
            Load += FrmPrincipal_Load;
            menuPrincipal.ResumeLayout(false);
            menuPrincipal.PerformLayout();
            barraEstado.ResumeLayout(false);
            barraEstado.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.MenuStrip menuPrincipal;
        private System.Windows.Forms.ToolStripMenuItem mnuArchivo;
        private System.Windows.Forms.ToolStripMenuItem mnuCerrarSesion;
        private System.Windows.Forms.ToolStripMenuItem mnuSalir;
        private System.Windows.Forms.ToolStripMenuItem mnuSeguridad;
        private System.Windows.Forms.ToolStripMenuItem mnuUsuarios;
        private System.Windows.Forms.ToolStripMenuItem mnuGestion;
        private System.Windows.Forms.ToolStripMenuItem mnuEdificios;
        private System.Windows.Forms.ToolStripMenuItem mnuLockers;
        private System.Windows.Forms.ToolStripMenuItem mnuResidentes;
        private System.Windows.Forms.ToolStripMenuItem mnuRepartidores;
        private System.Windows.Forms.ToolStripMenuItem mnuOperacion;
        private System.Windows.Forms.ToolStripMenuItem mnuReservar;
        private System.Windows.Forms.ToolStripMenuItem mnuEntrega;
        private System.Windows.Forms.ToolStripMenuItem mnuRetiro;
        private System.Windows.Forms.ToolStripMenuItem mnuInformacion;
        private System.Windows.Forms.ToolStripMenuItem mnuReportes;
        private System.Windows.Forms.ToolStripMenuItem mnuBitacora;
        private System.Windows.Forms.StatusStrip barraEstado;
        private System.Windows.Forms.ToolStripStatusLabel lblEstadoUsuario;
        private System.Windows.Forms.ToolStripMenuItem mnuEspaniol;
        private System.Windows.Forms.ToolStripMenuItem mnuIngles;
        private System.Windows.Forms.ToolStripMenuItem mnuIdioma;
    }
}