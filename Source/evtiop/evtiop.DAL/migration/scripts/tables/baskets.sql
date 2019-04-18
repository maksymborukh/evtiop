create table baskets(
	id bigserial not null,
	customerId bigserial not null,
	Added timestamp not null,
	primary key(id),
	foreign key(customerId) references customers(id)
);
alter sequence baskets_id_seq restart with 4294967296;