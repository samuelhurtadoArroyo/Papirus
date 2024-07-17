CREATE TABLE [dbo].[DocumentFields]
(
	[Id] INT NOT NULL, 
    [DocumentTypeId] INT NOT NULL, 
    [Name] VARCHAR(50) NOT NULL, 
    [Tag] VARCHAR(50) NOT NULL, 
    [Multiplicity] INT NULL, 
    [InitialPattern] VARCHAR(MAX) NOT NULL, 
    [MiddlePattern] VARCHAR(MAX) NOT NULL, 
    [FinalPattern] VARCHAR(MAX) NOT NULL, 
    CONSTRAINT [PK_DocumentFields] PRIMARY KEY (Id), 
    CONSTRAINT [FK_DocumentFields_DocumentTypes] FOREIGN KEY (DocumentTypeId) REFERENCES DocumentTypes(Id), 
    CONSTRAINT [AK_DocumentFields_Tag] UNIQUE (Tag) 
)
