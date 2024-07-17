CREATE TABLE [dbo].[ProcessDocumentTypes]
(
	[Id] INT NOT NULL IDENTITY, 
    [ProcessId] INT NOT NULL, 
    [DocumentTypeId] INT NOT NULL, 
    [Mandatory] BIT NOT NULL, 
    [Order] INT NOT NULL, 
    CONSTRAINT [PK_ProcessDocumentType] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_ProcessDocumentType_Processes] FOREIGN KEY (ProcessId) REFERENCES Processes(Id), 
    CONSTRAINT [FK_ProcessDocumentTypes_DocumentTypes] FOREIGN KEY (DocumentTypeId) REFERENCES DocumentTypes(Id)
)
