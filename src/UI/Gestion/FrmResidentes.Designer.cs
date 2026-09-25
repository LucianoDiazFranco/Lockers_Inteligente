namespace LockersInteligentes.UI.Gestion
{
    partial class FrmResidentes
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
            lblFiltroEdificio = new System.Windows.Forms.Label();
            cboFiltroEdificio = new System.Windows.Forms.ComboBox();
            dgvResidentes = new System.Windows.Forms.DataGridView();
            lblNombre = new System.Windows.Forms.Label();
            txtNombre = new System.Windows.Forms.TextBox();
            lblApellido = new System.Windows.Forms.Label();
            txtApellido = new System.Windows.Forms.TextBox();
            lblDni = new System.Windows.Forms.Label();
            txtDni = new System.Windows.Forms.TextBox();
            lblCorreo = new System.Windows.Forms.Label();
            txtCorreo = new System.Windows.Forms.TextBox();
            lblPiso = new System.Windows.Forms.Label();
            txtPiso = new System.Windows.Forms.TextBox();
            lblTelefono = new System.Windows.Forms.Label();
            txtTelefono = new System.Windows.Forms.TextBox();
            btnNuevo = new System.Windows.Forms.Button();
            btnGuardar = new System.Windows.Forms.Button();
            btnDesactivar = new System.Windows.Forms.Button();
            btnReactivar = new System.Windows.Forms.Button();
            lblMensaje = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)dgvResidentes).BeginInit();
            SuspendLayout();
            // 
            // lblFiltroEdificio
            // 
            lblFiltroEdificio.AutoSize = true;
            lblFiltroEdificio.Location = new System.Drawing.Point(20, 18);
            lblFiltroEdificio.Name = "lblFiltroEdificio";
            lblFiltroEdificio.Size = new System.Drawing.Size(59, 20);
            lblFiltroEdificio.TabIndex = 0;
            lblFiltroEdificio.Text = "Edificio";
            // 
            // cboFiltroEdificio
            // 
            cboFiltroEdificio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboFiltroEdificio.Location = new System.Drawing.Point(80, 14);
            cboFiltroEdificio.Name = "cboFiltroEdificio";
            cboFiltroEdificio.Size = new System.Drawing.Size(280, 28);
            cboFiltroEdificio.TabIndex = 0;
            cboFiltroEdificio.SelectedIndexChanged += cboFiltroEdificio_SelectedIndexChanged;
            // 
            // dgvResidentes
            // 
            dgvResidentes.AllowUserToAddRows = false;
            dgvResidentes.AllowUserToDeleteRows = false;
            dgvResidentes.BackgroundColor = System.Drawing.Color.MidnightBlue;
            dgvResidentes.ColumnHeadersHeight = 29;
            dgvResidentes.Location = new System.Drawing.Point(20, 52);
            dgvResidentes.MultiSelect = false;
            dgvResidentes.Name = "dgvResidentes";
            dgvResidentes.ReadOnly = true;
            dgvResidentes.RowHeadersVisible = false;
            dgvResidentes.RowHeadersWidth = 51;
            dgvResidentes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvResidentes.Size = new System.Drawing.Size(700, 220);
            dgvResidentes.TabIndex = 1;
            dgvResidentes.SelectionChanged += dgvResidentes_SelectionChanged;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Location = new System.Drawing.Point(20, 290);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new System.Drawing.Size(64, 20);
            lblNombre.TabIndex = 2;
            lblNombre.Text = "Nombre";
            // 
            // txtNombre
            // 
            txtNombre.Location = new System.Drawing.Point(20, 310);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new System.Drawing.Size(200, 27);
            txtNombre.TabIndex = 2;
            // 
            // lblApellido
            // 
            lblApellido.AutoSize = true;
            lblApellido.Location = new System.Drawing.Point(240, 290);
            lblApellido.Name = "lblApellido";
            lblApellido.Size = new System.Drawing.Size(66, 20);
            lblApellido.TabIndex = 3;
            lblApellido.Text = "Apellido";
            // 
            // txtApellido
            // 
            txtApellido.Location = new System.Drawing.Point(240, 310);
            txtApellido.Name = "txtApellido";
            txtApellido.Size = new System.Drawing.Size(200, 27);
            txtApellido.TabIndex = 3;
            // 
            // lblDni
            // 
            lblDni.AutoSize = true;
            lblDni.Location = new System.Drawing.Point(460, 290);
            lblDni.Name = "lblDni";
            lblDni.Size = new System.Drawing.Size(35, 20);
            lblDni.TabIndex = 4;
            lblDni.Text = "DNI";
            // 
            // txtDni
            // 
            txtDni.Location = new System.Drawing.Point(460, 310);
            txtDni.Name = "txtDni";
            txtDni.Size = new System.Drawing.Size(140, 27);
            txtDni.TabIndex = 4;
            // 
            // lblCorreo
            // 
            lblCorreo.AutoSize = true;
            lblCorreo.Location = new System.Drawing.Point(20, 350);
            lblCorreo.Name = "lblCorreo";
            lblCorreo.Size = new System.Drawing.Size(132, 20);
            lblCorreo.TabIndex = 5;
            lblCorreo.Text = "Correo electrónico";
            // 
            // txtCorreo
            // 
            txtCorreo.Location = new System.Drawing.Point(20, 370);
            txtCorreo.Name = "txtCorreo";
            txtCorreo.Size = new System.Drawing.Size(280, 27);
            txtCorreo.TabIndex = 5;
            // 
            // lblPiso
            // 
            lblPiso.AutoSize = true;
            lblPiso.Location = new System.Drawing.Point(320, 350);
            lblPiso.Name = "lblPiso";
            lblPiso.Size = new System.Drawing.Size(92, 20);
            lblPiso.TabIndex = 6;
            lblPiso.Text = "Piso / Depto";
            // 
            // txtPiso
            // 
            txtPiso.Location = new System.Drawing.Point(320, 370);
            txtPiso.Name = "txtPiso";
            txtPiso.Size = new System.Drawing.Size(120, 27);
            txtPiso.TabIndex = 6;
            // 
            // lblTelefono
            // 
            lblTelefono.AutoSize = true;
            lblTelefono.Location = new System.Drawing.Point(460, 350);
            lblTelefono.Name = "lblTelefono";
            lblTelefono.Size = new System.Drawing.Size(67, 20);
            lblTelefono.TabIndex = 7;
            lblTelefono.Text = "Teléfono";
            // 
            // txtTelefono
            // 
            txtTelefono.Location = new System.Drawing.Point(460, 370);
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new System.Drawing.Size(140, 27);
            txtTelefono.TabIndex = 7;
            // 
            // btnNuevo
            // 
            btnNuevo.BackColor = System.Drawing.Color.CornflowerBlue;
            btnNuevo.FlatAppearance.BorderSize = 2;
            btnNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnNuevo.Location = new System.Drawing.Point(620, 306);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new System.Drawing.Size(100, 30);
            btnNuevo.TabIndex = 8;
            btnNuevo.Text = "Nuevo";
            btnNuevo.UseVisualStyleBackColor = false;
            btnNuevo.Click += btnNuevo_Click;
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = System.Drawing.Color.CornflowerBlue;
            btnGuardar.FlatAppearance.BorderSize = 2;
            btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnGuardar.Location = new System.Drawing.Point(620, 342);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new System.Drawing.Size(100, 30);
            btnGuardar.TabIndex = 9;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnDesactivar
            // 
            btnDesactivar.BackColor = System.Drawing.Color.CornflowerBlue;
            btnDesactivar.FlatAppearance.BorderSize = 2;
            btnDesactivar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnDesactivar.Location = new System.Drawing.Point(620, 378);
            btnDesactivar.Name = "btnDesactivar";
            btnDesactivar.Size = new System.Drawing.Size(100, 30);
            btnDesactivar.TabIndex = 10;
            btnDesactivar.Text = "Dar de baja";
            btnDesactivar.UseVisualStyleBackColor = false;
            btnDesactivar.Click += btnDesactivar_Click;
            // 
            // btnReactivar
            // 
            btnReactivar.BackColor = System.Drawing.Color.CornflowerBlue;
            btnReactivar.FlatAppearance.BorderSize = 2;
            btnReactivar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnReactivar.Location = new System.Drawing.Point(620, 414);
            btnReactivar.Name = "btnReactivar";
            btnReactivar.Size = new System.Drawing.Size(100, 30);
            btnReactivar.TabIndex = 11;
            btnReactivar.Text = "Reactivar";
            btnReactivar.UseVisualStyleBackColor = false;
            btnReactivar.Click += btnReactivar_Click;
            // 
            // lblMensaje
            // 
            lblMensaje.Location = new System.Drawing.Point(18, 414);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new System.Drawing.Size(580, 44);
            lblMensaje.TabIndex = 12;
            // 
            // FrmResidentes
            // 
            BackColor = System.Drawing.Color.LightSkyBlue;
            ClientSize = new System.Drawing.Size(744, 470);
            Controls.Add(lblFiltroEdificio);
            Controls.Add(cboFiltroEdificio);
            Controls.Add(dgvResidentes);
            Controls.Add(lblNombre);
            Controls.Add(txtNombre);
            Controls.Add(lblApellido);
            Controls.Add(txtApellido);
            Controls.Add(lblDni);
            Controls.Add(txtDni);
            Controls.Add(lblCorreo);
            Controls.Add(txtCorreo);
            Controls.Add(lblPiso);
            Controls.Add(txtPiso);
            Controls.Add(lblTelefono);
            Controls.Add(txtTelefono);
            Controls.Add(btnNuevo);
            Controls.Add(btnGuardar);
            Controls.Add(btnDesactivar);
            Controls.Add(btnReactivar);
            Controls.Add(lblMensaje);
            Font = new System.Drawing.Font("Segoe UI", 9F);
            Name = "FrmResidentes";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Gestión de residentes";
            Load += FrmResidentes_Load;
            ((System.ComponentModel.ISupportInitialize)dgvResidentes).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Label lblFiltroEdificio;
        private System.Windows.Forms.ComboBox cboFiltroEdificio;
        private System.Windows.Forms.DataGridView dgvResidentes;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblApellido;
        private System.Windows.Forms.TextBox txtApellido;
        private System.Windows.Forms.Label lblDni;
        private System.Windows.Forms.TextBox txtDni;
        private System.Windows.Forms.Label lblCorreo;
        private System.Windows.Forms.TextBox txtCorreo;
        private System.Windows.Forms.Label lblPiso;
        private System.Windows.Forms.TextBox txtPiso;
        private System.Windows.Forms.Label lblTelefono;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnDesactivar;
        private System.Windows.Forms.Button btnReactivar;
        private System.Windows.Forms.Label lblMensaje; 
    }
}