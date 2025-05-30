USE [master]
GO
/****** Object:  Database [CFBikeStoreDB]    Script Date: 11/4/2025 9:56:59 PM ******/
CREATE DATABASE [CFBikeStoreDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CFBikeStoreDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\CFBikeStoreDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'CFBikeStoreDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\CFBikeStoreDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [CFBikeStoreDB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CFBikeStoreDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CFBikeStoreDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CFBikeStoreDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CFBikeStoreDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CFBikeStoreDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CFBikeStoreDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [CFBikeStoreDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [CFBikeStoreDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CFBikeStoreDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CFBikeStoreDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CFBikeStoreDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CFBikeStoreDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CFBikeStoreDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CFBikeStoreDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CFBikeStoreDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CFBikeStoreDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [CFBikeStoreDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CFBikeStoreDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CFBikeStoreDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CFBikeStoreDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CFBikeStoreDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CFBikeStoreDB] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [CFBikeStoreDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CFBikeStoreDB] SET RECOVERY FULL 
GO
ALTER DATABASE [CFBikeStoreDB] SET  MULTI_USER 
GO
ALTER DATABASE [CFBikeStoreDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CFBikeStoreDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CFBikeStoreDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CFBikeStoreDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [CFBikeStoreDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [CFBikeStoreDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'CFBikeStoreDB', N'ON'
GO
ALTER DATABASE [CFBikeStoreDB] SET QUERY_STORE = ON
GO
ALTER DATABASE [CFBikeStoreDB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [CFBikeStoreDB]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 11/4/2025 9:57:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 11/4/2025 9:57:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 11/4/2025 9:57:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 11/4/2025 9:57:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 11/4/2025 9:57:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 11/4/2025 9:57:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 11/4/2025 9:57:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 11/4/2025 9:57:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Brands]    Script Date: 11/4/2025 9:57:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Brands](
	[BrandId] [int] IDENTITY(1,1) NOT NULL,
	[BrandName] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Brands] PRIMARY KEY CLUSTERED 
(
	[BrandId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 11/4/2025 9:57:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 11/4/2025 9:57:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[CustomerId] [int] IDENTITY(1,1) NOT NULL,
	[CustumerName] [nvarchar](max) NOT NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[CustomerEmail] [nvarchar](max) NULL,
	[City] [nvarchar](max) NULL,
	[Street] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 11/4/2025 9:57:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[EmployeeId] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeName] [nvarchar](max) NOT NULL,
	[EmployeeEmail] [nvarchar](max) NULL,
	[EmployeePhone] [nvarchar](max) NULL,
	[EmployeeSalary] [decimal](18, 2) NOT NULL,
	[StoreId] [int] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderItems]    Script Date: 11/4/2025 9:57:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderItems](
	[OrderId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[Listprice] [decimal](10, 2) NOT NULL,
	[Discount] [decimal](10, 2) NULL,
	[TotalPrice]  AS ([Quantity]*[Listprice]-[Discount]),
	[CustomerName] [nvarchar](max) NOT NULL,
	[ProductName] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_OrderItems] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC,
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 11/4/2025 9:57:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[StoreId] [int] NOT NULL,
	[OrderDate] [datetime2](7) NOT NULL,
	[ShippedDate] [datetime2](7) NOT NULL,
	[IsExist] [bit] NOT NULL,
	[CustomerName] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 11/4/2025 9:57:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](max) NOT NULL,
	[ModelYear] [nvarchar](max) NOT NULL,
	[ListPrice] [decimal](18, 2) NOT NULL,
	[BrandId] [int] NULL,
	[CategoryId] [int] NULL,
	[IsExisit] [bit] NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductStores]    Script Date: 11/4/2025 9:57:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductStores](
	[ProductId] [int] NOT NULL,
	[StoreId] [int] NOT NULL,
	[Quanttity] [int] NOT NULL,
 CONSTRAINT [PK_ProductStores] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC,
	[StoreId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Stores]    Script Date: 11/4/2025 9:57:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stores](
	[StoreId] [int] IDENTITY(1,1) NOT NULL,
	[StoreName] [nvarchar](max) NOT NULL,
	[city] [nvarchar](max) NULL,
	[street] [nvarchar](max) NULL,
	[Phone] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[IsExist] [bit] NOT NULL,
 CONSTRAINT [PK_Stores] PRIMARY KEY CLUSTERED 
(
	[StoreId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250411151427_Create-all-tabels-with-relations', N'7.0.0')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250411152738_alter-store-add-IsExist-column', N'7.0.0')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250411153649_alter-listprice-column-in-product-table', N'7.0.0')
GO
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'fcd430af-a9c8-4271-b0ab-3cd2cef9aef9', N'Mohamed', N'MOHAMED', N'mohamed@gmail.com', N'MOHAMED@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEN4N/xqQmNrg7aIpXxMPauKLyQ/sv0TRyaC5SOHiXF90WXxN4YvRnkSbre0FdWkAEg==', N'YP3XS475OIOKLC45NX3XYZM6YA5O2P6B', N'3da1ca4d-cfba-4567-97ef-56795962999e', NULL, 0, 0, NULL, 1, 0)
GO
SET IDENTITY_INSERT [dbo].[Brands] ON 
GO
INSERT [dbo].[Brands] ( [BrandName]) VALUES ( N'Electra')
GO
INSERT [dbo].[Brands] ( [BrandName]) VALUES ( N'Haro')
GO
INSERT [dbo].[Brands] ( [BrandName]) VALUES ( N'Heller')
GO
INSERT [dbo].[Brands] ( [BrandName]) VALUES ( N'Pure Cycles')
GO
INSERT [dbo].[Brands] ( [BrandName]) VALUES ( N'Ritchey')
GO
INSERT [dbo].[Brands] ( [BrandName]) VALUES ( N'Strider')
GO
INSERT [dbo].[Brands] ( [BrandName]) VALUES ( N'Sun Bicycles')
GO
INSERT [dbo].[Brands] ( [BrandName]) VALUES ( N'Surly')
GO
INSERT [dbo].[Brands] ( [BrandName]) VALUES ( N'Trek')
GO
SET IDENTITY_INSERT [dbo].[Brands] OFF
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 
GO
INSERT [dbo].[Categories] ( [CategoryName]) VALUES ( N'Children Bicycles')
GO
INSERT [dbo].[Categories] ( [CategoryName]) VALUES ( N'Comfort Bicycles')
GO
INSERT [dbo].[Categories] ( [CategoryName]) VALUES ( N'Cruisers Bicycles')
GO
INSERT [dbo].[Categories] ( [CategoryName]) VALUES ( N'Cyclocross Bicycles')
GO
INSERT [dbo].[Categories] ( [CategoryName]) VALUES ( N'Electric Bikes')
GO
INSERT [dbo].[Categories] ( [CategoryName]) VALUES ( N'Mountain Bikes')
GO
INSERT [dbo].[Categories] ( [CategoryName]) VALUES ( N'Road Bikes')
GO
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[Customers] ON 
GO
INSERT [dbo].[Customers] ( [CustumerName], [PhoneNumber], [CustomerEmail], [City], [Street], [IsActive]) VALUES ( N'Mohamed Mahmoud', N'01211254551', N'mohamedmahmoud@gmail.com', N'Minya', N'Court Street', 1)
GO
INSERT [dbo].[Customers] ( [CustumerName], [PhoneNumber], [CustomerEmail], [City], [Street], [IsActive]) VALUES ( N'Ahmed Mohamed', N'01015826551', N'ahmedmohamed@gmail.com', N'Cairo', N'Train station street', 1)
GO
INSERT [dbo].[Customers] ( [CustumerName], [PhoneNumber], [CustomerEmail], [City], [Street], [IsActive]) VALUES ( N'Omar Mohamed', N'01248964551', N'omarmohamed@gmail.com', N'Cairo', N'Train station street', 1)
GO
INSERT [dbo].[Customers] ( [CustumerName], [PhoneNumber], [CustomerEmail], [City], [Street], [IsActive]) VALUES ( N'Salma Ahmed', N'01248695251', N'salmaahmed@gmail.com', N'Assiut', N'omar ben khatab Street', 1)
GO
INSERT [dbo].[Customers] ( [CustumerName], [PhoneNumber], [CustomerEmail], [City], [Street], [IsActive]) VALUES ( N'Marwa Mohamed', N'01111259712', N'marwamohamed@gmail.com', N'Minya', N'Abu Bakr Street', 1)
GO
INSERT [dbo].[Customers] ( [CustumerName], [PhoneNumber], [CustomerEmail], [City], [Street], [IsActive]) VALUES ( N'Hosam Ali', N'01554894231', N'hosamali@gmail.com', N'Alexandria', N'Governorate Street', 1)
GO
INSERT [dbo].[Customers] ( [CustumerName], [PhoneNumber], [CustomerEmail], [City], [Street], [IsActive]) VALUES ( N'Kareem Mahmoud', N'01054321231', N'kareemmahmoud@gmail.com', N'Assiut', N'Taha Huseen Street', 1)
GO
INSERT [dbo].[Customers] ( [CustumerName], [PhoneNumber], [CustomerEmail], [City], [Street], [IsActive]) VALUES ( N'Ali Ahmed', N'01155474298', N'aliahmed@gmail.com', N'Minya', N'Republic Street', 1)
GO
INSERT [dbo].[Customers] ( [CustumerName], [PhoneNumber], [CustomerEmail], [City], [Street], [IsActive]) VALUES ( N'Khaled Mohy', N'01214897452', N'khaledmohy@gmail.com', N'Cairo', N'Corniche Street', 1)
GO
INSERT [dbo].[Customers] ( [CustumerName], [PhoneNumber], [CustomerEmail], [City], [Street], [IsActive]) VALUES ( N'Eman Mohamed', N'01054865433', N'emanmohamed@gmail.com', N'Qena', N'16-elsalam Street', 1)
GO
INSERT [dbo].[Customers] ( [CustumerName], [PhoneNumber], [CustomerEmail], [City], [Street], [IsActive]) VALUES ( N'Shimaa Ahmed', N'01252134279', N'shimaahmed@gmail.com', N'Tanta', N'Adnan Al-Maliki Street', 1)
GO
INSERT [dbo].[Customers] ( [CustumerName], [PhoneNumber], [CustomerEmail], [City], [Street], [IsActive]) VALUES ( N'Adel Mohamed', N'01528714252', N'adelmohamed@gmail.com', N'Sohag', N'3- Omar bin Abdulaziz Street', 1)
GO
INSERT [dbo].[Customers] ( [CustumerName], [PhoneNumber], [CustomerEmail], [City], [Street], [IsActive]) VALUES ( N'Mostafa Ali', N'01084132269', N'mostafaali@gmail.com', N'Tanta', N'Ramses Street', 1)
GO
INSERT [dbo].[Customers] ( [CustumerName], [PhoneNumber], [CustomerEmail], [City], [Street], [IsActive]) VALUES ( N'Mohamed	Mostafa', N'01257214264', N'mohamedmostafa@gmail.com', N'Luxor', N'Ezbet El Nakhl Street', 1)
GO
INSERT [dbo].[Customers] ( [CustumerName], [PhoneNumber], [CustomerEmail], [City], [Street], [IsActive]) VALUES ( N'Ali Ahmed', N'01564214123', N'aliahmed2@gmail.com', N'Sohag', N'Al-Numais Street', 1)
GO
INSERT [dbo].[Customers] ( [CustumerName], [PhoneNumber], [CustomerEmail], [City], [Street], [IsActive]) VALUES ( N'Esraa Ahmed', N'01155344289', N'esraaahmed@gmail.com', N'Qena', N'Ahmed Oraby Street', 1)
GO
INSERT [dbo].[Customers] ( [CustumerName], [PhoneNumber], [CustomerEmail], [City], [Street], [IsActive]) VALUES ( N'Amira Kamel', N'01228714492', N'amirakamel@gmail.com', N'Luxor', N'Yousry Ragheb Street', 1)
GO
INSERT [dbo].[Customers] ( [CustumerName], [PhoneNumber], [CustomerEmail], [City], [Street], [IsActive]) VALUES ( N'waael Kareem', N'01128893974', N'waaelkareem@gmail.com', N'Alexandria', N'Court Street', 1)
GO
SET IDENTITY_INSERT [dbo].[Customers] OFF
GO
SET IDENTITY_INSERT [dbo].[Employees] ON 
GO
INSERT [dbo].[Employees] ( [EmployeeName], [EmployeeEmail], [EmployeePhone], [EmployeeSalary], [StoreId], [IsActive]) VALUES ( N'Mohamed Ahmed', N'mohamedahmed@gmail.com', N'01198726234', CAST(3700.00 AS Decimal(18, 2)), 2, 1)
GO
INSERT [dbo].[Employees] ( [EmployeeName], [EmployeeEmail], [EmployeePhone], [EmployeeSalary], [StoreId], [IsActive]) VALUES ( N'Ahmed Kamel', N'ahmedkamel@gmail.com', N'01184293734', CAST(5000.00 AS Decimal(18, 2)), 4, 1)
GO
INSERT [dbo].[Employees] ( [EmployeeName], [EmployeeEmail], [EmployeePhone], [EmployeeSalary], [StoreId], [IsActive]) VALUES ( N'Tony Sleem', N'tonysleem@gmail.com', N'01235729814', CAST(7800.00 AS Decimal(18, 2)), 2, 1)
GO
INSERT [dbo].[Employees] ( [EmployeeName], [EmployeeEmail], [EmployeePhone], [EmployeeSalary], [StoreId], [IsActive]) VALUES ( N'Shady Mohamed', N'shadymohamed@gmail.com', N'01018787834', CAST(10000.00 AS Decimal(18, 2)), 3, 1)
GO
INSERT [dbo].[Employees] ( [EmployeeName], [EmployeeEmail], [EmployeePhone], [EmployeeSalary], [StoreId], [IsActive]) VALUES ( N'Osama Ahmed', N'osamaahmed@gmail.com', N'01568722132', CAST(6430.00 AS Decimal(18, 2)), 3, 1)
GO
INSERT [dbo].[Employees] ( [EmployeeName], [EmployeeEmail], [EmployeePhone], [EmployeeSalary], [StoreId], [IsActive]) VALUES ( N'Gamal Yousef', N'gamalyousef@gmail.com', N'01249723454', CAST(9500.00 AS Decimal(18, 2)), 3, 1)
GO
INSERT [dbo].[Employees] ( [EmployeeName], [EmployeeEmail], [EmployeePhone], [EmployeeSalary], [StoreId], [IsActive]) VALUES ( N'Yaseer Sami', N'yaseersami@gmail.com', N'01248726987', CAST(6400.00 AS Decimal(18, 2)), 2, 1)
GO
INSERT [dbo].[Employees] ( [EmployeeName], [EmployeeEmail], [EmployeePhone], [EmployeeSalary], [StoreId], [IsActive]) VALUES ( N'Samer Ahmed', N'samerahmed@gmail.com', N'01118726644', CAST(6000.00 AS Decimal(18, 2)), 4, 1)
GO
INSERT [dbo].[Employees] ( [EmployeeName], [EmployeeEmail], [EmployeePhone], [EmployeeSalary], [StoreId], [IsActive]) VALUES ( N'Akram Hosny', N'akramhosny@gmail.com', N'01090306062', CAST(7200.00 AS Decimal(18, 2)), 4, 1)
GO
INSERT [dbo].[Employees] ( [EmployeeName], [EmployeeEmail], [EmployeePhone], [EmployeeSalary], [StoreId], [IsActive]) VALUES ( N'Ahmed Safwat', N'ahmedsafwat@gmail.com', N'01008729734', CAST(2500.00 AS Decimal(18, 2)), 2, 1)
GO
INSERT [dbo].[Employees] ([EmployeeName], [EmployeeEmail], [EmployeePhone], [EmployeeSalary], [StoreId], [IsActive]) VALUES ( N'Mohamed Omar', N'mohamedomar@gmail.com', N'01228336239', CAST(4000.00 AS Decimal(18, 2)), 4, 1)
GO
INSERT [dbo].[Employees] ( [EmployeeName], [EmployeeEmail], [EmployeePhone], [EmployeeSalary], [StoreId], [IsActive]) VALUES ( N'Mohamed Ashor', N'mohamedashor@gmail.com', N'01296715239', CAST(4500.00 AS Decimal(18, 2)), 3, 1)
GO
INSERT [dbo].[Employees] ( [EmployeeName], [EmployeeEmail], [EmployeePhone], [EmployeeSalary], [StoreId], [IsActive]) VALUES ( N'Ahmed Ali', N'ahmedali@gmail.com', N'01078767434', CAST(4600.00 AS Decimal(18, 2)), 4, 1)
GO
INSERT [dbo].[Employees] ( [EmployeeName], [EmployeeEmail], [EmployeePhone], [EmployeeSalary], [StoreId], [IsActive]) VALUES ( N'Mazen Safwat', N'mazensafwat@gmail.com', N'01192341234', CAST(3500.00 AS Decimal(18, 2)), 1, 1)
GO
INSERT [dbo].[Employees] ( [EmployeeName], [EmployeeEmail], [EmployeePhone], [EmployeeSalary], [StoreId], [IsActive]) VALUES ( N'Omar Mohamed', N'omarmohamed@gmail.com', N'01549726578', CAST(6000.00 AS Decimal(18, 2)), 3, 1)
GO
INSERT [dbo].[Employees] ( [EmployeeName], [EmployeeEmail], [EmployeePhone], [EmployeeSalary], [StoreId], [IsActive]) VALUES ( N'Mohamed Mahmoud', N'mohamedmahmoud@gmail.com', N'01118618557', CAST(10000.00 AS Decimal(18, 2)), 1, 1)
GO
INSERT [dbo].[Employees] ( [EmployeeName], [EmployeeEmail], [EmployeePhone], [EmployeeSalary], [StoreId], [IsActive]) VALUES ( N'Kareem Mohamed', N'kareemmohamed@gmail.com', N'01234567897', CAST(3000.00 AS Decimal(18, 2)), 1, 1)
GO
SET IDENTITY_INSERT [dbo].[Employees] OFF
GO
INSERT [dbo].[OrderItems] ([OrderId], [ProductId], [Quantity], [Listprice], [Discount], [ProductName]) VALUES (2, 1, 1, CAST(1900.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), N'Mohamed	 Mahmoud', N'Trek-650')
GO
INSERT [dbo].[OrderItems] ([OrderId], [ProductId], [Quantity], [Listprice], [Discount], [ProductName]) VALUES (5, 3, 2, CAST(800.00 AS Decimal(10, 2)), CAST(15.00 AS Decimal(10, 2)), N'Marwa Mohamed', N'Surly Ice Cream Truck Frameset')
GO
INSERT [dbo].[OrderItems] ([OrderId], [ProductId], [Quantity], [Listprice], [Discount], [ProductName]) VALUES (7, 2, 1, CAST(3000.00 AS Decimal(10, 2)), CAST(50.00 AS Decimal(10, 2)), N'Salma Ahmed', N'Ritchey Timberwolf Frameset ')
GO
INSERT [dbo].[OrderItems] ([OrderId], [ProductId], [Quantity], [Listprice], [Discount], [ProductName]) VALUES (8, 5, 3, CAST(2000.00 AS Decimal(10, 2)), CAST(20.00 AS Decimal(10, 2)), N'Salma Ahmed', N'Pure Cycles William 3-Speed ')
GO
INSERT [dbo].[OrderItems] ([OrderId], [ProductId], [Quantity], [Listprice], [Discount], [ProductName]) VALUES (9, 15, 2, CAST(1900.00 AS Decimal(10, 2)), CAST(10.00 AS Decimal(10, 2)), N'Ali Ahmed', N'Sun Bicycles Drifter 7 - Women')
GO
INSERT [dbo].[OrderItems] ([OrderId], [ProductId], [Quantity], [Listprice], [Discount], [ProductName]) VALUES (10, 8, 1, CAST(1800.00 AS Decimal(10, 2)), CAST(15.00 AS Decimal(10, 2)), N'Khaled Mohy', N'Haro Flightline One ST ')
GO
INSERT [dbo].[OrderItems] ([OrderId], [ProductId], [Quantity], [Listprice], [Discount], [ProductName]) VALUES (11, 15, 1, CAST(3500.00 AS Decimal(10, 2)), CAST(30.00 AS Decimal(10, 2)), N'Mohamed	 Mahmoud', N'Sun Bicycles Drifter 7 - Women')
GO
INSERT [dbo].[OrderItems] ([OrderId], [ProductId], [Quantity], [Listprice], [Discount], [ProductName]) VALUES (12, 14, 3, CAST(5000.00 AS Decimal(10, 2)), CAST(100.00 AS Decimal(10, 2)), N'Hosam Ali', N'Electra Townie Go! 8i Ladies')
GO
INSERT [dbo].[OrderItems] ([OrderId], [ProductId], [Quantity], [Listprice], [Discount], [ProductName]) VALUES (13, 3, 1, CAST(1000.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), N'Adel Mohamed', N'Surly Ice Cream Truck Frameset')
GO
INSERT [dbo].[OrderItems] ([OrderId], [ProductId], [Quantity], [Listprice], [Discount], [ProductName]) VALUES (18, 10, 1, CAST(980.00 AS Decimal(10, 2)), CAST(5.00 AS Decimal(10, 2)), N'Salma Ahmed', N'Trek Marlin 5 ')
GO
INSERT [dbo].[OrderItems] ([OrderId], [ProductId], [Quantity], [Listprice], [Discount], [ProductName]) VALUES (19, 9, 1, CAST(2000.00 AS Decimal(10, 2)), CAST(10.00 AS Decimal(10, 2)), N'Mohamed	 Mahmoud', N'Sun Bicycles Brickell Tandem CB ')
GO
INSERT [dbo].[OrderItems] ([OrderId], [ProductId], [Quantity], [Listprice], [Discount], [ProductName]) VALUES (21, 7, 1, CAST(2500.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), N'Kareem Mahmoud', N'Trek Farley Alloy Frameset')
GO
INSERT [dbo].[OrderItems] ([OrderId], [ProductId], [Quantity], [Listprice], [Discount], [ProductName]) VALUES (22, 5, 2, CAST(1900.00 AS Decimal(10, 2)), CAST(30.00 AS Decimal(10, 2)), N'Mohamed	 Mahmoud', N'Pure Cycles William 3-Speed ')
GO
INSERT [dbo].[OrderItems] ([OrderId], [ProductId], [Quantity], [Listprice], [Discount], [ProductName]) VALUES (23, 4, 1, CAST(3000.00 AS Decimal(10, 2)), CAST(10.00 AS Decimal(10, 2)), N'Mohamed	 Mahmoud', N'Surly Straggler ')
GO
INSERT [dbo].[OrderItems] ([OrderId], [ProductId], [Quantity], [Listprice], [Discount], [ProductName]) VALUES (24, 3, 1, CAST(1000.00 AS Decimal(10, 2)), CAST(10.00 AS Decimal(10, 2)), N'Mohamed	 Mahmoud', N'Surly Ice Cream Truck Frameset')
GO
INSERT [dbo].[OrderItems] ([OrderId], [ProductId], [Quantity], [Listprice], [Discount], [ProductName]) VALUES (25, 19, 2, CAST(690.00 AS Decimal(10, 2)), CAST(10.00 AS Decimal(10, 2)), N'Marwa Mohamed', N'Trek Kids Dual Sport')
GO
INSERT [dbo].[OrderItems] ([OrderId], [ProductId], [Quantity], [Listprice], [Discount], [ProductName]) VALUES (26, 20, 1, CAST(3000.00 AS Decimal(10, 2)), CAST(10.00 AS Decimal(10, 2)), N'Marwa Mohamed', N'Sun Bicycles Cruz 3 ')
GO
INSERT [dbo].[OrderItems] ([OrderId], [ProductId], [Quantity], [Listprice], [Discount], [ProductName]) VALUES (45, 1, 1, CAST(1200.00 AS Decimal(10, 2)), CAST(10.00 AS Decimal(10, 2)), N'', N'Trek-650')
GO
SET IDENTITY_INSERT [dbo].[Orders] ON 
GO
INSERT [dbo].[Orders] ( [CustomerId],[CustomerName], [EmployeeId], [StoreId], [OrderDate], [ShippedDate] , [IsExist] ) VALUES ( 1, N'Mohamed Mahmoud', 6, 2, CAST(N'2025-04-10T00:00:00.0000000' AS DateTime2), CAST(N'2025-04-15T00:00:00.0000000' AS DateTime2), 1 )
GO
INSERT [dbo].[Orders] ( [CustomerId],[CustomerName], [EmployeeId], [StoreId], [OrderDate], [ShippedDate], [IsExist] ) VALUES ( 5, N'Marwa Mohamed', 7, 4, CAST(N'2023-06-15T00:00:00.0000000' AS DateTime2), CAST(N'2023-06-18T00:00:00.0000000' AS DateTime2), 1)
GO
INSERT [dbo].[Orders] ( [CustomerId],[CustomerName], [EmployeeId], [StoreId], [OrderDate], [ShippedDate], [IsExist]) VALUES ( 4,N'Salma Ahmed', 10, 4, CAST(N'2023-12-06T00:00:00.0000000' AS DateTime2), CAST(N'2023-12-09T00:00:00.0000000' AS DateTime2), 1)
GO
INSERT [dbo].[Orders] ( [CustomerId],[CustomerName], [EmployeeId], [StoreId], [OrderDate], [ShippedDate], [IsExist] ) VALUES ( 4,N'Salma Ahmed', 8, 4, CAST(N'2023-10-20T00:00:00.0000000' AS DateTime2), CAST(N'2023-10-23T00:00:00.0000000' AS DateTime2), 1)
GO
INSERT [dbo].[Orders] ( [CustomerId],[CustomerName], [EmployeeId], [StoreId], [OrderDate], [ShippedDate], [IsExist] ) VALUES ( 8, N'Ali Ahmed', 11, 2, CAST(N'2023-12-25T00:00:00.0000000' AS DateTime2), CAST(N'2023-12-30T00:00:00.0000000' AS DateTime2), 1)
GO
INSERT [dbo].[Orders] ( [CustomerId],[CustomerName], [EmployeeId], [StoreId], [OrderDate], [ShippedDate], [IsExist] ) VALUES ( 9, N'Khaled Mohy', 13, 2, CAST(N'2023-12-12T00:00:00.0000000' AS DateTime2), CAST(N'2023-12-15T00:00:00.0000000' AS DateTime2), 1)
GO
INSERT [dbo].[Orders] ( [CustomerId],[CustomerName], [EmployeeId], [StoreId], [OrderDate], [ShippedDate], [IsExist] ) VALUES ( 1, N'Mohamed Mahmoud', 8, 2, CAST(N'2023-09-05T00:00:00.0000000' AS DateTime2), CAST(N'2023-09-07T00:00:00.0000000' AS DateTime2), 1)
GO
INSERT [dbo].[Orders] ( [CustomerId],[CustomerName], [EmployeeId], [StoreId], [OrderDate], [ShippedDate], [IsExist] ) VALUES ( 6, N'Hosam Ali', 14, 3, CAST(N'2024-06-03T00:00:00.0000000' AS DateTime2), CAST(N'2024-06-06T00:00:00.0000000' AS DateTime2), 1)
GO
INSERT [dbo].[Orders] ( [CustomerId],[CustomerName], [EmployeeId], [StoreId], [OrderDate], [ShippedDate], [IsExist] ) VALUES ( 12, N'Adel Mohamed', 9, 2, CAST(N'2024-05-03T00:00:00.0000000' AS DateTime2), CAST(N'2024-05-08T00:00:00.0000000' AS DateTime2), 1)
GO
INSERT [dbo].[Orders] ( [CustomerId],[CustomerName], [EmployeeId], [StoreId], [OrderDate], [ShippedDate], [IsExist] ) VALUES ( 4, N'Salma Ahmed', 13, 4, CAST(N'2024-02-14T00:00:00.0000000' AS DateTime2), CAST(N'2024-02-17T00:00:00.0000000' AS DateTime2), 1)
GO
INSERT [dbo].[Orders] ( [CustomerId],[CustomerName], [EmployeeId], [StoreId], [OrderDate], [ShippedDate], [IsExist] ) VALUES ( 1, N'Mohamed Mahmoud', 11, 2, CAST(N'2024-01-10T00:00:00.0000000' AS DateTime2), CAST(N'2024-01-15T00:00:00.0000000' AS DateTime2), 1)
GO
INSERT [dbo].[Orders] ( [CustomerId],[CustomerName], [EmployeeId], [StoreId], [OrderDate], [ShippedDate], [IsExist] ) VALUES ( 7, N'Kareem Mahmoud', 7, 4, CAST(N'2024-01-01T00:00:00.0000000' AS DateTime2), CAST(N'2024-01-05T00:00:00.0000000' AS DateTime2), 1)
GO
INSERT [dbo].[Orders] ( [CustomerId],[CustomerName], [EmployeeId], [StoreId], [OrderDate], [ShippedDate], [IsExist] ) VALUES ( 1, N'Mohamed Mahmoud', 2, 2, CAST(N'2023-01-03T00:00:00.0000000' AS DateTime2), CAST(N'2023-01-05T00:00:00.0000000' AS DateTime2), 1)
GO
INSERT [dbo].[Orders] ( [CustomerId],[CustomerName], [EmployeeId], [StoreId], [OrderDate], [ShippedDate], [IsExist] ) VALUES ( 1, N'Mohamed Mahmoud', 1, 3, CAST(N'2023-11-06T00:00:00.0000000' AS DateTime2), CAST(N'2023-11-08T00:00:00.0000000' AS DateTime2), 1)
GO
INSERT [dbo].[Orders] ( [CustomerId],[CustomerName], [EmployeeId], [StoreId], [OrderDate], [ShippedDate], [IsExist] ) VALUES ( 1, N'Mohamed Mahmoud', 1, 4, CAST(N'2023-05-10T00:00:00.0000000' AS DateTime2), CAST(N'2023-05-13T00:00:00.0000000' AS DateTime2), 1)
GO
INSERT [dbo].[Orders] ( [CustomerId],[CustomerName] ,[EmployeeId], [StoreId], [OrderDate], [ShippedDate], [IsExist] ) VALUES ( 5, N'Marwa Mohamed', 7, 4, CAST(N'2023-06-15T00:00:00.0000000' AS DateTime2), CAST(N'2023-06-18T00:00:00.0000000' AS DateTime2), 1)
GO
INSERT [dbo].[Orders] ( [CustomerId],[CustomerName] ,[EmployeeId], [StoreId], [OrderDate], [ShippedDate], [IsExist] ) VALUES ( 5, N'Marwa Mohamed', 6, 3, CAST(N'2023-03-11T00:00:00.0000000' AS DateTime2), CAST(N'2023-03-15T00:00:00.0000000' AS DateTime2), 1)
GO
INSERT [dbo].[Orders] ( [CustomerId],[CustomerName] ,[EmployeeId], [StoreId], [OrderDate], [ShippedDate], [IsExist] ) VALUES ( 4, N'Salma Ahmed', 10, 4, CAST(N'2023-12-06T00:00:00.0000000' AS DateTime2), CAST(N'2023-12-09T00:00:00.0000000' AS DateTime2), 1)
GO
INSERT [dbo].[Orders] ( [CustomerId],[CustomerName] ,[EmployeeId], [StoreId], [OrderDate], [ShippedDate], [IsExist] ) VALUES ( 4, N'Salma Ahmed', 8, 4, CAST(N'2023-10-20T00:00:00.0000000' AS DateTime2), CAST(N'2023-10-23T00:00:00.0000000' AS DateTime2), 1)
GO
INSERT [dbo].[Orders] ( [CustomerId],[CustomerName] ,[EmployeeId], [StoreId], [OrderDate], [ShippedDate], [IsExist] ) VALUES ( 8,N'Ali Ahmed', 11, 2, CAST(N'2023-12-25T00:00:00.0000000' AS DateTime2), CAST(N'2023-12-30T00:00:00.0000000' AS DateTime2), 1)
GO
INSERT [dbo].[Orders] ( [CustomerId],[CustomerName] ,[EmployeeId], [StoreId], [OrderDate], [ShippedDate], [IsExist] ) VALUES ( 9,N'Khaled Mohy', 13, 2, CAST(N'2023-12-12T00:00:00.0000000' AS DateTime2), CAST(N'2023-12-15T00:00:00.0000000' AS DateTime2), 1)
GO
INSERT [dbo].[Orders] ( [CustomerId],[CustomerName] ,[EmployeeId], [StoreId], [OrderDate], [ShippedDate], [IsExist] ) VALUES ( 1,N'Mohamed Mahmoud', 8, 2, CAST(N'2023-09-05T00:00:00.0000000' AS DateTime2), CAST(N'2023-09-07T00:00:00.0000000' AS DateTime2), 1)
GO
INSERT [dbo].[Orders] ( [CustomerId],[CustomerName] ,[EmployeeId], [StoreId], [OrderDate], [ShippedDate], [IsExist] ) VALUES ( 6, N'Hosam Ali', 14, 3, CAST(N'2024-06-03T00:00:00.0000000' AS DateTime2), CAST(N'2024-06-06T00:00:00.0000000' AS DateTime2), 1)
GO
INSERT [dbo].[Orders] ( [CustomerId],[CustomerName] ,[EmployeeId], [StoreId], [OrderDate], [ShippedDate], [IsExist] ) VALUES ( 12, N'Adel Mohamed', 9, 2, CAST(N'2024-05-03T00:00:00.0000000' AS DateTime2), CAST(N'2024-05-08T00:00:00.0000000' AS DateTime2), 1)
GO
INSERT [dbo].[Orders] ( [CustomerId],[CustomerName] ,[EmployeeId], [StoreId], [OrderDate], [ShippedDate], [IsExist] ) VALUES ( 15, N'Ali Ahmed', 17, 3, CAST(N'2024-05-06T00:00:00.0000000' AS DateTime2), CAST(N'2024-05-10T00:00:00.0000000' AS DateTime2), 1)
GO
INSERT [dbo].[Orders] ( [CustomerId],[CustomerName] ,[EmployeeId], [StoreId], [OrderDate], [ShippedDate], [IsExist] ) VALUES ( 4, N'Salma Ahmed', 9, 3, CAST(N'2024-07-15T00:00:00.0000000' AS DateTime2), CAST(N'2024-07-19T00:00:00.0000000' AS DateTime2), 1)
GO
INSERT [dbo].[Orders] ( [CustomerId],[CustomerName] ,[EmployeeId], [StoreId], [OrderDate], [ShippedDate], [IsExist] ) VALUES (7, N'Kareem Mahmoud', 10, 2, CAST(N'2024-08-18T00:00:00.0000000' AS DateTime2), CAST(N'2024-08-20T00:00:00.0000000' AS DateTime2), 1)
GO
INSERT [dbo].[Orders] ( [CustomerId],[CustomerName] ,[EmployeeId], [StoreId], [OrderDate], [ShippedDate], [IsExist] ) VALUES ( 9, N'Khaled Mohy', 12, 1, CAST(N'2024-08-19T00:00:00.0000000' AS DateTime2), CAST(N'2024-08-22T00:00:00.0000000' AS DateTime2), 1)
GO
INSERT [dbo].[Orders] ( [CustomerId],[CustomerName] ,[EmployeeId], [StoreId], [OrderDate], [ShippedDate], [IsExist] ) VALUES ( 4, N'Salma Ahmed', 13, 4, CAST(N'2024-02-14T00:00:00.0000000' AS DateTime2), CAST(N'2024-02-17T00:00:00.0000000' AS DateTime2), 1)
GO
INSERT [dbo].[Orders] ( [CustomerId],[CustomerName] ,[EmployeeId], [StoreId], [OrderDate], [ShippedDate], [IsExist] ) VALUES ( 1, N'Mohamed Mahmoud', 11, 2, CAST(N'2024-01-10T00:00:00.0000000' AS DateTime2), CAST(N'2024-01-15T00:00:00.0000000' AS DateTime2), 1)
GO
INSERT [dbo].[Orders] ( [CustomerId],[CustomerName] ,[EmployeeId], [StoreId], [OrderDate], [ShippedDate], [IsExist] ) VALUES ( 6, N'Hosam Ali', 10, 2, CAST(N'2024-01-03T00:00:00.0000000' AS DateTime2), CAST(N'2024-01-07T00:00:00.0000000' AS DateTime2), 1)
GO
INSERT [dbo].[Orders] ( [CustomerId],[CustomerName] ,[EmployeeId], [StoreId], [OrderDate], [ShippedDate], [IsExist] ) VALUES ( 7, N'Kareem Mahmoud', 7, 4, CAST(N'2024-01-01T00:00:00.0000000' AS DateTime2), CAST(N'2024-01-05T00:00:00.0000000' AS DateTime2), 1)
GO
INSERT [dbo].[Orders] ( [CustomerId],[CustomerName] ,[EmployeeId], [StoreId], [OrderDate], [ShippedDate], [IsExist] ) VALUES ( 1, N'Mohamed Mahmoud', 1, 1, CAST(N'2025-04-10T00:00:00.0000000' AS DateTime2), CAST(N'2025-04-12T00:00:00.0000000' AS DateTime2), 1)
GO
INSERT [dbo].[Orders] ( [CustomerId],[CustomerName], [EmployeeId], [StoreId], [OrderDate], [ShippedDate], [IsExist]) VALUES ( 3, N'Omar Mohamed', 1, 1, CAST(N'2025-04-11T14:24:26.3497805' AS DateTime2), CAST(N'2025-04-11T00:00:00.0000000' AS DateTime2), 1)
GO
SET IDENTITY_INSERT [dbo].[Orders] OFF
GO
SET IDENTITY_INSERT [dbo].[Products] ON 
GO
INSERT [dbo].[Products] (  [ProductName], [ModelYear], [ListPrice], [BrandId], [CategoryId], [IsExisit]) VALUES ( N'Trek-650', N'2018', CAST(1200.00 AS Decimal(18, 2)), 9, 6, 1)
GO
INSERT [dbo].[Products] (  [ProductName], [ModelYear], [ListPrice], [BrandId], [CategoryId], [IsExisit]) VALUES (  N'Ritchey Timberwolf Frameset ', N'2016', CAST(500.99 AS Decimal(18, 2)), 9, 6, 1)
GO
INSERT [dbo].[Products] (  [ProductName], [ModelYear], [ListPrice], [BrandId], [CategoryId], [IsExisit]) VALUES (  N'Surly Ice Cream Truck Frameset', N'2019', CAST(1000.00 AS Decimal(18, 2)), 8, 6, 1)
GO
INSERT [dbo].[Products] (  [ProductName], [ModelYear], [ListPrice], [BrandId], [CategoryId], [IsExisit]) VALUES (  N'Surly Straggler ', N'2020', CAST(3000.00 AS Decimal(18, 2)), 8, 4, 1)
GO
INSERT [dbo].[Products] (  [ProductName], [ModelYear], [ListPrice], [BrandId], [CategoryId], [IsExisit]) VALUES (  N'Pure Cycles William 3-Speed ', N'2017 ', CAST(1900.00 AS Decimal(18, 2)), 4, 3, 1)
GO
INSERT [dbo].[Products] (  [ProductName], [ModelYear], [ListPrice], [BrandId], [CategoryId], [IsExisit]) VALUES (  N'Pure Cycles William 3-Speed ', N'2017', CAST(1900.00 AS Decimal(18, 2)), 4, 3, 1)
GO
INSERT [dbo].[Products] (  [ProductName], [ModelYear], [ListPrice], [BrandId], [CategoryId], [IsExisit]) VALUES (  N'Trek Farley Alloy Frameset', N'2017', CAST(600.00 AS Decimal(18, 2)), 9, 6, 1)
GO
INSERT [dbo].[Products] (  [ProductName], [ModelYear], [ListPrice], [BrandId], [CategoryId], [IsExisit]) VALUES (  N'Haro Flightline One ST ', N'2018', CAST(2500.00 AS Decimal(18, 2)), 2, 6, 1)
GO
INSERT [dbo].[Products] (  [ProductName], [ModelYear], [ListPrice], [BrandId], [CategoryId], [IsExisit]) VALUES (  N'Sun Bicycles Brickell Tandem CB ', N'2017', CAST(1800.00 AS Decimal(18, 2)), 7, 3, 1)
GO
INSERT [dbo].[Products] (  [ProductName], [ModelYear], [ListPrice], [BrandId], [CategoryId], [IsExisit]) VALUES (  N'Trek Marlin 5 ', N'2018', CAST(2000.00 AS Decimal(18, 2)), 9, 6, 1)
GO
INSERT [dbo].[Products] (  [ProductName], [ModelYear], [ListPrice], [BrandId], [CategoryId], [IsExisit]) VALUES (  N'Surly ECR 27.5 ', N'2018', CAST(980.00 AS Decimal(18, 2)), 8, 7, 1)
GO
INSERT [dbo].[Products] (  [ProductName], [ModelYear], [ListPrice], [BrandId], [CategoryId], [IsExisit]) VALUES (  N'Electra Cruiser Lux 1 ', N'2018', CAST(3000.00 AS Decimal(18, 2)), 1, 3, 1)
GO
INSERT [dbo].[Products] (  [ProductName], [ModelYear], [ListPrice], [BrandId], [CategoryId], [IsExisit]) VALUES (  N'Trek Powerfly 7 FS ', N'2017', CAST(2199.99 AS Decimal(18, 2)), 9, 5, 1)
GO
INSERT [dbo].[Products] (  [ProductName], [ModelYear], [ListPrice], [BrandId], [CategoryId], [IsExisit]) VALUES (  N'Electra Townie Go! 8i Ladies', N'2019', CAST(999.99 AS Decimal(18, 2)), 1, 2, 1)
GO
INSERT [dbo].[Products] (  [ProductName], [ModelYear], [ListPrice], [BrandId], [CategoryId], [IsExisit]) VALUES (  N'Sun Bicycles Drifter 7 - Women', N'2019', CAST(5000.00 AS Decimal(18, 2)), 7, 2, 1)
GO
INSERT [dbo].[Products] (  [ProductName], [ModelYear], [ListPrice], [BrandId], [CategoryId], [IsExisit]) VALUES (  N'Haro Shredder Pro 20 ', N'2020', CAST(3500.00 AS Decimal(18, 2)), 2, 1, 1)
GO
INSERT [dbo].[Products] (  [ProductName], [ModelYear], [ListPrice], [BrandId], [CategoryId], [IsExisit]) VALUES (  N'Electra Sugar Skulls 1 (20-inch) - Girl', N'2020', CAST(2650.00 AS Decimal(18, 2)), 1, 1, 1)
GO
INSERT [dbo].[Products] (  [ProductName], [ModelYear], [ListPrice], [BrandId], [CategoryId], [IsExisit]) VALUES (  N'Trek Precaliber 24 (21-Speed) - Girls ', N'2017', CAST(1680.50 AS Decimal(18, 2)), 7, 1, 1)
GO
INSERT [dbo].[Products] (  [ProductName], [ModelYear], [ListPrice], [BrandId], [CategoryId], [IsExisit]) VALUES (  N'Trek Kids Dual Sport', N'2018', CAST(450.99 AS Decimal(18, 2)), 9, 6, 1)
GO
INSERT [dbo].[Products] (  [ProductName], [ModelYear], [ListPrice], [BrandId], [CategoryId], [IsExisit]) VALUES (  N'Sun Bicycles Cruz 3 ', N'2017', CAST(690.00 AS Decimal(18, 2)), 7, 2, 1)
GO
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (1, 1, 15)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (1, 2, 10)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (1, 3, 10)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (1, 4, 5)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (2, 1, 13)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (2, 2, 10)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (2, 3, 8)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (2, 4, 4)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (3, 1, 20)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (3, 2, 30)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (3, 3, 10)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (3, 4, 16)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (4, 1, 7)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (4, 2, 10)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (4, 3, 6)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (4, 4, 16)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (5, 1, 20)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (5, 2, 12)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (5, 3, 11)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (5, 4, 14)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (6, 1, 2)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (6, 2, 32)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (6, 3, 10)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (6, 4, 18)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (7, 1, 15)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (7, 2, 26)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (7, 4, 13)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (8, 1, 20)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (8, 2, 11)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (8, 3, 10)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (8, 4, 23)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (9, 1, 8)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (9, 2, 8)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (9, 3, 5)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (9, 4, 5)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (10, 1, 10)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (10, 2, 3)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (10, 3, 7)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (10, 4, 18)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (11, 1, 23)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (11, 2, 25)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (11, 3, 30)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (11, 4, 10)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (12, 1, 33)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (12, 2, 5)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (12, 3, 14)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (12, 4, 23)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (13, 1, 22)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (13, 2, 15)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (13, 3, 8)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (13, 4, 9)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (14, 1, 14)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (14, 2, 33)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (14, 3, 20)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (14, 4, 12)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (15, 1, 16)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (15, 2, 14)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (15, 3, 8)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (15, 4, 8)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (16, 1, 3)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (16, 2, 4)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (16, 3, 4)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (16, 4, 9)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (17, 1, 13)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (17, 2, 30)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (17, 3, 2)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (17, 4, 22)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (18, 1, 23)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (18, 2, 5)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (18, 3, 6)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (18, 4, 1)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (19, 1, 40)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (19, 2, 9)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (19, 3, 5)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (19, 4, 16)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (20, 1, 15)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (20, 2, 14)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (20, 3, 7)
GO
INSERT [dbo].[ProductStores] ([ProductId], [StoreId], [Quanttity]) VALUES (20, 4, 17)
GO
SET IDENTITY_INSERT [dbo].[Stores] ON 
GO
INSERT [dbo].[Stores] ( [StoreName], [city], [street], [Phone], [Email], [IsExist]) VALUES ( N'Smart Ride Bikes', N'Cairo', N'Fisal street', N'smartride@bikes.shop', N'(022)476-4321', 1)
GO
INSERT [dbo].[Stores] ( [StoreName], [city], [street], [Phone], [Email], [IsExist]) VALUES ( N'Fast Adventures', N'Assiut', N'Governorate Street ', N'fastadventures@bikes.shop', N'(088)379-8888', 1)
GO
INSERT [dbo].[Stores] ( [StoreName], [city], [street], [Phone], [Email], [IsExist]) VALUES ( N'Cruiser Speed', N'New Minya', N'Court Street', N'cruiserspeed@bikes.shop', N'(086)379-2518', 1)
GO
INSERT [dbo].[Stores] ( [StoreName], [city], [street], [Phone], [Email], [IsExist]) VALUES ( N'Speed Sport', N'Alexandrian', N'Nile Corniche', N'speedsport@bikes.shop', N'(03)379-5698', 1)
GO
SET IDENTITY_INSERT [dbo].[Stores] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetRoleClaims_RoleId]    Script Date: 11/4/2025 9:57:00 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 11/4/2025 9:57:00 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserClaims_UserId]    Script Date: 11/4/2025 9:57:00 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserLogins_UserId]    Script Date: 11/4/2025 9:57:00 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserRoles_RoleId]    Script Date: 11/4/2025 9:57:00 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 11/4/2025 9:57:00 PM ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 11/4/2025 9:57:00 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Employees_StoreId]    Script Date: 11/4/2025 9:57:00 PM ******/
CREATE NONCLUSTERED INDEX [IX_Employees_StoreId] ON [dbo].[Employees]
(
	[StoreId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_OrderItems_ProductId]    Script Date: 11/4/2025 9:57:00 PM ******/
CREATE NONCLUSTERED INDEX [IX_OrderItems_ProductId] ON [dbo].[OrderItems]
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Orders_CustomerId]    Script Date: 11/4/2025 9:57:00 PM ******/
CREATE NONCLUSTERED INDEX [IX_Orders_CustomerId] ON [dbo].[Orders]
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Orders_EmployeeId]    Script Date: 11/4/2025 9:57:00 PM ******/
CREATE NONCLUSTERED INDEX [IX_Orders_EmployeeId] ON [dbo].[Orders]
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Orders_StoreId]    Script Date: 11/4/2025 9:57:00 PM ******/
CREATE NONCLUSTERED INDEX [IX_Orders_StoreId] ON [dbo].[Orders]
(
	[StoreId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Products_BrandId]    Script Date: 11/4/2025 9:57:00 PM ******/
CREATE NONCLUSTERED INDEX [IX_Products_BrandId] ON [dbo].[Products]
(
	[BrandId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Products_CategoryId]    Script Date: 11/4/2025 9:57:00 PM ******/
CREATE NONCLUSTERED INDEX [IX_Products_CategoryId] ON [dbo].[Products]
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProductStores_StoreId]    Script Date: 11/4/2025 9:57:00 PM ******/
CREATE NONCLUSTERED INDEX [IX_ProductStores_StoreId] ON [dbo].[ProductStores]
(
	[StoreId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[OrderItems] ADD  DEFAULT (N'') FOR [CustomerName]
GO
ALTER TABLE [dbo].[OrderItems] ADD  DEFAULT (N'') FOR [ProductName]
GO
ALTER TABLE [dbo].[Orders] ADD  DEFAULT (N'') FOR [CustomerName]
GO
ALTER TABLE [dbo].[Products] ADD  DEFAULT (N'') FOR [ModelYear]
GO
ALTER TABLE [dbo].[Products] ADD  DEFAULT ((0.0)) FOR [ListPrice]
GO
ALTER TABLE [dbo].[Products] ADD  DEFAULT (CONVERT([bit],(1))) FOR [IsExisit]
GO
ALTER TABLE [dbo].[Stores] ADD  DEFAULT (CONVERT([bit],(1))) FOR [IsExist]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Stores_StoreId] FOREIGN KEY([StoreId])
REFERENCES [dbo].[Stores] ([StoreId])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_Stores_StoreId]
GO
ALTER TABLE [dbo].[OrderItems]  WITH CHECK ADD  CONSTRAINT [FK_OrderItems_Orders_OrderId] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([OrderId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderItems] CHECK CONSTRAINT [FK_OrderItems_Orders_OrderId]
GO
ALTER TABLE [dbo].[OrderItems]  WITH CHECK ADD  CONSTRAINT [FK_OrderItems_Products_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderItems] CHECK CONSTRAINT [FK_OrderItems_Products_ProductId]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Customers_CustomerId] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([CustomerId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Customers_CustomerId]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Employees_EmployeeId] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employees] ([EmployeeId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Employees_EmployeeId]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Stores_StoreId] FOREIGN KEY([StoreId])
REFERENCES [dbo].[Stores] ([StoreId])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Stores_StoreId]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Brands_BrandId] FOREIGN KEY([BrandId])
REFERENCES [dbo].[Brands] ([BrandId])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Brands_BrandId]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Categories_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([CategoryId])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Categories_CategoryId]
GO
ALTER TABLE [dbo].[ProductStores]  WITH CHECK ADD  CONSTRAINT [FK_ProductStores_Products_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductStores] CHECK CONSTRAINT [FK_ProductStores_Products_ProductId]
GO
ALTER TABLE [dbo].[ProductStores]  WITH CHECK ADD  CONSTRAINT [FK_ProductStores_Stores_StoreId] FOREIGN KEY([StoreId])
REFERENCES [dbo].[Stores] ([StoreId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductStores] CHECK CONSTRAINT [FK_ProductStores_Stores_StoreId]
GO
USE [master]
GO
ALTER DATABASE [CFBikeStoreDB] SET  READ_WRITE 
GO
