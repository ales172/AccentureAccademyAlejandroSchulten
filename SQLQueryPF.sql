CREATE DATABASE ProyectoFinalSchultenAlejandro;
GO
USE ProyectoFinalSchultenAlejandro;
GO
CREATE TABLE Genero
(
	Id_Genero INT PRIMARY KEY IDENTITY (1,1),
	Genero varchar(50) UNIQUE
);
GO
CREATE TABLE Editorial
(
	Id_Editorial INT PRIMARY KEY IDENTITY (1,1),
	Editorial varchar(50) UNIQUE
);
GO
CREATE TABLE Libro
(
	Id_Libro INT PRIMARY KEY IDENTITY (1,1),
	ISBN varchar(22) UNIQUE,
	Titulo varchar(100) NOT NULL,
	Id_Genero INT NOT NULL,
	Id_Editorial INT NOT NULL,
	Imagen VARCHAR(100),
	Descripcion VARCHAR(5000),
	CONSTRAINT FK_Libro_Genero
		FOREIGN KEY (Id_Genero) 
		REFERENCES Genero(Id_Genero)
		ON DELETE CASCADE,
	CONSTRAINT FK_Libro_Editorial
		FOREIGN KEY (Id_Editorial) 
		REFERENCES Editorial(Id_Editorial)
		ON DELETE CASCADE,
	CONSTRAINT UQ_ISBN_LIBRO 
	    UNIQUE(ISBN,Titulo)
);
GO
CREATE TABLE Autor
(
	Id_Autor INT PRIMARY KEY IDENTITY (1,1),
	Nombre VARCHAR(100) UNIQUE
);
GO
CREATE TABLE EscritoPor
(
   Id_EscritoPor INT PRIMARY KEY IDENTITY(1,1),
   Id_Libro INT not null,
   Id_Autor INT not null,
   CONSTRAINT UQ_Autor_Libro UNIQUE (Id_Libro,Id_Autor),
   CONSTRAINT FK_Author FOREIGN KEY (Id_Autor)
                         REFERENCES Autor(Id_Autor),
   CONSTRAINT FK_Book FOREIGN KEY (Id_Libro)
                         REFERENCES Libro(Id_Libro)
);
SELECT * FROM Genero 
SELECT * FROM Autor
SELECT * FROM Libro
SELECT * FROM Editorial

SELECT 
l.Titulo, a.Nombre AS Autor, ed.Editorial   
FROM 
Libro l INNER JOIN EscritoPor es 
ON l.Id_Libro = es.Id_Libro 
INNER JOIN Autor a 
ON es.Id_Autor = a.Id_Autor 
INNER JOIN Editorial ed 
ON l.Id_Editorial = ed.Id_Editorial