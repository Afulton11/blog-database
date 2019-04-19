USE BlogDatabase;
GO

CREATE OR ALTER PROCEDURE Blog.GetUserById
	@UserId AS INT,
	@GetDeleted AS BIT = 0
AS
	SELECT *
	FROM Blog.[User] AS u
	WHERE u.UserId = @UserId
		-- conditional where clause
		-- https://csharpcornerplus.wordpress.com/2014/09/24/conditional-where-clause-in-sql-server/
		AND
		(
			@GetDeleted = 1 AND u.DeletedAt IS NOT NULL
			OR
			@GetDeleted = 0 AND u.DeletedAt IS NULL
		)
GO
