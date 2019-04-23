--========================================================User Procedures===========================================================
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
	WHERE (U.Username = @Username) AND (U.DeletedAt = NULL OR U.DeletedAt > SYSDATETIME())
END
GO

--Update User info
CREATE OR ALTER PROCEDURE Blog.UserUpdate
(
	@UserID INT,
	@Email NVARCHAR(128),
	@Password NVARCHAR(128)
)
AS
BEGIN
	UPDATE Blog.[User]
	SET Email = ISNULL(@Email, Email), [Password] = ISNULL(@Password, [Password]), LastUpdateUserID = @UserID, LastUpdateTime = SYSDATETIME()
	WHERE UserID = @UserID
END
GO

--Delete User
CREATE OR ALTER PROCEDURE Blog.DeleteUser
(
	@UserID INT
)
AS
BEGIN
	UPDATE Blog.[User]
	SET DeletedAt = SYSDATETIME()
	WHERE UserID = @UserID
END
GO





--=========================================================================Author Procedures======================================================
--Enable a user to become an author
CREATE OR ALTER PROCEDURE Blog.BecomeAuthor
(
	@UserID INT,
	@FirstName NVARCHAR(64),
	@MiddleName NVARCHAR(64),
	@LastName NVARCHAR(64),
	@Birthdate DATE
)
AS
BEGIN
	INSERT Blog.Author(AuthorUserID, FirstName, MiddleName, LastName, Birthdate)
	VALUES (@UserID, @FirstName, @MiddleName, @LastName, @Birthdate)
END
GO

--Update Author info
CREATE OR ALTER PROCEDURE Blog.AuthorUpdate
(
	@AuthorUserID INT,
	@FirstName NVARCHAR(64),
	@MiddleName NVARCHAR(64),
	@LastName NVARCHAR(64),
	@BirthDate DATE
)
AS
BEGIN
	UPDATE Blog.Author
	SET FirstName = ISNULL(@FirstName, FirstName), MiddleName = ISNULL(@MiddleName, MiddleName), LastName = ISNULL(@LastName, LastName), BirthDate = ISNULL(@BirthDate, BirthDate)
	WHERE AuthorUserID = @AuthorUserID
END
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
	WHERE (Art.AuthorID = @AuthorID) AND (Art.DeletedAt = NULL OR Art.DeletedAt > SYSDATETIME())
	GROUP BY Art.AuthorID, Art.Title, Art.Description, Art.CreationDateTime
	ORDER BY Art.CreationDateTime DESC
	OFFSET @PageSize * @PageNumber ROWS
	FETCH NEXT @PageSize ROWS ONLY;
END
GO

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

--Delete Author
CREATE OR ALTER PROCEDURE Blog.DeleteAuthor
(
	@AuthorID INT
)
AS
BEGIN
	UPDATE Blog.Author
	SET DeletedAt = SYSDATETIME()
	WHERE AuthorUserID = @AuthorID
END
GO





--============================================================================Article Procedures=====================================================
--Add a new article
CREATE OR ALTER PROCEDURE Blog.AddNewArticle
(
	@AuthorID INT,
	@Title NVARCHAR(128),
	@Description NVARCHAR(512),
	@Body NVARCHAR(2048),
	@CategoryID INT
)
AS
BEGIN
	INSERT Blog.Article(AuthorID, Title, [Description], Body, CategoryID)
	VALUES (@AuthorID, @Title, @Description, @Body, @CategoryID)
END
GO

--Update an article
CREATE OR ALTER PROCEDURE Blog.UpdateArticle
(
	@ArticleID INT,
	@Title NVARCHAR(128),
	@Description NVARCHAR(512),
	@Body NVARCHAR(2048),
	@CategoryID INT,
	@Update DATETIME
)
AS
BEGIN
	UPDATE Blog.Article
	SET Title = ISNULL(@Title, Title), [Description] = ISNULL(@Description, [Description]), Body = ISNULL(@Body, Body), CategoryID = ISNULL(@CategoryID, CategoryID), LastUpdateDateTime = @Update
	WHERE ArticleID = @ArticleID
END
GO

--Display Article
CREATE OR ALTER PROCEDURE Blog.DisplayArticle
(
	@ArticleID INT
)
AS
BEGIN
	SELECT Art.Title AS Title,
		   (Auth.FirstName + Auth.MiddleName + Auth.LastName) AS Author,
		   Art.Body AS Body,
		   Cat.[Name] AS Category,
		   Art.CreationDateTime AS CreationDate,
		   Art.LastUpdatedDateTime AS LastUpdate,
		   COUNT(DISTINCT Fav.UserID) AS Favorited
	FROM Blog.Article Art
		 INNER JOIN Blog.Author Auth ON Auth.AuthorUserID = Art.AuthorID
		 INNER JOIN Blog.ArticleCategory Cat ON Cat.ArticleCategoryID = Art.CategoryID
		 INNER JOIN Blog.Comment Com ON Com.ArticleID = Art.ArticleID
		 INNER JOIN Blog.Favorite Fav ON Fav.ArticleID = Art.ArticleID
	WHERE (Art.ArticleID = @ArticleID) AND (Art.DeletedAt = NULL OR Art.DeletedAt > SYSDATETIME())
	GROUP BY Art.Title, Art.Body, Cat.[Name], Art.CreationDateTime, Art.LastUpdatedDateTime
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
	       Art.CreatedDateTime AS CreatedDateTime,
		   MAX(Art.LastUpdateDateTime) AS LastUpdateDateTime
	FROM Blog.Article Art
	WHERE Art.DeletedAt = NULL OR Art.DeletedAt > SYSDATETIME()
	GROUP BY Art.AuthorID, Art.Title, Art.Description, Art.CreationDateTime
	ORDER BY Art.LastUpdateDateTime DESC
	OFFSET @PageSize * @PageNumber ROWS
	FETCH NEXT @PageSize ROWS ONLY;
