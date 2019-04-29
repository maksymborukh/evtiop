create table if not exists categories(
	Id bigserial not null,
	Name varchar(255) not null,
	ImageURL varchar(255) not null,
	SortOrder varchar(255) not null,
	primary key(Id)
);
alter sequence categories_Id_seq restart with 4294967296;