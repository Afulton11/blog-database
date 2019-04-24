USE BlogDatabase
GO

CREATE OR ALTER FUNCTION Blog.FetchAllComments()
RETURNS TABLE
AS
RETURN(
	SELECT 
		*,
		CASE WHEN Com.ParentCommentId IS NOT NULL 
			THEN
			ROW_NUMBER() OVER(
				PARTITION BY Com.ParentCommentId
				ORDER BY Com.CreationDateTime ASC
			)
		ELSE
			0
		END AS ReplyNumber
		FROM Blog.Comment Com
)
GO

CREATE OR ALTER FUNCTION Blog.FetchAllArticleComments
(
	@ArticleId INT,
	@MaximumRepliesToFetch INT = 3
)
RETURNS TABLE 
AS
RETURN (
	SELECT
		*,
		CASE WHEN Com.ReplyNumber = 0
		THEN
			ROW_NUMBER() OVER(
				PARTITION BY Com.ReplyNumber
				ORDER BY Com.CreationDateTime ASC
			)
		ELSE
			0
		END AS CommentNumber
	FROM Blog.FetchAllComments() Com
	WHERE Com.ArticleId = @ArticleId AND Com.ReplyNumber <= @MaximumRepliesToFetch
)
GO

CREATE OR ALTER FUNCTION Blog.FetchAllArticleCommentsWithPaths
(
	@ArticleId INT,
	@MaximumRepliesToFetch INT = 3
)
RETURNS TABLE 
AS
RETURN (
	SELECT
		*,
		CASE WHEN Com.ReplyNumber = 0
		THEN
			CAST(Com.CommentNumber AS VARCHAR)
		ELSE
			(CAST(ROW_NUMBER() OVER(
					PARTITION BY Com.CommentNumber, Com.ReplyNumber
					ORDER BY Com.ParentCommentId ASC
				) AS VARCHAR)
			+ '_'
			+ CAST(Com.ReplyNumber AS VARCHAR))
		END AS PathSequence
			
	FROM Blog.FetchAllArticleComments(@ArticleId, @MaximumRepliesToFetch) Com
	WHERE Com.ArticleId = @ArticleId AND Com.ReplyNumber <= @MaximumRepliesToFetch
)
GO

/**
	Returns a paginated list of comments for an article Along with the
	first @MaximumRepliesToFetch replies for each comment.

	The @PageSize for this query doesn't determine the amount of results unless @MaximumRepliesToFetch = 0.
*/
CREATE OR ALTER PROCEDURE Blog.FetchArticleComments
	@ArticleId INT,
	@MaximumRepliesToFetch INT = 3,
	@PageSize INT = 10,
	@PageNumber INT = 0
AS
BEGIN
	SELECT
		Com.ReplyNumber,
		Com.CommentNumber,
		Com.PathSequence,
		Com.ParentCommentId,
		Com.CommentId,
		Com.UserId,
		U.Username,
		Com.CreationDateTime,
		Com.Body,
		Com.DeletedAt
	FROM Blog.FetchAllArticleCommentsWithPaths(@ArticleId, @MaximumRepliesToFetch) Com
		INNER JOIN Blog.[User] U ON U.UserId = Com.UserId
	WHERE (Com.CommentNumber > @PageSize * @PageNumber
		   AND Com.CommentNumber <= @PageSize * @PageNumber + @PageSize )
		  OR Com.CommentNumber = 0
	ORDER BY Com.PathSequence ASC
END
GO