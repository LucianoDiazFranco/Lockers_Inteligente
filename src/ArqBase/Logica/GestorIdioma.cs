using LockersInteligentes.ArqBase.DataAccess;
using LockersInteligentes.ArqBase.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LockersInteligentes.ArqBase.Logica
{
    public sealed class GestorIdioma
    {
        private static readonly GestorIdioma _instancia = new GestorIdioma();

        private readonly List<IObservadorIdioma> _observadores = new List<IObservadorIdioma>();
        private readonly RepositorioIdioma _repoIdioma = new RepositorioIdioma();
        private readonly RepositorioTraduccion _repoTraduccion = new RepositorioTraduccion();

        private IDictionary<string, string> _traducciones = new Dictionary<string, string>();

        private GestorIdioma()
        {
        }

        public static GestorIdioma Instancia
        {
            get { return _instancia; }
        }

        public Idioma IdiomaActual { get; private set; }

        /// <summary>
        /// Carga el idioma predeterminado. Se llama una vez al arrancar, antes de
        /// mostrar la primera pantalla.
        /// </summary>
        public void Inicializar()
        {
            try
            {
                Idioma predeterminado = _repoIdioma.ObtenerPredeterminado();

                if (predeterminado != null)
                    CargarIdioma(predeterminado);
            }
            catch (Exception ex)
            {
                // Si falla, el sistema sigue mostrando los textos por defecto.
                System.Diagnostics.Debug.WriteLine("Fallo al cargar idioma: " + ex.Message);
            }
        }

        public IList<Idioma> ListarIdiomas()
        {
            return _repoIdioma.ObtenerTodos();
        }

        /// <summary>Cambia el idioma activo y notifica a todos los observadores.</summary>
        public void CambiarIdioma(string codigo)
        {
            Idioma idioma = _repoIdioma.ObtenerPorCodigo(codigo);

            if (idioma == null)
                throw new InvalidOperationException("No existe el idioma '" + codigo + "'.");

            CargarIdioma(idioma);
            Notificar();
        }

        private void CargarIdioma(Idioma idioma)
        {
            IdiomaActual = idioma;
            _traducciones = _repoTraduccion.ObtenerDiccionario(idioma.Id);
        }

        /// <summary>
        /// Devuelve el texto de una clave. Si no existe la traducción, devuelve la
        /// clave entre corchetes en lugar de un texto vacío: así, una clave faltante
        /// se ve en pantalla y se detecta enseguida, en vez de pasar desapercibida.
        /// </summary>
        public string Traducir(string clave)
        {
            if (string.IsNullOrWhiteSpace(clave))
                return string.Empty;

            string texto;
            return _traducciones.TryGetValue(clave, out texto) ? texto : "[" + clave + "]";
        }

        public void Suscribir(IObservadorIdioma observador)
        {
            if (observador != null && !_observadores.Contains(observador))
                _observadores.Add(observador);
        }

        /// <summary>
        /// Desuscribe al observador. Es IMPRESCINDIBLE llamarlo al cerrar un
        /// formulario: si no, el gestor conserva una referencia a una ventana
        /// destruida, el recolector de basura no puede liberarla, y al cambiar el
        /// idioma se intentaría actualizar un formulario que ya no existe.
        /// </summary>
        public void Desuscribir(IObservadorIdioma observador)
        {
            _observadores.Remove(observador);
        }

        /// <summary>
        /// Avisa a todos. Se recorre una copia de la lista porque un observador
        /// podría desuscribirse durante la notificación, y modificar una colección
        /// mientras se la recorre lanza excepción.
        /// </summary>
        private void Notificar()
        {
            foreach (IObservadorIdioma observador in _observadores.ToList())
            {
                try
                {
                    observador.ActualizarIdioma();
                }
                catch (Exception ex)
                {
                    // Un formulario que falle al traducirse no debe impedir que
                    // los demás se actualicen.
                    System.Diagnostics.Debug.WriteLine("Fallo al traducir: " + ex.Message);
                }
            }
        }
        public string TraducirEnum(string prefijo, object valor)
        {
            if (valor == null)
                return string.Empty;

            return Traducir("Enum." + prefijo + "." + valor.ToString());
        }


    }
}
