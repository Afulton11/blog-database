USE BlogDatabase;
GO


CREATE OR ALTER PROCEDURE Blog.GetArticleCategoryById
(
	@ArticleCategoryId INT
)
AS
BEGIN
	SELECT *
	FROM Blog.ArticleCategory Category
	WHERE Category.ArticleCategoryId = @ArticleCategoryId
END
GO