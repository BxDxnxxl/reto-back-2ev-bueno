CREATE TABLE Usuarios (
    id INT IDENTITY(1,1) PRIMARY KEY,
    username NVARCHAR(50) UNIQUE NOT NULL,
    email NVARCHAR(100) UNIQUE NOT NULL,
    contraseña NVARCHAR(255) NOT NULL,
    nombre NVARCHAR(30),
    apellido1 NVARCHAR(30),
    apellido2 NVARCHAR(30),
    ProfilePic NVARCHAR(255)
);

CREATE TABLE Roles (
    id INT IDENTITY(1,1) PRIMARY KEY,
    nombre NVARCHAR(50) UNIQUE NOT NULL
);

CREATE TABLE UsuarioRol (
    fkIdUsuario INT,
    fkIdRol INT,
    PRIMARY KEY (fkIdUsuario, fkIdRol),
    FOREIGN KEY (fkIdUsuario) REFERENCES Usuarios(id) ON DELETE CASCADE,
    FOREIGN KEY (fkIdRol) REFERENCES Roles(id) ON DELETE CASCADE
);

CREATE TABLE Companias (
    id INT IDENTITY(1,1) PRIMARY KEY,
    nombre NVARCHAR(100) UNIQUE NOT NULL,
    url_imagen varchar(255)
);

CREATE TABLE Generos (
    id INT IDENTITY(1,1) PRIMARY KEY,
    nombre NVARCHAR(50) UNIQUE NOT NULL,
    url_imagen varchar(255)
);

CREATE TABLE Plataformas (
    id INT IDENTITY(1,1) PRIMARY KEY,
    nombre NVARCHAR(50) UNIQUE NOT NULL
);

CREATE TABLE Videojuegos (
    id INT IDENTITY(1,1) PRIMARY KEY,
    titulo NVARCHAR(100) NOT NULL,
    descripcion NVARCHAR(500),
    anioSalida DATE not null,
    pegi INT,
    caratula NVARCHAR(50),
    fkIdCompania INT,
    FOREIGN KEY (fkIdCompania) REFERENCES Companias(id) ON DELETE CASCADE
);

CREATE TABLE VideojuegoGenero (
    fkIdVideojuego INT,
    fkIdGenero INT,
    PRIMARY KEY (fkIdVideojuego, fkIdGenero),
    FOREIGN KEY (fkIdVideojuego) REFERENCES Videojuegos(id) ON DELETE CASCADE,
    FOREIGN KEY (fkIdGenero) REFERENCES Generos(id) ON DELETE CASCADE
);

CREATE TABLE VideojuegoPlataforma (
    fkIdVideojuego INT,
    fkIdPlataforma INT,
    PRIMARY KEY (fkIdVideojuego, fkIdPlataforma),
    FOREIGN KEY (fkIdVideojuego) REFERENCES Videojuegos(id) ON DELETE CASCADE,
    FOREIGN KEY (fkIdPlataforma) REFERENCES Plataformas(id) ON DELETE CASCADE
);

CREATE TABLE Comentarios (
    id INT IDENTITY(1,1) PRIMARY KEY,
    fkIdUsuario INT NOT NULL,
    fkIdVideojuego INT NOT NULL,
    titulo NVARCHAR(100) NOT NULL,
    texto NVARCHAR(500) NOT NULL,
    fecha DATETIME DEFAULT GETDATE(),
    valoracion INT NOT NULL CHECK (valoracion BETWEEN 1 AND 10),
    likes INT NOT NULL DEFAULT 0,
    dislikes INT NOT NULL DEFAULT 0,
    FOREIGN KEY (fkIdUsuario) REFERENCES Usuarios(id) ON DELETE CASCADE,
    FOREIGN KEY (fkIdVideojuego) REFERENCES Videojuegos(id) ON DELETE CASCADE
);

