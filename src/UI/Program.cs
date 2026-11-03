using LockersInteligentes.ArqBase.ArqBase;
using LockersInteligentes.ArqBase.Servicios;
using LockersInteligentes.ArqBase.Logica;
using System;
using System.Windows.Forms;

namespace LockersInteligentes.UI
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.ThreadException += (s, args) =>
            {
                ErrorLogService.Registrar(args.Exception, "Application.ThreadException",
                    GestorSesion.Instancia.ActorParaBitacora());

                MessageBox.Show(
                    "Ocurrió un error inesperado. Quedó registrado en el log de errores.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };

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