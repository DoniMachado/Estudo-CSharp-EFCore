IF NOT EXISTS (SELECT * FROM sys.objeCts WHERE object_id = OBJECT_ID(N'dbo.HeroBattle') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[HeroBattle](
		[Id]			BIGINT				NOT NULL,

		[HeroId]		BIGINT				NOT NULL,
		[BattleId]		BIGINT				NOT NULL,
		
		[CreatedAt]		DATETIMEOFFSET		NOT NULL DEFAULT SYSDATETIMEOFFSET(),
		[ModifiedBy]	VARCHAR(256)		NOT NULL,
		[UpdatedAt]		DATETIMEOFFSET		NULL,
		[DeletedAt]		DATETIMEOFFSET		NULL,
		[LastAction]	VARCHAR(64)			NOT NULL,
		[IsDeleted]		BIT					NOT NULL DEFAULT 0,


		CONSTRAINT PK_HeroBattle_Id PRIMARY KEY CLUSTERED (ID),		
		CONSTRAINT FK_HeroBattle_HeroId FOREIGN KEY (HeroId) REFERENCES [dbo].[Hero](Id),		
		CONSTRAINT FK_HeroBattle_BattleId FOREIGN KEY (BattleId) REFERENCES [dbo].[Battle](Id)
		)
END