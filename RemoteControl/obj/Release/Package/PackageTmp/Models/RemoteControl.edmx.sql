
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/28/2015 19:09:16
-- Generated from EDMX file: d:\1\RC2_27052015\RemoteControl\Models\RemoteControl.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [RC2];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_CameraLANController]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Camera] DROP CONSTRAINT [FK_CameraLANController];
GO
IF OBJECT_ID(N'[dbo].[FK_CameraUserCamera]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserCamera] DROP CONSTRAINT [FK_CameraUserCamera];
GO
IF OBJECT_ID(N'[dbo].[FK_LANControllerButton]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ButtonSet] DROP CONSTRAINT [FK_LANControllerButton];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Camera]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Camera];
GO
IF OBJECT_ID(N'[dbo].[LANController]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LANController];
GO
IF OBJECT_ID(N'[dbo].[UserCamera]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserCamera];
GO
IF OBJECT_ID(N'[dbo].[ButtonSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ButtonSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Camera'
CREATE TABLE [dbo].[Camera] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [CameraPath] nvarchar(max)  NOT NULL,
    [State] int  NOT NULL,
    [Category] int  NOT NULL,
    [ControllerID_Id] int  NULL,
    [CameraAction] int  NOT NULL,
    [UserManager] nvarchar(max)  NULL,
    [UserOwner] nvarchar(max)  NOT NULL,
    [IsPublic] bit  NOT NULL,
    [IsWork] bit  NOT NULL
);
GO

-- Creating table 'LANController'
CREATE TABLE [dbo].[LANController] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [ControllerPath] nvarchar(max)  NOT NULL,
    [UserManager] nvarchar(max)  NULL,
    [UserOwner] nvarchar(max)  NOT NULL,
    [IsPublic] bit  NOT NULL,
    [IsWork] bit  NOT NULL
);
GO

-- Creating table 'UserCamera'
CREATE TABLE [dbo].[UserCamera] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [StartUse] datetime  NULL,
    [EndUse] datetime  NULL,
    [CommonTimeUse] time  NULL,
    [UserId] nvarchar(max)  NOT NULL,
    [CameraId] int  NOT NULL,
    [UserName] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ButtonSet'
CREATE TABLE [dbo].[ButtonSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [LANControllerId] int  NOT NULL,
    [Left] nvarchar(max)  NOT NULL,
    [Right] nvarchar(max)  NOT NULL,
    [Top] nvarchar(max)  NOT NULL,
    [Bottom] nvarchar(max)  NOT NULL,
    [Width] nvarchar(max)  NOT NULL,
    [Height] nvarchar(max)  NOT NULL,
    [Out] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Camera'
ALTER TABLE [dbo].[Camera]
ADD CONSTRAINT [PK_Camera]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'LANController'
ALTER TABLE [dbo].[LANController]
ADD CONSTRAINT [PK_LANController]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserCamera'
ALTER TABLE [dbo].[UserCamera]
ADD CONSTRAINT [PK_UserCamera]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ButtonSet'
ALTER TABLE [dbo].[ButtonSet]
ADD CONSTRAINT [PK_ButtonSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ControllerID_Id] in table 'Camera'
ALTER TABLE [dbo].[Camera]
ADD CONSTRAINT [FK_CameraLANController]
    FOREIGN KEY ([ControllerID_Id])
    REFERENCES [dbo].[LANController]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CameraLANController'
CREATE INDEX [IX_FK_CameraLANController]
ON [dbo].[Camera]
    ([ControllerID_Id]);
GO

-- Creating foreign key on [CameraId] in table 'UserCamera'
ALTER TABLE [dbo].[UserCamera]
ADD CONSTRAINT [FK_CameraUserCamera]
    FOREIGN KEY ([CameraId])
    REFERENCES [dbo].[Camera]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CameraUserCamera'
CREATE INDEX [IX_FK_CameraUserCamera]
ON [dbo].[UserCamera]
    ([CameraId]);
GO

-- Creating foreign key on [LANControllerId] in table 'ButtonSet'
ALTER TABLE [dbo].[ButtonSet]
ADD CONSTRAINT [FK_LANControllerButton]
    FOREIGN KEY ([LANControllerId])
    REFERENCES [dbo].[LANController]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LANControllerButton'
CREATE INDEX [IX_FK_LANControllerButton]
ON [dbo].[ButtonSet]
    ([LANControllerId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------