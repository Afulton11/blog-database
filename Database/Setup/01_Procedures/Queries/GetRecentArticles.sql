USE BlogDatabase
GO

CREATE OR ALTER PROCEDURE Blog.GetRecentArticles
(
	@PageSize INT = 10,
	@PageNumber INT = 0
)
AS
BEGIN
	SELECT Art.ArticleId,
		   Art.AuthorID,
	       Art.Title,
	       Art.[Description],
	       Art.CreationDateTime,
		   C.[Name] AS ContentStatus
	FROM Blog.Article Art
		INNER JOIN Blog.ContentStatus C ON C.ContentStatusId = C.ContentStatusId
	WHERE Art.DeletedAt IS NULL AND C.[Name] = N'Published'
	ORDER BY Art.CreationDateTime DESC
	OFFSET @PageSize * @PageNumber ROWS
	FETCH NEXT @PageSize ROWS ONLY;
END
GO