INSERT INTO Companias (Nombre, url_imagen) VALUES 
('Nintendo', 'https://upload.wikimedia.org/wikipedia/commons/0/0d/Nintendo.svg'),          
('FromSoftware', 'https://upload.wikimedia.org/wikipedia/commons/4/47/FromSoftware_logo.svg'),      
('CD Projekt Red', 'https://upload.wikimedia.org/wikipedia/commons/5/5e/CD_Projekt_logo.svg'),    
('Rockstar Games', 'https://upload.wikimedia.org/wikipedia/commons/8/82/Rockstar_North_logo.svg'),    
('Team Cherry', 'https://example.com/imagenes/team_cherry.jpg'),       
('Santa Monica Studio', 'https://example.com/imagenes/santa_monica.jpg'), 
('Mojang', 'https://upload.wikimedia.org/wikipedia/commons/8/8a/Mojang_Studios_Logo.svg'),           
('Bandai Namco', 'https://upload.wikimedia.org/wikipedia/commons/2/2e/Bandai_Namco_Entertainment_Logo.svg'),      
('Ubisoft', 'https://upload.wikimedia.org/wikipedia/commons/7/7c/Ubisoft_logo.svg'),
('Bethesda Softworks', 'https://upload.wikimedia.org/wikipedia/commons/0/00/Bethesda_Softworks_logo.svg'),
('Electronic Arts', 'https://upload.wikimedia.org/wikipedia/commons/5/52/Electronic_Arts_Logo.svg'),
('Activision Blizzard', 'https://upload.wikimedia.org/wikipedia/commons/1/17/Activision_Blizzard_logo.svg'),
('Sony Interactive Entertainment', 'https://upload.wikimedia.org/wikipedia/commons/6/6b/Sony_logo.svg'),
('Microsoft Studios', 'https://upload.wikimedia.org/wikipedia/commons/9/9b/Microsoft_Logo.svg'),
('Square Enix', 'https://upload.wikimedia.org/wikipedia/commons/e/e0/Square_Enix_logo.svg'),
('Capcom', 'https://upload.wikimedia.org/wikipedia/commons/4/44/Capcom_logo.svg'),
('Sega', 'https://upload.wikimedia.org/wikipedia/commons/5/54/SEGA_logo.svg'),
('Konami', 'https://upload.wikimedia.org/wikipedia/commons/2/25/Konami_Logo.svg'),
('2K Games', 'https://upload.wikimedia.org/wikipedia/commons/2/2c/2K_Games_logo.svg'),
('Warner Bros. Games', 'https://upload.wikimedia.org/wikipedia/commons/9/9a/Warner_Bros_Games_Logo.svg'),
('Valve Corporation', 'https://upload.wikimedia.org/wikipedia/commons/a/ab/Valve_logo.svg'),
('Epic Games', 'https://upload.wikimedia.org/wikipedia/commons/3/31/Epic_Games_logo.svg'),
('Insomniac Games', 'https://upload.wikimedia.org/wikipedia/commons/2/24/Insomniac_Games_logo.svg'),
('Monolith Soft', 'https://example.com/imagenes/monolith_soft.jpg'),
('BioWare', 'https://upload.wikimedia.org/wikipedia/commons/3/3b/BioWare_Logo.svg'),
('PlatinumGames', 'https://example.com/imagenes/platinum_games.jpg'),
('Tango Gameworks', 'https://example.com/imagenes/tango_gameworks.jpg'),
('Rebellion Developments', 'https://example.com/imagenes/rebellion.jpg'),
('THQ Nordic', 'https://example.com/imagenes/thq_nordic.jpg'),
('Remedy Entertainment', 'https://upload.wikimedia.org/wikipedia/commons/7/74/Remedy_Entertainment_logo.svg'),
('Obsidian Entertainment', 'https://upload.wikimedia.org/wikipedia/commons/a/aa/Obsidian_Entertainment_Logo.svg'),
('Larian Studios', 'https://example.com/imagenes/larian_studios.jpg'),
('Supergiant Games', 'https://example.com/imagenes/supergiant_games.jpg');

-- Insertar usuarios
INSERT INTO Usuarios (Id, Username, Email, Contrasenia, Nombre, Apellido1, Apellido2, ProfilePic)
VALUES
(1, 'aaaaa', 'a@b.com', '123', NULL, NULL, NULL, NULL),
(2, 'eee', 'd@d.com', '123', NULL, NULL, NULL, NULL),
(3, 'eddd', 'f@g.com', '1234', NULL, NULL, NULL, NULL),
(4, '11', '1@a.com', '1', NULL, NULL, NULL, NULL);

