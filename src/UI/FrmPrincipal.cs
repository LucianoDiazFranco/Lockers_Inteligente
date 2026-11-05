using LockersInteligentes.ArqBase.Logica;
using LockersInteligentes.ArqBase.Entidades;
using LockersInteligentes.UI.Gestion;
using LockersInteligentes.UI.Informacion;
using LockersInteligentes.UI.Operacion;
using System;
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

            if (GestorSesion.Instancia.TienePermiso(PermisoService.Patentes.VerEstadoLockers))
                AbrirHijo<FrmEstadoLockers>();
        }

        private void AplicarPermisosDeMenu()
        {
            GestorSesion sesion = GestorSesion.Instancia;

            bool verUsuarios = sesion.TienePermiso(PermisoService.Patentes.GestionarUsuarios);
            mnuUsuarios.Visible = verUsuarios;
            mnuSeguridad.Visible = verUsuarios;

            bool verEdificios = sesion.TienePermiso(PermisoService.Patentes.GestionarEdificios);
            bool verLockers = sesion.TienePermiso(PermisoService.Patentes.GestionarLockers);
            bool verResidentes = sesion.TienePermiso(PermisoService.Patentes.GestionarResidentes);
            bool verRepartidores = sesion.TienePermiso(PermisoService.Patentes.GestionarRepartidores);

            mnuEdificios.Visible = verEdificios;
            mnuLockers.Visible = verLockers;
            mnuResidentes.Visible = verResidentes;
            mnuRepartidores.Visible = verRepartidores;
            mnuGestion.Visible = verEdificios || verLockers || verResidentes || verRepartidores;

            bool verEstado = sesion.TienePermiso(PermisoService.Patentes.VerEstadoLockers);
            bool verReservar = sesion.TienePermiso(PermisoService.Patentes.Reservar);
            bool verEntrega = sesion.TienePermiso(PermisoService.Patentes.RegistrarEntrega);
            bool verRetiro = sesion.TienePermiso(PermisoService.Patentes.RegistrarRetiro);

            mnuEstadoLockers.Visible = verEstado;
            mnuReservar.Visible = verReservar;
            mnuEntrega.Visible = verEntrega;
            mnuRetiro.Visible = verRetiro;
            mnuOperacion.Visible = verEstado || verReservar || verEntrega || verRetiro;

            bool verReportes = sesion.TienePermiso(PermisoService.Patentes.VerReportes);
            bool verBitacora = sesion.TienePermiso(PermisoService.Patentes.VerBitacora);
            bool verErrores = sesion.TienePermiso(PermisoService.Patentes.VerErrores);

            mnuReportes.Visible = verReportes;
            mnuBitacora.Visible = verBitacora;
            mnuErrores.Visible = verErrores;
            mnuInformacion.Visible = verReportes || verBitacora || verErrores;

            bool verPermisos = sesion.TienePermiso(PermisoService.Patentes.GestionarPermisos);
            mnuPermisos.Visible = verPermisos;
            mnuSeguridad.Visible = verUsuarios || verPermisos;
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
        private void mnuEntrega_Click(object sender, EventArgs e)
        {
            AbrirHijo<FrmRegistrarEntrega>();
        }
        private void mnuRetiro_Click(object sender, EventArgs e)
        {
            AbrirHijo<FrmRegistrarRetiro>();
        }
        private void mnuBitacora_Click(object sender, EventArgs e)
        {
            AbrirHijo<FrmBitacora>();
        } 
        private void mnuReportes_Click(object sender, EventArgs e)
        {
            AbrirHijo<FrmReportes>();
        }
        private void mnuErrores_Click(object sender, EventArgs e)
        {
            AbrirHijo<FrmErrores>();
        }
        private void mnuPermisos_Click(object sender, EventArgs e)
        {
            AbrirHijo<FrmPermisos>();
        }
    }
}