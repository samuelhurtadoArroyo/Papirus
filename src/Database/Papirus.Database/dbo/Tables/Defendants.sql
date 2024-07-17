CREATE TABLE [dbo].[Defendants]
(
	[Id] INT NOT NULL IDENTITY, 
    [FirstName] VARCHAR(50) NOT NULL, 
    [LastName] VARCHAR(50) NOT NULL, 
    [Email] VARCHAR(300) NOT NULL, 
    [IdentificationNumber] VARCHAR(50) NOT NULL, 
    [DemandId] INT NOT NULL, 
    [SupportFile] VARBINARY(MAX) NOT NULL, 
    CONSTRAINT [PK_Defendants] PRIMARY KEY (Id), 
    CONSTRAINT [FK_Defendants_Demands] FOREIGN KEY ([DemandId]) REFERENCES [Demands]([Id]) 
)
