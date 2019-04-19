USE BlogDatabase;
GO

CREATE OR ALTER PROCEDURE Blog.GetTotalPointsByUserId
	@UserId AS INT,
	@ExpireDate AS DATETIME = NULL	-- optional: default current datetime
AS
	SET @ExpireDate = ISNULL(@ExpireDate, SYSDATETIME())

	SELECT ISNULL(SUM(p.[Value]), 0) AS TotalPoints
	FROM Blog.Point AS p
	WHERE p.UserId = @UserId
		AND p.ExpiresAt >= @ExpireDate;
GO