USE master;

IF DB_ID('LockersInteligentes') IS NULL
    CREATE DATABASE LockersInteligentes;

USE LockersInteligentes;

CREATE TABLE dbo.Idioma (
    NumIdioma        INT IDENTITY(1,1) NOT NULL,
    Nombre           NVARCHAR(50)  NOT NULL,
    Codigo           NVARCHAR(10)  NOT NULL, 
    EsPredeterminado BIT           NOT NULL CONSTRAINT DF_Idioma_EsPred DEFAULT (0),
    CONSTRAINT PK_Idioma        PRIMARY KEY (NumIdioma),
    CONSTRAINT UQ_Idioma_Codigo UNIQUE (Codigo)
);

CREATE UNIQUE INDEX UQ_Idioma_UnSoloPredeterminado
    ON dbo.Idioma (EsPredeterminado)
    WHERE EsPredeterminado = 1;

CREATE TABLE dbo.Traduccion (
    NumTraduccion INT IDENTITY(1,1) NOT NULL,
    IdIdioma      INT           NOT NULL,
    Clave         NVARCHAR(100) NOT NULL,  
    Texto         NVARCHAR(500) NOT NULL,
    CONSTRAINT PK_Traduccion PRIMARY KEY (NumTraduccion),
    CONSTRAINT FK_Traduccion_Idioma FOREIGN KEY (IdIdioma)
        REFERENCES dbo.Idioma (NumIdioma) ON DELETE CASCADE,
    CONSTRAINT UQ_Traduccion_Idioma_Clave UNIQUE (IdIdioma, Clave)
);

CREATE TABLE dbo.Rol (
    IdRol       INT IDENTITY(1,1) NOT NULL,
    Nombre      NVARCHAR(50)  NOT NULL,
    Descripcion NVARCHAR(200) NULL,
    CONSTRAINT PK_Rol        PRIMARY KEY (IdRol),
    CONSTRAINT UQ_Rol_Nombre UNIQUE (Nombre)
);

CREATE TABLE dbo.Usuario (
    IdUsuario     INT IDENTITY(1,1) NOT NULL,
    NombreUsuario NVARCHAR(50) NOT NULL,
    Nombre        NVARCHAR(80) NOT NULL,
    Apellido      NVARCHAR(80) NOT NULL,
    Correo        NVARCHAR(120) NULL,
    PasswordHash  VARCHAR(64)  NOT NULL, 
    PasswordSalt  VARCHAR(64)  NOT NULL,   
    IdRol         INT          NOT NULL,
    IdIdioma      INT          NULL,
    Activo        BIT          NOT NULL CONSTRAINT DF_Usuario_Activo DEFAULT (1),
    CONSTRAINT PK_Usuario        PRIMARY KEY (IdUsuario),
    CONSTRAINT UQ_Usuario_Nombre UNIQUE (NombreUsuario),
    CONSTRAINT FK_Usuario_Rol    FOREIGN KEY (IdRol)    REFERENCES dbo.Rol (IdRol),
    CONSTRAINT FK_Usuario_Idioma FOREIGN KEY (IdIdioma) REFERENCES dbo.Idioma (NumIdioma)
);


CREATE TABLE dbo.Edificio (
    IdEdificio       INT IDENTITY(1,1) NOT NULL,
    Nombre           NVARCHAR(100) NOT NULL,
    Direccion        NVARCHAR(150) NOT NULL,
    Localidad        NVARCHAR(80)  NOT NULL,
    TelefonoContacto NVARCHAR(30)  NULL,
    CantLockers      INT           NOT NULL CONSTRAINT DF_Edificio_Cant DEFAULT (0),
    CONSTRAINT PK_Edificio PRIMARY KEY (IdEdificio)
);

