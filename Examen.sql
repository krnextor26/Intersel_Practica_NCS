CREATE DATABASE INTERTEL
GO

USE INTERTEL
GO

CREATE TABLE LineasCelulares
(
	MobileLineId INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	MobileLine VARCHAR (12),
	[Description] VARCHAR (100),
	IdEstatus INT DEFAULT 1,
	UsuarioAltaId INT,
	FechaAlta DATETIME DEFAULT GETDATE(),
	FechaUpdate DATETIME
)
go

CREATE TABLE DetallesLlamadas
(
	CallDetailId INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	MobileLine VARCHAR (12),
	CalledPartyNumber VARCHAR (15),
	CalledPartyDescription VARCHAR (100),
	FechaHora DateTime,
	[Duration] int,
	[TotalCost] Decimal(10,4),
	UsuarioAltaId INT,
	IdEstatus INT DEFAULT 1,
	FechaAlta DATETIME DEFAULT GETDATE(),
	FechaUpdate DATETIME
)
GO

CREATE TABLE dbo.TipoUsuario
(
	ID INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	Descripcion VARCHAR (50),
	IdEstatus INT DEFAULT 1,
	FechaAlta DATETIME DEFAULT GETDATE(),
	FechaUpdate DATETIME
)
GO

CREATE TABLE dbo.Rol
(
	ID INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	Descripcion VARCHAR (50),
	IdEstatus INT DEFAULT 1,
	FechaAlta DATETIME DEFAULT GETDATE(),
	FechaUpdate DATETIME
)
GO

CREATE TABLE dbo.Usuarios
(
	UsuarioID INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	Usuario VARCHAR (30),
	Contrasenia VARCHAR (30),
	NombreCompleto varchar(100),
	IdTipoUsuario INT,
	IdRol INT,
	Telefono VARCHAR(12),
	IdEstatus INT DEFAULT 1,
	FechaAlta DATETIME DEFAULT GETDATE(),
	FechaUpdate DATETIME,
	CONSTRAINT FK_TipoUsuario FOREIGN KEY (IdTipoUsuario)
    REFERENCES dbo.TipoUsuario (ID)
	ON DELETE CASCADE
    ON UPDATE CASCADE,
	CONSTRAINT FK_Rol FOREIGN KEY (IdRol)
    REFERENCES dbo.Rol (ID)
	ON DELETE CASCADE
    ON UPDATE CASCADE

)
GO

INSERT INTO dbo.TipoUsuario(Descripcion)
values('Interno')
go

INSERT INTO dbo.TipoUsuario(Descripcion)
values('Externo')
go

-----------------------------------------------------------

INSERT INTO dbo.Rol(Descripcion)
values('Administrador')
go

INSERT INTO dbo.Rol(Descripcion)
values('Supervisor')
go

INSERT INTO dbo.Rol(Descripcion)
values('Coordinador')
go

-----------------------------------------------------------

INSERT INTO dbo.Usuarios(Usuario, Contrasenia, NombreCompleto, IdRol, IdTipoUsuario, Telefono)
values ('Administrador', 'admin12345', 'Administrador del sistema', 1, 1, '5520804009')
go

CREATE PROCEDURE dbo.sp_loginUsuario
(
	@Usuario varchar(30) = NULL,
	@Contrasenia varchar(30) = NULL
)
AS 
BEGIN

	select TOP 1 UsuarioID, Usuario, NombreCompleto 
	from dbo.Usuarios
	where Usuario = @Usuario and Contrasenia = @Contrasenia and IdEstatus = 1


END
GO

CREATE PROCEDURE dbo.sp_ListaLineasCelulares
AS
BEGIN

	select lc.MobileLineId, lc.MobileLine, lc.Description, case when lc.IdEstatus = 1 then 'Activo' else 'Inactivo' end as Estatus, u.Usuario, lc.FechaAlta 
	from LineasCelulares lc 
	LEFT OUTER JOIN dbo.Usuarios u on lc.UsuarioAltaId = u.UsuarioID
	order by MobileLine desc
END
GO


