CREATE TABLE [dbo].[Permissions]
(
	[Id] INT NOT NULL IDENTITY(1,1), 
    [Name] VARCHAR(50) NOT NULL, 
    [Description] VARCHAR(250) NOT NULL,
    CONSTRAINT [PK_Permissions] PRIMARY KEY CLUSTERED ([Id] ASC)
)