END
GO

--Look up articles made between a start date and an end date
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
	       Art.CreationDateTime AS CreationDateTime,
		   Art.LastUpdateDateTime AS LastUpdateDateTime
	FROM Blog.Article Art
	WHERE (Art.CreationDateTime BETWEEN @StartDate AND @EndDate) AND (Art.DeletedAt = NULL OR Art.DeletedAt > SYSDATETIME())
	GROUP BY Art.Title, Art.AuthorID, Art.Description, Art.CreationDateTime, Art.LastUpdateDateTime
	ORDER BY Art.LastUpdateDateTime DESC, Art.CreationDateDate DESC
	OFFSET @PageSize * @PageNumber ROWS
	FETCH NEXT @PageSize ROWS ONLY;
END
GO

--Delete Article
CREATE OR ALTER PROCEDURE Blog.DeleteArticle
(
	@ArticleID INT
)
AS
BEGIN
	UPDATE Blog.Article
	SET DeletedAt = SYSDATETIME()
	WHERE ArticleID = @ArticleID
END
GO





--================================================================Category Procedures=========================================================
--Create article category
CREATE OR ALTER PROCEDURE Blog.AddCategory
(
	@Name NVARCHAR(28),
	@UserID INT
)
AS
BEGIN
	INSERT Blog.ArticleCategory(Name, CreationUserID)
	VALUES (@Name, @UserID)
END
GO

--Retrieve all articles in a given category
CREATE OR ALTER PROCEDURE Blog.GetArticlesByCategory
(
	@CategoryID INT,
	@PageSize INT = 10,
	@PageNumber INT = 1
)
AS
BEGIN
	SELECT Art.AuthorID AS AuthorID,
		   Art.Title AS Title,
		   Art.[Description] AS [Description],
		   Art.CreationDateTime AS CreationDateTime,
		   Art.LastUpdateDateTime AS LastUpdate
	FROM Blog.Article Art
	WHERE (Art.CategoryID = @CategoryID) AND (Art.DeletedAt = NULL OR Art.DeletedAt > SYSDATETIME())
	GROUP BY Art.AuthorID, Art.Title, Art.[Description], Art.CreationDateTime, Art.LastUpdateDateTime
	ORDER BY Art.LastUpdateDateTime DESC, Art.CreationDateTime DESC
	OFFSET @PageSize * @PageNumber ROWS
	FETCH NEXT @PageSize ROWS ONLY;
END
GO

--Update article category
CREATE OR ALTER PROCEDURE Blog.UpdateCategory
(
	@CategoryID INT,
	@Name NVARCHAR(28),
	@UserID INT
)
AS
BEGIN
	UPDATE Blog.ArticleCategory
	SET [Name] = ISNULL(@Name, [Name]), LastUpdateUserID = @UserID, LastUpdateDateTime = SYSDATETIME()
	WHERE ArticleCategoryID = @CategoryID
END
GO

--Archive (delete) article category
CREATE OR ALTER PROCEDURE Blog.ArchiveCategory
(
	@CategoryID INT
)
AS
BEGIN
	UPDATE Blog.ArticleCategory
	SET DeletedAt = SYSDATETIME()
	WHERE ArticleCategoryID = @CategoryID
END
GO





--=================================================================Comment Procedures=========================================================
--Add a new comment to an article
CREATE OR ALTER PROCEDURE Blog.AddNewComment
(
	@ArticleID INT,
	@UserID INT,
	@Body NVARCHAR(2048)
)
AS
BEGIN
	INSERT Blog.Comment(UserID, ArticleID, Body)
	VALUES (@UserID, @ArticleID, @Body)
END
GO

--Add a new comment to another comment
CREATE OR ALTER PROCEDURE Blog.AddNewSubComment
(
	@ParentCommentID INT,
	@UserID INT,
	@Body NVARCHAR(2048)
)
AS
BEGIN
	INSERT Blog.Comment(UserID, ParentCommentID, Body)
	VALUES (@UserID, @ParentCommentID, @Body)
END
GO

--Update a comment
CREATE OR ALTER PROCEDURE Blog.UpdateComment
(
	@CommentID INT,
	@Body NVARCHAR(2048)
)
AS
BEGIN
	UPDATE Blog.Comment
	SET Body = ISNULL(@Body, Body), LastUpdateDateTime = SYSDATETIME()
	WHERE CommentID = @CommentID
