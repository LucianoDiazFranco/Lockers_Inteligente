namespace LockersInteligentes.UI.Gestion
{
    partial class FrmLockers
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
            dgvLockers = new System.Windows.Forms.DataGridView();
            lblNumero = new System.Windows.Forms.Label();
            nudNumero = new System.Windows.Forms.NumericUpDown();
            lblSector = new System.Windows.Forms.Label();
            txtSector = new System.Windows.Forms.TextBox();
            lblTamanio = new System.Windows.Forms.Label();
            cboTamanio = new System.Windows.Forms.ComboBox();
            lblDescripcion = new System.Windows.Forms.Label();
            txtDescripcion = new System.Windows.Forms.TextBox();
            lblEstado = new System.Windows.Forms.Label();
            cboEstado = new System.Windows.Forms.ComboBox();
            btnNuevo = new System.Windows.Forms.Button();
            btnGuardar = new System.Windows.Forms.Button();
            btnEliminar = new System.Windows.Forms.Button();
            lblMensaje = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)dgvLockers).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudNumero).BeginInit();
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
            cboFiltroEdificio.Location = new System.Drawing.Point(78, 14);
            cboFiltroEdificio.Name = "cboFiltroEdificio";
            cboFiltroEdificio.Size = new System.Drawing.Size(280, 28);
            cboFiltroEdificio.TabIndex = 0;
            cboFiltroEdificio.SelectedIndexChanged += cboFiltroEdificio_SelectedIndexChanged;
            // 
            // dgvLockers
            // 
            dgvLockers.AllowUserToAddRows = false;
            dgvLockers.AllowUserToDeleteRows = false;
            dgvLockers.BackgroundColor = System.Drawing.Color.MidnightBlue;
            dgvLockers.ColumnHeadersHeight = 29;
            dgvLockers.Location = new System.Drawing.Point(20, 52);
            dgvLockers.MultiSelect = false;
            dgvLockers.Name = "dgvLockers";
            dgvLockers.ReadOnly = true;
            dgvLockers.RowHeadersVisible = false;
            dgvLockers.RowHeadersWidth = 51;
            dgvLockers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvLockers.Size = new System.Drawing.Size(700, 230);
            dgvLockers.TabIndex = 1;
            dgvLockers.SelectionChanged += dgvLockers_SelectionChanged;
            // 
            // lblNumero
            // 
            lblNumero.AutoSize = true;
            lblNumero.Location = new System.Drawing.Point(20, 300);
            lblNumero.Name = "lblNumero";
            lblNumero.Size = new System.Drawing.Size(63, 20);
            lblNumero.TabIndex = 2;
            lblNumero.Text = "Número";
            // 
            // nudNumero
            // 
            nudNumero.Location = new System.Drawing.Point(20, 320);
            nudNumero.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            nudNumero.Name = "nudNumero";
            nudNumero.Size = new System.Drawing.Size(90, 27);
            nudNumero.TabIndex = 2;
            // 
            // lblSector
            // 
            lblSector.AutoSize = true;
            lblSector.Location = new System.Drawing.Point(126, 300);
            lblSector.Name = "lblSector";
            lblSector.Size = new System.Drawing.Size(51, 20);
            lblSector.TabIndex = 3;
            lblSector.Text = "Sector";
            // 
            // txtSector
            // 
            txtSector.Location = new System.Drawing.Point(126, 320);
            txtSector.Name = "txtSector";
            txtSector.Size = new System.Drawing.Size(120, 27);
            txtSector.TabIndex = 3;
            // 
            // lblTamanio
            // 
            lblTamanio.AutoSize = true;
            lblTamanio.Location = new System.Drawing.Point(262, 300);
            lblTamanio.Name = "lblTamanio";
            lblTamanio.Size = new System.Drawing.Size(61, 20);
            lblTamanio.TabIndex = 4;
            lblTamanio.Text = "Tamaño";
            // 
            // cboTamanio
            // 
            cboTamanio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboTamanio.Location = new System.Drawing.Point(262, 320);
            cboTamanio.Name = "cboTamanio";
            cboTamanio.Size = new System.Drawing.Size(130, 28);
            cboTamanio.TabIndex = 4;
            // 
            // lblDescripcion
            // 
            lblDescripcion.AutoSize = true;
            lblDescripcion.Location = new System.Drawing.Point(20, 356);
            lblDescripcion.Name = "lblDescripcion";
            lblDescripcion.Size = new System.Drawing.Size(87, 20);
            lblDescripcion.TabIndex = 6;
            lblDescripcion.Text = "Descripción";
            // 
            // txtDescripcion
            // 
            txtDescripcion.Location = new System.Drawing.Point(20, 376);
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new System.Drawing.Size(548, 27);
            txtDescripcion.TabIndex = 6;
            // 
            // lblEstado
            // 
            lblEstado.AutoSize = true;
            lblEstado.Location = new System.Drawing.Point(408, 300);
            lblEstado.Name = "lblEstado";
            lblEstado.Size = new System.Drawing.Size(54, 20);
            lblEstado.TabIndex = 5;
            lblEstado.Text = "Estado";
            // 
            // cboEstado
            // 
            cboEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboEstado.Location = new System.Drawing.Point(408, 320);
            cboEstado.Name = "cboEstado";
            cboEstado.Size = new System.Drawing.Size(160, 28);
            cboEstado.TabIndex = 5;
            // 
            // btnNuevo
            // 
            btnNuevo.BackColor = System.Drawing.Color.CornflowerBlue;
            btnNuevo.FlatAppearance.BorderSize = 2;
            btnNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnNuevo.Location = new System.Drawing.Point(620, 316);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new System.Drawing.Size(100, 30);
            btnNuevo.TabIndex = 7;
            btnNuevo.Text = "Nuevo";
            btnNuevo.UseVisualStyleBackColor = false;
            btnNuevo.Click += btnNuevo_Click;
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = System.Drawing.Color.CornflowerBlue;
            btnGuardar.FlatAppearance.BorderSize = 2;
            btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnGuardar.Location = new System.Drawing.Point(620, 352);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new System.Drawing.Size(100, 30);
            btnGuardar.TabIndex = 8;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.BackColor = System.Drawing.Color.CornflowerBlue;
            btnEliminar.FlatAppearance.BorderSize = 2;
            btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnEliminar.Location = new System.Drawing.Point(620, 388);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new System.Drawing.Size(100, 30);
            btnEliminar.TabIndex = 9;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = false;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // lblMensaje
            // 
            lblMensaje.Location = new System.Drawing.Point(18, 416);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new System.Drawing.Size(580, 40);
            lblMensaje.TabIndex = 10;
            // 
            // FrmLockers
            // 
            BackColor = System.Drawing.Color.LightSkyBlue;
            ClientSize = new System.Drawing.Size(744, 464);
            Controls.Add(lblFiltroEdificio);
            Controls.Add(cboFiltroEdificio);
            Controls.Add(dgvLockers);
            Controls.Add(lblNumero);
            Controls.Add(nudNumero);
            Controls.Add(lblSector);
            Controls.Add(txtSector);
            Controls.Add(lblTamanio);
            Controls.Add(cboTamanio);
            Controls.Add(lblEstado);
            Controls.Add(cboEstado);
            Controls.Add(lblDescripcion);
            Controls.Add(txtDescripcion);
            Controls.Add(btnNuevo);
            Controls.Add(btnGuardar);
            Controls.Add(btnEliminar);
            Controls.Add(lblMensaje);
            Font = new System.Drawing.Font("Segoe UI", 9F);
            Name = "FrmLockers";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Gestión de lockers";
            Load += FrmLockers_Load;
            ((System.ComponentModel.ISupportInitialize)dgvLockers).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudNumero).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Label lblFiltroEdificio;
        private System.Windows.Forms.ComboBox cboFiltroEdificio;
        private System.Windows.Forms.DataGridView dgvLockers;
        private System.Windows.Forms.Label lblNumero;
        private System.Windows.Forms.NumericUpDown nudNumero;
        private System.Windows.Forms.Label lblSector;
        private System.Windows.Forms.TextBox txtSector;
        private System.Windows.Forms.Label lblTamanio;
        private System.Windows.Forms.ComboBox cboTamanio;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.ComboBox cboEstado;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Label lblMensaje; 
    }
}