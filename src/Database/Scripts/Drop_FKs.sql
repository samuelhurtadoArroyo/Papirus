-- Security
ALTER TABLE [Users] DROP CONSTRAINT FK_Users_Firms
ALTER TABLE [Users] DROP CONSTRAINT FK_Users_Roles
ALTER TABLE [RolePermissions] DROP CONSTRAINT FK_RolePermissions_Roles
ALTER TABLE [RolePermissions] DROP CONSTRAINT FK_RolePermissions_Permissions
GO

-- Teams
ALTER TABLE [TeamMembers] DROP CONSTRAINT FK_TeamMembers_Teams
ALTER TABLE [TeamMembers] DROP CONSTRAINT FK_TeamMembers_Users
ALTER TABLE [Assignments] DROP CONSTRAINT FK_Assignments_Status
ALTER TABLE [Assignments] DROP CONSTRAINT FK_Assignments_TeamMembers
ALTER TABLE [Assignments] DROP CONSTRAINT FK_Assignments_Cases
GO

-- Processes
ALTER TABLE [Processes] DROP CONSTRAINT FK_Processes_ProcessTypes
ALTER TABLE [SubProcesses] DROP CONSTRAINT FK_SubProcesses_Processes
ALTER TABLE [ProcessTemplates] DROP CONSTRAINT FK_ProcessTemplates_ProcessTypes
ALTER TABLE [ProcessTemplates] DROP CONSTRAINT FK_ProcessTemplates_Processes
ALTER TABLE [ProcessTemplates] DROP CONSTRAINT FK_ProcessTemplates_SubProcesses
ALTER TABLE [ProcessTemplates] DROP CONSTRAINT FK_ProcessTemplates_Firms
ALTER TABLE [ProcessDocumentTypes] DROP CONSTRAINT FK_ProcessDocumentTypes_Processes
ALTER TABLE [ProcessDocumentTypes] DROP CONSTRAINT FK_ProcessDocumentTypes_ProcessTemplates
GO

-- People
ALTER TABLE [People] DROP CONSTRAINT FK_People_PersonTypes
ALTER TABLE [People] DROP CONSTRAINT FK_People_IdentificationTypes
ALTER TABLE [Actors] DROP CONSTRAINT FK_Actors_ActorTypes
ALTER TABLE [Actors] DROP CONSTRAINT FK_Actors_People
GO

-- Cases
ALTER TABLE [Cases] DROP CONSTRAINT FK_Cases_ProcessTypes
ALTER TABLE [Cases] DROP CONSTRAINT FK_Cases_Processes
ALTER TABLE [Cases] DROP CONSTRAINT FK_Cases_SubProcesses
ALTER TABLE [Actors] DROP CONSTRAINT FK_Actors_Cases
ALTER TABLE [CaseProcessDocuments] DROP CONSTRAINT FK_CaseProcessDocuments_Cases
ALTER TABLE [CaseProcessDocuments] DROP CONSTRAINT FK_CaseProcessDocuments_DocumentTypes
ALTER TABLE [CaseProcessDocuments] DROP CONSTRAINT FK_CaseProcessDocuments_ProcessDocumentTypes
ALTER TABLE [ProcessDocumentTypes] DROP CONSTRAINT FK_ProcessDocumentTypes_DocumentTypes
ALTER TABLE [CaseDocumentFieldValues] DROP CONSTRAINT FK_CaseDocumentFieldValues_DocumentTypes
ALTER TABLE [CaseDocumentFieldValues] DROP CONSTRAINT FK_CaseDocumentFieldValues_ProcessDocumentTypes
ALTER TABLE [CaseDocumentFieldValues] DROP CONSTRAINT FK_CaseDocumentFieldValues_CaseProcessDocuments
GO
