namespace LockersInteligentes.UI.Gestion
{
    partial class FrmRepartidores
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
            dgvRepartidores = new System.Windows.Forms.DataGridView();
            lblNombre = new System.Windows.Forms.Label();
            txtNombre = new System.Windows.Forms.TextBox();
            lblApellido = new System.Windows.Forms.Label();
            txtApellido = new System.Windows.Forms.TextBox();
            lblDni = new System.Windows.Forms.Label();
            txtDni = new System.Windows.Forms.TextBox();
            lblEmpresa = new System.Windows.Forms.Label();
            txtEmpresa = new System.Windows.Forms.TextBox();
            lblTelefono = new System.Windows.Forms.Label();
            txtTelefono = new System.Windows.Forms.TextBox();
            btnNuevo = new System.Windows.Forms.Button();
            btnGuardar = new System.Windows.Forms.Button();
            btnDesactivar = new System.Windows.Forms.Button();
            btnReactivar = new System.Windows.Forms.Button();
            lblMensaje = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)dgvRepartidores).BeginInit();
            SuspendLayout();
            // 
            // dgvRepartidores
            // 
            dgvRepartidores.AllowUserToAddRows = false;
            dgvRepartidores.AllowUserToDeleteRows = false;
            dgvRepartidores.BackgroundColor = System.Drawing.Color.MidnightBlue;
            dgvRepartidores.ColumnHeadersHeight = 29;
            dgvRepartidores.Location = new System.Drawing.Point(20, 20);
            dgvRepartidores.MultiSelect = false;
            dgvRepartidores.Name = "dgvRepartidores";
            dgvRepartidores.ReadOnly = true;
            dgvRepartidores.RowHeadersVisible = false;
            dgvRepartidores.RowHeadersWidth = 51;
            dgvRepartidores.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvRepartidores.Size = new System.Drawing.Size(700, 230);
            dgvRepartidores.TabIndex = 0;
            dgvRepartidores.SelectionChanged += dgvRepartidores_SelectionChanged;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Location = new System.Drawing.Point(20, 268);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new System.Drawing.Size(64, 20);
            lblNombre.TabIndex = 1;
            lblNombre.Text = "Nombre";
            // 
            // txtNombre
            // 
            txtNombre.Location = new System.Drawing.Point(20, 288);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new System.Drawing.Size(200, 27);
            txtNombre.TabIndex = 1;
            // 
            // lblApellido
            // 
            lblApellido.AutoSize = true;
            lblApellido.Location = new System.Drawing.Point(240, 268);
            lblApellido.Name = "lblApellido";
            lblApellido.Size = new System.Drawing.Size(66, 20);
            lblApellido.TabIndex = 2;
            lblApellido.Text = "Apellido";
            // 
            // txtApellido
            // 
            txtApellido.Location = new System.Drawing.Point(240, 288);
            txtApellido.Name = "txtApellido";
            txtApellido.Size = new System.Drawing.Size(200, 27);
            txtApellido.TabIndex = 2;
            // 
            // lblDni
            // 
            lblDni.AutoSize = true;
            lblDni.Location = new System.Drawing.Point(460, 268);
            lblDni.Name = "lblDni";
            lblDni.Size = new System.Drawing.Size(35, 20);
            lblDni.TabIndex = 3;
            lblDni.Text = "DNI";
            // 
            // txtDni
            // 
            txtDni.Location = new System.Drawing.Point(460, 288);
            txtDni.Name = "txtDni";
            txtDni.Size = new System.Drawing.Size(140, 27);
            txtDni.TabIndex = 3;
            // 
            // lblEmpresa
            // 
            lblEmpresa.AutoSize = true;
            lblEmpresa.Location = new System.Drawing.Point(20, 328);
            lblEmpresa.Name = "lblEmpresa";
            lblEmpresa.Size = new System.Drawing.Size(66, 20);
            lblEmpresa.TabIndex = 4;
            lblEmpresa.Text = "Empresa";
            // 
            // txtEmpresa
            // 
            txtEmpresa.Location = new System.Drawing.Point(20, 348);
            txtEmpresa.Name = "txtEmpresa";
            txtEmpresa.Size = new System.Drawing.Size(200, 27);
            txtEmpresa.TabIndex = 4;
            // 
            // lblTelefono
            // 
            lblTelefono.AutoSize = true;
            lblTelefono.Location = new System.Drawing.Point(240, 328);
            lblTelefono.Name = "lblTelefono";
            lblTelefono.Size = new System.Drawing.Size(67, 20);
            lblTelefono.TabIndex = 5;
            lblTelefono.Text = "Teléfono";
            // 
            // txtTelefono
            // 
            txtTelefono.Location = new System.Drawing.Point(240, 348);
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new System.Drawing.Size(200, 27);
            txtTelefono.TabIndex = 5;
            // 
            // btnNuevo
            // 
            btnNuevo.BackColor = System.Drawing.Color.CornflowerBlue;
            btnNuevo.FlatAppearance.BorderSize = 2;
            btnNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnNuevo.Location = new System.Drawing.Point(620, 284);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new System.Drawing.Size(100, 30);
            btnNuevo.TabIndex = 6;
            btnNuevo.Text = "Nuevo";
            btnNuevo.UseVisualStyleBackColor = false;
            btnNuevo.Click += btnNuevo_Click;
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = System.Drawing.Color.CornflowerBlue;
            btnGuardar.FlatAppearance.BorderSize = 2;
            btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnGuardar.Location = new System.Drawing.Point(620, 320);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new System.Drawing.Size(100, 30);
            btnGuardar.TabIndex = 7;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnDesactivar
            // 
            btnDesactivar.BackColor = System.Drawing.Color.CornflowerBlue;
            btnDesactivar.FlatAppearance.BorderSize = 2;
            btnDesactivar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnDesactivar.Location = new System.Drawing.Point(460, 344);
            btnDesactivar.Name = "btnDesactivar";
            btnDesactivar.Size = new System.Drawing.Size(140, 30);
            btnDesactivar.TabIndex = 8;
            btnDesactivar.Text = "Dar de baja";
            btnDesactivar.UseVisualStyleBackColor = false;
            btnDesactivar.Click += btnDesactivar_Click;
            // 
            // btnReactivar
            // 
            btnReactivar.BackColor = System.Drawing.Color.CornflowerBlue;
            btnReactivar.FlatAppearance.BorderSize = 2;
            btnReactivar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnReactivar.Location = new System.Drawing.Point(460, 380);
            btnReactivar.Name = "btnReactivar";
            btnReactivar.Size = new System.Drawing.Size(140, 32);
            btnReactivar.TabIndex = 9;
            btnReactivar.Text = "Reactivar";
            btnReactivar.UseVisualStyleBackColor = false;
            btnReactivar.Click += btnReactivar_Click;
            // 
            // lblMensaje
            // 
            lblMensaje.Location = new System.Drawing.Point(18, 396);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new System.Drawing.Size(702, 40);
            lblMensaje.TabIndex = 10;
            // 
            // FrmRepartidores
            // 
            BackColor = System.Drawing.Color.LightSkyBlue;
            ClientSize = new System.Drawing.Size(800, 457);
            Controls.Add(dgvRepartidores);
            Controls.Add(lblNombre);
            Controls.Add(txtNombre);
            Controls.Add(lblApellido);
            Controls.Add(txtApellido);
            Controls.Add(lblDni);
            Controls.Add(txtDni);
            Controls.Add(lblEmpresa);
            Controls.Add(txtEmpresa);
            Controls.Add(lblTelefono);
            Controls.Add(txtTelefono);
            Controls.Add(btnNuevo);
            Controls.Add(btnGuardar);
            Controls.Add(btnDesactivar);
            Controls.Add(btnReactivar);
            Controls.Add(lblMensaje);
            Font = new System.Drawing.Font("Segoe UI", 9F);
            Name = "FrmRepartidores";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Gestión de repartidores";
            Load += FrmRepartidores_Load;
            ((System.ComponentModel.ISupportInitialize)dgvRepartidores).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.DataGridView dgvRepartidores;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblApellido;
        private System.Windows.Forms.TextBox txtApellido;
        private System.Windows.Forms.Label lblDni;
        private System.Windows.Forms.TextBox txtDni;
        private System.Windows.Forms.Label lblEmpresa;
        private System.Windows.Forms.TextBox txtEmpresa;
        private System.Windows.Forms.Label lblTelefono;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnDesactivar;
        private System.Windows.Forms.Button btnReactivar;
        private System.Windows.Forms.Label lblMensaje;
    }
}