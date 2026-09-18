namespace LockersInteligentes.UI
{
    partial class FrmUsuarios
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
            dgvUsuarios = new System.Windows.Forms.DataGridView();
            lblNombreUsuario = new System.Windows.Forms.Label();
            txtNombreUsuario = new System.Windows.Forms.TextBox();
            lblNombre = new System.Windows.Forms.Label();
            txtNombre = new System.Windows.Forms.TextBox();
            lblApellido = new System.Windows.Forms.Label();
            txtApellido = new System.Windows.Forms.TextBox();
            lblRol = new System.Windows.Forms.Label();
            cboRol = new System.Windows.Forms.ComboBox();
            lblPassword = new System.Windows.Forms.Label();
            txtPassword = new System.Windows.Forms.TextBox();
            lblAyudaPassword = new System.Windows.Forms.Label();
            btnNuevo = new System.Windows.Forms.Button();
            btnGuardar = new System.Windows.Forms.Button();
            btnDesactivar = new System.Windows.Forms.Button();
            lblMensaje = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).BeginInit();
            SuspendLayout();
            // 
            // dgvUsuarios
            // 
            dgvUsuarios.AllowUserToAddRows = false;
            dgvUsuarios.AllowUserToDeleteRows = false;
            dgvUsuarios.BackgroundColor = System.Drawing.Color.RoyalBlue;
            dgvUsuarios.ColumnHeadersHeight = 29;
            dgvUsuarios.Location = new System.Drawing.Point(20, 20);
            dgvUsuarios.MultiSelect = false;
            dgvUsuarios.Name = "dgvUsuarios";
            dgvUsuarios.ReadOnly = true;
            dgvUsuarios.RowHeadersVisible = false;
            dgvUsuarios.RowHeadersWidth = 51;
            dgvUsuarios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvUsuarios.Size = new System.Drawing.Size(640, 220);
            dgvUsuarios.TabIndex = 0;
            dgvUsuarios.CellContentClick += dgvUsuarios_CellContentClick;
            dgvUsuarios.SelectionChanged += dgvUsuarios_SelectionChanged;
            // 
            // lblNombreUsuario
            // 
            lblNombreUsuario.AutoSize = true;
            lblNombreUsuario.Location = new System.Drawing.Point(20, 262);
            lblNombreUsuario.Name = "lblNombreUsuario";
            lblNombreUsuario.Size = new System.Drawing.Size(59, 20);
            lblNombreUsuario.TabIndex = 1;
            lblNombreUsuario.Text = "Usuario";
            // 
            // txtNombreUsuario
            // 
            txtNombreUsuario.Location = new System.Drawing.Point(20, 282);
            txtNombreUsuario.Name = "txtNombreUsuario";
            txtNombreUsuario.Size = new System.Drawing.Size(200, 27);
            txtNombreUsuario.TabIndex = 2;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Location = new System.Drawing.Point(240, 262);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new System.Drawing.Size(64, 20);
            lblNombre.TabIndex = 3;
            lblNombre.Text = "Nombre";
            // 
            // txtNombre
            // 
            txtNombre.Location = new System.Drawing.Point(240, 282);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new System.Drawing.Size(200, 27);
            txtNombre.TabIndex = 4;
            // 
            // lblApellido
            // 
            lblApellido.AutoSize = true;
            lblApellido.Location = new System.Drawing.Point(460, 262);
            lblApellido.Name = "lblApellido";
            lblApellido.Size = new System.Drawing.Size(66, 20);
            lblApellido.TabIndex = 5;
            lblApellido.Text = "Apellido";
            // 
            // txtApellido
            // 
            txtApellido.Location = new System.Drawing.Point(460, 282);
            txtApellido.Name = "txtApellido";
            txtApellido.Size = new System.Drawing.Size(200, 27);
            txtApellido.TabIndex = 6;
            // 
            // lblRol
            // 
            lblRol.AutoSize = true;
            lblRol.Location = new System.Drawing.Point(20, 320);
            lblRol.Name = "lblRol";
            lblRol.Size = new System.Drawing.Size(31, 20);
            lblRol.TabIndex = 7;
            lblRol.Text = "Rol";
            // 
            // cboRol
            // 
            cboRol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboRol.Location = new System.Drawing.Point(20, 340);
            cboRol.Name = "cboRol";
            cboRol.Size = new System.Drawing.Size(200, 28);
            cboRol.TabIndex = 8;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Location = new System.Drawing.Point(240, 320);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new System.Drawing.Size(83, 20);
            lblPassword.TabIndex = 9;
            lblPassword.Text = "Contraseña";
            // 
            // txtPassword
            // 
            txtPassword.Location = new System.Drawing.Point(240, 340);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new System.Drawing.Size(200, 27);
            txtPassword.TabIndex = 10;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // lblAyudaPassword
            // 
            lblAyudaPassword.AutoSize = true;
            lblAyudaPassword.ForeColor = System.Drawing.Color.Gray;
            lblAyudaPassword.Location = new System.Drawing.Point(240, 374);
            lblAyudaPassword.Name = "lblAyudaPassword";
            lblAyudaPassword.Size = new System.Drawing.Size(305, 20);
            lblAyudaPassword.TabIndex = 11;
            lblAyudaPassword.Text = "Al modificar, dejala vacía para no cambiarla.";
            // 
            // btnNuevo
            // 
            btnNuevo.BackColor = System.Drawing.Color.CornflowerBlue;
            btnNuevo.FlatAppearance.BorderSize = 2;
            btnNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnNuevo.Location = new System.Drawing.Point(460, 338);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new System.Drawing.Size(95, 30);
            btnNuevo.TabIndex = 12;
            btnNuevo.Text = "Nuevo";
            btnNuevo.UseVisualStyleBackColor = false;
            btnNuevo.Click += btnNuevo_Click;
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = System.Drawing.Color.CornflowerBlue;
            btnGuardar.FlatAppearance.BorderSize = 2;
            btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnGuardar.Location = new System.Drawing.Point(565, 338);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new System.Drawing.Size(95, 30);
            btnGuardar.TabIndex = 13;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnDesactivar
            // 
            btnDesactivar.BackColor = System.Drawing.Color.CornflowerBlue;
            btnDesactivar.FlatAppearance.BorderSize = 2;
            btnDesactivar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnDesactivar.Location = new System.Drawing.Point(565, 374);
            btnDesactivar.Name = "btnDesactivar";
            btnDesactivar.Size = new System.Drawing.Size(95, 30);
            btnDesactivar.TabIndex = 14;
            btnDesactivar.Text = "Desactivar";
            btnDesactivar.UseVisualStyleBackColor = false;
            btnDesactivar.Click += btnDesactivar_Click;
            // 
            // lblMensaje
            // 
            lblMensaje.Location = new System.Drawing.Point(20, 416);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new System.Drawing.Size(640, 24);
            lblMensaje.TabIndex = 15;
            // 
            // FrmUsuarios
            // 
            BackColor = System.Drawing.Color.LightSkyBlue;
            ClientSize = new System.Drawing.Size(684, 451);
            Controls.Add(dgvUsuarios);
            Controls.Add(lblNombreUsuario);
            Controls.Add(txtNombreUsuario);
            Controls.Add(lblNombre);
            Controls.Add(txtNombre);
            Controls.Add(lblApellido);
            Controls.Add(txtApellido);
            Controls.Add(lblRol);
            Controls.Add(cboRol);
            Controls.Add(lblPassword);
            Controls.Add(txtPassword);
            Controls.Add(lblAyudaPassword);
            Controls.Add(btnNuevo);
            Controls.Add(btnGuardar);
            Controls.Add(btnDesactivar);
            Controls.Add(lblMensaje);
            Font = new System.Drawing.Font("Segoe UI", 9F);
            Name = "FrmUsuarios";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Gestión de usuarios";
            Load += FrmUsuarios_Load;
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.DataGridView dgvUsuarios;
        private System.Windows.Forms.Label lblNombreUsuario;
        private System.Windows.Forms.TextBox txtNombreUsuario;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblApellido;
        private System.Windows.Forms.TextBox txtApellido;
        private System.Windows.Forms.Label lblRol;
        private System.Windows.Forms.ComboBox cboRol;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblAyudaPassword;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnDesactivar;
        private System.Windows.Forms.Label lblMensaje;
    }
}