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
	       Art.CreationDateTime
	FROM Blog.Article Art
	ORDER BY Art.CreationDateTime DESC
	OFFSET @PageSize * @PageNumber ROWS
	FETCH NEXT @PageSize ROWS ONLY;
END
GO