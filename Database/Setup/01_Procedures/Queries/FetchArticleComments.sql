USE BlogDatabase
GO

CREATE OR ALTER PROCEDURE Blog.FetchArticleComments
	@ArticleId INT,
	@MaxDepth INT = 3,
	@PageSize INT = 10,
	@PageNumber INT = 0
AS
BEGIN
	WITH CommentCTE AS
	(
		-- anchor
		SELECT
			C.CommentId,
			C.ParentCommentId,
			C.UserId,
			C.CreationDateTime,
			C.Body,
			0 AS Depth,
			CAST(C.CommentId AS VARCHAR(1024)) AS CPath
		FROM Blog.Comment C
		WHERE C.ParentCommentId IS NULL AND C.ArticleId = @ArticleId

		UNION ALL
		-- recursive
		SELECT 
			CC.CommentId,
			CC.ParentCommentId,
			CC.UserId,
			CC.CreationDateTime,
			CC.Body,
			PC.Depth + 1 AS Depth,
			CAST(
				(PC.CPath + '_' + CAST(CC.CommentId AS VARCHAR(31)))
				AS VARCHAR(1024)
			) AS CPath
		FROM CommentCTE PC
			INNER JOIN Blog.Comment CC ON 
				CC.ParentCommentId = PC.CommentId
		WHERE Depth <= @MaxDepth
	)
	SELECT
		C.CommentId,
		C.ParentCommentId,
		C.UserId,
		C.CreationDateTime,
		C.Body,
		U.Username,
		C.Depth
	FROM CommentCTE C
		INNER JOIN Blog.[User] U ON U.UserId = C.UserId
	ORDER BY C.CPath
	OFFSET @PageSize * @PageNumber ROWS
	FETCH NEXT @PageSize ROWS ONLY
END
GO

-- EXEC Blog.FetchArticleComments 78