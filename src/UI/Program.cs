using System;
using System.Windows.Forms;
using LockersInteligentes.BLL;

namespace LockersInteligentes.UI
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // El ciclo permite cerrar sesión y volver al login sin reiniciar la app.
            while (true)
            {
                using (FrmLogin login = new FrmLogin())
                {
                    if (login.ShowDialog() != DialogResult.OK)
                        return;
                }

                FrmPrincipal principal = new FrmPrincipal();
                Application.Run(principal);

                if (!principal.CerroSesion)
                    return;
            }
        }
    }
}