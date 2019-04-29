create table if not exists productspecials(
	Id bigserial,
	ProductId bigserial not null,
	Available timestamp,
	ExpiryOn timestamp,
	Discount int,
	primary key(Id),
	foreign key (productId) references products(Id)
);
alter sequence productspecials_Id_seq restart with 4294967296;