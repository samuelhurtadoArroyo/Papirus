CREATE TABLE [dbo].[Processes]
(
	[Id] INT NOT NULL IDENTITY, 
    [Name] VARCHAR(250) NOT NULL, 
    [ProcessTypeId] INT NOT NULL, 
    CONSTRAINT [PK_Processes] PRIMARY KEY (Id), 
    CONSTRAINT [FK_Processes_ProcessTypes] FOREIGN KEY ([ProcessTypeId]) REFERENCES [ProcessTypes]([Id]) 
)
