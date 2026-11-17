USE LockersInteligentes;
GO

IF COL_LENGTH('dbo.Usuario', 'DVH') IS NULL
    ALTER TABLE dbo.Usuario ADD DVH VARCHAR(64) NULL;
GO

SELECT IdUsuario, NombreUsuario, DVH FROM dbo.Usuario;
GO