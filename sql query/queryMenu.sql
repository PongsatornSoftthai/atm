SELECT [nMenuID]
      ,[sMenuName]
      ,[IsHaveSub]
      ,[nHeadMenu]
      ,[sIcon]
      ,[sURL]
	  ,[nSortOrder]
	  ,
(CASE
WHEN nHeadMenu IS NULL THEN 1
ELSE 2
END) as nLevel
FROM [atmDB].[dbo].[TmMenu] t1
WHERE [IsActive] = 1;