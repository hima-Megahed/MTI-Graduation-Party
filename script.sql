USE [master]
GO
/****** Object:  Database [MTI_GraduationParty]    Script Date: 11/12/2019 13:15:35 ******/
CREATE DATABASE [MTI_GraduationParty] ON  PRIMARY 
( NAME = N'MTI_GraduationParty', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10.MSSQLSERVER\MSSQL\DATA\MTI_GraduationParty.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'MTI_GraduationParty_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10.MSSQLSERVER\MSSQL\DATA\MTI_GraduationParty_log.ldf' , SIZE = 1536KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [MTI_GraduationParty] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MTI_GraduationParty].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MTI_GraduationParty] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [MTI_GraduationParty] SET ANSI_NULLS OFF
GO
ALTER DATABASE [MTI_GraduationParty] SET ANSI_PADDING OFF
GO
ALTER DATABASE [MTI_GraduationParty] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [MTI_GraduationParty] SET ARITHABORT OFF
GO
ALTER DATABASE [MTI_GraduationParty] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [MTI_GraduationParty] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [MTI_GraduationParty] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [MTI_GraduationParty] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [MTI_GraduationParty] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [MTI_GraduationParty] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [MTI_GraduationParty] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [MTI_GraduationParty] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [MTI_GraduationParty] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [MTI_GraduationParty] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [MTI_GraduationParty] SET  DISABLE_BROKER
GO
ALTER DATABASE [MTI_GraduationParty] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [MTI_GraduationParty] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [MTI_GraduationParty] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [MTI_GraduationParty] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [MTI_GraduationParty] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [MTI_GraduationParty] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [MTI_GraduationParty] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [MTI_GraduationParty] SET  READ_WRITE
GO
ALTER DATABASE [MTI_GraduationParty] SET RECOVERY SIMPLE
GO
ALTER DATABASE [MTI_GraduationParty] SET  MULTI_USER
GO
ALTER DATABASE [MTI_GraduationParty] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [MTI_GraduationParty] SET DB_CHAINING OFF
GO
USE [MTI_GraduationParty]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 11/12/2019 13:15:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](100) NULL,
	[TableId] [int] NULL,
	[BusId] [int] NULL,
	[BreakfastOutlet] [int] NULL,
	[LunchOutlet] [int] NULL,
 CONSTRAINT [PK_Student_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Invitation]    Script Date: 11/12/2019 13:15:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invitation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [int] NULL,
	[Name] [nvarchar](100) NULL,
	[Relationship] [nvarchar](50) NULL,
	[NationalId] [nvarchar](50) NULL,
	[BirthDate] [date] NULL,
	[PlaceOfBirth] [nvarchar](50) NULL,
	[Address] [nvarchar](200) NULL,
	[Attended] [bit] NULL,
	[PresenceDateTime] [datetime] NULL,
	[Approved] [bit] NOT NULL,
 CONSTRAINT [PK_Invitation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ExtraInvitations]    Script Date: 11/12/2019 13:15:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExtraInvitations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [int] NULL,
	[Name] [nvarchar](100) NULL,
	[Relationship] [nvarchar](50) NULL,
	[NationalId] [nvarchar](50) NULL,
	[BirthDate] [date] NULL,
	[Address] [nvarchar](200) NULL,
	[PresenceDateTime] [datetime] NULL,
 CONSTRAINT [PK_SystemTest] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[AttendeesBusReport]    Script Date: 11/12/2019 13:15:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ibrahim Hasan
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[AttendeesBusReport]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [StudentId]
      ,[Invitation].[Name]
      ,[Relationship]
      ,[NationalId]
      ,[BusId]
      
  FROM [MTI_GraduationParty].[dbo].[Invitation] INNER JOIN [MTI_GraduationParty].[dbo].[Student]
  ON Invitation.StudentId = Student.Id
  WHERE Invitation.Approved = 1
  order by BusId;
END
GO
/****** Object:  Default [DF_Invitation_Attended]    Script Date: 11/12/2019 13:15:35 ******/
ALTER TABLE [dbo].[Invitation] ADD  CONSTRAINT [DF_Invitation_Attended]  DEFAULT ((0)) FOR [Attended]
GO
/****** Object:  Default [DF_Invitation_Approved]    Script Date: 11/12/2019 13:15:35 ******/
ALTER TABLE [dbo].[Invitation] ADD  CONSTRAINT [DF_Invitation_Approved]  DEFAULT ((0)) FOR [Approved]
GO
/****** Object:  Default [DF_SystemTest_StudentStatus]    Script Date: 11/12/2019 13:15:35 ******/
ALTER TABLE [dbo].[ExtraInvitations] ADD  CONSTRAINT [DF_SystemTest_StudentStatus]  DEFAULT ((0)) FOR [Relationship]
GO
/****** Object:  ForeignKey [FK_Invitation_Student]    Script Date: 11/12/2019 13:15:35 ******/
ALTER TABLE [dbo].[Invitation]  WITH CHECK ADD  CONSTRAINT [FK_Invitation_Student] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Student] ([Id])
GO
ALTER TABLE [dbo].[Invitation] CHECK CONSTRAINT [FK_Invitation_Student]
GO
/****** Object:  ForeignKey [FK_SystemTest_Student]    Script Date: 11/12/2019 13:15:35 ******/
ALTER TABLE [dbo].[ExtraInvitations]  WITH CHECK ADD  CONSTRAINT [FK_SystemTest_Student] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Student] ([Id])
GO
ALTER TABLE [dbo].[ExtraInvitations] CHECK CONSTRAINT [FK_SystemTest_Student]
GO
