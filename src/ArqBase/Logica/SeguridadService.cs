using LockersInteligentes.ArqBase.Servicios;


namespace LockersInteligentes.ArqBase.Logica
{
    public class SeguridadService
    {
        public void GenerarCredenciales(string password, out string salt, out string hash)
        {
            salt = EncriptadorService.GenerarSalt();
            hash = EncriptadorService.Encriptar(password, salt);
        }
    }
}
