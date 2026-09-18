using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace LockersInteligentes.ArqBase.Servicios
{
    /// <summary>
    /// Envío de correo por SMTP (CU.Not.001). Vive en Arq. Base porque no toca la
    /// base de datos: solo habla con un servidor externo.
    ///
    /// Devuelve bool en lugar de lanzar excepción porque el diseño establece que un
    /// fallo de notificación NO revierte la operación: el ciclo del paquete sigue y
    /// la notificación queda registrada como fallida.
    /// </summary>
    public class EmailService
    {
        public bool Enviar(string destinatario, string asunto, string cuerpo, out string error)
        {
            error = null;

            if (string.IsNullOrWhiteSpace(destinatario))
            {
                error = "El destinatario está vacío.";
                return false;
            }

            try
            {
                string host = Leer("SmtpHost");
                int puerto = int.Parse(Leer("SmtpPuerto"));
                bool usarSsl = bool.Parse(Leer("SmtpUsarSsl"));
                string remitente = Leer("SmtpRemitente");
                string usuario = Leer("SmtpUsuario");
                string password = Leer("SmtpPassword");

                using (SmtpClient cliente = new SmtpClient(host, puerto))
                using (MailMessage mensaje = new MailMessage())
                {
                    cliente.EnableSsl = usarSsl;
                    cliente.UseDefaultCredentials = false;
                    cliente.Credentials = new NetworkCredential(usuario, password);

                    mensaje.From = new MailAddress(remitente, "Lockers Inteligentes");
                    mensaje.To.Add(destinatario);
                    mensaje.Subject = asunto;
                    mensaje.Body = cuerpo;
                    mensaje.IsBodyHtml = false;

                    cliente.Send(mensaje);
                }

                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        private string Leer(string clave)
        {
            string valor = ConfigurationManager.AppSettings[clave];

            if (string.IsNullOrWhiteSpace(valor))
                throw new ConfigurationErrorsException(
                    "Falta la clave '" + clave + "'. Revisá el App.config y el secrets.config.");

            return valor;
        }
    }
}