-- Insertar videojuegos
INSERT INTO Videojuegos (Titulo, Descripcion, AnioSalida, Pegi, Caratula, FkIdCompania) VALUES 
('The Legend of Zelda: Breath of the Wild', 'Un juego de acción y aventura en un mundo abierto.', '2017-03-03', 12, 'zelda_botw.jpg', 1),
('Elden Ring', 'Un RPG de acción ambientado en un mundo de fantasía.', '2022-02-25', 16, 'elden_ring.jpg', 2),
('Cyberpunk 2077', 'Un RPG futurista en un mundo abierto lleno de peligros.', '2020-12-10', 18, 'cyberpunk_2077.jpg', 3),
('Red Dead Redemption 2', 'Un juego de mundo abierto en el salvaje oeste.', '2018-10-26', 18, 'rdr2.jpg', 4),
('Hollow Knight', 'Un metroidvania en 2D con un mundo profundo y desafiante.', '2017-02-24', 7, 'hollow_knight.jpg', 5),
('God of War', 'Kratos regresa en una historia mitológica nórdica.', '2018-04-20', 18, 'god_of_war.jpg', 6),
('Minecraft', 'Un juego de construcción y exploración en un mundo abierto.', '2011-11-18', 7, 'minecraft.jpg', 7),
('The Witcher 3: Wild Hunt', 'Un RPG de acción basado en la saga de Geralt de Rivia.', '2015-05-19', 18, 'witcher_3.jpg', 3),
('Dark Souls III', 'Un RPG de acción desafiante y oscuro.', '2016-04-12', 16, 'dark_souls_3.jpg', 8),
('Super Mario Odyssey', 'Mario explora diversos mundos en esta aventura de plataformas.', '2017-10-27', 3, 'super_mario_odyssey.jpg', 1),
('F1 Manager', 'Juego de simulación y gestión de equipos de Fórmula 1.', '2025-02-19', 3, 'zzzzz.jpg', 9);


-- Insertar comentarios
INSERT INTO Comentarios (fkIdUsuario, fkIdVideojuego, Titulo, Texto, Fecha, Valoracion, Likes, Dislikes)
VALUES
(1, 1, 'Increíble', 'Un juego innovador y hermoso.', '2023-01-10', 10, 200, 5),
(1, 2, 'Obra maestra', 'El mejor RPG de mundo abierto.', '2023-02-15', 9, 150, 3),
(1, 3, 'Decepción', 'Esperaba más, muchos bugs.', '2023-03-12', 6, 50, 100),
(1, 4, 'Una joya', 'Historia espectacular y un mundo vivo.', '2023-04-01', 9, 180, 4),
(1, 5, 'Juegazo', 'Un metroidvania brutalmente bien hecho.', '2023-05-06', 9, 120, 2),
(1, 6, 'Gran historia', 'Kratos en su mejor momento.', '2023-06-18', 10, 170, 7),
(1, 7, 'Clásico eterno', 'Siempre disfrutable.', '2023-07-20', 9, 200, 1),
(1, 8, 'El mejor RPG', 'Historia, jugabilidad y mundo espectaculares.', '2023-08-21', 10, 190, 2),
(1, 9, 'Desafiante', 'Un juego difícil pero muy satisfactorio.', '2023-09-15', 9, 160, 5),
(1, 10, 'Diversión garantizada', 'Mario en su máxima expresión.', '2023-10-05', 9, 140, 3),
(1, 11, 'Aburrido', 'No aporta nada nuevo.', '2023-11-11', 5, 20, 50);

   

INSERT INTO Plataformas (nombre) VALUES 
('PC'),
('PlayStation 5'),
('PlayStation 4'),
('Xbox Series X|S'),
('Xbox One'),
('Nintendo Switch'),
('Steam Deck');

INSERT INTO Generos (nombre, url_imagen) VALUES 
('Acción', 'https://example.com/imagenes/accion.jpg'),
('Aventura', 'https://example.com/imagenes/aventura.jpg'),
('RPG', 'https://example.com/imagenes/rpg.jpg'),
('Mundo Abierto', 'https://example.com/imagenes/mundo_abierto.jpg'),
('Metroidvania', 'https://example.com/imagenes/metroidvania.jpg'),
('Estrategia', 'https://example.com/imagenes/estrategia.jpg'),
('Simulación', 'https://example.com/imagenes/simulacion.jpg'),
('Plataformas', 'https://example.com/imagenes/plataformas.jpg'),
('Shooter', 'https://example.com/imagenes/shooter.jpg'),
('Survival', 'https://example.com/imagenes/survival.jpg'),
('Battle Royale', 'https://example.com/imagenes/battle_royale.jpg'),
('Puzzle', 'https://example.com/imagenes/puzzle.jpg'),
('Roguelike', 'https://example.com/imagenes/roguelike.jpg'),
('Hack and Slash', 'https://example.com/imagenes/hack_and_slash.jpg'),
('MMORPG', 'https://example.com/imagenes/mmorpg.jpg'),
('Carreras', 'https://example.com/imagenes/carreras.jpg'),
('Deportes', 'https://example.com/imagenes/deportes.jpg'),
('Lucha', 'https://example.com/imagenes/lucha.jpg'),
('Terror', 'https://example.com/imagenes/terror.jpg'),
('Exploración', 'https://example.com/imagenes/exploracion.jpg'),
('Defensa de Torres', 'https://example.com/imagenes/defensa_de_torres.jpg'),
('Arcade', 'https://example.com/imagenes/arcade.jpg'),
('RTS', 'https://example.com/imagenes/rts.jpg'),
('Táctico', 'https://example.com/imagenes/tactico.jpg'),
('Futurista', 'https://example.com/imagenes/futurista.jpg'),
('Stealth', 'https://example.com/imagenes/stealth.jpg'),
('Narrativo', 'https://example.com/imagenes/narrativo.jpg'),
('Cyberpunk', 'https://example.com/imagenes/cyberpunk.jpg'),
('Western', 'https://example.com/imagenes/western.jpg'),
('Educativo', 'https://example.com/imagenes/educativo.jpg');


