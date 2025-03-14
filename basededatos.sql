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

CREATE TABLE Ideas (
    id INT IDENTITY(1,1) PRIMARY KEY,
    fkIdUsuario INT NOT NULL,
    titulo NVARCHAR(100) NOT NULL,
    descripcion NVARCHAR(500) NOT NULL,
    tipo NVARCHAR(50) NOT NULL,
    fechaCreacion DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (fkIdUsuario) REFERENCES Usuarios(id) ON DELETE CASCADE
);

-- Inserts de Companias (correcto, sin cambios)
INSERT INTO Companias (Nombre, url_imagen) VALUES 
('Nintendo', 'https://upload.wikimedia.org/wikipedia/commons/0/0d/Nintendo.svg'),          
('FromSoftware', 'src/assets/img/FromSoftware_logo.png'),      
('CD Projekt Red', 'https://upload.wikimedia.org/wikipedia/commons/5/5e/CD_Projekt_logo.svg'),    
('Rockstar Games', 'src/assets/img/Rockstar_North_logo.jpg'),    
('Team Cherry', 'src/assets/img//team_cherry.png'),       
('Santa Monica Studio', 'src/assets/img/santa_monica.jpg'), 
('Mojang', 'src/assets/img/Mojang_Studios_Logo.png'),           
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

-- Inserts de Usuarios (correcto, sin cambios)
INSERT INTO Usuarios (Username, Email, contraseña, Nombre, Apellido1, Apellido2, ProfilePic)
VALUES
('anonimo', 'anonimo@anonimo.com', 'anonimo123', NULL, NULL, NULL, NULL),
('aaaaa', 'a@b.com', '123', NULL, NULL, NULL, NULL),
('eee', 'd@d.com', '123', NULL, NULL, NULL, NULL),
('eddd', 'f@g.com', '1234', NULL, NULL, NULL, NULL),
('11', '1@a.com', '1', NULL, NULL, NULL, NULL),
('juan123', 'juan@example.com', 'pass123', 'Juan', 'Pérez', 'López', NULL),
('maria_gamer', 'maria@example.com', 'gamerpass', 'María', 'Gómez', 'Fernández', NULL),
('pedro99', 'pedro@example.com', 'pedro2024', 'Pedro', 'Rodríguez', 'Sánchez', NULL),
('luisa87', 'luisa@example.com', 'luisaPass', 'Luisa', 'Martínez', 'Ramírez', NULL),
('daniel_gt', 'daniel@example.com', 'danielPass', 'Daniel', 'Torres', 'Hernández', NULL),
('alex_dev', 'alex@example.com', 'alex1234', 'Alex', 'Luna', 'Moreno', NULL),
('sofia21', 'sofia@example.com', 'sofiaGamer', 'Sofía', 'Jiménez', 'Ortiz', NULL),
('carlos_x', 'carlos@example.com', 'carlosPass', 'Carlos', 'Ruiz', 'Vargas', NULL),
('paula_gaming', 'paula@example.com', 'paulaRocks', 'Paula', 'Navarro', 'Castro', NULL),
('ricardo12', 'ricardo@example.com', 'ricardo321', 'Ricardo', 'Santos', 'Díaz', NULL),
('angel_rpg', 'angel@example.com', 'rpgMaster', 'Ángel', 'Morales', 'Peña', NULL),
('valeria22', 'valeria@example.com', 'valeriaLove', 'Valeria', 'Fernández', 'Mendoza', NULL),
('jorge_dev', 'jorge@example.com', 'devJorge', 'Jorge', 'Hidalgo', 'Silva', NULL),
('lucia_gamer', 'lucia@example.com', 'lucia2024', 'Lucía', 'Rojas', 'Aguilar', NULL),
('esteban77', 'esteban@example.com', 'estebanGamer', 'Esteban', 'Soto', 'Lara', NULL),
('antonio_yt', 'antonio@example.com', 'antonio123', 'Antonio', 'Paredes', 'Salinas', NULL),
('gabriela_star', 'gabriela@example.com', 'gabrielaPass', 'Gabriela', 'Iglesias', 'Méndez', NULL),
('sebastian99', 'sebastian@example.com', 'sebastianGame', 'Sebastián', 'Ávila', 'Ponce', NULL),
('andrea_queen', 'andrea@example.com', 'andreaWin', 'Andrea', 'Carrillo', 'Guerrero', NULL),
('fernando_gt', 'fernando@example.com', 'fernandoPower', 'Fernando', 'Vera', 'Rosales', NULL),
('isabel_game', 'isabel@example.com', 'isabelSuper', 'Isabel', 'Bustamante', 'Escobar', NULL),
('victor_gamer', 'victor@example.com', 'victorMVP', 'Víctor', 'Miranda', 'Solano', NULL),
('martin_prog', 'martin@example.com', 'martinCode', 'Martín', 'Cabrera', 'Moya', NULL),
('adriana_fx', 'adriana@example.com', 'adrianaFX', 'Adriana', 'Giménez', 'Estrada', NULL),
('julio_90', 'julio@example.com', 'julioGames', 'Julio', 'Palacios', 'Sáenz', NULL),
('claudia_pc', 'claudia@example.com', 'claudia2024', 'Claudia', 'Herrera', 'Fuentes', NULL),
('mario_dev', 'mario@example.com', 'marioCode', 'Mario', 'Castillo', 'Valdez', NULL),
('rebecca_x', 'rebecca@example.com', 'rebeccaWin', 'Rebecca', 'Pinto', 'Maldonado', NULL),
('francisco_gam', 'francisco@example.com', 'franciscoPro', 'Francisco', 'Mejía', 'Trujillo', NULL),
('roberto_77', 'roberto@example.com', 'robertoPass', 'Roberto', 'Cortés', 'Ojeda', NULL);

