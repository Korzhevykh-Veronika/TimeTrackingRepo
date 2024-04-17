﻿/*
Deployment script for time-tracking-tdb

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "time-tracking-tdb"
:setvar DefaultFilePrefix "time-tracking-tdb"
:setvar DefaultDataPath "C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\"
:setvar DefaultLogPath "C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
/*
The column [dbo].[Projects].[CustomerName] is being dropped, data loss could occur.

The column [dbo].[Projects].[EndDate] is being dropped, data loss could occur.

The column [dbo].[Projects].[StartDate] is being dropped, data loss could occur.

The type for column Id in table [dbo].[Projects] is currently  INT IDENTITY (1, 1) NOT NULL but is being changed to  UNIQUEIDENTIFIER NOT NULL. There is no implicit or explicit conversion.
*/

IF EXISTS (select top 1 1 from [dbo].[Projects])
    RAISERROR (N'Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
/*
The column [dbo].[WorkTimes].[EmployeeId] is being dropped, data loss could occur.

The column [dbo].[WorkTimes].[UserId] on table [dbo].[WorkTimes] must be added, but the column has no default value and does not allow NULL values. If the table contains data, the ALTER script will not work. To avoid this issue you must either: add a default value to the column, mark it as allowing NULL values, or enable the generation of smart-defaults as a deployment option.

The column ClockInTime on table [dbo].[WorkTimes] must be changed from NULL to NOT NULL. If the table contains data, the ALTER script may not work. To avoid this issue, you must add values to this column for all rows or mark it as allowing NULL values, or enable the generation of smart-defaults as a deployment option.

The type for column Id in table [dbo].[WorkTimes] is currently  INT IDENTITY (1, 1) NOT NULL but is being changed to  UNIQUEIDENTIFIER NOT NULL. There is no implicit or explicit conversion.

The type for column ProjectId in table [dbo].[WorkTimes] is currently  INT NOT NULL but is being changed to  UNIQUEIDENTIFIER NOT NULL. There is no implicit or explicit conversion.
*/

IF EXISTS (select top 1 1 from [dbo].[WorkTimes])
    RAISERROR (N'Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
PRINT N'Dropping Foreign Key [dbo].[FK_WorkTimes_Projects]...';


GO
ALTER TABLE [dbo].[WorkTimes] DROP CONSTRAINT [FK_WorkTimes_Projects];


GO
PRINT N'Dropping Foreign Key [dbo].[FK_WorkTimes_Employees]...';


GO
ALTER TABLE [dbo].[WorkTimes] DROP CONSTRAINT [FK_WorkTimes_Employees];


GO
PRINT N'Starting rebuilding table [dbo].[Projects]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Projects] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [Title]       NVARCHAR (250)   NOT NULL,
    [Description] NVARCHAR (250)   NULL,
    UNIQUE NONCLUSTERED ([Title] ASC),
    CONSTRAINT [tmp_ms_xx_constraint_PK_Projects_Id1] PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Projects])
    BEGIN
        INSERT INTO [dbo].[tmp_ms_xx_Projects] ([Id], [Title], [Description])
        SELECT   [Id],
                 [Title],
                 [Description]
        FROM     [dbo].[Projects]
        ORDER BY [Id] ASC;
    END

DROP TABLE [dbo].[Projects];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Projects]', N'Projects';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_Projects_Id1]', N'PK_Projects_Id', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Starting rebuilding table [dbo].[WorkTimes]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_WorkTimes] (
    [Id]           UNIQUEIDENTIFIER NOT NULL,
    [UserId]       UNIQUEIDENTIFIER NOT NULL,
    [ProjectId]    UNIQUEIDENTIFIER NOT NULL,
    [ClockInTime]  DATETIME         NOT NULL,
    [ClockOutTime] DATETIME         NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_WorkTimes_Id1] PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[WorkTimes])
    BEGIN
        INSERT INTO [dbo].[tmp_ms_xx_WorkTimes] ([Id], [ProjectId], [ClockInTime], [ClockOutTime])
        SELECT   [Id],
                 [ProjectId],
                 [ClockInTime],
                 [ClockOutTime]
        FROM     [dbo].[WorkTimes]
        ORDER BY [Id] ASC;
    END

DROP TABLE [dbo].[WorkTimes];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_WorkTimes]', N'WorkTimes';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_WorkTimes_Id1]', N'PK_WorkTimes_Id', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating Foreign Key [dbo].[FK_WorkTimes_Projects]...';


GO
ALTER TABLE [dbo].[WorkTimes] WITH NOCHECK
    ADD CONSTRAINT [FK_WorkTimes_Projects] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Projects] ([Id]);


GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[WorkTimes] WITH CHECK CHECK CONSTRAINT [FK_WorkTimes_Projects];


GO
PRINT N'Update complete.';


GO