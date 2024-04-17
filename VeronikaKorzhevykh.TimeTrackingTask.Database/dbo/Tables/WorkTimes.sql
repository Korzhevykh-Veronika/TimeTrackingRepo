CREATE TABLE [dbo].[WorkTimes]
(
    [Id]             UNIQUEIDENTIFIER      NOT NULL,
    [UserId]         UNIQUEIDENTIFIER      NOT NULL,
    [ProjectId]      UNIQUEIDENTIFIER      NOT NULL,
    [ClockInTime]    DATETIME              NOT NULL,
    [ClockOutTime]   DATETIME              NULL

    CONSTRAINT [PK_WorkTimes_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_WorkTimes_Projects] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Projects] ([Id])
)
