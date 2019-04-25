CREATE OR ALTER PROCEDURE Blog.FetchArticlesByAuthorId
    @AuthorId AS INT,
    @PageSize AS INT = 10,
    @PageNumber AS INT = 0
AS
    SELECT *
    FROM Blog.Article AS a
    WHERE a.AuthorId = @AuthorId
    ORDER BY a.ArticleId ASC
    OFFSET @PageSize * @PageNumber ROWS
    FETCH NEXT @PageSize ROWS ONLY;
GO
