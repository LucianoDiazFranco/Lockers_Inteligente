using LockersInteligentes.Dominio.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockersInteligentes.Dominio.Excepciones
{
    public class PinInvalidoException  : ExcepcionNegocio
    {
        public enum Motivo
        {
            Incorrecto,
            Vencido,
            YaUtilizado,
            NoExiste
        }

        public Motivo MotivoFalla { get; private set; }
        public TipoCodigo Tipo { get; private set; }

        public PinInvalidoException(Motivo motivo, TipoCodigo tipo, string mensaje)
            : base(mensaje)
        {
            MotivoFalla = motivo;
            Tipo = tipo;
        }
    }
}
