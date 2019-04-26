USE BlogDatabase;
GO

CREATE OR ALTER PROCEDURE Blog.DeleteFavorite
(
	@UserID INT,
	@ArticleID INT
)
AS
BEGIN
	UPDATE Blog.Favorite
	SET DeletedAt = SYSDATETIME()
	WHERE (UserID = @UserID) AND (ArticleID = @ArticleID)
END
GO
