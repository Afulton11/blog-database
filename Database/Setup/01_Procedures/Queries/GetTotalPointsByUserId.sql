USE BlogDatabase;
GO

CREATE OR ALTER PROCEDURE Blog.GetTotalPointsByUserId
    @UserId AS INT
AS
    SELECT ISNULL(SUM(p.[Value]), 0) AS TotalPoints
    FROM Blog.Point AS p
    WHERE p.UserId = @UserId
        AND p.ExpiresAt >= SYSDATETIME();
GO