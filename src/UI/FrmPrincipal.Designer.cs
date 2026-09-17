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
            this.menuPrincipal = new System.Windows.Forms.MenuStrip();
            this.mnuArchivo = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCerrarSesion = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSalir = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSeguridad = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuUsuarios = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGestion = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdificios = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLockers = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuResidentes = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRepartidores = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOperacion = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuReservar = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEntrega = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRetiro = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuInformacion = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuReportes = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBitacora = new System.Windows.Forms.ToolStripMenuItem();
            this.barraEstado = new System.Windows.Forms.StatusStrip();
            this.lblEstadoUsuario = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuPrincipal.SuspendLayout();
            this.barraEstado.SuspendLayout();
            this.SuspendLayout();

            this.menuPrincipal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.mnuArchivo, this.mnuSeguridad, this.mnuGestion,
                this.mnuOperacion, this.mnuInformacion});
            this.menuPrincipal.Name = "menuPrincipal";
            this.menuPrincipal.Size = new System.Drawing.Size(900, 24);

            this.mnuArchivo.Name = "mnuArchivo";
            this.mnuArchivo.Text = "&Archivo";
            this.mnuArchivo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.mnuCerrarSesion, this.mnuSalir});

            this.mnuCerrarSesion.Name = "mnuCerrarSesion";
            this.mnuCerrarSesion.Text = "Cerrar sesión";
            this.mnuCerrarSesion.Click += new System.EventHandler(this.mnuCerrarSesion_Click);

            this.mnuSalir.Name = "mnuSalir";
            this.mnuSalir.Text = "Salir";
            this.mnuSalir.Click += new System.EventHandler(this.mnuSalir_Click);

            this.mnuSeguridad.Name = "mnuSeguridad";
            this.mnuSeguridad.Text = "&Seguridad";
            this.mnuSeguridad.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.mnuUsuarios});

            this.mnuUsuarios.Name = "mnuUsuarios";
            this.mnuUsuarios.Text = "Usuarios";
            this.mnuUsuarios.Click += new System.EventHandler(this.mnuUsuarios_Click);

            this.mnuGestion.Name = "mnuGestion";
            this.mnuGestion.Text = "&Gestión";
            this.mnuGestion.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.mnuEdificios, this.mnuLockers, this.mnuResidentes, this.mnuRepartidores});

            this.mnuEdificios.Name = "mnuEdificios";
            this.mnuEdificios.Text = "Edificios";
            this.mnuEdificios.Enabled = false;

            this.mnuLockers.Name = "mnuLockers";
            this.mnuLockers.Text = "Lockers";
            this.mnuLockers.Enabled = false;

            this.mnuResidentes.Name = "mnuResidentes";
            this.mnuResidentes.Text = "Residentes";
            this.mnuResidentes.Enabled = false;

            this.mnuRepartidores.Name = "mnuRepartidores";
            this.mnuRepartidores.Text = "Repartidores";
            this.mnuRepartidores.Enabled = false;

            this.mnuOperacion.Name = "mnuOperacion";
            this.mnuOperacion.Text = "&Operación";
            this.mnuOperacion.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.mnuReservar, this.mnuEntrega, this.mnuRetiro});

            this.mnuReservar.Name = "mnuReservar";
            this.mnuReservar.Text = "Reservar locker";
            this.mnuReservar.Enabled = false;

            this.mnuEntrega.Name = "mnuEntrega";
            this.mnuEntrega.Text = "Registrar entrega";
            this.mnuEntrega.Enabled = false;

            this.mnuRetiro.Name = "mnuRetiro";
            this.mnuRetiro.Text = "Registrar retiro";
            this.mnuRetiro.Enabled = false;

            this.mnuInformacion.Name = "mnuInformacion";
            this.mnuInformacion.Text = "&Información";
            this.mnuInformacion.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.mnuReportes, this.mnuBitacora});

            this.mnuReportes.Name = "mnuReportes";
            this.mnuReportes.Text = "Reportes";
            this.mnuReportes.Enabled = false;

            this.mnuBitacora.Name = "mnuBitacora";
            this.mnuBitacora.Text = "Bitácora";
            this.mnuBitacora.Enabled = false;

            this.barraEstado.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.lblEstadoUsuario});
            this.barraEstado.Location = new System.Drawing.Point(0, 528);
            this.barraEstado.Name = "barraEstado";
            this.barraEstado.Size = new System.Drawing.Size(900, 22);

            this.lblEstadoUsuario.Name = "lblEstadoUsuario";
            this.lblEstadoUsuario.Text = "";

            this.ClientSize = new System.Drawing.Size(900, 550);
            this.Controls.Add(this.barraEstado);
            this.Controls.Add(this.menuPrincipal);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuPrincipal;
            this.Name = "FrmPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lockers Inteligentes";
            this.Load += new System.EventHandler(this.FrmPrincipal_Load);
            this.menuPrincipal.ResumeLayout(false);
            this.barraEstado.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
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
    }
}