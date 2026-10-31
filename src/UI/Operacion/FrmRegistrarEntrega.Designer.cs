namespace LockersInteligentes.UI.Operacion
{
    partial class FrmRegistrarEntrega
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
            lblRepartidor = new System.Windows.Forms.Label();
            cboRepartidor = new System.Windows.Forms.ComboBox();
            lblPin = new System.Windows.Forms.Label();
            txtPin = new System.Windows.Forms.TextBox();
            btnRegistrar = new System.Windows.Forms.Button();
            btnActualizar = new System.Windows.Forms.Button();
            lblMensaje = new System.Windows.Forms.Label();
            grpResultado = new System.Windows.Forms.GroupBox();
            lblResultado = new System.Windows.Forms.Label();
            lblPinRetiro = new System.Windows.Forms.Label();
            lblAvisoPin = new System.Windows.Forms.Label();
            btnLiberar = new System.Windows.Forms.Button();
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
            lblTitulo.Size = new System.Drawing.Size(212, 32);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Registrar entrega";
            // 
            // lblAyuda
            // 
            lblAyuda.AutoSize = true;
            lblAyuda.ForeColor = System.Drawing.Color.Black;
            lblAyuda.Location = new System.Drawing.Point(22, 50);
            lblAyuda.Name = "lblAyuda";
            lblAyuda.Size = new System.Drawing.Size(516, 20);
            lblAyuda.TabIndex = 1;
            lblAyuda.Text = "Seleccioná la orden, elegí el repartidor e ingresá el PIN de apertura que trae.";
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
            dgvPendientes.Size = new System.Drawing.Size(740, 200);
            dgvPendientes.TabIndex = 0;
            // 
            // lblRepartidor
            // 
            lblRepartidor.AutoSize = true;
            lblRepartidor.Location = new System.Drawing.Point(20, 296);
            lblRepartidor.Name = "lblRepartidor";
            lblRepartidor.Size = new System.Drawing.Size(80, 20);
            lblRepartidor.TabIndex = 2;
            lblRepartidor.Text = "Repartidor";
            // 
            // cboRepartidor
            // 
            cboRepartidor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboRepartidor.Location = new System.Drawing.Point(20, 316);
            cboRepartidor.Name = "cboRepartidor";
            cboRepartidor.Size = new System.Drawing.Size(320, 28);
            cboRepartidor.TabIndex = 1;
            // 
            // lblPin
            // 
            lblPin.AutoSize = true;
            lblPin.Location = new System.Drawing.Point(360, 296);
            lblPin.Name = "lblPin";
            lblPin.Size = new System.Drawing.Size(113, 20);
            lblPin.TabIndex = 3;
            lblPin.Text = "PIN de apertura";
            // 
            // txtPin
            // 
            txtPin.Font = new System.Drawing.Font("Consolas", 14F);
            txtPin.Location = new System.Drawing.Point(360, 314);
            txtPin.MaxLength = 6;
            txtPin.Name = "txtPin";
            txtPin.Size = new System.Drawing.Size(140, 35);
            txtPin.TabIndex = 2;
            txtPin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnRegistrar
            // 
            btnRegistrar.BackColor = System.Drawing.Color.CornflowerBlue;
            btnRegistrar.FlatAppearance.BorderSize = 2;
            btnRegistrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnRegistrar.Location = new System.Drawing.Point(520, 296);
            btnRegistrar.Name = "btnRegistrar";
            btnRegistrar.Size = new System.Drawing.Size(140, 34);
            btnRegistrar.TabIndex = 3;
            btnRegistrar.Text = "Registrar entrega";
            btnRegistrar.UseVisualStyleBackColor = false;
            btnRegistrar.Click += btnRegistrar_Click;
            // 
            // btnActualizar
            // 
            btnActualizar.BackColor = System.Drawing.Color.CornflowerBlue;
            btnActualizar.FlatAppearance.BorderSize = 2;
            btnActualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnActualizar.Location = new System.Drawing.Point(672, 296);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new System.Drawing.Size(88, 34);
            btnActualizar.TabIndex = 4;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = false;
            btnActualizar.Click += btnActualizar_Click;
            // 
            // lblMensaje
            // 
            lblMensaje.Location = new System.Drawing.Point(20, 352);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new System.Drawing.Size(500, 24);
            lblMensaje.TabIndex = 5;
            // 
            // grpResultado
            // 
            grpResultado.BackColor = System.Drawing.Color.SkyBlue;
            grpResultado.Controls.Add(lblResultado);
            grpResultado.Controls.Add(lblPinRetiro);
            grpResultado.Controls.Add(lblAvisoPin);
            grpResultado.Location = new System.Drawing.Point(20, 388);
            grpResultado.Name = "grpResultado";
            grpResultado.Size = new System.Drawing.Size(740, 180);
            grpResultado.TabIndex = 6;
            grpResultado.TabStop = false;
            grpResultado.Text = "Entrega registrada";
            grpResultado.Visible = false;
            // 
            // lblResultado
            // 
            lblResultado.Location = new System.Drawing.Point(16, 28);
            lblResultado.Name = "lblResultado";
            lblResultado.Size = new System.Drawing.Size(400, 140);
            lblResultado.TabIndex = 0;
            // 
            // lblPinRetiro
            // 
            lblPinRetiro.Font = new System.Drawing.Font("Consolas", 24F, System.Drawing.FontStyle.Bold);
            lblPinRetiro.ForeColor = System.Drawing.Color.SeaGreen;
            lblPinRetiro.Location = new System.Drawing.Point(430, 36);
            lblPinRetiro.Name = "lblPinRetiro";
            lblPinRetiro.Size = new System.Drawing.Size(290, 48);
            lblPinRetiro.TabIndex = 1;
            lblPinRetiro.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAvisoPin
            // 
            lblAvisoPin.ForeColor = System.Drawing.Color.Gray;
            lblAvisoPin.Location = new System.Drawing.Point(430, 90);
            lblAvisoPin.Name = "lblAvisoPin";
            lblAvisoPin.Size = new System.Drawing.Size(290, 72);
            lblAvisoPin.TabIndex = 2;
            // 
            // btnLiberar
            // 
            btnLiberar.BackColor = System.Drawing.Color.CornflowerBlue;
            btnLiberar.FlatAppearance.BorderSize = 2;
            btnLiberar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnLiberar.Location = new System.Drawing.Point(520, 336);
            btnLiberar.Name = "btnLiberar";
            btnLiberar.Size = new System.Drawing.Size(240, 46);
            btnLiberar.TabIndex = 7;
            btnLiberar.Text = "Liberar reserva vencida";
            btnLiberar.UseVisualStyleBackColor = false;
            btnLiberar.Click += btnLiberar_Click;
            // 
            // FrmRegistrarEntrega
            // 
            BackColor = System.Drawing.Color.LightSkyBlue;
            ClientSize = new System.Drawing.Size(784, 586);
            Controls.Add(lblTitulo);
            Controls.Add(lblAyuda);
            Controls.Add(dgvPendientes);
            Controls.Add(lblRepartidor);
            Controls.Add(cboRepartidor);
            Controls.Add(lblPin);
            Controls.Add(txtPin);
            Controls.Add(btnRegistrar);
            Controls.Add(btnActualizar);
            Controls.Add(lblMensaje);
            Controls.Add(grpResultado);
            Controls.Add(btnLiberar);
            Font = new System.Drawing.Font("Segoe UI", 9F);
            Name = "FrmRegistrarEntrega";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Registrar entrega";
            Load += FrmRegistrarEntrega_Load;
            ((System.ComponentModel.ISupportInitialize)dgvPendientes).EndInit();
            grpResultado.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblAyuda;
        private System.Windows.Forms.DataGridView dgvPendientes;
        private System.Windows.Forms.Label lblRepartidor;
        private System.Windows.Forms.ComboBox cboRepartidor;
        private System.Windows.Forms.Label lblPin;
        private System.Windows.Forms.TextBox txtPin;
        private System.Windows.Forms.Button btnRegistrar;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Label lblMensaje;
        private System.Windows.Forms.GroupBox grpResultado;
        private System.Windows.Forms.Label lblResultado;
        private System.Windows.Forms.Label lblPinRetiro;
        private System.Windows.Forms.Label lblAvisoPin;
        private System.Windows.Forms.Button btnLiberar;
    }

}
