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
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220617033424_Training')
BEGIN
    CREATE TABLE [NhanViens] (
        [Id] int NOT NULL IDENTITY,
        [Department] nvarchar(max) NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [Position] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_NhanViens] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220617033424_Training')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220617033424_Training', N'6.0.6');
END;
GO

COMMIT;
GO

