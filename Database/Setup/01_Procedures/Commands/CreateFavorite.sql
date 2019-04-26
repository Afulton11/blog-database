USE BlogDatabase;
GO

CREATE OR ALTER PROCEDURE Blog.CreateFavorite(
    @UserId INT,
    @ArticleId INT
) AS
BEGIN
    INSERT Blog.Favorite(UserId, ArticleId)
    VALUES (@UserId, @ArticleId)
END
GO