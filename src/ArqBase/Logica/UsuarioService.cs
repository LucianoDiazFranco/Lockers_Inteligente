using LockersInteligentes.ArqBase.DataAccess;
using LockersInteligentes.ArqBase.Entidades;
using LockersInteligentes.ArqBase.Enums;
using LockersInteligentes.ArqBase.Servicios;
using LockersInteligentes.ArqBase.DataAccess;
using System;
using System.Collections.Generic;

namespace LockersInteligentes.ArqBase.Logica
{
    public class UsuarioService
    {
        private readonly HistorialService _historial = new HistorialService();
        private const int LongitudMinimaPassword = 8;
        public IList<Usuario> Listar()
        {
            GestorSesion.Instancia.ValidarRolAdministrador();
            return RepositorioFactoryArq.Instancia.Usuarios.ObtenerTodos();
        }
        public IList<Rol> ListarRoles()
        {
            return RepositorioFactoryArq.Instancia.Roles.ObtenerTodos();
        }
        public Usuario Crear(string nombreUsuario, string nombre, string apellido,
                             string password, int idRol)
        {
            GestorSesion.Instancia.ValidarRolAdministrador();

            ValidarDatos(nombreUsuario, nombre, apellido, idRol);
            ValidarPassword(password);

            if (RepositorioFactoryArq.Instancia.Usuarios.ExisteNombreUsuario(nombreUsuario, 0))
                throw new InvalidOperationException(
                    "Ya existe un usuario con el nombre '" + nombreUsuario + "'.");

            Usuario usuario = new Usuario();
            usuario.NombreUsuario = nombreUsuario.Trim();
            usuario.Nombre = nombre.Trim();
            usuario.Apellido = apellido.Trim();
            usuario.Rol = RepositorioFactoryArq.Instancia.Roles.ObtenerPorId(idRol);
            usuario.Activo = true;

            AsignarPassword(usuario, password);

            RepositorioFactoryArq.Instancia.Usuarios.Insertar(usuario);

            _historial.Grabar(TipoOperacion.Alta,
                "Alta del usuario '" + usuario.NombreUsuario + "' con rol " + usuario.Rol.Nombre);


            return usuario;
        }

