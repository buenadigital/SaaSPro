USE [SaaSPro]
GO
/****** Object:  Table [dbo].[ApiSessionTokens]    Script Date: 8/30/2016 9:07:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApiSessionTokens](
	[Id] [uniqueidentifier] NOT NULL,
	[Token] [nvarchar](255) NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[QuestionAnswered] [bit] NOT NULL,
	[ExpirationDate] [datetime] NOT NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[UpdatedOn] [datetime] NULL,
	[SecurityQuestionId] [uniqueidentifier] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[ApiTokens]    Script Date: 8/30/2016 9:07:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApiTokens](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Token] [nvarchar](255) NOT NULL,
	[CustomerId] [uniqueidentifier] NOT NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK__ApiToken__3214EC077F60ED59] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[AuditLog]    Script Date: 8/30/2016 9:07:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AuditLog](
	[Id] [uniqueidentifier] NOT NULL,
	[TimeStamp] [datetime] NOT NULL,
	[Username] [nvarchar](255) NULL,
	[EntityType] [nvarchar](255) NOT NULL,
	[EntityId] [uniqueidentifier] NOT NULL,
	[Action] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CustomerId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK__AuditLog__3214EC0703317E3D] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[CustomerPaymentRefunds]    Script Date: 8/30/2016 9:07:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerPaymentRefunds](
	[Id] [uniqueidentifier] NOT NULL,
	[CustomerId] [uniqueidentifier] NOT NULL,
	[TransactionId] [nvarchar](50) NOT NULL,
	[ChargeId] [nvarchar](50) NOT NULL,
	[Amount] [money] NOT NULL,
	[Created] [datetime] NOT NULL,
 CONSTRAINT [PK_CustomerPaymentRefunds] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[CustomerPayments]    Script Date: 8/30/2016 9:07:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerPayments](
	[Id] [uniqueidentifier] NOT NULL,
	[CustomerId] [uniqueidentifier] NOT NULL,
	[TransactionId] [nvarchar](50) NOT NULL,
	[Amount] [money] NOT NULL,
	[Refunded] [bit] NOT NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_CustomerPayments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[Customers]    Script Date: 8/30/2016 9:07:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[Id] [uniqueidentifier] NOT NULL,
	[FullName] [nvarchar](50) NOT NULL,
	[Hostname] [nvarchar](128) NOT NULL,
	[Company] [nvarchar](50) NULL,
	[Enabled] [bit] NOT NULL,
	[EncryptionKey] [nvarchar](255) NOT NULL,
	[PaymentCustomerId] [nvarchar](64) NULL,
	[PlanId] [uniqueidentifier] NULL,
	[AdminUserId] [uniqueidentifier] NULL,
	[PlanCreatedOn] [datetime] NULL,
	[PlanUpdatedOn] [datetime] NULL,
	[PlanCanceledOn] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_dbo.CustomerDescriptions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[EmailTemplates]    Script Date: 8/30/2016 9:07:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmailTemplates](
	[Id] [uniqueidentifier] NOT NULL,
	[TemplateName] [nvarchar](50) NOT NULL,
	[Template] [text] NULL,
	[FromEmail] [nvarchar](100) NULL,
	[Subject] [nvarchar](max) NULL,
	[Body] [text] NULL,
	[TimeStamp] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_EmailTemplates] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[IPS]    Script Date: 8/30/2016 9:07:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[IPS](
	[Id] [uniqueidentifier] NOT NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
	[Name] [nvarchar](255) NULL,
	[StartBytes] [varbinary](max) NOT NULL,
	[EndBytes] [varbinary](max) NOT NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[CustomerId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK__IPS__3214EC077279D204] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Log]    Script Date: 8/30/2016 9:07:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Log](
	[LogId] [uniqueidentifier] NOT NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Thread] [varchar](255) NOT NULL,
	[Level] [varchar](50) NOT NULL,
	[Logger] [varchar](255) NOT NULL,
	[Message] [varchar](4000) NOT NULL,
	[Exception] [varchar](2000) NULL,
 CONSTRAINT [PK_Log] PRIMARY KEY CLUSTERED 
(
	[LogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Notes]    Script Date: 8/30/2016 9:07:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notes](
	[Id] [uniqueidentifier] NOT NULL,
	[CustomerId] [uniqueidentifier] NOT NULL,
	[Note] [text] NOT NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK__Notes__3214EC071367E606] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[NotificationMessages]    Script Date: 8/30/2016 9:07:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NotificationMessages](
	[Id] [uniqueidentifier] NOT NULL,
	[NotIFicationType] [nvarchar](255) NULL,
	[Subject] [nvarchar](255) NULL,
	[Body] [nvarchar](255) NULL,
	[ReferenceId] [uniqueidentifier] NULL,
	[SenderId] [uniqueidentifier] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK__tmp_ms_x__3214EC0766603565] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[PlanInfo]    Script Date: 8/30/2016 9:07:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlanInfo](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](64) NOT NULL,
	[OrderIndex] [int] NOT NULL,
 CONSTRAINT [PK_PlanInfoItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[PlanInfoValues]    Script Date: 8/30/2016 9:07:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlanInfoValues](
	[PlanId] [uniqueidentifier] NOT NULL,
	[PlanInfoItemId] [uniqueidentifier] NOT NULL,
	[Value] [nvarchar](64) NOT NULL,
 CONSTRAINT [PK_PlanInfoItemValues] PRIMARY KEY CLUSTERED 
(
	[PlanId] ASC,
	[PlanInfoItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[Plans]    Script Date: 8/30/2016 9:07:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Plans](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](64) NOT NULL,
	[Price] [money] NOT NULL,
	[Period] [nvarchar](50) NOT NULL,
	[OrderIndex] [int] NOT NULL,
	[PlanCode] [nvarchar](64) NULL,
	[Enabled] [bit] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_Plans_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[Projects]    Script Date: 8/30/2016 9:07:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Projects](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[CustomerId] [uniqueidentifier] NOT NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_Projects] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[ReferenceListItems]    Script Date: 8/30/2016 9:07:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReferenceListItems](
	[Id] [uniqueidentifier] NOT NULL,
	[Value] [nvarchar](255) NOT NULL,
	[ReferenceListId] [uniqueidentifier] NULL,
	[CustomerId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK__Referenc__3214EC0722AA2996] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[ReferenceLists]    Script Date: 8/30/2016 9:07:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReferenceLists](
	[Id] [uniqueidentifier] NOT NULL,
	[SystemName] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK__Referenc__3214EC07267ABA7A] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[Roles]    Script Date: 8/30/2016 9:07:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](255) NULL,
	[UserType] [nvarchar](255) NULL,
	[SystemRole] [bit] NULL,
	[CustomerId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK__Roles__3214EC072D27B809] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[RoleUsers]    Script Date: 8/30/2016 9:07:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleUsers](
	[RoleId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK__RoleUser__AF2760AD30F848ED] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[UserNotifications]    Script Date: 8/30/2016 9:07:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserNotifications](
	[UserId] [uniqueidentifier] NOT NULL,
	[MessageId] [uniqueidentifier] NOT NULL,
	[HasRead] [bit] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK__UserNoti__CB0F0C8534C8D9D1] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[MessageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[Users]    Script Date: 8/30/2016 9:07:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [uniqueidentifier] NOT NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
	[UserType] [nvarchar](255) NOT NULL,
	[Email] [nvarchar](255) NOT NULL,
	[FirstName] [nvarchar](255) NOT NULL,
	[LastName] [nvarchar](255) NOT NULL,
	[Password] [nvarchar](255) NOT NULL,
	[PasswordExpiryDate] [datetime] NOT NULL,
	[RegisteredDate] [datetime] NOT NULL,
	[Enabled] [bit] NOT NULL,
	[LastLoginIP] [nvarchar](255) NULL,
	[LastLoginDate] [datetime] NULL,
	[PasswordResetToken] [nvarchar](255) NULL,
	[PasswordResetTokenExpiry] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[CustomerId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK__Users__3214EC0738996AB5] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[UserSecurityQuestions]    Script Date: 8/30/2016 9:07:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserSecurityQuestions](
	[Id] [uniqueidentifier] NOT NULL,
	[Question] [nvarchar](255) NULL,
	[Answer] [nvarchar](255) NULL,
	[UserId] [uniqueidentifier] NULL,
 CONSTRAINT [PK__UserSecu__3214EC073C69FB99] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
INSERT [dbo].[EmailTemplates] ([Id], [TemplateName], [Template], [FromEmail], [Subject], [Body], [TimeStamp], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'79e87988-4fab-455e-b82a-096d80e3e90e', N'Contact Request', NULL, N'support@saaspro.net', N'Contact Form Request', N'<table class="wrapper" style="width: 100%; table-layout: fixed;">
    <tbody>
        <tr>
            <td style="padding: 0px; vertical-align: top;">                <center>                    <div class="email-top" style="font-size: 54px; line-height: 54px;">&nbsp;</div>
                    <table class="header" style="margin-left: auto; margin-right: auto; width: 560px;">
                        <tbody>
                            <tr>
                                <td align="left" style="padding: 0px; vertical-align: top; color: rgb(170, 170, 170); font-size: 24px; font-family: Georgia, serif;">                                    <table>
                                        <tbody>
                                            <tr>
                                                <td align="left" class="logo" id="emb-email-header" style="padding: 0px 0px 27px; vertical-align: top; color: rgb(32, 32, 32); font-size: 32px; font-weight: bold; line-height: 42px;"><a style="color: rgb(32, 32, 32);" href="http://preview.createsend1.com/t/t-l-jjlhjyy-l-r/"></a></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <img src="https://demo.saaspro.net/images/saaspro-logo.png" style="width: 100px;">                                </td>
                            </tr>
                        </tbody>
                    </table>
                </center>            </td>
        </tr>
    </tbody>
</table>
<table class="wrapper" style="width: 100%; table-layout: fixed;">
    <tbody>
        <tr>
            <td style="padding: 0px; vertical-align: top;">                <center>                    <table class="one-col" style="margin-left: auto; margin-right: auto;">
                        <tbody>
                            <tr>
                                <td class="column" style="padding: 0px; vertical-align: top; color: rgb(53, 54, 56);">                                    <div>                                        <div class="column-top" style="font-size: 20px;">&nbsp;</div>
                                    </div>
                                    <table class="contents">
                                        <tbody>
                                            <tr>
                                                <td class="padded" style="padding: 0px 20px; vertical-align: top;">                                                    <h1 style="margin-top: 0px; margin-bottom: 24px; font-size: 32px; font-family: Georgia, serif; line-height: 42px;">Hi, <br><br>##Email## Wants to Say Hello!</h1>
                                                    <p style="margin-bottom: 27px; letter-spacing: -0.01em; font-family: Georgia, serif; -webkit-font-smoothing: antialiased; text-rendering: optimizelegibility; font-size: 17px; line-height: 27px;">##Message##</p>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </center>            </td>
        </tr>
    </tbody>
</table>', CAST(0x0000A3C400000000 AS DateTime), NULL, CAST(0x0000A4ED017F3400 AS DateTime), NULL, CAST(0x0000A5910173B967 AS DateTime))
INSERT [dbo].[EmailTemplates] ([Id], [TemplateName], [Template], [FromEmail], [Subject], [Body], [TimeStamp], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'33248f4f-a376-4166-9383-54958f9333c1', N'Customer Subscription Updated', NULL, N'support@saaspro.net', N'Customer Subscription Updated', N'<table class="wrapper" style="width: 100%; table-layout: fixed;">    <tbody>        <tr>            <td style="padding: 0px; vertical-align: top;">                <center> <table class="one-col" style="margin-left: auto; margin-right: auto; width: 600px;">                        <tbody>                            <tr>                                <td class="column" style="padding: 0px; vertical-align: top; color: rgb(53, 54, 56);">                                    <div>                                        <div class="column-top" style="font-size: 20px;">&nbsp;</div>                                    </div>                                    <img src="https://demo.saaspro.net/images/saaspro-logo.png" style="width: 100px;"><div>                                        <div class="column-top" style="font-size: 20px;"><br></div>                                    </div>                                    <table class="contents" style="width: 600px;">                                        <tbody>                                            <tr>                                                <td class="padded" style="padding: 0px 20px; vertical-align: top;">                                                    <h1 style="margin-top: 0px; margin-bottom: 24px; font-size: 32px; font-family: Georgia, serif; line-height: 42px;">Hi #Customer.AdminUser.FistName##,&nbsp;<br><span style="color: inherit; background-color: transparent;"><br>Customer Subscription Updated</span><br></h1>                                                    <p style="margin-bottom: 27px; letter-spacing: -0.01em; font-family: Georgia, serif; -webkit-font-smoothing: antialiased; text-rendering: optimizelegibility; font-size: 17px; line-height: 27px;">Ipsum lorem, ipsem lorem.</p>                                                    <p style="margin-bottom: 27px; letter-spacing: -0.01em; font-family: Georgia, serif; -webkit-font-smoothing: antialiased; text-rendering: optimizelegibility; font-size: 17px; line-height: 27px;">                                                        <span style="font-weight: bold;">URL</span>: ##Customer.HostName## <br>                                                        <span style="font-weight: bold;">Plan</span>:&nbsp;##Customer.Plan.Name## <br>                                                        <span style="font-weight: bold;">Username</span>: ##Customer.AdminUser.Email## <br>                                                    </p>                                                </td>                                            </tr>                                        </tbody>                                    </table>                                    <div class="column-bottom" style="font-size: 3px; line-height: 3px;">&nbsp;</div>                                </td>                            </tr>                        </tbody>                    </table> </center>            </td>        </tr>    </tbody></table>', CAST(0x0000A4E400000000 AS DateTime), NULL, CAST(0x0000A4E50057BAFC AS DateTime), NULL, CAST(0x0000A59101731832 AS DateTime))
INSERT [dbo].[EmailTemplates] ([Id], [TemplateName], [Template], [FromEmail], [Subject], [Body], [TimeStamp], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'70723854-4dce-4f02-b8d2-5ea6e4f4f5ab', N'Charge Refunded', NULL, N'support@saaspro.net', N'Charge Refunded', N'<table class="wrapper" style="width: 100%; table-layout: fixed;">    <tbody>        <tr>            <td style="padding: 0px; vertical-align: top;">                <center> <table class="one-col" style="margin-left: auto; margin-right: auto; width: 600px;">                        <tbody>                            <tr>                                <td class="column" style="padding: 0px; vertical-align: top; color: rgb(53, 54, 56);">                                    <div>                                        <div class="column-top" style="font-size: 20px;">&nbsp;</div>                                    </div>                                    <img src="https://demo.saaspro.net/images/saaspro-logo.png" style="width: 100px;"><div>                                        <div class="column-top" style="font-size: 20px;"><br></div>                                    </div>                                    <table class="contents" style="width: 600px;">                                        <tbody>                                            <tr>                                                <td class="padded" style="padding: 0px 20px; vertical-align: top;">                                                    <h1 style="margin-top: 0px; margin-bottom: 24px; font-size: 32px; font-family: Georgia, serif; line-height: 42px;">Hi #Customer.AdminUser.FistName##,&nbsp;<br><span style="color: inherit; background-color: transparent;"><br>Customer Subscription Charge Refund</span><br></h1>                                                    <p style="margin-bottom: 27px; letter-spacing: -0.01em; font-family: Georgia, serif; -webkit-font-smoothing: antialiased; text-rendering: optimizelegibility; font-size: 17px; line-height: 27px;">Ipsum lorem, ipsem lorem.</p>                                                    <p style="margin-bottom: 27px; letter-spacing: -0.01em; font-family: Georgia, serif; -webkit-font-smoothing: antialiased; text-rendering: optimizelegibility; font-size: 17px; line-height: 27px;">                                                        <span style="font-weight: bold;">URL</span>: ##Customer.HostName## <br>                                                        <span style="font-weight: bold;">Plan</span>:&nbsp;##Customer.Plan.Name## <br>                                                        <span style="font-weight: bold;">Username</span>: ##Customer.AdminUser.Email## <br>                                                    </p>                                                </td>                                            </tr>                                        </tbody>                                    </table>                                    <div class="column-bottom" style="font-size: 3px; line-height: 3px;">&nbsp;</div>                                </td>                            </tr>                        </tbody>                    </table> </center>            </td>        </tr>    </tbody></table>', CAST(0x0000A3C400000000 AS DateTime), NULL, CAST(0x0000A4BA00BD144C AS DateTime), NULL, CAST(0x0000A59101743419 AS DateTime))
INSERT [dbo].[EmailTemplates] ([Id], [TemplateName], [Template], [FromEmail], [Subject], [Body], [TimeStamp], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'50f70271-9948-4fde-9a24-6b6712efa445', N'Payment', NULL, N'support@saaspro.net', N'Payment Received', N'<table class="wrapper" style="width: 100%; table-layout: fixed;">
    <tbody>
        <tr>
            <td style="padding: 0px; vertical-align: top;">
                <center>
                    <div class="email-top" style="font-size: 54px; line-height: 54px;">&nbsp;</div>
                    <table class="header" style="margin-left: auto; margin-right: auto; width: 560px;">
                        <tbody>
                            <tr>
                                <td align="left" style="padding: 0px; vertical-align: top; color: rgb(170, 170, 170); font-size: 24px; font-family: Georgia, serif;">
                                    <table>
                                        <tbody>
                                            <tr>
                                                <td align="left" class="logo" id="emb-email-header" style="padding: 0px 0px 27px; vertical-align: top; color: rgb(32, 32, 32); font-size: 32px; font-weight: bold; line-height: 42px;"><a style="color: rgb(32, 32, 32);" href="http://preview.createsend1.com/t/t-l-jjlhjyy-l-r/"></a></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <img src="https://demo.saaspro.net/images/saaspro-logo.png" style="width: 100px;">
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </center>
            </td>
        </tr>
    </tbody>
</table>
<table class="wrapper" style="width: 100%; table-layout: fixed;">
    <tbody>
        <tr>
            <td style="padding: 0px; vertical-align: top;">
                <center>
                    <table class="one-col" style="margin-left: auto; margin-right: auto; width: 600px;">
                        <tbody>
                            <tr>
                                <td class="column" style="padding: 0px; vertical-align: top; color: rgb(53, 54, 56);">
                                    <div>
                                        <div class="column-top" style="font-size: 20px;">&nbsp;</div>
                                    </div>
                                    <table class="contents" style="width: 600px;">
                                        <tbody>
                                            <tr>
                                                <td class="padded" style="padding: 0px 20px; vertical-align: top;">
                                                    <h1 style="margin-top: 0px; margin-bottom: 24px; font-size: 32px; font-family: Georgia, serif; line-height: 42px;">Payment Received</h1>
                                                    <p style="margin-bottom: 27px; letter-spacing: -0.01em; font-family: Georgia, serif; -webkit-font-smoothing: antialiased; text-rendering: optimizelegibility; font-size: 17px; line-height: 27px;">Ipsum lorem......</p>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <div class="column-bottom" style="font-size: 3px; line-height: 3px;">&nbsp;</div>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </center>
            </td>
        </tr>
    </tbody>
</table>', CAST(0x0000A3C400000000 AS DateTime), NULL, CAST(0x0000A4BA00BD4908 AS DateTime), NULL, CAST(0x0000A59101706659 AS DateTime))
INSERT [dbo].[EmailTemplates] ([Id], [TemplateName], [Template], [FromEmail], [Subject], [Body], [TimeStamp], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'c79bf7ce-6f09-472e-b24e-893fd8e0ecbf', N'Customer Subscription Deleted', NULL, N'support@saaspro.net', N'Customer Subscription Deleted', N'<table class="wrapper" style="width: 100%; table-layout: fixed;">    <tbody>        <tr>            <td style="padding: 0px; vertical-align: top;">                <center> <table class="one-col" style="margin-left: auto; margin-right: auto; width: 600px;">                        <tbody>                            <tr>                                <td class="column" style="padding: 0px; vertical-align: top; color: rgb(53, 54, 56);">                                    <div>                                        <div class="column-top" style="font-size: 20px;">&nbsp;</div>                                    </div>                                    <img src="https://demo.saaspro.net/images/saaspro-logo.png" style="width: 100px;"><div>                                        <div class="column-top" style="font-size: 20px;"><br></div>                                    </div>                                    <table class="contents" style="width: 600px;">                                        <tbody>                                            <tr>                                                <td class="padded" style="padding: 0px 20px; vertical-align: top;">                                                    <h1 style="margin-top: 0px; margin-bottom: 24px; font-size: 32px; font-family: Georgia, serif; line-height: 42px;">Hi #Customer.AdminUser.FistName##,&nbsp;<br><span style="color: inherit; background-color: transparent;"><br>Customer Subscription Deleted</span><br></h1>                                                    <p style="margin-bottom: 27px; letter-spacing: -0.01em; font-family: Georgia, serif; -webkit-font-smoothing: antialiased; text-rendering: optimizelegibility; font-size: 17px; line-height: 27px;">Ipsum lorem, ipsem lorem.</p>                                                    <p style="margin-bottom: 27px; letter-spacing: -0.01em; font-family: Georgia, serif; -webkit-font-smoothing: antialiased; text-rendering: optimizelegibility; font-size: 17px; line-height: 27px;">                                                        <span style="font-weight: bold;">URL</span>: ##Customer.HostName## <br>                                                        <span style="font-weight: bold;">Plan</span>:&nbsp;##Customer.Plan.Name## <br>                                                        <span style="font-weight: bold;">Username</span>: ##Customer.AdminUser.Email## <br>                                                    </p>                                                </td>                                            </tr>                                        </tbody>                                    </table>                                    <div class="column-bottom" style="font-size: 3px; line-height: 3px;">&nbsp;</div>                                </td>                            </tr>                        </tbody>                    </table> </center>            </td>        </tr>    </tbody></table>', CAST(0x0000A3C400000000 AS DateTime), NULL, CAST(0x0000A4BA00BD25E0 AS DateTime), NULL, CAST(0x0000A591017352B2 AS DateTime))
INSERT [dbo].[EmailTemplates] ([Id], [TemplateName], [Template], [FromEmail], [Subject], [Body], [TimeStamp], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'96ba2620-f005-41a6-b7af-d4cdb0e5c04d', N'Sign Up Greeting', NULL, N'support@saaspro.net', N'Welcome to SaaSPro!!', N'<table class="wrapper" style="width: 100%; table-layout: fixed;">
    <tbody>
        <tr>
            <td style="padding: 0px; vertical-align: top;">
                <center> <div class="email-top" style="font-size: 54px; line-height: 54px;"><br></div>
                    <table class="header" style="margin-left: auto; margin-right: auto; width: 560px;">
                        <tbody>
                            <tr>
                                <td align="left" style="padding: 0px; vertical-align: top; color: rgb(170, 170, 170); font-size: 24px; font-family: Georgia, serif;"></td>
                            </tr>
                        </tbody>
                    </table> </center>
            </td>
        </tr>
    </tbody>
</table>
<table class="wrapper" style="width: 100%; table-layout: fixed;">
    <tbody>
        <tr>
            <td style="padding: 0px; vertical-align: top;">
                <center> <table class="one-col" style="margin-left: auto; margin-right: auto; width: 600px;">
                        <tbody>
                            <tr>
                                <td class="column" style="padding: 0px; vertical-align: top; color: rgb(53, 54, 56);">
                                    <div>
                                        <div class="column-top" style="font-size: 20px;">&nbsp;</div>
                                    </div>
                                    <img src="https://demo.saaspro.net/images/saaspro-logo.png" style="width: 100px;"><div>
                                        <div class="column-top" style="font-size: 20px;"><br></div>
                                    </div>
                                    <table class="contents" style="width: 600px;">
                                        <tbody>
                                            <tr>
                                                <td class="padded" style="padding: 0px 20px; vertical-align: top;">
                                                    <h1 style="margin-top: 0px; margin-bottom: 24px; font-size: 32px; font-family: Georgia, serif; line-height: 42px;">Hi ##Customer.AdminUser.FistName##, <br>                                                        <br>Welcome to SaaSPro!</h1>
                                                    <p style="margin-bottom: 27px; letter-spacing: -0.01em; font-family: Georgia, serif; -webkit-font-smoothing: antialiased; text-rendering: optimizelegibility; font-size: 17px; line-height: 27px;">Ipsum lorem, ipsem lorem.</p>
                                                    <p style="margin-bottom: 27px; letter-spacing: -0.01em; font-family: Georgia, serif; -webkit-font-smoothing: antialiased; text-rendering: optimizelegibility; font-size: 17px; line-height: 27px;">                                                        <span style="font-weight: bold;">URL</span>: ##Customer.HostName## <br>                                                        <span style="font-weight: bold;">Plan</span>:&nbsp;##Customer.Plan.Name## <br>                                                        <span style="font-weight: bold;">Username</span>: ##Customer.AdminUser.Email## <br>                                                    </p>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <div class="column-bottom" style="font-size: 3px; line-height: 3px;">&nbsp;</div>
                                </td>
                            </tr>
                        </tbody>
                    </table> </center>
            </td>
        </tr>
    </tbody>
</table>
<table class="wrapper" style="width: 100%; table-layout: fixed;">
    <tbody>
        <tr>
            <td style="padding: 0px; vertical-align: top;">
                <center> <br>                    <div class="email-bottom" style="font-size: 54px; line-height: 54px;">                        <br>                    </div> </center>
            </td>
        </tr>
    </tbody>
</table>', CAST(0x0000A3C400000000 AS DateTime), NULL, CAST(0x0000A4BA00BD4EE4 AS DateTime), NULL, CAST(0x0000A591017EC730 AS DateTime))
INSERT [dbo].[EmailTemplates] ([Id], [TemplateName], [Template], [FromEmail], [Subject], [Body], [TimeStamp], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'4645f5a2-f3d4-42df-83b1-e20630406448', N'Forgot Password', NULL, N'support@saaspro.net', N'It''s Cool. We Forget Sometimes.', N'<table class="wrapper" style="width: 100%; table-layout: fixed;">
    <tbody>
        <tr>
            <td style="padding: 0px; vertical-align: top;">
                <center>
                    <div class="email-top" style="font-size: 54px; line-height: 54px;"><br></div>
                    <table class="header" style="margin-left: auto; margin-right: auto; width: 560px;">
                        <tbody>
                            <tr>
                                <td align="left" style="padding: 0px; vertical-align: top; color: rgb(170, 170, 170); font-size: 24px; font-family: Georgia, serif;"><img src="https://demo.saaspro.net/images/saaspro-logo.png" style="width: 100px;"></td>

                            </tr>
                        </tbody>
                    </table>
                </center>
            </td>
        </tr>
    </tbody>
</table>

<table class="wrapper" style="width: 100%; table-layout: fixed;">
    <tbody>
        <tr>
            <td style="padding: 0px; vertical-align: top;">
                <center>
                    <table>

                        <tbody>

                            <tr>

                                <td class="column" style="padding: 0px; vertical-align: top; color: rgb(53, 54, 56);">
                                    <div>
                                        <div class="column-top" style="font-size: 20px;">&nbsp;</div>

                                    </div>

                                    <table class="contents" style="width: 600px;">

                                        <tbody>

                                            <tr>

                                                <td class="padded" style="padding: 0px 20px; vertical-align: top;">
                                                    <h1 style="margin-top: 0px; margin-bottom: 24px; font-size: 32px; font-family: Georgia, serif; line-height: 42px;">Forgot Password</h1>

                                                    <p style="margin-bottom: 27px; letter-spacing: -0.01em; font-family: Georgia, serif; -webkit-font-smoothing: antialiased; text-rendering: optimizelegibility; font-size: 17px; line-height: 27px;">
                                                        Click on the link below:&nbsp;
                                                        <br>
                                                        <a href="http://##ResetPasswordUrl##" target="_blank">Go to reset password page</a><br>
                                                    </p>

                                                </td>

                                            </tr>

                                        </tbody>

                                    </table>

                                    <div class="column-bottom" style="font-size: 3px; line-height: 3px;">&nbsp;</div>

                                </td>
                            </tr>
                        </tbody>
                    </table>
                </center>
            </td>
        </tr>
    </tbody>
</table>', CAST(0x0000A3C400000000 AS DateTime), NULL, CAST(0x0000A4BA00BD33F0 AS DateTime), NULL, CAST(0x0000A5910170FFFB AS DateTime))
INSERT [dbo].[EmailTemplates] ([Id], [TemplateName], [Template], [FromEmail], [Subject], [Body], [TimeStamp], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'f7d97e5e-bf93-4ffb-a405-f20077a50b41', N'Charge Failed', NULL, N'support@saaspro.net', N'Charge Failed', N'<table class="wrapper" style="width: 100%; table-layout: fixed;">    <tbody>        <tr>            <td style="padding: 0px; vertical-align: top;">                <center> <table class="one-col" style="margin-left: auto; margin-right: auto; width: 600px;">                        <tbody>                            <tr>                                <td class="column" style="padding: 0px; vertical-align: top; color: rgb(53, 54, 56);">                                    <div>                                        <div class="column-top" style="font-size: 20px;">&nbsp;</div>                                    </div>                                    <img src="https://demo.saaspro.net/images/saaspro-logo.png" style="width: 100px;"><div>                                        <div class="column-top" style="font-size: 20px;"><br></div>                                    </div>                                    <table class="contents" style="width: 600px;">                                        <tbody>                                            <tr>                                                <td class="padded" style="padding: 0px 20px; vertical-align: top;">                                                    <h1 style="margin-top: 0px; margin-bottom: 24px; font-size: 32px; font-family: Georgia, serif; line-height: 42px;">Hi #Customer.AdminUser.FistName##,&nbsp;<br><span style="color: inherit; background-color: transparent;"><br>Charge Failed</span><br></h1>                                                    <p style="margin-bottom: 27px; letter-spacing: -0.01em; font-family: Georgia, serif; -webkit-font-smoothing: antialiased; text-rendering: optimizelegibility; font-size: 17px; line-height: 27px;">Ipsum lorem, ipsem lorem.</p>                                                    <p style="margin-bottom: 27px; letter-spacing: -0.01em; font-family: Georgia, serif; -webkit-font-smoothing: antialiased; text-rendering: optimizelegibility; font-size: 17px; line-height: 27px;">                                                        <span style="font-weight: bold;">URL</span>: ##Customer.HostName## <br>                                                        <span style="font-weight: bold;">Plan</span>:&nbsp;##Customer.Plan.Name## <br>                                                        <span style="font-weight: bold;">Username</span>: ##Customer.AdminUser.Email## <br>                                                    </p>                                                </td>                                            </tr>                                        </tbody>                                    </table>                                    <div class="column-bottom" style="font-size: 3px; line-height: 3px;">&nbsp;</div>                                </td>                            </tr>                        </tbody>                    </table> </center>            </td>        </tr>    </tbody></table>', CAST(0x0000A3C400000000 AS DateTime), NULL, CAST(0x0000A4BA00BD0510 AS DateTime), NULL, CAST(0x0000A59101744D16 AS DateTime))
INSERT [dbo].[EmailTemplates] ([Id], [TemplateName], [Template], [FromEmail], [Subject], [Body], [TimeStamp], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'956bba07-9ce1-478c-8c72-fcf4a2ff5e5f', N'Charge Successfull', NULL, N'support@saaspro.net', N'Charge Successful', N'<table class="wrapper" style="width: 100%; table-layout: fixed;">    <tbody>        <tr>            <td style="padding: 0px; vertical-align: top;">                <center> <table class="one-col" style="margin-left: auto; margin-right: auto; width: 600px;">                        <tbody>                            <tr>                                <td class="column" style="padding: 0px; vertical-align: top; color: rgb(53, 54, 56);">                                    <div>                                        <div class="column-top" style="font-size: 20px;">&nbsp;</div>                                    </div>                                    <img src="https://demo.saaspro.net/images/saaspro-logo.png" style="width: 100px;"><div>                                        <div class="column-top" style="font-size: 20px;"><br></div>                                    </div>                                    <table class="contents" style="width: 600px;">                                        <tbody>                                            <tr>                                                <td class="padded" style="padding: 0px 20px; vertical-align: top;">                                                    <h1 style="margin-top: 0px; margin-bottom: 24px; font-size: 32px; font-family: Georgia, serif; line-height: 42px;">Hi #Customer.AdminUser.FistName##,&nbsp;<br><span style="color: inherit; background-color: transparent;"><br>Monthly Customer Subscription Invoice</span><br></h1>                                                    <p style="margin-bottom: 27px; letter-spacing: -0.01em; font-family: Georgia, serif; -webkit-font-smoothing: antialiased; text-rendering: optimizelegibility; font-size: 17px; line-height: 27px;">Ipsum lorem, ipsem lorem.</p>                                                    <p style="margin-bottom: 27px; letter-spacing: -0.01em; font-family: Georgia, serif; -webkit-font-smoothing: antialiased; text-rendering: optimizelegibility; font-size: 17px; line-height: 27px;">                                                        <span style="font-weight: bold;">URL</span>: ##Customer.HostName## <br>                                                        <span style="font-weight: bold;">Plan</span>:&nbsp;##Customer.Plan.Name## <br>                                                        <span style="font-weight: bold;">Username</span>: ##Customer.AdminUser.Email## <br>                                                    </p>                                                </td>                                            </tr>                                        </tbody>                                    </table>                                    <div class="column-bottom" style="font-size: 3px; line-height: 3px;">&nbsp;</div>                                </td>                            </tr>                        </tbody>                    </table> </center>            </td>        </tr>    </tbody></table>', CAST(0x0000A3C400000000 AS DateTime), NULL, CAST(0x0000A4BA00BD1DAC AS DateTime), NULL, CAST(0x0000A5910174162E AS DateTime))
INSERT [dbo].[PlanInfo] ([Id], [Name], [OrderIndex]) VALUES (N'e576ea39-ed2b-4bee-bb56-0d46d070a0be', N'Comments', 5)
INSERT [dbo].[PlanInfo] ([Id], [Name], [OrderIndex]) VALUES (N'1b9e0d1b-ddd1-4291-ab78-4080c2ef77ca', N'SSL Security', 3)
INSERT [dbo].[PlanInfo] ([Id], [Name], [OrderIndex]) VALUES (N'27bb321a-c24e-4f8a-9081-445fabed6231', N'Users', 2)
INSERT [dbo].[PlanInfo] ([Id], [Name], [OrderIndex]) VALUES (N'ccf96e6f-5a74-42d2-b4db-cbec2b47b328', N'Notifications', 4)
INSERT [dbo].[PlanInfo] ([Id], [Name], [OrderIndex]) VALUES (N'd010b9c3-34d2-4d38-86ec-d738fe8f29c9', N'Work Items', 1)
INSERT [dbo].[PlanInfoValues] ([PlanId], [PlanInfoItemId], [Value]) VALUES (N'c996026a-bc1b-40ab-8871-08ec61c4aa65', N'1b9e0d1b-ddd1-4291-ab78-4080c2ef77ca', N'Yes')
INSERT [dbo].[PlanInfoValues] ([PlanId], [PlanInfoItemId], [Value]) VALUES (N'c996026a-bc1b-40ab-8871-08ec61c4aa65', N'27bb321a-c24e-4f8a-9081-445fabed6231', N'8')
INSERT [dbo].[PlanInfoValues] ([PlanId], [PlanInfoItemId], [Value]) VALUES (N'c996026a-bc1b-40ab-8871-08ec61c4aa65', N'd010b9c3-34d2-4d38-86ec-d738fe8f29c9', N'3')
INSERT [dbo].[PlanInfoValues] ([PlanId], [PlanInfoItemId], [Value]) VALUES (N'94997c02-7a31-4d83-9cfd-951af1e9002c', N'27bb321a-c24e-4f8a-9081-445fabed6231', N'3')
INSERT [dbo].[PlanInfoValues] ([PlanId], [PlanInfoItemId], [Value]) VALUES (N'94997c02-7a31-4d83-9cfd-951af1e9002c', N'd010b9c3-34d2-4d38-86ec-d738fe8f29c9', N'1')
INSERT [dbo].[PlanInfoValues] ([PlanId], [PlanInfoItemId], [Value]) VALUES (N'079b1f22-32ed-41e9-9a2a-c226766fa2ec', N'e576ea39-ed2b-4bee-bb56-0d46d070a0be', N'Yes')
INSERT [dbo].[PlanInfoValues] ([PlanId], [PlanInfoItemId], [Value]) VALUES (N'079b1f22-32ed-41e9-9a2a-c226766fa2ec', N'1b9e0d1b-ddd1-4291-ab78-4080c2ef77ca', N'Yes')
INSERT [dbo].[PlanInfoValues] ([PlanId], [PlanInfoItemId], [Value]) VALUES (N'079b1f22-32ed-41e9-9a2a-c226766fa2ec', N'27bb321a-c24e-4f8a-9081-445fabed6231', N'Unlimited')
INSERT [dbo].[PlanInfoValues] ([PlanId], [PlanInfoItemId], [Value]) VALUES (N'079b1f22-32ed-41e9-9a2a-c226766fa2ec', N'ccf96e6f-5a74-42d2-b4db-cbec2b47b328', N'Yes')
INSERT [dbo].[PlanInfoValues] ([PlanId], [PlanInfoItemId], [Value]) VALUES (N'079b1f22-32ed-41e9-9a2a-c226766fa2ec', N'd010b9c3-34d2-4d38-86ec-d738fe8f29c9', N'Unlimited')
INSERT [dbo].[Plans] ([Id], [Name], [Price], [Period], [OrderIndex], [PlanCode], [Enabled], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'c996026a-bc1b-40ab-8871-08ec61c4aa65', N'Medium', 19.9900, N'month', 2, N'Medium', 1, NULL, CAST(0x0000A4BA00924BF4 AS DateTime), NULL, CAST(0x0000A4BD00C20E20 AS DateTime))
INSERT [dbo].[Plans] ([Id], [Name], [Price], [Period], [OrderIndex], [PlanCode], [Enabled], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'94997c02-7a31-4d83-9cfd-951af1e9002c', N'Small', 9.9900, N'month', 1, N'Small', 1, NULL, CAST(0x0000A4BA009250A4 AS DateTime), NULL, CAST(0x0000A4BD00C218AC AS DateTime))
INSERT [dbo].[Plans] ([Id], [Name], [Price], [Period], [OrderIndex], [PlanCode], [Enabled], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'079b1f22-32ed-41e9-9a2a-c226766fa2ec', N'Large', 29.9900, N'month', 3, N'Large', 1, NULL, CAST(0x0000A4BA00919C2C AS DateTime), NULL, CAST(0x0000A4BD00BBF65C AS DateTime))
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'529d5e84-f6e7-4893-bee2-034fba144fda', N'What is your maternal grandmother''''s maiden name?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'6ae4e1bc-f5e5-47a8-8766-084b85effa5b')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'db116fa5-f6ef-4c48-b622-09e3f041c993', N'What street did you live on in third grade?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'16fe7bd0-29ae-4e85-9e79-ca93edb90f52')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'3e64ef36-d3ad-4063-a3ff-0c51c42d4b6b', N'What is your favorite sports team?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'6ae4e1bc-f5e5-47a8-8766-084b85effa5b')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'cf781ffc-cb43-4279-b4c8-0c8238693f4e', N'What is the name of your favorite childhood friend?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'68927241-0c0d-4426-8cf5-71e5934e1305')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'a6ed8560-10c3-4c47-8029-115758f0b7df', N'What school did you attend for sixth grade?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'16fe7bd0-29ae-4e85-9e79-ca93edb90f52')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'e23f8f3e-798b-49bf-8721-11f049d66fdc', N'In what city did you meet your spouse/significant other?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'6ae4e1bc-f5e5-47a8-8766-084b85effa5b')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'6caef85c-84f3-4067-bb31-13d27d6e57cb', N'What is your favorite sports team?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'68927241-0c0d-4426-8cf5-71e5934e1305')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'1f319eec-df47-4027-8a55-1403460c5e26', N'What city were you in for New Years Eve 2000?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'16fe7bd0-29ae-4e85-9e79-ca93edb90f52')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'dfbe6676-5d08-41da-8545-1751fd46d424', N'What is the name of your favorite childhood friend?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'16fe7bd0-29ae-4e85-9e79-ca93edb90f52')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'5302ceae-b795-4bdf-b937-1b1cc50fa86d', N'What is your maternal grandmother''''s maiden name?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'68927241-0c0d-4426-8cf5-71e5934e1305')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'41c98e6e-10e4-4e8e-bd10-20506b6c0419', N'What is your oldest sibling''''s middle name?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'caca3875-e110-485a-9ee5-a5b466747a35')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'ff4964f6-e7b8-422e-b7d0-220a42253d41', N'What is your maternal grandmother''''s maiden name?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'd8177b2f-7185-42d5-a5da-4a29e9f1f12d')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'a1449117-40dc-4ed3-8f9d-244f4b002a54', N'What school did you attend for sixth grade?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'6ae4e1bc-f5e5-47a8-8766-084b85effa5b')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'a37ab014-9391-4a90-b58a-24ebc44b135b', N'What is your favorite sports team?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'5727ef86-8932-496d-a01f-856777e52e07')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'81fa21b3-c775-47ec-9e6d-257a2b6424dc', N'What is your father''''s middle name?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'caca3875-e110-485a-9ee5-a5b466747a35')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'fbee1a88-c3c6-45c7-ab2c-2b0b84ef2942', N'What is your oldest sibling''''s middle name?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'6ae4e1bc-f5e5-47a8-8766-084b85effa5b')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'71cfd288-30c5-46d5-9108-31f6a81f7ca5', N'What was your childhood nickname?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'8c7b9ba5-21a8-4949-ab2d-06aa7513a590')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'cdb6ab4f-d1f0-4264-bfec-32e6dbaeeb0d', N'In what city or town did your mother and father meet?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'caca3875-e110-485a-9ee5-a5b466747a35')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'8ddbaac9-d879-470e-be20-398803826742', N'What is the middle name of your oldest child?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'16fe7bd0-29ae-4e85-9e79-ca93edb90f52')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'7865d259-06a8-44f6-a39d-3bbe6e12e054', N'What is the name of your favorite childhood friend?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'6ae4e1bc-f5e5-47a8-8766-084b85effa5b')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'3ec77923-2c2f-4485-8997-3d0b5f97f3a7', N'In what city did you meet your spouse/significant other?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'68927241-0c0d-4426-8cf5-71e5934e1305')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'8f2c0b7a-f286-477a-b9e3-3d12a8ed2408', N'In what city did you meet your spouse/significant other?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'caca3875-e110-485a-9ee5-a5b466747a35')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'897d8313-0b2d-41ce-a036-3f808057761f', N'What is your maternal grandmother''''s maiden name?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'16fe7bd0-29ae-4e85-9e79-ca93edb90f52')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'd7c6940f-a6d6-470f-b734-41baae3cd913', N'What is your father''''s middle name?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'd8177b2f-7185-42d5-a5da-4a29e9f1f12d')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'42248658-01ed-4364-a9ca-4297f69dbf78', N'What is your father''''s middle name?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'68927241-0c0d-4426-8cf5-71e5934e1305')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'a4adaabc-333b-431a-ab40-42f9a2205d6a', N'In what town was your first job?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'8c7b9ba5-21a8-4949-ab2d-06aa7513a590')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'b6f14934-e809-4272-ae0f-49d115affe73', N'What is your favorite sports team?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'caca3875-e110-485a-9ee5-a5b466747a35')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'7d24125c-1de6-4347-bc30-4aecc498a0a4', N'What street did you live on in third grade?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'5727ef86-8932-496d-a01f-856777e52e07')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'1304f886-93c3-4bee-b7f7-4b77b7f5b600', N'What street did you live on in third grade?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'd8177b2f-7185-42d5-a5da-4a29e9f1f12d')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'fcd8e3df-fa2e-48c5-b24a-4c91166d7abd', N'What is your father''''s middle name?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'16fe7bd0-29ae-4e85-9e79-ca93edb90f52')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'9b4f4003-7cd6-4c24-a7dd-4d7ae720c0df', N'What is the middle name of your oldest child?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'8c7b9ba5-21a8-4949-ab2d-06aa7513a590')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'2b69786f-0e57-41de-ae5b-504ba999deb0', N'What street did you live on in third grade?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'68927241-0c0d-4426-8cf5-71e5934e1305')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'5fc0eab8-413e-4c2c-a472-518d5c2acfe9', N'What is your father''''s middle name?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'5727ef86-8932-496d-a01f-856777e52e07')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'c0ff1357-fb90-446e-852f-5cd62b24ff43', N'What city were you in for New Years Eve 2000?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'68927241-0c0d-4426-8cf5-71e5934e1305')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'0b296b38-bdcb-441c-97a2-5d07a8e342fb', N'In what city or town did your mother and father meet?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'5727ef86-8932-496d-a01f-856777e52e07')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'13457ab7-e1a8-45b7-b187-5e95479329df', N'What is your oldest sibling''''s middle name?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'16fe7bd0-29ae-4e85-9e79-ca93edb90f52')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'0d54a87a-c7df-4d08-bac4-6062dd96eb70', N'What is your maternal grandmother''''s maiden name?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'8c7b9ba5-21a8-4949-ab2d-06aa7513a590')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'953d0a60-233c-4e9a-919c-6361dcee6b2c', N'What was your childhood nickname?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'5727ef86-8932-496d-a01f-856777e52e07')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'8032eb46-b01d-4a9b-98d8-6f8518e1c158', N'What is the name of your favorite childhood friend?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'5727ef86-8932-496d-a01f-856777e52e07')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'a648789f-24b7-48f0-ba50-74ae3abe0999', N'In what city or town did your mother and father meet?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'8c7b9ba5-21a8-4949-ab2d-06aa7513a590')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'a0120777-a41e-46f5-8037-78a183fe5a32', N'In what city did you meet your spouse/significant other?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'8c7b9ba5-21a8-4949-ab2d-06aa7513a590')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'fef504dc-1107-4bf0-8d21-7b2e2c0836bc', N'What city were you in for New Years Eve 2000?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'6ae4e1bc-f5e5-47a8-8766-084b85effa5b')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'53ea8ef5-d0cf-4c38-9901-7c77c5faa4aa', N'What was your childhood nickname?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'68927241-0c0d-4426-8cf5-71e5934e1305')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'30c763d9-8b15-475e-9a91-7d4c4f57d24b', N'What is your oldest sibling''''s middle name?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'd8177b2f-7185-42d5-a5da-4a29e9f1f12d')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'5d9a4960-a585-43ef-9835-7e932f7d1137', N'What street did you live on in third grade?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'6ae4e1bc-f5e5-47a8-8766-084b85effa5b')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'ff147272-d28f-4522-8fa4-7f8843a05c16', N'In what town was your first job?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'5727ef86-8932-496d-a01f-856777e52e07')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'a468c554-047d-460b-bd8a-8087bebb4be9', N'What is the middle name of your oldest child?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'd8177b2f-7185-42d5-a5da-4a29e9f1f12d')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'f868b47a-097a-4ea1-b92b-80f5ab6493b4', N'In what town was your first job?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'6ae4e1bc-f5e5-47a8-8766-084b85effa5b')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'8bb43747-ffba-4926-b6af-87fe40b86c8f', N'What is the middle name of your oldest child?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'68927241-0c0d-4426-8cf5-71e5934e1305')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'f01f6127-a4ef-4ed8-bf1f-88bbdcd8cabc', N'What is your favorite sports team?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'8c7b9ba5-21a8-4949-ab2d-06aa7513a590')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'170eb54f-7c94-4d2e-9c23-8ab9d54e6434', N'What is your oldest sibling''''s middle name?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'5727ef86-8932-496d-a01f-856777e52e07')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'8b854deb-d03c-4dd6-84d0-8acdffa6e95d', N'What is the name of your favorite childhood friend?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'caca3875-e110-485a-9ee5-a5b466747a35')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'90c4b060-68c4-4fa1-888b-8c532262e555', N'What was your childhood nickname?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'caca3875-e110-485a-9ee5-a5b466747a35')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'f9784313-6bfd-4ddc-a777-8d103eeb34e5', N'What is your maternal grandmother''''s maiden name?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'caca3875-e110-485a-9ee5-a5b466747a35')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'f6a846e5-4428-4526-ab2a-8d4179a2730f', N'What is your oldest sibling''''s middle name?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'68927241-0c0d-4426-8cf5-71e5934e1305')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'f4dddb69-d7b7-4872-83a1-9046da6a5149', N'What is your favorite sports team?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'd8177b2f-7185-42d5-a5da-4a29e9f1f12d')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'4083d090-fecd-49fe-b311-91a6733a9adf', N'What school did you attend for sixth grade?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'5727ef86-8932-496d-a01f-856777e52e07')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'3d34f50d-3649-48b1-8aef-92ed73dfaa3a', N'What school did you attend for sixth grade?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'68927241-0c0d-4426-8cf5-71e5934e1305')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'53cf685e-eb48-40dc-8dc7-939de70769e9', N'What is the middle name of your oldest child?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'5727ef86-8932-496d-a01f-856777e52e07')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'f4bba53e-cd53-4528-9e5b-9f8c5781d418', N'What is your favorite sports team?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'16fe7bd0-29ae-4e85-9e79-ca93edb90f52')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'01ea8bd7-9406-4c77-b37a-a59137956c00', N'What is your oldest sibling''''s middle name?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'8c7b9ba5-21a8-4949-ab2d-06aa7513a590')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'd02cfa0e-53ab-4d37-80d8-a618630371a6', N'In what city or town did your mother and father meet?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'68927241-0c0d-4426-8cf5-71e5934e1305')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'13add346-427a-4882-9227-aa46f1841794', N'What school did you attend for sixth grade?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'd8177b2f-7185-42d5-a5da-4a29e9f1f12d')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'ad498630-3d4e-4a45-9f51-aff83fb57b88', N'In what town was your first job?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'caca3875-e110-485a-9ee5-a5b466747a35')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'c4f7651d-846f-44fa-9b20-b359118b156b', N'In what city or town did your mother and father meet?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'16fe7bd0-29ae-4e85-9e79-ca93edb90f52')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'1c31260d-8b8b-4996-82bc-baef5feb38d2', N'What is the middle name of your oldest child?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'caca3875-e110-485a-9ee5-a5b466747a35')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'a316249d-e9b2-4246-ad79-be752c24ca10', N'In what city did you meet your spouse/significant other?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'd8177b2f-7185-42d5-a5da-4a29e9f1f12d')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'e36e4abe-f179-4a0d-8756-c0652e51ddfd', N'In what city did you meet your spouse/significant other?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'5727ef86-8932-496d-a01f-856777e52e07')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'8e2798c5-ab1c-4e5c-8808-c2e7831c046e', N'What was your childhood nickname?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'd8177b2f-7185-42d5-a5da-4a29e9f1f12d')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'5a5164b4-d3ca-4c83-81f5-cc5aa22599bb', N'In what city or town did your mother and father meet?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'6ae4e1bc-f5e5-47a8-8766-084b85effa5b')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'39bd42f4-0e10-4800-88f9-d319c85f44ce', N'In what town was your first job?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'68927241-0c0d-4426-8cf5-71e5934e1305')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'5f159f4f-70d2-4c41-b2ad-d3d9c182c948', N'What city were you in for New Years Eve 2000?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'5727ef86-8932-496d-a01f-856777e52e07')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'77cd1647-f952-4764-9072-d3e03d9d47ff', N'What city were you in for New Years Eve 2000?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'8c7b9ba5-21a8-4949-ab2d-06aa7513a590')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'ca1b2b40-693c-4326-ac31-d4763e7101a1', N'In what city did you meet your spouse/significant other?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'16fe7bd0-29ae-4e85-9e79-ca93edb90f52')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'32623ee9-a860-4d1f-9349-d80b1a8dadec', N'What is your father''''s middle name?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'8c7b9ba5-21a8-4949-ab2d-06aa7513a590')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'f84c7d5f-dab8-439e-a95a-d94fd267f8ba', N'What city were you in for New Years Eve 2000?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'caca3875-e110-485a-9ee5-a5b466747a35')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'd0082b4e-96b1-4d89-bb6b-db047e6b23c9', N'What was your childhood nickname?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'6ae4e1bc-f5e5-47a8-8766-084b85effa5b')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'26a1f0ff-eb52-46d8-a199-de39d6b9bca9', N'What street did you live on in third grade?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'8c7b9ba5-21a8-4949-ab2d-06aa7513a590')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'4f8412ba-37f4-480b-98a3-ded8c4bb4841', N'What is the name of your favorite childhood friend?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'8c7b9ba5-21a8-4949-ab2d-06aa7513a590')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'e58e8a57-7a97-4111-8c0d-df6e9f3fd9fd', N'What city were you in for New Years Eve 2000?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'd8177b2f-7185-42d5-a5da-4a29e9f1f12d')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'2fdb8f4c-c196-4826-8a5e-e0a9657ae50d', N'What is your maternal grandmother''''s maiden name?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'5727ef86-8932-496d-a01f-856777e52e07')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'e0326cea-68f9-4e26-80dc-e1a6019e458d', N'What is the middle name of your oldest child?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'6ae4e1bc-f5e5-47a8-8766-084b85effa5b')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'8899e36f-4275-490d-8272-e49449132b52', N'What street did you live on in third grade?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'caca3875-e110-485a-9ee5-a5b466747a35')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'764e0c20-80ed-4637-bd17-ecde8d352142', N'What school did you attend for sixth grade?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'8c7b9ba5-21a8-4949-ab2d-06aa7513a590')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'0382ba55-29b3-483e-ba74-ed4b37f8e2a0', N'What is the name of your favorite childhood friend?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'd8177b2f-7185-42d5-a5da-4a29e9f1f12d')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'012bc3f4-7f35-4254-b5ab-f1cd2cdd95fa', N'What is your father''''s middle name?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'6ae4e1bc-f5e5-47a8-8766-084b85effa5b')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'2c034458-4433-4352-94df-f2283d7d1a30', N'In what town was your first job?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'd8177b2f-7185-42d5-a5da-4a29e9f1f12d')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'd902fb9e-4f59-4a6b-886b-f3d64faf05c0', N'In what town was your first job?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'16fe7bd0-29ae-4e85-9e79-ca93edb90f52')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'f8bef5b4-f354-405d-a24f-f62efba79adc', N'In what city or town did your mother and father meet?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'd8177b2f-7185-42d5-a5da-4a29e9f1f12d')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'dd4f2820-2a38-4833-811a-fc375e249d31', N'What was your childhood nickname?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'16fe7bd0-29ae-4e85-9e79-ca93edb90f52')
INSERT [dbo].[ReferenceListItems] ([Id], [Value], [ReferenceListId], [CustomerId]) VALUES (N'b28f4d8f-a9da-46ab-b03b-fcc07ebf7b72', N'What school did you attend for sixth grade?', N'd16e4a19-179f-4137-8f90-f60704c73026', N'caca3875-e110-485a-9ee5-a5b466747a35')
INSERT [dbo].[ReferenceLists] ([Id], [SystemName]) VALUES (N'd16e4a19-179f-4137-8f90-f60704c73026', N'Security Questions')
INSERT [dbo].[Roles] ([Id], [Name], [UserType], [SystemRole], [CustomerId]) VALUES (N'f750b7a4-3f72-4bc2-9da6-1b6a4bee80e4', N'Administrator', N'SystemUser', 1, N'caca3875-e110-485a-9ee5-a5b466747a35')
INSERT [dbo].[Roles] ([Id], [Name], [UserType], [SystemRole], [CustomerId]) VALUES (N'dede4909-9817-45e7-a2bc-2e6cd3eeeb26', N'Administrator', N'SystemUser', 1, N'd8177b2f-7185-42d5-a5da-4a29e9f1f12d')
INSERT [dbo].[Roles] ([Id], [Name], [UserType], [SystemRole], [CustomerId]) VALUES (N'97ef8448-9f8b-4476-8b22-31325493d247', N'Administrator', N'SystemUser', 1, N'68927241-0c0d-4426-8cf5-71e5934e1305')
INSERT [dbo].[Roles] ([Id], [Name], [UserType], [SystemRole], [CustomerId]) VALUES (N'f5047504-0fca-4c31-8910-48677e8a0f7e', N'Administrator', N'SystemUser', 1, N'5727ef86-8932-496d-a01f-856777e52e07')
INSERT [dbo].[Roles] ([Id], [Name], [UserType], [SystemRole], [CustomerId]) VALUES (N'6814fb4f-ae81-4d25-a0c1-b7d7eba2d6cb', N'Administrator', N'SystemUser', 1, N'16fe7bd0-29ae-4e85-9e79-ca93edb90f52')
INSERT [dbo].[Roles] ([Id], [Name], [UserType], [SystemRole], [CustomerId]) VALUES (N'9c1e6ad4-3df9-4289-b833-e176bf6f2b0e', N'Administrator', N'SystemUser', 1, N'6ae4e1bc-f5e5-47a8-8766-084b85effa5b')
INSERT [dbo].[Roles] ([Id], [Name], [UserType], [SystemRole], [CustomerId]) VALUES (N'7776ed87-078c-41c0-8b02-e1d4480b270e', N'Administrator', N'SystemUser', 1, N'8c7b9ba5-21a8-4949-ab2d-06aa7513a590')

SET ANSI_PADDING ON

GO
/****** Object:  Index [UQ__Referenc__B40377A829572725]    Script Date: 8/30/2016 9:07:38 AM ******/
ALTER TABLE [dbo].[ReferenceLists] ADD  CONSTRAINT [UQ__Referenc__B40377A829572725] UNIQUE NONCLUSTERED 
(
	[SystemName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
ALTER TABLE [dbo].[ApiSessionTokens] ADD  DEFAULT ((0)) FOR [QuestionAnswered]
GO
ALTER TABLE [dbo].[ApiSessionTokens] ADD  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[ApiSessionTokens] ADD  DEFAULT (getdate()) FOR [UpdatedOn]
GO
ALTER TABLE [dbo].[ApiTokens] ADD  CONSTRAINT [DF__ApiTokens__Creat__5AEE82B9]  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[ApiTokens] ADD  CONSTRAINT [DF__ApiTokens__Updat__5BE2A6F2]  DEFAULT (getdate()) FOR [UpdatedOn]
GO
ALTER TABLE [dbo].[CustomerPayments] ADD  CONSTRAINT [DF__CustomerP__Creat__5CD6CB2B]  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[CustomerPayments] ADD  CONSTRAINT [DF__CustomerP__Updat__5DCAEF64]  DEFAULT (getdate()) FOR [UpdatedOn]
GO
ALTER TABLE [dbo].[Customers] ADD  CONSTRAINT [DF__Customers__Creat__5EBF139D]  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[Customers] ADD  CONSTRAINT [DF__Customers__Updat__5FB337D6]  DEFAULT (getdate()) FOR [UpdatedOn]
GO
ALTER TABLE [dbo].[EmailTemplates] ADD  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[EmailTemplates] ADD  DEFAULT (getdate()) FOR [UpdatedOn]
GO
ALTER TABLE [dbo].[Log] ADD  CONSTRAINT [DF__Log__LogId__3F466844]  DEFAULT (newid()) FOR [LogId]
GO
ALTER TABLE [dbo].[Notes] ADD  CONSTRAINT [DF__Notes__CreatedOn__628FA481]  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[Notes] ADD  CONSTRAINT [DF__Notes__UpdatedOn__6383C8BA]  DEFAULT (getdate()) FOR [UpdatedOn]
GO
ALTER TABLE [dbo].[NotificationMessages] ADD  CONSTRAINT [DF__tmp_ms_xx__Creat__68487DD7]  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[NotificationMessages] ADD  CONSTRAINT [DF__tmp_ms_xx__Updat__693CA210]  DEFAULT (getdate()) FOR [UpdatedOn]
GO
ALTER TABLE [dbo].[PlanInfo] ADD  CONSTRAINT [DF_PlanInfoItems_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[PlanInfo] ADD  CONSTRAINT [DF_PlanOptions_OrderIndex]  DEFAULT ((0)) FOR [OrderIndex]
GO
ALTER TABLE [dbo].[Plans] ADD  CONSTRAINT [DF_Plans_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Plans] ADD  CONSTRAINT [DF_Plans_OrderIndex]  DEFAULT ((0)) FOR [OrderIndex]
GO
ALTER TABLE [dbo].[Plans] ADD  CONSTRAINT [DF__Plans__CreatedOn__6A30C649]  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[Plans] ADD  CONSTRAINT [DF__Plans__UpdatedOn__6B24EA82]  DEFAULT (getdate()) FOR [UpdatedOn]
GO
ALTER TABLE [dbo].[Projects] ADD  CONSTRAINT [DF__Projects__Create__6C190EBB]  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[Projects] ADD  CONSTRAINT [DF__Projects__Update__6D0D32F4]  DEFAULT (getdate()) FOR [UpdatedOn]
GO
ALTER TABLE [dbo].[UserNotifications] ADD  CONSTRAINT [DF__UserNotif__Creat__6E01572D]  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[UserNotifications] ADD  CONSTRAINT [DF__UserNotif__Updat__6EF57B66]  DEFAULT (getdate()) FOR [UpdatedOn]
GO
ALTER TABLE [dbo].[ApiSessionTokens]  WITH CHECK ADD  CONSTRAINT [FK_ApiSessionTokens_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ApiSessionTokens] CHECK CONSTRAINT [FK_ApiSessionTokens_Users]
GO
ALTER TABLE [dbo].[ApiSessionTokens]  WITH CHECK ADD  CONSTRAINT [FK_ApiSessionTokens_UserSecurityQuestions] FOREIGN KEY([SecurityQuestionId])
REFERENCES [dbo].[UserSecurityQuestions] ([Id])
GO
ALTER TABLE [dbo].[ApiSessionTokens] CHECK CONSTRAINT [FK_ApiSessionTokens_UserSecurityQuestions]
GO
ALTER TABLE [dbo].[ApiTokens]  WITH CHECK ADD  CONSTRAINT [FK_ApiTokens_Customers] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ApiTokens] CHECK CONSTRAINT [FK_ApiTokens_Customers]
GO
ALTER TABLE [dbo].[ApiTokens]  WITH CHECK ADD  CONSTRAINT [FK_ApiTokensCreated_ToUsers] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ApiTokens] CHECK CONSTRAINT [FK_ApiTokensCreated_ToUsers]
GO
ALTER TABLE [dbo].[ApiTokens]  WITH CHECK ADD  CONSTRAINT [FK_ApiTokensUpdated_ToUsers] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ApiTokens] CHECK CONSTRAINT [FK_ApiTokensUpdated_ToUsers]
GO
ALTER TABLE [dbo].[AuditLog]  WITH CHECK ADD  CONSTRAINT [FK_AuditLog_Customers] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AuditLog] CHECK CONSTRAINT [FK_AuditLog_Customers]
GO
ALTER TABLE [dbo].[CustomerPayments]  WITH CHECK ADD  CONSTRAINT [FK_CustomerPaymentsCreated_ToUsers] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[CustomerPayments] CHECK CONSTRAINT [FK_CustomerPaymentsCreated_ToUsers]
GO
ALTER TABLE [dbo].[CustomerPayments]  WITH CHECK ADD  CONSTRAINT [FK_CustomerPaymentsUpdated_ToUsers] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[CustomerPayments] CHECK CONSTRAINT [FK_CustomerPaymentsUpdated_ToUsers]
GO
ALTER TABLE [dbo].[Customers]  WITH CHECK ADD  CONSTRAINT [FK_Customers_Plans] FOREIGN KEY([PlanId])
REFERENCES [dbo].[Plans] ([Id])
GO
ALTER TABLE [dbo].[Customers] CHECK CONSTRAINT [FK_Customers_Plans]
GO
ALTER TABLE [dbo].[Customers]  WITH CHECK ADD  CONSTRAINT [FK_Customers_Users] FOREIGN KEY([AdminUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Customers] CHECK CONSTRAINT [FK_Customers_Users]
GO
ALTER TABLE [dbo].[Customers]  WITH CHECK ADD  CONSTRAINT [FK_CustomersCreated_ToUsers] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Customers] CHECK CONSTRAINT [FK_CustomersCreated_ToUsers]
GO
ALTER TABLE [dbo].[Customers]  WITH CHECK ADD  CONSTRAINT [FK_CustomersUpdated_ToUsers] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Customers] CHECK CONSTRAINT [FK_CustomersUpdated_ToUsers]
GO
ALTER TABLE [dbo].[EmailTemplates]  WITH CHECK ADD  CONSTRAINT [FK_EmailTemplatesCreated_ToUsers] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[EmailTemplates] CHECK CONSTRAINT [FK_EmailTemplatesCreated_ToUsers]
GO
ALTER TABLE [dbo].[EmailTemplates]  WITH CHECK ADD  CONSTRAINT [FK_EmailTemplatesUpdated_ToUsers] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[EmailTemplates] CHECK CONSTRAINT [FK_EmailTemplatesUpdated_ToUsers]
GO
ALTER TABLE [dbo].[IPS]  WITH CHECK ADD  CONSTRAINT [FK_IPS_Customers] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[IPS] CHECK CONSTRAINT [FK_IPS_Customers]
GO
ALTER TABLE [dbo].[IPS]  WITH CHECK ADD  CONSTRAINT [FKA0CDFB32804A62A3] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[IPS] CHECK CONSTRAINT [FKA0CDFB32804A62A3]
GO
ALTER TABLE [dbo].[IPS]  WITH CHECK ADD  CONSTRAINT [FKA0CDFB32869EBE96] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[IPS] CHECK CONSTRAINT [FKA0CDFB32869EBE96]
GO
ALTER TABLE [dbo].[Notes]  WITH CHECK ADD  CONSTRAINT [FK_Notes_ToCustomers] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([Id])
GO
ALTER TABLE [dbo].[Notes] CHECK CONSTRAINT [FK_Notes_ToCustomers]
GO
ALTER TABLE [dbo].[Notes]  WITH CHECK ADD  CONSTRAINT [FK_NotesCreated_ToUsers] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Notes] CHECK CONSTRAINT [FK_NotesCreated_ToUsers]
GO
ALTER TABLE [dbo].[Notes]  WITH CHECK ADD  CONSTRAINT [FK_NotesUpdated_ToUsers] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Notes] CHECK CONSTRAINT [FK_NotesUpdated_ToUsers]
GO
ALTER TABLE [dbo].[NotificationMessages]  WITH CHECK ADD  CONSTRAINT [FK_NotificationMessagesCreated_ToUsers] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[NotificationMessages] CHECK CONSTRAINT [FK_NotificationMessagesCreated_ToUsers]
GO
ALTER TABLE [dbo].[NotificationMessages]  WITH CHECK ADD  CONSTRAINT [FK_NotificationMessagesUpdated_ToUsers] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[NotificationMessages] CHECK CONSTRAINT [FK_NotificationMessagesUpdated_ToUsers]
GO
ALTER TABLE [dbo].[NotificationMessages]  WITH CHECK ADD  CONSTRAINT [FK31FA2BF489C67B9A] FOREIGN KEY([SenderId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[NotificationMessages] CHECK CONSTRAINT [FK31FA2BF489C67B9A]
GO
ALTER TABLE [dbo].[Plans]  WITH CHECK ADD  CONSTRAINT [FK_PlansCreated_ToUsers] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Plans] CHECK CONSTRAINT [FK_PlansCreated_ToUsers]
GO
ALTER TABLE [dbo].[Plans]  WITH CHECK ADD  CONSTRAINT [FK_PlansUpdated_ToUsers] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Plans] CHECK CONSTRAINT [FK_PlansUpdated_ToUsers]
GO
ALTER TABLE [dbo].[Projects]  WITH CHECK ADD  CONSTRAINT [FK_ProjectsCreated_ToUsers] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Projects] CHECK CONSTRAINT [FK_ProjectsCreated_ToUsers]
GO
ALTER TABLE [dbo].[Projects]  WITH CHECK ADD  CONSTRAINT [FK_ProjectsUpdated_ToUsers] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Projects] CHECK CONSTRAINT [FK_ProjectsUpdated_ToUsers]
GO
ALTER TABLE [dbo].[ReferenceListItems]  WITH CHECK ADD  CONSTRAINT [FK_ReferenceListItems_Customers] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ReferenceListItems] CHECK CONSTRAINT [FK_ReferenceListItems_Customers]
GO
ALTER TABLE [dbo].[ReferenceListItems]  WITH CHECK ADD  CONSTRAINT [FKB7F41DBDE42A69B6] FOREIGN KEY([ReferenceListId])
REFERENCES [dbo].[ReferenceLists] ([Id])
GO
ALTER TABLE [dbo].[ReferenceListItems] CHECK CONSTRAINT [FKB7F41DBDE42A69B6]
GO
ALTER TABLE [dbo].[Roles]  WITH CHECK ADD  CONSTRAINT [FK_Roles_Customers] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Roles] CHECK CONSTRAINT [FK_Roles_Customers]
GO
ALTER TABLE [dbo].[RoleUsers]  WITH CHECK ADD  CONSTRAINT [FK1D67E2AC9900A6BA] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
GO
ALTER TABLE [dbo].[RoleUsers] CHECK CONSTRAINT [FK1D67E2AC9900A6BA]
GO
ALTER TABLE [dbo].[RoleUsers]  WITH CHECK ADD  CONSTRAINT [FK1D67E2ACCE5FEF8C] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[RoleUsers] CHECK CONSTRAINT [FK1D67E2ACCE5FEF8C]
GO
ALTER TABLE [dbo].[UserNotifications]  WITH CHECK ADD  CONSTRAINT [FK_UserNotificationsCreated_ToUsers] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[UserNotifications] CHECK CONSTRAINT [FK_UserNotificationsCreated_ToUsers]
GO
ALTER TABLE [dbo].[UserNotifications]  WITH CHECK ADD  CONSTRAINT [FK_UserNotificationsUpdated_ToUsers] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[UserNotifications] CHECK CONSTRAINT [FK_UserNotificationsUpdated_ToUsers]
GO
ALTER TABLE [dbo].[UserNotifications]  WITH CHECK ADD  CONSTRAINT [FKD944DD8E5266C05C] FOREIGN KEY([MessageId])
REFERENCES [dbo].[NotificationMessages] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserNotifications] CHECK CONSTRAINT [FKD944DD8E5266C05C]
GO
ALTER TABLE [dbo].[UserNotifications]  WITH CHECK ADD  CONSTRAINT [FKD944DD8ECE5FEF8C] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserNotifications] CHECK CONSTRAINT [FKD944DD8ECE5FEF8C]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Customers] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([Id])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Customers]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK2C1C7FE5804A62A3] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK2C1C7FE5804A62A3]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK2C1C7FE5869EBE96] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK2C1C7FE5869EBE96]
GO
ALTER TABLE [dbo].[UserSecurityQuestions]  WITH CHECK ADD  CONSTRAINT [FK_SecurityQuestions_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserSecurityQuestions] CHECK CONSTRAINT [FK_SecurityQuestions_Users]
GO
USE [SaaSPro]
GO
ALTER DATABASE [SaaSPro] SET  READ_WRITE 
GO
