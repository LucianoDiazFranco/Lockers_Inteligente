namespace LockersInteligentes.UI
{
    partial class FrmPermisos
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
            lblAyuda = new System.Windows.Forms.Label();
            tvArbol = new System.Windows.Forms.TreeView();
            btnActualizar = new System.Windows.Forms.Button();
            btnQuitarDeFamilia = new System.Windows.Forms.Button();
            grpComponente = new System.Windows.Forms.GroupBox();
            lblTipo = new System.Windows.Forms.Label();
            cboTipo = new System.Windows.Forms.ComboBox();
            lblNombre = new System.Windows.Forms.Label();
            txtNombre = new System.Windows.Forms.TextBox();
            lblDescripcion = new System.Windows.Forms.Label();
            txtDescripcion = new System.Windows.Forms.TextBox();
            btnCrear = new System.Windows.Forms.Button();
            btnEditar = new System.Windows.Forms.Button();
            btnEliminar = new System.Windows.Forms.Button();
            grpAnidar = new System.Windows.Forms.GroupBox();
            lblFamiliaDestino = new System.Windows.Forms.Label();
            cboFamiliaDestino = new System.Windows.Forms.ComboBox();
            btnAgregarAFamilia = new System.Windows.Forms.Button();
            grpUsuario = new System.Windows.Forms.GroupBox();
            lblUsuario = new System.Windows.Forms.Label();
            cboUsuario = new System.Windows.Forms.ComboBox();
            lstAsignados = new System.Windows.Forms.ListBox();
            btnAsignar = new System.Windows.Forms.Button();
            btnQuitarDeUsuario = new System.Windows.Forms.Button();
            lblMensaje = new System.Windows.Forms.Label();
            grpComponente.SuspendLayout();
            grpAnidar.SuspendLayout();
            grpUsuario.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            lblTitulo.Location = new System.Drawing.Point(20, 16);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new System.Drawing.Size(252, 32);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Permisos del sistema";
            // 
            // lblAyuda
            // 
            lblAyuda.AutoSize = true;
            lblAyuda.ForeColor = System.Drawing.Color.Gray;
            lblAyuda.Location = new System.Drawing.Point(22, 48);
            lblAyuda.Name = "lblAyuda";
            lblAyuda.Size = new System.Drawing.Size(626, 20);
            lblAyuda.TabIndex = 1;
            lblAyuda.Text = "Las familias agrupan patentes y otras familias. Un usuario puede recibir cualquiera de las dos.";
            // 
            // tvArbol
            // 
            tvArbol.HideSelection = false;
            tvArbol.Location = new System.Drawing.Point(20, 76);
            tvArbol.Name = "tvArbol";
            tvArbol.Size = new System.Drawing.Size(420, 420);
            tvArbol.TabIndex = 0;
            tvArbol.AfterSelect += tvArbol_AfterSelect;
            // 
            // btnActualizar
            // 
            btnActualizar.BackColor = System.Drawing.Color.CornflowerBlue;
            btnActualizar.FlatAppearance.BorderSize = 2;
            btnActualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnActualizar.Location = new System.Drawing.Point(20, 504);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new System.Drawing.Size(120, 30);
            btnActualizar.TabIndex = 2;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = false;
            btnActualizar.Click += btnActualizar_Click;
            // 
            // btnQuitarDeFamilia
            // 
            btnQuitarDeFamilia.BackColor = System.Drawing.Color.CornflowerBlue;
            btnQuitarDeFamilia.FlatAppearance.BorderSize = 2;
            btnQuitarDeFamilia.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnQuitarDeFamilia.Location = new System.Drawing.Point(152, 504);
            btnQuitarDeFamilia.Name = "btnQuitarDeFamilia";
            btnQuitarDeFamilia.Size = new System.Drawing.Size(288, 30);
            btnQuitarDeFamilia.TabIndex = 3;
            btnQuitarDeFamilia.Text = "Quitar el seleccionado de su familia";
            btnQuitarDeFamilia.UseVisualStyleBackColor = false;
            btnQuitarDeFamilia.Click += btnQuitarDeFamilia_Click;
            // 
            // grpComponente
            // 
            grpComponente.Controls.Add(lblTipo);
            grpComponente.Controls.Add(cboTipo);
            grpComponente.Controls.Add(lblNombre);
            grpComponente.Controls.Add(txtNombre);
            grpComponente.Controls.Add(lblDescripcion);
            grpComponente.Controls.Add(txtDescripcion);
            grpComponente.Controls.Add(btnCrear);
            grpComponente.Controls.Add(btnEditar);
            grpComponente.Controls.Add(btnEliminar);
            grpComponente.Location = new System.Drawing.Point(460, 76);
            grpComponente.Name = "grpComponente";
            grpComponente.Size = new System.Drawing.Size(440, 220);
            grpComponente.TabIndex = 4;
            grpComponente.TabStop = false;
            grpComponente.Text = "Familia o patente";
            // 
            // lblTipo
            // 
            lblTipo.AutoSize = true;
            lblTipo.Location = new System.Drawing.Point(16, 30);
            lblTipo.Name = "lblTipo";
            lblTipo.Size = new System.Drawing.Size(39, 20);
            lblTipo.TabIndex = 0;
            lblTipo.Text = "Tipo";
            // 
            // cboTipo
            // 
            cboTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboTipo.Location = new System.Drawing.Point(16, 54);
            cboTipo.Name = "cboTipo";
            cboTipo.Size = new System.Drawing.Size(180, 28);
            cboTipo.TabIndex = 1;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Location = new System.Drawing.Point(212, 30);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new System.Drawing.Size(64, 20);
            lblNombre.TabIndex = 2;
            lblNombre.Text = "Nombre";
            // 
            // txtNombre
            // 
            txtNombre.Location = new System.Drawing.Point(212, 54);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new System.Drawing.Size(210, 27);
            txtNombre.TabIndex = 3;
            // 
            // lblDescripcion
            // 
            lblDescripcion.AutoSize = true;
            lblDescripcion.Location = new System.Drawing.Point(16, 94);
            lblDescripcion.Name = "lblDescripcion";
            lblDescripcion.Size = new System.Drawing.Size(87, 20);
            lblDescripcion.TabIndex = 4;
            lblDescripcion.Text = "Descripción";
            // 
            // txtDescripcion
            // 
            txtDescripcion.Location = new System.Drawing.Point(16, 118);
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new System.Drawing.Size(406, 27);
            txtDescripcion.TabIndex = 5;
            // 
            // btnCrear
            // 
            btnCrear.BackColor = System.Drawing.Color.CornflowerBlue;
            btnCrear.FlatAppearance.BorderSize = 2;
            btnCrear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnCrear.Location = new System.Drawing.Point(16, 166);
            btnCrear.Name = "btnCrear";
            btnCrear.Size = new System.Drawing.Size(126, 34);
            btnCrear.TabIndex = 6;
            btnCrear.Text = "Crear";
            btnCrear.UseVisualStyleBackColor = false;
            btnCrear.Click += btnCrear_Click;
            // 
            // btnEditar
            // 
            btnEditar.BackColor = System.Drawing.Color.CornflowerBlue;
            btnEditar.FlatAppearance.BorderSize = 2;
            btnEditar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnEditar.Location = new System.Drawing.Point(156, 166);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new System.Drawing.Size(126, 34);
            btnEditar.TabIndex = 7;
            btnEditar.Text = "Editar";
            btnEditar.UseVisualStyleBackColor = false;
            btnEditar.Click += btnEditar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.BackColor = System.Drawing.Color.CornflowerBlue;
            btnEliminar.FlatAppearance.BorderSize = 2;
            btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnEliminar.Location = new System.Drawing.Point(296, 166);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new System.Drawing.Size(126, 34);
            btnEliminar.TabIndex = 8;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = false;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // grpAnidar
            // 
            grpAnidar.Controls.Add(lblFamiliaDestino);
            grpAnidar.Controls.Add(cboFamiliaDestino);
            grpAnidar.Controls.Add(btnAgregarAFamilia);
            grpAnidar.Location = new System.Drawing.Point(460, 306);
            grpAnidar.Name = "grpAnidar";
            grpAnidar.Size = new System.Drawing.Size(440, 110);
            grpAnidar.TabIndex = 5;
            grpAnidar.TabStop = false;
            grpAnidar.Text = "Anidar el seleccionado dentro de una familia";
            // 
            // lblFamiliaDestino
            // 
            lblFamiliaDestino.AutoSize = true;
            lblFamiliaDestino.Location = new System.Drawing.Point(16, 30);
            lblFamiliaDestino.Name = "lblFamiliaDestino";
            lblFamiliaDestino.Size = new System.Drawing.Size(109, 20);
            lblFamiliaDestino.TabIndex = 0;
            lblFamiliaDestino.Text = "Familia destino";
            // 
            // cboFamiliaDestino
            // 
            cboFamiliaDestino.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboFamiliaDestino.Location = new System.Drawing.Point(16, 54);
            cboFamiliaDestino.Name = "cboFamiliaDestino";
            cboFamiliaDestino.Size = new System.Drawing.Size(250, 28);
            cboFamiliaDestino.TabIndex = 1;
            // 
            // btnAgregarAFamilia
            // 
            btnAgregarAFamilia.BackColor = System.Drawing.Color.CornflowerBlue;
            btnAgregarAFamilia.FlatAppearance.BorderSize = 2;
            btnAgregarAFamilia.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnAgregarAFamilia.Location = new System.Drawing.Point(282, 50);
            btnAgregarAFamilia.Name = "btnAgregarAFamilia";
            btnAgregarAFamilia.Size = new System.Drawing.Size(140, 34);
            btnAgregarAFamilia.TabIndex = 2;
            btnAgregarAFamilia.Text = "Agregar";
            btnAgregarAFamilia.UseVisualStyleBackColor = false;
            btnAgregarAFamilia.Click += btnAgregarAFamilia_Click;
            // 
            // grpUsuario
            // 
            grpUsuario.Controls.Add(lblUsuario);
            grpUsuario.Controls.Add(cboUsuario);
            grpUsuario.Controls.Add(lstAsignados);
            grpUsuario.Controls.Add(btnAsignar);
            grpUsuario.Controls.Add(btnQuitarDeUsuario);
            grpUsuario.Location = new System.Drawing.Point(460, 426);
            grpUsuario.Name = "grpUsuario";
            grpUsuario.Size = new System.Drawing.Size(440, 220);
            grpUsuario.TabIndex = 6;
            grpUsuario.TabStop = false;
            grpUsuario.Text = "Asignar a usuario";
            // 
            // lblUsuario
            // 
            lblUsuario.AutoSize = true;
            lblUsuario.Location = new System.Drawing.Point(16, 30);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new System.Drawing.Size(59, 20);
            lblUsuario.TabIndex = 0;
            lblUsuario.Text = "Usuario";
            // 
            // cboUsuario
            // 
            cboUsuario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboUsuario.Location = new System.Drawing.Point(16, 54);
            cboUsuario.Name = "cboUsuario";
            cboUsuario.Size = new System.Drawing.Size(250, 28);
            cboUsuario.TabIndex = 1;
            cboUsuario.SelectedIndexChanged += cboUsuario_SelectedIndexChanged;
            // 
            // lstAsignados
            // 
            lstAsignados.Location = new System.Drawing.Point(16, 92);
            lstAsignados.Name = "lstAsignados";
            lstAsignados.Size = new System.Drawing.Size(250, 104);
            lstAsignados.TabIndex = 2;
            // 
            // btnAsignar
            // 
            btnAsignar.BackColor = System.Drawing.Color.CornflowerBlue;
            btnAsignar.FlatAppearance.BorderSize = 2;
            btnAsignar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnAsignar.Location = new System.Drawing.Point(282, 92);
            btnAsignar.Name = "btnAsignar";
            btnAsignar.Size = new System.Drawing.Size(140, 42);
            btnAsignar.TabIndex = 3;
            btnAsignar.Text = "Asignar el seleccionado del árbol";
            btnAsignar.UseVisualStyleBackColor = false;
            btnAsignar.Click += btnAsignar_Click;
            // 
            // btnQuitarDeUsuario
            // 
            btnQuitarDeUsuario.BackColor = System.Drawing.Color.CornflowerBlue;
            btnQuitarDeUsuario.FlatAppearance.BorderSize = 2;
            btnQuitarDeUsuario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnQuitarDeUsuario.Location = new System.Drawing.Point(282, 144);
            btnQuitarDeUsuario.Name = "btnQuitarDeUsuario";
            btnQuitarDeUsuario.Size = new System.Drawing.Size(140, 42);
            btnQuitarDeUsuario.TabIndex = 4;
            btnQuitarDeUsuario.Text = "Quitar el de la lista";
            btnQuitarDeUsuario.UseVisualStyleBackColor = false;
            btnQuitarDeUsuario.Click += btnQuitarDeUsuario_Click;
            // 
            // lblMensaje
            // 
            lblMensaje.Location = new System.Drawing.Point(18, 544);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new System.Drawing.Size(422, 100);
            lblMensaje.TabIndex = 7;
            // 
            // FrmPermisos
            // 
            BackColor = System.Drawing.Color.LightSkyBlue;
            ClientSize = new System.Drawing.Size(924, 668);
            Controls.Add(lblTitulo);
            Controls.Add(lblAyuda);
            Controls.Add(tvArbol);
            Controls.Add(btnActualizar);
            Controls.Add(btnQuitarDeFamilia);
            Controls.Add(grpComponente);
            Controls.Add(grpAnidar);
            Controls.Add(grpUsuario);
            Controls.Add(lblMensaje);
            Font = new System.Drawing.Font("Segoe UI", 9F);
            Name = "FrmPermisos";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Permisos";
            Load += FrmPermisos_Load;
            grpComponente.ResumeLayout(false);
            grpComponente.PerformLayout();
            grpAnidar.ResumeLayout(false);
            grpAnidar.PerformLayout();
            grpUsuario.ResumeLayout(false);
            grpUsuario.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblAyuda;
        private System.Windows.Forms.TreeView tvArbol;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Button btnQuitarDeFamilia;
        private System.Windows.Forms.GroupBox grpComponente;
        private System.Windows.Forms.Label lblTipo;
        private System.Windows.Forms.ComboBox cboTipo;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Button btnCrear;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.GroupBox grpAnidar;
        private System.Windows.Forms.Label lblFamiliaDestino;
        private System.Windows.Forms.ComboBox cboFamiliaDestino;
        private System.Windows.Forms.Button btnAgregarAFamilia;
        private System.Windows.Forms.GroupBox grpUsuario;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.ComboBox cboUsuario;
        private System.Windows.Forms.ListBox lstAsignados;
        private System.Windows.Forms.Button btnAsignar;
        private System.Windows.Forms.Button btnQuitarDeUsuario;
        private System.Windows.Forms.Label lblMensaje;
    }
}