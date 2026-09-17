namespace LockersInteligentes.UI
{
    partial class FrmLogin
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
            lblSubtitulo = new System.Windows.Forms.Label();
            lblUsuario = new System.Windows.Forms.Label();
            txtUsuario = new System.Windows.Forms.TextBox();
            lblContrasenia = new System.Windows.Forms.Label();
            txtContrasenia = new System.Windows.Forms.TextBox();
            chkMostrarContrasenia = new System.Windows.Forms.CheckBox();
            lblMensaje = new System.Windows.Forms.Label();
            btnIngresar = new System.Windows.Forms.Button();
            btnCancelar = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            lblTitulo.Location = new System.Drawing.Point(28, 24);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new System.Drawing.Size(184, 37);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Iniciar sesión";
            // 
            // lblSubtitulo
            // 
            lblSubtitulo.AutoSize = true;
            lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblSubtitulo.ForeColor = System.Drawing.Color.Gray;
            lblSubtitulo.Location = new System.Drawing.Point(30, 62);
            lblSubtitulo.Name = "lblSubtitulo";
            lblSubtitulo.Size = new System.Drawing.Size(310, 20);
            lblSubtitulo.TabIndex = 1;
            lblSubtitulo.Text = "Ingresá con tu usuario del sistema de gestión.";
            // 
            // lblUsuario
            // 
            lblUsuario.AutoSize = true;
            lblUsuario.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblUsuario.Location = new System.Drawing.Point(30, 102);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new System.Drawing.Size(59, 20);
            lblUsuario.TabIndex = 2;
            lblUsuario.Text = "Usuario";
            // 
            // txtUsuario
            // 
            txtUsuario.Font = new System.Drawing.Font("Segoe UI", 10F);
            txtUsuario.Location = new System.Drawing.Point(30, 122);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new System.Drawing.Size(360, 30);
            txtUsuario.TabIndex = 0;
            // 
            // lblContrasenia
            // 
            lblContrasenia.AutoSize = true;
            lblContrasenia.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblContrasenia.Location = new System.Drawing.Point(30, 162);
            lblContrasenia.Name = "lblContrasenia";
            lblContrasenia.Size = new System.Drawing.Size(83, 20);
            lblContrasenia.TabIndex = 3;
            lblContrasenia.Text = "Contraseña";
            // 
            // txtContrasenia
            // 
            txtContrasenia.Font = new System.Drawing.Font("Segoe UI", 10F);
            txtContrasenia.Location = new System.Drawing.Point(30, 182);
            txtContrasenia.Name = "txtContrasenia";
            txtContrasenia.Size = new System.Drawing.Size(360, 30);
            txtContrasenia.TabIndex = 1;
            txtContrasenia.UseSystemPasswordChar = true;
            // 
            // chkMostrarContrasenia
            // 
            chkMostrarContrasenia.AutoSize = true;
            chkMostrarContrasenia.Font = new System.Drawing.Font("Segoe UI", 9F);
            chkMostrarContrasenia.Location = new System.Drawing.Point(30, 218);
            chkMostrarContrasenia.Name = "chkMostrarContrasenia";
            chkMostrarContrasenia.Size = new System.Drawing.Size(158, 24);
            chkMostrarContrasenia.TabIndex = 2;
            chkMostrarContrasenia.Text = "Mostrar contraseña";
            chkMostrarContrasenia.UseVisualStyleBackColor = true;
            chkMostrarContrasenia.CheckedChanged += chkMostrarContrasenia_CheckedChanged;
            // 
            // lblMensaje
            // 
            lblMensaje.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblMensaje.ForeColor = System.Drawing.Color.Firebrick;
            lblMensaje.Location = new System.Drawing.Point(28, 246);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new System.Drawing.Size(362, 32);
            lblMensaje.TabIndex = 4;
            // 
            // btnIngresar
            // 
            btnIngresar.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            btnIngresar.Location = new System.Drawing.Point(30, 284);
            btnIngresar.Name = "btnIngresar";
            btnIngresar.Size = new System.Drawing.Size(235, 36);
            btnIngresar.TabIndex = 3;
            btnIngresar.Text = "Ingresar";
            btnIngresar.UseVisualStyleBackColor = true;
            btnIngresar.Click += btnIngresar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            btnCancelar.Location = new System.Drawing.Point(275, 284);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new System.Drawing.Size(115, 36);
            btnCancelar.TabIndex = 4;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            this.lnkRegistrarse = new System.Windows.Forms.LinkLabel();
            this.lnkRegistrarse.AutoSize = true;
            this.lnkRegistrarse.Location = new System.Drawing.Point(30, 332);
            this.lnkRegistrarse.Name = "lnkRegistrarse";
            this.lnkRegistrarse.TabIndex = 5;
            this.lnkRegistrarse.TabStop = true;
            this.lnkRegistrarse.Text = "Crear una cuenta";
            this.lnkRegistrarse.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkRegistrarse_LinkClicked);
            //
            // FrmLogin
            // 
            AcceptButton = btnIngresar;
            BackColor = System.Drawing.Color.White;
            CancelButton = btnCancelar;
            ClientSize = new System.Drawing.Size(519, 371);
            Controls.Add(lblTitulo);
            Controls.Add(lblSubtitulo);
            Controls.Add(lblUsuario);
            Controls.Add(txtUsuario);
            Controls.Add(lblContrasenia);
            Controls.Add(txtContrasenia);
            Controls.Add(chkMostrarContrasenia);
            Controls.Add(lblMensaje);
            Controls.Add(btnIngresar);
            Controls.Add(btnCancelar);
            Controls.Add(lnkRegistrarse);
            Font = new System.Drawing.Font("Segoe UI", 9F);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmLogin";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Ingreso al sistema";
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Label lblContrasenia;
        private System.Windows.Forms.TextBox txtContrasenia;
        private System.Windows.Forms.CheckBox chkMostrarContrasenia;
        private System.Windows.Forms.Label lblMensaje;
        private System.Windows.Forms.Button btnIngresar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.LinkLabel lnkRegistrarse;
    }
}