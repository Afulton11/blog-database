
USE BlogDatabase;
GO

CREATE OR ALTER PROCEDURE Blog.CreateFavorite(
    @UserId INT,
    @ArticleId INT
) AS
BEGIN
	MERGE Blog.Favorite F
	USING (VALUES (@UserId, @ArticleId)) AS S(UserId, ArticleId)
	ON F.ArticleId = S.ArticleId
	   AND F.UserId = S.UserId
	WHEN MATCHED THEN
		UPDATE SET DeletedAt = NULL
	WHEN NOT MATCHED THEN
		INSERT (UserId, ArticleId)
		VALUES (@UserId, @ArticleId);
END
GO