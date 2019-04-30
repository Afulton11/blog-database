USE BlogDatabase;
GO

CREATE OR ALTER PROCEDURE Blog.GetArticleById
(
	@ArticleId INT
)
AS (
	SELECT *, C.[Name] AS ContentStatus
	FROM Blog.Article A
		INNER JOIN Blog.ContentStatus C ON C.ContentStatusId = A.ContentStatusId
	WHERE A.ArticleId = @ArticleId
)
GO