END
GO

--Retrieve all comments for a given article
CREATE OR ALTER PROCEDURE Blog.GetCommentsArticle
(
	@ArticleID INT,
	@PageSize INT = 10,
	@PageNumber INT = 1
)
AS
BEGIN
	SELECT *
	FROM Blog.Comment Com
	WHERE Com.ArticleID = @ArticleID
	ORDER BY  Com.LastUpdateDateTime DESC, Com.CreationDateTime DESC
	OFFSET @PageSize * @PageNumber ROWS
	FETCH NEXT @PageSize ROWS ONLY;
END
GO

--Retrieve all comments by a given user
CREATE OR ALTER PROCEDURE Blog.GetCommentsUser
(
	@UserID INT,
	@PageSize INT = 10,
	@PageNumber INT = 1
)
AS
BEGIN
	SELECT *
	FROM Blog.Comment Com
	WHERE Com.UserID = @UserID
	ORDER BY Com.LastUpdateDateTime DESC, Com.CreationDateTime DESC
	OFFSET @PageSize * @PageNumber ROWS
	FETCH NEXT @PageSize ROWS ONLY;
END
GO

--Delete Comment
CREATE OR ALTER PROCEDURE Blog.DeleteComment
(
	@CommentID INT
)
AS
BEGIN
	UPDATE Blog.Comment
	SET  Body = NULL, DeletedAt = SYSDATETIME()
	WHERE CommentID = @CommentID
END
GO





--==================================================================Points Procedures===========================
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





--===================================================================Favorites Procedures============================================================
--Add a new favorite article to a given user
CREATE OR ALTER PROCEDURE Blog.AddFavorite
(
	@ArticleID INT,
	@UserID INT
)
AS
BEGIN
	INSERT Blog.Favorite(UserID, ArticleID)
	VALUES (@UserID, @ArticleID)
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
	WHERE (Fav.UserID = @UserID) AND (Fav.DeletedAt = NULL OR Fav.DeletedAt > SYSDATETIME())
	GROUP BY Fav.ArticleID
	ORDER BY Fav.ArticleID ASC
	OFFSET @PageSize * @PageNumber ROWS
	FETCH NEXT @PageSize ROWS ONLY;
END
GO

--Delete Favorite
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





--======================================================================Followers Procedures=====================================================
--Add a new Follower to a given Author
CREATE OR ALTER PROCEDURE Blog.AddFollower
(
	@FollowedUserID INT,
	@NewFollower INT
)
AS
BEGIN
	INSERT Blog.Follower(FollowedUserID, FollowingUserID)
	VALUES (@FollowedUserID, @NewFollower)
END
GO

--Delete author from 'following' list
CREATE OR ALTER PROCEDURE Blog.RemoveAuthorFollowing
(
	@FollowingID INT,
	@FollowedID INT
)
AS
BEGIN
	UPDATE Blog.Follower
	SET DeletedAt = SYSDATETIME()
	WHERE (FollowingUserID = @FollowingID) AND (FollowedUserID = @FollowedID)
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
	WHERE (Fol.FollowingUserID = @UserID) AND (Fol.DeletedAt = NULL OR Fol.DeletedAT > SYSDATETIME())
	GROUP BY Fol.FollowedUserID
	ORDER BY Fol.FollowedUserID ASC
	OFFSET @PageSize * @PageNumber ROWS
	FETCH NEXT @PageSize ROWS ONLY;
END
GO

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
	WHERE (Fol.FollowedUserID = @AuthorID) AND (Fol.DeletedAt = NULL OR Fol.DeletedAt > SYSDATETIME())
	GROUP BY Fol.FollowingUserID
	ORDER BY Fol.FollowingUserID ASC
	OFFSET @PageSize * @PageNumber ROWS
	FETCH NEXT @PageSize ROWS ONLY;
END
GO

--Retrieve a count of authors a given user is following
CREATE OR ALTER PROCEDURE Blog.GetAuthorFollowCount
(
	@UserID INT
)
AS
BEGIN
	SELECT COUNT(DISTINCT Fol.FollowedUserID) AS AuthorsFollowed
	FROM Blog.Follower Fol
	WHERE FollowingUserID = @UserID
END
GO

--Retrieve a count of users following a given author
CREATE OR ALTER PROCEDURE Blog.GetAuthorFollowCount
(
	@AuthorID INT
)
AS
BEGIN
	SELECT COUNT(DISTINCT Fol.FollowingUserID) AS AuthorsFollowed
	FROM Blog.Follower Fol
	WHERE FollowedUserID = @AuthorID
END
GO





--================================================================Useless Procedures/Templates=========================================================
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

--Update Favorite Articles for a given user
CREATE OR ALTER PROCEDURE Blog.FavoriteUpdate
(
	@UserID INT,
	@ArticleID INT
)
AS
BEGIN
	UPDATE Blog.Favorite
	SET ArticleID = ISNULL(@ArticleID, ArticleID)
	WHERE UserID = @UserID
END
GO
*/