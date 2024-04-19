
-- Crear la base de datos
CREATE DATABASE UsersDB;
GO

-- Usar la base de datos recién creada
USE UsersDB;
GO

-- Crear la tabla de tipos de identificación
CREATE TABLE TipoDocumento (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Descripcion NVARCHAR(50)
);
GO

--Insertar los tipos de identificación
INSERT INTO TipoIdentificacion (Nombre) VALUES ('CC'), ('RC'), ('TI'), ('PA');
GO


-- Crear la tabla de usuarios
CREATE TABLE Usuario (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(50) NOT NULL,
    Apellido NVARCHAR(50) NOT NULL,
    TipoDocumentoId INT NOT NULL,
    Clave NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Documento BIGINT NOT NULL,
    FOREIGN KEY (TipoDocumentoId) REFERENCES TipoDocumento(Id)
);
GO
