namespace LockersInteligentes.UI.Informacion
{
    partial class FrmReportes
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
            grpGenerar = new System.Windows.Forms.GroupBox();
            lblEdificio = new System.Windows.Forms.Label();
            cboEdificio = new System.Windows.Forms.ComboBox();
            lblDesde = new System.Windows.Forms.Label();
            dtpDesde = new System.Windows.Forms.DateTimePicker();
            lblHasta = new System.Windows.Forms.Label();
            dtpHasta = new System.Windows.Forms.DateTimePicker();
            btnGenerar = new System.Windows.Forms.Button();
            grpResultado = new System.Windows.Forms.GroupBox();
            lblEncabezado = new System.Windows.Forms.Label();
            lblOcupacionTitulo = new System.Windows.Forms.Label();
            lblOcupacion = new System.Windows.Forms.Label();
            lblPertenenciaTitulo = new System.Windows.Forms.Label();
            lblPertenencia = new System.Windows.Forms.Label();
            lblVencidosTitulo = new System.Windows.Forms.Label();
            lblVencidos = new System.Windows.Forms.Label();
            lblRankRepTitulo = new System.Windows.Forms.Label();
            lblRankRepartidores = new System.Windows.Forms.Label();
            lblRankLockTitulo = new System.Windows.Forms.Label();
            lblRankLockers = new System.Windows.Forms.Label();
            lblHistorialTitulo = new System.Windows.Forms.Label();
            dgvReportes = new System.Windows.Forms.DataGridView();
            lblMensaje = new System.Windows.Forms.Label();
            grpGenerar.SuspendLayout();
            grpResultado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvReportes).BeginInit();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            lblTitulo.Location = new System.Drawing.Point(20, 18);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new System.Drawing.Size(274, 32);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Reportes y estadísticas";
            // 
            // grpGenerar
            // 
            grpGenerar.Controls.Add(lblEdificio);
            grpGenerar.Controls.Add(cboEdificio);
            grpGenerar.Controls.Add(lblDesde);
            grpGenerar.Controls.Add(dtpDesde);
            grpGenerar.Controls.Add(lblHasta);
            grpGenerar.Controls.Add(dtpHasta);
            grpGenerar.Controls.Add(btnGenerar);
            grpGenerar.Location = new System.Drawing.Point(20, 56);
            grpGenerar.Name = "grpGenerar";
            grpGenerar.Size = new System.Drawing.Size(880, 100);
            grpGenerar.TabIndex = 1;
            grpGenerar.TabStop = false;
            grpGenerar.Text = "Generar reporte";
            // 
            // lblEdificio
            // 
            lblEdificio.AutoSize = true;
            lblEdificio.Location = new System.Drawing.Point(16, 30);
            lblEdificio.Name = "lblEdificio";
            lblEdificio.Size = new System.Drawing.Size(59, 20);
            lblEdificio.TabIndex = 0;
            lblEdificio.Text = "Edificio";
            // 
            // cboEdificio
            // 
            cboEdificio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboEdificio.Location = new System.Drawing.Point(16, 54);
            cboEdificio.Name = "cboEdificio";
            cboEdificio.Size = new System.Drawing.Size(280, 28);
            cboEdificio.TabIndex = 1;
            // 
            // lblDesde
            // 
            lblDesde.AutoSize = true;
            lblDesde.Location = new System.Drawing.Point(316, 30);
            lblDesde.Name = "lblDesde";
            lblDesde.Size = new System.Drawing.Size(51, 20);
            lblDesde.TabIndex = 2;
            lblDesde.Text = "Desde";
            // 
            // dtpDesde
            // 
            dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            dtpDesde.Location = new System.Drawing.Point(316, 54);
            dtpDesde.Name = "dtpDesde";
            dtpDesde.Size = new System.Drawing.Size(150, 27);
            dtpDesde.TabIndex = 3;
            // 
            // lblHasta
            // 
            lblHasta.AutoSize = true;
            lblHasta.Location = new System.Drawing.Point(486, 30);
            lblHasta.Name = "lblHasta";
            lblHasta.Size = new System.Drawing.Size(47, 20);
            lblHasta.TabIndex = 4;
            lblHasta.Text = "Hasta";
            // 
            // dtpHasta
            // 
            dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            dtpHasta.Location = new System.Drawing.Point(486, 54);
            dtpHasta.Name = "dtpHasta";
            dtpHasta.Size = new System.Drawing.Size(150, 27);
            dtpHasta.TabIndex = 5;
            // 
            // btnGenerar
            // 
            btnGenerar.BackColor = System.Drawing.Color.CornflowerBlue;
            btnGenerar.FlatAppearance.BorderSize = 2;
            btnGenerar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnGenerar.Location = new System.Drawing.Point(660, 50);
            btnGenerar.Name = "btnGenerar";
            btnGenerar.Size = new System.Drawing.Size(180, 34);
            btnGenerar.TabIndex = 6;
            btnGenerar.Text = "Generar reporte";
            btnGenerar.UseVisualStyleBackColor = false;
            btnGenerar.Click += btnGenerar_Click;
            // 
            // grpResultado
            // 
            grpResultado.BackColor = System.Drawing.Color.SteelBlue;
            grpResultado.Controls.Add(lblEncabezado);
            grpResultado.Controls.Add(lblOcupacionTitulo);
            grpResultado.Controls.Add(lblOcupacion);
            grpResultado.Controls.Add(lblPertenenciaTitulo);
            grpResultado.Controls.Add(lblPertenencia);
            grpResultado.Controls.Add(lblVencidosTitulo);
            grpResultado.Controls.Add(lblVencidos);
            grpResultado.Controls.Add(lblRankRepTitulo);
            grpResultado.Controls.Add(lblRankRepartidores);
            grpResultado.Controls.Add(lblRankLockTitulo);
            grpResultado.Controls.Add(lblRankLockers);
            grpResultado.Location = new System.Drawing.Point(20, 166);
            grpResultado.Name = "grpResultado";
            grpResultado.Size = new System.Drawing.Size(880, 290);
            grpResultado.TabIndex = 2;
            grpResultado.TabStop = false;
            grpResultado.Text = "Resultado";
            grpResultado.Visible = false;
            // 
            // lblEncabezado
            // 
            lblEncabezado.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            lblEncabezado.Location = new System.Drawing.Point(16, 26);
            lblEncabezado.Name = "lblEncabezado";
            lblEncabezado.Size = new System.Drawing.Size(850, 24);
            lblEncabezado.TabIndex = 0;
            // 
            // lblOcupacionTitulo
            // 
            lblOcupacionTitulo.AutoSize = true;
            lblOcupacionTitulo.ForeColor = System.Drawing.Color.White;
            lblOcupacionTitulo.Location = new System.Drawing.Point(16, 60);
            lblOcupacionTitulo.Name = "lblOcupacionTitulo";
            lblOcupacionTitulo.Size = new System.Drawing.Size(124, 20);
            lblOcupacionTitulo.TabIndex = 1;
            lblOcupacionTitulo.Text = "Ocupación actual";
            // 
            // lblOcupacion
            // 
            lblOcupacion.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            lblOcupacion.ForeColor = System.Drawing.Color.White;
            lblOcupacion.Location = new System.Drawing.Point(14, 80);
            lblOcupacion.Name = "lblOcupacion";
            lblOcupacion.Size = new System.Drawing.Size(160, 42);
            lblOcupacion.TabIndex = 2;
            // 
            // lblPertenenciaTitulo
            // 
            lblPertenenciaTitulo.AutoSize = true;
            lblPertenenciaTitulo.ForeColor = System.Drawing.Color.White;
            lblPertenenciaTitulo.Location = new System.Drawing.Point(196, 60);
            lblPertenenciaTitulo.Name = "lblPertenenciaTitulo";
            lblPertenenciaTitulo.Size = new System.Drawing.Size(201, 20);
            lblPertenenciaTitulo.TabIndex = 3;
            lblPertenenciaTitulo.Text = "Lockers usados en el período";
            // 
            // lblPertenencia
            // 
            lblPertenencia.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            lblPertenencia.ForeColor = System.Drawing.Color.LawnGreen;
            lblPertenencia.Location = new System.Drawing.Point(194, 80);
            lblPertenencia.Name = "lblPertenencia";
            lblPertenencia.Size = new System.Drawing.Size(180, 42);
            lblPertenencia.TabIndex = 4;
            // 
            // lblVencidosTitulo
            // 
            lblVencidosTitulo.AutoSize = true;
            lblVencidosTitulo.ForeColor = System.Drawing.Color.White;
            lblVencidosTitulo.Location = new System.Drawing.Point(416, 60);
            lblVencidosTitulo.Name = "lblVencidosTitulo";
            lblVencidosTitulo.Size = new System.Drawing.Size(130, 20);
            lblVencidosTitulo.TabIndex = 5;
            lblVencidosTitulo.Text = "Paquetes vencidos";
            // 
            // lblVencidos
            // 
            lblVencidos.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            lblVencidos.ForeColor = System.Drawing.Color.Red;
            lblVencidos.Location = new System.Drawing.Point(414, 80);
            lblVencidos.Name = "lblVencidos";
            lblVencidos.Size = new System.Drawing.Size(140, 42);
            lblVencidos.TabIndex = 6;
            // 
            // lblRankRepTitulo
            // 
            lblRankRepTitulo.AutoSize = true;
            lblRankRepTitulo.BackColor = System.Drawing.Color.LightBlue;
            lblRankRepTitulo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblRankRepTitulo.Location = new System.Drawing.Point(0, 142);
            lblRankRepTitulo.Name = "lblRankRepTitulo";
            lblRankRepTitulo.Size = new System.Drawing.Size(125, 20);
            lblRankRepTitulo.TabIndex = 7;
            lblRankRepTitulo.Text = "Top repartidores";
            lblRankRepTitulo.Click += lblRankRepTitulo_Click;
            // 
            // lblRankRepartidores
            // 
            lblRankRepartidores.BackColor = System.Drawing.Color.LightBlue;
            lblRankRepartidores.Location = new System.Drawing.Point(0, 162);
            lblRankRepartidores.Name = "lblRankRepartidores";
            lblRankRepartidores.Size = new System.Drawing.Size(444, 128);
            lblRankRepartidores.TabIndex = 8;
            // 
            // lblRankLockTitulo
            // 
            lblRankLockTitulo.AutoSize = true;
            lblRankLockTitulo.BackColor = System.Drawing.Color.LightBlue;
            lblRankLockTitulo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblRankLockTitulo.Location = new System.Drawing.Point(444, 142);
            lblRankLockTitulo.Name = "lblRankLockTitulo";
            lblRankLockTitulo.Size = new System.Drawing.Size(148, 20);
            lblRankLockTitulo.TabIndex = 9;
            lblRankLockTitulo.Text = "Lockers más usados";
            // 
            // lblRankLockers
            // 
            lblRankLockers.BackColor = System.Drawing.Color.LightBlue;
            lblRankLockers.Location = new System.Drawing.Point(444, 162);
            lblRankLockers.Name = "lblRankLockers";
            lblRankLockers.Size = new System.Drawing.Size(436, 128);
            lblRankLockers.TabIndex = 10;
            // 
            // lblHistorialTitulo
            // 
            lblHistorialTitulo.AutoSize = true;
            lblHistorialTitulo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblHistorialTitulo.Location = new System.Drawing.Point(20, 468);
            lblHistorialTitulo.Name = "lblHistorialTitulo";
            lblHistorialTitulo.Size = new System.Drawing.Size(149, 20);
            lblHistorialTitulo.TabIndex = 3;
            lblHistorialTitulo.Text = "Reportes generados";
            // 
            // dgvReportes
            // 
            dgvReportes.AllowUserToAddRows = false;
            dgvReportes.AllowUserToDeleteRows = false;
            dgvReportes.BackgroundColor = System.Drawing.Color.SteelBlue;
            dgvReportes.ColumnHeadersHeight = 29;
            dgvReportes.Location = new System.Drawing.Point(20, 492);
            dgvReportes.MultiSelect = false;
            dgvReportes.Name = "dgvReportes";
            dgvReportes.ReadOnly = true;
            dgvReportes.RowHeadersVisible = false;
            dgvReportes.RowHeadersWidth = 51;
            dgvReportes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvReportes.Size = new System.Drawing.Size(880, 160);
            dgvReportes.TabIndex = 2;
            dgvReportes.SelectionChanged += dgvReportes_SelectionChanged;
            // 
            // lblMensaje
            // 
            lblMensaje.Location = new System.Drawing.Point(18, 658);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new System.Drawing.Size(882, 41);
            lblMensaje.TabIndex = 4;
            // 
            // FrmReportes
            // 
            BackColor = System.Drawing.Color.LightSkyBlue;
            ClientSize = new System.Drawing.Size(924, 729);
            Controls.Add(lblTitulo);
            Controls.Add(grpGenerar);
            Controls.Add(grpResultado);
            Controls.Add(lblHistorialTitulo);
            Controls.Add(dgvReportes);
            Controls.Add(lblMensaje);
            Font = new System.Drawing.Font("Segoe UI", 9F);
            Name = "FrmReportes";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Reportes y estadísticas";
            Load += FrmReportes_Load;
            grpGenerar.ResumeLayout(false);
            grpGenerar.PerformLayout();
            grpResultado.ResumeLayout(false);
            grpResultado.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvReportes).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.GroupBox grpGenerar;
        private System.Windows.Forms.Label lblEdificio;
        private System.Windows.Forms.ComboBox cboEdificio;
        private System.Windows.Forms.Label lblDesde;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        private System.Windows.Forms.Label lblHasta;
        private System.Windows.Forms.DateTimePicker dtpHasta;
        private System.Windows.Forms.Button btnGenerar;
        private System.Windows.Forms.GroupBox grpResultado;
        private System.Windows.Forms.Label lblEncabezado;
        private System.Windows.Forms.Label lblOcupacionTitulo;
        private System.Windows.Forms.Label lblOcupacion;
        private System.Windows.Forms.Label lblPertenenciaTitulo;
        private System.Windows.Forms.Label lblPertenencia;
        private System.Windows.Forms.Label lblVencidosTitulo;
        private System.Windows.Forms.Label lblVencidos;
        private System.Windows.Forms.Label lblRankRepTitulo;
        private System.Windows.Forms.Label lblRankRepartidores;
        private System.Windows.Forms.Label lblRankLockTitulo;
        private System.Windows.Forms.Label lblRankLockers;
        private System.Windows.Forms.Label lblHistorialTitulo;
        private System.Windows.Forms.DataGridView dgvReportes;
        private System.Windows.Forms.Label lblMensaje;


    }
}