USE [master]
GO
/****** Object:  Database [mobiledb1]    Script Date: 30.11.2021 8:42:48 ******/
CREATE DATABASE [mobiledb1]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'mobiledb1', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\mobiledb1.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'mobiledb1_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\mobiledb1_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [mobiledb1] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [mobiledb1].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [mobiledb1] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [mobiledb1] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [mobiledb1] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [mobiledb1] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [mobiledb1] SET ARITHABORT OFF 
GO
ALTER DATABASE [mobiledb1] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [mobiledb1] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [mobiledb1] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [mobiledb1] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [mobiledb1] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [mobiledb1] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [mobiledb1] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [mobiledb1] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [mobiledb1] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [mobiledb1] SET  ENABLE_BROKER 
GO
ALTER DATABASE [mobiledb1] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [mobiledb1] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [mobiledb1] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [mobiledb1] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [mobiledb1] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [mobiledb1] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [mobiledb1] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [mobiledb1] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [mobiledb1] SET  MULTI_USER 
GO
ALTER DATABASE [mobiledb1] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [mobiledb1] SET DB_CHAINING OFF 
GO
ALTER DATABASE [mobiledb1] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [mobiledb1] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [mobiledb1] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [mobiledb1] SET QUERY_STORE = OFF
GO
USE [mobiledb1]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 30.11.2021 8:42:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Achievements]    Script Date: 30.11.2021 8:42:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Achievements](
	[Id] [uniqueidentifier] NOT NULL,
	[Date] [datetime] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Mark] [int] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_dbo.Achievements] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Exercises]    Script Date: 30.11.2021 8:42:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Exercises](
	[Id] [uniqueidentifier] NOT NULL,
	[Number] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CorrectAnswer] [nvarchar](max) NULL,
	[Answers] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Exercises] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Journals]    Script Date: 30.11.2021 8:42:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Journals](
	[Id] [uniqueidentifier] NOT NULL,
	[Grade] [int] NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Journals] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lessons]    Script Date: 30.11.2021 8:42:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lessons](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[ScheduleId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_dbo.Lessons] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LessonTasks]    Script Date: 30.11.2021 8:42:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LessonTasks](
	[Id] [uniqueidentifier] NOT NULL,
	[LessonId] [uniqueidentifier] NOT NULL,
	[TaskId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_dbo.LessonTasks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Logs]    Script Date: 30.11.2021 8:42:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Logs](
	[Id] [uniqueidentifier] NOT NULL,
	[Date] [datetime] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[UserId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_dbo.Logs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Schedules]    Script Date: 30.11.2021 8:42:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Schedules](
	[Id] [uniqueidentifier] NOT NULL,
	[Date] [datetime] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_dbo.Schedules] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 30.11.2021 8:42:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [uniqueidentifier] NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[DateOfBirth] [datetime] NULL,
	[Grade] [int] NULL,
	[WorkExperience] [int] NULL,
	[Role] [int] NOT NULL,
	[knowledgeLevel] [int] NOT NULL,
	[JournalId] [uniqueidentifier] NULL,
	[Password] [nvarchar](30) NOT NULL,
 CONSTRAINT [PK_dbo.Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vocabularies]    Script Date: 30.11.2021 8:42:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vocabularies](
	[Id] [uniqueidentifier] NOT NULL,
	[LessonId] [uniqueidentifier] NOT NULL,
	[WordsTranslationsByteArray] [varbinary](max) NULL,
 CONSTRAINT [PK_dbo.Vocabularies] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Index [IX_UserId]    Script Date: 30.11.2021 8:42:48 ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[Achievements]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ScheduleId]    Script Date: 30.11.2021 8:42:48 ******/
CREATE NONCLUSTERED INDEX [IX_ScheduleId] ON [dbo].[Lessons]
(
	[ScheduleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_LessonId]    Script Date: 30.11.2021 8:42:48 ******/
CREATE NONCLUSTERED INDEX [IX_LessonId] ON [dbo].[LessonTasks]
(
	[LessonId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_TaskId]    Script Date: 30.11.2021 8:42:48 ******/
CREATE NONCLUSTERED INDEX [IX_TaskId] ON [dbo].[LessonTasks]
(
	[TaskId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserId]    Script Date: 30.11.2021 8:42:48 ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[Logs]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserId]    Script Date: 30.11.2021 8:42:48 ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[Schedules]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_JournalId]    Script Date: 30.11.2021 8:42:48 ******/
CREATE NONCLUSTERED INDEX [IX_JournalId] ON [dbo].[Users]
(
	[JournalId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_LessonId]    Script Date: 30.11.2021 8:42:48 ******/
CREATE NONCLUSTERED INDEX [IX_LessonId] ON [dbo].[Vocabularies]
(
	[LessonId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ('') FOR [Password]
GO
ALTER TABLE [dbo].[Achievements]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Achievements_dbo.Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Achievements] CHECK CONSTRAINT [FK_dbo.Achievements_dbo.Users_UserId]
GO
ALTER TABLE [dbo].[Lessons]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Lessons_dbo.Schedules_ScheduleId] FOREIGN KEY([ScheduleId])
REFERENCES [dbo].[Schedules] ([Id])
GO
ALTER TABLE [dbo].[Lessons] CHECK CONSTRAINT [FK_dbo.Lessons_dbo.Schedules_ScheduleId]
GO
ALTER TABLE [dbo].[LessonTasks]  WITH CHECK ADD  CONSTRAINT [FK_dbo.LessonTasks_dbo.Exercises_TaskId] FOREIGN KEY([TaskId])
REFERENCES [dbo].[Exercises] ([Id])
GO
ALTER TABLE [dbo].[LessonTasks] CHECK CONSTRAINT [FK_dbo.LessonTasks_dbo.Exercises_TaskId]
GO
ALTER TABLE [dbo].[LessonTasks]  WITH CHECK ADD  CONSTRAINT [FK_dbo.LessonTasks_dbo.Lessons_LessonId] FOREIGN KEY([LessonId])
REFERENCES [dbo].[Lessons] ([Id])
GO
ALTER TABLE [dbo].[LessonTasks] CHECK CONSTRAINT [FK_dbo.LessonTasks_dbo.Lessons_LessonId]
GO
ALTER TABLE [dbo].[Logs]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Logs_dbo.Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Logs] CHECK CONSTRAINT [FK_dbo.Logs_dbo.Users_UserId]
GO
ALTER TABLE [dbo].[Schedules]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Schedules_dbo.Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Schedules] CHECK CONSTRAINT [FK_dbo.Schedules_dbo.Users_UserId]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Users_dbo.Journals_JournalId] FOREIGN KEY([JournalId])
REFERENCES [dbo].[Journals] ([Id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_dbo.Users_dbo.Journals_JournalId]
GO
ALTER TABLE [dbo].[Vocabularies]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Vocabularies_dbo.Lessons_LessonId] FOREIGN KEY([LessonId])
REFERENCES [dbo].[Lessons] ([Id])
GO
ALTER TABLE [dbo].[Vocabularies] CHECK CONSTRAINT [FK_dbo.Vocabularies_dbo.Lessons_LessonId]
GO
USE [master]
GO
ALTER DATABASE [mobiledb1] SET  READ_WRITE 
GO
