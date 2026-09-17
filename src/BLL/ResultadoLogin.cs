using LockersInteligentes.Dominio.Entidades;

namespace LockersInteligentes.BLL
{
    public class ResultadoLogin
    {
        public bool Exitoso { get; set; }
        public string Mensaje { get; set; }
        public Usuario Usuario { get; set; }
    }
}