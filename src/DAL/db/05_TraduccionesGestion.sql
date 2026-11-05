USE LockersInteligentes;
GO

DECLARE @es INT = (SELECT NumIdioma FROM dbo.Idioma WHERE Codigo = N'es-AR');
DECLARE @en INT = (SELECT NumIdioma FROM dbo.Idioma WHERE Codigo = N'en-US');

INSERT INTO dbo.Traduccion (IdIdioma, Clave, Texto)
SELECT v.IdIdioma, v.Clave, v.Texto
FROM (VALUES
    /* ---------------------- Comunes nuevos ---------------------- */
    (@es, N'Comun.Direccion',   N'Dirección'),
    (@en, N'Comun.Direccion',   N'Address'),
    (@es, N'Comun.Localidad',   N'Localidad'),
    (@en, N'Comun.Localidad',   N'City'),
    (@es, N'Comun.Descripcion', N'Descripción'),
    (@en, N'Comun.Descripcion', N'Description'),
    (@es, N'Comun.Sector',      N'Sector'),
    (@en, N'Comun.Sector',      N'Section'),
    (@es, N'Comun.Tamanio',     N'Tamaño'),
    (@en, N'Comun.Tamanio',     N'Size'),
    (@es, N'Comun.Numero',      N'Número'),
    (@en, N'Comun.Numero',      N'Number'),
    (@es, N'Comun.Empresa',     N'Empresa'),
    (@en, N'Comun.Empresa',     N'Company'),
    (@es, N'Comun.Piso',        N'Piso / Depto'),
    (@en, N'Comun.Piso',        N'Floor / Apt'),
    (@es, N'Comun.Lockers',     N'Lockers'),
    (@en, N'Comun.Lockers',     N'Lockers'),

    /* ------------------------ FrmEdificios ------------------------ */
    (@es, N'FrmEdificios.Caption',   N'Gestión de edificios'),
    (@en, N'FrmEdificios.Caption',   N'Building management'),
    (@es, N'FrmEdificios.lblTel',    N'Teléfono de contacto'),
    (@en, N'FrmEdificios.lblTel',    N'Contact phone'),
    (@es, N'FrmEdificios.lblCant',   N'Lockers cargados'),
    (@en, N'FrmEdificios.lblCant',   N'Lockers loaded'),
    (@es, N'FrmEdificios.grpGen',    N'Generar lockers para el edificio seleccionado'),
    (@en, N'FrmEdificios.grpGen',    N'Generate lockers for the selected building'),
    (@es, N'FrmEdificios.lblCantGen',N'Cantidad'),
    (@en, N'FrmEdificios.lblCantGen',N'Quantity'),
    (@es, N'FrmEdificios.lblDesde',  N'Desde el N°'),
    (@en, N'FrmEdificios.lblDesde',  N'Starting at'),
    (@es, N'FrmEdificios.btnGen',    N'Generar lockers'),
    (@en, N'FrmEdificios.btnGen',    N'Generate lockers'),

    /* ------------------------- FrmLockers ------------------------- */
    (@es, N'FrmLockers.Caption',     N'Gestión de lockers'),
    (@en, N'FrmLockers.Caption',     N'Locker management'),

    /* ------------------------ FrmResidentes ------------------------ */
    (@es, N'FrmResidentes.Caption',  N'Gestión de residentes'),
    (@en, N'FrmResidentes.Caption',  N'Resident management'),

    /* ----------------------- FrmRepartidores ----------------------- */
    (@es, N'FrmRepartidores.Caption',N'Gestión de repartidores'),
    (@en, N'FrmRepartidores.Caption',N'Courier management')
) AS v(IdIdioma, Clave, Texto)
WHERE NOT EXISTS (
    SELECT 1 FROM dbo.Traduccion t
    WHERE t.IdIdioma = v.IdIdioma AND t.Clave = v.Clave
);
GO