WITH MenuCTE AS (
    SELECT 
        m1.nMenuID,
        m1.sMenuName,
        m1.IsHaveSub,
        m1.nHeadMenu,
        m1.sIcon,
        m1.sURL,
        m1.nSortOrder,
        ISNULL(
            (SELECT m2.nMenuID, m2.sMenuName, m2.IsHaveSub, m2.nHeadMenu, m2.sIcon, m2.sURL, m2.nSortOrder
             FROM TmMenu m2 
             WHERE m2.nHeadMenu = m1.nMenuID 
             FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
            ), '{}') AS lstSubmenu
    FROM 
        TmMenu m1
    WHERE 
        m1.IsActive = 1 AND nHeadMenu IS NULL
	ORDER BY
		nSortOrder
)
SELECT 
    nMenuID,
    sMenuName,
    IsHaveSub,
    nHeadMenu,
    sIcon,
    sURL,
    nSortOrder,
    lstSubmenu
FROM 
    MenuCTE
WHERE
	nHeadMenu IS NULL
ORDER BY 
    nSortOrder;