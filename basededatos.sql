CREATE DATABASE VideojuegosDB;
USE VideojuegosDB;

CREATE TABLE Usuarios (
    id INT IDENTITY(1,1) PRIMARY KEY,
    username NVARCHAR(50) UNIQUE NOT NULL,
    email NVARCHAR(100) UNIQUE NOT NULL,
    contraseña NVARCHAR(255) NOT NULL
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
    descripcion NVARCHAR(500)
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
    texto NVARCHAR(500) NOT NULL,
    fecha DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (fkIdUsuario) REFERENCES Usuarios(id) ON DELETE CASCADE,
    FOREIGN KEY (fkIdVideojuego) REFERENCES Videojuegos(id) ON DELETE CASCADE
);
