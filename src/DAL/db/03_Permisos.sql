USE LockersInteligentes;
GO

CREATE TABLE dbo.Componente (
    IdComponente   INT IDENTITY(1,1) NOT NULL,
    Nombre         NVARCHAR(80)  NOT NULL,
    Descripcion    NVARCHAR(200) NULL,
    TipoComponente TINYINT       NOT NULL,   -- 1 Patente (hoja), 2 Familia (compuesto)
    CONSTRAINT PK_Componente        PRIMARY KEY (IdComponente),
    CONSTRAINT UQ_Componente_Nombre UNIQUE (Nombre),
    CONSTRAINT CK_Componente_Tipo   CHECK (TipoComponente IN (1, 2))
);
GO

CREATE TABLE dbo.ComponenteHijo (
    IdPadre INT NOT NULL,
    IdHijo  INT NOT NULL,
    CONSTRAINT PK_ComponenteHijo PRIMARY KEY (IdPadre, IdHijo),
    CONSTRAINT FK_CompHijo_Padre FOREIGN KEY (IdPadre) REFERENCES dbo.Componente (IdComponente),
    CONSTRAINT FK_CompHijo_Hijo  FOREIGN KEY (IdHijo)  REFERENCES dbo.Componente (IdComponente),
    CONSTRAINT CK_CompHijo_NoSiMismo CHECK (IdPadre <> IdHijo)
);
GO

/* Qué tiene asignado cada usuario: puede ser una familia entera o patentes sueltas. */
CREATE TABLE dbo.UsuarioComponente (
    IdUsuario    INT NOT NULL,
    IdComponente INT NOT NULL,
    CONSTRAINT PK_UsuarioComponente PRIMARY KEY (IdUsuario, IdComponente),
    CONSTRAINT FK_UsuComp_Usuario    FOREIGN KEY (IdUsuario)
        REFERENCES dbo.Usuario (IdUsuario) ON DELETE CASCADE,
    CONSTRAINT FK_UsuComp_Componente FOREIGN KEY (IdComponente)
        REFERENCES dbo.Componente (IdComponente)
);
GO

/* ------------------------- Patentes ------------------------- */

INSERT INTO dbo.Componente (Nombre, Descripcion, TipoComponente)
SELECT v.Nombre, v.Descripcion, 1
FROM (VALUES
    (N'Usuarios.Gestionar',        N'ABM de usuarios'),
    (N'Permisos.Gestionar',        N'Administrar familias y patentes'),
    (N'Edificios.Gestionar',       N'ABM de edificios'),
    (N'Lockers.Gestionar',         N'ABM de lockers'),
    (N'Residentes.Gestionar',      N'ABM de residentes'),
    (N'Repartidores.Gestionar',    N'ABM de repartidores'),
    (N'Lockers.VerEstado',         N'Ver el panel de estado de lockers'),
    (N'Paquetes.Reservar',         N'Reservar locker'),
    (N'Paquetes.RegistrarEntrega', N'Registrar entrega'),
    (N'Paquetes.RegistrarRetiro',  N'Registrar retiro'),
    (N'Reportes.Ver',              N'Generar reportes y estadísticas'),
    (N'Bitacora.Ver',              N'Consultar la bitácora'),
    (N'Errores.Ver',               N'Ver el log de errores'),
    (N'Backup.Ejecutar',           N'Generar backup de la base')
) AS v(Nombre, Descripcion)
WHERE NOT EXISTS (SELECT 1 FROM dbo.Componente c WHERE c.Nombre = v.Nombre);
GO

/* ------------------------- Familias ------------------------- */

INSERT INTO dbo.Componente (Nombre, Descripcion, TipoComponente)
SELECT v.Nombre, v.Descripcion, 2
FROM (VALUES
    (N'Operación de paquetes', N'Ciclo completo del paquete'),
    (N'Gestión de datos',      N'ABM de edificios, lockers, residentes y repartidores'),
    (N'Supervisión',           N'Reportes, bitácora y errores'),
    (N'Administración',        N'Acceso total al sistema')
) AS v(Nombre, Descripcion)
WHERE NOT EXISTS (SELECT 1 FROM dbo.Componente c WHERE c.Nombre = v.Nombre);
GO

