USE [master]
GO
/****** Object:  Database [ORBook2]    Script Date: 9/16/2024 2:53:36 PM ******/
CREATE DATABASE [ORBook2]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ORBook2', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER1\MSSQL\DATA\ORBook2.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ORBook2_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER1\MSSQL\DATA\ORBook2_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [ORBook2] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ORBook2].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ORBook2] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ORBook2] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ORBook2] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ORBook2] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ORBook2] SET ARITHABORT OFF 
GO
ALTER DATABASE [ORBook2] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ORBook2] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ORBook2] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ORBook2] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ORBook2] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ORBook2] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ORBook2] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ORBook2] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ORBook2] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ORBook2] SET  ENABLE_BROKER 
GO
ALTER DATABASE [ORBook2] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ORBook2] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ORBook2] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ORBook2] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ORBook2] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ORBook2] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [ORBook2] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ORBook2] SET RECOVERY FULL 
GO
ALTER DATABASE [ORBook2] SET  MULTI_USER 
GO
ALTER DATABASE [ORBook2] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ORBook2] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ORBook2] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ORBook2] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ORBook2] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ORBook2] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'ORBook2', N'ON'
GO
ALTER DATABASE [ORBook2] SET QUERY_STORE = ON
GO
ALTER DATABASE [ORBook2] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [ORBook2]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 9/16/2024 2:53:36 PM ******/
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
/****** Object:  Table [dbo].[Book]    Script Date: 9/16/2024 2:53:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Book](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](60) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Author] [nvarchar](max) NULL,
	[CreatedTime] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Book] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BookGenre]    Script Date: 9/16/2024 2:53:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookGenre](
	[BookId] [int] NOT NULL,
	[GenreId] [int] NOT NULL,
 CONSTRAINT [PK_BookGenre] PRIMARY KEY CLUSTERED 
(
	[BookId] ASC,
	[GenreId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BookUser]    Script Date: 9/16/2024 2:53:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookUser](
	[BookId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_BookUser] PRIMARY KEY CLUSTERED 
(
	[BookId] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Genre]    Script Date: 9/16/2024 2:53:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Genre](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](60) NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_Genre] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notification]    Script Date: 9/16/2024 2:53:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notification](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[VolumnId] [int] NOT NULL,
	[IsRead] [bit] NOT NULL,
	[CreatedTime] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Notification] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 9/16/2024 2:53:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[role] [nvarchar](max) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Volumn]    Script Date: 9/16/2024 2:53:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Volumn](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](60) NULL,
	[CreatedTime] [datetime2](7) NOT NULL,
	[UpdatedTime] [datetime] NULL,
	[BookId] [int] NOT NULL,
 CONSTRAINT [PK_Volumn] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240814040312_book_genre_volumn', N'8.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240815043950_update_volumn', N'8.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240815045902_update_volumn_2', N'8.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240815065111_update_volumn_3', N'8.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240815075141_user', N'8.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240816014325_update_book', N'8.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240816084514_BookUser', N'8.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240818044630_notification', N'8.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240818052026_update_notification', N'8.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240818062945_update_notification_4', N'8.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240910020822_rowversion', N'8.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240910025232_rowversion_2', N'8.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240910085147_validation_6', N'8.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240910085235_validation_7', N'8.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240910085328_validation_8', N'8.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240911034336_update-notification_5', N'8.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240916035400_update', N'8.0.8')
GO
SET IDENTITY_INSERT [dbo].[Book] ON 

INSERT [dbo].[Book] ([Id], [Name], [Description], [Author], [CreatedTime]) VALUES (18, N'Âm Thanh Và Cuồng Nộ', N'Âm Thanh Và Cuồng Nộ là một trong những cuốn tiểu thuyết hay mà bạn không thể bỏ qua, được viết bởi hào văn William Faulkner. Tiểu thuyết được ấn hành lần đầu tiên vào ngày 7.10.1929, chính là tác phẩm đã giúp William Faulkner đạt đến đỉnh cao của sự thành công.', N'William Faulkner', CAST(N'2024-09-11T09:21:35.9387084' AS DateTime2))
INSERT [dbo].[Book] ([Id], [Name], [Description], [Author], [CreatedTime]) VALUES (19, N'Thép Đã Tôi Thế Đấy', N'Đối với những ai đam mê văn học lịch sử thì có lẽ sẽ biết đến Thép Đã Tôi Thế Đấy của nhà văn Nikolai Ostrovsky. Tác phẩm là một cuốn nhật ký ghi chép lại cả quá trình tôi thép, những bước đường gian khổ trưởng thành của thế hệ thanh niên Xô Viết đời đầu.', N'Nikolai Ostrovsky', CAST(N'2024-09-11T09:21:57.9103787' AS DateTime2))
INSERT [dbo].[Book] ([Id], [Name], [Description], [Author], [CreatedTime]) VALUES (20, N'Nhà Giả Kim', N'Nhà Giả Kim luôn nằm trong top những cuốn sách bán chạy nhất thế giới, được viết bởi nhà văn Paulo Coelho. Quyển sách sẽ rất phù hợp cho những ai đang cần một sự định hướng đúng đắn cho cuộc đời của mình.', N'Paulo Coelho', CAST(N'2024-09-11T09:22:11.8996618' AS DateTime2))
INSERT [dbo].[Book] ([Id], [Name], [Description], [Author], [CreatedTime]) VALUES (21, N'Không Gia Đình', N'Nội dung cuốn tiểu thuyết kể về cuộc đời phiêu bạt của một cậu bé Remi - không có cha, không có mẹ. Cậu được đem về nuôi trong một gia đình sống ở vùng quê xa xôi, hẻo lánh nhưng may mắn thay, Remi được mẹ nuôi Barberin yêu thương và xem như con ruột của mình.', N'Paulo Coelho', CAST(N'2024-09-11T09:23:08.5766039' AS DateTime2))
INSERT [dbo].[Book] ([Id], [Name], [Description], [Author], [CreatedTime]) VALUES (22, N'Ông Già Và Biển Cả', N'Ông Già Và Biển Cả hay còn có tên tiếng Anh là The Old Man and the Sea, đây là một cuốn tiểu thuyết hay và vô cùng nổi tiếng được viết dưới ngòi bút của nhà văn người Mỹ - Ernest Hemingway. Tác phẩm được nhà văn sáng tác vào năm 1951 tại Cuba, đã mang về giải thưởng giá trị Pulitzer.', N'Nikolai Ostrovsky', CAST(N'2024-09-11T09:23:37.2216374' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Book] OFF
GO
INSERT [dbo].[BookGenre] ([BookId], [GenreId]) VALUES (18, 1)
INSERT [dbo].[BookGenre] ([BookId], [GenreId]) VALUES (19, 1)
INSERT [dbo].[BookGenre] ([BookId], [GenreId]) VALUES (22, 1)
INSERT [dbo].[BookGenre] ([BookId], [GenreId]) VALUES (20, 2)
INSERT [dbo].[BookGenre] ([BookId], [GenreId]) VALUES (22, 2)
INSERT [dbo].[BookGenre] ([BookId], [GenreId]) VALUES (18, 3)
INSERT [dbo].[BookGenre] ([BookId], [GenreId]) VALUES (21, 3)
INSERT [dbo].[BookGenre] ([BookId], [GenreId]) VALUES (22, 3)
INSERT [dbo].[BookGenre] ([BookId], [GenreId]) VALUES (19, 4)
INSERT [dbo].[BookGenre] ([BookId], [GenreId]) VALUES (22, 4)
INSERT [dbo].[BookGenre] ([BookId], [GenreId]) VALUES (18, 5)
INSERT [dbo].[BookGenre] ([BookId], [GenreId]) VALUES (18, 6)
INSERT [dbo].[BookGenre] ([BookId], [GenreId]) VALUES (19, 7)
INSERT [dbo].[BookGenre] ([BookId], [GenreId]) VALUES (20, 7)
INSERT [dbo].[BookGenre] ([BookId], [GenreId]) VALUES (21, 8)
INSERT [dbo].[BookGenre] ([BookId], [GenreId]) VALUES (20, 10)
INSERT [dbo].[BookGenre] ([BookId], [GenreId]) VALUES (21, 10)
GO
INSERT [dbo].[BookUser] ([BookId], [UserId]) VALUES (18, 1)
INSERT [dbo].[BookUser] ([BookId], [UserId]) VALUES (18, 2)
GO
SET IDENTITY_INSERT [dbo].[Genre] ON 

INSERT [dbo].[Genre] ([Id], [Name]) VALUES (1, N'Phiêu lưu')
INSERT [dbo].[Genre] ([Id], [Name]) VALUES (2, N'Hiện thực')
INSERT [dbo].[Genre] ([Id], [Name]) VALUES (3, N'Văn học hiện đại')
INSERT [dbo].[Genre] ([Id], [Name]) VALUES (4, N'Huyền bí')
INSERT [dbo].[Genre] ([Id], [Name]) VALUES (5, N'Triết lý')
INSERT [dbo].[Genre] ([Id], [Name]) VALUES (6, N'Khoa học')
INSERT [dbo].[Genre] ([Id], [Name]) VALUES (7, N'Lịch sửc')
INSERT [dbo].[Genre] ([Id], [Name]) VALUES (8, N'Lãng mạn')
INSERT [dbo].[Genre] ([Id], [Name]) VALUES (9, N'Chính trị')
INSERT [dbo].[Genre] ([Id], [Name]) VALUES (10, N'Gia đình')
SET IDENTITY_INSERT [dbo].[Genre] OFF
GO
SET IDENTITY_INSERT [dbo].[Notification] ON 

INSERT [dbo].[Notification] ([Id], [UserId], [VolumnId], [IsRead], [CreatedTime]) VALUES (1, 2, 8, 1, CAST(N'2024-09-11T10:18:48.0473525' AS DateTime2))
INSERT [dbo].[Notification] ([Id], [UserId], [VolumnId], [IsRead], [CreatedTime]) VALUES (2, 2, 9, 1, CAST(N'2024-09-11T10:44:25.6697359' AS DateTime2))
INSERT [dbo].[Notification] ([Id], [UserId], [VolumnId], [IsRead], [CreatedTime]) VALUES (3, 2, 10, 1, CAST(N'2024-09-11T10:46:10.7764773' AS DateTime2))
INSERT [dbo].[Notification] ([Id], [UserId], [VolumnId], [IsRead], [CreatedTime]) VALUES (4, 1, 11, 0, CAST(N'2024-09-11T10:47:00.8479553' AS DateTime2))
INSERT [dbo].[Notification] ([Id], [UserId], [VolumnId], [IsRead], [CreatedTime]) VALUES (5, 2, 11, 0, CAST(N'2024-09-11T10:47:00.8509954' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Notification] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([Id], [Name], [Email], [Password], [role]) VALUES (1, N'Tuyến', N'tuyen@gmail.com', N'123', N'Admin')
INSERT [dbo].[User] ([Id], [Name], [Email], [Password], [role]) VALUES (2, N'Tuyến 2', N'tuyenad@gmail.com', N'123', N'Admin')
SET IDENTITY_INSERT [dbo].[User] OFF
GO
SET IDENTITY_INSERT [dbo].[Volumn] ON 

INSERT [dbo].[Volumn] ([Id], [Title], [CreatedTime], [UpdatedTime], [BookId]) VALUES (8, N'When Harry Met Sally123', CAST(N'2024-09-11T10:18:48.0025372' AS DateTime2), CAST(N'2024-09-11T10:18:48.003' AS DateTime), 18)
INSERT [dbo].[Volumn] ([Id], [Title], [CreatedTime], [UpdatedTime], [BookId]) VALUES (9, N'When Harry Met Sally123', CAST(N'2024-09-11T10:44:25.5517802' AS DateTime2), CAST(N'2024-09-11T10:44:25.553' AS DateTime), 18)
INSERT [dbo].[Volumn] ([Id], [Title], [CreatedTime], [UpdatedTime], [BookId]) VALUES (10, N'When Harry Met Sally123', CAST(N'2024-09-11T10:46:10.7684563' AS DateTime2), CAST(N'2024-09-11T10:46:10.770' AS DateTime), 18)
INSERT [dbo].[Volumn] ([Id], [Title], [CreatedTime], [UpdatedTime], [BookId]) VALUES (11, N'When Harry Met Sally123', CAST(N'2024-09-11T10:47:00.8412684' AS DateTime2), CAST(N'2024-09-11T10:47:00.840' AS DateTime), 18)
SET IDENTITY_INSERT [dbo].[Volumn] OFF
GO
/****** Object:  Index [IX_BookGenre_GenreId]    Script Date: 9/16/2024 2:53:36 PM ******/
CREATE NONCLUSTERED INDEX [IX_BookGenre_GenreId] ON [dbo].[BookGenre]
(
	[GenreId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_BookUser_UserId]    Script Date: 9/16/2024 2:53:36 PM ******/
CREATE NONCLUSTERED INDEX [IX_BookUser_UserId] ON [dbo].[BookUser]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Notification_UserId]    Script Date: 9/16/2024 2:53:36 PM ******/
CREATE NONCLUSTERED INDEX [IX_Notification_UserId] ON [dbo].[Notification]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Notification_VolumnId]    Script Date: 9/16/2024 2:53:36 PM ******/
CREATE NONCLUSTERED INDEX [IX_Notification_VolumnId] ON [dbo].[Notification]
(
	[VolumnId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Volumn_BookId]    Script Date: 9/16/2024 2:53:36 PM ******/
CREATE NONCLUSTERED INDEX [IX_Volumn_BookId] ON [dbo].[Volumn]
(
	[BookId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Volumn] ADD  DEFAULT ((0)) FOR [BookId]
GO
ALTER TABLE [dbo].[BookGenre]  WITH CHECK ADD  CONSTRAINT [FK_BookGenre_Book_BookId] FOREIGN KEY([BookId])
REFERENCES [dbo].[Book] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BookGenre] CHECK CONSTRAINT [FK_BookGenre_Book_BookId]
GO
ALTER TABLE [dbo].[BookGenre]  WITH CHECK ADD  CONSTRAINT [FK_BookGenre_Genre_GenreId] FOREIGN KEY([GenreId])
REFERENCES [dbo].[Genre] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BookGenre] CHECK CONSTRAINT [FK_BookGenre_Genre_GenreId]
GO
ALTER TABLE [dbo].[BookUser]  WITH CHECK ADD  CONSTRAINT [FK_BookUser_Book_BookId] FOREIGN KEY([BookId])
REFERENCES [dbo].[Book] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BookUser] CHECK CONSTRAINT [FK_BookUser_Book_BookId]
GO
ALTER TABLE [dbo].[BookUser]  WITH CHECK ADD  CONSTRAINT [FK_BookUser_User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BookUser] CHECK CONSTRAINT [FK_BookUser_User_UserId]
GO
ALTER TABLE [dbo].[Notification]  WITH CHECK ADD  CONSTRAINT [FK_Notification_User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Notification] CHECK CONSTRAINT [FK_Notification_User_UserId]
GO
ALTER TABLE [dbo].[Notification]  WITH CHECK ADD  CONSTRAINT [FK_Notification_Volumn_VolumnId] FOREIGN KEY([VolumnId])
REFERENCES [dbo].[Volumn] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Notification] CHECK CONSTRAINT [FK_Notification_Volumn_VolumnId]
GO
ALTER TABLE [dbo].[Volumn]  WITH CHECK ADD  CONSTRAINT [FK_Volumn_Book_BookId] FOREIGN KEY([BookId])
REFERENCES [dbo].[Book] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Volumn] CHECK CONSTRAINT [FK_Volumn_Book_BookId]
GO
USE [master]
GO
ALTER DATABASE [ORBook2] SET  READ_WRITE 
GO
