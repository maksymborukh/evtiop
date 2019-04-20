create table if not exists manufacturers(
	Id bigserial not null,
	Name varchar(255) not null,
	ImageURL varchar(255),
	primary key(Id)
);
alter sequence manufacturers_Id_seq restart with 4294967296;