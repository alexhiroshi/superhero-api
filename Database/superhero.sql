IF EXISTS (SELECT name FROM master.sys.databases WHERE name = N'SuperHero')
BEGIN
	DROP DATABASE SuperHero
END

CREATE DATABASE SuperHero
GO
USE SuperHero

CREATE TABLE [dbo].[Superhero](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Alias] [varchar](100) NOT NULL,
	[ProtectionAreaId] [uniqueidentifier] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NULL,
 CONSTRAINT [PK_Superhero] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


CREATE TABLE [dbo].[Superpower](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Description] [varchar](200) NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NULL,
 CONSTRAINT [PK_Superpower] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


CREATE TABLE [dbo].[SuperheroSuperpower](
	[Id] [uniqueidentifier] NOT NULL,
	[SuperheroId] [uniqueidentifier] NOT NULL,
	[SuperpowerId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_SuperheroSuperpower] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


CREATE TABLE [dbo].[ProtectionArea](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Lat] [float] NULL,
	[Long] [float] NULL,
	[Radius] [float] NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NULL,
 CONSTRAINT [PK_ProtectionArea] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


ALTER TABLE [dbo].[Superhero]  WITH CHECK ADD  CONSTRAINT [FK_Superhero_ProtectionArea] FOREIGN KEY([ProtectionAreaId])
REFERENCES [dbo].[ProtectionArea] ([Id])
GO

ALTER TABLE [dbo].[Superhero] CHECK CONSTRAINT [FK_Superhero_ProtectionArea]
GO

ALTER TABLE [dbo].[SuperheroSuperpower]  WITH CHECK ADD  CONSTRAINT [FK_SuperheroSuperpower_Superhero] FOREIGN KEY([SuperheroId])
REFERENCES [dbo].[Superhero] ([Id])
GO

ALTER TABLE [dbo].[SuperheroSuperpower] CHECK CONSTRAINT [FK_SuperheroSuperpower_Superhero]
GO

ALTER TABLE [dbo].[SuperheroSuperpower]  WITH CHECK ADD  CONSTRAINT [FK_SuperheroSuperpower_Superpower] FOREIGN KEY([SuperpowerId])
REFERENCES [dbo].[Superpower] ([Id])
GO

ALTER TABLE [dbo].[SuperheroSuperpower] CHECK CONSTRAINT [FK_SuperheroSuperpower_Superpower]
GO


CREATE TABLE [dbo].[User](
	[Id] [uniqueidentifier] NOT NULL,
	[Username] [varchar](100) NOT NULL,
	[Password] [varchar](100) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


CREATE TABLE [dbo].[Role](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


CREATE TABLE [dbo].[UserRole](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_UserRole_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO

ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_UserRole_User]
GO

ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_UserRole_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO

ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_UserRole_Role]
GO

CREATE TABLE [dbo].[AuditEvent](
	[Id] [uniqueidentifier] NOT NULL,
	[Entity] [varchar](100) NOT NULL,
	[EntityId] [uniqueidentifier] NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[Action] [tinyint] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NULL,
 CONSTRAINT [PK_AuditEvent] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO






DECLARE @ProtectionAreaId UNIQUEIDENTIFIER = NEWID()
DECLARE @SuperheroId UNIQUEIDENTIFIER = NEWID()
DECLARE @SuperpowerId UNIQUEIDENTIFIER = NEWID()

INSERT INTO ProtectionArea VALUES (@ProtectionAreaId, 'Forest Hills', 40.7186746, -73.8506536, 2000, GETDATE(), NULL)
INSERT INTO Superhero VALUES (@SuperheroId, 'Spider-Man', 'Peter Parker', @ProtectionAreaId, GETDATE(), NULL)
INSERT INTO Superpower VALUES (@SuperpowerId, 'Spider-Sense', 'spider-sense', GETDATE(), NULL)
INSERT INTO SuperheroSuperpower VALUES (NEWID(), @SuperheroId, @SuperpowerId)

SET @ProtectionAreaId = NEWID()
SET @SuperheroId = NEWID()
SET @SuperpowerId = NEWID()

INSERT INTO ProtectionArea VALUES (@ProtectionAreaId, 'USA', 36.2472082, -113.7167676, 2000, GETDATE(), NULL)
INSERT INTO Superhero VALUES (@SuperheroId, 'Hulk', 'Dr. Robert Bruce Banner', @ProtectionAreaId, GETDATE(), NULL)
INSERT INTO Superpower VALUES (@SuperpowerId, 'Stronger', 'Stronger', GETDATE(), NULL)
INSERT INTO SuperheroSuperpower VALUES (NEWID(), @SuperheroId, @SuperpowerId)

SET @ProtectionAreaId = NEWID()
SET @SuperheroId = NEWID()
SET @SuperpowerId = NEWID()

INSERT INTO ProtectionArea VALUES (@ProtectionAreaId, 'Alberta', 54.1782494,-123.9572788, 2000, GETDATE(), NULL)
INSERT INTO Superhero VALUES (@SuperheroId, 'Wolverine', 'Logan', @ProtectionAreaId, GETDATE(), NULL)
INSERT INTO Superpower VALUES (@SuperpowerId, 'Accelerated healing process', 'Mutant', GETDATE(), NULL)
INSERT INTO SuperheroSuperpower VALUES (NEWID(), @SuperheroId, @SuperpowerId)


DECLARE @RoleAdminId UNIQUEIDENTIFIER = NEWID()
DECLARE @RoleStandardId UNIQUEIDENTIFIER = NEWID()
DECLARE @UserAdminId UNIQUEIDENTIFIER = NEWID()
DECLARE @UserStandardId UNIQUEIDENTIFIER = NEWID()

INSERT INTO [Role] VALUES (@RoleAdminId, 'Admin', GETDATE(), NULL)
INSERT INTO [Role] VALUES (@RoleStandardId, 'Standard', GETDATE(), NULL)

INSERT INTO [User] VALUES (@UserAdminId, 'superhero@adm.com.br', 'lcLzXuwIhRZz+sjQjqAsDAwdvaOviCCiUsFy+DIYQkAb6omQf3vANJCMfUNoE6GsvUH24i3zScXqRNRYIq0R3A==', GETDATE(), NULL)
INSERT INTO [User] VALUES (@UserStandardId, 'superhero@standard.com.br', 'lcLzXuwIhRZz+sjQjqAsDAwdvaOviCCiUsFy+DIYQkAb6omQf3vANJCMfUNoE6GsvUH24i3zScXqRNRYIq0R3A==', GETDATE(), NULL)

INSERT INTO [UserRole] VALUES (GETDATE(), @UserAdminId, @RoleAdminId)
INSERT INTO [UserRole] VALUES (GETDATE(), @UserStandardId, @RoleStandardId)