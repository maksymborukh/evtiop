create table basketproducts(
	basketId bigserial not null,
	productId bigserial not null,
	quantity int,
	foreign key(basketId) references baskets(id),
	foreign key(productId) references products(id)
);