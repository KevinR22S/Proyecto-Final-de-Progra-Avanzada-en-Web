CREATE DATABASE Proyecto_Final_Web;
USE Proyecto_Final_Web;

-- Tabla de usuarios
CREATE TABLE usuarios (
    usuario_id INT IDENTITY(1,1) PRIMARY KEY,
    nombre_usuario VARCHAR(50) NOT NULL UNIQUE,
    contrasena_hash VARCHAR(255) NOT NULL,
    rol NVARCHAR(10) CHECK (rol IN ('admin', 'cliente')) NOT NULL,
    creado_en DATETIME DEFAULT GETDATE()
);
GO

-- Tabla de cartas
CREATE TABLE cartas (
    carta_id INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    descripcion TEXT,
    puntos_ataque INT,
    puntos_defensa INT,
    creado_en DATETIME DEFAULT GETDATE()
);
GO

-- Tabla de mazos (deck)
CREATE TABLE mazos (
    mazo_id INT IDENTITY(1,1) PRIMARY KEY,
    usuario_id INT FOREIGN KEY REFERENCES usuarios(usuario_id),
    nombre_mazo VARCHAR(100) NOT NULL,
    creado_en DATETIME DEFAULT GETDATE()
);
GO

-- Tabla de cartas en mazo
CREATE TABLE cartas_en_mazo (
    mazo_id INT FOREIGN KEY REFERENCES mazos(mazo_id),
    carta_id INT FOREIGN KEY REFERENCES cartas(carta_id),
    PRIMARY KEY (mazo_id, carta_id)
);
GO

-- Procedimientos almacenados para usuarios
CREATE PROCEDURE CrearUsuario 
    @nombre_usuario VARCHAR(50),
    @contrasena_hash VARCHAR(255),
    @rol NVARCHAR(10)
AS
BEGIN
    INSERT INTO usuarios (nombre_usuario, contrasena_hash, rol) 
    VALUES (@nombre_usuario, @contrasena_hash, @rol);
END;
GO

CREATE PROCEDURE ObtenerUsuario 
    @usuario_id INT
AS
BEGIN
    SELECT * FROM usuarios WHERE usuario_id = @usuario_id;
END;
GO

CREATE PROCEDURE ActualizarUsuario 
    @usuario_id INT,
    @nombre_usuario VARCHAR(50),
    @contrasena_hash VARCHAR(255),
    @rol NVARCHAR(10)
AS
BEGIN
    UPDATE usuarios
    SET nombre_usuario = @nombre_usuario, contrasena_hash = @contrasena_hash, rol = @rol
    WHERE usuario_id = @usuario_id;
END;
GO

CREATE PROCEDURE EliminarUsuario 
    @usuario_id INT
AS
BEGIN
    DELETE FROM usuarios WHERE usuario_id = @usuario_id;
END;
GO

-- Procedimientos almacenados para cartas
CREATE PROCEDURE CrearCarta 
    @nombre VARCHAR(100),
    @descripcion TEXT,
    @puntos_ataque INT,
    @puntos_defensa INT
AS
BEGIN
    INSERT INTO cartas (nombre, descripcion, puntos_ataque, puntos_defensa) 
    VALUES (@nombre, @descripcion, @puntos_ataque, @puntos_defensa);
END;
GO

CREATE PROCEDURE ObtenerCarta 
    @carta_id INT
AS
BEGIN
    SELECT * FROM cartas WHERE carta_id = @carta_id;
END;
GO

CREATE PROCEDURE ActualizarCarta 
    @carta_id INT,
    @nombre VARCHAR(100),
    @descripcion TEXT,
    @puntos_ataque INT,
    @puntos_defensa INT
AS
BEGIN
    UPDATE cartas
    SET nombre = @nombre, descripcion = @descripcion, puntos_ataque = @puntos_ataque, puntos_defensa = @puntos_defensa
    WHERE carta_id = @carta_id;
END;
GO

CREATE PROCEDURE EliminarCarta 
    @carta_id INT
AS
BEGIN
    DELETE FROM cartas WHERE carta_id = @carta_id;
END;
GO

-- Procedimientos almacenados para mazos
CREATE PROCEDURE CrearMazo 
    @usuario_id INT,
    @nombre_mazo VARCHAR(100)
AS
BEGIN
    INSERT INTO mazos (usuario_id, nombre_mazo) 
    VALUES (@usuario_id, @nombre_mazo);
END;
GO

CREATE PROCEDURE ObtenerMazo 
    @mazo_id INT
AS
BEGIN
    SELECT * FROM mazos WHERE mazo_id = @mazo_id;
END;
GO

CREATE PROCEDURE ActualizarMazo 
    @mazo_id INT,
    @nombre_mazo VARCHAR(100)
AS
BEGIN
    UPDATE mazos
    SET nombre_mazo = @nombre_mazo
    WHERE mazo_id = @mazo_id;
END;
GO

CREATE PROCEDURE EliminarMazo 
    @mazo_id INT
AS
BEGIN
    DELETE FROM mazos WHERE mazo_id = @mazo_id;
END;
GO

-- Procedimientos almacenados para cartas en mazo
CREATE PROCEDURE AgregarCartaAMazo 
    @mazo_id INT,
    @carta_id INT
AS
BEGIN
    INSERT INTO cartas_en_mazo (mazo_id, carta_id) 
    VALUES (@mazo_id, @carta_id);
END;
GO

CREATE PROCEDURE EliminarCartaDeMazo 
    @mazo_id INT,
    @carta_id INT
AS
BEGIN
    DELETE FROM cartas_en_mazo 
    WHERE mazo_id = @mazo_id AND carta_id = @carta_id;
END;
GO

-- Procedimiento de autenticación de usuario
CREATE PROCEDURE AutenticarUsuario 
    @nombre_usuario VARCHAR(50),
    @contrasena_hash VARCHAR(255)
AS
BEGIN
    DECLARE @rol_usuario NVARCHAR(10);

    SELECT @rol_usuario = rol
    FROM usuarios
    WHERE nombre_usuario = @nombre_usuario AND contrasena_hash = @contrasena_hash;

    IF @rol_usuario IS NOT NULL
    BEGIN
        SELECT 'Autenticación exitosa' AS mensaje, @rol_usuario AS rol;
    END
    ELSE
    BEGIN
        SELECT 'Autenticación fallida' AS mensaje;
    END
END;
GO

