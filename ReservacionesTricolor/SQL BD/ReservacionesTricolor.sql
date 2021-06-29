USE [master]
GO
/****** Object:  Database [activos]    Script Date: 2/8/2020 13:01:01 ******/
CREATE DATABASE [ReservacionesTricolor]
 CONTAINMENT = NONE
 GO
ALTER DATABASE [ReservacionesTricolor] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ReservacionesTricolor].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
USE [ReservacionesTricolor]


CREATE TABLE [dbo].[Pais]
(
  [IdPais] [INT] IDENTITY(1,1),
  [NombrePais] [VARCHAR] (30) NOT NULL,
)
ALTER TABLE [dbo].[Pais] ADD CONSTRAINT PK_PAIS PRIMARY KEY ([IdPais])



CREATE TABLE [dbo].[Provincia]
(
  [IdProvincia] [int] IDENTITY(1,1),
  [NombreProvincia] [VARCHAR] (30) NOT NULL,
  [IdPais] [INT] NOT NULL,
)
ALTER TABLE [dbo].[Provincia] ADD CONSTRAINT PK_PROVINCIA PRIMARY KEY ([IdProvincia])
ALTER TABLE [dbo].[Provincia] ADD CONSTRAINT FK_PAIS FOREIGN KEY ([IdPais]) REFERENCES [dbo].[Pais]


CREATE TABLE [dbo].[Canton]
(
  [IdCanton] [int] IDENTITY(1,1),
  [NombreCanton] [VARCHAR] (30) NOT NULL,
  [IdPais] [INT] NOT NULL,
  [IdProvincia] [INT] NOT NULL,
)
ALTER TABLE [dbo].[Canton] ADD CONSTRAINT PK_CANTON PRIMARY KEY ([IdCanton])
ALTER TABLE [dbo].[Canton] ADD CONSTRAINT FK_PAIS_CANTON FOREIGN KEY ([IdPais]) REFERENCES [dbo].[Pais]
ALTER TABLE [dbo].[Canton] ADD CONSTRAINT FK_PROVINCIA FOREIGN KEY ([IdProvincia]) REFERENCES [dbo].[Provincia]


CREATE TABLE [dbo].[Usuario]
(
  [IdUsuario][INT] IDENTITY(1,1),
  [Nombre] [VARCHAR](50) NOT NULL,
  [PrimerApellido] [VARCHAR](50) NOT NULL,
  [SegundoApellido] [VARCHAR](50) NOT NULL,
  [Email] [VARCHAR](50) NOT NULL,
  [Telefono] [VARCHAR](50) NOT NULL, 
  [Contrasena] [VARCHAR](50) NOT NULL
)
ALTER TABLE [dbo].[Usuario] ADD CONSTRAINT PK_USUARIO PRIMARY KEY ([IdUsuario])



CREATE TABLE [dbo].[Rol]
(
  [IdRol][INT] IDENTITY(1,1),
  [NombreRol] [VARCHAR](50) NOT NULL,
)
ALTER TABLE [dbo].[Rol] ADD CONSTRAINT PK_ROL PRIMARY KEY ([IdRol])



CREATE TABLE [dbo].[UsuarioRol]
(
  [Id][INT] IDENTITY(1,1),
  [IdRol] [INT] NOT NULL,
  [IdUsuario] [INT] NOT NULL
)
ALTER TABLE [dbo].[UsuarioRol] ADD CONSTRAINT PK_USUARIO_ROL PRIMARY KEY ([Id])
ALTER TABLE [dbo].[UsuarioRol] ADD CONSTRAINT FK_ROL FOREIGN KEY ([IdRol]) REFERENCES [dbo].[Rol]
ALTER TABLE [dbo].[UsuarioRol] ADD CONSTRAINT FK_USUARIO FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[Usuario]


CREATE TABLE [dbo].[Hotel]
(
  [IdHotel][int] IDENTITY(1,1),
  [NombreHotel] [VARCHAR](50) NOT NULL,
  [Descripcion] [VARCHAR](50) NOT NULL, 
  [Estrellas] [INT] NOT NULL,
  [Telefono] [VARCHAR](50) NOT NULL,
  [Email] [VARCHAR](50) NOT NULL,
  [Direccion] [VARCHAR](50) NOT NULL,
  [IdAdministrador] [INT] NOT NULL,   
  [IdPais] [INT] NOT NULL,
  [IdProvincia] [INT] NOT NULL,
  [IdCanton] [INT] NOT NULL,
)
ALTER TABLE [dbo].[Hotel] ADD CONSTRAINT PK_HOTEL PRIMARY KEY ([IdHotel])
ALTER TABLE [dbo].[Hotel] ADD CONSTRAINT FK_USUARIO_HOTEL FOREIGN KEY (IdAdministrador) REFERENCES [dbo].[Usuario]
ALTER TABLE [dbo].[Hotel] ADD CONSTRAINT FK_PAIS_HOTEL FOREIGN KEY ([IdPais]) REFERENCES [dbo].[Pais]
ALTER TABLE [dbo].[Hotel] ADD CONSTRAINT FK_PROVINCIA_HOTEL FOREIGN KEY (IdProvincia) REFERENCES [dbo].[Provincia]
ALTER TABLE [dbo].[Hotel] ADD CONSTRAINT FK_CANTON_HOTEL FOREIGN KEY (IdCanton) REFERENCES [dbo].[Canton]



