USE BlogDatabase;
GO

CREATE OR ALTER PROCEDURE Blog.GetArticleById
(
	@ArticleId INT
)
AS (
	SELECT *
	FROM Blog.Article A
	WHERE A.ArticleId = @ArticleId
)
GO
