using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace LockersInteligentes.UI
{
    partial class FrmReservarLocker
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
            lblTitulo = new Label();
            lblResidente = new Label();
            cboResidente = new ComboBox();
            lblEdificio = new Label();
            lblTamanio = new Label();
            cboTamanio = new ComboBox();
            lblDescripcion = new Label();
            txtDescripcion = new TextBox();
            btnReservar = new Button();
            btnLimpiar = new Button();
            grpResultado = new GroupBox();
            lblResultado = new Label();
            lblPin = new Label();
            lblAvisoPin = new Label();
            lblMensaje = new Label();
            grpResultado.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            lblTitulo.Location = new System.Drawing.Point(24, 20);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new System.Drawing.Size(190, 32);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Reservar locker";
            // 
            // lblResidente
            // 
            lblResidente.AutoSize = true;
            lblResidente.Location = new System.Drawing.Point(26, 70);
            lblResidente.Name = "lblResidente";
            lblResidente.Size = new System.Drawing.Size(74, 20);
            lblResidente.TabIndex = 1;
            lblResidente.Text = "Residente";
            // 
            // cboResidente
            // 
            cboResidente.DropDownStyle = ComboBoxStyle.DropDownList;
            cboResidente.Location = new System.Drawing.Point(26, 90);
            cboResidente.Name = "cboResidente";
            cboResidente.Size = new System.Drawing.Size(380, 28);
            cboResidente.TabIndex = 0;
            cboResidente.SelectedIndexChanged += cboResidente_SelectedIndexChanged;
            // 
            // lblEdificio
            // 
            lblEdificio.AutoSize = true;
            lblEdificio.ForeColor = System.Drawing.Color.Gray;
            lblEdificio.Location = new System.Drawing.Point(26, 120);
            lblEdificio.Name = "lblEdificio";
            lblEdificio.Size = new System.Drawing.Size(0, 20);
            lblEdificio.TabIndex = 2;
            // 
            // lblTamanio
            // 
            lblTamanio.AutoSize = true;
            lblTamanio.Location = new System.Drawing.Point(26, 150);
            lblTamanio.Name = "lblTamanio";
            lblTamanio.Size = new System.Drawing.Size(145, 20);
            lblTamanio.TabIndex = 3;
            lblTamanio.Text = "Tamaño del paquete";
            // 
            // cboTamanio
            // 
            cboTamanio.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTamanio.Location = new System.Drawing.Point(26, 170);
            cboTamanio.Name = "cboTamanio";
            cboTamanio.Size = new System.Drawing.Size(180, 28);
            cboTamanio.TabIndex = 1;
            // 
            // lblDescripcion
            // 
            lblDescripcion.AutoSize = true;
            lblDescripcion.Location = new System.Drawing.Point(26, 206);
            lblDescripcion.Name = "lblDescripcion";
            lblDescripcion.Size = new System.Drawing.Size(243, 20);
            lblDescripcion.TabIndex = 4;
            lblDescripcion.Text = "Descripción del paquete (opcional)";
            // 
            // txtDescripcion
            // 
            txtDescripcion.Location = new System.Drawing.Point(26, 226);
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new System.Drawing.Size(380, 27);
            txtDescripcion.TabIndex = 2;
            // 
            // btnReservar
            // 
            btnReservar.BackColor = System.Drawing.Color.CornflowerBlue;
            btnReservar.FlatAppearance.BorderSize = 2;
            btnReservar.FlatStyle = FlatStyle.Flat;
            btnReservar.Location = new System.Drawing.Point(26, 268);
            btnReservar.Name = "btnReservar";
            btnReservar.Size = new System.Drawing.Size(250, 36);
            btnReservar.TabIndex = 3;
            btnReservar.Text = "Reservar locker";
            btnReservar.UseVisualStyleBackColor = false;
            btnReservar.Click += btnReservar_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.BackColor = System.Drawing.Color.CornflowerBlue;
            btnLimpiar.FlatAppearance.BorderSize = 2;
            btnLimpiar.FlatStyle = FlatStyle.Flat;
            btnLimpiar.Location = new System.Drawing.Point(286, 268);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new System.Drawing.Size(120, 36);
            btnLimpiar.TabIndex = 4;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = false;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // grpResultado
            // 
            grpResultado.BackColor = System.Drawing.Color.DodgerBlue;
            grpResultado.Controls.Add(lblResultado);
            grpResultado.Controls.Add(lblPin);
            grpResultado.Controls.Add(lblAvisoPin);
            grpResultado.Location = new System.Drawing.Point(430, 66);
            grpResultado.Name = "grpResultado";
            grpResultado.Size = new System.Drawing.Size(340, 290);
            grpResultado.TabIndex = 6;
            grpResultado.TabStop = false;
            grpResultado.Text = "Reserva registrada";
            grpResultado.Visible = false;
            // 
            // lblResultado
            // 
            lblResultado.Location = new System.Drawing.Point(16, 30);
            lblResultado.Name = "lblResultado";
            lblResultado.Size = new System.Drawing.Size(308, 130);
            lblResultado.TabIndex = 0;
            // 
            // lblPin
            // 
            lblPin.Font = new System.Drawing.Font("Consolas", 22F, System.Drawing.FontStyle.Bold);
            lblPin.ForeColor = System.Drawing.Color.SeaGreen;
            lblPin.Location = new System.Drawing.Point(16, 166);
            lblPin.Name = "lblPin";
            lblPin.Size = new System.Drawing.Size(308, 44);
            lblPin.TabIndex = 1;
            lblPin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAvisoPin
            // 
            lblAvisoPin.ForeColor = System.Drawing.Color.Gray;
            lblAvisoPin.Location = new System.Drawing.Point(16, 216);
            lblAvisoPin.Name = "lblAvisoPin";
            lblAvisoPin.Size = new System.Drawing.Size(308, 60);
            lblAvisoPin.TabIndex = 2;
            // 
            // lblMensaje
            // 
            lblMensaje.ForeColor = System.Drawing.Color.Firebrick;
            lblMensaje.Location = new System.Drawing.Point(24, 316);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new System.Drawing.Size(382, 40);
            lblMensaje.TabIndex = 5;
            // 
            // FrmReservarLocker
            // 
            BackColor = System.Drawing.Color.LightSkyBlue;
            ClientSize = new System.Drawing.Size(874, 462);
            Controls.Add(lblTitulo);
            Controls.Add(lblResidente);
            Controls.Add(cboResidente);
            Controls.Add(lblEdificio);
            Controls.Add(lblTamanio);
            Controls.Add(cboTamanio);
            Controls.Add(lblDescripcion);
            Controls.Add(txtDescripcion);
            Controls.Add(btnReservar);
            Controls.Add(btnLimpiar);
            Controls.Add(lblMensaje);
            Controls.Add(grpResultado);
            Font = new System.Drawing.Font("Segoe UI", 9F);
            Name = "FrmReservarLocker";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Reservar locker";
            Load += FrmReservarLocker_Load;
            grpResultado.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblResidente;
        private System.Windows.Forms.ComboBox cboResidente;
        private System.Windows.Forms.Label lblEdificio;
        private System.Windows.Forms.Label lblTamanio;
        private System.Windows.Forms.ComboBox cboTamanio;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Button btnReservar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.GroupBox grpResultado;
        private System.Windows.Forms.Label lblResultado;
        private System.Windows.Forms.Label lblPin;
        private System.Windows.Forms.Label lblAvisoPin;
        private System.Windows.Forms.Label lblMensaje;
    }
}