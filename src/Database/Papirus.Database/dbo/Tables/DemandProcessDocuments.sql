CREATE TABLE [dbo].[DemandProcessDocuments]
(
    [Id] INT NOT NULL IDENTITY, 
    [ProcessDocumentTypeId] INT NOT NULL, 
    [DemandId] INT NOT NULL, 
    CONSTRAINT [PK_DemandProcessDocuments] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_DemandProcessDocuments_ProcessDocumentTypes] FOREIGN KEY (ProcessDocumentTypeId) REFERENCES ProcessDocumentTypes(Id), 
    CONSTRAINT [FK_DemandProcessDocuments_Demands] FOREIGN KEY (DemandId) REFERENCES Demands(Id) 
)