-- Inserts de Videojuegos (corregido, relaciones de compañías)
INSERT INTO Videojuegos (Titulo, Descripcion, AnioSalida, Pegi, Caratula, FkIdCompania) VALUES 
('The Legend of Zelda: Breath of the Wild', 'Un juego de acción y aventura en un mundo abierto.', '2017-03-03', 12, 'src/assets/img/zelda_botw.jpg', 1),
('Elden Ring', 'Un RPG de acción ambientado en un mundo de fantasía.', '2022-02-25', 16, 'src/assets/img/elden_ring.jpg', 2),
('Cyberpunk 2077', 'Un RPG futurista en un mundo abierto lleno de peligros.', '2020-12-10', 18, 'src/assets/img/cyberpunk_2077.jpg', 3),
('Red Dead Redemption 2', 'Un juego de mundo abierto en el salvaje oeste.', '2018-10-26', 18, 'src/assets/img/rdr2.jpg', 4),
('Hollow Knight', 'Un metroidvania en 2D con un mundo profundo y desafiante.', '2017-02-24', 7, 'src/assets/img/hollow_knight.jpg', 5),
('God of War', 'Kratos regresa en una historia mitológica nórdica.', '2018-04-20', 18, 'src/assets/img/god_of_war.jpg', 6),
('Minecraft', 'Un juego de construcción y exploración en un mundo abierto.', '2011-11-18', 7, 'src/assets/img/minecraft.jpg', 7),
('The Witcher 3: Wild Hunt', 'Un RPG de acción basado en la saga de Geralt de Rivia.', '2015-05-19', 18, 'src/assets/img/witcher_3.jpg', 3),
('Dark Souls III', 'Un RPG de acción desafiante y oscuro.', '2016-04-12', 16, 'src/assets/img/dark_souls_3.jpg', 2), -- Corregido: era Bandai, ahora FromSoftware
('Super Mario Odyssey', 'Mario explora diversos mundos en esta aventura de plataformas.', '2017-10-27', 3, 'src/assets/img/super_mario_odyssey.jpg', 1),
('F1 Manager', 'Juego de simulación y gestión de equipos de Fórmula 1.', '2025-02-19', 3, 'src/assets/img/zzzzz.jpg', 11), -- Corregido: era Ubisoft, ahora EA (más probable para F1)
('Halo Infinite', 'FPS futurista con acción intensa.', '2021-12-08', 16, 'src/assets/img/halo_infinite.jpg', 14), -- Corregido: era Rockstar, ahora Microsoft
('GTA V', 'Mundo abierto con crimen y acción.', '2013-09-17', 18, 'src/assets/img/gta_v.jpg', 4), -- Corregido: era FromSoftware, ahora Rockstar
('Fortnite', 'Battle Royale multijugador.', '2017-07-21', 12, 'src/assets/img/fortnite.jpg', 22), -- Corregido: era CD Projekt, ahora Epic Games
('Among Us', 'Juego de deducción social.', '2018-06-15', 7, 'src/assets/img/among_us.jpg', 7), -- Corregido: era Santa Monica, ahora un indie tipo Mojang
('League of Legends', 'MOBA con estrategia en equipo.', '2009-10-27', 12, 'src/assets/img/lol.jpg', 22), -- Corregido: era Mojang, ahora similar a Epic (Riot no está pero Epic es más cercano)
('FIFA 23', 'Simulación de fútbol con licencias oficiales.', '2022-09-30', 3, 'src/assets/img/fifa_23.jpg', 11), -- Corregido: era Team Cherry, ahora EA
('Resident Evil Village', 'Horror de supervivencia.', '2021-05-07', 18, 'src/assets/img/re_village.jpg', 16), -- Corregido: era Bandai, ahora Capcom
('Doom Eternal', 'Shooter en primera persona con acción intensa.', '2020-03-20', 18, 'src/assets/img/doom_eternal.jpg', 10), -- Corregido: era Rockstar, ahora Bethesda
('Sekiro: Shadows Die Twice', 'Juego de acción con combate desafiante.', '2019-03-22', 18, 'src/assets/img/sekiro.jpg', 2), -- Corregido: era Ubisoft, ahora FromSoftware
('Horizon Forbidden West', 'Aventura en un mundo postapocalíptico.', '2022-02-18', 16, 'src/assets/img/horizon_fw.jpg', 13), -- Corregido: era Nintendo, ahora Sony
('Final Fantasy VII Remake', 'Remake del clásico RPG.', '2020-04-10', 16, 'src/assets/img/ff7_remake.jpg', 15), -- Corregido: era Nintendo, ahora Square Enix
('CyberConnect: Future Wars', 'Juego de estrategia en tiempo real.', '2023-08-25', 12, 'src/assets/img/cyberconnect_fw.jpg', 8), -- Corregido: era CD Projekt, ahora Bandai Namco
('The Sims 4', 'Simulación de vida con muchas opciones.', '2014-09-02', 12, 'src/assets/img/the_sims_4.jpg', 11), -- Corregido: era Team Cherry, ahora EA
('Valorant', 'Shooter táctico multijugador.', '2020-06-02', 16, 'src/assets/img/valorant.jpg', 22), -- Corregido: era Mojang, ahora Epic (similar a Riot)
('Elder Scrolls Online', 'MMORPG de mundo abierto.', '2014-04-04', 16, 'src/assets/img/eso.jpg', 10), -- Corregido: nombre corregido (era Elden Scrolls) y era FromSoftware, ahora Bethesda
('Call of Duty: Warzone', 'Shooter Battle Royale.', '2020-03-10', 18, 'src/assets/img/warzone.jpg', 12), -- Corregido: era Rockstar, ahora Activision
('The Last of Us Part II', 'Aventura con historia profunda.', '2020-06-19', 18, 'src/assets/img/tlou2.jpg', 13), -- Corregido: era Nintendo, ahora Sony
('Overwatch 2', 'Shooter multijugador por equipos.', '2022-10-04', 12, 'src/assets/img/overwatch2.jpg', 12), -- Corregido: era Team Cherry, ahora Activision
('Stardew Valley', 'Simulación de granja con exploración.', '2016-02-26', 7, 'src/assets/img/stardew_valley.jpg', 33), -- Corregido: era Santa Monica, ahora un indie más apropiado
('Monster Hunter Rise', 'Caza de monstruos en un mundo épico.', '2021-03-26', 12, 'src/assets/img/mh_rise.jpg', 16),
('No Mans Sky', 'Exploración espacial con galaxias infinitas.', '2016-08-09', 12, 'src/assets/img/no_mans_sky.jpg', 7),
('Terraria', 'Sandbox con aventura y construcción.', '2011-05-16', 7, 'src/assets/img/terraria.jpg', 33), -- Corregido: era Mojang, ahora indie similar
('Rainbow Six Siege', 'Shooter táctico en equipo.', '2015-12-01', 18, 'src/assets/img/r6_siege.jpg', 9), -- Corregido: era FromSoftware, ahora Ubisoft
('Persona 5', 'RPG con historia profunda y estrategia.', '2016-09-15', 16, 'src/assets/img/persona5.jpg', 17), -- Corregido: era Nintendo, ahora Sega (Atlus)
('Left 4 Dead 2', 'Shooter cooperativo contra zombies.', '2009-11-17', 18, 'src/assets/img/l4d2.jpg', 21), -- Corregido: era CD Projekt, ahora Valve
('Hitman 3', 'Sigilo y asesinatos estratégicos.', '2021-01-20', 18, 'src/assets/img/hitman3.jpg', 13), -- Corregido: era Team Cherry, ahora Sony/IO (cercano a Sony)
('Dead by Daylight', 'Juego de terror asimétrico.', '2016-06-14', 18, 'src/assets/img/dbd.jpg', 8), -- Corregido: era Santa Monica, ahora Bandai Namco (publisher)
('Little Nightmares II', 'Aventura de terror con puzzles.', '2021-02-11', 12, 'src/assets/img/ln2.jpg', 8), -- Corregido: era Bandai, sin cambios (correcto)
('Fall Guys', 'Carreras de obstáculos divertidas.', '2020-08-04', 3, 'src/assets/img/fall_guys.jpg', 22), -- Corregido: era Mojang, ahora Epic Games (actual dueño)
('Hades', 'Roguelike con acción y mitología.', '2020-09-17', 12, 'src/assets/img/hades.jpg', 33); -- Corregido: era Ubisoft, ahora Supergiant Games

