create table if not exists orders(
	Id bigserial not null,
	CustomerId bigserial not null,
	AddressId bigserial not null,
	DeliveryDate timestamp default current_timestamp not null,
	PurchaseDate timestamp default current_timestamp not null,
	Status text,
	primary key(Id),
	foreign key(CustomerId) references customers(Id),
	foreign key(AddressId) references addresses(Id)
);
ALTER SEQUENCE orders_Id_seq RESTART WITH 4294967296;