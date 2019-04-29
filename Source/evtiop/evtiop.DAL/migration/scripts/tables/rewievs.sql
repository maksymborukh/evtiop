create table if not exists reviews(
	id bigserial not null,
	ProductId bigserial not null,
	ReviewerId bigserial not null,
	Rating int not null,
	Text text,
	primary key(id),
	foreign key(productId) references products(id),
	foreign key(reviewerId) references customers(id)
);
alter sequence reviews_id_seq restart with 4294967296;