INSERT INTO VideojuegoGenero (fkIdVideojuego, fkIdGenero) VALUES
(1, 1), -- The Legend of Zelda: Breath of the Wild - Acción
(1, 2), -- The Legend of Zelda: Breath of the Wild - Aventura
(1, 4), -- The Legend of Zelda: Breath of the Wild - Mundo Abierto

(2, 1), -- Elden Ring - Acción
(2, 2), -- Elden Ring - Aventura
(2, 3), -- Elden Ring - RPG
(2, 4), -- Elden Ring - Mundo Abierto

(3, 3), -- Cyberpunk 2077 - RPG
(3, 4), -- Cyberpunk 2077 - Mundo Abierto
(3, 9), -- Cyberpunk 2077 - Shooter

(4, 2), -- Red Dead Redemption 2 - Aventura
(4, 4), -- Red Dead Redemption 2 - Mundo Abierto
(4, 1), -- Red Dead Redemption 2 - Acción

(5, 5), -- Hollow Knight - Metroidvania
(5, 2), -- Hollow Knight - Aventura

(6, 1), -- God of War - Acción
(6, 2), -- God of War - Aventura

(7, 10), -- Minecraft - Survival
(7, 4), -- Minecraft - Mundo Abierto

(8, 1), -- The Witcher 3: Wild Hunt - Acción
(8, 3), -- The Witcher 3: Wild Hunt - RPG
(8, 4), -- The Witcher 3: Wild Hunt - Mundo Abierto

(9, 1), -- Dark Souls III - Acción
(9, 3), -- Dark Souls III - RPG

(10, 8), -- Super Mario Odyssey - Plataformas
(10, 2), -- Super Mario Odyssey - Aventura

(11, 6), -- F1 Manager - Estrategia
(11, 7); -- F1 Manager - Simulación

INSERT INTO VideojuegoPlataforma (fkIdVideojuego, fkIdPlataforma) VALUES
(1, 6),  -- The Legend of Zelda: Breath of the Wild - Nintendo Switch
(2, 2),  -- Elden Ring - PlayStation 5
(2, 3),  -- Elden Ring - PlayStation 4
(2, 4),  -- Elden Ring - Xbox Series X|S
(2, 5),  -- Elden Ring - Xbox One
(2, 1),  -- Elden Ring - PC

(3, 1),  -- Cyberpunk 2077 - PC
(3, 3),  -- Cyberpunk 2077 - PlayStation 4
(3, 4),  -- Cyberpunk 2077 - Xbox Series X|S
(3, 5),  -- Cyberpunk 2077 - Xbox One

(4, 3),  -- Red Dead Redemption 2 - PlayStation 4
(4, 5),  -- Red Dead Redemption 2 - Xbox One
(4, 1),  -- Red Dead Redemption 2 - PC

(5, 1),  -- Hollow Knight - PC
(5, 6),  -- Hollow Knight - Nintendo Switch

(6, 2),  -- God of War - PlayStation 5
(6, 3),  -- God of War - PlayStation 4

(7, 1),  -- Minecraft - PC
(7, 6),  -- Minecraft - Nintendo Switch
(7, 2),  -- Minecraft - PlayStation 5
(7, 4),  -- Minecraft - Xbox Series X|S

(8, 1),  -- The Witcher 3: Wild Hunt - PC
(8, 3),  -- The Witcher 3: Wild Hunt - PlayStation 4
(8, 5),  -- The Witcher 3: Wild Hunt - Xbox One

(9, 1),  -- Dark Souls III - PC
(9, 3),  -- Dark Souls III - PlayStation 4
(9, 5),  -- Dark Souls III - Xbox One

(10, 6), -- Super Mario Odyssey - Nintendo Switch

(11, 1), -- F1 Manager - PC
(11, 2), -- F1 Manager - PlayStation 5
(11, 4); -- F1 Manager - Xbox Series X|S

