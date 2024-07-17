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

-- Cartas Inspiradas en Mitologías Orientales (50)
INSERT INTO cartas (nombre, descripcion, puntos_ataque, puntos_defensa) 
VALUES
('Dragón Celestial', 'Invoca el poder del legendario Dragón Celestial de la mitología china. Este dragón otorga 300 puntos adicionales de ataque a todas las criaturas aliadas en el campo de batalla y reduce en 200 puntos el ataque de las criaturas enemigas. Si el Dragón Celestial es destruido, puedes sacrificar una criatura aliada para resucitarlo con la mitad de sus puntos de ataque originales.', 2500, 2000),
('Fénix de Fuego', 'El legendario Fénix de la mitología china resurge de sus cenizas. Cuando es destruido, vuelve al campo de batalla con el doble de sus puntos de ataque originales.', 1500, 1200),
('Samurái Honorable', 'El Samurái Honorable de Japón puede realizar un ataque adicional por turno. Si derrota a una criatura enemiga, aumenta sus puntos de ataque en 500.', 1800, 1700),
('Dragón de Jade', 'El Dragón de Jade de la mitología china otorga 500 puntos de defensa adicionales a todas las criaturas aliadas. Si es destruido, reduce a la mitad los puntos de ataque de las criaturas enemigas.', 2200, 2500),
('Tigre Blanco', 'El Tigre Blanco, guardián del oeste en la mitología china, puede atacar dos veces por turno y otorga 200 puntos adicionales de ataque a todas las criaturas aliadas.', 2000, 1800),
('Diosa Amaterasu', 'La diosa del sol en la mitología japonesa ilumina el campo de batalla. Todas las criaturas aliadas ganan 300 puntos de ataque adicionales.', 1900, 1600),
('Kitsune Encantadora', 'El zorro de nueve colas de la mitología japonesa puede hipnotizar a una criatura enemiga, haciéndola atacar a su propio dueño durante un turno.', 1700, 1500),
('Guerrero de Terracota', 'El Guerrero de Terracota de China otorga un escudo de 300 puntos de defensa a todas las criaturas aliadas. Si es destruido, invoca otro Guerrero de Terracota.', 1600, 2000),
('Hannya', 'El demonio Hannya de la mitología japonesa puede infligir miedo en las criaturas enemigas, reduciendo sus puntos de ataque en 300.', 2100, 1700),
('Dragón Azul', 'El Dragón Azul, guardián del este en la mitología china, puede invocar una tormenta que reduce en 400 puntos los ataques enemigos durante dos turnos.', 2300, 2100),
('Kappa', 'El espíritu del agua Kappa de la mitología japonesa puede atrapar a una criatura enemiga en el agua, impidiéndole atacar durante un turno.', 1400, 1600),
('León Guardián de Shishi', 'El León Guardián de Shishi protege a todas las criaturas aliadas, otorgándoles 300 puntos de defensa adicionales.', 1500, 2500),
('Kirin', 'El Kirin, criatura mítica japonesa, puede restaurar 500 puntos de vida a su dueño cada vez que derrota a una criatura enemiga.', 1800, 2000),
('Guerrero Yamato', 'El Guerrero Yamato de Japón puede desafiar a una criatura enemiga a un duelo, reduciendo sus puntos de defensa en 500.', 1900, 1800),
('Ryu', 'El dragón Ryu de la mitología japonesa puede volar sobre el campo de batalla, evitando ataques directos durante dos turnos.', 2100, 1900),
('Dios Susanoo', 'El dios de las tormentas en la mitología japonesa puede invocar un tifón que reduce en 400 puntos los ataques enemigos y otorga 200 puntos adicionales de ataque a todas las criaturas aliadas.', 2200, 2000),
('Tsukuyomi', 'El dios de la luna en la mitología japonesa puede invocar la noche, reduciendo en 300 puntos los ataques enemigos durante dos turnos.', 2000, 1800),
('Oni Destructor', 'El Oni Destructor de la mitología japonesa puede duplicar sus puntos de ataque durante un turno si sacrifica una criatura aliada.', 2500, 2200),
('Emperador Amarillo', 'El Emperador Amarillo de China otorga 500 puntos de vida adicionales a su dueño cada vez que una criatura aliada derrota a una criatura enemiga.', 1600, 1400),
('Guerrero Qin', 'El Guerrero Qin de China puede desafiar a una criatura enemiga a un duelo, reduciendo sus puntos de defensa en 500.', 1700, 1500),
('Shinigami', 'El dios de la muerte en la mitología japonesa puede eliminar instantáneamente a una criatura enemiga del campo de batalla si es invocado durante la noche.', 2400, 2000),
('Kannon', 'La diosa de la misericordia en la mitología japonesa puede restaurar 300 puntos de vida a su dueño cada vez que una criatura aliada es destruida.', 1500, 1300),
('Dios Raijin', 'El dios del trueno en la mitología japonesa puede lanzar un rayo que reduce en 400 puntos los ataques enemigos y otorga 200 puntos adicionales de ataque a todas las criaturas aliadas.', 2300, 2100),
('Buda de la Sabiduría', 'El Buda de la Sabiduría puede otorgar 300 puntos de defensa adicionales a todas las criaturas aliadas y restaurar 200 puntos de vida a su dueño cada vez que es atacado.', 1400, 1600),
('Genbu', 'El Genbu, guardián del norte en la mitología china, puede invocar una barrera de hielo que reduce en 300 puntos los ataques enemigos durante dos turnos.', 1700, 2000),
('Shisa', 'El Shisa, león-perro guardián de Okinawa, puede proteger a una criatura aliada de un ataque enemigo, reduciendo el daño en 500 puntos.', 1800, 1600),
('Tengu', 'El Tengu, espíritu del aire en la mitología japonesa, puede volar sobre el campo de batalla, evitando ataques directos durante dos turnos.', 2000, 1800),
('Bishamon', 'El dios de la guerra en la mitología japonesa puede duplicar sus puntos de ataque durante un turno si sacrifica una criatura aliada.', 2500, 2200),
('Guerrero Asura', 'El Guerrero Asura de la mitología hindú-japonesa puede atacar tres veces en un turno si sacrifica una criatura aliada.', 2700, 2400),
('Yurei', 'El espíritu Yurei de la mitología japonesa puede poseer a una criatura enemiga, reduciendo sus puntos de ataque en 300 y aumentando los propios en la misma cantidad.', 1600, 1400),
('Espíritu Kitsune', 'El Espíritu Kitsune puede transformarse en cualquier criatura aliada destruida, copiando sus puntos de ataque y defensa.', 1700, 1500),
('Dragón Negro', 'El Dragón Negro, guardián del sur en la mitología china, puede invocar una llama oscura que reduce en 400 puntos los ataques enemigos y otorga 200 puntos adicionales de ataque a todas las criaturas aliadas.', 2300, 2100),
('Guerrero Hwarang', 'El Guerrero Hwarang de Corea puede desafiar a una criatura enemiga a un duelo, reduciendo sus puntos de defensa en 500.', 1900, 1700),
('Hanzo el Ninja', 'El famoso ninja Hanzo de Japón puede atacar sin ser detectado, evitando el contraataque enemigo.', 2000, 1800),
('Golem de Hierro', 'El Golem de Hierro de China otorga 500 puntos de defensa adicionales a todas las criaturas aliadas.', 1800, 2500),
('Zanbato', 'El legendario espadachín de Japón puede atacar a todas las criaturas enemigas en el campo de batalla una vez por turno.', 2100, 1900),
('Dios Hachiman', 'El dios de la guerra en la mitología japonesa puede otorgar 300 puntos de ataque adicionales a todas las criaturas aliadas y reducir en 200 puntos los ataques enemigos.', 2200, 2000),
('Diosa Benten', 'La diosa del conocimiento en la mitología japonesa puede restaurar 500 puntos de vida a su dueño cada vez que una criatura aliada derrota a una criatura enemiga.', 1600, 1400),
('Dragón de Nieve', 'El Dragón de Nieve de la mitología china puede invocar una tormenta de nieve que reduce en 400 puntos los ataques enemigos durante dos turnos.', 2000, 2300),
('Espíritu de los Cerezos', 'El Espíritu de los Cerezos en flor de Japón otorga 200 puntos de vida adicionales a su dueño cada vez que una criatura aliada es destruida.', 1500, 1300),
('Dios Fujin', 'El dios del viento en la mitología japonesa puede invocar un vendaval que reduce en 400 puntos los ataques enemigos y otorga 200 puntos adicionales de ataque a todas las criaturas aliadas.', 2300, 2100),
('Guerrero Bushi', 'El Guerrero Bushi de Japón puede desafiar a una criatura enemiga a un duelo, reduciendo sus puntos de defensa en 500.', 1700, 1500),
('Tortuga Mística', 'La Tortuga Mística de la mitología china puede otorgar 300 puntos de defensa adicionales a todas las criaturas aliadas y reducir en 200 puntos los ataques enemigos.', 1800, 2000),
('Espectro de la Noche', 'El Espectro de la Noche de la mitología japonesa puede eliminar instantáneamente a una criatura enemiga del campo de batalla si es invocado durante la noche.', 2400, 2000),
('Diosa Izanami', 'La diosa creadora en la mitología japonesa puede restaurar 300 puntos de vida a su dueño cada vez que una criatura aliada es destruida.', 1500, 1300),
('Buda del Trueno', 'El Buda del Trueno puede lanzar un rayo que reduce en 400 puntos los ataques enemigos y otorga 200 puntos adicionales de ataque a todas las criaturas aliadas.', 2300, 2100),
('León Celestial', 'El León Celestial de la mitología china puede proteger a todas las criaturas aliadas, otorgándoles 300 puntos de defensa adicionales.', 1500, 2500),
('Guerrero Nioh', 'El Guerrero Nioh de Japón puede desafiar a una criatura enemiga a un duelo, reduciendo sus puntos de defensa en 500.', 1700, 1500),
('Dragón Dorado', 'El Dragón Dorado de la mitología china puede invocar una lluvia dorada que otorga 500 puntos adicionales de ataque a todas las criaturas aliadas.', 2000, 1800);
GO

