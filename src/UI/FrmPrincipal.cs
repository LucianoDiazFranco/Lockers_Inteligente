using System;
using System.Windows.Forms;
using LockersInteligentes.BLL;
using LockersInteligentes.Dominio.Entidades;

namespace LockersInteligentes.UI
{
    public partial class FrmPrincipal : Form
    {
        public bool CerroSesion { get; private set; }

        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            Usuario usuario = GestorSesion.Instancia.UsuarioActual;

            lblEstadoUsuario.Text = "Usuario: " + usuario.NombreCompleto() +
                                    "   |   Rol: " + usuario.Rol.Nombre;

            AplicarPermisosDeMenu();
        }

        private void AplicarPermisosDeMenu()
        {
            bool esAdmin = GestorSesion.Instancia.EsAdministrador();

            mnuSeguridad.Visible = esAdmin;
            mnuGestion.Visible = esAdmin;
            mnuInformacion.Visible = esAdmin;
        }

        private void mnuUsuarios_Click(object sender, EventArgs e)
        {
            AbrirHijo<FrmUsuarios>();
        }
        private void mnuReservar_Click(object sender, EventArgs e)
        {
            AbrirHijo<FrmReservarLocker>();
        }
        private void AbrirHijo<T>() where T : Form, new()
        {
            foreach (Form abierto in MdiChildren)
            {
                if (abierto is T)
                {
                    abierto.Activate();
                    return;
                }
            }

            T hijo = new T();
            hijo.MdiParent = this;
            hijo.Show();
        }
        private void mnuCerrarSesion_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Cerrar la sesión actual?", "Confirmar",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            GestorSesion.Instancia.CerrarSesion();
            CerroSesion = true;
            Close();
        }

        private void mnuSalir_Click(object sender, EventArgs e)
        {
            CerroSesion = false;
            Close();
        }
    }
}