CREATE TABLE TbAtmData(
	nAtmID int not null,
	sAtmName nvarchar(120) not null,
	nTotalAmount decimal (18,2) null,
	nCountThousand int null,
	nCountFiveHundred int null,
	nCountHundred int null,
	isActive bit not null,
	dCreate datetime null,
	nCreateID int null,
	dUpdate datetime null,
	nUpdateID int null,
);