CREATE TABLE dbo.Locker (
    IdLocker    INT IDENTITY(1,1) NOT NULL,
    IdEdificio  INT           NOT NULL,
    Numero      INT           NOT NULL,
    GrupLocker  NVARCHAR(30)  NULL,
    Descripcion NVARCHAR(150) NULL,
    Tamanio     TINYINT       NOT NULL,  
    Estado      TINYINT       NOT NULL CONSTRAINT DF_Locker_Estado DEFAULT (1),
    CONSTRAINT PK_Locker PRIMARY KEY (IdLocker),
    CONSTRAINT FK_Locker_Edificio FOREIGN KEY (IdEdificio)
        REFERENCES dbo.Edificio (IdEdificio) ON DELETE CASCADE,  
    CONSTRAINT CK_Locker_Tamanio CHECK (Tamanio BETWEEN 1 AND 3),
    CONSTRAINT CK_Locker_Estado  CHECK (Estado  BETWEEN 1 AND 4)
);

CREATE UNIQUE INDEX UQ_Locker_Edificio_Nro ON dbo.Locker (IdEdificio, Numero);

CREATE TABLE dbo.Residente (
    IdResidente INT IDENTITY(1,1) NOT NULL,
    IdEdificio  INT           NOT NULL,
    Nombre      NVARCHAR(80)  NOT NULL,
    Apellido    NVARCHAR(80)  NOT NULL,
    Dni         NVARCHAR(15)  NOT NULL,
    Correo      NVARCHAR(120) NOT NULL, 
    Piso        NVARCHAR(10)  NULL,
    Telefono    NVARCHAR(30)  NULL,
    CONSTRAINT PK_Residente          PRIMARY KEY (IdResidente),
    CONSTRAINT UQ_Residente_Dni      UNIQUE (Dni),
    CONSTRAINT FK_Residente_Edificio FOREIGN KEY (IdEdificio)
        REFERENCES dbo.Edificio (IdEdificio)
);

CREATE TABLE dbo.Repartidor (
    IdRepartidor INT IDENTITY(1,1) NOT NULL,
    Nombre       NVARCHAR(80) NOT NULL,
    Apellido     NVARCHAR(80) NOT NULL,
    Dni          NVARCHAR(15) NOT NULL,
    Empresa      NVARCHAR(80) NULL,
    Telefono     NVARCHAR(30) NULL,
    CONSTRAINT PK_Repartidor     PRIMARY KEY (IdRepartidor),
    CONSTRAINT UQ_Repartidor_Dni UNIQUE (Dni)
);

CREATE TABLE dbo.OrdenDeEntrega (
    IdOrden        INT IDENTITY(1,1) NOT NULL,
    Descripcion    NVARCHAR(200) NULL,
    Estado         TINYINT       NOT NULL CONSTRAINT DF_Orden_Estado DEFAULT (1),
    TamanioPaquete TINYINT       NOT NULL,
    FechaReserva   DATETIME2(0)  NOT NULL CONSTRAINT DF_Orden_FReserva DEFAULT (SYSDATETIME()),
    FechaEntrega   DATETIME2(0)  NULL,
    FechaRetiro    DATETIME2(0)  NULL,
    IdLocker       INT           NOT NULL,
    IdResidente    INT           NOT NULL,
    IdRepartidor   INT           NULL,   
    CONSTRAINT PK_OrdenDeEntrega PRIMARY KEY (IdOrden),
    CONSTRAINT FK_Orden_Locker     FOREIGN KEY (IdLocker)     REFERENCES dbo.Locker (IdLocker),
    CONSTRAINT FK_Orden_Residente  FOREIGN KEY (IdResidente)  REFERENCES dbo.Residente (IdResidente),
    CONSTRAINT FK_Orden_Repartidor FOREIGN KEY (IdRepartidor) REFERENCES dbo.Repartidor (IdRepartidor),
    CONSTRAINT CK_Orden_Estado  CHECK (Estado BETWEEN 1 AND 3),
    CONSTRAINT CK_Orden_Tamanio CHECK (TamanioPaquete BETWEEN 1 AND 3),
    CONSTRAINT CK_Orden_Fechas  CHECK (FechaRetiro IS NULL OR FechaEntrega IS NULL
                                       OR FechaRetiro >= FechaEntrega)
);

