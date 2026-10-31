namespace LockersInteligentes.UI.Operacion
{
    partial class FrmRegistrarRetiro
    {

        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            lblTitulo = new System.Windows.Forms.Label();
            lblAyuda = new System.Windows.Forms.Label();
            dgvPendientes = new System.Windows.Forms.DataGridView();
            lblPin = new System.Windows.Forms.Label();
            txtPin = new System.Windows.Forms.TextBox();
            btnRegistrar = new System.Windows.Forms.Button();
            btnActualizar = new System.Windows.Forms.Button();
            lblMensaje = new System.Windows.Forms.Label();
            grpResultado = new System.Windows.Forms.GroupBox();
            lblResultado = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)dgvPendientes).BeginInit();
            grpResultado.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            lblTitulo.Location = new System.Drawing.Point(20, 18);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new System.Drawing.Size(188, 32);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Registrar retiro";
            // 
            // lblAyuda
            // 
            lblAyuda.AutoSize = true;
            lblAyuda.ForeColor = System.Drawing.Color.Black;
            lblAyuda.Location = new System.Drawing.Point(22, 50);
            lblAyuda.Name = "lblAyuda";
            lblAyuda.Size = new System.Drawing.Size(486, 20);
            lblAyuda.TabIndex = 1;
            lblAyuda.Text = "Seleccioná el paquete e ingresá el PIN de retiro que recibió el residente.";
            // 
            // dgvPendientes
            // 
            dgvPendientes.AllowUserToAddRows = false;
            dgvPendientes.AllowUserToDeleteRows = false;
            dgvPendientes.BackgroundColor = System.Drawing.Color.CornflowerBlue;
            dgvPendientes.ColumnHeadersHeight = 29;
            dgvPendientes.Location = new System.Drawing.Point(20, 78);
            dgvPendientes.MultiSelect = false;
            dgvPendientes.Name = "dgvPendientes";
            dgvPendientes.ReadOnly = true;
            dgvPendientes.RowHeadersVisible = false;
            dgvPendientes.RowHeadersWidth = 51;
            dgvPendientes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvPendientes.Size = new System.Drawing.Size(740, 220);
            dgvPendientes.TabIndex = 0;
            // 
            // lblPin
            // 
            lblPin.AutoSize = true;
            lblPin.Location = new System.Drawing.Point(20, 316);
            lblPin.Name = "lblPin";
            lblPin.Size = new System.Drawing.Size(93, 20);
            lblPin.TabIndex = 2;
            lblPin.Text = "PIN de retiro";
            // 
            // txtPin
            // 
            txtPin.Font = new System.Drawing.Font("Consolas", 14F);
            txtPin.Location = new System.Drawing.Point(20, 336);
            txtPin.MaxLength = 6;
            txtPin.Name = "txtPin";
            txtPin.Size = new System.Drawing.Size(140, 35);
            txtPin.TabIndex = 1;
            txtPin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnRegistrar
            // 
            btnRegistrar.BackColor = System.Drawing.Color.CornflowerBlue;
            btnRegistrar.FlatAppearance.BorderSize = 2;
            btnRegistrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnRegistrar.Location = new System.Drawing.Point(180, 336);
            btnRegistrar.Name = "btnRegistrar";
            btnRegistrar.Size = new System.Drawing.Size(160, 34);
            btnRegistrar.TabIndex = 2;
            btnRegistrar.Text = "Registrar retiro";
            btnRegistrar.UseVisualStyleBackColor = false;
            btnRegistrar.Click += btnRegistrar_Click;
            // 
            // btnActualizar
            // 
            btnActualizar.BackColor = System.Drawing.Color.CornflowerBlue;
            btnActualizar.FlatAppearance.BorderSize = 2;
            btnActualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnActualizar.Location = new System.Drawing.Point(352, 336);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new System.Drawing.Size(100, 34);
            btnActualizar.TabIndex = 3;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = false;
            btnActualizar.Click += btnActualizar_Click;
            // 
            // lblMensaje
            // 
            lblMensaje.Location = new System.Drawing.Point(18, 382);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new System.Drawing.Size(742, 24);
            lblMensaje.TabIndex = 4;
            // 
            // grpResultado
            // 
            grpResultado.Controls.Add(lblResultado);
            grpResultado.Location = new System.Drawing.Point(20, 412);
            grpResultado.Name = "grpResultado";
            grpResultado.Size = new System.Drawing.Size(740, 150);
            grpResultado.TabIndex = 5;
            grpResultado.TabStop = false;
            grpResultado.Text = "Retiro registrado";
            grpResultado.Visible = false;
            // 
            // lblResultado
            // 
            lblResultado.Location = new System.Drawing.Point(16, 28);
            lblResultado.Name = "lblResultado";
            lblResultado.Size = new System.Drawing.Size(708, 112);
            lblResultado.TabIndex = 0;
            // 
            // FrmRegistrarRetiro
            // 
            BackColor = System.Drawing.Color.LightSkyBlue;
            ClientSize = new System.Drawing.Size(784, 580);
            Controls.Add(lblTitulo);
            Controls.Add(lblAyuda);
            Controls.Add(dgvPendientes);
            Controls.Add(lblPin);
            Controls.Add(txtPin);
            Controls.Add(btnRegistrar);
            Controls.Add(btnActualizar);
            Controls.Add(lblMensaje);
            Controls.Add(grpResultado);
            Font = new System.Drawing.Font("Segoe UI", 9F);
            Name = "FrmRegistrarRetiro";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Registrar retiro";
            Load += FrmRegistrarRetiro_Load;
            ((System.ComponentModel.ISupportInitialize)dgvPendientes).EndInit();
            grpResultado.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblAyuda;
        private System.Windows.Forms.DataGridView dgvPendientes;
        private System.Windows.Forms.Label lblPin;
        private System.Windows.Forms.TextBox txtPin;
        private System.Windows.Forms.Button btnRegistrar;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Label lblMensaje;
        private System.Windows.Forms.GroupBox grpResultado;
        private System.Windows.Forms.Label lblResultado;
    }
}
