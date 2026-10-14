namespace LockersInteligentes.UI.Gestion
{
    partial class FrmEdificios
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
            dgvEdificios = new System.Windows.Forms.DataGridView();
            lblNombre = new System.Windows.Forms.Label();
            txtNombre = new System.Windows.Forms.TextBox();
            lblDireccion = new System.Windows.Forms.Label();
            txtDireccion = new System.Windows.Forms.TextBox();
            lblLocalidad = new System.Windows.Forms.Label();
            txtLocalidad = new System.Windows.Forms.TextBox();
            lblTelefono = new System.Windows.Forms.Label();
            txtTelefono = new System.Windows.Forms.TextBox();
            lblCantLockers = new System.Windows.Forms.Label();
            nudCantLockers = new System.Windows.Forms.NumericUpDown();
            btnNuevo = new System.Windows.Forms.Button();
            btnGuardar = new System.Windows.Forms.Button();
            btnEliminar = new System.Windows.Forms.Button();
            lblMensaje = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)dgvEdificios).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudCantLockers).BeginInit();
            SuspendLayout();
            // 
            // dgvEdificios
            // 
            dgvEdificios.AllowUserToAddRows = false;
            dgvEdificios.AllowUserToDeleteRows = false;
            dgvEdificios.BackgroundColor = System.Drawing.Color.MidnightBlue;
            dgvEdificios.ColumnHeadersHeight = 29;
            dgvEdificios.Location = new System.Drawing.Point(20, 20);
            dgvEdificios.MultiSelect = false;
            dgvEdificios.Name = "dgvEdificios";
            dgvEdificios.ReadOnly = true;
            dgvEdificios.RowHeadersVisible = false;
            dgvEdificios.RowHeadersWidth = 51;
            dgvEdificios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvEdificios.Size = new System.Drawing.Size(700, 220);
            dgvEdificios.TabIndex = 0;
            dgvEdificios.SelectionChanged += dgvEdificios_SelectionChanged;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Location = new System.Drawing.Point(20, 258);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new System.Drawing.Size(64, 20);
            lblNombre.TabIndex = 1;
            lblNombre.Text = "Nombre";
            // 
            // txtNombre
            // 
            txtNombre.Location = new System.Drawing.Point(20, 278);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new System.Drawing.Size(280, 27);
            txtNombre.TabIndex = 1;
            // 
            // lblDireccion
            // 
            lblDireccion.AutoSize = true;
            lblDireccion.Location = new System.Drawing.Point(320, 258);
            lblDireccion.Name = "lblDireccion";
            lblDireccion.Size = new System.Drawing.Size(72, 20);
            lblDireccion.TabIndex = 2;
            lblDireccion.Text = "Dirección";
            // 
            // txtDireccion
            // 
            txtDireccion.Location = new System.Drawing.Point(320, 278);
            txtDireccion.Name = "txtDireccion";
            txtDireccion.Size = new System.Drawing.Size(280, 27);
            txtDireccion.TabIndex = 2;
            // 
            // lblLocalidad
            // 
            lblLocalidad.AutoSize = true;
            lblLocalidad.Location = new System.Drawing.Point(20, 314);
            lblLocalidad.Name = "lblLocalidad";
            lblLocalidad.Size = new System.Drawing.Size(74, 20);
            lblLocalidad.TabIndex = 3;
            lblLocalidad.Text = "Localidad";
            // 
            // txtLocalidad
            // 
            txtLocalidad.Location = new System.Drawing.Point(20, 334);
            txtLocalidad.Name = "txtLocalidad";
            txtLocalidad.Size = new System.Drawing.Size(280, 27);
            txtLocalidad.TabIndex = 3;
            // 
            // lblTelefono
            // 
            lblTelefono.AutoSize = true;
            lblTelefono.Location = new System.Drawing.Point(320, 314);
            lblTelefono.Name = "lblTelefono";
            lblTelefono.Size = new System.Drawing.Size(150, 20);
            lblTelefono.TabIndex = 4;
            lblTelefono.Text = "Teléfono de contacto";
            // 
            // txtTelefono
            // 
            txtTelefono.Location = new System.Drawing.Point(320, 334);
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new System.Drawing.Size(180, 27);
            txtTelefono.TabIndex = 4;
            // 
            // lblCantLockers
            // 
            lblCantLockers.AutoSize = true;
            lblCantLockers.Location = new System.Drawing.Point(520, 314);
            lblCantLockers.Name = "lblCantLockers";
            lblCantLockers.Size = new System.Drawing.Size(92, 20);
            lblCantLockers.TabIndex = 5;
            lblCantLockers.Text = "Cant. lockers";
            // 
            // nudCantLockers
            // 
            nudCantLockers.Location = new System.Drawing.Point(520, 334);
            nudCantLockers.Maximum = new decimal(new int[] { 999, 0, 0, 0 });
            nudCantLockers.Name = "nudCantLockers";
            nudCantLockers.Size = new System.Drawing.Size(80, 27);
            nudCantLockers.TabIndex = 5;
            // 
            // btnNuevo
            // 
            btnNuevo.BackColor = System.Drawing.Color.CornflowerBlue;
            btnNuevo.FlatAppearance.BorderSize = 2;
            btnNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnNuevo.Location = new System.Drawing.Point(620, 274);
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
            btnGuardar.Location = new System.Drawing.Point(620, 310);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new System.Drawing.Size(100, 30);
            btnGuardar.TabIndex = 7;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.BackColor = System.Drawing.Color.CornflowerBlue;
            btnEliminar.FlatAppearance.BorderSize = 2;
            btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnEliminar.Location = new System.Drawing.Point(620, 346);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new System.Drawing.Size(100, 30);
            btnEliminar.TabIndex = 8;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = false;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // lblMensaje
            // 
            lblMensaje.Location = new System.Drawing.Point(18, 386);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new System.Drawing.Size(702, 24);
            lblMensaje.TabIndex = 9;
            // 
            // FrmEdificios
            // 
            BackColor = System.Drawing.Color.LightSkyBlue;
            ClientSize = new System.Drawing.Size(749, 426);
            Controls.Add(dgvEdificios);
            Controls.Add(lblNombre);
            Controls.Add(txtNombre);
            Controls.Add(lblDireccion);
            Controls.Add(txtDireccion);
            Controls.Add(lblLocalidad);
            Controls.Add(txtLocalidad);
            Controls.Add(lblTelefono);
            Controls.Add(txtTelefono);
            Controls.Add(lblCantLockers);
            Controls.Add(nudCantLockers);
            Controls.Add(btnNuevo);
            Controls.Add(btnGuardar);
            Controls.Add(btnEliminar);
            Controls.Add(lblMensaje);
            Font = new System.Drawing.Font("Segoe UI", 9F);
            Name = "FrmEdificios";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Gestión de edificios";
            Load += FrmEdificios_Load;
            ((System.ComponentModel.ISupportInitialize)dgvEdificios).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudCantLockers).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.DataGridView dgvEdificios;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblDireccion;
        private System.Windows.Forms.TextBox txtDireccion;
        private System.Windows.Forms.Label lblLocalidad;
        private System.Windows.Forms.TextBox txtLocalidad;
        private System.Windows.Forms.Label lblTelefono;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.Label lblCantLockers;
        private System.Windows.Forms.NumericUpDown nudCantLockers;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Label lblMensaje;
    }
}