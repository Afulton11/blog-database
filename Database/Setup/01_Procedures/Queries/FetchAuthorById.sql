USE BlogDatabase;
GO

CREATE OR ALTER PROCEDURE Blog.FetchAuthorById
(
	@AuthorId INT
)
AS (
	SELECT *
	FROM Blog.Author A
	WHERE A.AuthorUserId = @AuthorId
)
GO
