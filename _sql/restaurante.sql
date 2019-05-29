CREATE DATABASE crucero;
GO

USE crucero;
GO

CREATE TABLE restaurante(
	id int NOT NULL IDENTITY,
	nombre nvarchar(100) NOT NULL,
	horario nvarchar(100),
	principal int NOT NULL DEFAULT 0,
	capacidad int,
	CONSTRAINT pk_restaurante PRIMARY KEY(id)
);

CREATE TABLE jornada(
	id int NOT NULL IDENTITY,
	nombre nvarchar(100) NOT NULL,
	CONSTRAINT pk_jornada PRIMARY KEY(id)
);


CREATE TABLE menu(
	id int NOT NULL IDENTITY,
	nombre nvarchar(100) NOT NULL,
	restaurante int NOT NULL,
	jornada int NOT NULL,
	fecha datetime,
	tipo nvarchar(10) NOT NULL CHECK (tipo IN ('General', 'Especial')) DEFAULT 'General',
	CONSTRAINT pk_menu PRIMARY KEY(id),
	CONSTRAINT fk_menu_restaurante FOREIGN KEY(restaurante) REFERENCES restaurante (id) ON DELETE CASCADE,
	CONSTRAINT fk_menu_jornada FOREIGN KEY(jornada) REFERENCES jornada (id) ON DELETE CASCADE
);


CREATE TABLE categoria(
	id int NOT NULL IDENTITY,
	nombre nvarchar(100) NOT NULL,
	CONSTRAINT pk_categoria PRIMARY KEY(id)
);



CREATE TABLE plato(
	id int NOT NULL IDENTITY,
	nombre nvarchar(200) NOT NULL,
	descripcion nvarchar(300),
	menu int NOT NULL,
	categoria int NOT NULL,
	CONSTRAINT pk_plato PRIMARY KEY(id),
	CONSTRAINT fk_plato_menu FOREIGN KEY(menu) REFERENCES menu (id) ON DELETE CASCADE,
	CONSTRAINT fk_plato_categoria FOREIGN KEY(categoria) REFERENCES categoria (id) ON DELETE CASCADE
);


INSERT INTO restaurante (nombre,horario,principal, capacidad) VALUES ('Pizza del Oceano','7:00 am - 10:00 pm',0, 300);
INSERT INTO restaurante (nombre,horario,principal, capacidad) VALUES ('Arepa la concha del mar','7:00 am - 10:00 pm',0, 500);
INSERT INTO restaurante (nombre,horario,principal, capacidad) VALUES ('Restaurante principal','7:00 am - 12:00 pm',0, 200);

INSERT INTO jornada (nombre) VALUES ('Desayuno');
INSERT INTO jornada (nombre) VALUES ('Almuerzo');
INSERT INTO jornada (nombre) VALUES ('Cena');
INSERT INTO jornada (nombre) VALUES ('General');

INSERT INTO menu (nombre, restaurante, jornada, fecha, tipo) VALUES ('Menú desayuno', 3, 1, CONVERT(datetime, '2019-06-01'),'General');
INSERT INTO menu (nombre, restaurante, jornada, tipo) VALUES ('Menú pizza', 1, 4, 'General');

INSERT INTO categoria (nombre) VALUES ('Pizza tradicional');

INSERT INTO plato (nombre, descripcion, menu, categoria) VALUES ('Pizza pollo','TODO: Una descripción pendiente.',2,1);

/*SELECT p.id, p.nombre, p.descripcion, c.nombre as categoria
FROM plato p
INNER JOIN categoria c ON p.categoria = c.id;*/