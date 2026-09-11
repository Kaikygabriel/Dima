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

BEGIN TRANSACTION;
DECLARE @var nvarchar(max);
SELECT @var = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[IdentityUser]') AND [c].[name] = N'Id');
IF @var IS NOT NULL EXEC(N'ALTER TABLE [IdentityUser] DROP CONSTRAINT ' + @var + ';');
ALTER TABLE [IdentityUser] ADD DEFAULT '3f224111-61ca-46d7-9dfb-e3a26419b9e5' FOR [Id];

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260616132034_UpdateModel', N'10.0.8');

COMMIT;
GO

BEGIN TRANSACTION;
DECLARE @var1 nvarchar(max);
SELECT @var1 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[IdentityUser]') AND [c].[name] = N'Id');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [IdentityUser] DROP CONSTRAINT ' + @var1 + ';');

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260616133327_UpdateModel.2', N'10.0.8');

COMMIT;
GO

BEGIN TRANSACTION;
CREATE TABLE [Product] (
    [Id] uniqueidentifier NOT NULL,
    [Title] VARCHAR(160) NOT NULL,
    [Description] VARCHAR(250) NOT NULL,
    [IsActive] BIT NOT NULL,
    [Price] MONEY NOT NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY ([Id])
);

CREATE TABLE [Voucher] (
    [Id] uniqueidentifier NOT NULL,
    [Code] CHAR(8) NOT NULL,
    [Title] VARCHAR(160) NOT NULL,
    [Description] VARCHAR(255) NOT NULL,
    [Amount] MONEY NOT NULL,
    [StartDate] DATETIME2 NOT NULL,
    [EndDate] DATETIME2 NOT NULL,
    [UserId] uniqueidentifier NULL,
    CONSTRAINT [PK_Voucher] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Voucher_IdentityUser_UserId] FOREIGN KEY ([UserId]) REFERENCES [IdentityUser] ([Id])
);

CREATE TABLE [Order] (
    [Id] uniqueidentifier NOT NULL,
    [ExternalReference] VARCHAR(100) NULL,
    [CreateAt] DATETIME2 NOT NULL,
    [UpdateAt] DATETIME2 NOT NULL,
    [PaymentGateway] nvarchar(max) NOT NULL,
    [StatePayment] nvarchar(max) NOT NULL,
    [ProductId] uniqueidentifier NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    [VoucherId] uniqueidentifier NULL,
    CONSTRAINT [PK_Order] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Order_Product_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Product] ([Id]),
    CONSTRAINT [FK_Order_Voucher_VoucherId] FOREIGN KEY ([VoucherId]) REFERENCES [Voucher] ([Id])
);

CREATE INDEX [IX_Order_ProductId] ON [Order] ([ProductId]);

CREATE INDEX [IX_Order_VoucherId] ON [Order] ([VoucherId]);

CREATE UNIQUE INDEX [IX_Voucher_Code] ON [Voucher] ([Code]);

CREATE UNIQUE INDEX [IX_Voucher_Title] ON [Voucher] ([Title]);

CREATE INDEX [IX_Voucher_UserId] ON [Voucher] ([UserId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260727122950_ReportsModels', N'10.0.8');

COMMIT;
GO

BEGIN TRANSACTION;
ALTER TABLE [Voucher] DROP CONSTRAINT [FK_Voucher_IdentityUser_UserId];

DROP INDEX [IX_Voucher_UserId] ON [Voucher];

DECLARE @var2 nvarchar(max);
SELECT @var2 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Voucher]') AND [c].[name] = N'UserId');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Voucher] DROP CONSTRAINT ' + @var2 + ';');
ALTER TABLE [Voucher] DROP COLUMN [UserId];

CREATE TABLE [VoucherUser] (
    [VoucherId] uniqueidentifier NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_VoucherUser] PRIMARY KEY ([VoucherId], [UserId]),
    CONSTRAINT [FK_VoucherUser_UserId] FOREIGN KEY ([UserId]) REFERENCES [IdentityUser] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_VoucherUser_VoucherId] FOREIGN KEY ([VoucherId]) REFERENCES [Voucher] ([Id]) ON DELETE CASCADE
);

CREATE INDEX [IX_VoucherUser_UserId] ON [VoucherUser] ([UserId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260728132908_UpdateReportsModels', N'10.0.8');

COMMIT;
GO

BEGIN TRANSACTION;
ALTER TABLE [Order] ADD [PaytAt] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260803135841_UpdateReportsModels.2', N'10.0.8');

COMMIT;
GO

