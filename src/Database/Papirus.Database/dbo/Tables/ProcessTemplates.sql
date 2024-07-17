CREATE TABLE [dbo].[ProcessTemplates]
(
	[Id] INT NOT NULL IDENTITY, 
    [FirmId] INT NOT NULL, 
    [ProcessId] INT NOT NULL, 
    [FileName] VARCHAR(MAX) NOT NULL, 
    [FilePath] VARCHAR(MAX) NOT NULL, 
    CONSTRAINT [PK_ProcessTemplate] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_ProcessTemplate_Firms] FOREIGN KEY (FirmId) REFERENCES [Firms]([Id]), 
    CONSTRAINT [FK_ProcessTemplates_Processes] FOREIGN KEY ([ProcessId]) REFERENCES [Processes]([Id]) 
)
