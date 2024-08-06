CREATE TABLE TmMasterData (
	nMasterID int not null PRIMARY KEY,
	nTypeID int not null,
	MasterNane nvarchar(120) null,
	sValue nvarchar(120) null,
	nValue decimal (18,2) null,
	isActive bit not null,
);