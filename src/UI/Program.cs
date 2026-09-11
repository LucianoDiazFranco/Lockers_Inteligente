using System;
using System.Windows.Forms;

namespace LockersInteligentes.UI
{
    /// <summary>
    /// Punto de entrada de la aplicacion de escritorio.
    /// Por ahora es un placeholder: existe solo para que la solucion compile
    /// (un proyecto WinExe necesita un Main). Cuando armemos la capa UI se
    /// reemplaza por el arranque real: FrmLogin y luego FrmPrincipal (MDI).
    /// </summary>
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MessageBox.Show(
                "Capa UI pendiente. Por el momento solo esta implementada la capa de Dominio.",
                "Lockers Inteligentes",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
    }
}
