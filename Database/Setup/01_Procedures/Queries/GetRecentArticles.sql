USE BlogDatabase
GO

CREATE OR ALTER PROCEDURE Blog.GetRecentArticles
(
	@PageSize INT = 10,
	@PageNumber INT = 0
)
AS
BEGIN
	DECLARE @PublishedContentStatusId INT =
		(
			SELECT C.ContentStatusId
			FROM Blog.ContentStatus C
			WHERE C.[Name] = N'Published'
		)
	SELECT Art.ArticleId,
		   Art.AuthorID,
	       Art.Title,
	       Art.[Description],
	       Art.CreationDateTime
	FROM Blog.Article Art
	WHERE Art.DeletedAt IS NULL AND Art.ContentStatusId = @PublishedContentStatusId
	ORDER BY Art.CreationDateTime DESC
	OFFSET @PageSize * @PageNumber ROWS
	FETCH NEXT @PageSize ROWS ONLY;
END
GO