using LockersInteligentes.ArqBase.Entidades;

namespace LockersInteligentes.ArqBase.Logica
{
    public class ResultadoLogin
    {
        public bool Exitoso { get; set; }
        public string Mensaje { get; set; }
        public Usuario Usuario { get; set; }
    }
}