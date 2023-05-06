USE [master]
GO
/****** Object:  Database [DelawareDB]    Script Date: 22/12/2022 14:56:25 ******/
CREATE DATABASE [DelawareDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DelawareDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\DelawareDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DelawareDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\DelawareDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [DelawareDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DelawareDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DelawareDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DelawareDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DelawareDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DelawareDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DelawareDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [DelawareDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DelawareDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DelawareDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DelawareDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DelawareDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DelawareDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DelawareDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DelawareDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DelawareDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DelawareDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [DelawareDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DelawareDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DelawareDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DelawareDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DelawareDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DelawareDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DelawareDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DelawareDB] SET RECOVERY FULL 
GO
ALTER DATABASE [DelawareDB] SET  MULTI_USER 
GO
ALTER DATABASE [DelawareDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DelawareDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DelawareDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DelawareDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DelawareDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [DelawareDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'DelawareDB', N'ON'
GO
ALTER DATABASE [DelawareDB] SET QUERY_STORE = OFF
GO
USE [DelawareDB]
GO
/****** Object:  Schema [delaware_db]    Script Date: 22/12/2022 14:56:26 ******/
CREATE SCHEMA [delaware_db]
GO
/****** Object:  Table [delaware_db].[notification]    Script Date: 22/12/2022 14:56:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [delaware_db].[notification](
	[idnotification] [nvarchar](20) NOT NULL,
	[message] [nvarchar](max) NOT NULL,
	[isSeen] [smallint] NOT NULL,
	[duration] [int] NULL,
	[notificationDate] [datetime2](0) NOT NULL,
	[orderId] [nvarchar](20) NULL,
 CONSTRAINT [PK_notification_idnotification] PRIMARY KEY CLUSTERED 
(
	[idnotification] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [delaware_db].[order]    Script Date: 22/12/2022 14:56:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [delaware_db].[order](
	[idorder] [nvarchar](20) NOT NULL,
	[netPrice] [float] NOT NULL,
	[orderDate] [datetime2](0) NOT NULL,
	[statusDate] [datetime2](0) NOT NULL,
	[orderStatus] [nvarchar](45) NOT NULL,
	[deliveryAdress] [nvarchar](45) NOT NULL,
	[invoiceAdress] [nvarchar](45) NOT NULL,
	[userId] [nvarchar](20) NOT NULL,
	[packageId] [nvarchar](20) NOT NULL,
	[taxAmount] [int] NOT NULL,
	[totalAmount] [float] NOT NULL,
 CONSTRAINT [PK_order_idorder] PRIMARY KEY CLUSTERED 
(
	[idorder] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [delaware_db].[orderitem]    Script Date: 22/12/2022 14:56:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [delaware_db].[orderitem](
	[idorderItem] [nvarchar](20) NOT NULL,
	[quantity] [int] NOT NULL,
	[totalPrice] [float] NOT NULL,
	[productId] [nvarchar](20) NOT NULL,
	[orderId] [nvarchar](20) NULL,
 CONSTRAINT [PK_orderitem_idorderItem] PRIMARY KEY CLUSTERED 
(
	[idorderItem] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [delaware_db].[package]    Script Date: 22/12/2022 14:56:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [delaware_db].[package](
	[idpackage] [nvarchar](20) NOT NULL,
	[height] [float] NOT NULL,
	[width] [float] NOT NULL,
	[debth] [float] NOT NULL,
	[price] [float] NOT NULL,
 CONSTRAINT [PK_package_idpackage] PRIMARY KEY CLUSTERED 
(
	[idpackage] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [delaware_db].[product]    Script Date: 22/12/2022 14:56:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [delaware_db].[product](
	[idproduct] [nvarchar](20) NOT NULL,
	[productCatergoryId] [int] NOT NULL,
	[name] [nvarchar](45) NOT NULL,
	[unitPrice] [float] NOT NULL,
	[leftInStock] [int] NOT NULL,
	[description] [nvarchar](max) NULL,
	[height] [float] NOT NULL,
	[width] [float] NOT NULL,
	[depth] [float] NOT NULL,
	[supplierId] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_product_idproduct] PRIMARY KEY CLUSTERED 
(
	[idproduct] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [delaware_db].[supplier]    Script Date: 22/12/2022 14:56:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [delaware_db].[supplier](
	[idsupplier] [nvarchar](20) NOT NULL,
	[name] [nvarchar](45) NOT NULL,
	[adress] [nvarchar](45) NOT NULL,
 CONSTRAINT [PK_supplier_idsupplier] PRIMARY KEY CLUSTERED 
(
	[idsupplier] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [delaware_db].[user]    Script Date: 22/12/2022 14:56:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [delaware_db].[user](
	[username] [nvarchar](16) NOT NULL,
	[email] [nvarchar](255) NOT NULL,
	[iduser] [nvarchar](45) NOT NULL,
	[adress] [nvarchar](45) NOT NULL,
 CONSTRAINT [PK_user_iduser] PRIMARY KEY CLUSTERED 
(
	[iduser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [delaware_db].[notification] ([idnotification], [message], [isSeen], [duration], [notificationDate], [orderId]) VALUES (N'11', N'Uw bestelling werd succesvol aangemaakt!', 0, 1, CAST(N'2022-12-05T08:05:01.0000000' AS DateTime2), N'47072')
GO
INSERT [delaware_db].[notification] ([idnotification], [message], [isSeen], [duration], [notificationDate], [orderId]) VALUES (N'12', N'Uw bestelling werd verwerkt!', 0, 1, CAST(N'2022-12-06T14:16:01.0000000' AS DateTime2), N'47072')
GO
INSERT [delaware_db].[notification] ([idnotification], [message], [isSeen], [duration], [notificationDate], [orderId]) VALUES (N'13', N'De bezorger is onderweg', 0, 1, CAST(N'2022-12-07T08:05:01.0000000' AS DateTime2), N'47072')
GO
INSERT [delaware_db].[notification] ([idnotification], [message], [isSeen], [duration], [notificationDate], [orderId]) VALUES (N'14', N'Uw bestelling werd succesvol aangemaakt!', 0, 1, CAST(N'2022-12-02T16:24:09.0000000' AS DateTime2), N'47073')
GO
INSERT [delaware_db].[notification] ([idnotification], [message], [isSeen], [duration], [notificationDate], [orderId]) VALUES (N'15', N'Uw bestelling werd verwerkt!', 0, 1, CAST(N'2022-12-03T08:49:01.0000000' AS DateTime2), N'47073')
GO
INSERT [delaware_db].[notification] ([idnotification], [message], [isSeen], [duration], [notificationDate], [orderId]) VALUES (N'16', N'De bezorger is onderweg', 0, 1, CAST(N'2022-12-03T13:05:01.0000000' AS DateTime2), N'47073')
GO
INSERT [delaware_db].[notification] ([idnotification], [message], [isSeen], [duration], [notificationDate], [orderId]) VALUES (N'17', N'Uw bestelling is geleverd!', 0, 1, CAST(N'2022-12-03T17:05:01.0000000' AS DateTime2), N'47073')
GO
INSERT [delaware_db].[notification] ([idnotification], [message], [isSeen], [duration], [notificationDate], [orderId]) VALUES (N'18', N'Uw bestelling werd succesvol aangemaakt!', 0, 1, CAST(N'2022-12-09T10:05:26.0000000' AS DateTime2), N'47076')
GO
INSERT [delaware_db].[notification] ([idnotification], [message], [isSeen], [duration], [notificationDate], [orderId]) VALUES (N'21', N'Uw bestelling werd succesvol aangemaakt!', 0, 1, CAST(N'2022-12-08T08:05:01.0000000' AS DateTime2), N'47081')
GO
INSERT [delaware_db].[notification] ([idnotification], [message], [isSeen], [duration], [notificationDate], [orderId]) VALUES (N'22', N'Uw bestelling werd verwerkt!', 0, 1, CAST(N'2022-12-08T18:33:01.0000000' AS DateTime2), N'47081')
GO
INSERT [delaware_db].[notification] ([idnotification], [message], [isSeen], [duration], [notificationDate], [orderId]) VALUES (N'cb76db56-e6aa-45', N'Uw bestelling werd succesvol aangemaakt!', 0, 1, CAST(N'2022-12-21T14:43:27.0000000' AS DateTime2), N'1b63d8')
GO
INSERT [delaware_db].[order] ([idorder], [netPrice], [orderDate], [statusDate], [orderStatus], [deliveryAdress], [invoiceAdress], [userId], [packageId], [taxAmount], [totalAmount]) VALUES (N'1b63d8', 532.7, CAST(N'2022-12-21T14:43:27.0000000' AS DateTime2), CAST(N'2022-12-21T14:43:27.0000000' AS DateTime2), N'1', N'Potterstraat 51, 9170 Sint-Pauwels', N'Potterstraat 51, 9170 Sint-Pauwels', N'1', N'4667fe03-9d9f-4f', 1, 532.7)
GO
INSERT [delaware_db].[order] ([idorder], [netPrice], [orderDate], [statusDate], [orderStatus], [deliveryAdress], [invoiceAdress], [userId], [packageId], [taxAmount], [totalAmount]) VALUES (N'47072', 400, CAST(N'2022-12-05T08:05:01.0000000' AS DateTime2), CAST(N'2022-12-07T08:05:01.0000000' AS DateTime2), N'3', N'Stationsstraat 5, 9080 Lochristi', N'Stationsstraat 5, 9080 Lochristi', N'CP100110', N'11', 80, 480)
GO
INSERT [delaware_db].[order] ([idorder], [netPrice], [orderDate], [statusDate], [orderStatus], [deliveryAdress], [invoiceAdress], [userId], [packageId], [taxAmount], [totalAmount]) VALUES (N'47073', 1.71, CAST(N'2022-12-02T16:24:09.0000000' AS DateTime2), CAST(N'2022-12-03T17:05:01.0000000' AS DateTime2), N'4', N'Gentstraat 30, 9041 Gent', N'Gentstraat 30, 9041 Gent', N'CP100110', N'12', 342, 2.052)
GO
INSERT [delaware_db].[order] ([idorder], [netPrice], [orderDate], [statusDate], [orderStatus], [deliveryAdress], [invoiceAdress], [userId], [packageId], [taxAmount], [totalAmount]) VALUES (N'47076', 1.71, CAST(N'2022-12-09T10:05:26.0000000' AS DateTime2), CAST(N'2022-12-09T10:05:26.0000000' AS DateTime2), N'1', N'Zandstraat 55, 8200 Brugge', N'Zandstraat 55, 8200 Brugge', N'CP100110', N'15', 342, 2.052)
GO
INSERT [delaware_db].[order] ([idorder], [netPrice], [orderDate], [statusDate], [orderStatus], [deliveryAdress], [invoiceAdress], [userId], [packageId], [taxAmount], [totalAmount]) VALUES (N'47081', 1.71, CAST(N'2022-12-08T08:05:01.0000000' AS DateTime2), CAST(N'2022-12-08T18:33:01.0000000' AS DateTime2), N'2', N'Spiegelstraat 20, 1730 Asse', N'Spiegelstraat 20, 1730 Asse', N'CP100110', N'18', 342, 2.052)
GO
INSERT [delaware_db].[orderitem] ([idorderItem], [quantity], [totalPrice], [productId], [orderId]) VALUES (N'11', 40, 400, N'P300009', N'47072')
GO
INSERT [delaware_db].[orderitem] ([idorderItem], [quantity], [totalPrice], [productId], [orderId]) VALUES (N'12', 10, 1.5, N'EXH_001_BLACK', N'47073')
GO
INSERT [delaware_db].[orderitem] ([idorderItem], [quantity], [totalPrice], [productId], [orderId]) VALUES (N'15', 10, 1.5, N'EXH_001_BLACK', N'47076')
GO
INSERT [delaware_db].[orderitem] ([idorderItem], [quantity], [totalPrice], [productId], [orderId]) VALUES (N'16', 10, 210, N'SPARE_01', N'47076')
GO
INSERT [delaware_db].[orderitem] ([idorderItem], [quantity], [totalPrice], [productId], [orderId]) VALUES (N'20', 10, 1.5, N'EXH_001_BLACK', N'47081')
GO
INSERT [delaware_db].[orderitem] ([idorderItem], [quantity], [totalPrice], [productId], [orderId]) VALUES (N'21', 10, 210, N'SPARE_01', N'47081')
GO
INSERT [delaware_db].[orderitem] ([idorderItem], [quantity], [totalPrice], [productId], [orderId]) VALUES (N'a5869bb5-018a-43', 1, 500, N'EXH_001_BLACK', N'1b63d8')
GO
INSERT [delaware_db].[orderitem] ([idorderItem], [quantity], [totalPrice], [productId], [orderId]) VALUES (N'f44e0dfa-3c83-4a', 1, 0, N'P120102', N'1b63d8')
GO
INSERT [delaware_db].[package] ([idpackage], [height], [width], [debth], [price]) VALUES (N'11', 110, 55, 60, 480)
GO
INSERT [delaware_db].[package] ([idpackage], [height], [width], [debth], [price]) VALUES (N'12', 120, 60, 22, 2.052)
GO
INSERT [delaware_db].[package] ([idpackage], [height], [width], [debth], [price]) VALUES (N'15', 150, 75, 22, 2.052)
GO
INSERT [delaware_db].[package] ([idpackage], [height], [width], [debth], [price]) VALUES (N'18', 180, 90, 22, 2.052)
GO
INSERT [delaware_db].[package] ([idpackage], [height], [width], [debth], [price]) VALUES (N'4667fe03-9d9f-4f', 104, 54, 66, 532)
GO
INSERT [delaware_db].[product] ([idproduct], [productCatergoryId], [name], [unitPrice], [leftInStock], [description], [height], [width], [depth], [supplierId]) VALUES (N'EXH_001_BLACK', 193, N'sokken', 500, 1000, N'nieuwe sokken', 30, 20, 30, N'1')
GO
INSERT [delaware_db].[product] ([idproduct], [productCatergoryId], [name], [unitPrice], [leftInStock], [description], [height], [width], [depth], [supplierId]) VALUES (N'P120102', 5, N'Fish Frenzy T-shirt', 0.7, 500, N'een tshirt', 101, 50, 50, N'1')
GO
INSERT [delaware_db].[product] ([idproduct], [productCatergoryId], [name], [unitPrice], [leftInStock], [description], [height], [width], [depth], [supplierId]) VALUES (N'P300009', 3, N'visserspak', 630, 200, N'Heupborstpakket - Klaar om vestvrij te worden? Dit is het klassieke alternatief', 160, 60, 50, N'2')
GO
INSERT [delaware_db].[product] ([idproduct], [productCatergoryId], [name], [unitPrice], [leftInStock], [description], [height], [width], [depth], [supplierId]) VALUES (N'SPARE_01', 4, N'trui', 200, 8000, N'Het zachte wafelbreisel van de Mandeville is een soepele mix van 40% biologisch katoen, 35% Tencel en 20% gerecycled polyester met 5% spandex voor stretch. De ribgebreide capuchon (met trekkoord) heeft een volledige ritssluiting.', 101, 60, 60, N'1')
GO
INSERT [delaware_db].[supplier] ([idsupplier], [name], [adress]) VALUES (N'1', N'Jack&Jones', N'Veldstraat 65, 9000 Gent')
GO
INSERT [delaware_db].[supplier] ([idsupplier], [name], [adress]) VALUES (N'2', N'Trendretail', N'Westerpark 76, 1742BX SCHAGEN')
GO
INSERT [delaware_db].[supplier] ([idsupplier], [name], [adress]) VALUES (N'3', N'Mediamarkt', N'Kernenergiestraat 56, 2610 Antwerpen')
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [orderNotific_idx]    Script Date: 22/12/2022 14:56:26 ******/
CREATE NONCLUSTERED INDEX [orderNotific_idx] ON [delaware_db].[notification]
(
	[orderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [package_idx]    Script Date: 22/12/2022 14:56:26 ******/
CREATE NONCLUSTERED INDEX [package_idx] ON [delaware_db].[order]
(
	[packageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [user_idx]    Script Date: 22/12/2022 14:56:26 ******/
CREATE NONCLUSTERED INDEX [user_idx] ON [delaware_db].[order]
(
	[userId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [orderOrderItem_idx]    Script Date: 22/12/2022 14:56:26 ******/
CREATE NONCLUSTERED INDEX [orderOrderItem_idx] ON [delaware_db].[orderitem]
(
	[orderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [product_idx]    Script Date: 22/12/2022 14:56:26 ******/
CREATE NONCLUSTERED INDEX [product_idx] ON [delaware_db].[orderitem]
(
	[productId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [supplier_idx]    Script Date: 22/12/2022 14:56:26 ******/
CREATE NONCLUSTERED INDEX [supplier_idx] ON [delaware_db].[product]
(
	[supplierId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [delaware_db].[notification] ADD  DEFAULT (NULL) FOR [duration]
GO
ALTER TABLE [delaware_db].[notification] ADD  DEFAULT (NULL) FOR [orderId]
GO
ALTER TABLE [delaware_db].[orderitem] ADD  DEFAULT (NULL) FOR [orderId]
GO
ALTER TABLE [delaware_db].[notification]  WITH NOCHECK ADD  CONSTRAINT [notification$orderNotific] FOREIGN KEY([orderId])
REFERENCES [delaware_db].[order] ([idorder])
GO
ALTER TABLE [delaware_db].[notification] CHECK CONSTRAINT [notification$orderNotific]
GO
ALTER TABLE [delaware_db].[order]  WITH NOCHECK ADD  CONSTRAINT [order$package] FOREIGN KEY([packageId])
REFERENCES [delaware_db].[package] ([idpackage])
GO
ALTER TABLE [delaware_db].[order] CHECK CONSTRAINT [order$package]
GO
ALTER TABLE [delaware_db].[orderitem]  WITH NOCHECK ADD  CONSTRAINT [orderitem$orderOrderItem] FOREIGN KEY([orderId])
REFERENCES [delaware_db].[order] ([idorder])
GO
ALTER TABLE [delaware_db].[orderitem] CHECK CONSTRAINT [orderitem$orderOrderItem]
GO
ALTER TABLE [delaware_db].[orderitem]  WITH NOCHECK ADD  CONSTRAINT [orderitem$product] FOREIGN KEY([productId])
REFERENCES [delaware_db].[product] ([idproduct])
GO
ALTER TABLE [delaware_db].[orderitem] CHECK CONSTRAINT [orderitem$product]
GO
ALTER TABLE [delaware_db].[product]  WITH NOCHECK ADD  CONSTRAINT [product$supplier] FOREIGN KEY([supplierId])
REFERENCES [delaware_db].[supplier] ([idsupplier])
GO
ALTER TABLE [delaware_db].[product] CHECK CONSTRAINT [product$supplier]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'delaware_db.notification' , @level0type=N'SCHEMA',@level0name=N'delaware_db', @level1type=N'TABLE',@level1name=N'notification'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'delaware_db.`order`' , @level0type=N'SCHEMA',@level0name=N'delaware_db', @level1type=N'TABLE',@level1name=N'order'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'delaware_db.orderitem' , @level0type=N'SCHEMA',@level0name=N'delaware_db', @level1type=N'TABLE',@level1name=N'orderitem'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'delaware_db.package' , @level0type=N'SCHEMA',@level0name=N'delaware_db', @level1type=N'TABLE',@level1name=N'package'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'delaware_db.product' , @level0type=N'SCHEMA',@level0name=N'delaware_db', @level1type=N'TABLE',@level1name=N'product'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'delaware_db.supplier' , @level0type=N'SCHEMA',@level0name=N'delaware_db', @level1type=N'TABLE',@level1name=N'supplier'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'delaware_db.`user`' , @level0type=N'SCHEMA',@level0name=N'delaware_db', @level1type=N'TABLE',@level1name=N'user'
GO
USE [master]
GO
ALTER DATABASE [DelawareDB] SET  READ_WRITE 
GO
