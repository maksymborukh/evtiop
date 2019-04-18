create table products(
	Id bigserial not null,
	Name varchar(255) not null,
	Description text,
	Quantity int not null,
	Price int not null,
	ImageId bigserial not null,
	ManufacturerId bigserial not null,
	primary key(Id),
	foreign key(ManufacturerId) references manufacturer(Id)
);
ALTER SEQUENCE products_Id_seq RESTART WITH 4294967296;
