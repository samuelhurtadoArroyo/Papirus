CREATE TABLE [dbo].[ExtractionStrategies]
(
	[Id] INT NOT NULL IDENTITY, 
    [Name] VARCHAR(50) NOT NULL, 
    CONSTRAINT [PK_ExtractionStrategies] PRIMARY KEY (Id), 
    CONSTRAINT [AK_ExtractionStrategies_Name] UNIQUE (Name) 
)
