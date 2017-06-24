USE master ;  
GO  
CREATE DATABASE Kraken  
ON   
( NAME = Kraken_dat,  
    FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL13.SQLEXPRESS\MSSQL\DATA\krakendat.mdf',  
    SIZE = 10,
    MAXSIZE = 50, 
    FILEGROWTH = 5 )  
LOG ON  
( NAME = Kraken_log,  
    FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL13.SQLEXPRESS\MSSQL\DATA\krakenlog.ldf',  
    SIZE = 5MB,
    MAXSIZE = 25MB,  
    FILEGROWTH = 5MB ) ;  
GO

USE Kraken;
GO
drop table [Posts];
drop table [Comments];
drop table [Communities];
drop table [Ratings];
drop table [CommunitySubscriptions];
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
    [KindId] int NOT NULL,
    [TargetKindId] int NOT NULL,
    [TargetId] int NOT NULL,
    [CreateTime] datetime NOT NULL,
);

CREATE TABLE [CommunitySubscriptions] (
    [Id] int primary key NOT NULL IDENTITY(1,1),
    [UserId] int NOT NULL,
    [CommunityId] int NOT NULL,
    [CreateTime] datetime NOT NULL,
);

GO

use kraken;
go
select * from comments;
go