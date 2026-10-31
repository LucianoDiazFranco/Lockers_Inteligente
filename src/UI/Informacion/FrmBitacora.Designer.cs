namespace LockersInteligentes.UI.Informacion
{
    partial class FrmBitacora
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
            grpFiltros = new System.Windows.Forms.GroupBox();
            chkDesde = new System.Windows.Forms.CheckBox();
            dtpDesde = new System.Windows.Forms.DateTimePicker();
            chkHasta = new System.Windows.Forms.CheckBox();
            dtpHasta = new System.Windows.Forms.DateTimePicker();
            lblActor = new System.Windows.Forms.Label();
            txtActor = new System.Windows.Forms.TextBox();
            lblTipo = new System.Windows.Forms.Label();
            cboTipo = new System.Windows.Forms.ComboBox();
            btnBuscar = new System.Windows.Forms.Button();
            btnLimpiar = new System.Windows.Forms.Button();
            dgvHistorial = new System.Windows.Forms.DataGridView();
            lblResumen = new System.Windows.Forms.Label();
            grpFiltros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHistorial).BeginInit();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            lblTitulo.Location = new System.Drawing.Point(20, 18);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new System.Drawing.Size(289, 32);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Bitácora de operaciones";
            // 
            // grpFiltros
            // 
            grpFiltros.Controls.Add(chkDesde);
            grpFiltros.Controls.Add(dtpDesde);
            grpFiltros.Controls.Add(chkHasta);
            grpFiltros.Controls.Add(dtpHasta);
            grpFiltros.Controls.Add(lblActor);
            grpFiltros.Controls.Add(txtActor);
            grpFiltros.Controls.Add(lblTipo);
            grpFiltros.Controls.Add(cboTipo);
            grpFiltros.Controls.Add(btnBuscar);
            grpFiltros.Controls.Add(btnLimpiar);
            grpFiltros.Location = new System.Drawing.Point(20, 56);
            grpFiltros.Name = "grpFiltros";
            grpFiltros.Size = new System.Drawing.Size(860, 110);
            grpFiltros.TabIndex = 1;
            grpFiltros.TabStop = false;
            grpFiltros.Text = "Filtros";
            // 
            // chkDesde
            // 
            chkDesde.AutoSize = true;
            chkDesde.Location = new System.Drawing.Point(16, 32);
            chkDesde.Name = "chkDesde";
            chkDesde.Size = new System.Drawing.Size(73, 24);
            chkDesde.TabIndex = 0;
            chkDesde.Text = "Desde";
            chkDesde.UseVisualStyleBackColor = true;
            chkDesde.CheckedChanged += chkDesde_CheckedChanged;
            // 
            // dtpDesde
            // 
            dtpDesde.Enabled = false;
            dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            dtpDesde.Location = new System.Drawing.Point(16, 58);
            dtpDesde.Name = "dtpDesde";
            dtpDesde.Size = new System.Drawing.Size(140, 27);
            dtpDesde.TabIndex = 1;
            // 
            // chkHasta
            // 
            chkHasta.AutoSize = true;
            chkHasta.Location = new System.Drawing.Point(176, 32);
            chkHasta.Name = "chkHasta";
            chkHasta.Size = new System.Drawing.Size(69, 24);
            chkHasta.TabIndex = 2;
            chkHasta.Text = "Hasta";
            chkHasta.UseVisualStyleBackColor = true;
            chkHasta.CheckedChanged += chkHasta_CheckedChanged;
            // 
            // dtpHasta
            // 
            dtpHasta.Enabled = false;
            dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            dtpHasta.Location = new System.Drawing.Point(176, 58);
            dtpHasta.Name = "dtpHasta";
            dtpHasta.Size = new System.Drawing.Size(140, 27);
            dtpHasta.TabIndex = 3;
            // 
            // lblActor
            // 
            lblActor.AutoSize = true;
            lblActor.Location = new System.Drawing.Point(336, 34);
            lblActor.Name = "lblActor";
            lblActor.Size = new System.Drawing.Size(59, 20);
            lblActor.TabIndex = 4;
            lblActor.Text = "Usuario";
            // 
            // txtActor
            // 
            txtActor.Location = new System.Drawing.Point(336, 58);
            txtActor.Name = "txtActor";
            txtActor.Size = new System.Drawing.Size(180, 27);
            txtActor.TabIndex = 5;
            // 
            // lblTipo
            // 
            lblTipo.AutoSize = true;
            lblTipo.Location = new System.Drawing.Point(536, 34);
            lblTipo.Name = "lblTipo";
            lblTipo.Size = new System.Drawing.Size(78, 20);
            lblTipo.TabIndex = 6;
            lblTipo.Text = "Operación";
            // 
            // cboTipo
            // 
            cboTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboTipo.Location = new System.Drawing.Point(536, 58);
            cboTipo.Name = "cboTipo";
            cboTipo.Size = new System.Drawing.Size(160, 28);
            cboTipo.TabIndex = 7;
            // 
            // btnBuscar
            // 
            btnBuscar.BackColor = System.Drawing.Color.CornflowerBlue;
            btnBuscar.FlatAppearance.BorderSize = 2;
            btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnBuscar.Location = new System.Drawing.Point(716, 56);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new System.Drawing.Size(60, 30);
            btnBuscar.TabIndex = 8;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = false;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.BackColor = System.Drawing.Color.CornflowerBlue;
            btnLimpiar.FlatAppearance.BorderSize = 2;
            btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnLimpiar.Location = new System.Drawing.Point(784, 56);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new System.Drawing.Size(70, 30);
            btnLimpiar.TabIndex = 9;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = false;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // dgvHistorial
            // 
            dgvHistorial.AllowUserToAddRows = false;
            dgvHistorial.AllowUserToDeleteRows = false;
            dgvHistorial.BackgroundColor = System.Drawing.Color.CornflowerBlue;
            dgvHistorial.ColumnHeadersHeight = 29;
            dgvHistorial.Location = new System.Drawing.Point(20, 180);
            dgvHistorial.MultiSelect = false;
            dgvHistorial.Name = "dgvHistorial";
            dgvHistorial.ReadOnly = true;
            dgvHistorial.RowHeadersVisible = false;
            dgvHistorial.RowHeadersWidth = 51;
            dgvHistorial.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvHistorial.Size = new System.Drawing.Size(860, 360);
            dgvHistorial.TabIndex = 1;
            // 
            // lblResumen
            // 
            lblResumen.ForeColor = System.Drawing.Color.DimGray;
            lblResumen.Location = new System.Drawing.Point(20, 548);
            lblResumen.Name = "lblResumen";
            lblResumen.Size = new System.Drawing.Size(860, 24);
            lblResumen.TabIndex = 2;
            // 
            // FrmBitacora
            // 
            BackColor = System.Drawing.Color.LightSkyBlue;
            ClientSize = new System.Drawing.Size(904, 584);
            Controls.Add(lblTitulo);
            Controls.Add(grpFiltros);
            Controls.Add(dgvHistorial);
            Controls.Add(lblResumen);
            Font = new System.Drawing.Font("Segoe UI", 9F);
            Name = "FrmBitacora";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Bitácora";
            Load += FrmBitacora_Load;
            grpFiltros.ResumeLayout(false);
            grpFiltros.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHistorial).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.GroupBox grpFiltros;
        private System.Windows.Forms.CheckBox chkDesde;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        private System.Windows.Forms.CheckBox chkHasta;
        private System.Windows.Forms.DateTimePicker dtpHasta;
        private System.Windows.Forms.Label lblActor;
        private System.Windows.Forms.TextBox txtActor;
        private System.Windows.Forms.Label lblTipo;
        private System.Windows.Forms.ComboBox cboTipo;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.DataGridView dgvHistorial;
        private System.Windows.Forms.Label lblResumen;

    }
}