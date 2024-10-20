CREATE TABLE [dbo].[Photos] (
    [Id]                INT            IDENTITY (1, 1) NOT NULL,
    [Url]               NVARCHAR (MAX) NOT NULL,
    [IsMain]            BIT            NOT NULL,
    [PublicId]          NVARCHAR (MAX) NULL,
    [ApplicationUserId] INT            NOT NULL,
    CONSTRAINT [PK_Photos] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Photos_Users_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [dbo].[Users] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Photos_ApplicationUserId]
    ON [dbo].[Photos]([ApplicationUserId] ASC);

