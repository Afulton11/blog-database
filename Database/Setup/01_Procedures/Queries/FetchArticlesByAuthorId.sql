USE BlogDatabase;
GO

CREATE OR ALTER PROCEDURE Blog.FetchArticlesByAuthorId
    @AuthorId AS INT,
    @PageSize AS INT = 10,
    @PageNumber AS INT = 0
AS
    SELECT 
        a.*,
        cs.[Name] AS ContentStatus
    FROM Blog.Article AS a
    WHERE a.AuthorId = @AuthorId
        INNER JOIN Blog.ContentStatus AS cs
            ON cs.ContententStatusId = a.ContententStatusId
    ORDER BY a.ArticleId ASC
    OFFSET @PageSize * @PageNumber ROWS
    FETCH NEXT @PageSize ROWS ONLY;
GO
