/* =====================================================================
   Traducciones del sistema (T05 Multi-idioma, patrón Observer).
   Convención de claves: Formulario.Control
   Las claves que empiezan con "Comun." se usan en varias pantallas.
   Re-ejecutable: no duplica claves ya cargadas.
   ===================================================================== */

USE LockersInteligentes;
GO

DECLARE @es INT = (SELECT NumIdioma FROM dbo.Idioma WHERE Codigo = N'es-AR');
DECLARE @en INT = (SELECT NumIdioma FROM dbo.Idioma WHERE Codigo = N'en-US');

INSERT INTO dbo.Traduccion (IdIdioma, Clave, Texto)
SELECT v.IdIdioma, v.Clave, v.Texto
FROM (VALUES
    /* ---------------------- Textos comunes ---------------------- */
    (@es, N'Comun.Nuevo',        N'Nuevo'),
    (@en, N'Comun.Nuevo',        N'New'),
    (@es, N'Comun.Guardar',      N'Guardar'),
    (@en, N'Comun.Guardar',      N'Save'),
    (@es, N'Comun.Eliminar',     N'Eliminar'),
    (@en, N'Comun.Eliminar',     N'Delete'),
    (@es, N'Comun.Cancelar',     N'Cancelar'),
    (@en, N'Comun.Cancelar',     N'Cancel'),
    (@es, N'Comun.Actualizar',   N'Actualizar'),
    (@en, N'Comun.Actualizar',   N'Refresh'),
    (@es, N'Comun.Limpiar',      N'Limpiar'),
    (@en, N'Comun.Limpiar',      N'Clear'),
    (@es, N'Comun.Buscar',       N'Buscar'),
    (@en, N'Comun.Buscar',       N'Search'),
    (@es, N'Comun.Volver',       N'Volver'),
    (@en, N'Comun.Volver',       N'Back'),
    (@es, N'Comun.DarDeBaja',    N'Dar de baja'),
    (@en, N'Comun.DarDeBaja',    N'Deactivate'),
    (@es, N'Comun.Reactivar',    N'Reactivar'),
    (@en, N'Comun.Reactivar',    N'Reactivate'),
    (@es, N'Comun.Nombre',       N'Nombre'),
    (@en, N'Comun.Nombre',       N'First name'),
    (@es, N'Comun.Apellido',     N'Apellido'),
    (@en, N'Comun.Apellido',     N'Last name'),
    (@es, N'Comun.Dni',          N'DNI'),
    (@en, N'Comun.Dni',          N'ID number'),
    (@es, N'Comun.Correo',       N'Correo electrónico'),
    (@en, N'Comun.Correo',       N'Email'),
    (@es, N'Comun.Telefono',     N'Teléfono'),
    (@en, N'Comun.Telefono',     N'Phone'),
    (@es, N'Comun.Usuario',      N'Usuario'),
    (@en, N'Comun.Usuario',      N'User'),
    (@es, N'Comun.Contrasenia',  N'Contraseña'),
    (@en, N'Comun.Contrasenia',  N'Password'),
    (@es, N'Comun.Rol',          N'Rol'),
    (@en, N'Comun.Rol',          N'Role'),
    (@es, N'Comun.Estado',       N'Estado'),
    (@en, N'Comun.Estado',       N'Status'),
    (@es, N'Comun.Edificio',     N'Edificio'),
    (@en, N'Comun.Edificio',     N'Building'),
    (@es, N'Comun.Activo',       N'Activo'),
    (@en, N'Comun.Activo',       N'Active'),
    (@es, N'Comun.Baja',         N'Baja'),
    (@en, N'Comun.Baja',         N'Inactive'),
    (@es, N'Comun.Confirmar',    N'Confirmar'),
    (@en, N'Comun.Confirmar',    N'Confirm'),
    (@es, N'Comun.Seleccione',   N'Seleccioná un registro de la lista.'),
    (@en, N'Comun.Seleccione',   N'Select a record from the list.'),

    /* --------------------------- FrmLogin --------------------------- */
    (@es, N'FrmLogin.Caption',       N'Ingreso al sistema'),
    (@en, N'FrmLogin.Caption',       N'System login'),
    (@es, N'FrmLogin.Subtitulo',     N'Ingresá con tu usuario del sistema'),
    (@en, N'FrmLogin.Subtitulo',     N'Sign in with your system account'),
    (@es, N'FrmLogin.chkMostrar',    N'Mostrar contraseña'),
    (@en, N'FrmLogin.chkMostrar',    N'Show password'),
    (@es, N'FrmLogin.lnkRegistrar',  N'Crear una cuenta'),
    (@en, N'FrmLogin.lnkRegistrar',  N'Create an account'),

    /* ------------------------- FrmRegistro ------------------------- */
    (@es, N'FrmRegistro.Caption',    N'Registro de operador'),
    (@en, N'FrmRegistro.Caption',    N'Operator registration'),
    (@es, N'FrmRegistro.Titulo',     N'Crear una cuenta'),
    (@en, N'FrmRegistro.Titulo',     N'Create an account'),
    (@es, N'FrmRegistro.Subtitulo',  N'Se creará con rol Operador.'),
    (@en, N'FrmRegistro.Subtitulo',  N'It will be created with the Operator role.'),
    (@es, N'FrmRegistro.lblUsuario', N'Nombre de usuario'),
    (@en, N'FrmRegistro.lblUsuario', N'Username'),
    (@es, N'FrmRegistro.lblRepetir', N'Repetir contraseña'),
    (@en, N'FrmRegistro.lblRepetir', N'Repeat password'),
    (@es, N'FrmRegistro.Ayuda',      N'Mínimo 8 caracteres, combinando letras y números.'),
    (@en, N'FrmRegistro.Ayuda',      N'At least 8 characters, combining letters and numbers.'),
    (@es, N'FrmRegistro.btnCrear',   N'Crear cuenta'),
    (@en, N'FrmRegistro.btnCrear',   N'Create account'),

    /* ------------------------- FrmUsuarios ------------------------- */
    (@es, N'FrmUsuarios.Caption',    N'Gestión de usuarios'),
    (@en, N'FrmUsuarios.Caption',    N'User management'),
    (@es, N'FrmUsuarios.Ayuda',      N'Al modificar, dejá la contraseña vacía para no cambiarla.'),
    (@en, N'FrmUsuarios.Ayuda',      N'When editing, leave the password empty to keep it.'),
    (@es, N'FrmUsuarios.btnDesact',  N'Desactivar'),
    (@en, N'FrmUsuarios.btnDesact',  N'Deactivate'),
    (@es, N'FrmUsuarios.colNombre',  N'Nombre'),
    (@en, N'FrmUsuarios.colNombre',  N'Name'),

    /* ------------------------- FrmPermisos ------------------------- */
    (@es, N'FrmPermisos.Caption',    N'Permisos'),
    (@en, N'FrmPermisos.Caption',    N'Permissions'),
    (@es, N'FrmPermisos.Titulo',     N'Permisos del sistema'),
    (@en, N'FrmPermisos.Titulo',     N'System permissions'),
    (@es, N'FrmPermisos.Ayuda',      N'Las familias agrupan patentes y otras familias. Un usuario puede recibir cualquiera de las dos.'),
    (@en, N'FrmPermisos.Ayuda',      N'Families group permissions and other families. A user can receive either one.'),
    (@es, N'FrmPermisos.grpComp',    N'Familia o patente'),
    (@en, N'FrmPermisos.grpComp',    N'Family or permission'),
    (@es, N'FrmPermisos.lblTipo',    N'Tipo'),
    (@en, N'FrmPermisos.lblTipo',    N'Type'),
    (@es, N'FrmPermisos.lblDesc',    N'Descripción'),
    (@en, N'FrmPermisos.lblDesc',    N'Description'),
    (@es, N'FrmPermisos.btnEditar',  N'Editar'),
    (@en, N'FrmPermisos.btnEditar',  N'Edit'),
    (@es, N'FrmPermisos.grpAnidar',  N'Anidar el seleccionado dentro de una familia'),
    (@en, N'FrmPermisos.grpAnidar',  N'Nest the selected item inside a family'),
    (@es, N'FrmPermisos.lblDestino', N'Familia destino'),
    (@en, N'FrmPermisos.lblDestino', N'Target family'),
    (@es, N'FrmPermisos.btnAgregar', N'Agregar'),
    (@en, N'FrmPermisos.btnAgregar', N'Add'),
    (@es, N'FrmPermisos.btnQuitarF', N'Quitar el seleccionado de su familia'),
    (@en, N'FrmPermisos.btnQuitarF', N'Remove selected from its family'),
    (@es, N'FrmPermisos.grpUsuario', N'Asignar a usuario'),
    (@en, N'FrmPermisos.grpUsuario', N'Assign to user'),
    (@es, N'FrmPermisos.btnAsignar', N'Asignar el seleccionado del árbol'),
    (@en, N'FrmPermisos.btnAsignar', N'Assign selected from tree'),
    (@es, N'FrmPermisos.btnQuitarU', N'Quitar el de la lista'),
    (@en, N'FrmPermisos.btnQuitarU', N'Remove from list'),

    /* ------------------------ FrmPrincipal ------------------------ */
    (@es, N'Menu.Archivo',        N'&Archivo'),
    (@en, N'Menu.Archivo',        N'&File'),
    (@es, N'Menu.CerrarSesion',   N'Cerrar sesión'),
    (@en, N'Menu.CerrarSesion',   N'Sign out'),
    (@es, N'Menu.Salir',          N'Salir'),
    (@en, N'Menu.Salir',          N'Exit'),
    (@es, N'Menu.Seguridad',      N'&Seguridad'),
    (@en, N'Menu.Seguridad',      N'&Security'),
    (@es, N'Menu.Usuarios',       N'Usuarios'),
    (@en, N'Menu.Usuarios',       N'Users'),
    (@es, N'Menu.Permisos',       N'Permisos'),
    (@en, N'Menu.Permisos',       N'Permissions'),
    (@es, N'Menu.Gestion',        N'&Gestión'),
    (@en, N'Menu.Gestion',        N'&Management'),
    (@es, N'Menu.Edificios',      N'Edificios'),
    (@en, N'Menu.Edificios',      N'Buildings'),
    (@es, N'Menu.Lockers',        N'Lockers'),
    (@en, N'Menu.Lockers',        N'Lockers'),
    (@es, N'Menu.Residentes',     N'Residentes'),
    (@en, N'Menu.Residentes',     N'Residents'),
    (@es, N'Menu.Repartidores',   N'Repartidores'),
    (@en, N'Menu.Repartidores',   N'Couriers'),
    (@es, N'Menu.Operacion',      N'&Operación'),
    (@en, N'Menu.Operacion',      N'&Operations'),
    (@es, N'Menu.EstadoLockers',  N'Estado de lockers'),
    (@en, N'Menu.EstadoLockers',  N'Locker status'),
    (@es, N'Menu.Reservar',       N'Reservar locker'),
    (@en, N'Menu.Reservar',       N'Book a locker'),
    (@es, N'Menu.Entrega',        N'Registrar entrega'),
    (@en, N'Menu.Entrega',        N'Register delivery'),
    (@es, N'Menu.Retiro',         N'Registrar retiro'),
    (@en, N'Menu.Retiro',         N'Register pickup'),
    (@es, N'Menu.Informacion',    N'&Información'),
    (@en, N'Menu.Informacion',    N'&Information'),
    (@es, N'Menu.Reportes',       N'Reportes'),
    (@en, N'Menu.Reportes',       N'Reports'),
    (@es, N'Menu.Bitacora',       N'Bitácora'),
    (@en, N'Menu.Bitacora',       N'Audit log'),
    (@es, N'Menu.Errores',        N'Log de errores'),
    (@en, N'Menu.Errores',        N'Error log'),
    (@es, N'Menu.Idioma',         N'&Idioma'),
    (@en, N'Menu.Idioma',         N'&Language'),
    (@es, N'FrmPrincipal.Estado', N'Usuario: {0}   |   Rol: {1}'),
    (@en, N'FrmPrincipal.Estado', N'User: {0}   |   Role: {1}')
) AS v(IdIdioma, Clave, Texto)
WHERE NOT EXISTS (
    SELECT 1 FROM dbo.Traduccion t
    WHERE t.IdIdioma = v.IdIdioma AND t.Clave = v.Clave
);
GO

SELECT i.Codigo, COUNT(1) AS Claves
FROM dbo.Traduccion t JOIN dbo.Idioma i ON i.NumIdioma = t.IdIdioma
GROUP BY i.Codigo;
GO