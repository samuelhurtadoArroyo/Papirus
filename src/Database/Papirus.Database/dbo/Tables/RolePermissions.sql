CREATE TABLE [dbo].[RolePermissions]
(
    [Id] INT NOT NULL IDENTITY, 
    [RoleId] INT NOT NULL, 
    [PermissionId] INT NOT NULL, 
    CONSTRAINT [PK_RolePermissions] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_RolePermissions_Roles] FOREIGN KEY ([RoleId]) REFERENCES Roles([Id]),
    CONSTRAINT [FK_RolePermissions_Permissions] FOREIGN KEY (PermissionId) REFERENCES Permissions([Id])
)
