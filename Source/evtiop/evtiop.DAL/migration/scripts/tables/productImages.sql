create table productimages(
	Id bigserial not null,
	ProductId bigserial not null,
	Name varchar(255),
	ImageURL varchar(255),
	primary key(Id),
	foreign key(ProductId) references products(id)
);
alter sequence productimages_Id_seq restart with 4294967296;