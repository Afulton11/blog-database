USE BlogDatabase;
GO

CREATE OR ALTER PROCEDURE Blog.IsArticleFavoritedByUser(
    @UserId INT,
    @ArticleId INT
) AS
BEGIN
    IF EXISTS (
        SELECT F.UserId
        FROM Blog.Favorite F
        WHERE F.UserId = @UserId
            AND F.ArticleId = @ArticleId
    )
        RETURN 1
    ELSE
        RETURN 0
END
GO