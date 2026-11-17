USE LockersInteligentes;
GO

DECLARE @es INT = (SELECT NumIdioma FROM dbo.Idioma WHERE Codigo = N'es-AR');
DECLARE @en INT = (SELECT NumIdioma FROM dbo.Idioma WHERE Codigo = N'en-US');

INSERT INTO dbo.Traduccion (IdIdioma, Clave, Texto)
SELECT v.IdIdioma, v.Clave, v.Texto
FROM (VALUES
    /* ---------------------- Comunes nuevos ---------------------- */
    (@es, N'Comun.Desde',    N'Desde'),
    (@en, N'Comun.Desde',    N'From'),
    (@es, N'Comun.Hasta',    N'Hasta'),
    (@en, N'Comun.Hasta',    N'To'),
    (@es, N'Comun.Fecha',    N'Fecha'),
    (@en, N'Comun.Fecha',    N'Date'),
    (@es, N'Comun.Detalle',  N'Detalle'),
    (@en, N'Comun.Detalle',  N'Details'),
    (@es, N'Comun.Periodo',  N'Período'),
    (@en, N'Comun.Periodo',  N'Period'),
    (@es, N'Comun.Tipo',     N'Tipo'),
    (@en, N'Comun.Tipo',     N'Type'),

    /* ------------------------ FrmBitacora ------------------------ */
    (@es, N'FrmBitacora.Caption',   N'Bitácora'),
    (@en, N'FrmBitacora.Caption',   N'Audit log'),
    (@es, N'FrmBitacora.Titulo',    N'Bitácora de operaciones'),
    (@en, N'FrmBitacora.Titulo',    N'Operations audit log'),
    (@es, N'FrmBitacora.grpFiltros',N'Filtros'),
    (@en, N'FrmBitacora.grpFiltros',N'Filters'),
    (@es, N'FrmBitacora.lblOper',   N'Operación'),
    (@en, N'FrmBitacora.lblOper',   N'Operation'),
    (@es, N'FrmBitacora.colNumero', N'N°'),
    (@en, N'FrmBitacora.colNumero', N'No.'),
    (@es, N'FrmBitacora.colOper',   N'Operación'),
    (@en, N'FrmBitacora.colOper',   N'Operation'),
    (@es, N'FrmBitacora.Resumen',   N'{0} registro(s) encontrado(s).'),
    (@en, N'FrmBitacora.Resumen',   N'{0} record(s) found.'),

    /* ------------------------ FrmReportes ------------------------ */
    (@es, N'FrmReportes.Caption',   N'Reportes y estadísticas'),
    (@en, N'FrmReportes.Caption',   N'Reports and statistics'),
    (@es, N'FrmReportes.grpGen',    N'Generar reporte'),
    (@en, N'FrmReportes.grpGen',    N'Generate report'),
    (@es, N'FrmReportes.btnGen',    N'Generar reporte'),
    (@en, N'FrmReportes.btnGen',    N'Generate report'),
    (@es, N'FrmReportes.grpRes',    N'Resultado'),
    (@en, N'FrmReportes.grpRes',    N'Result'),
    (@es, N'FrmReportes.lblOcup',   N'Ocupación actual'),
    (@en, N'FrmReportes.lblOcup',   N'Current occupancy'),
    (@es, N'FrmReportes.lblPert',   N'Lockers usados en el período'),
    (@en, N'FrmReportes.lblPert',   N'Lockers used in the period'),
    (@es, N'FrmReportes.lblVenc',   N'Paquetes vencidos'),
    (@en, N'FrmReportes.lblVenc',   N'Overdue packages'),
    (@es, N'FrmReportes.lblRankRep',N'Top repartidores'),
    (@en, N'FrmReportes.lblRankRep',N'Top couriers'),
    (@es, N'FrmReportes.lblRankLoc',N'Lockers más usados'),
    (@en, N'FrmReportes.lblRankLoc',N'Most used lockers'),
    (@es, N'FrmReportes.lblHist',   N'Reportes generados'),
    (@en, N'FrmReportes.lblHist',   N'Generated reports'),
    (@es, N'FrmReportes.Encabezado',N'{0}   |   Período {1}   |   Generado el {2}'),
    (@en, N'FrmReportes.Encabezado',N'{0}   |   Period {1}   |   Generated on {2}'),
    (@es, N'FrmReportes.OkGenerado',N'Reporte N° {0} generado y guardado.'),
    (@en, N'FrmReportes.OkGenerado',N'Report #{0} generated and saved.'),
    (@es, N'FrmReportes.ValEdif',   N'Seleccioná un edificio.'),
    (@en, N'FrmReportes.ValEdif',   N'Select a building.'),
    (@es, N'FrmReportes.colNumero', N'N°'),
    (@en, N'FrmReportes.colNumero', N'No.'),
    (@es, N'FrmReportes.colGenerado',N'Generado'),
    (@en, N'FrmReportes.colGenerado',N'Generated'),
    (@es, N'FrmReportes.colOcup',   N'Ocupación'),
    (@en, N'FrmReportes.colOcup',   N'Occupancy'),
    (@es, N'FrmReportes.colVenc',   N'Vencidos'),
    (@en, N'FrmReportes.colVenc',   N'Overdue'),

    /* ------------------------- FrmErrores ------------------------- */
    (@es, N'FrmErrores.Caption',    N'Log de errores'),
    (@en, N'FrmErrores.Caption',    N'Error log'),
    (@es, N'FrmErrores.lblRuta',    N'Archivo: {0}'),
    (@en, N'FrmErrores.lblRuta',    N'File: {0}'),
    (@es, N'FrmErrores.lblDetalle', N'Detalle técnico'),
    (@en, N'FrmErrores.lblDetalle', N'Technical details'),
    (@es, N'FrmErrores.btnCarpeta', N'Abrir carpeta del log'),
    (@en, N'FrmErrores.btnCarpeta', N'Open log folder'),
    (@es, N'FrmErrores.btnVaciar',  N'Vaciar log'),
    (@en, N'FrmErrores.btnVaciar',  N'Clear log'),
    (@es, N'FrmErrores.btnBackup',  N'Generar backup de la base'),
    (@en, N'FrmErrores.btnBackup',  N'Generate database backup'),
    (@es, N'FrmErrores.SinErrores', N'No hay errores registrados.'),
    (@en, N'FrmErrores.SinErrores', N'No errors logged.'),
    (@es, N'FrmErrores.Cantidad',   N'{0} error(es) registrado(s).'),
    (@en, N'FrmErrores.Cantidad',   N'{0} error(s) logged.'),
    (@es, N'FrmErrores.colFecha',   N'Fecha'),
    (@en, N'FrmErrores.colFecha',   N'Date'),
    (@es, N'FrmErrores.colOrigen',  N'Origen'),
    (@en, N'FrmErrores.colOrigen',  N'Source'),
    (@es, N'FrmErrores.colMensaje', N'Mensaje'),
    (@en, N'FrmErrores.colMensaje', N'Message')
) AS v(IdIdioma, Clave, Texto)
WHERE NOT EXISTS (
    SELECT 1 FROM dbo.Traduccion t
    WHERE t.IdIdioma = v.IdIdioma AND t.Clave = v.Clave
);
GO