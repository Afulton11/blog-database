--Look up and count the number of articles by a given author
CREATE OR ALTER PROCEDURE Blog.AuthorArticleCount
(
	@AuthorUserID INT
)
AS (
	SELECT Auth.AuthorUserID AS UserID,
	       COUNT(DISTINCT Art.ArticleID) AS NumberOfArticles
	FROM Blog.Author Auth
	     INNER JOIN Blog.Article Art ON Art.AuthorID = Auth.AuthorUserID
	WHERE Auth.AuthorUserID = @AuthorUserID
	GROUP BY Auth.AuthorUserID
)
GO

--Look up all articles by a given author
CREATE OR ALTER PROCEDURE Blog.GetAuthorArticles
(
	@AuthorID INT,
	@PageSize INT = 10,
	@PageNumber INT = 1

)
AS
BEGIN

	SELECT Art.AuthorID AS AuthorID,
	       Art.Title AS Title,
	       Art.Description AS Description,
	       Art.CreationDateTime AS CreationDateTime
	FROM Blog.Article Art
	WHERE Art.AuthorID = @AuthorID
	GROUP BY Art.AuthorID, Art.Title, Art.Description, Art.CreationDateTime
	ORDER BY Art.CreationDateTime DESC
	OFFSET @PageSize * @PageNumber ROWS
	FETCH NEXT @PageSize ROWS ONLY;
END
GO

--Look up posts made between a start date and an end date
CREATE OR ALTER PROCEDURE Blog.GetArticlesTimeSpan
(
	@StartDate DATETIME,
	@EndDate DATETIME,
	@PageSize INT = 10,
	@PageNumber INT = 1
)
AS
BEGIN
	SELECT Art.Title AS Title,
	       Art.AuthorID AS AuthorID,
	       Art.Description AS Description,
	       Art.CreationDateTime AS CreationDateTime
	FROM Blog.Article Art
	WHERE Art.CreationDateTime BETWEEN @StartDate AND @EndDate
	GROUP BY Art.Title, Art.AuthorID, Art.Description, Art.CreationDateTime
	ORDER BY Art.CreationDateDate DESC
	OFFSET @PageSize * @PageNumber ROWS
	FETCH NEXT @PageSize ROWS ONLY;
END
GO

--Look up most recent articles
CREATE OR ALTER PROCEDURE Blog.MostRecentArticles
(
	@PageSize INT = 10,
	@PageNumber INT = 1
)
AS
BEGIN
	SELECT Art.AuthorID AS AuthorID,
	       Art.Title AS Title,
	       Art.Description AS Description,
	       MAX(Art.CreatedDateTime) AS CreatedDateTime
	FROM Blog.Article Art
	GROUP BY Art.AuthorID, Art.Title, Art.Description
	ORDER BY Art.CreatedDateTime DESC
	OFFSET @PageSize * @PageNumber ROWS
	FETCH NEXT @PageSize ROWS ONLY;
END
GO

/*
--General template for pagenation (Useless?)
CREATE OR ALTER PROCEDURE Blog.Pagenation
(
	@PageNumber INT,
	@PageSize INT
)
AS(
	SELECT
	FROM
	WHERE
	GROUP BY
	ORDER BY
	OFFSET @PageSize * @PageNumber ROWS
	FETCH NEXT @PageSize ROWS ONLY
)
GO
*/

--Retrieve all followers of a given author
CREATE OR ALTER PROCEDURE Blog.GetFollowers
(
	@AuthorID INT,
	@PageSize INT = 10,
	@PageNumber INT = 1
)
AS
BEGIN
	SELECT Fol.FollowingUserID AS Followers
	FROM Blog.Followed Fol
	WHERE Fol.FollowedUserID = @AuthorID
	GROUP BY Fol.FollowingUserID
	ORDER BY Fol.FollowingUserID ASC
	OFFSET @PageSize * @PageNumber ROWS
	FETCH NEXT @PageSize ROWS ONLY;
END
GO

--Retrieve a given user's favorite articles
CREATE OR ALTER PROCEDURE Blog.GetFavoriteArticles
(
	@UserID INT,
	@PageSize INT = 10,
	@PageNumber INT = 1
)
AS
BEGIN
	SELECT Fav.ArticleID AS ArticleID
	FROM Blog.Favorite Fav
	WHERE Fav.UserID = @UserID
	GROUP BY Fav.ArticleID
	ORDER BY Fav.ArticleID ASC
	OFFSET @PageSize * @PageNumber ROWS
	FETCH NEXT @PageSize ROWS ONLY;
END
GO

--Retrieve all comments for a given article
CREATE OR ALTER PROCEDURE Blog.GetComments
(
	@ArticleID INT,
	@PageSize INT = 10,
	@PageNumber INT = 1
)
AS
BEGIN
	SELECT Com.Body AS Comment
	FROM Blog.Comment Com
	WHERE Com.ArticleID = @ArticleID
	GROUP BY Com.Body
	ORDER BY Com.CreationDateTime DESC
	OFFSET @PageSize * @PageNumber ROWS
	FETCH NEXT @PageSize ROWS ONLY;
END
GO

--Update: 4-18-19

