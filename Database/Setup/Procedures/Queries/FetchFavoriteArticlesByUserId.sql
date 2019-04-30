USE BlogDatabase;
GO

CREATE OR ALTER PROCEDURE Blog.FetchFavoriteArticlesByUserId
    @UserId AS INT
AS
    SELECT a.*
    FROM Blog.Favorite AS f
        INNER JOIN Blog.Article AS a
            ON f.ArticleId = a.ArticleId
    WHERE f.UserId = @UserId
        AND f.DeletedAt IS NULL
        AND a.DeletedAt IS NULL
GO