-- Cartas de Apoyo, Trampas y Equipables (50)
INSERT INTO cartas (nombre, descripcion, puntos_ataque, puntos_defensa)
VALUES 
('Amuleto de Protección', 'Este amuleto otorga 500 puntos adicionales de defensa a la criatura equipada. Además, la criatura no puede ser destruida por efectos de cartas trampa.', 0, 0),
('Espada del Samurái', 'Esta espada legendaria otorga 700 puntos adicionales de ataque a la criatura equipada.', 0, 0),
('Escudo del Guerrero', 'Este escudo otorga 500 puntos adicionales de defensa a la criatura equipada. Además, reduce en 300 puntos los ataques de las criaturas enemigas que la enfrenten.', 0, 0),
('Flechas de Luz', 'Estas flechas mágicas reducen en 400 puntos los ataques de todas las criaturas enemigas durante dos turnos.', 0, 0),
('Poción de Vida', 'Esta poción restaura 1000 puntos de vida al jugador.', 0, 0),
('Trampa de Fuego', 'Activa esta carta cuando una criatura enemiga ataca. La criatura atacante recibe 500 puntos de daño.', 0, 0),
('Anillo de Poder', 'Este anillo otorga 300 puntos adicionales de ataque y defensa a la criatura equipada.', 0, 0),
('Escudo de Espinas', 'Este escudo inflige 300 puntos de daño a cualquier criatura enemiga que ataque a la criatura equipada.', 0, 0),
('Amuleto de Curación', 'Este amuleto restaura 500 puntos de vida al jugador cada vez que una criatura aliada es destruida.', 0, 0),
('Trampa de Hielo', 'Activa esta carta cuando una criatura enemiga ataca. La criatura atacante no puede atacar en el siguiente turno.', 0, 0),
('Espada de la Oscuridad', 'Esta espada otorga 600 puntos adicionales de ataque a la criatura equipada. Además, reduce en 200 puntos los ataques de todas las criaturas enemigas en el campo de batalla.', 0, 0),
('Escudo de la Luz', 'Este escudo otorga 400 puntos adicionales de defensa a la criatura equipada y restaura 300 puntos de vida al jugador cada vez que la criatura equipada es atacada.', 0, 0),
('Trampa de Relámpago', 'Activa esta carta cuando una criatura enemiga ataca. La criatura atacante recibe 400 puntos de daño y no puede atacar en el siguiente turno.', 0, 0),
('Cetro de Poder', 'Este cetro otorga 500 puntos adicionales de ataque y defensa a la criatura equipada. Además, permite atacar dos veces por turno.', 0, 0),
('Armadura de Dragón', 'Esta armadura otorga 700 puntos adicionales de defensa a la criatura equipada. Además, la criatura no puede ser destruida por efectos de cartas trampa.', 0, 0),
('Trampa de Viento', 'Activa esta carta cuando una criatura enemiga ataca. La criatura atacante pierde 300 puntos de ataque durante dos turnos.', 0, 0),
('Pergamino de Sabiduría', 'Este pergamino otorga 400 puntos adicionales de ataque a la criatura equipada y permite robar una carta adicional al inicio del turno.', 0, 0),
('Escudo de la Montaña', 'Este escudo otorga 500 puntos adicionales de defensa a la criatura equipada. Además, reduce en 200 puntos los ataques de las criaturas enemigas que la enfrenten.', 0, 0),
('Trampa de Tierra', 'Activa esta carta cuando una criatura enemiga ataca. La criatura atacante queda inmovilizada y no puede atacar ni defenderse en el siguiente turno.', 0, 0),
('Espada de los Dioses', 'Esta espada otorga 800 puntos adicionales de ataque a la criatura equipada. Además, permite atacar a todas las criaturas enemigas en el campo de batalla una vez por turno.', 0, 0),
('Armadura de Hielo', 'Esta armadura otorga 600 puntos adicionales de defensa a la criatura equipada. Además, reduce en 300 puntos los ataques de las criaturas enemigas que la enfrenten.', 0, 0),
('Amuleto de Velocidad', 'Este amuleto otorga 300 puntos adicionales de ataque y defensa a la criatura equipada y permite atacar dos veces por turno.', 0, 0),
('Trampa de Espadas', 'Activa esta carta cuando una criatura enemiga ataca. La criatura atacante recibe 500 puntos de daño y pierde 300 puntos de ataque.', 0, 0),
('Capa de Invisibilidad', 'Esta capa otorga 400 puntos adicionales de defensa a la criatura equipada y permite evitar un ataque directo por turno.', 0, 0),
('Trampa de Agua', 'Activa esta carta cuando una criatura enemiga ataca. La criatura atacante no puede atacar ni defenderse en el siguiente turno y pierde 200 puntos de ataque.', 0, 0),
('Espada de la Justicia', 'Esta espada otorga 500 puntos adicionales de ataque a la criatura equipada. Además, permite eliminar instantáneamente una criatura enemiga del campo de batalla una vez por turno.', 0, 0),
('Armadura del Guerrero', 'Esta armadura otorga 500 puntos adicionales de defensa a la criatura equipada. Además, restaura 300 puntos de vida al jugador cada vez que la criatura equipada es atacada.', 0, 0),
('Trampa de Veneno', 'Activa esta carta cuando una criatura enemiga ataca. La criatura atacante recibe 300 puntos de daño y pierde 200 puntos de ataque durante dos turnos.', 0, 0),
('Cetro de los Dioses', 'Este cetro otorga 600 puntos adicionales de ataque y defensa a la criatura equipada. Además, permite atacar dos veces por turno.', 0, 0),
('Escudo del Dragón', 'Este escudo otorga 700 puntos adicionales de defensa a la criatura equipada. Además, reduce en 300 puntos los ataques de las criaturas enemigas que la enfrenten.', 0, 0),
('Trampa de Fuego Fatuo', 'Activa esta carta cuando una criatura enemiga ataca. La criatura atacante pierde 500 puntos de ataque durante dos turnos.', 0, 0),
('Espada de la Luz', 'Esta espada otorga 500 puntos adicionales de ataque a la criatura equipada. Además, reduce en 200 puntos los ataques de todas las criaturas enemigas en el campo de batalla.', 0, 0),
('Escudo de la Sombra', 'Este escudo otorga 400 puntos adicionales de defensa a la criatura equipada y permite evitar un ataque directo por turno.', 0, 0),
('Trampa de la Oscuridad', 'Activa esta carta cuando una criatura enemiga ataca. La criatura atacante no puede atacar en el siguiente turno y pierde 200 puntos de ataque.', 0, 0),
('Cetro del Rey', 'Este cetro otorga 600 puntos adicionales de ataque y defensa a la criatura equipada. Además, permite atacar a todas las criaturas enemigas en el campo de batalla una vez por turno.', 0, 0),
('Amuleto de Energía', 'Este amuleto otorga 300 puntos adicionales de ataque y defensa a la criatura equipada y restaura 200 puntos de vida al jugador cada vez que la criatura equipada es atacada.', 0, 0),
('Trampa de Roca', 'Activa esta carta cuando una criatura enemiga ataca. La criatura atacante queda inmovilizada y pierde 300 puntos de ataque durante dos turnos.', 0, 0),
('Espada del Viento', 'Esta espada otorga 500 puntos adicionales de ataque a la criatura equipada. Además, reduce en 200 puntos los ataques de todas las criaturas enemigas en el campo de batalla.', 0, 0),
('Escudo del Trueno', 'Este escudo otorga 400 puntos adicionales de defensa a la criatura equipada y permite infligir 200 puntos de daño a cualquier criatura enemiga que la ataque.', 0, 0),
('Trampa de Arena', 'Activa esta carta cuando una criatura enemiga ataca. La criatura atacante queda inmovilizada y no puede atacar ni defenderse en el siguiente turno.', 0, 0),
('Cetro de Luz', 'Este cetro otorga 500 puntos adicionales de ataque y defensa a la criatura equipada. Además, restaura 300 puntos de vida al jugador cada vez que la criatura equipada derrota a una criatura enemiga.', 0, 0),
('Amuleto de Fuerza', 'Este amuleto otorga 300 puntos adicionales de ataque a la criatura equipada y permite atacar dos veces por turno.', 0, 0),
('Trampa de Vapor', 'Activa esta carta cuando una criatura enemiga ataca. La criatura atacante pierde 300 puntos de ataque durante dos turnos.', 0, 0),
('Espada del Fuego', 'Esta espada otorga 600 puntos adicionales de ataque a la criatura equipada. Además, inflige 200 puntos de daño a cualquier criatura enemiga que la ataque.', 0, 0),
('Escudo de la Tierra', 'Este escudo otorga 500 puntos adicionales de defensa a la criatura equipada. Además, reduce en 200 puntos los ataques de las criaturas enemigas que la enfrenten.', 0, 0),
('Trampa de Rayo', 'Activa esta carta cuando una criatura enemiga ataca. La criatura atacante recibe 400 puntos de daño y no puede atacar en el siguiente turno.', 0, 0),
('Cetro del Fuego', 'Este cetro otorga 600 puntos adicionales de ataque y defensa a la criatura equipada. Además, inflige 300 puntos de daño a cualquier criatura enemiga que la ataque.', 0, 0),
('Amuleto de Protección Divina', 'Este amuleto otorga 500 puntos adicionales de defensa a la criatura equipada. Además, la criatura equipada no puede ser destruida por efectos de cartas trampa.', 0, 0);
GO

