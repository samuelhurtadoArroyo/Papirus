CREATE TABLE [dbo].[ProcessTypes]
(
	[Id] INT NOT NULL IDENTITY, 
    [Name] VARCHAR(50) NOT NULL, 
    CONSTRAINT [PK_ProcessTypes] PRIMARY KEY ([Id]), 
    CONSTRAINT [AK_ProcessTypes_Name] UNIQUE ([Name]) 
)
