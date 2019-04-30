USE BlogDatabase;
GO

CREATE OR ALTER PROCEDURE Blog.FetchArticleCategories
AS
BEGIN
	SELECT *
	FROM Blog.ArticleCategory
END
GO