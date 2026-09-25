namespace LockersInteligentes.UI
{
    partial class FrmEdificios
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
            lblCantidadReal = new System.Windows.Forms.Label();
            btnNuevo = new System.Windows.Forms.Button();
            btnGuardar = new System.Windows.Forms.Button();
            btnEliminar = new System.Windows.Forms.Button();
            grpGenerar = new System.Windows.Forms.GroupBox();
            lblCantidadGenerar = new System.Windows.Forms.Label();
            nudCantidadGenerar = new System.Windows.Forms.NumericUpDown();
            lblNumeroInicial = new System.Windows.Forms.Label();
            nudNumeroInicial = new System.Windows.Forms.NumericUpDown();
            lblSectorGenerar = new System.Windows.Forms.Label();
            txtSectorGenerar = new System.Windows.Forms.TextBox();
            lblTamanioGenerar = new System.Windows.Forms.Label();
            cboTamanioGenerar = new System.Windows.Forms.ComboBox();
            btnGenerar = new System.Windows.Forms.Button();
            lblMensaje = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)dgvEdificios).BeginInit();
            grpGenerar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudCantidadGenerar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudNumeroInicial).BeginInit();
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
            lblCantLockers.Size = new System.Drawing.Size(123, 20);
            lblCantLockers.TabIndex = 5;
            lblCantLockers.Text = "Lockers cargados";
            // 
            // lblCantidadReal
            // 
            lblCantidadReal.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            lblCantidadReal.ForeColor = System.Drawing.Color.DimGray;
            lblCantidadReal.Location = new System.Drawing.Point(520, 334);
            lblCantidadReal.Name = "lblCantidadReal";
            lblCantidadReal.Size = new System.Drawing.Size(90, 28);
            lblCantidadReal.TabIndex = 6;
            lblCantidadReal.Text = "0";
            // 
            // btnNuevo
            // 
            btnNuevo.BackColor = System.Drawing.Color.CornflowerBlue;
            btnNuevo.FlatAppearance.BorderSize = 2;
            btnNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnNuevo.Location = new System.Drawing.Point(659, 274);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new System.Drawing.Size(100, 30);
            btnNuevo.TabIndex = 5;
            btnNuevo.Text = "Nuevo";
            btnNuevo.UseVisualStyleBackColor = false;
            btnNuevo.Click += btnNuevo_Click;
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = System.Drawing.Color.CornflowerBlue;
            btnGuardar.FlatAppearance.BorderSize = 2;
            btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnGuardar.Location = new System.Drawing.Point(659, 310);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new System.Drawing.Size(100, 30);
            btnGuardar.TabIndex = 6;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.BackColor = System.Drawing.Color.CornflowerBlue;
            btnEliminar.FlatAppearance.BorderSize = 2;
            btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnEliminar.Location = new System.Drawing.Point(659, 346);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new System.Drawing.Size(100, 30);
            btnEliminar.TabIndex = 7;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = false;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // grpGenerar
            // 
            grpGenerar.BackColor = System.Drawing.Color.DodgerBlue;
            grpGenerar.Controls.Add(lblCantidadGenerar);
            grpGenerar.Controls.Add(nudCantidadGenerar);
            grpGenerar.Controls.Add(lblNumeroInicial);
            grpGenerar.Controls.Add(nudNumeroInicial);
            grpGenerar.Controls.Add(lblSectorGenerar);
            grpGenerar.Controls.Add(txtSectorGenerar);
            grpGenerar.Controls.Add(lblTamanioGenerar);
            grpGenerar.Controls.Add(cboTamanioGenerar);
            grpGenerar.Controls.Add(btnGenerar);
            grpGenerar.Location = new System.Drawing.Point(20, 386);
            grpGenerar.Name = "grpGenerar";
            grpGenerar.Size = new System.Drawing.Size(700, 96);
            grpGenerar.TabIndex = 8;
            grpGenerar.TabStop = false;
            grpGenerar.Text = "Generar lockers para el edificio seleccionado";
            // 
            // lblCantidadGenerar
            // 
            lblCantidadGenerar.AutoSize = true;
            lblCantidadGenerar.Location = new System.Drawing.Point(16, 28);
            lblCantidadGenerar.Name = "lblCantidadGenerar";
            lblCantidadGenerar.Size = new System.Drawing.Size(69, 20);
            lblCantidadGenerar.TabIndex = 0;
            lblCantidadGenerar.Text = "Cantidad";
            // 
            // nudCantidadGenerar
            // 
            nudCantidadGenerar.Location = new System.Drawing.Point(16, 48);
            nudCantidadGenerar.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
            nudCantidadGenerar.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudCantidadGenerar.Name = "nudCantidadGenerar";
            nudCantidadGenerar.Size = new System.Drawing.Size(80, 27);
            nudCantidadGenerar.TabIndex = 1;
            nudCantidadGenerar.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // lblNumeroInicial
            // 
            lblNumeroInicial.AutoSize = true;
            lblNumeroInicial.Location = new System.Drawing.Point(112, 28);
            lblNumeroInicial.Name = "lblNumeroInicial";
            lblNumeroInicial.Size = new System.Drawing.Size(88, 20);
            lblNumeroInicial.TabIndex = 2;
            lblNumeroInicial.Text = "Desde el N°";
            // 
            // nudNumeroInicial
            // 
            nudNumeroInicial.Location = new System.Drawing.Point(112, 48);
            nudNumeroInicial.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            nudNumeroInicial.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudNumeroInicial.Name = "nudNumeroInicial";
            nudNumeroInicial.Size = new System.Drawing.Size(90, 27);
            nudNumeroInicial.TabIndex = 3;
            nudNumeroInicial.Value = new decimal(new int[] { 101, 0, 0, 0 });
            // 
            // lblSectorGenerar
            // 
            lblSectorGenerar.AutoSize = true;
            lblSectorGenerar.Location = new System.Drawing.Point(218, 28);
            lblSectorGenerar.Name = "lblSectorGenerar";
            lblSectorGenerar.Size = new System.Drawing.Size(51, 20);
            lblSectorGenerar.TabIndex = 4;
            lblSectorGenerar.Text = "Sector";
            // 
            // txtSectorGenerar
            // 
            txtSectorGenerar.Location = new System.Drawing.Point(218, 48);
            txtSectorGenerar.Name = "txtSectorGenerar";
            txtSectorGenerar.Size = new System.Drawing.Size(120, 27);
            txtSectorGenerar.TabIndex = 5;
            txtSectorGenerar.Text = "A";
            // 
            // lblTamanioGenerar
            // 
            lblTamanioGenerar.AutoSize = true;
            lblTamanioGenerar.Location = new System.Drawing.Point(354, 28);
            lblTamanioGenerar.Name = "lblTamanioGenerar";
            lblTamanioGenerar.Size = new System.Drawing.Size(61, 20);
            lblTamanioGenerar.TabIndex = 6;
            lblTamanioGenerar.Text = "Tamaño";
            // 
            // cboTamanioGenerar
            // 
            cboTamanioGenerar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboTamanioGenerar.Location = new System.Drawing.Point(354, 48);
            cboTamanioGenerar.Name = "cboTamanioGenerar";
            cboTamanioGenerar.Size = new System.Drawing.Size(130, 28);
            cboTamanioGenerar.TabIndex = 7;
            // 
            // btnGenerar
            // 
            btnGenerar.BackColor = System.Drawing.Color.CornflowerBlue;
            btnGenerar.FlatAppearance.BorderSize = 2;
            btnGenerar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnGenerar.Location = new System.Drawing.Point(500, 46);
            btnGenerar.Name = "btnGenerar";
            btnGenerar.Size = new System.Drawing.Size(180, 30);
            btnGenerar.TabIndex = 8;
            btnGenerar.Text = "Generar lockers";
            btnGenerar.UseVisualStyleBackColor = false;
            btnGenerar.Click += btnGenerar_Click;
            // 
            // lblMensaje
            // 
            lblMensaje.Location = new System.Drawing.Point(18, 492);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new System.Drawing.Size(702, 40);
            lblMensaje.TabIndex = 9;
            // 
            // FrmEdificios
            // 
            BackColor = System.Drawing.Color.LightSkyBlue;
            ClientSize = new System.Drawing.Size(979, 546);
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
            Controls.Add(lblCantidadReal);
            Controls.Add(btnNuevo);
            Controls.Add(btnGuardar);
            Controls.Add(btnEliminar);
            Controls.Add(grpGenerar);
            Controls.Add(lblMensaje);
            Font = new System.Drawing.Font("Segoe UI", 9F);
            Name = "FrmEdificios";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Gestión de edificios";
            Load += FrmEdificios_Load;
            ((System.ComponentModel.ISupportInitialize)dgvEdificios).EndInit();
            grpGenerar.ResumeLayout(false);
            grpGenerar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudCantidadGenerar).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudNumeroInicial).EndInit();
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
        private System.Windows.Forms.Label lblCantidadReal;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.GroupBox grpGenerar;
        private System.Windows.Forms.Label lblCantidadGenerar;
        private System.Windows.Forms.NumericUpDown nudCantidadGenerar;
        private System.Windows.Forms.Label lblNumeroInicial;
        private System.Windows.Forms.NumericUpDown nudNumeroInicial;
        private System.Windows.Forms.Label lblSectorGenerar;
        private System.Windows.Forms.TextBox txtSectorGenerar;
        private System.Windows.Forms.Label lblTamanioGenerar;
        private System.Windows.Forms.ComboBox cboTamanioGenerar;
        private System.Windows.Forms.Button btnGenerar;
        private System.Windows.Forms.Label lblMensaje;
    }
}