CREATE TABLE dbo.CodigoAcceso (
    IdCodigo        INT IDENTITY(1,1) NOT NULL,
    IdOrden         INT          NOT NULL,
    TipoCodigo      TINYINT      NOT NULL, 
    PinHash         VARCHAR(64)  NOT NULL,   
    PinSalt         VARCHAR(64)  NOT NULL,
    FechaGeneracion DATETIME2(0) NOT NULL CONSTRAINT DF_Codigo_FGen DEFAULT (SYSDATETIME()),
    Usado           BIT          NOT NULL CONSTRAINT DF_Codigo_Usado DEFAULT (0),
    CONSTRAINT PK_CodigoAcceso PRIMARY KEY (IdCodigo),
    CONSTRAINT FK_Codigo_Orden FOREIGN KEY (IdOrden)
        REFERENCES dbo.OrdenDeEntrega (IdOrden) ON DELETE CASCADE,  -- composicion
    CONSTRAINT CK_Codigo_Tipo CHECK (TipoCodigo IN (1, 2))
);

CREATE TABLE dbo.Notificacion (
    IdNotificacion INT IDENTITY(1,1) NOT NULL,
    IdOrden        INT           NOT NULL,
    IdResidente    INT           NOT NULL,
    Mensaje        NVARCHAR(500) NOT NULL,
    Estado         TINYINT       NOT NULL CONSTRAINT DF_Notif_Estado DEFAULT (1),
    FechaEnvio     DATETIME2(0)  NULL,
    CONSTRAINT PK_Notificacion PRIMARY KEY (IdNotificacion),
    CONSTRAINT FK_Notif_Orden     FOREIGN KEY (IdOrden)
        REFERENCES dbo.OrdenDeEntrega (IdOrden) ON DELETE CASCADE,
    CONSTRAINT FK_Notif_Residente FOREIGN KEY (IdResidente)
        REFERENCES dbo.Residente (IdResidente),
    CONSTRAINT CK_Notif_Estado CHECK (Estado BETWEEN 1 AND 3)
);

CREATE TABLE dbo.Historial (
    NumHistorial    INT IDENTITY(1,1) NOT NULL,
    Actor           NVARCHAR(50)  NOT NULL, 
    TipoDeOperacion TINYINT       NOT NULL,
    FechaOperacion  DATETIME2(0)  NOT NULL CONSTRAINT DF_Hist_Fecha DEFAULT (SYSDATETIME()),
    IdLocker        INT           NULL,
    IdOrden         INT           NULL,
    Detalle         NVARCHAR(500) NULL,
    CONSTRAINT PK_Historial PRIMARY KEY (NumHistorial),
    CONSTRAINT FK_Hist_Locker FOREIGN KEY (IdLocker) REFERENCES dbo.Locker (IdLocker),
    CONSTRAINT FK_Hist_Orden  FOREIGN KEY (IdOrden)  REFERENCES dbo.OrdenDeEntrega (IdOrden)
);

CREATE INDEX IX_Historial_Fecha ON dbo.Historial (FechaOperacion DESC);

CREATE TABLE dbo.Reporte (
    NumReporte                  INT IDENTITY(1,1) NOT NULL,
    IdEdificio                  INT           NOT NULL,
    FechaGeneracion             DATETIME2(0)  NOT NULL CONSTRAINT DF_Rep_Fecha DEFAULT (SYSDATETIME()),
    Periodo                     NVARCHAR(20)  NOT NULL,
    PorcentajeOcupacionLocker   DECIMAL(5,2)  NOT NULL CONSTRAINT DF_Rep_Ocup DEFAULT (0),
    PorcentajePertenenciaLocker DECIMAL(5,2)  NOT NULL CONSTRAINT DF_Rep_Pert DEFAULT (0),
    PaqueteVencido              INT           NOT NULL CONSTRAINT DF_Rep_Venc DEFAULT (0),
    RankingRepartidores         NVARCHAR(MAX) NULL,
    RankingTopLockers           NVARCHAR(MAX) NULL,
    CONSTRAINT PK_Reporte          PRIMARY KEY (NumReporte),
    CONSTRAINT FK_Reporte_Edificio FOREIGN KEY (IdEdificio) REFERENCES dbo.Edificio (IdEdificio)
);