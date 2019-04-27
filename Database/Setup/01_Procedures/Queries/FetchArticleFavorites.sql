USE BlogDatabase;
GO

CREATE OR ALTER PROCEDURE Blog.FetchArticleFavorites(
	@ArticleId INT
) AS
BEGIN
	SELECT F.UserId,
		   U.Username
	FROM Blog.Favorite F
		INNER JOIN Blog.Article A ON 
			A.ArticleId = @ArticleId 
			AND A.ArticleId = F.ArticleId
		INNER JOIN Blog.[User] U ON U.UserId = F.UserId
	WHERE F.DeletedAt IS NULL
END
GO
