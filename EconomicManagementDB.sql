CREATE DATABASE [EconomicManagementDB]
GO
USE [EconomicManagementDB]
GO
CREATE TABLE [Users](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Email] [nvarchar](256) NOT NULL,
	[StandarEmail] [nvarchar](256) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
)
GO
CREATE TABLE [OperationTypes](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[Description] [nvarchar](1000) NOT NULL
)
GO
CREATE TABLE [AccountTypes](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Name] [nvarchar](50) NOT NULL
)
GO
CREATE TABLE [Accounts](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[AccountTypeId] [int] NOT NULL,
	[Balance] [decimal](18, 2) NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[UserId][int] NOT NULL,
    CONSTRAINT [FK_AccountType] FOREIGN KEY (AccountTypeId) REFERENCES AccountTypes(Id),
	CONSTRAINT [FK_AccountUsers] FOREIGN KEY (UserId) REFERENCES Users(Id)
)
GO
CREATE TABLE Categories(
	[Id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Name] [nvarchar](50) NOT NULL
) 
GO
CREATE TABLE [Transactions](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[UserId] [int] NOT NULL,
	[TransactionDate] [datetime] NOT NULL,
	[Total] [decimal](18, 2) NOT NULL,
	[OperationTypeId] [int] NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[AccountId] [int] NOT NULL,
	[CategoryId] [int] NOT NULL,
        CONSTRAINT [FK_TransactionsUsers] FOREIGN KEY (UserId) REFERENCES Users(Id),
	CONSTRAINT [FK_TransactiosOperationType] FOREIGN KEY (OperationTypeId) REFERENCES OperationTypes(Id),
	CONSTRAINT [FK_TransactionsAccount] FOREIGN KEY (AccountId) REFERENCES Accounts(Id),
	CONSTRAINT [FK_TransactionsCategories] FOREIGN KEY (CategoryId) REFERENCES Categories(Id)
)
GO

INSERT INTO [dbo].[OperationTypes]([Name],[Description])VALUES('Ingreso','Llega dinero');
INSERT INTO [dbo].[OperationTypes]([Name],[Description])VALUES('Gasto','Sale dinero');


INSERT INTO [dbo].[Categories]([Name])VALUES('Ocio');
INSERT INTO [dbo].[Categories]([Name])VALUES('Viajes');


INSERT INTO [dbo].[AccountTypes]([Name])VALUES('Corriente');
INSERT INTO [dbo].[AccountTypes]([Name])VALUES('Ahorros');
GO