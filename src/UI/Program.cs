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

            DiagnosticoService diagnostico = new DiagnosticoService();
            string error;

            if (diagnostico.ProbarConexion(out error))
            {
                MessageBox.Show(
                    "Conexion a la base establecida correctamente.",
                    "Lockers Inteligentes",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(
                    "No se pudo conectar a la base de datos:" + Environment.NewLine + Environment.NewLine + error,
                    "Lockers Inteligentes",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}