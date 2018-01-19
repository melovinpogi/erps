USE [freshlawn]
GO

/****** Object:  Table [dbo].[sys_email]    Script Date: 1/18/2018 8:50:12 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[sys_email](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[sender] [varchar](100) NULL,
	[recipient] [varchar](100) NULL,
	[subject] [varchar](255) NULL,
	[message] [varchar](max) NULL,
	[date_created] [datetime] NULL,
	[date_sent] [datetime] NULL,
	[status] [int] NOT NULL,
	[is_office] [bit] NOT NULL,
 CONSTRAINT [PK_sys_email] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[sys_email] ADD  DEFAULT ((0)) FOR [status]
GO

ALTER TABLE [dbo].[sys_email] ADD  DEFAULT ((0)) FOR [is_office]
GO


USE [freshlawn]
GO

/****** Object:  Table [dbo].[sys_email_default_recipient]    Script Date: 1/18/2018 8:50:25 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[sys_email_default_recipient](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[recipient] [varchar](100) NOT NULL,
 CONSTRAINT [PK_sys_email_default_recipient] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


