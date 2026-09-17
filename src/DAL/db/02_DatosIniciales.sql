USE LockersInteligentes;

INSERT INTO dbo.Rol (Nombre, Descripcion) VALUES
    (N'Administrador', N'Acceso total: ABM, reportes, bitacora y operacion de paquetes.'),
    (N'Operador',      N'Solo el ciclo del paquete: reservar, registrar entrega y registrar retiro.');

INSERT INTO dbo.Idioma (Nombre, Codigo, EsPredeterminado) VALUES
    (N'Español (Argentina)',     N'es-AR', 1),
    (N'English (United States)', N'en-US', 0);

DECLARE @es INT = (SELECT NumIdioma FROM dbo.Idioma WHERE Codigo = N'es-AR');
DECLARE @en INT = (SELECT NumIdioma FROM dbo.Idioma WHERE Codigo = N'en-US');

INSERT INTO dbo.Traduccion (IdIdioma, Clave, Texto) VALUES
    (@es, N'FrmLogin.Titulo',         N'Iniciar sesión'),
    (@es, N'FrmLogin.lblUsuario',     N'Usuario'),
    (@es, N'FrmLogin.lblContrasenia', N'Contraseña'),
    (@es, N'FrmLogin.btnIngresar',    N'Ingresar'),
    (@en, N'FrmLogin.Titulo',         N'Sign in'),
    (@en, N'FrmLogin.lblUsuario',     N'User'),
    (@en, N'FrmLogin.lblContrasenia', N'Password'),
    (@en, N'FrmLogin.btnIngresar',    N'Sign in');