create table if not exists addresses(
	Id bigserial not null,
	Street varchar(255) null,
	City varchar(255) null,
	State varchar(255) null,
	Country varchar(255) null,
	CustomerId bigserial not null,
	primary key(Id),
	foreign key(CustomerId) references customers(Id)
);
ALTER SEQUENCE addresses_Id_seq RESTART WITH 4294967296;