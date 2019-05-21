create table if not exists helps(
	Id bigserial,
	FirstName varchar(255) not null,
	LastName varchar(255)not null,
	EmailAddress varchar(255) not null,
	Subject varchar(255) not null,
	Message text not null,
	CreatedAt timestamp DEFAULT CURRENT_TIMESTAMP not null,
	CustomerId bigserial not null,
	primary key(Id),
	foreign key(CustomerId) references customers(Id)
);
ALTER SEQUENCE helps_Id_seq RESTART WITH 4294967296;