-- Cartas de Campo (25)
INSERT INTO cartas (nombre, descripcion, puntos_ataque, puntos_defensa)
VALUES 
('Santuario del Dragón', 'Todas las criaturas tipo Dragón en el campo ganan 500 puntos de ataque y defensa.', 0, 0),
('Templo del Fénix', 'Todas las criaturas tipo Fénix en el campo pueden resucitar una vez con la mitad de sus puntos de ataque originales.', 0, 0),
('Campos de Batalla del Samurái', 'Todas las criaturas tipo Samurái en el campo ganan 300 puntos de ataque y pueden atacar dos veces por turno.', 0, 0),
('Bosque de los Espíritus', 'Todas las criaturas tipo Espíritu en el campo ganan 400 puntos de defensa y pueden evitar un ataque por turno.', 0, 0),
('Tierra de los Oni', 'Todas las criaturas tipo Oni en el campo ganan 500 puntos de ataque y pueden destruir instantáneamente una criatura enemiga una vez por turno.', 0, 0),
('Palacio de los Dioses', 'Todas las criaturas tipo Dios en el campo ganan 700 puntos de ataque y defensa.', 0, 0),
('Montañas del Dragón', 'Todas las criaturas tipo Dragón en el campo ganan 300 puntos de ataque y pueden volar, evitando ataques directos.', 0, 0),
('Valle del Tigre Blanco', 'Todas las criaturas tipo Tigre en el campo ganan 400 puntos de ataque y pueden atacar dos veces por turno.', 0, 0),
('Santuario de Amaterasu', 'Todas las criaturas aliadas ganan 300 puntos de ataque durante el día.', 0, 0),
('Cueva del Kappa', 'Todas las criaturas tipo Kappa en el campo pueden atrapar a una criatura enemiga por turno, impidiéndole atacar.', 0, 0),
('Fortaleza de los Guerreros', 'Todas las criaturas tipo Guerrero en el campo ganan 400 puntos de defensa y pueden desafiar a una criatura enemiga a un duelo.', 0, 0),
('Campo de los Fuegos Fatuos', 'Todas las criaturas enemigas pierden 300 puntos de ataque durante la noche.', 0, 0),
('Santuario de Benten', 'Todas las criaturas aliadas ganan 200 puntos de ataque y defensa y el jugador roba una carta adicional por turno.', 0, 0),
('Templo de Hachiman', 'Todas las criaturas tipo Guerrero en el campo ganan 300 puntos de ataque y defensa.', 0, 0),
('Isla de los Dragones', 'Todas las criaturas tipo Dragón en el campo ganan 500 puntos de ataque y pueden invocar una tormenta que reduce en 200 puntos los ataques enemigos.', 0, 0),
('Pradera de los Kirin', 'Todas las criaturas tipo Kirin en el campo ganan 400 puntos de defensa y pueden restaurar 500 puntos de vida al jugador cada vez que derrotan a una criatura enemiga.', 0, 0),
('Bosque de los Kitsune', 'Todas las criaturas tipo Kitsune en el campo pueden hipnotizar a una criatura enemiga por turno, haciéndola atacar a su propio dueño.', 0, 0),
('Santuario de Susanoo', 'Todas las criaturas tipo Dios en el campo ganan 300 puntos de ataque y defensa y pueden invocar un tifón que reduce en 300 puntos los ataques enemigos.', 0, 0),
('Montaña del Ryu', 'Todas las criaturas tipo Dragón en el campo ganan 400 puntos de ataque y pueden evitar ataques directos durante dos turnos.', 0, 0),
('Campo de los Guerreros Qin', 'Todas las criaturas tipo Guerrero en el campo ganan 300 puntos de ataque y defensa y pueden desafiar a una criatura enemiga a un duelo.', 0, 0),
('Valle de los Yurei', 'Todas las criaturas tipo Espíritu en el campo pueden poseer a una criatura enemiga, reduciendo sus puntos de ataque en 300.', 0, 0),
('Santuario de Tsukuyomi', 'Todas las criaturas aliadas ganan 200 puntos de ataque y defensa durante la noche.', 0, 0),
('Cueva del Genbu', 'Todas las criaturas tipo Genbu en el campo ganan 500 puntos de defensa y pueden invocar una barrera de hielo que reduce en 300 puntos los ataques enemigos.', 0, 0),
('Fortaleza de los Tengu', 'Todas las criaturas tipo Tengu en el campo ganan 400 puntos de ataque y pueden volar, evitando ataques directos.', 0, 0),
('Santuario de Bishamon', 'Todas las criaturas tipo Guerrero en el campo ganan 500 puntos de ataque y defensa y pueden duplicar sus puntos de ataque una vez por turno.', 0, 0);
GO

