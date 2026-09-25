using LockersInteligentes.BLL;
using LockersInteligentes.Dominio.Entidades;
using LockersInteligentes.Dominio.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LockersInteligentes.UI.Operacion
{
    public partial class FrmEstadoLockers : Form
    {
        private static readonly Color ColorLibre = Color.FromArgb(46, 160, 67);
        private static readonly Color ColorReservado = Color.FromArgb(47, 109, 199);
        private static readonly Color ColorOcupado = Color.FromArgb(200, 55, 55);
        private static readonly Color ColorVencido = Color.FromArgb(222, 160, 32);
        private static readonly Color ColorFuera = Color.FromArgb(110, 118, 129);

        private readonly LockerService _servicio = new LockerService();
        private IList<EstadoLockerVista> _vista = new List<EstadoLockerVista>();

        public FrmEstadoLockers()
        {
            InitializeComponent();
        }
        private void FrmEstadoLockers_Load(object sender, EventArgs e)
        {
            try
            {
                cboEdificio.DisplayMember = "Nombre";
                cboEdificio.ValueMember = "Id";
                cboEdificio.DataSource = _servicio.ListarEdificios();

                MostrarLeyenda();
                Recargar();
            }
            catch (Exception ex)
            {
                lblDetalle.Text = ex.Message;
            }
        }

        private void cboEdificio_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarSectores();
            Recargar();
        }

        private void cboSector_SelectedIndexChanged(object sender, EventArgs e)
        {
            Dibujar();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            Recargar();
        }

        private void CargarSectores()
        {
            if (cboEdificio.SelectedValue == null)
                return;

            List<string> sectores = new List<string> { "(todos)" };
            sectores.AddRange(_servicio.ListarSectores(Convert.ToInt32(cboEdificio.SelectedValue)));

            cboSector.DataSource = sectores;
        }

        /// <summary>Va a la base y redibuja todo.</summary>
        private void Recargar()
        {
            if (cboEdificio.SelectedValue == null)
                return;

            try
            {
                _vista = _servicio.ObtenerEstado(Convert.ToInt32(cboEdificio.SelectedValue));
                MostrarResumen();
                Dibujar();
            }
            catch (Exception ex)
            {
                lblDetalle.Text = ex.Message;
            }
        }

        private void MostrarResumen()
        {
            ResumenLockers r = _servicio.Resumir(_vista);

            lblResumen.Text =
                r.Total + " lockers   |   " +
                r.Libres + " libres   |   " +
                r.Reservados + " reservados   |   " +
                r.Ocupados + " ocupados   |   " +
                r.FueraDeServicio + " fuera de servicio   |   " +
                "Ocupación: " + r.PorcentajeOcupacion() + "%" +
                (r.Vencidos > 0 ? "   |   " + r.Vencidos + " vencidos" : "");
        }


        private void Dibujar()     /// <summary> Redibuja los casilleros sin volver a consultar la base.</summary>
        {
            panelLockers.SuspendLayout();
            panelLockers.Controls.Clear();

            string sector = cboSector.SelectedItem as string;
            IEnumerable<EstadoLockerVista> filtrada = _vista;

            if (!string.IsNullOrEmpty(sector) && sector != "(todos)")
                filtrada = filtrada.Where(v => (v.Locker.GrupoLocker ?? "(sin sector)") == sector);

            foreach (EstadoLockerVista item in filtrada.OrderBy(v => v.Locker.Numero))
                panelLockers.Controls.Add(CrearCasillero(item));

            panelLockers.ResumeLayout();
        }

        /// <summary>
        /// Dibuja un casillero: número arriba, sector debajo, luz de estado al
        /// centro y ocupante al pie.
        /// </summary>
        private Control CrearCasillero(EstadoLockerVista item)
        {
            Panel puerta = new Panel();
            puerta.Size = new Size(104, 132);
            puerta.Margin = new Padding(8);
            puerta.BackColor = Color.FromArgb(150, 156, 164);
            puerta.BorderStyle = BorderStyle.FixedSingle;
            puerta.Cursor = Cursors.Hand;

            Label numero = new Label();
            numero.Text = item.Locker.Numero.ToString();
            numero.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            numero.TextAlign = ContentAlignment.MiddleCenter;
            numero.BackColor = Color.FromArgb(206, 210, 216);
            numero.Size = new Size(84, 24);
            numero.Location = new Point(10, 8);

            Label sector = new Label();
            sector.Text = item.Locker.GrupoLocker ?? "-";
            sector.Font = new Font("Segoe UI", 7.5F);
            sector.ForeColor = Color.FromArgb(60, 66, 74);
            sector.TextAlign = ContentAlignment.MiddleCenter;
            sector.Size = new Size(84, 16);
            sector.Location = new Point(10, 34);

            // Luz centrada: se calcula la posición en vez de escribir el número,
            // así sigue centrada si cambia el ancho de la puerta.
            Panel luz = new Panel();
            luz.Size = new Size(30, 30);
            luz.BackColor = ColorDe(item);
            luz.Location = new Point((puerta.Width - luz.Width) / 2 - 1, 58);

            Label tamanio = new Label();
            tamanio.Text = item.Locker.Tamanio.ToString();
            tamanio.Font = new Font("Segoe UI", 7.5F);
            tamanio.ForeColor = Color.FromArgb(70, 76, 84);
            tamanio.TextAlign = ContentAlignment.MiddleCenter;
            tamanio.Size = new Size(84, 16);
            tamanio.Location = new Point(10, 94);

            Label ocupante = new Label();
            ocupante.Text = item.Detalle();
            ocupante.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            ocupante.ForeColor = Color.FromArgb(30, 34, 40);
            ocupante.TextAlign = ContentAlignment.MiddleCenter;
            ocupante.Size = new Size(84, 18);
            ocupante.Location = new Point(10, 110);

            puerta.Controls.Add(numero);
            puerta.Controls.Add(sector);
            puerta.Controls.Add(luz);
            puerta.Controls.Add(tamanio);
            puerta.Controls.Add(ocupante);

            // El clic se engancha en la puerta y en cada hijo: si no, tocar sobre el
            // número no dispararía nada, porque el hijo se queda con el evento.
            EventHandler clic = (s, e) => MostrarDetalle(item);
            puerta.Click += clic;

            foreach (Control hijo in puerta.Controls)
                hijo.Click += clic;

            return puerta;
        }

        /// <summary>El vencido tiene prioridad: pinta ámbar aunque el estado sea Ocupado.</summary>
        private Color ColorDe(EstadoLockerVista item)
        {
            if (item.Vencido)
                return ColorVencido;

            switch (item.Locker.Estado)
            {
                case EstadoLocker.Libre: return ColorLibre;
                case EstadoLocker.Reservado: return ColorReservado;
                case EstadoLocker.Ocupado: return ColorOcupado;
                default: return ColorFuera;
            }
        }

        private void MostrarDetalle(EstadoLockerVista item)
        {
            Locker l = item.Locker;
            string encabezado = "Locker " + l.Numero + " (" + l.GrupoLocker + ")  -  " +
                                l.Tamanio + "  -  " + l.Estado;

            if (item.OrdenActiva == null)
            {
                lblDetalle.ForeColor = Color.Black;
                lblDetalle.Text = encabezado;
                return;
            }

            OrdenDeEntrega o = item.OrdenActiva;
            string texto = encabezado + Environment.NewLine +
                           "Orden N° " + o.Id + "  -  " + o.ResidenteAsignado.NombreCompleto() +
                           "  (" + o.ResidenteAsignado.Piso + ")  -  paquete " + o.TamanioPaquete;

            if (o.Estado == EstadoOrden.Reservada)
            {
                texto += Environment.NewLine + "Reservado hace " +
                         Math.Round(o.HorasDesdeReserva()) + " h. " +
                         (item.ReservaExpirada
                            ? "El PIN de apertura ya venció: conviene liberar el locker."
                            : "Esperando al repartidor.");
            }
            else
            {
                int? dias = o.DiasRestantesParaRetiro();
                texto += Environment.NewLine + "Entregado el " +
                         o.FechaEntrega.Value.ToString("dd/MM/yyyy HH:mm") + ". " +
                         (item.Vencido
                            ? "VENCIDO hace " + Math.Abs(dias.Value) + " días."
                            : "Quedan " + dias + " días para el retiro.");
            }

            lblDetalle.ForeColor = (item.Vencido || item.ReservaExpirada) ? Color.Firebrick : Color.Black;
            lblDetalle.Text = texto;
        }

        private void MostrarLeyenda()
        {
            lblLeyenda.Text = "Verde: libre    Azul: reservado, esperando repartidor    " +
                              "Rojo: ocupado, esperando retiro    Ámbar: vencido (más de " +
                              OrdenDeEntrega.DiasParaRetirar + " días)    Gris: fuera de servicio";
        }
        public void Refrescar()
        {
            Recargar();
        }
    }
}

