USE [brickcom]
GO
/****** Object:  Table [dbo].[quiz_t]    Script Date: 07/23/2014 07:26:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[quiz_t](
	[qid] [int] IDENTITY(1,1) NOT NULL,
	[question] [nvarchar](2000) NOT NULL,
	[op1] [nvarchar](1000) NULL,
	[op2] [nvarchar](1000) NULL,
	[op3] [nvarchar](1000) NULL,
	[op4] [nvarchar](1000) NULL,
	[type] [int] NOT NULL,
	[seq] [int] NULL,
	[answer] [int] NULL,
	[tag] [nvarchar](50) NULL,
	[created] [datetime] NULL,
	[lastmodified] [datetime] NULL,
	[active] [bit] NOT NULL,
 CONSTRAINT [PK_quiz_t] PRIMARY KEY CLUSTERED 
(
	[qid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[organ_t]    Script Date: 07/23/2014 07:26:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[organ_t](
	[oid] [int] IDENTITY(1,1) NOT NULL,
	[orgname] [nvarchar](50) NULL,
	[orglevel] [int] NULL,
	[parentid] [int] NULL,
	[created] [datetime] NULL,
	[lastmodified] [datetime] NULL,
	[active] [bit] NULL,
 CONSTRAINT [PK_ORGAN_T] PRIMARY KEY CLUSTERED 
(
	[oid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[module_t]    Script Date: 07/23/2014 07:26:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[module_t](
	[mid] [int] IDENTITY(1,1) NOT NULL,
	[mname] [nvarchar](50) NULL,
	[mtag] [varchar](50) NULL,
	[parentid] [int] NULL,
	[enable] [varchar](50) NULL,
	[enable_expr] [varchar](200) NULL,
	[created] [datetime] NULL,
	[lastmodified] [datetime] NULL,
	[active] [bit] NULL,
 CONSTRAINT [PK_MODULE_T] PRIMARY KEY CLUSTERED 
(
	[mid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[document_t]    Script Date: 07/23/2014 07:26:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[document_t](
	[did] [int] IDENTITY(1,1) NOT NULL,
	[dname] [nvarchar](50) NOT NULL,
	[comment] [nvarchar](200) NULL,
	[media] [nvarchar](512) NOT NULL,
	[created] [datetime] NOT NULL,
	[lastmodified] [datetime] NOT NULL,
 CONSTRAINT [PK_document_t] PRIMARY KEY CLUSTERED 
(
	[did] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[course_video_t]    Script Date: 07/23/2014 07:26:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[course_video_t](
	[cid] [int] NOT NULL,
	[vid] [int] NOT NULL,
 CONSTRAINT [PK_course_video_t] PRIMARY KEY CLUSTERED 
(
	[cid] ASC,
	[vid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[course_t]    Script Date: 07/23/2014 07:26:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[course_t](
	[cid] [int] IDENTITY(1,1) NOT NULL,
	[cname] [nvarchar](50) NOT NULL,
	[category] [nvarchar](50) NULL,
	[rank] [int] NOT NULL,
	[limit] [int] NULL,
	[type] [char](10) NOT NULL,
	[media] [varchar](50) NULL,
	[created] [datetime] NULL,
	[lastmodified] [datetime] NULL,
	[active] [bit] NOT NULL,
 CONSTRAINT [PK_course_t] PRIMARY KEY CLUSTERED 
(
	[cid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[course_quiz_t]    Script Date: 07/23/2014 07:26:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[course_quiz_t](
	[cid] [int] NOT NULL,
	[qid] [int] NOT NULL,
	[pageno] [int] NOT NULL,
 CONSTRAINT [PK_course_quiz_t] PRIMARY KEY CLUSTERED 
(
	[cid] ASC,
	[qid] ASC,
	[pageno] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[course_document_t]    Script Date: 07/23/2014 07:26:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[course_document_t](
	[cid] [int] NOT NULL,
	[did] [int] NOT NULL,
 CONSTRAINT [PK_course_document_t] PRIMARY KEY CLUSTERED 
(
	[cid] ASC,
	[did] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[video_t]    Script Date: 07/23/2014 07:26:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[video_t](
	[vid] [int] IDENTITY(1,1) NOT NULL,
	[vname] [nvarchar](50) NOT NULL,
	[comment] [nvarchar](200) NULL,
	[media] [nvarchar](512) NOT NULL,
	[created] [datetime] NOT NULL,
	[lastmodified] [datetime] NOT NULL,
 CONSTRAINT [PK_video_t] PRIMARY KEY CLUSTERED 
(
	[vid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[score_t]    Script Date: 07/23/2014 07:26:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[score_t](
	[pid] [int] IDENTITY(1,1) NOT NULL,
	[uid] [int] NOT NULL,
	[cid] [int] NOT NULL,
	[score] [decimal](8, 2) NOT NULL,
	[prescore] [decimal](8, 2) NOT NULL,
	[created] [datetime] NULL,
	[lastmodified] [datetime] NULL,
 CONSTRAINT [PK_score_t] PRIMARY KEY CLUSTERED 
(
	[pid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[score_detail_t]    Script Date: 07/23/2014 07:26:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[score_detail_t](
	[sdid] [int] IDENTITY(1,1) NOT NULL,
	[wronglist] [varchar](2000) NULL,
	[correctlist] [varchar](2000) NULL,
 CONSTRAINT [PK_score_detail_t] PRIMARY KEY CLUSTERED 
(
	[sdid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[role_t]    Script Date: 07/23/2014 07:26:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[role_t](
	[rid] [int] NOT NULL,
	[rtag] [varchar](50) NULL,
	[rname] [nvarchar](50) NULL,
	[comment] [nvarchar](255) NULL,
	[created] [datetime] NULL,
	[lastmodified] [datetime] NULL,
	[active] [bit] NULL,
 CONSTRAINT [PK_ROLE_T] PRIMARY KEY CLUSTERED 
(
	[rid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[role_module_t]    Script Date: 07/23/2014 07:26:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[role_module_t](
	[rid] [int] NULL,
	[mid] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user_t]    Script Date: 07/23/2014 07:26:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[user_t](
	[uid] [int] IDENTITY(1,1) NOT NULL,
	[ucode] [varchar](50) NULL,
	[uname] [nvarchar](50) NULL,
	[upass] [varchar](32) NULL,
	[ucard] [varchar](50) NULL,
	[utag] [varchar](50) NULL,
	[email] [varchar](50) NULL,
	[mobile] [varchar](50) NULL,
	[im] [varchar](50) NULL,
	[bday] [datetime] NULL,
	[oid] [int] NULL,
	[comment] [nvarchar](255) NULL,
	[created] [datetime] NULL,
	[lastmodified] [datetime] NULL,
	[active] [bit] NULL,
 CONSTRAINT [PK_USER_T] PRIMARY KEY CLUSTERED 
(
	[uid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[user_role_t]    Script Date: 07/23/2014 07:26:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user_role_t](
	[uid] [int] NOT NULL,
	[rid] [int] NOT NULL,
 CONSTRAINT [PK_USER_ROLE_T] PRIMARY KEY CLUSTERED 
(
	[uid] ASC,
	[rid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Default [DF_course_t_active]    Script Date: 07/23/2014 07:26:19 ******/
