USE BlogDatabase;
GO

CREATE OR ALTER PROCEDURE Blog.GetAllUsers
	@WithDeleted AS BIT = 0
AS
	SELECT *
	FROM Blog.[User] AS u
	WHERE
	(
		@WithDeleted = 0 AND u.DeletedAt IS NULL
		OR
		@WithDeleted = 1 AND (u.DeletedAt IS NULL OR u.DeletedAt IS NOT NULL)
	)
GO