-- Inserts de Comentarios (correcto, sin cambios)
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
(1, 11, 'Aburrido', 'No aporta nada nuevo.', '2023-11-11', 5, 20, 50),
(1, 2, 'Increíble juego', 'Una historia que me atrapó de principio a fin.', '2024-02-10', 10, 230, 5),
(2, 5, 'Visualmente impresionante', 'El diseño del mundo es espectacular.', '2024-01-15', 9, 180, 4),
(3, 8, 'Rejugable', 'Cada partida se siente diferente.', '2023-12-12', 8, 200, 3),
(4, 10, 'Meh', 'Esperaba más de este juego, un poco repetitivo.', '2023-11-07', 6, 120, 50),
(5, 6, 'Lo mejor de la saga', 'Si eres fan, este es un imprescindible.', '2023-10-18', 10, 270, 8),
(6, 1, 'Mejor juego del año', 'Sin palabras, lo mejor que he jugado en mucho tiempo.', '2023-09-20', 10, 320, 2),
(7, 9, 'Difícil pero satisfactorio', 'Los jefes son un reto, pero vale la pena.', '2023-08-22', 9, 190, 6),
(8, 4, 'Un clásico moderno', 'Definitivamente uno de los mejores de la generación.', '2023-07-14', 9, 250, 5),
(9, 3, 'No tan bueno', 'Esperaba mucho más, se siente incompleto.', '2023-06-10', 5, 100, 150),
(10, 7, 'Diversión pura', 'Este juego nunca se vuelve aburrido.', '2023-05-05', 9, 300, 1),
(11, 11, 'Monótono', 'Jugabilidad repetitiva, no lo recomendaría.', '2023-04-02', 4, 50, 70),
(12, 2, 'Historia épica', 'El final me dejó impactado.', '2023-03-22', 10, 220, 3),
(13, 6, 'Vale cada centavo', 'Gran inversión de tiempo.', '2023-02-18', 10, 210, 4),
(14, 8, 'Perfecto', 'No cambiaría nada de este juego.', '2023-01-11', 10, 400, 0),
(15, 1, 'Jugabilidad top', 'Los controles se sienten súper fluidos.', '2022-12-20', 9, 290, 2),
(16, 4, 'Historia emotiva', 'Uno de los mejores guiones en un videojuego.', '2022-11-08', 10, 275, 3),
(17, 9, 'Muy desafiante', 'No es para todos, pero me encantó.', '2022-10-17', 9, 150, 6),
(18, 3, 'Desilusión', 'El hype me mató las expectativas.', '2022-09-14', 5, 60, 120),
(19, 5, 'Mejor en cooperativo', 'Si juegas con amigos es más divertido.', '2022-08-21', 8, 230, 4),
(20, 7, 'Mejor DLC', 'Las expansiones valen la pena.', '2022-07-30', 10, 310, 1);

