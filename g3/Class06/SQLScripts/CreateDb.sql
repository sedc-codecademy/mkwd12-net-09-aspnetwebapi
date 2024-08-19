CREATE DATABASE [NotesScaffoldDb]
GO

USE [NotesScaffoldDb]
GO

CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Username] [nvarchar](30) NULL,
	[Password] [nvarchar](max) NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL
)
GO

CREATE TABLE [dbo].[Notes](
	[Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Text] [nvarchar](100) NULL,
	[Color] [nvarchar](30) NULL,
	[Tag] [int] NOT NULL,
	[UserId] [int] NOT NULL FOREIGN KEY REFERENCES Users(Id)
)
GO