-- Cartas de Apoyo Relacionadas con el Campo (25)
INSERT INTO cartas (nombre, descripcion, puntos_ataque, puntos_defensa)
VALUES 
('Destrucción de Campo', 'Destruye todas las cartas de campo en el juego y reduce en 500 puntos los ataques de todas las criaturas enemigas durante dos turnos.', 0, 0),
('Reemplazo de Campo', 'Destruye la carta de campo actual y permite al jugador colocar una nueva carta de campo de su mano.', 0, 0),
('Fortalecimiento de Campo', 'Aumenta en 500 puntos los ataques y defensas de todas las criaturas aliadas mientras una carta de campo esté activa.', 0, 0),
('Debilitamiento de Campo', 'Reduce en 300 puntos los ataques y defensas de todas las criaturas enemigas mientras una carta de campo esté activa.', 0, 0),
('Protección de Campo', 'Las cartas de campo aliadas no pueden ser destruidas por efectos de cartas trampa.', 0, 0),
('Curación de Campo', 'Restaura 1000 puntos de vida al jugador cada vez que una carta de campo aliada es activada.', 0, 0),
('Ataque de Campo', 'Todas las criaturas aliadas ganan 400 puntos de ataque durante dos turnos mientras una carta de campo esté activa.', 0, 0),
('Defensa de Campo', 'Todas las criaturas aliadas ganan 400 puntos de defensa durante dos turnos mientras una carta de campo esté activa.', 0, 0),
('Campo de Fuego', 'Inflige 500 puntos de daño a todas las criaturas enemigas cuando una carta de campo aliada es activada.', 0, 0),
('Campo de Hielo', 'Inmoviliza a una criatura enemiga durante un turno cuando una carta de campo aliada es activada.', 0, 0),
('Campo de Relámpagos', 'Inflige 400 puntos de daño a todas las criaturas enemigas cada vez que una carta de campo aliada es activada.', 0, 0),
('Campo de Viento', 'Reduce en 300 puntos los ataques de todas las criaturas enemigas cada vez que una carta de campo aliada es activada.', 0, 0),
('Campo de la Vida', 'Restaura 500 puntos de vida al jugador cada vez que una carta de campo aliada es activada.', 0, 0),
('Campo de la Muerte', 'Destruye una criatura enemiga cada vez que una carta de campo aliada es activada.', 0, 0),
('Campo de la Luz', 'Todas las criaturas aliadas ganan 300 puntos de ataque y defensa mientras una carta de campo esté activa.', 0, 0),
('Campo de la Oscuridad', 'Todas las criaturas enemigas pierden 300 puntos de ataque y defensa mientras una carta de campo esté activa.', 0, 0),
('Campo de la Tierra', 'Todas las criaturas aliadas ganan 400 puntos de defensa mientras una carta de campo esté activa.', 0, 0),
('Campo de la Tormenta', 'Todas las criaturas enemigas pierden 400 puntos de ataque durante dos turnos mientras una carta de campo esté activa.', 0, 0),
('Campo de la Serenidad', 'Todas las criaturas aliadas ganan 500 puntos de defensa mientras una carta de campo esté activa.', 0, 0),
('Campo de la Fuerza', 'Todas las criaturas aliadas ganan 500 puntos de ataque mientras una carta de campo esté activa.', 0, 0),
('Campo de la Energía', 'Restaura 300 puntos de vida al jugador cada turno mientras una carta de campo esté activa.', 0, 0),
('Campo de la Destrucción', 'Inflige 300 puntos de daño a todas las criaturas enemigas cada turno mientras una carta de campo esté activa.', 0, 0),
('Campo de la Resurrección', 'Restaura una criatura aliada destruida cada vez que una carta de campo aliada es activada.', 0, 0),
('Campo de la Protección', 'Las criaturas aliadas no pueden ser destruidas por efectos de cartas trampa mientras una carta de campo esté activa.', 0, 0),
('Campo de la Dominación', 'Todas las criaturas enemigas pierden 500 puntos de ataque y defensa durante dos turnos mientras una carta de campo esté activa.', 0, 0);
GO