CREATE PROCEDURE dbo.sp_DetalleLineasCelulares
(
	@MobileLine VARCHAR (12) = null
)
AS
BEGIN

	select lc.MobileLineId, lc.MobileLine, lc.Description, dl.CalledPartyNumber, dl.CalledPartyDescription, dl.FechaHora, dl.Duration, dl.TotalCost, case when lc.IdEstatus = 1 then 'Activo' else 'Inactivo' end as Estatus, u.NombreCompleto 
	from LineasCelulares lc 
	LEFT OUTER JOIN dbo.DetallesLlamadas dl on lc.MobileLine = dl.MobileLine
	LEFT OUTER JOIN dbo.Usuarios u on lc.UsuarioAltaId = u.UsuarioID
	WHERE lc.MobileLine = @MobileLine
	order by dl.FechaHora desc

END
GO

CREATE PROCEDURE dbo.sp_TraerLista
(
	@tabla varchar(100) = null
)
AS
BEGIN
	DECLARE @EJECUTAR VARCHAR(8000)
	
	SET @EJECUTAR = 'SELECT 0 AS ID, ''-Seleccione-'' as DESCRIPCION
	UNION ALL
	SELECT ID, DESCRIPCION FROM dbo.' + @tabla + ''

	EXEC (@EJECUTAR)
END
GO


CREATE PROCEDURE dbo.sp_ListaUsuarios
AS
BEGIN

	select u.UsuarioID, u.Usuario, u.Contrasenia, u.NombreCompleto, u.Telefono, tu.Descripcion as TipoUsuario, r.Descripcion as Rol, case when u.IdEstatus = 1 then 'Activo' else 'Inactivo' end as Estatus, u.FechaAlta 
	from dbo.Usuarios u
	LEFT OUTER JOIN dbo.Rol r on u.IdRol = r.ID
	LEFT OUTER JOIN dbo.TipoUsuario tu on u.IdTipoUsuario = tu.ID
	Where u.IdEstatus = 1

END
GO


CREATE PROCEDURE dbo.sp_InsertarUsuario
(
	@Usuario VARCHAR (30) = NULL,
	@Contrasenia VARCHAR (30) = NULL,
	@NombreCompleto varchar(100) = NULL,
	@IdTipoUsuario INT = NULL,
	@IdRol INT = NULL,
	@Telefono VARCHAR(12) = NULL
)
AS
BEGIN

	INSERT INTO dbo.Usuarios(Usuario, Contrasenia, NombreCompleto, IdRol, IdTipoUsuario, Telefono)
	values (@Usuario, @Contrasenia, @NombreCompleto,  @IdRol, @IdTipoUsuario, @Telefono)

END
GO


CREATE PROCEDURE dbo.sp_ActualizarUsuario
(
	@UsuarioID INT = NULL,
	@Usuario VARCHAR (30) = NULL,
	@Contrasenia VARCHAR (30) = NULL,
	@NombreCompleto varchar(100) = NULL,
	@IdTipoUsuario INT = NULL,
	@IdRol INT = NULL,
	@Telefono VARCHAR(12) = NULL
)
AS
BEGIN
	
	UPDATE dbo.Usuarios SET Usuario = @Usuario, Contrasenia = @Contrasenia, NombreCompleto = @NombreCompleto, 
	IdRol = @IdRol, IdTipoUsuario = @IdTipoUsuario, Telefono = @Telefono, FechaUpdate = GETDATE()
	where UsuarioID = @UsuarioID

END
GO

CREATE PROCEDURE dbo.sp_EliminarUsuario
(
	@UsuarioID INT = NULL
)
AS
BEGIN
	
	UPDATE dbo.Usuarios SET IdEstatus = 0, FechaUpdate = GETDATE()
	where UsuarioID = @UsuarioID

END
GO

CREATE PROCEDURE dbo.sp_ConsultarUsuario
(
	@UsuarioID INT = NULL
)
AS
BEGIN
	
	Select UsuarioID, Usuario, NombreCompleto, Contrasenia, IdRol, IdTipoUsuario, Telefono
	from dbo.Usuarios 
	where UsuarioID = @UsuarioID

END
GO


