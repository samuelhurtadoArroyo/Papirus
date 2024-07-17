CREATE TABLE [dbo].[Users] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [Email]            VARCHAR(300) NOT NULL,
    [FirstName]        VARCHAR(50)  NOT NULL,
    [LastName]         VARCHAR(50)  NOT NULL,
    [HashedPassword]   VARCHAR(MAX) NOT NULL,
    [Salt]             VARCHAR(MAX) NOT NULL,
    [RegistrationDate] DATETIME       NOT NULL,
    [Active]           BIT            NOT NULL DEFAULT 1,
    [RoleId]           INT            NOT NULL,
    [FirmId] INT NOT NULL, 
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Users_Roles] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles] ([Id]),
    CONSTRAINT [UK_Users_Email] UNIQUE NONCLUSTERED ([Email] ASC), 
    CONSTRAINT [FK_Users_Firms] FOREIGN KEY (FirmId) REFERENCES [Firms]([Id])
);

