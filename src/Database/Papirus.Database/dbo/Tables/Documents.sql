CREATE TABLE [dbo].[Documents]
(
	[Id] INT NOT NULL, 
    [DocumentTypeId] INT NOT NULL, 
    [DocumentIdentifier] UNIQUEIDENTIFIER NOT NULL, 
    [FilePath] VARCHAR(MAX) NOT NULL, 
    [FileName] VARCHAR(MAX) NOT NULL, 
    CONSTRAINT [PK_Documents] PRIMARY KEY (Id), 
    CONSTRAINT [FK_Documents_DocumentTypes] FOREIGN KEY (DocumentTypeId) REFERENCES DocumentTypes(Id) 
)
