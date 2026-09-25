using LockersInteligentes.BLL;
using LockersInteligentes.Dominio.Entidades;
using LockersInteligentes.UI.Gestion;
using LockersInteligentes.UI.Operacion;
using System;
using System.Drawing;
using System.Drawing;
using System.Windows.Forms;

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
            foreach (Control control in Controls) // color del fondo 
            {
                MdiClient area = control as MdiClient;

                if (area != null)
                {
                    area.BackColor = Color.RoyalBlue;
                    break;
                }
            }
                Usuario usuario = GestorSesion.Instancia.UsuarioActual;

            lblEstadoUsuario.Text = "Usuario: " + usuario.NombreCompleto() +
                                    "   |   Rol: " + usuario.Rol.Nombre;

            AplicarPermisosDeMenu();
            AbrirHijo<FrmEstadoLockers>();
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

                    FrmEstadoLockers panel = abierto as FrmEstadoLockers;

                    if (panel != null)
                        panel.Refrescar();

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
        private void mnuEstadoLockers_Click(object sender, EventArgs e)
        {
            AbrirHijo<FrmEstadoLockers>();
        }
        private void mnuEdificios_Click(object sender, EventArgs e)
        {
            AbrirHijo<FrmEdificios>();
        }
        private void mnuLockers_Click(object sender, EventArgs e)
        {
            AbrirHijo<FrmLockers>();
        }
        private void mnuRepartidores_Click(object sender, EventArgs e)
        {
            AbrirHijo<FrmRepartidores>();
        }
        private void mnuResidentes_Click(object sender, EventArgs e)
        {
            AbrirHijo<FrmResidentes>();
        }
    }
}