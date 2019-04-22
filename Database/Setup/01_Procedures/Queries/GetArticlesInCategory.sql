USE BlogDatabase
GO

CREATE OR ALTER PROCEDURE Blog.GetArticlesInCategory
(
	@CategoryName NVARCHAR(28),
	@PageSize INT,
	@PageNumber INT
)
AS
BEGIN
	DECLARE @PublishedStatusId INT = (SELECT S.ContentStatusId FROM Blog.ContentStatus S WHERE S.[Name] = N'Published');

	SELECT A.ArticleId,
		   A.AuthorId,
		   A.CreationDateTime,
		   A.Title,
		   A.[Description]
	FROM Blog.Article A
		INNER JOIN Blog.ArticleCategory C ON
			C.[Name] = @CategoryName
			AND C.ArticleCategoryId = A.CategoryId
	WHERE A.ContentStatusId = @PublishedStatusId
		AND A.DeletedAt IS NULL
	ORDER BY A.CreationDateTime DESC
	OFFSET @PageSize * @PageNumber ROWS
	FETCH NEXT @PageSize ROWS ONLY
END
GO