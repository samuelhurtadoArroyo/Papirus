CREATE TABLE [dbo].[Demands]
(
	[Id] INT NOT NULL IDENTITY, 
    [RegistrationDate] DATETIME NULL, 
    [DemandIdentifier] UNIQUEIDENTIFIER NOT NULL, 
    [Court] VARCHAR(50) NOT NULL, 
    [City] VARCHAR(50) NOT NULL, 
    [ProcessTypeId] INT NOT NULL, 
    [ProcessId] INT NOT NULL, 
    [SubProcessId] INT NOT NULL, 
    [DemandantId] INT NULL, 
    [MergedDocument] VARCHAR(MAX) NULL, 
    CONSTRAINT [PK_Demands] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_Demands_ProcessTypes] FOREIGN KEY ([ProcessTypeId]) REFERENCES [ProcessTypes]([Id]), 
    CONSTRAINT [FK_Demands_Processes] FOREIGN KEY ([ProcessId]) REFERENCES [Processes]([Id]), 
    CONSTRAINT [FK_Demands_SubProcesses] FOREIGN KEY ([SubProcessId]) REFERENCES [SubProcesses]([Id]), 
    CONSTRAINT [FK_Demands_Demandants] FOREIGN KEY ([DemandantId]) REFERENCES [Demandants]([Id]) 
)
