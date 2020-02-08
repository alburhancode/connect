
use alburhan_batch5

if not exists (select 1 from sys.tables where name = 'DashboardMessage')
	begin 
		create table [dbo].[DashboardMessage]
		(
			MessageId int identity(1,1),
			[Text] nvarchar(max) null,
			[Status] nvarchar(64) null,
			Created datetime null,
			Updated datetime null
		)
	end
go

if not exists (select 1 from sys.tables where name = 'MessageReply')
	begin 
		create table [dbo].[MessageReply]
		(
			MessageReplyId int identity(1,1),
			MessageId int,
			[Text] nvarchar(max) null,
			Created datetime null,
			Updated datetime null
		)
	end
go

if exists (select 1 from sys.procedures where name = 'GetAllMessages')
	drop procedure GetAllMessages
go

	create procedure [dbo].[GetAllMessages]
	as
	begin
		select * from DashboardMessage order by created desc
	end
go


if exists (select 1 from sys.procedures where name = 'GetMessage')
	drop procedure GetMessage
go

	create procedure [dbo].[GetMessage]
	(
		@MessageId int
	)
	as
	begin
		select * from DashboardMessage where MessageId = @MessageId
	end
go

if exists (select 1 from sys.procedures where name = 'InsertMessage')
	drop procedure InsertMessage
go

	create procedure [dbo].[InsertMessage]
	(
		@Text nvarchar(max),
		@Status nvarchar(64)
	)
	as
	begin
		insert into DashboardMessage values (@Text, @Status, GETDATE(), null)
	end
go


if exists (select 1 from sys.procedures where name = 'UpdateMessage')
	drop procedure UpdateMessage
go

	create procedure [dbo].[UpdateMessage]
	(
		@MessageId int,
		@Text nvarchar(max),
		@Status nvarchar(64)
	)
	as
	begin
		update DashboardMessage set [Text] = @Text, [Status] = @Status, Updated = getdate() where MessageId = @MessageId
	end
go


if exists (select 1 from sys.procedures where name = 'AddReplyToMessage')
	drop procedure AddReplyToMessage
go

	create procedure [dbo].[AddReplyToMessage]
	(
		@MessageId int,
		@Text nvarchar(max)
	)
	as
	begin
		insert into MessageReply values (@MessageId, @Text, getdate(), null)
	end
go


if exists (select 1 from sys.procedures where name = 'GetAllReplies')
	drop procedure GetAllReplies
go

	create procedure [dbo].[GetAllReplies]
	(
		@MessageId int	
	)
	as
	begin
		select [Text] from MessageReply where MessageId = @Messageid order by created desc
	end
go


use alburhan_batch5

if exists (select 1 from sys.procedures where name = 'DeleteMessage')
	drop procedure DeleteMessage
go

	create procedure [dbo].[DeleteMessage]
	(
		@MessageId int
	)
	as
	begin
		delete from MessageReply where MessageId = @MessageId
		delete from DashboardMessage where MessageId = @MessageId
	end
go