-- Inserts de Plataformas (correcto, sin cambios)
INSERT INTO Plataformas (nombre) VALUES 
('PC'),
('PlayStation 5'),
('PlayStation 4'),
('PlayStation 3'),
('PlayStation Vita'),
('Xbox Series X|S'),
('Xbox One'),
('Xbox 360'),
('Nintendo Switch'),
('Nintendo Wii U'),
('Nintendo 3DS'),
('Steam Deck'),
('Google Stadia'),
('Amazon Luna'),
('iOS'),
('Android'),
('Oculus Quest'),
('HTC Vive'),
('PlayStation VR'),
('Meta Quest 2'),
('GeForce Now'),
('Apple Arcade'),
('Google Play Games'),
('Linux'),
('MacOS'),
('Chromebook'),
('Nintendo DS'),
('Sega Dreamcast'),
('Atari VCS'),
('Commodore 64');

-- Inserts de Generos (correcto, sin cambios)
INSERT INTO Generos (nombre, url_imagen) VALUES 
('Acción', 'src/assets/img/accion.jpg'),
('Aventura', 'src/assets/img/aventura.jpg'),
('RPG', 'src/assets/img/rpg.jpg'),
('Mundo Abierto', 'src/assets/img/mundo_abierto.jpg'),
('Metroidvania', 'src/assets/img/metroidvania.jpg'),
('Estrategia', 'src/assets/img/estrategia.jpg'),
('Simulación', 'src/assets/img/simulacion.jpg'),
('Plataformas', 'src/assets/img/plataformas.jpg'),
('Shooter', 'src/assets/img/shooter.jpg'),
('Survival', 'src/assets/img/survival.jpg'),
('Battle Royale', 'src/assets/img/battle_royale.jpg'),
('Puzzle', 'src/assets/img/puzzle.jpg'),
('Roguelike', 'src/assets/img/roguelike.jpg'),
('Hack and Slash', 'src/assets/img/hack_and_slash.jpg'),
('MMORPG', 'src/assets/img/mmorpg.jpg'),
('Carreras', 'src/assets/img/carreras.jpg'),
('Deportes', 'src/assets/img/deportes.jpg'),
('Lucha', 'src/assets/img/lucha.jpg'),
('Terror', 'src/assets/img/terror.jpg'),
('Exploración', 'src/assets/img/exploracion.jpg'),
('Defensa de Torres', 'src/assets/img/defensa_de_torres.jpg'),
('Arcade', 'src/assets/img/arcade.jpg'),
('RTS', 'src/assets/img/rts.jpg'),
('Táctico', 'src/assets/img/tactico.jpg'),
('Futurista', 'src/assets/img/futurista.jpg'),
('Stealth', 'src/assets/img/stealth.jpg'),
('Narrativo', 'src/assets/img/narrativo.jpg'),
('Cyberpunk', 'src/assets/img/cyberpunk.jpg'),
('Western', 'src/assets/img/western.jpg'),
('Educativo', 'src/assets/img/educativo.jpg');

