CREATE TABLE TbCustomer (
    nCustomerID int not null PRIMARY KEY,
	sCustomerCode nvarchar(10) not null,
	sSecurityCoden nvarchar(120) not null,
	nTitleID int not null,
	sFname nvarchar(100) not null,
	sLname nvarchar(100) not null,
	sCardID nvarchar(13) not null,
	sPhone nvarchar(10) not null,
	sAddress nvarchar(500) not null,
	sProfileUrl nvarchar(250) not null,
	sEmail nvarchar(100) not null,
	isActive bit not null,
	isDel bit not null,
	dCreate datetime null,
	nCreateID int null,
	dUpdate datetime null,
	nUpdateID int null,
	nTotalAmount decimal(18,2) null,
);