CREATE TABLE TbTransactionHistory (
	nHistoryID int not null PRIMARY KEY,
	nCustomerID int not null,
	nItemType int not null,
	nAmount	decimal (18,2) null,
	nRemainingAmount decimal (18,2) null,
	nCountThousand int null,
	nCountFiveHundred int null,
	nCountHundred int null,
	nStatusID int not null,
	sRemark nvarchar(500) null,
	dCreate datetime null,
);