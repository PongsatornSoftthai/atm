CREATE TABLE TbAtmDataHistory(
	nItemID	int not null,
	nAtmID	int not null,
	nItemTypeID	int not null,
	nAmount	decimal (18,2) null,
	nCountThousand	int null,
	nCountFiveHundred	int null,
	nCountHundred	int null,
	dCreate	datetime null,
	nUpdateID	int null,
);