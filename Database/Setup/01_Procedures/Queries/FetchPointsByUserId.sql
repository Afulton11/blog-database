USE BlogDatabase;
GO

CREATE OR ALTER PROCEDURE Blog.FetchPointsByUserId
    @UserId AS INT
AS
    SELECT *
    FROM Blog.Point AS p
    WHERE p.UserId = @UserId
        AND p.ExpiresAt >= SYSDATETIME();
GO
