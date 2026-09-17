namespace LockersInteligentes.UI
{
    partial class FrmRegistro
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
            lblNombre = new System.Windows.Forms.Label();
            txtNombre = new System.Windows.Forms.TextBox();
            lblApellido = new System.Windows.Forms.Label();
            txtApellido = new System.Windows.Forms.TextBox();
            lblCorreo = new System.Windows.Forms.Label();
            txtCorreo = new System.Windows.Forms.TextBox();
            lblNombreUsuario = new System.Windows.Forms.Label();
            txtNombreUsuario = new System.Windows.Forms.TextBox();
            lblPassword = new System.Windows.Forms.Label();
            txtPassword = new System.Windows.Forms.TextBox();
            lblPasswordRepetida = new System.Windows.Forms.Label();
            txtPasswordRepetida = new System.Windows.Forms.TextBox();
            lblAyuda = new System.Windows.Forms.Label();
            lblMensaje = new System.Windows.Forms.Label();
            btnRegistrar = new System.Windows.Forms.Button();
            btnCancelar = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            lblTitulo.Location = new System.Drawing.Point(28, 24);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new System.Drawing.Size(233, 37);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Crear una cuenta";
            // 
            // lblSubtitulo
            // 
            lblSubtitulo.AutoSize = true;
            lblSubtitulo.ForeColor = System.Drawing.Color.Gray;
            lblSubtitulo.Location = new System.Drawing.Point(30, 62);
            lblSubtitulo.Name = "lblSubtitulo";
            lblSubtitulo.Size = new System.Drawing.Size(191, 20);
            lblSubtitulo.TabIndex = 1;
            lblSubtitulo.Text = "Se creará con rol Operador.";
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Location = new System.Drawing.Point(30, 96);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new System.Drawing.Size(64, 20);
            lblNombre.TabIndex = 2;
            lblNombre.Text = "Nombre";
            // 
            // txtNombre
            // 
            txtNombre.Location = new System.Drawing.Point(30, 116);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new System.Drawing.Size(175, 27);
            txtNombre.TabIndex = 0;
            // 
            // lblApellido
            // 
            lblApellido.AutoSize = true;
            lblApellido.Location = new System.Drawing.Point(215, 96);
            lblApellido.Name = "lblApellido";
            lblApellido.Size = new System.Drawing.Size(66, 20);
            lblApellido.TabIndex = 3;
            lblApellido.Text = "Apellido";
            // 
            // txtApellido
            // 
            txtApellido.Location = new System.Drawing.Point(215, 116);
            txtApellido.Name = "txtApellido";
            txtApellido.Size = new System.Drawing.Size(175, 27);
            txtApellido.TabIndex = 1;
            // 
            // lblCorreo
            // 
            lblCorreo.AutoSize = true;
            lblCorreo.Location = new System.Drawing.Point(30, 152);
            lblCorreo.Name = "lblCorreo";
            lblCorreo.Size = new System.Drawing.Size(132, 20);
            lblCorreo.TabIndex = 4;
            lblCorreo.Text = "Correo electrónico";
            // 
            // txtCorreo
            // 
            txtCorreo.Location = new System.Drawing.Point(30, 172);
            txtCorreo.Name = "txtCorreo";
            txtCorreo.Size = new System.Drawing.Size(360, 27);
            txtCorreo.TabIndex = 2;
            // 
            // lblNombreUsuario
            // 
            lblNombreUsuario.AutoSize = true;
            lblNombreUsuario.Location = new System.Drawing.Point(30, 208);
            lblNombreUsuario.Name = "lblNombreUsuario";
            lblNombreUsuario.Size = new System.Drawing.Size(137, 20);
            lblNombreUsuario.TabIndex = 5;
            lblNombreUsuario.Text = "Nombre de usuario";
            // 
            // txtNombreUsuario
            // 
            txtNombreUsuario.Location = new System.Drawing.Point(30, 228);
            txtNombreUsuario.Name = "txtNombreUsuario";
            txtNombreUsuario.Size = new System.Drawing.Size(360, 27);
            txtNombreUsuario.TabIndex = 3;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Location = new System.Drawing.Point(30, 264);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new System.Drawing.Size(83, 20);
            lblPassword.TabIndex = 6;
            lblPassword.Text = "Contraseña";
            // 
            // txtPassword
            // 
            txtPassword.Location = new System.Drawing.Point(30, 284);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new System.Drawing.Size(175, 27);
            txtPassword.TabIndex = 4;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // lblPasswordRepetida
            // 
            lblPasswordRepetida.AutoSize = true;
            lblPasswordRepetida.Location = new System.Drawing.Point(215, 264);
            lblPasswordRepetida.Name = "lblPasswordRepetida";
            lblPasswordRepetida.Size = new System.Drawing.Size(133, 20);
            lblPasswordRepetida.TabIndex = 7;
            lblPasswordRepetida.Text = "Repetir contraseña";
            // 
            // txtPasswordRepetida
            // 
            txtPasswordRepetida.Location = new System.Drawing.Point(215, 284);
            txtPasswordRepetida.Name = "txtPasswordRepetida";
            txtPasswordRepetida.Size = new System.Drawing.Size(175, 27);
            txtPasswordRepetida.TabIndex = 5;
            txtPasswordRepetida.UseSystemPasswordChar = true;
            // 
            // lblAyuda
            // 
            lblAyuda.AutoSize = true;
            lblAyuda.ForeColor = System.Drawing.Color.Gray;
            lblAyuda.Location = new System.Drawing.Point(30, 312);
            lblAyuda.Name = "lblAyuda";
            lblAyuda.Size = new System.Drawing.Size(349, 20);
            lblAyuda.TabIndex = 8;
            lblAyuda.Text = "Mínimo 8 caracteres, combinando letras y números.";
            // 
            // lblMensaje
            // 
            lblMensaje.ForeColor = System.Drawing.Color.Firebrick;
            lblMensaje.Location = new System.Drawing.Point(28, 338);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new System.Drawing.Size(362, 34);
            lblMensaje.TabIndex = 9;
            // 
            // btnRegistrar
            // 
            btnRegistrar.Location = new System.Drawing.Point(30, 380);
            btnRegistrar.Name = "btnRegistrar";
            btnRegistrar.Size = new System.Drawing.Size(235, 36);
            btnRegistrar.TabIndex = 6;
            btnRegistrar.Text = "Crear cuenta";
            btnRegistrar.UseVisualStyleBackColor = true;
            btnRegistrar.Click += btnRegistrar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new System.Drawing.Point(275, 380);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new System.Drawing.Size(115, 36);
            btnCancelar.TabIndex = 7;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // FrmRegistro
            // 
            AcceptButton = btnRegistrar;
            BackColor = System.Drawing.Color.White;
            CancelButton = btnCancelar;
            ClientSize = new System.Drawing.Size(590, 446);
            Controls.Add(lblTitulo);
            Controls.Add(lblSubtitulo);
            Controls.Add(lblNombre);
            Controls.Add(txtNombre);
            Controls.Add(lblApellido);
            Controls.Add(txtApellido);
            Controls.Add(lblCorreo);
            Controls.Add(txtCorreo);
            Controls.Add(lblNombreUsuario);
            Controls.Add(txtNombreUsuario);
            Controls.Add(lblPassword);
            Controls.Add(txtPassword);
            Controls.Add(lblPasswordRepetida);
            Controls.Add(txtPasswordRepetida);
            Controls.Add(lblAyuda);
            Controls.Add(lblMensaje);
            Controls.Add(btnRegistrar);
            Controls.Add(btnCancelar);
            Font = new System.Drawing.Font("Segoe UI", 9F);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmRegistro";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Registro de operador";
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblApellido;
        private System.Windows.Forms.TextBox txtApellido;
        private System.Windows.Forms.Label lblCorreo;
        private System.Windows.Forms.TextBox txtCorreo;
        private System.Windows.Forms.Label lblNombreUsuario;
        private System.Windows.Forms.TextBox txtNombreUsuario;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblPasswordRepetida;
        private System.Windows.Forms.TextBox txtPasswordRepetida;
        private System.Windows.Forms.Label lblAyuda;
        private System.Windows.Forms.Label lblMensaje;
        private System.Windows.Forms.Button btnRegistrar;
        private System.Windows.Forms.Button btnCancelar;
    }
}