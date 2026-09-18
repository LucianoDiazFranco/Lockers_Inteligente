using System;
using System.Security.Cryptography;
using LockersInteligentes.ArqBase.Servicios;
using LockersInteligentes.Dominio.Entidades;
using LockersInteligentes.Dominio.Enums;

namespace LockersInteligentes.BLL
{
    public static class CodigoAccesoFactory
    {
        private const int CantidadDigitos = 6;

        public class ResultadoPin
        {
            public string PinEnClaro { get; set; }
            public CodigoAcceso Codigo { get; set; }
        }

        public static ResultadoPin Generar(OrdenDeEntrega orden, TipoCodigo tipo)
        {
            if (orden == null)
                throw new ArgumentNullException("orden");

            string pin = GenerarPinNumerico();
            string salt = EncriptadorService.GenerarSalt();

            CodigoAcceso codigo = new CodigoAcceso();
            codigo.OrdenDeEntrega = orden;
            codigo.TipoCodigo = tipo;
            codigo.PinSalt = salt;
            codigo.PinHash = EncriptadorService.Encriptar(pin, salt);
            codigo.FechaGeneracion = DateTime.Now;
            codigo.Usado = false;

            ResultadoPin resultado = new ResultadoPin();
            resultado.PinEnClaro = pin;
            resultado.Codigo = codigo;

            return resultado;
        }

        /// PIN numérico de 6 dígitos con generador criptográfico.
        ///
        /// Se usa RandomNumberGenerator y no Random 

        private static string GenerarPinNumerico()
        {
            int maximo = (int)Math.Pow(10, CantidadDigitos);   // 1000000
            int valor = RandomNumberGenerator.GetInt32(0, maximo);

            return valor.ToString("D" + CantidadDigitos);      // rellena con ceros a la izquierda
        }
    }
}