USE BlogDatabase;
GO

CREATE OR ALTER PROCEDURE Blog.GetAllUsers
	@WithDeleted AS BIT = 0,
	@PageSize AS INT = 10,
	@PageNumber AS INT = 0
AS
	SELECT *
	FROM Blog.[User] AS u
	WHERE
	(
		@WithDeleted = 0 AND u.DeletedAt IS NULL
		OR
		@WithDeleted = 1 AND (u.DeletedAt IS NULL OR u.DeletedAt IS NOT NULL)
	)
	ORDER BY u.UserId ASC
	OFFSET @PageSize * @PageNumber ROWS
	FETCH NEXT @PageSize ROWS ONLY;
GO
