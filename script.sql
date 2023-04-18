USE [master]
GO
/****** Object:  Database [OnlineShop]    Script Date: 2023-04-18 8:49:32 PM ******/
CREATE DATABASE [OnlineShop]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'OnlineShop', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.ASHKAN_MAKTAB\MSSQL\DATA\OnlineShop.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'OnlineShop_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.ASHKAN_MAKTAB\MSSQL\DATA\OnlineShop_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [OnlineShop] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [OnlineShop].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [OnlineShop] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [OnlineShop] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [OnlineShop] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [OnlineShop] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [OnlineShop] SET ARITHABORT OFF 
GO
ALTER DATABASE [OnlineShop] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [OnlineShop] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [OnlineShop] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [OnlineShop] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [OnlineShop] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [OnlineShop] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [OnlineShop] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [OnlineShop] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [OnlineShop] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [OnlineShop] SET  DISABLE_BROKER 
GO
ALTER DATABASE [OnlineShop] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [OnlineShop] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [OnlineShop] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [OnlineShop] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [OnlineShop] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [OnlineShop] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [OnlineShop] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [OnlineShop] SET RECOVERY FULL 
GO
ALTER DATABASE [OnlineShop] SET  MULTI_USER 
GO
ALTER DATABASE [OnlineShop] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [OnlineShop] SET DB_CHAINING OFF 
GO
ALTER DATABASE [OnlineShop] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [OnlineShop] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [OnlineShop] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [OnlineShop] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'OnlineShop', N'ON'
GO
ALTER DATABASE [OnlineShop] SET QUERY_STORE = ON
GO
ALTER DATABASE [OnlineShop] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [OnlineShop]
GO
/****** Object:  Table [dbo].[Baskets]    Script Date: 2023-04-18 8:49:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Baskets](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Customer_Id] [int] NOT NULL,
	[Product_Id] [int] NOT NULL,
	[Count] [int] NOT NULL,
 CONSTRAINT [PK_Baskets] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 2023-04-18 8:49:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[First_Name] [nvarchar](50) NOT NULL,
	[Last_Name] [nvarchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Factors]    Script Date: 2023-04-18 8:49:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Factors](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Create_Date] [date] NOT NULL,
	[Total_Price] [int] NOT NULL,
	[Discount] [int] NOT NULL,
	[Customer_Id] [int] NOT NULL,
 CONSTRAINT [PK_Factors] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Inventories]    Script Date: 2023-04-18 8:49:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Inventories](
	[Inventory_Id] [int] IDENTITY(1,1) NOT NULL,
	[Product_Id] [int] NOT NULL,
	[Qty] [int] NOT NULL,
	[Enter_Time] [date] NOT NULL,
	[Exit_Time] [date] NULL,
 CONSTRAINT [PK_Inventories] PRIMARY KEY CLUSTERED 
(
	[Inventory_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product_Factor]    Script Date: 2023-04-18 8:49:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product_Factor](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Factor_Id] [int] NOT NULL,
	[Product_Id] [int] NOT NULL,
	[Qty] [int] NOT NULL,
 CONSTRAINT [PK_Product_Factor] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 2023-04-18 8:49:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Product_Name] [nvarchar](50) NOT NULL,
	[Unit_Price] [int] NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Index [IX_Inventories__Unique]    Script Date: 2023-04-18 8:49:32 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Inventories__Unique] ON [dbo].[Inventories]
(
	[Product_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Baskets]  WITH CHECK ADD  CONSTRAINT [FK_Baskets_Baskets___Customer] FOREIGN KEY([Customer_Id])
REFERENCES [dbo].[Customers] ([Id])
GO
ALTER TABLE [dbo].[Baskets] CHECK CONSTRAINT [FK_Baskets_Baskets___Customer]
GO
ALTER TABLE [dbo].[Baskets]  WITH CHECK ADD  CONSTRAINT [FK_Baskets_Baskets___Product] FOREIGN KEY([Product_Id])
REFERENCES [dbo].[Products] ([Id])
GO
ALTER TABLE [dbo].[Baskets] CHECK CONSTRAINT [FK_Baskets_Baskets___Product]
GO
ALTER TABLE [dbo].[Factors]  WITH CHECK ADD  CONSTRAINT [FK_Factors_Factors___Customer] FOREIGN KEY([Customer_Id])
REFERENCES [dbo].[Customers] ([Id])
GO
ALTER TABLE [dbo].[Factors] CHECK CONSTRAINT [FK_Factors_Factors___Customer]
GO
ALTER TABLE [dbo].[Inventories]  WITH CHECK ADD  CONSTRAINT [FK_Inventories_Products] FOREIGN KEY([Product_Id])
REFERENCES [dbo].[Products] ([Id])
GO
ALTER TABLE [dbo].[Inventories] CHECK CONSTRAINT [FK_Inventories_Products]
GO
ALTER TABLE [dbo].[Product_Factor]  WITH CHECK ADD  CONSTRAINT [FK_Product_Factor___Factor] FOREIGN KEY([Factor_Id])
REFERENCES [dbo].[Factors] ([Id])
GO
ALTER TABLE [dbo].[Product_Factor] CHECK CONSTRAINT [FK_Product_Factor___Factor]
GO
ALTER TABLE [dbo].[Product_Factor]  WITH CHECK ADD  CONSTRAINT [FK_Product_Factor___Product] FOREIGN KEY([Product_Id])
REFERENCES [dbo].[Products] ([Id])
GO
ALTER TABLE [dbo].[Product_Factor] CHECK CONSTRAINT [FK_Product_Factor___Product]
GO
USE [master]
GO
ALTER DATABASE [OnlineShop] SET  READ_WRITE 
GO
