USE [master]
GO
/****** Object:  Database [dbproduct]    Script Date: 27/2/2024 20:39:43 ******/
CREATE DATABASE [dbproduct]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'dbproduct', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\dbproduct.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'dbproduct_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\dbproduct_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [dbproduct] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [dbproduct].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [dbproduct] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [dbproduct] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [dbproduct] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [dbproduct] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [dbproduct] SET ARITHABORT OFF 
GO
ALTER DATABASE [dbproduct] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [dbproduct] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [dbproduct] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [dbproduct] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [dbproduct] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [dbproduct] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [dbproduct] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [dbproduct] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [dbproduct] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [dbproduct] SET  DISABLE_BROKER 
GO
ALTER DATABASE [dbproduct] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [dbproduct] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [dbproduct] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [dbproduct] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [dbproduct] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [dbproduct] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [dbproduct] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [dbproduct] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [dbproduct] SET  MULTI_USER 
GO
ALTER DATABASE [dbproduct] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [dbproduct] SET DB_CHAINING OFF 
GO
ALTER DATABASE [dbproduct] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [dbproduct] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [dbproduct] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [dbproduct] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [dbproduct] SET QUERY_STORE = OFF
GO
USE [dbproduct]
GO
/****** Object:  Table [dbo].[category]    Script Date: 27/2/2024 20:39:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[category](
	[categoryId] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_category] PRIMARY KEY CLUSTERED 
(
	[categoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[invoice_detail]    Script Date: 27/2/2024 20:39:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[invoice_detail](
	[invoiceDetailId] [int] IDENTITY(1,1) NOT NULL,
	[productId] [int] NOT NULL,
	[count] [float] NOT NULL,
	[price] [float] NOT NULL,
	[invoiceHeadId] [int] NOT NULL,
 CONSTRAINT [PK_invoice_detail] PRIMARY KEY CLUSTERED 
(
	[invoiceDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[invoice_head]    Script Date: 27/2/2024 20:39:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[invoice_head](
	[invoiceHeadId] [int] IDENTITY(1,1) NOT NULL,
	[total] [float] NOT NULL,
	[dateTime] [datetime] NOT NULL,
	[userId] [int] NULL,
 CONSTRAINT [PK_invoice_head] PRIMARY KEY CLUSTERED 
(
	[invoiceHeadId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[product]    Script Date: 27/2/2024 20:39:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[product](
	[productId] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[price] [float] NOT NULL,
	[count] [int] NOT NULL,
	[categoryId] [int] NOT NULL,
 CONSTRAINT [PK_product] PRIMARY KEY CLUSTERED 
(
	[productId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[t_user]    Script Date: 27/2/2024 20:39:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[t_user](
	[userId] [int] IDENTITY(1,1) NOT NULL,
	[userName] [varchar](25) NOT NULL,
	[password] [varchar](200) NOT NULL,
	[status] [varchar](10) NOT NULL,
 CONSTRAINT [PK_t_user] PRIMARY KEY CLUSTERED 
(
	[userId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[category] ON 

INSERT [dbo].[category] ([categoryId], [name]) VALUES (1, N'Dulces')
INSERT [dbo].[category] ([categoryId], [name]) VALUES (2, N'Bebidas')
INSERT [dbo].[category] ([categoryId], [name]) VALUES (3, N'Snack')
SET IDENTITY_INSERT [dbo].[category] OFF
GO
SET IDENTITY_INSERT [dbo].[invoice_detail] ON 

INSERT [dbo].[invoice_detail] ([invoiceDetailId], [productId], [count], [price], [invoiceHeadId]) VALUES (27, 5, 5, 0.8, 16)
INSERT [dbo].[invoice_detail] ([invoiceDetailId], [productId], [count], [price], [invoiceHeadId]) VALUES (28, 6, 3, 0.8, 16)
INSERT [dbo].[invoice_detail] ([invoiceDetailId], [productId], [count], [price], [invoiceHeadId]) VALUES (29, 7, 1, 0.2, 17)
INSERT [dbo].[invoice_detail] ([invoiceDetailId], [productId], [count], [price], [invoiceHeadId]) VALUES (30, 8, 3, 0.4, 17)
INSERT [dbo].[invoice_detail] ([invoiceDetailId], [productId], [count], [price], [invoiceHeadId]) VALUES (31, 7, 1, 0.2, 18)
INSERT [dbo].[invoice_detail] ([invoiceDetailId], [productId], [count], [price], [invoiceHeadId]) VALUES (32, 8, 1, 0.4, 18)
INSERT [dbo].[invoice_detail] ([invoiceDetailId], [productId], [count], [price], [invoiceHeadId]) VALUES (33, 7, 1, 0.2, 19)
INSERT [dbo].[invoice_detail] ([invoiceDetailId], [productId], [count], [price], [invoiceHeadId]) VALUES (34, 8, 1, 0.4, 19)
INSERT [dbo].[invoice_detail] ([invoiceDetailId], [productId], [count], [price], [invoiceHeadId]) VALUES (35, 7, 1, 0.2, 20)
INSERT [dbo].[invoice_detail] ([invoiceDetailId], [productId], [count], [price], [invoiceHeadId]) VALUES (36, 8, 1, 0.4, 20)
SET IDENTITY_INSERT [dbo].[invoice_detail] OFF
GO
SET IDENTITY_INSERT [dbo].[invoice_head] ON 

INSERT [dbo].[invoice_head] ([invoiceHeadId], [total], [dateTime], [userId]) VALUES (16, 6.4, CAST(N'2024-02-24T20:18:44.630' AS DateTime), NULL)
INSERT [dbo].[invoice_head] ([invoiceHeadId], [total], [dateTime], [userId]) VALUES (17, 1.4000000000000001, CAST(N'2024-02-24T20:41:42.000' AS DateTime), 1)
INSERT [dbo].[invoice_head] ([invoiceHeadId], [total], [dateTime], [userId]) VALUES (18, 0.60000000000000009, CAST(N'2024-02-26T20:30:09.290' AS DateTime), 3)
INSERT [dbo].[invoice_head] ([invoiceHeadId], [total], [dateTime], [userId]) VALUES (19, 0.60000000000000009, CAST(N'2024-02-27T19:34:50.073' AS DateTime), 3)
INSERT [dbo].[invoice_head] ([invoiceHeadId], [total], [dateTime], [userId]) VALUES (20, 0.60000000000000009, CAST(N'2024-02-27T19:35:30.883' AS DateTime), 3)
SET IDENTITY_INSERT [dbo].[invoice_head] OFF
GO
SET IDENTITY_INSERT [dbo].[product] ON 

INSERT [dbo].[product] ([productId], [name], [price], [count], [categoryId]) VALUES (1, N'Caramelo', 0.1, 0, 1)
INSERT [dbo].[product] ([productId], [name], [price], [count], [categoryId]) VALUES (4, N'Chupete', 0.2, 1, 1)
INSERT [dbo].[product] ([productId], [name], [price], [count], [categoryId]) VALUES (5, N'Sprite', 0.8, 0, 2)
INSERT [dbo].[product] ([productId], [name], [price], [count], [categoryId]) VALUES (6, N'Fanta', 0.8, 6, 2)
INSERT [dbo].[product] ([productId], [name], [price], [count], [categoryId]) VALUES (7, N'Producto53', 0.2, 7, 3)
INSERT [dbo].[product] ([productId], [name], [price], [count], [categoryId]) VALUES (8, N'producto prueba', 0.4, 15, 3)
SET IDENTITY_INSERT [dbo].[product] OFF
GO
SET IDENTITY_INSERT [dbo].[t_user] ON 

INSERT [dbo].[t_user] ([userId], [userName], [password], [status]) VALUES (1, N'jose', N'Pt1pDTW6mCmEcmADq8VTJs8PNru2OedFci6zAGTsjVT2bD/GppRSLOzDJ5lwXIiHeQlWGgeDZtyDxRA1L0CxUw==', N'A')
INSERT [dbo].[t_user] ([userId], [userName], [password], [status]) VALUES (3, N'jose2', N'i2mbHvoYh0lLz/FbEEIQdAwpONtfuYwwActsaqLX6Nj/wwGk8bX1GThCKdk9Tl0udR615BTpcma2R4uuhq3+Xw==', N'A')
INSERT [dbo].[t_user] ([userId], [userName], [password], [status]) VALUES (4, N'jose4', N'UoRR+udz5pa8WSZa9Ucknf5cWu/74WwKOw+tqepU33id0NhkI3ZNQB6sM1J0BN1mi0yT1W2qnETIKEMjQcLjvA==', N'A')
SET IDENTITY_INSERT [dbo].[t_user] OFF
GO
ALTER TABLE [dbo].[invoice_detail]  WITH CHECK ADD  CONSTRAINT [FK_invoice_detail_invoice_head] FOREIGN KEY([invoiceHeadId])
REFERENCES [dbo].[invoice_head] ([invoiceHeadId])
GO
ALTER TABLE [dbo].[invoice_detail] CHECK CONSTRAINT [FK_invoice_detail_invoice_head]
GO
ALTER TABLE [dbo].[invoice_head]  WITH CHECK ADD  CONSTRAINT [FK_invoice_head_t_user] FOREIGN KEY([userId])
REFERENCES [dbo].[t_user] ([userId])
GO
ALTER TABLE [dbo].[invoice_head] CHECK CONSTRAINT [FK_invoice_head_t_user]
GO
ALTER TABLE [dbo].[product]  WITH CHECK ADD  CONSTRAINT [FK_product_category] FOREIGN KEY([categoryId])
REFERENCES [dbo].[category] ([categoryId])
GO
ALTER TABLE [dbo].[product] CHECK CONSTRAINT [FK_product_category]
GO
USE [master]
GO
ALTER DATABASE [dbproduct] SET  READ_WRITE 
GO
