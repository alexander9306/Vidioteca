create database dbBiblioteca
go

use dbBiblioteca
go
--La imagen se guardara directamente en la BD para evitar extra configuraciones al gestor de Base de datos. 
--Una solucion mas recomendable es utilzar FileStream para guardar las rutas de las imagenes

create table actor(
	idactor int  identity(1,1) constraint pk_actor primary key not null,
	nombre varchar(100) not null,
	fechanac date not null,
	sexo char(1) not null check(sexo in('F','M'))
);
go
create table actor_foto(
	idfoto int constraint pk_actor_foto primary key not null,
	constraint fk_actor_foto FOREIGN KEY (idfoto) references actor(idactor) ON DELETE CASCADE,
	foto varbinary(max) null --Omitida la restriccion de tipo nulo para que se pueda crear la pelicula sin una foto en el momento
); 
go
create table pelicula(
	idpelicula int identity(1,1) constraint pk_pelicula primary key not null,
	titulo varchar(100) not null constraint uc_pelicula_titulo unique,
	genero varchar(60) not null,
	fechaestreno date not null
);
go
create table pelicula_foto(
	idfoto int constraint pk_pelicula_foto primary key not null,
	constraint fk_pelicula_foto FOREIGN KEY (idfoto) references pelicula(idpelicula) ON DELETE CASCADE,
	foto varbinary(max) null --Omitida la restriccion de tipo nulo para que se pueda crear la pelicula sin una foto en el momento
); 
go
create table elenco(
	idelenco int  identity(1,1) constraint pk_elenco primary key not null,
	idpelicula int not null,
	idactor int not null
	constraint fk_elenco_pelicula foreign key (idpelicula) 
	references pelicula (idpelicula),
	constraint fk_elenco_actor foreign key (idactor) 
	references actor(idactor),
	constraint uc_actor_pelicula unique (idactor,idpelicula)
);
go