create table if not exists orderedproducts(
	Id bigserial not null,
	productId bigserial not null,
	orderId bigserial not null,
	Quantity int not null,
	primary key(Id),
	foreign key(productId) references products(Id),
	foreign key(orderId) references orders(Id)
);
alter sequence  orderedproducts_Id_seq restart with 4294967296;