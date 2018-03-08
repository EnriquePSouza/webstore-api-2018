IF db_id('webstore') IS NULL 
    CREATE DATABASE webstore

GO

CREATE TABLE [webstore].[dbo].[User]
(
	[Id] UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
	[Username] VARCHAR(20) NOT NULL,
	[Password] VARCHAR(32) NOT NULL,
	[Active] BIT NOT NULL
)

CREATE TABLE [webstore].[dbo].[Customer]
(
	[Id] UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
	[UserId] UNIQUEIDENTIFIER NOT NULL,
	[FirstName] VARCHAR(40) NOT NULL,
	[LastName] VARCHAR(40) NOT NULL,
	[Document] CHAR(11) NOT NULL,
	[Email] VARCHAR(160) NOT NULL,
	FOREIGN KEY([UserId]) REFERENCES [User]([Id])
)

CREATE TABLE [webstore].[dbo].[Product]
(
	[Id] UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
	[Title] VARCHAR(255) NOT NULL,
	[Image] VARCHAR(1024) NOT NULL,
	[Price] MONEY NOT NULL,
	[QuantityOnHand] DECIMAL(10, 2) NOT NULL,
)

CREATE TABLE [webstore].[dbo].[Order]
(
	[Id] UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
	[CustomerId] UNIQUEIDENTIFIER NOT NULL,
	[CreateDate] DATETIME NOT NULL DEFAULT(GETDATE()),
	[Status] INT NOT NULL DEFAULT(1),
	FOREIGN KEY([CustomerId]) REFERENCES [Customer]([Id])
)

CREATE TABLE [webstore].[dbo].[OrderItem] 
(
	[Id] UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
	[OrderId] UNIQUEIDENTIFIER NOT NULL,
	[ProductId] UNIQUEIDENTIFIER NOT NULL,
	[Quantity] DECIMAL(10, 2) NOT NULL,
	[Price] MONEY NOT NULL,
	FOREIGN KEY([OrderId]) REFERENCES [Order]([Id]),
	FOREIGN KEY([ProductId]) REFERENCES [Product]([Id])
)

INSERT INTO [webstore].[dbo].[Product]
(
			[Id]
           ,[Title]
           ,[Image]
           ,[Price]
           ,[QuantityOnHand]
)
VALUES
(
		   '76b1ee7e-1a95-42ba-be91-bd47fbe56883'
           ,'Cadeira'
           ,'http://lorempixel.com/250/250/'
           ,'430'
           ,'20'
)

INSERT INTO [webstore].[dbo].[Product]
(
			[Id]
           ,[Title]
           ,[Image]
           ,[Price]
           ,[QuantityOnHand]
)
VALUES
(
		   'BC82A8FC-4A01-4C68-9AAD-5FA74C1F5DAD'
           ,'Teclado'
           ,'http://lorempixel.com/250/250/'
           ,'160'
           ,'20'
)

INSERT INTO [webstore].[dbo].[Product]
(
			[Id]
           ,[Title]
           ,[Image]
           ,[Price]
           ,[QuantityOnHand]
)
VALUES
(
		   '0D69F5CE-1548-4FD9-B078-32DBE5E75F14'
           ,'Mouse'
           ,'http://lorempixel.com/250/250/'
           ,'80'
           ,'20'
)

INSERT INTO [webstore].[dbo].[Product]
(
			[Id]
           ,[Title]
           ,[Image]
           ,[Price]
           ,[QuantityOnHand]
)
VALUES
(
		   '35032F58-5E5A-46D6-907B-2BEBBC4F7B65'
           ,'Monitor'
           ,'http://lorempixel.com/250/250/'
           ,'830'
           ,'20'
)