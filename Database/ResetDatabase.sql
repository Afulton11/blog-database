-- Enable SQLCMD Mode.

-- remove and create Tables
:r ./Setup/Tables.sql

:r ./Setup/Triggers.sql

-- Create OR Alter Command Procedures
:r ./Setup/Procedures/Commands/CreatePoints.sql
:r ./Setup/Procedures/Commands/CreateUser.sql
:r ./Setup/Procedures/Commands/DeleteUser.sql
:r ./Setup/Procedures/Commands/UpdateUserEmail.sql
:r ./Setup/Procedures/Commands/UpdateUsername.sql
:r ./Setup/Procedures/Commands/VerifyUserEmail.sql
:r ./Setup/Procedures/Commands/CreateOrUpdateRole.sql
:r ./Setup/Procedures/Commands/CreateOrUpdateArticle.sql
:r ./Setup/Procedures/Commands/CreateOrUpdateComment.sql
:r ./Setup/Procedures/Commands/CreateOrUpdateAuthor.sql
:r ./Setup/Procedures/Commands/DeleteRole.sql
:r ./Setup/Procedures/Commands/CreateFavorite.sql
:r ./Setup/Procedures/Commands/DeleteFavorite.sql

-- Create OR Alter Query Procedures
:r ./Setup/Procedures/Queries/GetArticleById.sql
:r ./Setup/Procedures/Queries/GetTotalPointsByUserId.sql
:r ./Setup/Procedures/Queries/GetArticleCategoryById.sql
:r ./Setup/Procedures/Queries/GetArticlesInCategory.sql
:r ./Setup/Procedures/Queries/GetArticlesInCategory.sql
:r ./Setup/Procedures/Queries/FetchArticlesByAuthorId.sql
:r ./Setup/Procedures/Queries/FetchUserByUsername.sql
:r ./Setup/Procedures/Queries/FetchUserByNormalizedUsername.sql
:r ./Setup/Procedures/Queries/FetchUserByNormalizedEmail.sql
:r ./Setup/Procedures/Queries/GetAllUsers.sql
:r ./Setup/Procedures/Queries/GetUserById.sql
:r ./Setup/Procedures/Queries/GetUserRoles.sql
:r ./Setup/Procedures/Queries/GetReasonForPoint.sql
:r ./Setup/Procedures/Queries/GetTotalPointsByUserId.sql
:r ./Setup/Procedures/Queries/GetRecentArticles.sql
:r ./Setup/Procedures/Queries/SearchArticles.sql
:r ./Setup/Procedures/Queries/FetchRoleById.sql
:r ./Setup/Procedures/Queries/FetchRoleByNormalizedName.sql
:r ./Setup/Procedures/Queries/FetchAuthorById.sql
:r ./Setup/Procedures/Queries/FetchArticleCategories.sql
:r ./Setup/Procedures/Queries/FetchArticleComments.sql

:r ./Setup/Procedures/Queries/FetchPointsByUserId.sql
:r ./Setup/Procedures/Queries/FetchAllPointReasons.sql
:r ./Setup/Procedures/Queries/GetPointBreakdownByUserId.sql
:r ./Setup/Procedures/Queries/FetchArticleFavorites.sql
:r ./Setup/Procedures/Queries/FetchArticlePageById.sql
:r ./Setup/Procedures/Queries/FetchFavoriteArticlesByUserId.sql

-- Populate Tables
:r ./Setup/Data/00_Role.sql
:r ./Setup/Data/01_User.sql
:r ./Setup/Data/02_Author.sql
:r ./Setup/Data/03_ContentStatus.sql
:r ./Setup/Data/04_ArticleCategory.sql
:r ./Setup/Data/05_Article.sql
:r ./Setup/Data/06_Favorite.sql
:r ./Setup/Data/07_Comment.sql
:r ./Setup/Data/07_Comment2.sql
:r ./Setup/Data/07_Comment3.sql
:r ./Setup/Data/07_Comment4.sql
:r ./Setup/Data/08_Reason.sql
:r ./Setup/Data/09_Point.sql
