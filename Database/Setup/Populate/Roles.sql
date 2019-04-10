USE SimpleDatabase;
GO

INSERT Blog.Roles (Name)
VALUES 
    ('User'),
    ('Author'),
    ('Administrator'),
    ('Developer'),
    ('Owner');

SELECT *
FROM Blog.Roles;