--Counts up all of a given user's points
CREATE OR ALTER PROCEDURE Blog.CountAllPoints
(
	@UserID INT
)
AS(
	SELECT SUM(P.Value) AS Points
	FROM Blog.Point P
	WHERE (P.UserID = @UserID)
)
GO

--Counts up all of a given user's unexpired points
CREATE OR ALTER PROCEDURE Blog.CountCurrentPoints
(
	@UserID INT
)
AS(
	SELECT SUM(P.Value) AS Points
	FROM Blog.Point P
	WHERE (P.UserID = @UserID) AND (CURRENT_TIMESTAMP < P.ExpiresAt)
)
GO

--Counts up all of a given user's expired points
CREATE OR ALTER PROCEDURE Blog.CountExpiredPoints
(
	@UserID INT
)
AS(
	SELECT SUM(P.Value) AS Points
	FROM Blog.Point P
	WHERE (P.UserID = @UserID) AND (CURRENT_TIMESTAMP >= P.ExpiresAt)
)
GO

--Verifies a user's login using username and password
CREATE OR ALTER PROCEDURE Blog.VerfyLoginUsername
(
	@Username NVARCHAR(64),
	@Password NVARCHAR(128)
)
AS
BEGIN
	SELECT *
	FROM Blog.[User] U
	WHERE (U.Username = @Username) AND (U.Password = @Password)
END
GO

--Verifies a user's login using e-mail and password
CREATE OR ALTER PROCEDURE Blog.VerfyLoginEmail
(
	@Email NVARCHAR(128),
	@Password NVARCHAR(128)
)
AS
BEGIN
	SELECT *
	FROM Blog.[User] U
	WHERE (U.Email = @Email) AND (U.Password = @Password)

END
GO

--Register a new user
CREATE OR ALTER PROCEDURE Blog.RegisterNewUser
(
	@Username NVARCHAR(64),
	@Password NVARCHAR(128),
	@Email NVARCHAR(128)
)
AS
BEGIN
	DECLARE @UserRole INT = (SELECT R.RoleID FROM Blog.Role R WHERE R.Name = @Username);
	INSERT Blog.[User](RoleID, Username, Password, Email)
	VALUES (@UserRole, @Username, @Password, @Email)
END
GO

--Display user info
CREATE OR ALTER PROCEDURE Blog.DisplayUser
(
	@Username NVARCHAR(64)
)
AS
BEGIN
	SELECT U.Username AS Username,
		   U.Email AS Email,
		   U.IsEmailVerified AS IsEmaiVerified,
		   (SELECT R.[Name] FROM Blog.Role R WHERE R.RoleID = U.RoleID) AS Role,
		   U.CreationDateTime AS CreationDateTime,
		   U.LastUpdateTime AS LastUpdateTime
	FROM Blog.[User] U
	WHERE (U.Username = @Username)
END
GO

--Retrieve all authors a given user is following
CREATE OR ALTER PROCEDURE Blog.GetFollowers
(
	@UserID INT,
	@PageSize INT = 10,
	@PageNumber INT = 1
)
AS
BEGIN
	SELECT Fol.FollowedUserID AS [Following]
	FROM Blog.Follower Fol
	WHERE Fol.FollowingUserID = @UserID
	GROUP BY Fol.FollowedUserID
	ORDER BY Fol.FollowedUserID ASC
	OFFSET @PageSize * @PageNumber ROWS
	FETCH NEXT @PageSize ROWS ONLY;
END
GO

--Update User info
CREATE OR ALTER PROCEDURE Blog.UserUpdate
(
	@UserID INT,
	@Email NVARCHAR(128),
	@Password NVARCHAR(128),
	@DeletedAt DATETIME
)
AS
BEGIN
	UPDATE Blog.[User]
	SET Email = ISNULL(@Email, Email), [Password] = ISNULL(@Password, [Password]), DeletedAt = ISNULL(@DeletedAt, DeletedAt)
	WHERE UserID = @UserID
END
GO

--Update Author info
CREATE OR ALTER PROCEDURE Blog.AuthorUpdate
(
	@AuthorUserID INT,
	@FirstName NVARCHAR(64),
	@MiddleName NVARCHAR(64),
	@LastName NVARCHAR(64),
	@BirthDate DATE,
	@DeletedAt DATETIME
)
AS
BEGIN
	UPDATE Blog.Author
	SET FirstName = ISNULL(@FirstName, FirstName), MiddleName = ISNULL(@MiddleName, MiddleName), LastName = ISNULL(@LastName, LastName), BirthDate = ISNULL(@BirthDate, BirthDate), DeletedAt = ISNULL(@DeletedAt, DeletedAt)
	WHERE AuthorUserID = @AuthorUserID
END
GO

--Update Favorite Articles for a given user
CREATE OR ALTER PROCEDURE Blog.FavoriteUpdate
(
	@UserID INT,
	@ArticleID INT,
	@DeletedAt DATETIME
)
AS
BEGIN
	UPDATE Blog.Favorite
	SET ArticleID = ISNULL(@ArticleID, ArticleID), DeletedAt = ISNULL(@DeletedAt, DeletedAt)
	WHERE UserID = @UserID
END
GO

--