-- Inserts de VideojuegoGenero (corregido, con relaciones actualizadas)
INSERT INTO VideojuegoGenero (fkIdVideojuego, fkIdGenero) VALUES
-- The Legend of Zelda: Breath of the Wild
(1, 1), (1, 2), (1, 4), (1, 20),

-- Elden Ring
(2, 1), (2, 3), (2, 4), (2, 14),

-- Cyberpunk 2077
(3, 1), (3, 3), (3, 4), (3, 9), (3, 28),

-- Red Dead Redemption 2
(4, 1), (4, 2), (4, 4), (4, 29),

-- Hollow Knight
(5, 5), (5, 2), (5, 8),

-- God of War
(6, 1), (6, 2), (6, 14), (6, 27),

-- Minecraft
(7, 10), (7, 4), (7, 20),

-- The Witcher 3: Wild Hunt
(8, 1), (8, 3), (8, 4), (8, 27),

-- Dark Souls III
(9, 1), (9, 3), (9, 14),

-- Super Mario Odyssey
(10, 8), (10, 2), (10, 22),

-- F1 Manager
(11, 6), (11, 7), (11, 16),

-- Halo Infinite
(12, 9), (12, 1), (12, 25),

-- GTA V
(13, 1), (13, 2), (13, 4), (13, 9),

-- Fortnite
(14, 9), (14, 11), (14, 4),

