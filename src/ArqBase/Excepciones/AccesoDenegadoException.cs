using LockersInteligentes.ArqBase.Excepciones;


namespace LockersInteligentes.ArqBaseExcepciones
{
    public class AccesoDenegadoException : ExcepcionNegocio
    {
        public AccesoDenegadoException(string mensaje) : base(mensaje)
        {
        }
    }
}
