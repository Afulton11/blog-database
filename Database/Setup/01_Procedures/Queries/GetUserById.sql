USE BlogDatabase;
GO

CREATE OR ALTER PROCEDURE Blog.GetUserById
	@UserId AS INT
AS
	SELECT *
	FROM Blog.[User] AS u
	WHERE u.UserId = @UserId;
GO