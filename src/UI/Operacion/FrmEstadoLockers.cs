using LockersInteligentes.ArqBase.Logica;
using LockersInteligentes.BLL;
using LockersInteligentes.Dominio.Entidades;
using LockersInteligentes.Dominio.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace LockersInteligentes.UI
{
    /// <summary>
    /// Panel operativo con el estado de los lockers de un edificio.
    /// Los controles fijos están en el Designer; los casilleros se crean por código
    /// porque su cantidad depende de cuántos lockers tenga el edificio.
    /// </summary>
    public partial class FrmEstadoLockers : Form, IObservadorIdioma
    {
        private static readonly Color ColorLibre = Color.FromArgb(46, 160, 67);
        private static readonly Color ColorReservado = Color.FromArgb(47, 109, 199);
        private static readonly Color ColorOcupado = Color.FromArgb(200, 55, 55);
        private static readonly Color ColorVencido = Color.FromArgb(222, 160, 32);
        private static readonly Color ColorFuera = Color.FromArgb(110, 118, 129);

        private readonly SistemaLockersFacade _sistema = SistemaLockersFacade.Instancia;
        private IList<EstadoLockerVista> _vista = new List<EstadoLockerVista>();

        public FrmEstadoLockers()
        {
            InitializeComponent();
            GestorIdioma.Instancia.Suscribir(this);
        }

        private void FrmEstadoLockers_Load(object sender, EventArgs e)
        {
            try
            {
                cboEdificio.DisplayMember = "Nombre";
                cboEdificio.ValueMember = "Id";
                cboEdificio.DataSource = _sistema.EdificiosParaPanel();

                ActualizarIdioma();
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

            List<string> sectores = new List<string>
            {
                GestorIdioma.Instancia.Traducir("Comun.Todos")
            };

            sectores.AddRange(_sistema.SectoresDeEdificio(
                Convert.ToInt32(cboEdificio.SelectedValue)));

            cboSector.DataSource = sectores;
        }

        /// <summary>Va a la base y redibuja todo.</summary>
        private void Recargar()
        {
            if (cboEdificio.SelectedValue == null)
                return;

            try
            {
                _vista = _sistema.EstadoDeLockers(Convert.ToInt32(cboEdificio.SelectedValue));
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
            ResumenLockers r = _sistema.ResumirLockers(_vista);
            GestorIdioma idioma = GestorIdioma.Instancia;

            lblResumen.Text = string.Format(idioma.Traducir("FrmEstado.Resumen"),
                r.Total, r.Libres, r.Reservados, r.Ocupados,
                r.FueraDeServicio, r.PorcentajeOcupacion());

            if (r.Vencidos > 0)
                lblResumen.Text += string.Format(
                    idioma.Traducir("FrmEstado.Vencidos"), r.Vencidos);
        }

        /// <summary>Redibuja los casilleros sin volver a consultar la base.</summary>
        private void Dibujar()
        {
            panelLockers.SuspendLayout();
            panelLockers.Controls.Clear();
            IEnumerable<EstadoLockerVista> filtrada = _vista;

            if (cboSector.SelectedIndex > 0)
            {
                string sector = cboSector.SelectedItem as string;
                filtrada = filtrada.Where(v => (v.Locker.GrupoLocker ?? "(sin sector)") == sector);
            }

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
            tamanio.Text = GestorIdioma.Instancia.TraducirEnum("Tamanio", item.Locker.Tamanio);
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
            GestorIdioma idioma = GestorIdioma.Instancia;

            string encabezado = "Locker " + l.Numero + " (" + l.GrupoLocker + ")  -  " +
                                idioma.TraducirEnum("Tamanio", l.Tamanio) + "  -  " +
                                idioma.TraducirEnum("EstadoLocker", l.Estado);

            if (item.OrdenActiva == null)
            {
                lblDetalle.ForeColor = Color.Black;
                lblDetalle.Text = encabezado;
                return;
            }

            OrdenDeEntrega o = item.OrdenActiva;

            string texto = encabezado + Environment.NewLine +
                string.Format(idioma.Traducir("Det.Orden"),
                    o.Id,
                    o.ResidenteAsignado.NombreCompleto(),
                    o.ResidenteAsignado.Piso,
                    idioma.TraducirEnum("Tamanio", o.TamanioPaquete));

            if (o.Estado == EstadoOrden.Reservada)
            {
                texto += Environment.NewLine +
                    string.Format(idioma.Traducir("Det.Reservado"),
                                  Math.Round(o.HorasDesdeReserva())) +
                    (item.ReservaExpirada
                        ? idioma.Traducir("Det.PinVencido")
                        : idioma.Traducir("Det.Esperando"));
            }
            else
            {
                int? dias = o.DiasRestantesParaRetiro();

                texto += Environment.NewLine +
                    string.Format(idioma.Traducir("Det.EntregadoEl"),
                                  o.FechaEntrega.Value.ToString("dd/MM/yyyy HH:mm")) +
                    (item.Vencido
                        ? string.Format(idioma.Traducir("Det.VencidoDias"), Math.Abs(dias.Value))
                        : string.Format(idioma.Traducir("Det.QuedanDias"), dias));
            }

            lblDetalle.ForeColor = (item.Vencido || item.ReservaExpirada)
                ? Color.Firebrick : Color.Black;
            lblDetalle.Text = texto;
        }

        private void MostrarLeyenda()
        {
            lblLeyenda.Text = string.Format(
                GestorIdioma.Instancia.Traducir("FrmEstado.Leyenda"),
                OrdenDeEntrega.DiasParaRetirar);
        }
        public void Refrescar()
        {
            Recargar();
        }

        public void ActualizarIdioma()
        {
            GestorIdioma idioma = GestorIdioma.Instancia;

            Text = idioma.Traducir("FrmEstado.Caption");
            lblEdificio.Text = idioma.Traducir("Comun.Edificio");
            lblSector.Text = idioma.Traducir("Comun.Sector");
            btnActualizar.Text = idioma.Traducir("Comun.Actualizar");
            lblDetalle.Text = idioma.Traducir("FrmEstado.Detalle");

            MostrarLeyenda();
            Recargar();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            GestorIdioma.Instancia.Desuscribir(this);
            base.OnFormClosed(e);
        }
    }
}