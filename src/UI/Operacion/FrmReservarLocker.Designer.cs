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
            lblTitulo = new System.Windows.Forms.Label();
            lblResidente = new System.Windows.Forms.Label();
            cboResidente = new System.Windows.Forms.ComboBox();
            lblEdificio = new System.Windows.Forms.Label();
            lblTamanio = new System.Windows.Forms.Label();
            cboTamanio = new System.Windows.Forms.ComboBox();
            lblDescripcion = new System.Windows.Forms.Label();
            txtDescripcion = new System.Windows.Forms.TextBox();
            btnReservar = new System.Windows.Forms.Button();
            btnLimpiar = new System.Windows.Forms.Button();
            grpResultado = new System.Windows.Forms.GroupBox();
            lblResultado = new System.Windows.Forms.Label();
            lblPin = new System.Windows.Forms.Label();
            lblAvisoPin = new System.Windows.Forms.Label();
            lblMensaje = new System.Windows.Forms.Label();
            grpResultado.SuspendLayout();
            SuspendLayout();

            lblTitulo.AutoSize = true;
            lblTitulo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            lblTitulo.Location = new System.Drawing.Point(24, 20);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Text = "Reservar locker";

            lblResidente.AutoSize = true;
            lblResidente.Location = new System.Drawing.Point(26, 70);
            lblResidente.Name = "lblResidente";
            lblResidente.Text = "Residente";

            cboResidente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboResidente.Location = new System.Drawing.Point(26, 90);
            cboResidente.Name = "cboResidente";
            cboResidente.Size = new System.Drawing.Size(380, 25);
            cboResidente.TabIndex = 0;
            cboResidente.SelectedIndexChanged += cboResidente_SelectedIndexChanged;

            lblEdificio.AutoSize = true;
            lblEdificio.ForeColor = System.Drawing.Color.Gray;
            lblEdificio.Location = new System.Drawing.Point(26, 120);
            lblEdificio.Name = "lblEdificio";
            lblEdificio.Text = "";

            lblTamanio.AutoSize = true;
            lblTamanio.Location = new System.Drawing.Point(26, 150);
            lblTamanio.Name = "lblTamanio";
            lblTamanio.Text = "Tamaño del paquete";

            cboTamanio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboTamanio.Location = new System.Drawing.Point(26, 170);
            cboTamanio.Name = "cboTamanio";
            cboTamanio.Size = new System.Drawing.Size(180, 25);
            cboTamanio.TabIndex = 1;

            lblDescripcion.AutoSize = true;
            lblDescripcion.Location = new System.Drawing.Point(26, 206);
            lblDescripcion.Name = "lblDescripcion";
            lblDescripcion.Text = "Descripción del paquete (opcional)";

            txtDescripcion.Location = new System.Drawing.Point(26, 226);
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new System.Drawing.Size(380, 25);
            txtDescripcion.TabIndex = 2;

            btnReservar.Location = new System.Drawing.Point(26, 268);
            btnReservar.Name = "btnReservar";
            btnReservar.Size = new System.Drawing.Size(250, 36);
            btnReservar.TabIndex = 3;
            btnReservar.Text = "Reservar locker";
            btnReservar.UseVisualStyleBackColor = true;
            btnReservar.Click += btnReservar_Click;

            btnLimpiar.Location = new System.Drawing.Point(286, 268);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new System.Drawing.Size(120, 36);
            btnLimpiar.TabIndex = 4;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;

            lblMensaje.ForeColor = System.Drawing.Color.Firebrick;
            lblMensaje.Location = new System.Drawing.Point(24, 316);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new System.Drawing.Size(382, 40);
            lblMensaje.Text = "";

            grpResultado.Controls.Add(lblResultado);
            grpResultado.Controls.Add(lblPin);
            grpResultado.Controls.Add(lblAvisoPin);
            grpResultado.Location = new System.Drawing.Point(430, 66);
            grpResultado.Name = "grpResultado";
            grpResultado.Size = new System.Drawing.Size(340, 290);
            grpResultado.TabStop = false;
            grpResultado.Text = "Reserva registrada";
            grpResultado.Visible = false;

            lblResultado.Location = new System.Drawing.Point(16, 30);
            lblResultado.Name = "lblResultado";
            lblResultado.Size = new System.Drawing.Size(308, 130);
            lblResultado.Text = "";

            lblPin.Font = new System.Drawing.Font("Consolas", 22F, System.Drawing.FontStyle.Bold);
            lblPin.ForeColor = System.Drawing.Color.SeaGreen;
            lblPin.Location = new System.Drawing.Point(16, 166);
            lblPin.Name = "lblPin";
            lblPin.Size = new System.Drawing.Size(308, 44);
            lblPin.Text = "";
            lblPin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            lblAvisoPin.ForeColor = System.Drawing.Color.Gray;
            lblAvisoPin.Location = new System.Drawing.Point(16, 216);
            lblAvisoPin.Name = "lblAvisoPin";
            lblAvisoPin.Size = new System.Drawing.Size(308, 60);
            lblAvisoPin.Text = "";

            ClientSize = new System.Drawing.Size(800, 380);
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
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
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