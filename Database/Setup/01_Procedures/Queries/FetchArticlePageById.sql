USE BlogDatabase;
GO

CREATE OR ALTER PROCEDURE Blog.FetchArticlePageById(
	@ArticleId INT,
	@UserId INT = NULL
) AS
BEGIN
	WITH CurrentUserFavorite AS
	(
		SELECT U.UserId
		FROM Blog.[User] U
			INNER JOIN Blog.Favorite F ON 
				F.UserId = @UserId 
				AND F.ArticleId = @ArticleId
				AND F.DeletedAt IS NULL
		WHERE U.UserId = @UserId
	),
	IsFavorited AS
	(
		SELECT CAST(1 AS BIT) AS IsFavorited
		WHERE EXISTS (SELECT F.UserId FROM CurrentUserFavorite F)

		UNION ALL

		SELECT CAST(0 AS BIT) AS IsFavorited
		WHERE NOT EXISTS (SELECT F.UserId FROM CurrentUserFavorite F)
	)
	SELECT A.ArticleId,
		   AU.AuthorUserId,
		   (AU.FirstName + N' ' + AU.LastName) AS AuthorFullName,
		   C.[Name] AS CategoryName,
		   A.CreationDateTime,
		   A.Title,
		   A.Body,
		   (SELECT * FROM IsFavorited) AS DidUserFavorite,
		   COUNT(Comment.CommentId) AS CommentCount,
		   COUNT(DISTINCT F.UserId) AS FavoriteCount
	FROM Blog.Article A
		LEFT JOIN Blog.Favorite F ON F.ArticleId = A.ArticleId AND F.DeletedAt IS NULL
		LEFT JOIN Blog.Author AU ON AU.AuthorUserId = A.AuthorId
		LEFT JOIN Blog.ArticleCategory C ON C.ArticleCategoryId = A.CategoryId
		LEFT JOIN Blog.Comment Comment ON Comment.ArticleId = A.ArticleId
	WHERE A.ArticleId = @ArticleId
	GROUP BY 
		A.ArticleId,
		A.CreationDateTime,
		A.Title,
		A.Body,
		AU.AuthorUserId,
		AU.FirstName,
		AU.LastName,
		C.[Name]
END
GO