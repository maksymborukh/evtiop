create table if not exists customers(
	Id bigserial not null,
	FirstName varchar(255) null,
	LastName varchar(255) null,
	EmailAddress varchar(255) not null,
	Phone int null,
	Password varchar(255) not null,
	RegistrationDate timestamp DEFAULT CURRENT_TIMESTAMP,
	ImageURL text null,
	primary key(Id),
	unique(EmailAddress)
	);
ALTER SEQUENCE customers_Id_seq RESTART WITH 4294967296;