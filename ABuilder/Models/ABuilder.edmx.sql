
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 08/28/2013 18:28:59
-- Generated from EDMX file: C:\Users\Dan\Documents\Visual Studio 2012\Projects\ABuilder\ABuilder\Models\ABuilder.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ABuilder];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_StatSingleModel_Stat]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SingleModel_Stat] DROP CONSTRAINT [FK_StatSingleModel_Stat];
GO
IF OBJECT_ID(N'[dbo].[FK_SingleModelSingleModel_Stat]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SingleModel_Stat] DROP CONSTRAINT [FK_SingleModelSingleModel_Stat];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[SingleModels]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SingleModels];
GO
IF OBJECT_ID(N'[dbo].[Stats]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Stats];
GO
IF OBJECT_ID(N'[dbo].[SingleModel_Stat]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SingleModel_Stat];
GO
IF OBJECT_ID(N'[dbo].[Equations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Equations];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'SingleModels'
CREATE TABLE [dbo].[SingleModels] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Stats'
CREATE TABLE [dbo].[Stats] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Acronym] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'SingleModel_Stat'
CREATE TABLE [dbo].[SingleModel_Stat] (
    [SingleModelId] int  NOT NULL,
    [Value] smallint  NOT NULL,
    [StatId] int  NOT NULL
);
GO

-- Creating table 'Equations'
CREATE TABLE [dbo].[Equations] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Value] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'SingleModels'
ALTER TABLE [dbo].[SingleModels]
ADD CONSTRAINT [PK_SingleModels]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Stats'
ALTER TABLE [dbo].[Stats]
ADD CONSTRAINT [PK_Stats]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [SingleModelId], [StatId] in table 'SingleModel_Stat'
ALTER TABLE [dbo].[SingleModel_Stat]
ADD CONSTRAINT [PK_SingleModel_Stat]
    PRIMARY KEY CLUSTERED ([SingleModelId], [StatId] ASC);
GO

-- Creating primary key on [Id] in table 'Equations'
ALTER TABLE [dbo].[Equations]
ADD CONSTRAINT [PK_Equations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [StatId] in table 'SingleModel_Stat'
ALTER TABLE [dbo].[SingleModel_Stat]
ADD CONSTRAINT [FK_StatSingleModel_Stat]
    FOREIGN KEY ([StatId])
    REFERENCES [dbo].[Stats]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_StatSingleModel_Stat'
CREATE INDEX [IX_FK_StatSingleModel_Stat]
ON [dbo].[SingleModel_Stat]
    ([StatId]);
GO

-- Creating foreign key on [SingleModelId] in table 'SingleModel_Stat'
ALTER TABLE [dbo].[SingleModel_Stat]
ADD CONSTRAINT [FK_SingleModelSingleModel_Stat]
    FOREIGN KEY ([SingleModelId])
    REFERENCES [dbo].[SingleModels]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------