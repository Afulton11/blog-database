USE BlogDatabase;
GO

CREATE OR ALTER PROCEDURE FetchArticleFavoriteCount(
    @ArticleId INT
)
AS
(
    SELECT COUNT(F.UserId)
    FROM Blog.Favorite F
    WHERE F.DeletedAt IS NULL
)
GO