-- Cartas de Campeones o Personajes Principales (20)
INSERT INTO cartas (nombre, descripcion, puntos_ataque, puntos_defensa)
VALUES 
('Emperador Jade', 'El Emperador Jade, gobernante del cielo en la mitología china, otorga 1000 puntos de vida adicionales al jugador y 500 puntos de ataque a todas las criaturas aliadas.', 3000, 3000),
('Reina Xi Wangmu', 'La Reina Madre del Oeste en la mitología china, puede restaurar 1000 puntos de vida al jugador cada vez que derrota a una criatura enemiga.', 2800, 2600),
('Rey Yama', 'El Rey del Inframundo en la mitología china, puede destruir una criatura enemiga instantáneamente cada vez que es invocado.', 3200, 2700),
('Guerrero Sun Wukong', 'El Rey Mono de la mitología china, puede atacar tres veces por turno y duplicar sus puntos de ataque al inicio del combate.', 3500, 3000),
('Guerrero Guan Yu', 'El legendario general Guan Yu de China, otorga 700 puntos adicionales de ataque y defensa a todas las criaturas aliadas.', 3000, 3200),
('Diosa Nuwa', 'La diosa creadora en la mitología china, puede restaurar 500 puntos de vida al jugador y duplicar los puntos de ataque de todas las criaturas aliadas.', 2700, 2800),
('Diosa Chang e', 'La diosa de la luna en la mitología china, puede invocar una barrera que reduce en 500 puntos los ataques enemigos durante la noche.', 2900, 2500),
('Dios Erlang Shen', 'El dios guerrero de la mitología china, puede atacar dos veces por turno y reduce en 300 puntos los ataques enemigos.', 3100, 2700),
('Reina Himiko', 'La Reina Sacerdotisa de Japón, puede invocar espíritus que otorgan 400 puntos de ataque y defensa adicionales a todas las criaturas aliadas.', 2800, 2600),
('Guerrero Minamoto no Yoshitsune', 'El legendario guerrero samurái de Japón, puede desafiar a una criatura enemiga a un duelo y duplicar sus puntos de ataque.', 3200, 3000),
('Guerrero Miyamoto Musashi', 'El famoso espadachín de Japón, puede atacar dos veces por turno y reducir en 500 puntos los ataques enemigos.', 3300, 3100),
('Princesa Kaguya', 'La princesa de la luna en la mitología japonesa, puede restaurar 700 puntos de vida al jugador y duplicar los puntos de defensa de todas las criaturas aliadas.', 2600, 2800),
('Guerrero Tomoe Gozen', 'La legendaria guerrera samurái de Japón, otorga 600 puntos adicionales de ataque y defensa a todas las criaturas aliadas.', 2900, 2700),
('Diosa Amaterasu', 'La diosa del sol en la mitología japonesa, ilumina el campo de batalla, otorgando 500 puntos de ataque adicionales a todas las criaturas aliadas.', 3000, 2900),
('Dios Susanoo', 'El dios de las tormentas en la mitología japonesa, puede invocar un tifón que reduce en 400 puntos los ataques enemigos y otorga 300 puntos adicionales de ataque a todas las criaturas aliadas.', 3100, 3000),
('Dios Raijin', 'El dios del trueno en la mitología japonesa, puede lanzar un rayo que reduce en 400 puntos los ataques enemigos y otorga 200 puntos adicionales de ataque a todas las criaturas aliadas.', 3200, 2900),
('Diosa Izanami', 'La diosa creadora en la mitología japonesa, puede restaurar 500 puntos de vida al jugador y duplicar los puntos de defensa de todas las criaturas aliadas.', 2800, 2700),
('Dios Hachiman', 'El dios de la guerra en la mitología japonesa, puede duplicar sus puntos de ataque durante un turno si sacrifica una criatura aliada.', 3300, 3100),
('Diosa Tsukuyomi', 'La diosa de la luna en la mitología japonesa, puede invocar la noche, reduciendo en 300 puntos los ataques enemigos durante dos turnos.', 2900, 2700),
('Emperador Jimmu', 'El primer emperador de Japón, otorga 800 puntos adicionales de ataque y defensa a todas las criaturas aliadas.', 3100, 2900);
GO