ALTER TABLE [dbo].[course_t] ADD  CONSTRAINT [DF_course_t_active]  DEFAULT ((1)) FOR [active]
GO
/****** Object:  Default [DF_quiz_t_type]    Script Date: 07/23/2014 07:26:19 ******/
ALTER TABLE [dbo].[quiz_t] ADD  CONSTRAINT [DF_quiz_t_type]  DEFAULT ((1)) FOR [type]
GO
/****** Object:  Default [DF_quiz_t_answer]    Script Date: 07/23/2014 07:26:19 ******/
ALTER TABLE [dbo].[quiz_t] ADD  CONSTRAINT [DF_quiz_t_answer]  DEFAULT ((0)) FOR [answer]
GO
/****** Object:  Default [DF_quiz_t_active]    Script Date: 07/23/2014 07:26:19 ******/
ALTER TABLE [dbo].[quiz_t] ADD  CONSTRAINT [DF_quiz_t_active]  DEFAULT ((1)) FOR [active]
GO
/****** Object:  Default [DF_score_t_score]    Script Date: 07/23/2014 07:26:19 ******/
ALTER TABLE [dbo].[score_t] ADD  CONSTRAINT [DF_score_t_score]  DEFAULT ((0.0)) FOR [score]
GO
/****** Object:  Default [DF_score_t_prescore]    Script Date: 07/23/2014 07:26:19 ******/
ALTER TABLE [dbo].[score_t] ADD  CONSTRAINT [DF_score_t_prescore]  DEFAULT ((0.0)) FOR [prescore]
GO
/****** Object:  Default [DF_video_t_created]    Script Date: 07/23/2014 07:26:19 ******/
ALTER TABLE [dbo].[video_t] ADD  CONSTRAINT [DF_video_t_created]  DEFAULT (getdate()) FOR [created]
GO
/****** Object:  Default [DF_video_t_lastmodified]    Script Date: 07/23/2014 07:26:19 ******/
ALTER TABLE [dbo].[video_t] ADD  CONSTRAINT [DF_video_t_lastmodified]  DEFAULT (getdate()) FOR [lastmodified]
GO
/****** Object:  ForeignKey [FK_ROLE_MOD_REFERENCE_MODULE_T]    Script Date: 07/23/2014 07:26:19 ******/
ALTER TABLE [dbo].[role_module_t]  WITH CHECK ADD  CONSTRAINT [FK_ROLE_MOD_REFERENCE_MODULE_T] FOREIGN KEY([mid])
REFERENCES [dbo].[module_t] ([mid])
GO
ALTER TABLE [dbo].[role_module_t] CHECK CONSTRAINT [FK_ROLE_MOD_REFERENCE_MODULE_T]
GO
/****** Object:  ForeignKey [FK_ROLE_MOD_REFERENCE_ROLE_T]    Script Date: 07/23/2014 07:26:19 ******/
ALTER TABLE [dbo].[role_module_t]  WITH CHECK ADD  CONSTRAINT [FK_ROLE_MOD_REFERENCE_ROLE_T] FOREIGN KEY([rid])
REFERENCES [dbo].[role_t] ([rid])
GO
ALTER TABLE [dbo].[role_module_t] CHECK CONSTRAINT [FK_ROLE_MOD_REFERENCE_ROLE_T]
GO
/****** Object:  ForeignKey [FK_USER_ROL_REFERENCE_ROLE_T]    Script Date: 07/23/2014 07:26:19 ******/
ALTER TABLE [dbo].[user_role_t]  WITH CHECK ADD  CONSTRAINT [FK_USER_ROL_REFERENCE_ROLE_T] FOREIGN KEY([rid])
REFERENCES [dbo].[role_t] ([rid])
GO
ALTER TABLE [dbo].[user_role_t] CHECK CONSTRAINT [FK_USER_ROL_REFERENCE_ROLE_T]
GO
/****** Object:  ForeignKey [FK_USER_ROL_REFERENCE_USER_T]    Script Date: 07/23/2014 07:26:19 ******/
ALTER TABLE [dbo].[user_role_t]  WITH CHECK ADD  CONSTRAINT [FK_USER_ROL_REFERENCE_USER_T] FOREIGN KEY([uid])
REFERENCES [dbo].[user_t] ([uid])
GO
ALTER TABLE [dbo].[user_role_t] CHECK CONSTRAINT [FK_USER_ROL_REFERENCE_USER_T]
GO
/****** Object:  ForeignKey [FK_USER_T_REFERENCE_ORGAN_T]    Script Date: 07/23/2014 07:26:19 ******/
ALTER TABLE [dbo].[user_t]  WITH CHECK ADD  CONSTRAINT [FK_USER_T_REFERENCE_ORGAN_T] FOREIGN KEY([oid])
REFERENCES [dbo].[organ_t] ([oid])
GO
ALTER TABLE [dbo].[user_t] CHECK CONSTRAINT [FK_USER_T_REFERENCE_ORGAN_T]
GO
