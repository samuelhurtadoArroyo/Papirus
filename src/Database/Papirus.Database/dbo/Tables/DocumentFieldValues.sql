CREATE TABLE [dbo].[DocumentFieldValues]
(
	[Id] INT NOT NULL IDENTITY, 
    [DocumentId] INT NOT NULL, 
    [DocumentFieldId] INT NOT NULL, 
    [FieldValue] VARCHAR(MAX) NOT NULL, 
    CONSTRAINT [PK_DocumentFieldValues] PRIMARY KEY (Id), 
    CONSTRAINT [FK_DocumentFieldValues_Documents] FOREIGN KEY (DocumentId) REFERENCES Documents(Id), 
    CONSTRAINT [FK_DocumentFieldValues_DocumentFields] FOREIGN KEY (DocumentFieldId) REFERENCES DocumentFields(Id) 
)
