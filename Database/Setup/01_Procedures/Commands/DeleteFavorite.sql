USE BlogDatabase;
GO

CREATE OR ALTER PROCEDURE Blog.DeleteFavorite
(
	@UserId INT,
	@ArticleId INT
)
AS
BEGIN
	UPDATE Blog.Favorite
	SET DeletedAt = SYSDATETIME()
	WHERE (UserId = @UserId) AND (ArticleId = @ArticleId)
END
GO