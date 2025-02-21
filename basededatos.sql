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
    nombre NVARCHAR(100) UNIQUE NOT NULL
);

CREATE TABLE Generos (
    id INT IDENTITY(1,1) PRIMARY KEY,
    nombre NVARCHAR(50) UNIQUE NOT NULL
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
    caratula NVARCHAR(50)
);

CREATE TABLE VideojuegoCompania (
    fkIdVideojuego INT,
    fkIdCompania INT,
    PRIMARY KEY (fkIdVideojuego, fkIdCompania),
    FOREIGN KEY (fkIdVideojuego) REFERENCES Videojuegos(id) ON DELETE CASCADE,
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

-- Insertar usuarios
INSERT INTO Usuarios (Id, Username, Email, Contrasenia, Nombre, Apellido1, Apellido2, ProfilePic)
VALUES
(1, 'aaaaa', 'a@b.com', '123', NULL, NULL, NULL, NULL),
(2, 'eee', 'd@d.com', '123', NULL, NULL, NULL, NULL),
(3, 'eddd', 'f@g.com', '1234', NULL, NULL, NULL, NULL),
(4, '11', '1@a.com', '1', NULL, NULL, NULL, NULL);

-- Insertar videojuegos
INSERT INTO Videojuegos (Id, Titulo, Descripcion, AnioSalida, Pegi, Caratula)
VALUES
(1, 'The Legend of Zelda: Breath of the Wild', 'Un juego de acción y aventura en un mundo abierto.', '2017-03-03', 12, 'zelda_botw.jpg'),
(2, 'Elden Ring', 'Un RPG de acción ambientado en un mundo de fantasía.', '2022-02-25', 16, 'elden_ring.jpg'),
(3, 'Cyberpunk 2077', 'Un RPG futurista en un mundo abierto lleno de acción.', '2020-12-10', 18, 'cyberpunk_2077.jpg'),
(4, 'Red Dead Redemption 2', 'Un juego de mundo abierto en el salvaje oeste.', '2018-10-26', 18, 'rdr2.jpg'),
(5, 'Hollow Knight', 'Un metroidvania en 2D con un mundo profundo y desafiante.', '2017-02-24', 7, 'hollow_knight.jpg'),
(6, 'God of War', 'Kratos regresa en una historia mitológica nórdica.', '2018-04-20', 18, 'god_of_war.jpg'),
(7, 'Minecraft', 'Un juego de construcción y exploración en un mundo abierto.', '2011-11-18', 7, 'minecraft.jpg'),
(8, 'The Witcher 3: Wild Hunt', 'Un RPG de acción basado en la saga de Geralt de Rivia.', '2015-05-19', 18, 'witcher_3.jpg'),
(9, 'Dark Souls III', 'Un RPG de acción desafiante y oscuro.', '2016-04-12', 16, 'dark_souls_3.jpg'),
(10, 'Super Mario Odyssey', 'Mario explora diversos mundos en esta aventura de plataformas.', '2017-10-27', 3, 'super_mario_odyssey.jpg'),
(11, 'f1 manager', 'zzzzzzzzzz', '2025-02-19', 3, 'zzzzz');

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