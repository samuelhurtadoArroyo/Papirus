CREATE TABLE [dbo].[Firms](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GuidIdentifier] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
CONSTRAINT [PK_Firms] PRIMARY KEY CLUSTERED ([Id] ASC),
CONSTRAINT [UQ_Firms_GuidIdentifier] UNIQUE NONCLUSTERED ([GuidIdentifier] ASC)
)
GO

CREATE TABLE [Roles] (
  [Id] integer IDENTITY(1, 1),
  [Name] nvarchar(50) NOT NULL,
CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED ([Id] ASC)
)
GO

CREATE TABLE [Permissions] (
  [Id] integer IDENTITY(1, 1),
  [Name] nvarchar(50) NOT NULL,
  [Description] nvarchar(300) NOT NULL,
CONSTRAINT [PK_Permissions] PRIMARY KEY CLUSTERED ([Id] ASC)
)
GO

CREATE TABLE [RolePermissions] (
  [Id] integer IDENTITY(1, 1),
  [RoleId] integer NOT NULL,
  [PermissionId] integer NOT NULL,
CONSTRAINT [PK_RolePermissions] PRIMARY KEY CLUSTERED ([Id] ASC)
)
GO

CREATE TABLE [Users] (
  [Id] integer IDENTITY(1, 1),
  [Email] nvarchar(300) NOT NULL,
  [FirstName] nvarchar(50) NOT NULL,
  [LastName] nvarchar(50) NOT NULL,
  [HashedPassword] nvarchar(max) NOT NULL,
  [Salt] nvarchar(max) NOT NULL,
  [RegistrationDate] datetime NOT NULL,
  [Active] bit NOT NULL,
  [RoleId] integer NOT NULL,
  [FirmId] integer NOT NULL,
CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC),
CONSTRAINT [UQ_Users_Email] UNIQUE NONCLUSTERED ([Email] ASC)
)
GO

CREATE TABLE [Teams] (
  [Id] integer IDENTITY(1, 1),
  [Name] nvarchar(50) NOT NULL,
CONSTRAINT [PK_Teams] PRIMARY KEY CLUSTERED ([Id] ASC)
)
GO

CREATE TABLE [TeamMembers] (
  [Id] integer IDENTITY(1, 1),
  [TeamId] integer NOT NULL,
  [MemberId] integer NOT NULL,
  [IsLead] Bit NOT NULL,
  [MaxCases] integer NOT NULL,
CONSTRAINT [PK_TeamMembers] PRIMARY KEY CLUSTERED ([Id] ASC)
)
GO

CREATE TABLE [Status] (
  [Id] integer IDENTITY(1, 1),
  [Name] nvarchar(50) NOT NULL,
CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED ([Id] ASC)
)
GO

CREATE TABLE [Assignments] (
  [Id] integer IDENTITY(1, 1),
  [TeamMemberId] integer NOT NULL,
  [CaseId] integer NOT NULL,
  [StatusId] integer NOT NULL,
CONSTRAINT [PK_Assignments] PRIMARY KEY CLUSTERED ([Id] ASC)
)
GO

