namespace LockersInteligentes.Dominio.Entidades
{

    public abstract class EntidadBase
    {
        public int Id { get; set; }

        public bool EsNueva()
        {
            return Id == 0;
        }
    }
}