-- Cartas Adicionales (29)
INSERT INTO cartas (nombre, descripcion, puntos_ataque, puntos_defensa)
VALUES 
('Llama de Fénix', 'Inflige 500 puntos de daño a todas las criaturas enemigas y restaura 300 puntos de vida al jugador.', 0, 0),
('Espada de Hades', 'Otorga 700 puntos adicionales de ataque a la criatura equipada y puede destruir una criatura enemiga una vez por turno.', 0, 0),
('Escudo de Atenea', 'Otorga 600 puntos adicionales de defensa a la criatura equipada y reduce en 300 puntos los ataques enemigos.', 0, 0),
('Trampa de Ares', 'Activa esta carta cuando una criatura enemiga ataca. La criatura atacante pierde 400 puntos de ataque y recibe 300 puntos de daño.', 0, 0),
('Campo de Zeus', 'Todas las criaturas aliadas ganan 500 puntos de ataque y defensa mientras esta carta de campo esté activa.', 0, 0),
('Amuleto de Hermes', 'Otorga 300 puntos adicionales de ataque y defensa a la criatura equipada y permite atacar dos veces por turno.', 0, 0),
('Trampa de Medusa', 'Activa esta carta cuando una criatura enemiga ataca. La criatura atacante queda petrificada y no puede atacar ni defenderse durante dos turnos.', 0, 0),
('Cetro de Poseidón', 'Otorga 500 puntos adicionales de ataque y defensa a la criatura equipada y permite invocar una tormenta que reduce en 300 puntos los ataques enemigos.', 0, 0),
('Armadura de Hefesto', 'Otorga 600 puntos adicionales de defensa a la criatura equipada y reduce en 200 puntos los ataques enemigos.', 0, 0),
('Trampa de Hades', 'Activa esta carta cuando una criatura enemiga ataca. La criatura atacante es destruida y el jugador roba una carta.', 0, 0),
('Campo de Poseidón', 'Todas las criaturas aliadas ganan 400 puntos de ataque y defensa mientras esta carta de campo esté activa.', 0, 0),
('Espada de Atenea', 'Otorga 600 puntos adicionales de ataque a la criatura equipada y permite destruir una carta trampa enemiga una vez por turno.', 0, 0),
('Escudo de Hades', 'Otorga 500 puntos adicionales de defensa a la criatura equipada y reduce en 200 puntos los ataques enemigos.', 0, 0),
('Trampa de Zeus', 'Activa esta carta cuando una criatura enemiga ataca. La criatura atacante recibe 500 puntos de daño y pierde 300 puntos de ataque.', 0, 0),
('Amuleto de Atenea', 'Otorga 400 puntos adicionales de ataque y defensa a la criatura equipada y permite evitar un ataque directo por turno.', 0, 0),
('Campo de Hades', 'Todas las criaturas aliadas ganan 500 puntos de defensa mientras esta carta de campo esté activa.', 0, 0),
('Trampa de Poseidón', 'Activa esta carta cuando una criatura enemiga ataca. La criatura atacante pierde 300 puntos de ataque y queda inmovilizada durante un turno.', 0, 0),
('Espada de Hermes', 'Otorga 500 puntos adicionales de ataque a la criatura equipada y permite atacar dos veces por turno.', 0, 0),
('Cetro de Hades', 'Otorga 600 puntos adicionales de ataque y defensa a la criatura equipada y permite destruir una criatura enemiga una vez por turno.', 0, 0),
('Campo de Hermes', 'Todas las criaturas aliadas ganan 300 puntos de ataque y defensa mientras esta carta de campo esté activa.', 0, 0),
('Trampa de Atenea', 'Activa esta carta cuando una criatura enemiga ataca. La criatura atacante queda inmovilizada y pierde 400 puntos de ataque durante dos turnos.', 0, 0),
('Amuleto de Zeus', 'Otorga 500 puntos adicionales de ataque y defensa a la criatura equipada y permite invocar un rayo que reduce en 300 puntos los ataques enemigos.', 0, 0),
('Espada de Ares', 'Otorga 700 puntos adicionales de ataque a la criatura equipada y permite atacar a todas las criaturas enemigas en el campo de batalla una vez por turno.', 0, 0),
('Escudo de Hermes', 'Otorga 400 puntos adicionales de defensa a la criatura equipada y permite evitar un ataque directo por turno.', 0, 0),
('Trampa de Hermes', 'Activa esta carta cuando una criatura enemiga ataca. La criatura atacante pierde 300 puntos de ataque y no puede atacar ni defenderse en el siguiente turno.', 0, 0),
('Campo de Ares', 'Todas las criaturas aliadas ganan 500 puntos de ataque mientras esta carta de campo esté activa.', 0, 0),
('Amuleto de Poseidón', 'Otorga 400 puntos adicionales de ataque y defensa a la criatura equipada y permite invocar una tormenta que reduce en 200 puntos los ataques enemigos.', 0, 0),
('Escudo de Ares', 'Otorga 600 puntos adicionales de defensa a la criatura equipada y reduce en 300 puntos los ataques enemigos.', 0, 0),
('Trampa de Atenea', 'Activa esta carta cuando una criatura enemiga ataca. La criatura atacante queda inmovilizada y pierde 400 puntos de ataque durante dos turnos.', 0, 0),
('Cetro de Zeus', 'Otorga 500 puntos adicionales de ataque y defensa a la criatura equipada y permite invocar un rayo que reduce en 300 puntos los ataques enemigos.', 0, 0);
GO