-- Enable SQLCMD Mode.

-- remove and create Tables
:r ./Setup/00_Blog.sql

-- Create OR Alter Command Procedures
:r ./Setup/01_Procedures/Commands/CreatePoints.sql
:r ./Setup/01_Procedures/Commands/CreateUser.sql
:r ./Setup/01_Procedures/Commands/DeleteUser.sql
:r ./Setup/01_Procedures/Commands/UpdateUserEmail.sql
:r ./Setup/01_Procedures/Commands/UpdateUsername.sql
:r ./Setup/01_Procedures/Commands/VerifyUserEmail.sql

-- Create OR Alter Query Procedures
:r ./Setup/01_Procedures/Queries/GetArticleById.sql
:r ./Setup/01_Procedures/Queries/GetTotalPointsByUserId.sql
:r ./Setup/01_Procedures/Queries/FetchUserByUsername.sql
:r ./Setup/01_Procedures/Queries/FetchUserByNormalizedUsername.sql
:r ./Setup/01_Procedures/Queries/FetchUserByNormalizedEmail.sql
:r ./Setup/01_Procedures/Queries/GetAllUsers.sql
:r ./Setup/01_Procedures/Queries/GetUserById.sql
:r ./Setup/01_Procedures/Queries/GetUserRoles.sql
:r ./Setup/01_Procedures/Queries/GetReasonForPoint.sql
:r ./Setup/01_Procedures/Queries/GetTotalPointsByUserId.sql
:r ./Setup/01_Procedures/Queries/GetRecentArticles.sql
:r ./Setup/01_Procedures/Queries/SearchArticles.sql

-- Populate Tables
:r ./Setup/02_Populate/00_Role.sql
:r ./Setup/02_Populate/01_User.sql
:r ./Setup/02_Populate/02_Author.sql
:r ./Setup/02_Populate/03_ContentStatus.sql
:r ./Setup/02_Populate/04_ArticleCategory.sql
:r ./Setup/02_Populate/05_Article.sql
:r ./Setup/02_Populate/06_Favorite.sql
:r ./Setup/02_Populate/07_Comment.sql
:r ./Setup/02_Populate/08_Reason.sql
:r ./Setup/02_Populate/09_Point.sql


