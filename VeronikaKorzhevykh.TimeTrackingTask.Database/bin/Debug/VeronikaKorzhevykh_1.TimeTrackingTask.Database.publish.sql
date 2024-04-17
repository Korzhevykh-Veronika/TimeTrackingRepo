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
PRINT N'Dropping Check Constraint [dbo].[CK_Employees_Position]...';


GO
ALTER TABLE [dbo].[Employees] DROP CONSTRAINT [CK_Employees_Position];


GO
PRINT N'Altering Table [dbo].[WorkTimes]...';


GO
ALTER TABLE [dbo].[WorkTimes] ALTER COLUMN [ClockInTime] DATETIME NULL;


GO
PRINT N'Creating Unique Constraint unnamed constraint on [dbo].[Employees]...';


GO
ALTER TABLE [dbo].[Employees]
    ADD UNIQUE NONCLUSTERED ([Email] ASC);


GO
PRINT N'Creating Check Constraint [dbo].[CK_Employees_Position]...';


GO
ALTER TABLE [dbo].[Employees] WITH NOCHECK
    ADD CONSTRAINT [CK_Employees_Position] CHECK ([Position] IN ('Manager', 'Developer', 'Designer'));


GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[Employees] WITH CHECK CHECK CONSTRAINT [CK_Employees_Position];


GO
PRINT N'Update complete.';


GO