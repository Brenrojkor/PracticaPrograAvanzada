USE u484426513_pac324;

CREATE TABLE G2_Usuarios (
	Id_Usuario INT AUTO_INCREMENT PRIMARY KEY,
    Usuario VARCHAR(45),
    Contraseña VARCHAR(80) NOT NULL
);

CREATE TABLE G2_Canciones (
	Id_Cancion INT PRIMARY KEY,
	Titulo VARCHAR(50) NOT NULL,
	Artista VARCHAR(100) NOT NULL,
	Id_Usuario INT, FOREIGN KEY (Id_Usuario) REFERENCES G2_Usuarios(Id_Usuario)
);

CREATE TABLE G2_Playlists (
    Id_Playlist INT AUTO_INCREMENT PRIMARY KEY,
    Nombre_Playlist VARCHAR(100) NOT NULL,
    Nombre_Creador VARCHAR(100) NOT NULL,
    Descripcion_Playlist VARCHAR(100) NOT NULL,
    Genero_Playlist VARCHAR(100) NOT NULL,
    Fecha_Creacion DATE
);

INSERT INTO G2_Usuarios (Id_Usuario, Usuario, Contraseña) VALUES 
	(1, 'Carlos', '19900515'),
	(2, 'Laura', '19850722'),
	(3, 'María', '19920310'),
	(4, 'Juan', '19881201'),
	(5, 'Ana', '19950918'),
	(6, 'Pedro', '19911103');
    
INSERT INTO G2_Canciones (Id_Cancion,Titulo, Artista, Id_Usuario ) VALUES 
	(1, 'Espresso', 'Sabrina Carpenter', 2),
	(2, 'Neva Play', 'Megan the Stallion ft RM', 5),
	(3, 'Black Out', 'Park Chanyeol', 3),
	(4, 'Burbujas de Cristal', 'Cartel de Santa', 1),
	(5, 'Older', 'Isabel LaRosa', 4),
	(6, 'Do Re Mi', 'blackbear', 6);

INSERT INTO G2_Playlists (Id_Playlist, Nombre_Playlist, Nombre_Creador, Descripcion_Playlist, Genero_Playlist, Fecha_Creacion) VALUES
	(1, '50 Cent Hits', 'Yarious', 'Mejores temas de 50 Cent', 'Hip hop', '2019-01-12'),
	(2, 'Gym Worksongs', 'Verónica', 'Música para hacer gym', 'Varios', '2020-02-29'),
	(3, 'Cinema Songs', 'Fabián', 'Música popular de películas', 'Varios', '2021-03-25'),
	(4, 'Korean Hits', 'Brenda', 'Canciones del género regional de Korea', 'K-pop', '2022-04-15'),
	(5, 'Classic Rock', 'Daniel', 'Rock clásico de los 60s/70s/80s', 'rock', '2023-05-19'),
	(6, 'World Music', 'Mariana', 'Música del mundo', 'worldbeat', '2024-06-21');
