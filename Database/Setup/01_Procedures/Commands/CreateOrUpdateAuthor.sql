USE BlogDatabase
GO

CREATE OR ALTER PROCEDURE Blog.CreateOrUpdateAuthor
(
	@UserId INT,
	@FirstName NVARCHAR(64),
	@MiddleName NVARCHAR(64) = NULL,
	@LastName NVARCHAR(64),
	@BirthDate DATE = NULL
)
AS
BEGIN

	IF NOT EXISTS (
			SELECT U.UserId
			FROM Blog.[User] U
			WHERE U.UserId = @UserId)
		THROW 51000, N'Attempted to create author for non-existent user.', 1;
			

	MERGE Blog.Author AS T
	USING (VALUES (
		@UserID,
		@FirstName,
		@MiddleName,
		@LastName,
		@BirthDate)) AS S(UserId, FirstName, MiddleName, LastName, BirthDate)
	ON T.AuthorUserId = S.UserId
	WHEN MATCHED AND NOT EXISTS 
			(
				SELECT S.FirstName, S.MiddleName, S.LastName, S.BirthDate
				INTERSECT
				SELECT T.FirstName, T.MiddleName, T.LastName, T.BirthDate
			) THEN
		UPDATE SET FirstName = S.FirstName,
				   MiddleName = S.MiddleName,
				   LastName = S.LastName,
				   BirthDate = S.BirthDate
	WHEN NOT MATCHED THEN
		INSERT (AuthorUserId, FirstName, MiddleName, LastName, BirthDate)
		VALUES (S.UserId, S.FirstName, S.MiddleName, S.LastName, S.BirthDate);
END
GO