/* ---------------------- Armado del árbol ---------------------- */

/* "Operación de paquetes" contiene las cuatro patentes operativas. */
INSERT INTO dbo.ComponenteHijo (IdPadre, IdHijo)
SELECT f.IdComponente, p.IdComponente
FROM dbo.Componente f, dbo.Componente p
WHERE f.Nombre = N'Operación de paquetes'
  AND p.Nombre IN (N'Lockers.VerEstado', N'Paquetes.Reservar',
                   N'Paquetes.RegistrarEntrega', N'Paquetes.RegistrarRetiro')
  AND NOT EXISTS (SELECT 1 FROM dbo.ComponenteHijo h
                  WHERE h.IdPadre = f.IdComponente AND h.IdHijo = p.IdComponente);
GO

/* "Gestión de datos" contiene los cuatro ABM de negocio. */
INSERT INTO dbo.ComponenteHijo (IdPadre, IdHijo)
SELECT f.IdComponente, p.IdComponente
FROM dbo.Componente f, dbo.Componente p
WHERE f.Nombre = N'Gestión de datos'
  AND p.Nombre IN (N'Edificios.Gestionar', N'Lockers.Gestionar',
                   N'Residentes.Gestionar', N'Repartidores.Gestionar')
  AND NOT EXISTS (SELECT 1 FROM dbo.ComponenteHijo h
                  WHERE h.IdPadre = f.IdComponente AND h.IdHijo = p.IdComponente);
GO

/* "Supervisión" contiene las tres patentes de consulta. */
INSERT INTO dbo.ComponenteHijo (IdPadre, IdHijo)
SELECT f.IdComponente, p.IdComponente
FROM dbo.Componente f, dbo.Componente p
WHERE f.Nombre = N'Supervisión'
  AND p.Nombre IN (N'Reportes.Ver', N'Bitacora.Ver', N'Errores.Ver')
  AND NOT EXISTS (SELECT 1 FROM dbo.ComponenteHijo h
                  WHERE h.IdPadre = f.IdComponente AND h.IdHijo = p.IdComponente);
GO


INSERT INTO dbo.ComponenteHijo (IdPadre, IdHijo)
SELECT a.IdComponente, h.IdComponente
FROM dbo.Componente a, dbo.Componente h
WHERE a.Nombre = N'Administración'
  AND h.Nombre IN (N'Operación de paquetes', N'Gestión de datos', N'Supervisión',
                   N'Usuarios.Gestionar', N'Permisos.Gestionar', N'Backup.Ejecutar')
  AND NOT EXISTS (SELECT 1 FROM dbo.ComponenteHijo x
                  WHERE x.IdPadre = a.IdComponente AND x.IdHijo = h.IdComponente);
GO


INSERT INTO dbo.UsuarioComponente (IdUsuario, IdComponente)
SELECT u.IdUsuario, c.IdComponente
FROM dbo.Usuario u
     INNER JOIN dbo.Rol r ON r.IdRol = u.IdRol
     CROSS JOIN dbo.Componente c
WHERE r.Nombre = N'Administrador'
  AND c.Nombre = N'Administración'
  AND NOT EXISTS (SELECT 1 FROM dbo.UsuarioComponente uc
                  WHERE uc.IdUsuario = u.IdUsuario AND uc.IdComponente = c.IdComponente);
GO

INSERT INTO dbo.UsuarioComponente (IdUsuario, IdComponente)
SELECT u.IdUsuario, c.IdComponente
FROM dbo.Usuario u
     INNER JOIN dbo.Rol r ON r.IdRol = u.IdRol
     CROSS JOIN dbo.Componente c
WHERE r.Nombre = N'Operador'
  AND c.Nombre = N'Operación de paquetes'
  AND NOT EXISTS (SELECT 1 FROM dbo.UsuarioComponente uc
                  WHERE uc.IdUsuario = u.IdUsuario AND uc.IdComponente = c.IdComponente);
GO
