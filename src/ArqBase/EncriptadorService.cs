using System;
using System.Security.Cryptography;
using System.Text;

namespace LockersInteligentes.ArqBase.Servicios
{
    public static class EncriptadorService
    {
        private const int TamanioSaltEnBytes = 16;

        public static string GenerarSalt()
        {
            byte[] salt = RandomNumberGenerator.GetBytes(TamanioSaltEnBytes);
            return Convert.ToBase64String(salt);
        }
        public static string Encriptar(string texto, string salt)
        {
            if (string.IsNullOrEmpty(texto))
                throw new ArgumentException("El texto a encriptar no puede estar vacio.", "texto");

            if (string.IsNullOrEmpty(salt))
                throw new ArgumentException("El salt no puede estar vacio.", "salt");

            byte[] datos = Encoding.UTF8.GetBytes(salt + texto);
            byte[] hash = SHA256.HashData(datos);

            return Convert.ToBase64String(hash);
        }
        public static bool Verificar(string textoIngresado, string salt, string hashGuardado)
        {
            if (string.IsNullOrEmpty(textoIngresado) ||
                string.IsNullOrEmpty(salt) ||
                string.IsNullOrEmpty(hashGuardado))
                return false;

            string hashCalculado = Encriptar(textoIngresado, salt);

            byte[] calculado = Encoding.UTF8.GetBytes(hashCalculado);
            byte[] guardado = Encoding.UTF8.GetBytes(hashGuardado);

            return CryptographicOperations.FixedTimeEquals(calculado, guardado);
        }
    }
}