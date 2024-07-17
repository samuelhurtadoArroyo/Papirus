CREATE TABLE [dbo].[FormatTypes]
(
	[Id] INT NOT NULL IDENTITY, 
    [Name] VARCHAR(50) NOT NULL, 
    [Extensions] VARCHAR(50) NOT NULL, 
    CONSTRAINT [PK_FormatTypes] PRIMARY KEY (Id) 
)
