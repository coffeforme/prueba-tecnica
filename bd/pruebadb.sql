USE [master]
GO
/****** Object:  Database [pruebadb]    Script Date: 14/09/2020 3:25:34 p. m. ******/
CREATE DATABASE [pruebadb]
GO
USE [pruebadb]
GO
/****** Object:  Schema [auth]    Script Date: 14/09/2020 3:25:34 p. m. ******/
CREATE SCHEMA [auth]
GO
/****** Object:  Schema [parameter]    Script Date: 14/09/2020 3:25:34 p. m. ******/
CREATE SCHEMA [parameter]
GO
/****** Object:  Table [auth].[role]    Script Date: 14/09/2020 3:25:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [auth].[role](
	[uid] [uniqueidentifier] NOT NULL,
	[name] [varchar](50) NULL,
	[description] [varchar](50) NULL,
 CONSTRAINT [pk_role] PRIMARY KEY CLUSTERED 
(
	[uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [auth].[userrole]    Script Date: 14/09/2020 3:25:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [auth].[userrole](
	[uid] [uniqueidentifier] NOT NULL,
	[date] [date] NULL,
	[id_user] [uniqueidentifier] NOT NULL,
	[id_role] [uniqueidentifier] NOT NULL,
 CONSTRAINT [pk_userrole] PRIMARY KEY CLUSTERED 
(
	[uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [auth].[user]    Script Date: 14/09/2020 3:25:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [auth].[user](
	[uid] [uniqueidentifier] NOT NULL,
	[username] [varchar](60) NOT NULL,
	[password] [varbinary](max) NOT NULL,
	[email] [varchar](100) NOT NULL,
	[firstname] [varchar](50) NOT NULL,
	[lastname] [varchar](50) NULL,
	[fullname]  AS (([firstname]+' ')+[lastname]),
	[cel] [varchar](10) NULL,
	[address] [varchar] (50) NOT NULL,
	[salt] [varbinary](max) NOT NULL,
	[requirechangepass] [bit] NOT NULL,
	[confirmationcode] [varchar](6) NULL,
	[sayhi] [bit] NOT NULL,
	[emailconfirmed] [bit] NOT NULL,
	[state] [bit] NOT NULL,
	[date] [datetime] NOT NULL,
 CONSTRAINT [pk_user] PRIMARY KEY CLUSTERED 
(
	[uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [parameter].[page]    Script Date: 14/09/2020 3:25:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [parameter].[page](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [varchar](50) NULL,
	[icon] [varchar](max) NULL,
	[route] [varchar](50) NULL,
 CONSTRAINT [pk_page] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [parameter].[pagerole]    Script Date: 14/09/2020 3:25:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [parameter].[pagerole](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_role] [uniqueidentifier] NOT NULL,
	[id_page] [int] NOT NULL,
	[date] [datetime] NULL,
 CONSTRAINT [pk_pagerole] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [parameter].[action]    Script Date: 21/09/2020 12:09:55 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [parameter].[action](
	[id] [smallint] NOT NULL,
	[name] [varchar](10) NOT NULL,
 CONSTRAINT [pk_action] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [parameter].[pageroleaction]    Script Date: 21/09/2020 12:09:56 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [parameter].[pageroleaction](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_pagerole] [int] NOT NULL,
	[id_action] [smallint] NOT NULL,
 CONSTRAINT [pk_pageroleaction] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
--Data
INSERT [parameter].[action] ([id], [name]) VALUES (1, N'CREATE')
GO
INSERT [parameter].[action] ([id], [name]) VALUES (2, N'UPDATE')
GO
INSERT [parameter].[action] ([id], [name]) VALUES (3, N'DELETE')
GO
INSERT [parameter].[action] ([id], [name]) VALUES (4, N'CONSULT')
GO
INSERT [auth].[role] ([uid], [name], [description]) VALUES (N'4c4095de-c86e-4144-8083-2b11ba18df1d', N'visitor', N'Visitante')
GO
INSERT [auth].[role] ([uid], [name], [description]) VALUES (N'b695acf7-9768-4743-b9a4-f5b190d8a19a', N'assistant', N'Asistente')
GO
INSERT [auth].[role] ([uid], [name], [description]) VALUES (N'b695acf7-9768-4743-b9a4-f5b190d8a19b', N'editor', N'Editor')
GO
INSERT [auth].[role] ([uid], [name], [description]) VALUES (N'd679b0c2-5f98-487f-b709-eb46884a0b82', N'admin', N'Administrador')
GO
INSERT [auth].[user] ([uid], [username], [email], [firstname], [lastname], [cel], [address], [password], [salt], [requirechangepass], [confirmationcode], [sayhi], [emailconfirmed], [state], [date]) VALUES (N'003d5f00-2d00-4ccf-ac8d-48dae2b16998', 'admin', N'admin@admin.com', 'Administrador', 'Sistema', '3022165997', 'Cll 123 #45-67', 0xE49CB5B2F2B70FC86A82CAA0CF1C9451AB33102CCDF232FF4B8B5F92F9425483, 0x4B6341782E41B1C77809B5075667F64F9247DD75557B3D96FC6F5E1F02FD71DF, 0, NULL, 0, 0, 0, CAST(N'2020-05-29T21:17:02.070' AS DateTime))
GO
INSERT [auth].[userrole] ([uid], [date], [id_user], [id_role]) VALUES (N'344b8c87-327d-405a-b143-c0b7061b8a97', CAST(N'2020-06-23' AS Date), N'003d5f00-2d00-4ccf-ac8d-48dae2b16998', N'd679b0c2-5f98-487f-b709-eb46884a0b82')
GO
SET IDENTITY_INSERT [parameter].[page] ON 
GO
INSERT [parameter].[page] ([id], [title], [icon], [route]) VALUES (1, N'Home', N'fas fa-home', N'home')
GO
INSERT [parameter].[page] ([id], [title], [icon], [route]) VALUES (2, N'Usuarios', N'fas fa-user', N'users')
GO
SET IDENTITY_INSERT [parameter].[page] OFF
GO
SET IDENTITY_INSERT [parameter].[pagerole] ON 
GO
INSERT [parameter].[pagerole] ([id], [id_role], [id_page], [date]) VALUES (1, N'4c4095de-c86e-4144-8083-2b11ba18df1d', 1, CAST(N'2020-06-23T22:47:11.357' AS DateTime))
GO
INSERT [parameter].[pagerole] ([id], [id_role], [id_page], [date]) VALUES (2, N'b695acf7-9768-4743-b9a4-f5b190d8a19a', 1, CAST(N'2020-06-23T22:47:11.357' AS DateTime))
GO
INSERT [parameter].[pagerole] ([id], [id_role], [id_page], [date]) VALUES (3, N'b695acf7-9768-4743-b9a4-f5b190d8a19a', 2, CAST(N'2020-06-23T22:47:11.357' AS DateTime))
GO
INSERT [parameter].[pagerole] ([id], [id_role], [id_page], [date]) VALUES (4, N'b695acf7-9768-4743-b9a4-f5b190d8a19b', 1, CAST(N'2020-06-23T22:47:11.353' AS DateTime))
GO
INSERT [parameter].[pagerole] ([id], [id_role], [id_page], [date]) VALUES (5, N'b695acf7-9768-4743-b9a4-f5b190d8a19b', 2, CAST(N'2020-06-23T22:47:11.353' AS DateTime))
GO
INSERT [parameter].[pagerole] ([id], [id_role], [id_page], [date]) VALUES (6, N'd679b0c2-5f98-487f-b709-eb46884a0b82', 1, CAST(N'2020-06-23T22:47:11.357' AS DateTime))
GO
INSERT [parameter].[pagerole] ([id], [id_role], [id_page], [date]) VALUES (7, N'd679b0c2-5f98-487f-b709-eb46884a0b82', 2, CAST(N'2020-06-23T22:47:11.357' AS DateTime))
GO
SET IDENTITY_INSERT [parameter].[pagerole] OFF
GO
SET IDENTITY_INSERT [parameter].[pageroleaction] ON 
GO
INSERT [parameter].[pageroleaction] ([id], [id_pagerole], [id_action]) VALUES (1, 3, 4)
GO
INSERT [parameter].[pageroleaction] ([id], [id_pagerole], [id_action]) VALUES (2, 5, 2)
GO
INSERT [parameter].[pageroleaction] ([id], [id_pagerole], [id_action]) VALUES (3, 5, 4)
GO
INSERT [parameter].[pageroleaction] ([id], [id_pagerole], [id_action]) VALUES (4, 7, 1)
GO
INSERT [parameter].[pageroleaction] ([id], [id_pagerole], [id_action]) VALUES (5, 7, 2)
GO
INSERT [parameter].[pageroleaction] ([id], [id_pagerole], [id_action]) VALUES (6, 7, 3)
GO
INSERT [parameter].[pageroleaction] ([id], [id_pagerole], [id_action]) VALUES (7, 7, 4)
GO
SET IDENTITY_INSERT [parameter].[pageroleaction] OFF
GO
--Constraints
ALTER TABLE [auth].[role] ADD  CONSTRAINT [df_newguidrole]  DEFAULT (newid()) FOR [uid]
GO
ALTER TABLE [auth].[user] ADD  CONSTRAINT [df_newguiduser]  DEFAULT (newid()) FOR [uid]
GO
ALTER TABLE [auth].[user] ADD  CONSTRAINT [df_userdate]  DEFAULT (getdate()) FOR [date]
GO
ALTER TABLE [auth].[userrole] ADD  CONSTRAINT [df_newguiduserrole]  DEFAULT (newid()) FOR [uid]
GO
ALTER TABLE [parameter].[pagerole] ADD  CONSTRAINT [df_employeedate]  DEFAULT (getdate()) FOR [date]
GO
ALTER TABLE [auth].[userrole]  WITH CHECK ADD  CONSTRAINT [fk_userroletorole] FOREIGN KEY([id_role])
REFERENCES [auth].[role] ([uid])
GO
ALTER TABLE [auth].[userrole] CHECK CONSTRAINT [fk_userroletorole]
GO
ALTER TABLE [auth].[userrole]  WITH CHECK ADD  CONSTRAINT [fk_userroletouser] FOREIGN KEY([id_user])
REFERENCES [auth].[user] ([uid])
GO
ALTER TABLE [auth].[userrole] CHECK CONSTRAINT [fk_userroletouser]
GO
ALTER TABLE [parameter].[pagerole]  WITH CHECK ADD  CONSTRAINT [fk_pageroletopage] FOREIGN KEY([id_page])
REFERENCES [parameter].[page] ([id])
GO
ALTER TABLE [parameter].[pagerole] CHECK CONSTRAINT [fk_pageroletopage]
GO
ALTER TABLE [parameter].[pagerole]  WITH CHECK ADD  CONSTRAINT [fk_pageroletorole] FOREIGN KEY([id_role])
REFERENCES [auth].[role] ([uid])
GO
ALTER TABLE [parameter].[pagerole] CHECK CONSTRAINT [fk_pageroletorole]
GO
ALTER TABLE [parameter].[pageroleaction] ADD  CONSTRAINT [uq_pagerolaction] UNIQUE NONCLUSTERED 
(
	[id_pagerole] ASC,
	[id_action] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [parameter].[pageroleaction]  WITH CHECK ADD  CONSTRAINT [fk_pageroleactiontoaction] FOREIGN KEY([id_action])
REFERENCES [parameter].[action] ([id])
GO
ALTER TABLE [parameter].[pageroleaction] CHECK CONSTRAINT [fk_pageroleactiontoaction]
GO
ALTER TABLE [parameter].[pageroleaction]  WITH CHECK ADD  CONSTRAINT [fk_pageroleactiontopagerole] FOREIGN KEY([id_pagerole])
REFERENCES [parameter].[pagerole] ([id])
GO
ALTER TABLE [parameter].[pageroleaction] CHECK CONSTRAINT [fk_pageroleactiontopagerole]
GO
/****** Object:  StoredProcedure [parameter].[getpages]    Script Date: 14/09/2020 3:25:35 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [parameter].[getpages]
(
	@uid uniqueidentifier
)
AS
BEGIN
	select
		pr.id
		,pr.id_role
		,pr.id_page
		,p.title
		,p.icon
		,p.[route]
		,(select * from [parameter].[pageroleaction] pra where pra.id_pagerole = pr.id FOR JSON PATH) actions
	from
		parameter.[pagerole] pr
	inner join
		 parameter.[page] p on p.id = pr.[id_page]
	inner join
		auth.[role] r on pr.[id_role] = r.[uid]
	inner join
		auth.[userrole] ur on ur.[id_role] = r.[uid]
	inner join
		auth.[user] u on ur.[id_user] = u.[uid]
	where
		(@uid is null or (@uid is not null and (u.[uid] = @uid)))
END
GO

/****** Object:  StoredProcedure [parameter].[getpagesbyrole]    Script Date: 14/09/2020 3:25:35 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [parameter].[getpagesbyrole]
(
	@uid uniqueidentifier
)
as
begin
	with cruze as
	(
		select * from parameter.[page] p, auth.[role] r
	)
	select
		isnull(pr.id,cruze.id * -1) id
			,isnull(pr.id_role,cruze.uid) id_role
			,isnull(pr.id_page,cruze.id) id_page
			,cruze.title
			,cruze.icon
			,cruze.[route]
	from cruze
		left join parameter.[pagerole] pr on (pr.[id_role] = cruze.[uid] and pr.[id_page] = cruze.id)
	where
		(@uid is null or (@uid is not null and (cruze.[uid] = @uid)))
end
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [auth].[getuser]
(
	@username varchar(60) = null,
	@uid uniqueidentifier = null
)
as
begin
	
	select
		u.[uid]	
		,u.username
		,u.email
		,u.firstname
		,u.lastname
		,u.fullname
		,u.cel
		,u.[address]
		,u.salt
		,u.requirechangepass
		,u.confirmationcode
		,u.sayhi
		,u.emailconfirmed
		,u.[state]
		,u.[date]
		,r.[description]
		,r.[uid] id_role
	from 
		auth.[user] u
	inner join auth.userrole ur on u.[uid] = ur.id_user 
	inner join auth.[role] r on ur.id_role = r.[uid]
	where
		(@username is null or (@username is not null and (u.[username] = @username)))
	AND
		(@uid is null or (@uid is not null and (u.[uid] = @uid)))
end
GO