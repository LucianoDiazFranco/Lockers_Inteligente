using LockersInteligentes.ArqBase.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockersInteligentes.ArqBase.Servicios
{
    public static class IntegridadService
    {
        private const string Separador = "|";

        /// <summary>
        /// Calcula el DVH de un usuario a partir de los campos que importa
        /// proteger: identidad, credenciales, rol y estado. Se dejan afuera los
        /// datos de contacto, cuya alteración no compromete la seguridad.
        /// </summary>
        public static string CalcularDVH(Usuario usuario)
        {
            if (usuario == null)
                throw new ArgumentNullException("usuario");

            string cadena = string.Join(Separador,
                usuario.Id.ToString(),
                usuario.NombreUsuario,
                usuario.PasswordHash,
                usuario.PasswordSalt,
                usuario.Rol != null ? usuario.Rol.Id.ToString() : "0",
                usuario.Activo ? "1" : "0");

            // Se reutiliza el encriptador con una semilla fija. No es un secreto:
            // el DVH no oculta nada, solo detecta cambios.
            return EncriptadorService.Encriptar(cadena, "DVH");
        }

        /// <summary>
        /// Verifica un usuario. Devuelve false si el dígito guardado no coincide
        /// con el recalculado, lo que indica que la fila fue modificada fuera
        /// del sistema.
        /// </summary>
        public static bool EsIntegro(Usuario usuario, string dvhGuardado)
        {
            if (string.IsNullOrWhiteSpace(dvhGuardado))
                return false;

            return string.Equals(CalcularDVH(usuario), dvhGuardado, StringComparison.Ordinal);
        }
    }
}
