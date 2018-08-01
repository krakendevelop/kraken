USE master ;  
GO  
CREATE DATABASE Kraken  
ON   
( NAME = Kraken_dat,  
    FILENAME = 'g:\Programs\SQL_Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\krakendat.mdf',  
    SIZE = 10,
    MAXSIZE = 50, 
    FILEGROWTH = 5 )  
LOG ON  
( NAME = Kraken_log,  
    FILENAME = 'g:\Programs\SQL_Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\krakenlog.ldf',  
    SIZE = 5MB,
    MAXSIZE = 25MB,  
    FILEGROWTH = 5MB ) ;  
GO

USE Kraken;
GO

CREATE TABLE [Posts] (
    [Id] int primary key NOT NULL IDENTITY(1,1),
    [UserId] int NOT NULL,
    [CommunityId] int NULL,

    [Text] nvarchar(max) NULL,
    [ImageUrl] nvarchar(max) NULL,

    [CreateTime] datetime NOT NULL,
    [UpdateTime] datetime NOT NULL,

    [IsDeleted] bit NOT NULL,
);

CREATE TABLE [Comments] (
    [Id] int primary key NOT NULL IDENTITY(1,1),
    [UserId] int NOT NULL,
    [PostId] int NOT NULL,
    [CommentId] int NULL,
    
    [Text] nvarchar(max) NULL,
    [ImageUrl] nvarchar(max) NULL,
    
    [CreateTime] datetime NOT NULL,
    [UpdateTime] datetime NOT NULL,
    
    [IsDeleted] bit NOT NULL,
);

CREATE TABLE [Communities] (
    [Id] int primary key NOT NULL IDENTITY(1,1),
    [OwnerUserId] int NOT NULL,
    [Name] nvarchar(2048) NOT NULL,
    [ImageUrl] nvarchar(max) NULL,
    
    [CreateTime] datetime NOT NULL,
    [UpdateTime] datetime NOT NULL,
    
    [IsDeleted] bit NOT NULL,
);

CREATE TABLE [Ratings] (
    [Id] int primary key NOT NULL IDENTITY(1,1),
    [UserId] int NOT NULL,
    [KindId] int NOT NULL,
    [TargetKindId] int NOT NULL,
    [TargetId] int NOT NULL,
    [CreateTime] datetime NOT NULL,
);

CREATE TABLE [Follows] (
    [Id] int primary key NOT NULL IDENTITY(1,1),
    [InitiatorUserId] int NOT NULL,
    [TargetUserId] int NOT NULL,
    [CreateTime] datetime NOT NULL,
);

GO

use Kraken;
-- Creates the login AbolrousHazem with password '340$Uuxwp7Mcxo7Khy'.  
IF NOT EXISTS (SELECT name FROM sys.server_principals WHERE name = 'IIS APPPOOL\KrakenPool')
BEGIN
    CREATE LOGIN [IIS APPPOOL\KrakenPool] 
      FROM WINDOWS WITH DEFAULT_DATABASE=[master], 
      DEFAULT_LANGUAGE=[us_english]
END
GO

use Kraken;
CREATE USER [IIS APPPOOL\KrakenPool] 
  FOR LOGIN [IIS APPPOOL\KrakenPool]
GO
EXEC sp_addrolemember 'db_owner', 'IIS APPPOOL\KrakenPool'
GO