-- Among Us
(15, 6), (15, 12), (15, 24),

-- League of Legends
(16, 6), (16, 1), (16, 24),

-- FIFA 23
(17, 17), (17, 7),

-- Resident Evil Village
(18, 10), (18, 19), (18, 1),

-- Doom Eternal
(19, 9), (19, 1), (19, 25),

-- Sekiro: Shadows Die Twice
(20, 1), (20, 3), (20, 14),

-- Horizon Forbidden West
(21, 1), (21, 2), (21, 4),

-- Final Fantasy VII Remake
(22, 1), (22, 3), (22, 27),

-- CyberConnect: Future Wars
(23, 6), (23, 23), (23, 25),

-- The Sims 4
(24, 7), (24, 4),

-- Valorant
(25, 9), (25, 24), (25, 1),

-- Elder Scrolls Online
(26, 3), (26, 4), (26, 15),

-- Call of Duty: Warzone
(27, 9), (27, 11), (27, 1),

-- The Last of Us Part II
(28, 1), (28, 19), (28, 2), (28, 27),

-- Overwatch 2
(29, 9), (29, 1), (29, 25),

-- Stardew Valley
(30, 7), (30, 2), (30, 20),

-- Monster Hunter Rise
(31, 1), (31, 3), (31, 10),

-- No Man's Sky
(32, 20), (32, 10), (32, 25),

-- Terraria
(33, 10), (33, 4), (33, 2), (33, 8),

-- Rainbow Six Siege
(34, 9), (34, 24), (34, 1),

-- Persona 5
(35, 3), (35, 27), (35, 24),

-- Left 4 Dead 2
(36, 9), (36, 1), (36, 19), (36, 10),

-- Hitman 3
(37, 1), (37, 26), (37, 12),

-- Dead by Daylight
(38, 19), (38, 10), (38, 1),

-- Little Nightmares II
(39, 2), (39, 19), (39, 12),

-- Fall Guys
(40, 8), (40, 22), (40, 11);

