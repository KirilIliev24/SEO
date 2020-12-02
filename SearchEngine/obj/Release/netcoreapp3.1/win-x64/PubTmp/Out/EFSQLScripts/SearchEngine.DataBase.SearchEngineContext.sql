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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201009084803_InitialCreate')
BEGIN
    CREATE TABLE [Keywords] (
        [Keyword] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_Keywords] PRIMARY KEY ([Keyword])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201009084803_InitialCreate')
BEGIN
    CREATE TABLE [LinkDetails] (
        [Id] int NOT NULL IDENTITY,
        [Link] nvarchar(max) NULL,
        [Title] nvarchar(max) NULL,
        [Snippet] nvarchar(max) NULL,
        [Index] int NOT NULL,
        CONSTRAINT [PK_LinkDetails] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201009084803_InitialCreate')
BEGIN
    CREATE TABLE [LinkTracker] (
        [Keywords] nvarchar(450) NOT NULL,
        [Link] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_LinkTracker] PRIMARY KEY ([Keywords], [Link])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201009084803_InitialCreate')
BEGIN
    CREATE TABLE [PositonAndDates] (
        [PositionAndDateId] int NOT NULL IDENTITY,
        [Keywords] nvarchar(max) NULL,
        [Link] nvarchar(max) NULL,
        [Position] int NOT NULL,
        [Date] datetime2 NOT NULL,
        CONSTRAINT [PK_PositonAndDates] PRIMARY KEY ([PositionAndDateId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201009084803_InitialCreate')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201009084803_InitialCreate', N'5.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201110095536_addJsCssCount')
BEGIN
    ALTER TABLE [PositonAndDates] ADD [Css] float NOT NULL DEFAULT 0.0E0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201110095536_addJsCssCount')
BEGIN
    ALTER TABLE [PositonAndDates] ADD [Js] float NOT NULL DEFAULT 0.0E0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201110095536_addJsCssCount')
BEGIN
    ALTER TABLE [PositonAndDates] ADD [WordCount] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201110095536_addJsCssCount')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201110095536_addJsCssCount', N'5.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201117095312_addExternalLinks')
BEGIN
    CREATE TABLE [ExternalLinks] (
        [IDOfExternal] int NOT NULL IDENTITY,
        [Id] int NOT NULL,
        [date] datetime2 NOT NULL,
        [externalLink] nvarchar(max) NULL,
        CONSTRAINT [PK_ExternalLinks] PRIMARY KEY ([IDOfExternal])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201117095312_addExternalLinks')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201117095312_addExternalLinks', N'5.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201125132702_addMeaningfulText')
BEGIN
    ALTER TABLE [PositonAndDates] ADD [MeaningfulText] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201125132702_addMeaningfulText')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201125132702_addMeaningfulText', N'5.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201201143544_addKeywordsFromText')
BEGIN
    CREATE TABLE [KeywordsInText] (
        [IDOfKMT] int NOT NULL IDENTITY,
        [Id] int NOT NULL,
        [date] datetime2 NOT NULL,
        [keyword] nvarchar(max) NULL,
        [noOfKeywords] int NOT NULL,
        CONSTRAINT [PK_KeywordsInText] PRIMARY KEY ([IDOfKMT])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201201143544_addKeywordsFromText')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201201143544_addKeywordsFromText', N'5.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201202121914_addedNumberInTags')
BEGIN
    EXEC sp_rename N'[KeywordsInText].[noOfKeywords]', N'keywordsInText', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201202121914_addedNumberInTags')
BEGIN
    ALTER TABLE [KeywordsInText] ADD [keywordsInMetaTags] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201202121914_addedNumberInTags')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201202121914_addedNumberInTags', N'5.0.0');
END;
GO

COMMIT;
GO