CREATE TABLE [dbo].[Habitacion]
(
  [IdHabitacion][int] IDENTITY(1,1),
  [NombreHabitacion] [VARCHAR](50) NOT NULL,
  [Descripcion] [VARCHAR](50) NOT NULL, 
  [Precio] [MONEY] NOT NULL,
  [Capacidad] [INT] NOT NULL,
  [IdHotel] [INT] NOT NULL
)
ALTER TABLE [dbo].[Habitacion] ADD CONSTRAINT PK_HABITACION PRIMARY KEY ([IdHabitacion])
ALTER TABLE [dbo].[Habitacion] ADD CONSTRAINT FK_HOTEL FOREIGN KEY (IdHotel) REFERENCES [dbo].[Hotel]




CREATE TABLE [dbo].[Caracteristica]
(
  [IdCaracteristica][INT] IDENTITY(1,1),
  [NombreCaracteristica] [VARCHAR](50) NOT NULL,
)
ALTER TABLE [dbo].[Caracteristica] ADD CONSTRAINT PK_CARACTERISTICA PRIMARY KEY ([IdCaracteristica])



CREATE TABLE [dbo].[CaracteristicaHabitacion]
(
  [Id][INT] IDENTITY(1,1),
  [IdHabitacion] [INT] NOT NULL,
  [IdCaracteristica] [INT] NOT NULL
)
ALTER TABLE [dbo].[CaracteristicaHabitacion] ADD CONSTRAINT PK_CARACTERISTICA_HABITACION PRIMARY KEY (Id)
ALTER TABLE [dbo].[CaracteristicaHabitacion] ADD CONSTRAINT FK_HABITACION_CARACTERISTICA FOREIGN KEY (IdHabitacion) REFERENCES [dbo].[Habitacion]
ALTER TABLE [dbo].[CaracteristicaHabitacion] ADD CONSTRAINT FK_CARACTERISTICA_CARACTERISTICA FOREIGN KEY (IdCaracteristica) REFERENCES [dbo].[Caracteristica]



CREATE TABLE [dbo].[Reservacion]
(
  [IdReservacion][int] IDENTITY(1,1),
  [CantidadPersonas] [INT] NOT NULL,
  [CheckIn] [DATE] NOT NULL, 
  [CheckOut] [DATE] NOT NULL,
  [IdHabitacion] [INT] NOT NULL, 
  [IdUsuario] [INT] NOT NULL, 
)
ALTER TABLE [dbo].[Reservacion] ADD CONSTRAINT PK_RESERVACION PRIMARY KEY ([IdReservacion])
ALTER TABLE [dbo].[Reservacion] ADD CONSTRAINT FK_HABITACION_RESERVACION FOREIGN KEY (IdHabitacion) REFERENCES [dbo].[Habitacion]
ALTER TABLE [dbo].[Reservacion] ADD CONSTRAINT FK_USUARIO_RESERVACION FOREIGN KEY (IdUsuario) REFERENCES [dbo].[Usuario]




CREATE TABLE [dbo].[Factura]
(
  [IdFactura][INT] IDENTITY(1,1),
  [SubTotal] [INT] NOT NULL,
  [IVA] [DATE] NOT NULL, 
  [Total] [DATE] NOT NULL,
  [IdReservacion] [INT] NOT NULL, 
)
ALTER TABLE [dbo].[Factura] ADD CONSTRAINT PK_FACTURA PRIMARY KEY ([IdFactura])
ALTER TABLE [dbo].[Factura] ADD CONSTRAINT FK_RESERVACION FOREIGN KEY (IdReservacion) REFERENCES [dbo].[Reservacion]



--PAISES
INSERT INTO [dbo].[Pais] ([NombrePais]) VALUES ('Costa Rica')

--PROVINCIA
INSERT INTO [dbo].[Provincia]([NombreProvincia],[IdPais]) VALUES('Alajuela',1)

--CANTON
INSERT INTO [dbo].[Canton]([NombreCanton] ,[IdPais] ,[IdProvincia]) VALUES ('San Carlos',1,1)




--DROP DATABASE [ReservacionesTricolor];