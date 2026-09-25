namespace LockersInteligentes.UI.Operacion
{
    partial class FrmEstadoLockers
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
            panelSuperior = new System.Windows.Forms.Panel();
            lblEdificio = new System.Windows.Forms.Label();
            cboEdificio = new System.Windows.Forms.ComboBox();
            lblSector = new System.Windows.Forms.Label();
            cboSector = new System.Windows.Forms.ComboBox();
            btnActualizar = new System.Windows.Forms.Button();
            lblResumen = new System.Windows.Forms.Label();
            panelLockers = new System.Windows.Forms.FlowLayoutPanel();
            panelInferior = new System.Windows.Forms.Panel();
            lblLeyenda = new System.Windows.Forms.Label();
            lblDetalle = new System.Windows.Forms.Label();
            panelSuperior.SuspendLayout();
            panelInferior.SuspendLayout();
            SuspendLayout();
            // 
            // panelSuperior
            // 
            panelSuperior.BackColor = System.Drawing.Color.LightSkyBlue;
            panelSuperior.Controls.Add(lblEdificio);
            panelSuperior.Controls.Add(cboEdificio);
            panelSuperior.Controls.Add(lblSector);
            panelSuperior.Controls.Add(cboSector);
            panelSuperior.Controls.Add(btnActualizar);
            panelSuperior.Controls.Add(lblResumen);
            panelSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            panelSuperior.Location = new System.Drawing.Point(0, 0);
            panelSuperior.Name = "panelSuperior";
            panelSuperior.Size = new System.Drawing.Size(1291, 78);
            panelSuperior.TabIndex = 0;
            // 
            // lblEdificio
            // 
            lblEdificio.AutoSize = true;
            lblEdificio.Location = new System.Drawing.Point(14, 16);
            lblEdificio.Name = "lblEdificio";
            lblEdificio.Size = new System.Drawing.Size(59, 20);
            lblEdificio.TabIndex = 0;
            lblEdificio.Text = "Edificio";
            // 
            // cboEdificio
            // 
            cboEdificio.BackColor = System.Drawing.Color.AliceBlue;
            cboEdificio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboEdificio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            cboEdificio.Location = new System.Drawing.Point(72, 12);
            cboEdificio.Name = "cboEdificio";
            cboEdificio.Size = new System.Drawing.Size(260, 28);
            cboEdificio.TabIndex = 0;
            cboEdificio.SelectedIndexChanged += cboEdificio_SelectedIndexChanged;
            // 
            // lblSector
            // 
            lblSector.AutoSize = true;
            lblSector.Location = new System.Drawing.Point(350, 16);
            lblSector.Name = "lblSector";
            lblSector.Size = new System.Drawing.Size(51, 20);
            lblSector.TabIndex = 1;
            lblSector.Text = "Sector";
            // 
            // cboSector
            // 
            cboSector.BackColor = System.Drawing.Color.AliceBlue;
            cboSector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboSector.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            cboSector.Location = new System.Drawing.Point(402, 12);
            cboSector.Name = "cboSector";
            cboSector.Size = new System.Drawing.Size(180, 28);
            cboSector.TabIndex = 1;
            cboSector.SelectedIndexChanged += cboSector_SelectedIndexChanged;
            // 
            // btnActualizar
            // 
            btnActualizar.BackColor = System.Drawing.Color.CornflowerBlue;
            btnActualizar.FlatAppearance.BorderSize = 2;
            btnActualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnActualizar.Location = new System.Drawing.Point(600, 11);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new System.Drawing.Size(110, 28);
            btnActualizar.TabIndex = 2;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = false;
            btnActualizar.Click += btnActualizar_Click;
            // 
            // lblResumen
            // 
            lblResumen.AutoSize = true;
            lblResumen.ForeColor = System.Drawing.Color.DimGray;
            lblResumen.Location = new System.Drawing.Point(14, 50);
            lblResumen.Name = "lblResumen";
            lblResumen.Size = new System.Drawing.Size(0, 20);
            lblResumen.TabIndex = 3;
            // 
            // panelLockers
            // 
            panelLockers.AutoScroll = true;
            panelLockers.BackColor = System.Drawing.Color.MidnightBlue;
            panelLockers.Dock = System.Windows.Forms.DockStyle.Fill;
            panelLockers.Location = new System.Drawing.Point(0, 78);
            panelLockers.Name = "panelLockers";
            panelLockers.Padding = new System.Windows.Forms.Padding(14);
            panelLockers.Size = new System.Drawing.Size(1291, 489);
            panelLockers.TabIndex = 1;
            // 
            // panelInferior
            // 
            panelInferior.BackColor = System.Drawing.Color.LightSkyBlue;
            panelInferior.Controls.Add(lblLeyenda);
            panelInferior.Controls.Add(lblDetalle);
            panelInferior.Dock = System.Windows.Forms.DockStyle.Bottom;
            panelInferior.Location = new System.Drawing.Point(0, 567);
            panelInferior.Name = "panelInferior";
            panelInferior.Size = new System.Drawing.Size(1291, 76);
            panelInferior.TabIndex = 2;
            // 
            // lblLeyenda
            // 
            lblLeyenda.AutoSize = true;
            lblLeyenda.Location = new System.Drawing.Point(14, 10);
            lblLeyenda.Name = "lblLeyenda";
            lblLeyenda.Size = new System.Drawing.Size(0, 20);
            lblLeyenda.TabIndex = 0;
            // 
            // lblDetalle
            // 
            lblDetalle.Location = new System.Drawing.Point(14, 32);
            lblDetalle.Name = "lblDetalle";
            lblDetalle.Size = new System.Drawing.Size(880, 38);
            lblDetalle.TabIndex = 1;
            lblDetalle.Text = "Hacé clic sobre un locker para ver el detalle.";
            // 
            // FrmEstadoLockers
            // 
            ClientSize = new System.Drawing.Size(1291, 643);
            Controls.Add(panelLockers);
            Controls.Add(panelInferior);
            Controls.Add(panelSuperior);
            Font = new System.Drawing.Font("Segoe UI", 9F);
            Name = "FrmEstadoLockers";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Estado de lockers";
            Load += FrmEstadoLockers_Load;
            panelSuperior.ResumeLayout(false);
            panelSuperior.PerformLayout();
            panelInferior.ResumeLayout(false);
            panelInferior.PerformLayout();
            ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panelSuperior;
        private System.Windows.Forms.Label lblEdificio;
        private System.Windows.Forms.ComboBox cboEdificio;
        private System.Windows.Forms.Label lblSector;
        private System.Windows.Forms.ComboBox cboSector;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Label lblResumen;
        private System.Windows.Forms.FlowLayoutPanel panelLockers;
        private System.Windows.Forms.Panel panelInferior;
        private System.Windows.Forms.Label lblLeyenda;
        private System.Windows.Forms.Label lblDetalle;
    }
}
