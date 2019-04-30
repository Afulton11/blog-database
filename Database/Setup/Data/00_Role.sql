USE BlogDatabase;
GO

INSERT Blog.[Role] ([Name])
VALUES 
    (N'User'),
    (N'Author'),
    (N'Administrator'),
    (N'Developer'),
    (N'Owner');