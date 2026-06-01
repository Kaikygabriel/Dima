IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [Category] (
    [Id] uniqueidentifier NOT NULL,
    [Title] VARCHAR(120) NOT NULL,
    [Description] TEXT NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Category] PRIMARY KEY ([Id])
);

CREATE TABLE [IdentityUser] (
    [Id] uniqueidentifier NOT NULL,
    [UserName] VARCHAR(120) NOT NULL,
    [NormalizedUserName] VARCHAR(120) NOT NULL,
    [Email] VARCHAR(160) NOT NULL,
    [NormalizedEmail] VARCHAR(160) NOT NULL,
    [EmailConfirmed] BIT NOT NULL,
    [PasswordHash] NVARCHAR(200) NOT NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] VARCHAR(20) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_IdentityUser] PRIMARY KEY ([Id])
);

CREATE TABLE [Transaction] (
    [Id] uniqueidentifier NOT NULL,
    [Title] VARCHAR(120) NOT NULL,
    [CreateAt] DATETIME2 NOT NULL,
    [PaidOrReceivedAt] DATETIME2 NULL,
    [EType] VARCHAR(50) NOT NULL,
    [Amount] MONEY NOT NULL,
    [CategoryId] uniqueidentifier NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Transaction] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Transaction_Category] FOREIGN KEY ([CategoryId]) REFERENCES [Category] ([Id])
);

CREATE TABLE [IdentityClaim] (
    [Id] int NOT NULL IDENTITY,
    [UserId] uniqueidentifier NOT NULL,
    [ClaimType] VARCHAR(240) NULL,
    [ClaimValue] VARCHAR(240) NULL,
    CONSTRAINT [PK_IdentityClaim] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_IdentityClaim_IdentityUser_UserId] FOREIGN KEY ([UserId]) REFERENCES [IdentityUser] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [IdentityRole] (
    [Id] uniqueidentifier NOT NULL,
    [Name] VARCHAR(140) NULL,
    [NormalizedName] VARCHAR(140) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [UserId] uniqueidentifier NULL,
    CONSTRAINT [PK_IdentityRole] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_IdentityRole_IdentityUser_UserId] FOREIGN KEY ([UserId]) REFERENCES [IdentityUser] ([Id])
);

CREATE TABLE [IdentityUserLogin] (
    [LoginProvider] VARCHAR(255) NOT NULL,
    [ProviderKey] VARCHAR(200) NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    [ProviderDisplayName] VARCHAR(255) NULL,
    CONSTRAINT [PK_IdentityUserLogin] PRIMARY KEY ([LoginProvider], [UserId], [ProviderKey]),
    CONSTRAINT [FK_IdentityUserLogin_IdentityUser_UserId] FOREIGN KEY ([UserId]) REFERENCES [IdentityUser] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [IdentityUserToken] (
    [UserId] uniqueidentifier NOT NULL,
    [LoginProvider] VARCHAR(180) NOT NULL,
    [Name] VARCHAR(160) NULL,
    [Value] VARCHAR(255) NULL,
    CONSTRAINT [PK_IdentityUserToken] PRIMARY KEY ([LoginProvider], [UserId]),
    CONSTRAINT [FK_IdentityUserToken_IdentityUser_UserId] FOREIGN KEY ([UserId]) REFERENCES [IdentityUser] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [IdentityRoleClaim] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] uniqueidentifier NOT NULL,
    [ClaimType] VARCHAR(240) NULL,
    [ClaimValue] VARCHAR(240) NULL,
    CONSTRAINT [PK_IdentityRoleClaim] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_IdentityRoleClaim_IdentityRole_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [IdentityRole] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [IdentityUserRole] (
    [UserId] uniqueidentifier NOT NULL,
    [RoleId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_IdentityUserRole] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_IdentityUserRole_IdentityRole_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [IdentityRole] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_IdentityUserRole_IdentityUser_UserId] FOREIGN KEY ([UserId]) REFERENCES [IdentityUser] ([Id]) ON DELETE CASCADE
);

CREATE INDEX [IX_IdentityClaim_UserId] ON [IdentityClaim] ([UserId]);

CREATE INDEX [IX_IdentityRole_UserId] ON [IdentityRole] ([UserId]);

CREATE UNIQUE INDEX [RoleNameIndex] ON [IdentityRole] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;

CREATE INDEX [IX_IdentityRoleClaim_RoleId] ON [IdentityRoleClaim] ([RoleId]);

CREATE UNIQUE INDEX [EmailIndex] ON [IdentityUser] ([NormalizedEmail]);

CREATE UNIQUE INDEX [UserNameIndex] ON [IdentityUser] ([NormalizedUserName]);

CREATE INDEX [IX_IdentityUserLogin_UserId] ON [IdentityUserLogin] ([UserId]);

CREATE INDEX [IX_IdentityUserRole_RoleId] ON [IdentityUserRole] ([RoleId]);

CREATE INDEX [IX_IdentityUserToken_UserId] ON [IdentityUserToken] ([UserId]);

CREATE INDEX [IX_Transaction_CategoryId] ON [Transaction] ([CategoryId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260531131905_v1', N'10.0.8');

COMMIT;
GO