        public void Modificar(Usuario usuario)
        {
            GestorSesion.Instancia.ValidarRolAdministrador();

            if (usuario == null)
                throw new ArgumentNullException("usuario");

            ValidarDatos(usuario.NombreUsuario, usuario.Nombre, usuario.Apellido,
                         usuario.Rol != null ? usuario.Rol.Id : 0);

            if (RepositorioFactoryArq.Instancia.Usuarios.ExisteNombreUsuario(usuario.NombreUsuario, usuario.Id))
                throw new InvalidOperationException(
                    "Ya existe otro usuario con el nombre '" + usuario.NombreUsuario + "'.");

            RepositorioFactoryArq.Instancia.Usuarios.Actualizar(usuario);
            _historial.Grabar(TipoOperacion.Modificacion,
                "Modificación del usuario '" + usuario.NombreUsuario + "'");

        }
        public void Desactivar(int idUsuario)
        {
            GestorSesion.Instancia.ValidarRolAdministrador();

            if (GestorSesion.Instancia.UsuarioActual.Id == idUsuario)
                throw new InvalidOperationException("No podés desactivar tu propio usuario.");

            Usuario usuario = RepositorioFactoryArq.Instancia.Usuarios.ObtenerPorId(idUsuario);

            if (usuario == null)
                throw new InvalidOperationException("El usuario no existe.");

            if (usuario.EsAdministrador() &&
                RepositorioFactoryArq.Instancia.Usuarios.ContarAdministradoresActivos() <= 1)
                throw new InvalidOperationException(
                    "No se puede desactivar al último administrador activo del sistema.");

            RepositorioFactoryArq.Instancia.Usuarios.Eliminar(idUsuario);

            _historial.Grabar(TipoOperacion.Baja,
                "Baja del usuario '" + usuario.NombreUsuario + "'");

        }
        public void RestablecerPassword(int idUsuario, string passwordNueva)
        {
            GestorSesion.Instancia.ValidarRolAdministrador();
            ValidarPassword(passwordNueva);

            Usuario usuario = RepositorioFactoryArq.Instancia.Usuarios.ObtenerPorId(idUsuario);

            if (usuario == null)
                throw new InvalidOperationException("El usuario no existe.");

            AsignarPassword(usuario, passwordNueva);
            RepositorioFactoryArq.Instancia.Usuarios.Actualizar(usuario);

            _historial.Grabar(TipoOperacion.Modificacion,
                "Restablecimiento de contraseña del usuario '" + usuario.NombreUsuario + "'");

        }
        public void CambiarPasswordPropia(string passwordActual, string passwordNueva)
        {
            Usuario usuario = GestorSesion.Instancia.UsuarioActual;

            if (usuario == null)
                throw new InvalidOperationException("No hay una sesión activa.");

            if (!EncriptadorService.Verificar(passwordActual, usuario.PasswordSalt, usuario.PasswordHash))
                throw new InvalidOperationException("La contraseña actual es incorrecta.");

            ValidarPassword(passwordNueva);

            AsignarPassword(usuario, passwordNueva);
            RepositorioFactoryArq.Instancia.Usuarios.Actualizar(usuario);

            _historial.Grabar(TipoOperacion.Modificacion,
             "Cambio de contraseña propia");

        }
        private void AsignarPassword(Usuario usuario, string password)
        {
            usuario.PasswordSalt = EncriptadorService.GenerarSalt();
            usuario.PasswordHash = EncriptadorService.Encriptar(password, usuario.PasswordSalt);
        }
        private void ValidarDatosBasicos(string nombreUsuario, string nombre, string apellido)
        {
            if (string.IsNullOrWhiteSpace(nombreUsuario))
                throw new InvalidOperationException("El nombre de usuario es obligatorio.");

            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellido))
                throw new InvalidOperationException("Nombre y apellido son obligatorios.");
        }
        private void ValidarDatos(string nombreUsuario, string nombre, string apellido, int idRol)
        {
            ValidarDatosBasicos(nombreUsuario, nombre, apellido);

            if (idRol <= 0)
                throw new InvalidOperationException("Debe seleccionar un rol.");
        }
        private void ValidarPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password) || password.Length < LongitudMinimaPassword)
                throw new InvalidOperationException(
                    "La contraseña debe tener al menos " + LongitudMinimaPassword + " caracteres.");

            bool tieneLetra = false;
            bool tieneNumero = false;

            foreach (char c in password)
            {
                if (char.IsLetter(c)) tieneLetra = true;
                if (char.IsDigit(c)) tieneNumero = true;
            }

            if (!tieneLetra || !tieneNumero)
                throw new InvalidOperationException(
                    "La contraseña debe combinar letras y números.");
        }
        public Usuario RegistrarOperador(string nombreUsuario, string nombre,
                                         string apellido, string correo, string password)
        {
            ValidarDatosBasicos(nombreUsuario, nombre, apellido);
            ValidarPassword(password);

            if (string.IsNullOrWhiteSpace(correo) || !correo.Contains("@"))
                throw new InvalidOperationException("Ingresá un correo electrónico válido.");

            if (RepositorioFactoryArq.Instancia.Usuarios.ExisteNombreUsuario(nombreUsuario, 0))
                throw new InvalidOperationException(
                    "Ya existe un usuario con el nombre '" + nombreUsuario + "'.");

            Rol rolOperador = RepositorioFactoryArq.Instancia.Roles
                                                .ObtenerPorNombre(Rol.NombreOperador);

            if (rolOperador == null)
                throw new InvalidOperationException(
                    "No está configurado el rol Operador en la base de datos.");

            Usuario usuario = new Usuario();
            usuario.NombreUsuario = nombreUsuario.Trim();
            usuario.Nombre = nombre.Trim();
            usuario.Apellido = apellido.Trim();
            usuario.Correo = correo.Trim();
            usuario.Rol = rolOperador;
            usuario.Activo = true;

            AsignarPassword(usuario, password);

            RepositorioFactoryArq.Instancia.Usuarios.Insertar(usuario);

            _historial.Grabar(TipoOperacion.Alta,
               "Autorregistro del operador '" + usuario.NombreUsuario + "'");


            return usuario;
        }
    }
}