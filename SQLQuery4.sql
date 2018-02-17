USE [freshlawn]
GO

/****** Object:  Table [dbo].[sys_chat_crew]    Script Date: 2/16/2018 10:24:02 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[sys_chat_crew](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[user_from] [bigint] NOT NULL,
	[user_to] [bigint] NOT NULL,
	[user_type_from] [varchar](50) NULL,
	[user_type_to] [varchar](50) NULL,
	[msg] [varchar](max) NULL,
	[msg_attachment] [varbinary](max) NULL,
	[date_sent] [datetime] NULL,
	[dispatch_id] [bigint] NOT NULL,
	[msg_type] [varchar](50) NULL,
 CONSTRAINT [PK_sys_chat_crew] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


