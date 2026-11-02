namespace LockersInteligentes.UI.Informacion
{
    partial class FrmErrores
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
            lblRuta = new System.Windows.Forms.Label();
            dgvErrores = new System.Windows.Forms.DataGridView();
            lblDetalleTitulo = new System.Windows.Forms.Label();
            txtDetalle = new System.Windows.Forms.TextBox();
            btnActualizar = new System.Windows.Forms.Button();
            btnLimpiar = new System.Windows.Forms.Button();
            btnAbrirCarpeta = new System.Windows.Forms.Button();
            lblMensaje = new System.Windows.Forms.Label();
            btnBackup = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)dgvErrores).BeginInit();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            lblTitulo.Location = new System.Drawing.Point(20, 18);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new System.Drawing.Size(180, 32);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Log de errores";
            // 
            // lblRuta
            // 
            lblRuta.AutoSize = true;
            lblRuta.ForeColor = System.Drawing.Color.DimGray;
            lblRuta.Location = new System.Drawing.Point(22, 50);
            lblRuta.Name = "lblRuta";
            lblRuta.Size = new System.Drawing.Size(0, 20);
            lblRuta.TabIndex = 1;
            // 
            // dgvErrores
            // 
            dgvErrores.AllowUserToAddRows = false;
            dgvErrores.AllowUserToDeleteRows = false;
            dgvErrores.ColumnHeadersHeight = 29;
            dgvErrores.Location = new System.Drawing.Point(20, 78);
            dgvErrores.MultiSelect = false;
            dgvErrores.Name = "dgvErrores";
            dgvErrores.ReadOnly = true;
            dgvErrores.RowHeadersVisible = false;
            dgvErrores.RowHeadersWidth = 51;
            dgvErrores.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvErrores.Size = new System.Drawing.Size(860, 240);
            dgvErrores.TabIndex = 0;
            dgvErrores.SelectionChanged += dgvErrores_SelectionChanged;
            // 
            // lblDetalleTitulo
            // 
            lblDetalleTitulo.AutoSize = true;
            lblDetalleTitulo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblDetalleTitulo.Location = new System.Drawing.Point(20, 330);
            lblDetalleTitulo.Name = "lblDetalleTitulo";
            lblDetalleTitulo.Size = new System.Drawing.Size(112, 20);
            lblDetalleTitulo.TabIndex = 2;
            lblDetalleTitulo.Text = "Detalle técnico";
            // 
            // txtDetalle
            // 
            txtDetalle.Font = new System.Drawing.Font("Consolas", 9F);
            txtDetalle.Location = new System.Drawing.Point(20, 354);
            txtDetalle.Multiline = true;
            txtDetalle.Name = "txtDetalle";
            txtDetalle.ReadOnly = true;
            txtDetalle.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            txtDetalle.Size = new System.Drawing.Size(860, 200);
            txtDetalle.TabIndex = 1;
            // 
            // btnActualizar
            // 
            btnActualizar.BackColor = System.Drawing.Color.CornflowerBlue;
            btnActualizar.FlatAppearance.BorderSize = 2;
            btnActualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnActualizar.Location = new System.Drawing.Point(20, 568);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new System.Drawing.Size(120, 32);
            btnActualizar.TabIndex = 3;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = false;
            btnActualizar.Click += btnActualizar_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.BackColor = System.Drawing.Color.CornflowerBlue;
            btnLimpiar.FlatAppearance.BorderSize = 2;
            btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnLimpiar.Location = new System.Drawing.Point(760, 568);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new System.Drawing.Size(120, 32);
            btnLimpiar.TabIndex = 5;
            btnLimpiar.Text = "Vaciar log";
            btnLimpiar.UseVisualStyleBackColor = false;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // btnAbrirCarpeta
            // 
            btnAbrirCarpeta.BackColor = System.Drawing.Color.CornflowerBlue;
            btnAbrirCarpeta.FlatAppearance.BorderSize = 2;
            btnAbrirCarpeta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnAbrirCarpeta.Location = new System.Drawing.Point(152, 568);
            btnAbrirCarpeta.Name = "btnAbrirCarpeta";
            btnAbrirCarpeta.Size = new System.Drawing.Size(160, 32);
            btnAbrirCarpeta.TabIndex = 4;
            btnAbrirCarpeta.Text = "Abrir carpeta del log";
            btnAbrirCarpeta.UseVisualStyleBackColor = false;
            btnAbrirCarpeta.Click += btnAbrirCarpeta_Click;
            // 
            // lblMensaje
            // 
            lblMensaje.Location = new System.Drawing.Point(536, 574);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new System.Drawing.Size(210, 24);
            lblMensaje.TabIndex = 6;
            // 
            // btnBackup
            // 
            btnBackup.BackColor = System.Drawing.Color.CornflowerBlue;
            btnBackup.FlatAppearance.BorderSize = 2;
            btnBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnBackup.Location = new System.Drawing.Point(324, 568);
            btnBackup.Name = "btnBackup";
            btnBackup.Size = new System.Drawing.Size(200, 32);
            btnBackup.TabIndex = 7;
            btnBackup.Text = "Generar backup de la base";
            btnBackup.UseVisualStyleBackColor = false;
            btnBackup.Click += btnBackup_Click;
            // 
            // FrmErrores
            // 
            BackColor = System.Drawing.Color.LightSkyBlue;
            ClientSize = new System.Drawing.Size(904, 616);
            Controls.Add(lblTitulo);
            Controls.Add(lblRuta);
            Controls.Add(dgvErrores);
            Controls.Add(lblDetalleTitulo);
            Controls.Add(txtDetalle);
            Controls.Add(btnActualizar);
            Controls.Add(btnAbrirCarpeta);
            Controls.Add(btnLimpiar);
            Controls.Add(lblMensaje);
            Controls.Add(btnBackup);
            Font = new System.Drawing.Font("Segoe UI", 9F);
            Name = "FrmErrores";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Log de errores";
            Load += FrmErrores_Load;
            ((System.ComponentModel.ISupportInitialize)dgvErrores).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblRuta;
        private System.Windows.Forms.DataGridView dgvErrores;
        private System.Windows.Forms.Label lblDetalleTitulo;
        private System.Windows.Forms.TextBox txtDetalle;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Button btnAbrirCarpeta;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Label lblMensaje;
        private System.Windows.Forms.Button btnBackup;
    }
}