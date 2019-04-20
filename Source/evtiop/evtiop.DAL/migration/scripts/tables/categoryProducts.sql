create table if not exists categoryproducts(
	productId bigserial not null,
	categoryId bigserial not null,
	foreign key(productId) references products(Id),
	foreign key(categoryId) references categories(Id)
);