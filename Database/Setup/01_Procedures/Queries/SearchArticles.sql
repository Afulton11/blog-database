USE BlogDatabase;
GO


CREATE OR ALTER PROCEDURE Blog.SearchArticles
(
	@SearchText NVARCHAR(510),
	@PageNumber INT = 0,
	@PageSize INT = 10
)
AS
BEGIN
	DECLARE @Search NVARCHAR(512) = '%' + @SearchText + '%'

	SELECT *
	FROM Blog.Article A
	WHERE A.DeletedAt IS NULL
		AND (
			A.Body LIKE @Search
			OR A.Title LIKE @Search
			OR A.[Description] LIKE @Search
		)
	ORDER BY A.CreationDateTime DESC
	OFFSET @PageNumber * @PageSize ROWS
	FETCH NEXT @PageSize ROWS ONLY
END
GO