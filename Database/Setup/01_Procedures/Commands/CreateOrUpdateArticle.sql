USE BlogDatabase
GO

CREATE OR ALTER PROCEDURE Blog.CreateOrUpdateArticle
(
	@ArticleId INT,
	@AuthorId INT,
	@Title NVARCHAR(128),
	@Description NVARCHAR(512),
	@Body NVARCHAR(2048),
	@ContentStatus NVARCHAR(28),
	@CategoryId INT
)
AS
BEGIN
	DECLARE @ContentStatusId INT = (
		SELECT C.ContentStatusId
		FROM Blog.ContentStatus C
		WHERE C.[Name] = @ContentStatus
	);

	MERGE Blog.Article AS T
	USING (VALUES (
		@ArticleId,
		@AuthorId,
		@Title,
		@Description,
		@Body,
		@ContentStatusId,
		@CategoryId)) AS S(ArticleId, AuthorId, Title, [Description], Body, ContentStatusId, CategoryId)
	ON T.ArticleId = S.ArticleId
	WHEN MATCHED THEN
		UPDATE SET AuthorId = S.AuthorId,
				   Title = S.Title,
				   [Description] = S.[Description],
				   Body = S.Body,
				   ContentStatusId = S.ContentStatusId,
				   CategoryId = S.CategoryId,
				   LastUpdatedDateTime = SYSDATETIME()
	WHEN NOT MATCHED THEN
		INSERT (AuthorId, Title, [Description], Body, ContentStatusId, CategoryId)
		VALUES (S.AuthorId, S.Title, S.[Description], S.Body, S.ContentStatusId, S.CategoryId);
END
GO