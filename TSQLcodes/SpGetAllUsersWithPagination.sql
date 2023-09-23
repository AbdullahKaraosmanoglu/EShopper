alter PROCEDURE SpGetAllUsersWithPagination
    @PageNumber INT,
    @PageSize INT
AS
BEGIN
    DECLARE @Offset INT
    SET @Offset = (@PageNumber - 1) * @PageSize
    
    SELECT *,
    CASE 
        WHEN Gender = 1 THEN 'Kadýn'
        WHEN Gender = 0 THEN 'Erkek'
        ELSE 'Diðer'
    END AS GenderTypeText
    FROM TblUsers
    ORDER BY UserId
    OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY
END;
