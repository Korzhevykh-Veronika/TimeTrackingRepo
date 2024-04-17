CREATE TABLE [dbo].[Projects]
(
    [Id]               UNIQUEIDENTIFIER     NOT NULL,
    [Title]            NVARCHAR (250)       NOT NULL UNIQUE,
    [Description]      NVARCHAR (250)       NULL,

    CONSTRAINT [PK_Projects_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
)
