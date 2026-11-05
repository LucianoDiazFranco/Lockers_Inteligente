using LockersInteligentes.ArqBase.Composite;
using LockersInteligentes.ArqBase.Entidades;
using LockersInteligentes.ArqBase.Enums;
using LockersInteligentes.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace LockersInteligentes.UI
{
    public partial class FrmPermisos : Form
    {
        private readonly SistemaLockersFacade _sistema = SistemaLockersFacade.Instancia;
        private IList<Usuario> _usuarios = new List<Usuario>();
        public FrmPermisos()
        {
            InitializeComponent();
        }

        private void FrmPermisos_Load(object sender, EventArgs e)
        {
            try
            {
                cboTipo.DataSource = Enum.GetValues(typeof(TipoComponente));

                _usuarios = _sistema.ListarUsuarios();
                cboUsuario.DisplayMember = "Texto";
                cboUsuario.ValueMember = "Id";
                cboUsuario.DataSource = _usuarios
                    .Select(u => new { u.Id, Texto = u.NombreUsuario + " - " + u.NombreCompleto() })
                    .ToList();

                Recargar();
            }
            catch (Exception ex)
            {
                Mostrar(ex.Message, true);
            }
        }

        private void Recargar()
        {
            CargarArbol();
            CargarFamiliasDestino();
            CargarAsignados();
        }

        /// <summary>
        /// Dibuja el árbol desde las raíces hacia abajo.
        ///
        /// Un componente puede aparecer más de una vez si está en varias familias:
        /// eso no es un error, es el grafo real. El Tag de cada nodo guarda el
        /// objeto Componente, que es la misma instancia en todas sus apariciones.
        /// </summary>
        private void CargarArbol()
        {
            tvArbol.BeginUpdate();
            tvArbol.Nodes.Clear();

            foreach (Componente raiz in _sistema.ListarPermisosRaiz())
                tvArbol.Nodes.Add(CrearNodo(raiz, 0));

            tvArbol.ExpandAll();
            tvArbol.EndUpdate();
        }

        /// <summary>
        /// Construye el nodo y sus hijos recursivamente.
        ///
        /// El límite de profundidad es una red de seguridad: si por algún motivo
        /// quedara un ciclo en la base, sin este tope el dibujo del árbol entraría
        /// en recursión infinita y colgaría la aplicación.
        /// </summary>
        private TreeNode CrearNodo(Componente componente, int profundidad)
        {
            bool esFamilia = componente is Familia;

            TreeNode nodo = new TreeNode(
                componente.Nombre + (esFamilia ? "   [familia]" : ""));

            nodo.Tag = componente;
            nodo.ForeColor = esFamilia ? Color.DarkSlateBlue : Color.Black;

            if (profundidad >= 10)
            {
                nodo.Nodes.Add("... (profundidad máxima alcanzada)");
                return nodo;
            }

            foreach (Componente hijo in componente.ObtenerHijos().OrderBy(c => c.Nombre))
                nodo.Nodes.Add(CrearNodo(hijo, profundidad + 1));

            return nodo;
        }

        private void CargarFamiliasDestino()
        {
            List<Componente> familias = _sistema.ObtenerArbolPermisos().Values
                .Where(c => c is Familia)
                .OrderBy(c => c.Nombre)
                .ToList();

            cboFamiliaDestino.DisplayMember = "Nombre";
            cboFamiliaDestino.ValueMember = "Id";
            cboFamiliaDestino.DataSource = familias;
        }

        private Componente Seleccionado()
        {
            return tvArbol.SelectedNode != null
                ? tvArbol.SelectedNode.Tag as Componente
                : null;
        }

        private void tvArbol_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Componente componente = Seleccionado();

            if (componente == null)
                return;

            txtNombre.Text = componente.Nombre;
            txtDescripcion.Text = componente.Descripcion;
            cboTipo.SelectedItem = componente is Familia
                ? TipoComponente.Familia
                : TipoComponente.Patente;

            int hijos = componente.ObtenerHijos().Count();

            Mostrar(componente.Nombre + (componente is Familia
                ? " es una familia con " + hijos + " hijo(s) directo(s)."
                : " es una patente."), false);
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            try
            {
                _sistema.CrearComponente(txtNombre.Text, txtDescripcion.Text,
                                (TipoComponente)cboTipo.SelectedItem);

                Mostrar("Componente creado.", false);
                Recargar();
            }
            catch (Exception ex)
            {
                Mostrar(ex.Message, true);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Componente componente = Seleccionado();

            if (componente == null)
            {
                Mostrar("Seleccioná un componente del árbol.", true);
                return;
            }

            try
            {
                _sistema.ModificarComponente(componente.Id, txtNombre.Text, txtDescripcion.Text);
                Mostrar("Componente actualizado.", false);
                Recargar();
            }
            catch (Exception ex)
            {
                Mostrar(ex.Message, true);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Componente componente = Seleccionado();

            if (componente == null)
            {
                Mostrar("Seleccioná un componente del árbol.", true);
                return;
            }

            if (MessageBox.Show(
                    "¿Eliminar '" + componente.Nombre + "'?" + Environment.NewLine +
                    "Se va a quitar de todas las familias y de todos los usuarios.",
                    "Confirmar", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            try
            {
                _sistema.EliminarComponente(componente.Id);
                Mostrar("Componente eliminado.", false);
                Recargar();
            }
            catch (Exception ex)
            {
                Mostrar(ex.Message, true);
            }
        }

        /// <summary>
        /// Mete el componente seleccionado dentro de la familia elegida.
        /// Acá es donde la validación de ciclos deja de ser teórica: si se intenta
        /// crear una circularidad, el service la rechaza antes de tocar la base.
        /// </summary>
        private void btnAgregarAFamilia_Click(object sender, EventArgs e)
        {
            Componente hijo = Seleccionado();

            if (hijo == null)
            {
                Mostrar("Seleccioná del árbol el componente que querés anidar.", true);
                return;
            }

            if (cboFamiliaDestino.SelectedValue == null)
            {
                Mostrar("Elegí la familia destino.", true);
                return;
            }

            try
            {
                _sistema.AnidarComponente(Convert.ToInt32(cboFamiliaDestino.SelectedValue), hijo.Id);
                Mostrar("'" + hijo.Nombre + "' agregado correctamente.", false);
                Recargar();
            }
            catch (Exception ex)
            {
                Mostrar(ex.Message, true);
            }
        }

        private void btnQuitarDeFamilia_Click(object sender, EventArgs e)
        {
            TreeNode nodo = tvArbol.SelectedNode;

            if (nodo == null || nodo.Parent == null)
            {
                Mostrar("Seleccioná un componente que esté dentro de una familia.", true);
                return;
            }

            Componente hijo = nodo.Tag as Componente;
            Componente padre = nodo.Parent.Tag as Componente;

            if (hijo == null || padre == null)
                return;

            try
            {
                _sistema.DesanidarComponente(padre.Id, hijo.Id);
                Mostrar("'" + hijo.Nombre + "' quitado de '" + padre.Nombre + "'.", false);
                Recargar();
            }
            catch (Exception ex)
            {
                Mostrar(ex.Message, true);
            }
        }

        private void cboUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarAsignados();
        }

        private void CargarAsignados()
        {
            if (cboUsuario.SelectedValue == null)
                return;

            try
            {
                lstAsignados.DisplayMember = "Nombre";
                lstAsignados.DataSource = _sistema
                    .PermisosDeUsuario(Convert.ToInt32(cboUsuario.SelectedValue))
                    .ToList();
            }
            catch (Exception ex)
            {
                Mostrar(ex.Message, true);
            }
        }

        private void btnAsignar_Click(object sender, EventArgs e)
        {
            Componente componente = Seleccionado();

            if (componente == null || cboUsuario.SelectedValue == null)
            {
                Mostrar("Seleccioná un componente del árbol y un usuario.", true);
                return;
            }

            try
            {
                _sistema.AsignarPermisoAUsuario(
                    Convert.ToInt32(cboUsuario.SelectedValue), componente.Id);

                Mostrar("'" + componente.Nombre + "' asignado. El usuario lo ve " +
                        "al volver a iniciar sesión.", false);

                CargarAsignados();
            }
            catch (Exception ex)
            {
                Mostrar(ex.Message, true);
            }
        }

        private void btnQuitarDeUsuario_Click(object sender, EventArgs e)
        {
            Componente componente = lstAsignados.SelectedItem as Componente;

            if (componente == null || cboUsuario.SelectedValue == null)
            {
                Mostrar("Seleccioná un componente de la lista de asignados.", true);
                return;
            }

            try
            {
                _sistema.QuitarPermisoAUsuario(
                    Convert.ToInt32(cboUsuario.SelectedValue), componente.Id);

                Mostrar("'" + componente.Nombre + "' quitado del usuario.", false);
                CargarAsignados();
            }
            catch (Exception ex)
            {
                Mostrar(ex.Message, true);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            Recargar();
        }

        private void Mostrar(string mensaje, bool esError)
        {
            lblMensaje.ForeColor = esError ? Color.Firebrick : Color.SeaGreen;
            lblMensaje.Text = mensaje;
        }

    }
}
