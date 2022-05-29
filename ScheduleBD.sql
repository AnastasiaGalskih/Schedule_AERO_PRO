USE [master]
GO
/****** Object:  Database [Schedule]    Script Date: 29.05.2022 3:56:10 ******/
CREATE DATABASE [Schedule]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Schedule', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\Schedule.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Schedule_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\Schedule_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Schedule] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Schedule].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Schedule] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Schedule] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Schedule] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Schedule] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Schedule] SET ARITHABORT OFF 
GO
ALTER DATABASE [Schedule] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Schedule] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Schedule] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Schedule] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Schedule] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Schedule] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Schedule] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Schedule] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Schedule] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Schedule] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Schedule] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Schedule] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Schedule] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Schedule] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Schedule] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Schedule] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Schedule] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Schedule] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Schedule] SET  MULTI_USER 
GO
ALTER DATABASE [Schedule] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Schedule] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Schedule] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Schedule] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Schedule] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Schedule] SET QUERY_STORE = OFF
GO
USE [Schedule]
GO
/****** Object:  Table [dbo].[History]    Script Date: 29.05.2022 3:56:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[History](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdUser] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
 CONSTRAINT [PK_History] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pass]    Script Date: 29.05.2022 3:56:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pass](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdSchedule] [int] NOT NULL,
	[Comment] [nvarchar](max) NULL,
	[IdWorker] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
 CONSTRAINT [PK_Pass] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 29.05.2022 3:56:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Schedule]    Script Date: 29.05.2022 3:56:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Schedule](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdWorker] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
	[IdTypeMachine] [int] NOT NULL,
	[PlaceNumber] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_Schedule] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TypeMachine]    Script Date: 29.05.2022 3:56:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TypeMachine](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[NumberSeats] [int] NOT NULL,
 CONSTRAINT [PK_TypeMachine] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 29.05.2022 3:56:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[Pathronymic] [nvarchar](50) NULL,
	[Phone] [nvarchar](50) NOT NULL,
	[Login] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](20) NOT NULL,
	[IdRole] [int] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Worker]    Script Date: 29.05.2022 3:56:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Worker](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[Patronymic] [nvarchar](50) NULL,
	[Phone] [nvarchar](50) NOT NULL,
	[Passport] [nvarchar](10) NOT NULL,
	[Birthday] [date] NOT NULL,
	[IdTypeMachine] [int] NOT NULL,
 CONSTRAINT [PK_Worker] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[History] ON 

INSERT [dbo].[History] ([Id], [IdUser], [Date]) VALUES (50, 1003, CAST(N'2022-05-29T00:06:14.630' AS DateTime))
INSERT [dbo].[History] ([Id], [IdUser], [Date]) VALUES (51, 1003, CAST(N'2022-05-29T00:10:02.040' AS DateTime))
INSERT [dbo].[History] ([Id], [IdUser], [Date]) VALUES (52, 1003, CAST(N'2022-05-29T03:02:03.953' AS DateTime))
SET IDENTITY_INSERT [dbo].[History] OFF
GO
SET IDENTITY_INSERT [dbo].[Pass] ON 

INSERT [dbo].[Pass] ([Id], [IdSchedule], [Comment], [IdWorker], [Date]) VALUES (13, 13, NULL, 1, CAST(N'2022-05-25T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[Pass] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([Id], [Type]) VALUES (1, N'Начальник производства')
INSERT [dbo].[Role] ([Id], [Type]) VALUES (2, N'Заместитель начальника производства')
INSERT [dbo].[Role] ([Id], [Type]) VALUES (3, N'Бухгалтер')
INSERT [dbo].[Role] ([Id], [Type]) VALUES (4, N'Администратор')
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[Schedule] ON 

INSERT [dbo].[Schedule] ([Id], [IdWorker], [Date], [IdTypeMachine], [PlaceNumber]) VALUES (13, 1, CAST(N'2022-05-25T00:00:00.000' AS DateTime), 1, CAST(1 AS Decimal(18, 0)))
INSERT [dbo].[Schedule] ([Id], [IdWorker], [Date], [IdTypeMachine], [PlaceNumber]) VALUES (14, 3, CAST(N'2022-05-25T00:00:00.000' AS DateTime), 1, CAST(2 AS Decimal(18, 0)))
INSERT [dbo].[Schedule] ([Id], [IdWorker], [Date], [IdTypeMachine], [PlaceNumber]) VALUES (15, 5, CAST(N'2022-05-25T00:00:00.000' AS DateTime), 2, CAST(1 AS Decimal(18, 0)))
INSERT [dbo].[Schedule] ([Id], [IdWorker], [Date], [IdTypeMachine], [PlaceNumber]) VALUES (16, 1, CAST(N'2022-05-27T00:00:00.000' AS DateTime), 1, CAST(1 AS Decimal(18, 0)))
INSERT [dbo].[Schedule] ([Id], [IdWorker], [Date], [IdTypeMachine], [PlaceNumber]) VALUES (17, 3, CAST(N'2022-05-27T00:00:00.000' AS DateTime), 1, CAST(2 AS Decimal(18, 0)))
SET IDENTITY_INSERT [dbo].[Schedule] OFF
GO
SET IDENTITY_INSERT [dbo].[TypeMachine] ON 

INSERT [dbo].[TypeMachine] ([Id], [Name], [NumberSeats]) VALUES (1, N'Розлив', 8)
INSERT [dbo].[TypeMachine] ([Id], [Name], [NumberSeats]) VALUES (2, N'Сборка', 6)
INSERT [dbo].[TypeMachine] ([Id], [Name], [NumberSeats]) VALUES (3, N'Упаковка', 4)
SET IDENTITY_INSERT [dbo].[TypeMachine] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([Id], [LastName], [FirstName], [Pathronymic], [Phone], [Login], [Password], [IdRole]) VALUES (1, N'Иванов', N'Иван', N'Иванович', N'89457654356', N'user1', N'user1', 1)
INSERT [dbo].[User] ([Id], [LastName], [FirstName], [Pathronymic], [Phone], [Login], [Password], [IdRole]) VALUES (2, N'Смирнов', N'Николай', N'Сергеевич', N'83482374628', N'user2', N'user2', 2)
INSERT [dbo].[User] ([Id], [LastName], [FirstName], [Pathronymic], [Phone], [Login], [Password], [IdRole]) VALUES (1003, N'Сидоров ', N'Анатолий', N'Павлович', N'89643847535', N'user4', N'user4', 4)
SET IDENTITY_INSERT [dbo].[User] OFF
GO
SET IDENTITY_INSERT [dbo].[Worker] ON 

INSERT [dbo].[Worker] ([Id], [LastName], [FirstName], [Patronymic], [Phone], [Passport], [Birthday], [IdTypeMachine]) VALUES (1, N'Платонов', N'Антон', N'Сергеевич', N'89876543456', N'1234565678', CAST(N'1995-06-30' AS Date), 1)
INSERT [dbo].[Worker] ([Id], [LastName], [FirstName], [Patronymic], [Phone], [Passport], [Birthday], [IdTypeMachine]) VALUES (3, N'Ювелиров', N'Иван', N'Анатольевич', N'89453752455', N'2353565784', CAST(N'1998-11-25' AS Date), 1)
INSERT [dbo].[Worker] ([Id], [LastName], [FirstName], [Patronymic], [Phone], [Passport], [Birthday], [IdTypeMachine]) VALUES (5, N'Антонов', N'Сергей', N'Викторович', N'89665545766', N'8975978923', CAST(N'1998-11-02' AS Date), 2)
INSERT [dbo].[Worker] ([Id], [LastName], [FirstName], [Patronymic], [Phone], [Passport], [Birthday], [IdTypeMachine]) VALUES (7, N'Чижов', N'Александр', N'Сергеевич', N'89765467865', N'5678543677', CAST(N'1990-12-08' AS Date), 3)
SET IDENTITY_INSERT [dbo].[Worker] OFF
GO
ALTER TABLE [dbo].[History]  WITH CHECK ADD  CONSTRAINT [FK_History_User] FOREIGN KEY([IdUser])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[History] CHECK CONSTRAINT [FK_History_User]
GO
ALTER TABLE [dbo].[Pass]  WITH CHECK ADD  CONSTRAINT [FK_Pass_Schedule] FOREIGN KEY([IdSchedule])
REFERENCES [dbo].[Schedule] ([Id])
GO
ALTER TABLE [dbo].[Pass] CHECK CONSTRAINT [FK_Pass_Schedule]
GO
ALTER TABLE [dbo].[Pass]  WITH CHECK ADD  CONSTRAINT [FK_Pass_Worker] FOREIGN KEY([IdWorker])
REFERENCES [dbo].[Worker] ([Id])
GO
ALTER TABLE [dbo].[Pass] CHECK CONSTRAINT [FK_Pass_Worker]
GO
ALTER TABLE [dbo].[Schedule]  WITH CHECK ADD  CONSTRAINT [FK_Schedule_TypeMachine] FOREIGN KEY([IdTypeMachine])
REFERENCES [dbo].[TypeMachine] ([Id])
GO
ALTER TABLE [dbo].[Schedule] CHECK CONSTRAINT [FK_Schedule_TypeMachine]
GO
ALTER TABLE [dbo].[Schedule]  WITH CHECK ADD  CONSTRAINT [FK_Schedule_Worker] FOREIGN KEY([IdWorker])
REFERENCES [dbo].[Worker] ([Id])
GO
ALTER TABLE [dbo].[Schedule] CHECK CONSTRAINT [FK_Schedule_Worker]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Role] FOREIGN KEY([IdRole])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Role]
GO
ALTER TABLE [dbo].[Worker]  WITH CHECK ADD  CONSTRAINT [FK_Worker_TypeMachine] FOREIGN KEY([IdTypeMachine])
REFERENCES [dbo].[TypeMachine] ([Id])
GO
ALTER TABLE [dbo].[Worker] CHECK CONSTRAINT [FK_Worker_TypeMachine]
GO
USE [master]
GO
ALTER DATABASE [Schedule] SET  READ_WRITE 
GO
