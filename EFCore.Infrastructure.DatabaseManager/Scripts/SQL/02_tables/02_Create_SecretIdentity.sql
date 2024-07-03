IF NOT EXISTS (SELECT * FROM sys.objeCts WHERE object_id = OBJECT_ID(N'dbo.SecretIdentity') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[SecretIdentity](
		[Id]			BIGINT				NOT NULL,

		[Name]			VARCHAR(256)		NOT NULL,
		[HeroId]			BIGINT		NOT NULL,
		
		[CreatedAt]		DATETIMEOFFSET		NOT NULL DEFAULT SYSDATETIMEOFFSET(),
		[ModifiedBy]	VARCHAR(256)		NOT NULL,
		[UpdatedAt]		DATETIMEOFFSET		NULL,
		[DeletedAt]		DATETIMEOFFSET		NULL,
		[LastAction]	VARCHAR(64)			NOT NULL,
		[IsDeleted]		BIT					NOT NULL  DEFAULT 0,


		CONSTRAINT PK_SecretIdentity_Id PRIMARY KEY CLUSTERED (ID),
		CONSTRAINT FK_SecretIdentity_HeroId FOREIGN KEY (HeroId) REFERENCES [dbo].[Hero](Id)
		)
END