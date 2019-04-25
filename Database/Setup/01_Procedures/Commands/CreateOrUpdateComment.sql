USE BlogDatabase
GO

CREATE OR ALTER PROCEDURE Blog.CreateOrUpdateComment
(
	@CommentId INT = NULL,
	@ParentCommentId INT = NULL,
	@UserId INT,
	@ArticleId INT,
	@Body NVARCHAR(2048)
)
AS
BEGIN

	MERGE Blog.Comment AS T
	USING (VALUES (
		@CommentId,
		@ParentCommentId,
		@UserId,
		@ArticleId,
		@Body)) AS S(CommentId, ParentCommentId, UserId, ArticleId, Body)
	ON T.CommentId = S.CommentId
	WHEN MATCHED AND 
			(
				S.Body <> T.Body
			) THEN
		UPDATE SET Body = S.Body,
				   LastUpdatedDateTime = SYSDATETIME()
	WHEN NOT MATCHED THEN
		INSERT (ParentCommentId, UserId, ArticleId, Body)
		VALUES (S.ParentCommentId, S.UserId, S.ArticleId, S.Body);
END
GO