-- Inserts de VideojuegoPlataforma (corregido con relaciones actualizadas)
INSERT INTO VideojuegoPlataforma (fkIdVideojuego, fkIdPlataforma) VALUES
(1, 9), -- Zelda: Switch
(2, 1), (2, 2), (2, 3), (2, 6), (2, 7), -- Elden Ring: PC, PS5, PS4, Xbox Series, Xbox One
(3, 1), (3, 2), (3, 3), (3, 6), (3, 7), -- Cyberpunk: PC, PS5, PS4, Xbox Series, Xbox One
(4, 1), (4, 3), (4, 7), -- RDR2: PC, PS4, Xbox One
(5, 1), (5, 9), (5, 3), (5, 7), -- Hollow Knight: PC, Switch, PS4, Xbox One
(6, 2), (6, 3), -- God of War: PS5, PS4
(7, 1), (7, 2), (7, 3), (7, 6), (7, 7), (7, 9), (7, 15), (7, 16), -- Minecraft: PC, PS5, PS4, Xbox Series, Xbox One, Switch, iOS, Android
(8, 1), (8, 2), (8, 3), (8, 6), (8, 7), (8, 9), -- Witcher 3: PC, PS5, PS4, Xbox Series, Xbox One, Switch
(9, 1), (9, 3), (9, 7), -- Dark Souls III: PC, PS4, Xbox One
(10, 9), -- Super Mario Odyssey: Switch
(11, 1), (11, 2), (11, 6), -- F1 Manager: PC, PS5, Xbox Series
(12, 1), (12, 6), (12, 7), -- Halo Infinite: PC, Xbox Series, Xbox One
(13, 1), (13, 2), (13, 3), (13, 6), (13, 7), -- GTA V: PC, PS5, PS4, Xbox Series, Xbox One
(14, 1), (14, 2), (14, 3), (14, 6), (14, 7), (14, 9), (14, 15), (14, 16), -- Fortnite: PC, PS5, PS4, Xbox Series, Xbox One, Switch, iOS, Android
(15, 1), (15, 9), (15, 15), (15, 16), -- Among Us: PC, Switch, iOS, Android
(16, 1), (16, 15), (16, 16), -- League of Legends: PC, iOS, Android
(17, 1), (17, 2), (17, 3), (17, 6), (17, 7), (17, 9), -- FIFA 23: PC, PS5, PS4, Xbox Series, Xbox One, Switch
(18, 1), (18, 2), (18, 3), (18, 6), (18, 7), -- Resident Evil Village: PC, PS5, PS4, Xbox Series, Xbox One
(19, 1), (19, 2), (19, 3), (19, 6), (19, 7), (19, 9), -- Doom Eternal: PC, PS5, PS4, Xbox Series, Xbox One, Switch
(20, 1), (20, 2), (20, 3), (20, 6), (20, 7), -- Sekiro: PC, PS5, PS4, Xbox Series, Xbox One
(21, 2), (21, 3), -- Horizon Forbidden West: PS5, PS4
(22, 2), (22, 3), (22, 1), -- Final Fantasy VII Remake: PS5, PS4, PC
(23, 1), (23, 2), (23, 6), -- CyberConnect: PC, PS5, Xbox Series
(24, 1), (24, 2), (24, 3), (24, 6), (24, 7), -- Sims 4: PC, PS5, PS4, Xbox Series, Xbox One
(25, 1), -- Valorant: PC
(26, 1), (26, 2), (26, 3), (26, 6), (26, 7), -- Elder Scrolls Online: PC, PS5, PS4, Xbox Series, Xbox One
(27, 1), (27, 2), (27, 3), (27, 6), (27, 7), -- Warzone: PC, PS5, PS4, Xbox Series, Xbox One
(28, 2), (28, 3), -- Last of Us 2: PS5, PS4
(29, 1), (29, 2), (29, 3), (29, 6), (29, 7), (29, 9), -- Overwatch 2: PC, PS5, PS4, Xbox Series, Xbox One, Switch
(30, 1), (30, 2), (30, 3), (30, 6), (30, 7), (30, 9), (30, 15), (30, 16), -- Stardew Valley: PC, PS5, PS4, Xbox Series, Xbox One, Switch, iOS, Android
(31, 1), (31, 9), -- Monster Hunter Rise: PC, Switch
(32, 1), (32, 2), (32, 3), (32, 6), (32, 7), (32, 9), -- No Man's Sky: PC, PS5, PS4, Xbox Series, Xbox One, Switch
(33, 1), (33, 2), (33, 3), (33, 6), (33, 7), (33, 9), (33, 15), (33, 16), -- Terraria: PC, PS5, PS4, Xbox Series, Xbox One, Switch, iOS, Android
(34, 1), (34, 2), (34, 3), (34, 6), (34, 7), -- Rainbow Six Siege: PC, PS5, PS4, Xbox Series, Xbox One
(35, 1), (35, 2), (35, 3), (35, 9), -- Persona 5: PC, PS5, PS4, Switch
(36, 1), (36, 3), (36, 7), -- Left 4 Dead 2: PC, PS4, Xbox One
(37, 1), (37, 2), (37, 3), (37, 6), (37, 7), -- Hitman 3: PC, PS5, PS4, Xbox Series, Xbox One
(38, 1), (38, 2), (38, 3), (38, 6), (38, 7), (38, 9), -- Dead by Daylight: PC, PS5, PS4, Xbox Series, Xbox One, Switch
(39, 1), (39, 2), (39, 3), (39, 6), (39, 7), (39, 9), -- Little Nightmares II: PC, PS5, PS4, Xbox Series, Xbox One, Switch
(40, 1), (40, 2), (40, 3), (40, 6), (40, 7), (40, 9); -- Fall Guys: PC, PS5, PS4, Xbox Series, Xbox One, Switch

INSERT INTO Ideas (fkIdUsuario, titulo, descripcion, tipo, fechaCreacion) 
VALUES 
(1, 'Nueva interfaz de usuario', 'Rediseñar la UI para mejorar la experiencia de usuario.', 'Mejora', GETDATE()),
(2, 'Implementar dark mode', 'Agregar un modo oscuro para reducir la fatiga visual.', 'Mejora', GETDATE()),
(3, 'Chat en tiempo real', 'Incorporar un chat en la plataforma para que los usuarios puedan interactuar.', 'Servicio', GETDATE()),
(4, 'Integración con redes sociales', 'Permitir iniciar sesión y compartir contenido en redes sociales.', 'Producto', GETDATE()),
(5, 'Sistema de recompensas', 'Crear un sistema de puntos y logros para motivar a los usuarios.', 'Otro', GETDATE());
