CREATE TABLE [dbo].[SubProcesses]
(
    [Id] INT NOT NULL IDENTITY, 
    [Description] VARCHAR(250) NOT NULL, 
    [ProcessId] INT NOT NULL, 
    CONSTRAINT [PK_SubProcesses] PRIMARY KEY (Id), 
    CONSTRAINT [FK_SubProcesses_Processes] FOREIGN KEY (ProcessId) REFERENCES [Processes]([Id])
)
