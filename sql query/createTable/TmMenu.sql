CREATE TABLE TmMenu(
	nMenuID int not null,
	sMenuName nvarchar(120) null,
	IsHaveSub Bit null,
	nHeadMenu int null,
	sIcon nvarchar(120) null,
	sURL nvarchar(250) null,
	nSortOrder int null,
	IsActive Bit null,
);