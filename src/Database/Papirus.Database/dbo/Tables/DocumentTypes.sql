CREATE TABLE [dbo].[DocumentTypes]
(
	[Id] INT NOT NULL IDENTITY, 
    [Name] VARCHAR(50) NOT NULL, 
    [FormatTypeId] INT NOT NULL, 
    [ExtractionStrategyId] INT NOT NULL, 
    [Readable] BIT NOT NULL, 
    [ClassExtractor] VARCHAR(50) NOT NULL, 
    CONSTRAINT [PK_DocumentTypes] PRIMARY KEY (Id), 
    CONSTRAINT [FK_DocumentTypes_FormatTypes] FOREIGN KEY (FormatTypeId) REFERENCES FormatTypes(Id), 
    CONSTRAINT [FK_DocumentTypes_ExtractionStrategyId] FOREIGN KEY (ExtractionStrategyId) REFERENCES ExtractionStrategies(Id) 
)