CREATE TABLE [ProcessTypes] (
  [Id] integer IDENTITY(1, 1),
  [Name] nvarchar(50) NOT NULL,
CONSTRAINT [PK_ProcessTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
)
GO

CREATE TABLE [Processes] (
  [Id] integer IDENTITY(1, 1),
  [Name] nvarchar(50) NOT NULL,
  [ProcessTypeId] integer NOT NULL,
CONSTRAINT [PK_Processes] PRIMARY KEY CLUSTERED ([Id] ASC)
)
GO

CREATE TABLE [SubProcesses] (
  [Id] integer IDENTITY(1, 1),
  [Description] nvarchar(300) NOT NULL,
  [Abbreviation] nvarchar(50),
  [ProcessId] integer NOT NULL,
CONSTRAINT [PK_SubProcesses] PRIMARY KEY CLUSTERED ([Id] ASC)
)
GO

CREATE TABLE [PersonTypes] (
  [Id] integer IDENTITY(1, 1),
  [Name] nvarchar(50) NOT NULL,
CONSTRAINT [PK_PersonTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
)
GO

CREATE TABLE [IdentificationTypes] (
  [Id] integer IDENTITY(1, 1),
  [Name] nvarchar(50) NOT NULL,
  [Abbreviation] nvarchar(50) NOT NULL,
CONSTRAINT [PK_IdentificationTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
)
GO

CREATE TABLE [People] (
  [Id] integer IDENTITY(1, 1),
  [GuidIdentifier] uniqueidentifier NOT NULL,
  [PersonTypeId] integer NOT NULL,
  [Name] nvarchar(300) NOT NULL,
  [Email] nvarchar(300) NOT NULL,
  [IdentificationTypeId] integer NOT NULL,
  [IdentificationNumber] nvarchar(50) NOT NULL,
  [SupportFileName] nvarchar(MAX) NOT NULL,
  [SupportFilePath] nvarchar(MAX) NOT NULL,
CONSTRAINT [PK_People] PRIMARY KEY CLUSTERED ([Id] ASC),
CONSTRAINT [UQ_People_GuidIdentifier] UNIQUE NONCLUSTERED ([GuidIdentifier] ASC),
CONSTRAINT [UQ_People_Email] UNIQUE NONCLUSTERED ([Email] ASC)
)
GO

CREATE TABLE [ActorTypes] (
  [Id] integer IDENTITY(1, 1),
  [Name] nvarchar(50) NOT NULL,
CONSTRAINT [PK_ActorTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
)
GO

CREATE TABLE [Actors] (
  [Id] integer IDENTITY(1, 1),
  [ActorTypeId] integer NOT NULL,
  [PersonId] integer NOT NULL,
  [CaseId] integer NOT NULL,
CONSTRAINT [PK_Actors] PRIMARY KEY CLUSTERED ([Id] ASC)
)
GO

CREATE TABLE [Cases] (
  [Id] integer IDENTITY(1, 1),
  [RegistrationDate] datetime,
  [GuidIdentifier] uniqueidentifier NOT NULL,
  [Court] nvarchar(50) NOT NULL,
  [City] nvarchar(50) NOT NULL,
  [Amount] money NOT NULL,
  [SubmissionDate] datetime,
  [SubmissionIdentifier] nvarchar(50),
  [DeadLineDate] datetime,
  [ProcessTypeId] integer NOT NULL,
  [ProcessId] integer NOT NULL,
  [SubProcessId] integer,
  [FilePath] nvarchar(MAX) NOT NULL,
  [FileName] nvarchar(MAX) NOT NULL,
  [IsAssigned] bit NOT NULL,
CONSTRAINT [PK_Cases] PRIMARY KEY CLUSTERED ([Id] ASC),
CONSTRAINT [UQ_Cases_GuidIdentifier] UNIQUE NONCLUSTERED ([GuidIdentifier] ASC)
)
GO

CREATE TABLE [DocumentTypes] (
  [Id] integer IDENTITY(1, 1),
  [Name] nvarchar(50) NOT NULL,
CONSTRAINT [PK_DocumentTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
)
GO

CREATE TABLE [ProcessTemplates] (
  [Id] integer IDENTITY(1, 1),
  [FirmId] integer NOT NULL,
  [ProcessTypeId] integer NOT NULL,
  [ProcessId] integer NOT NULL,
  [SubProcessId] integer,
  [FileName] nvarchar(MAX) NOT NULL,
  [FilePath] nvarchar(MAX) NOT NULL,
CONSTRAINT [PK_ProcessTemplates] PRIMARY KEY CLUSTERED ([Id] ASC)
)
GO

CREATE TABLE [ProcessDocumentTypes] (
  [Id] integer IDENTITY(1, 1),
  [ProcessId] integer NOT NULL,
  [DocumentTypeId] integer NOT NULL,
  [Mandatory] bit NOT NULL,
  [DocOrder] integer NOT NULL,
  [ProcessTemplateId] integer NOT NULL,
CONSTRAINT [PK_ProcessDocumentTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
)
GO

CREATE TABLE [CaseProcessDocuments] (
  [Id] integer IDENTITY(1, 1),
  [DocumentTypeId] integer NOT NULL,
  [ProcessDocumentTypeId] integer NOT NULL,
  [CaseId] integer NOT NULL,
  [FileName] nvarchar(MAX) NOT NULL,
  [FilePath] nvarchar(MAX) NOT NULL,
CONSTRAINT [PK_CaseProcessDocuments] PRIMARY KEY CLUSTERED ([Id] ASC)
)
GO

CREATE TABLE [CaseDocumentFieldValues] (
  [Id] integer IDENTITY(1, 1),
  [CaseProcessDocumentId] integer NOT NULL,
  [DocumentTypeId] integer NOT NULL,
  [ProcessDocumentTypeId] integer NOT NULL,
  [CaseId] integer NOT NULL,
  [Name] nvarchar(50) NOT NULL,
  [Tag] nvarchar(50) NOT NULL,
  [Multiplicity] integer,
  [FieldValue] nvarchar(MAX) NOT NULL,
CONSTRAINT [PK_CaseDocumentFieldValues] PRIMARY KEY CLUSTERED ([Id] ASC)
)
GO

-- Security
ALTER TABLE [Users] ADD CONSTRAINT FK_Users_Firms FOREIGN KEY ([FirmId]) REFERENCES [Firms] ([Id])
GO

ALTER TABLE [Users] ADD CONSTRAINT FK_Users_Roles FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([Id])
GO

ALTER TABLE [RolePermissions] ADD CONSTRAINT FK_RolePermissions_Roles FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([Id])
GO

ALTER TABLE [RolePermissions] ADD CONSTRAINT FK_RolePermissions_Permissions FOREIGN KEY ([PermissionId]) REFERENCES [Permissions] ([Id])
GO

-- Teams
ALTER TABLE [TeamMembers] ADD CONSTRAINT FK_TeamMembers_Teams FOREIGN KEY ([TeamId]) REFERENCES [Teams] ([Id])
GO

ALTER TABLE [TeamMembers] ADD CONSTRAINT FK_TeamMembers_Users FOREIGN KEY ([MemberId]) REFERENCES [Users] ([Id])
GO

ALTER TABLE [Assignments] ADD CONSTRAINT FK_Assignments_Status FOREIGN KEY ([StatusId]) REFERENCES [Status] ([Id])
GO

ALTER TABLE [Assignments] ADD CONSTRAINT FK_Assignments_TeamMembers FOREIGN KEY ([TeamMemberId]) REFERENCES [TeamMembers] ([Id])
GO

ALTER TABLE [Assignments] ADD CONSTRAINT FK_Assignments_Cases FOREIGN KEY ([CaseId]) REFERENCES [Cases] ([Id])
GO

-- Processes
ALTER TABLE [Processes] ADD CONSTRAINT FK_Processes_ProcessTypes FOREIGN KEY ([ProcessTypeId]) REFERENCES [ProcessTypes] ([Id])
GO

ALTER TABLE [SubProcesses] ADD CONSTRAINT FK_SubProcesses_Processes FOREIGN KEY ([ProcessId]) REFERENCES [Processes] ([Id])
GO

ALTER TABLE [ProcessTemplates] ADD CONSTRAINT FK_ProcessTemplates_ProcessTypes FOREIGN KEY ([ProcessTypeId]) REFERENCES [ProcessTypes] ([Id])
GO

ALTER TABLE [ProcessTemplates] ADD CONSTRAINT FK_ProcessTemplates_Processes FOREIGN KEY ([ProcessId]) REFERENCES [Processes] ([Id])
GO

ALTER TABLE [ProcessTemplates] ADD CONSTRAINT FK_ProcessTemplates_SubProcesses FOREIGN KEY ([SubProcessId]) REFERENCES [SubProcesses] ([Id])
GO

ALTER TABLE [ProcessTemplates] ADD CONSTRAINT FK_ProcessTemplates_Firms FOREIGN KEY ([FirmId]) REFERENCES [Firms] ([Id])
GO

ALTER TABLE [ProcessDocumentTypes] ADD CONSTRAINT FK_ProcessDocumentTypes_Processes FOREIGN KEY ([ProcessId]) REFERENCES [Processes] ([Id])
GO

ALTER TABLE [ProcessDocumentTypes] ADD CONSTRAINT FK_ProcessDocumentTypes_ProcessTemplates FOREIGN KEY ([ProcessTemplateId]) REFERENCES [ProcessTemplates] ([Id])
GO

-- People
ALTER TABLE [People] ADD CONSTRAINT FK_People_PersonTypes FOREIGN KEY ([PersonTypeId]) REFERENCES [PersonTypes] ([Id])
GO

ALTER TABLE [People] ADD CONSTRAINT FK_People_IdentificationTypes FOREIGN KEY ([IdentificationTypeId]) REFERENCES [IdentificationTypes] ([Id])
GO

ALTER TABLE [Actors] ADD CONSTRAINT FK_Actors_ActorTypes FOREIGN KEY ([ActorTypeId]) REFERENCES [ActorTypes] ([Id])
GO

ALTER TABLE [Actors] ADD CONSTRAINT FK_Actors_People FOREIGN KEY ([PersonId]) REFERENCES [People] ([Id])
GO

-- Cases
ALTER TABLE [Cases] ADD CONSTRAINT FK_Cases_ProcessTypes FOREIGN KEY ([ProcessTypeId]) REFERENCES [ProcessTypes] ([Id])
GO

ALTER TABLE [Cases] ADD CONSTRAINT FK_Cases_Processes FOREIGN KEY ([ProcessId]) REFERENCES [Processes] ([Id])
GO

ALTER TABLE [Cases] ADD CONSTRAINT FK_Cases_SubProcesses FOREIGN KEY ([SubProcessId]) REFERENCES [SubProcesses] ([Id])
GO

ALTER TABLE [Actors] ADD CONSTRAINT FK_Actors_Cases FOREIGN KEY ([CaseId]) REFERENCES [Cases] ([Id])
GO

ALTER TABLE [CaseProcessDocuments] ADD CONSTRAINT FK_CaseProcessDocuments_Cases FOREIGN KEY ([CaseId]) REFERENCES [Cases] ([Id])
GO

ALTER TABLE [CaseProcessDocuments] ADD CONSTRAINT FK_CaseProcessDocuments_DocumentTypes FOREIGN KEY ([DocumentTypeId]) REFERENCES [DocumentTypes] ([Id])
GO

ALTER TABLE [CaseProcessDocuments] ADD CONSTRAINT FK_CaseProcessDocuments_ProcessDocumentTypes FOREIGN KEY ([ProcessDocumentTypeId]) REFERENCES [ProcessDocumentTypes] ([Id])
GO

ALTER TABLE [ProcessDocumentTypes] ADD CONSTRAINT FK_ProcessDocumentTypes_DocumentTypes FOREIGN KEY ([DocumentTypeId]) REFERENCES [DocumentTypes] ([Id])
GO

ALTER TABLE [CaseDocumentFieldValues] ADD CONSTRAINT FK_CaseDocumentFieldValues_DocumentTypes FOREIGN KEY ([DocumentTypeId]) REFERENCES [DocumentTypes] ([Id])
GO

ALTER TABLE [CaseDocumentFieldValues] ADD CONSTRAINT FK_CaseDocumentFieldValues_ProcessDocumentTypes FOREIGN KEY ([ProcessDocumentTypeId]) REFERENCES [ProcessDocumentTypes] ([Id])
GO

ALTER TABLE [CaseDocumentFieldValues] ADD CONSTRAINT FK_CaseDocumentFieldValues_CaseProcessDocuments FOREIGN KEY ([CaseProcessDocumentId]) REFERENCES [CaseProcessDocuments] ([Id])
GO
