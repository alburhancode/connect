
use alburhan_batch5

if not exists (select 1 from sys.tables where name = 'EventType')
	begin 
		create table [dbo].[EventType]
		(
			EventTypeId int identity(1,1),
			[Type] nvarchar(32) null,
			[Subject] nvarchar(128) null,
			AdditionalText nvarchar(256) null,
			FilePath nvarchar(128) null,
			Created datetime null,
			Updated datetime null
		)
	end
go

if not exists (select 1 from sys.tables where name = 'Event')
	begin 
		create table [dbo].[Event]
		(
			EventId int identity(1,1),
			EventTypeId int,
			EventName nvarchar(64),
			DateConducted datetime null,
			ConductedBy nvarchar(32) null,
			AdditionalText nvarchar(256) null,
			Created datetime null,
			Updated datetime null
		)
	end
go

if not exists (select 1 from sys.tables where name = 'IndividualEvent')
	begin 
		create table [dbo].[IndividualEvent]
		(
			IndividualEventId int identity(1,1),
			IndividualId int null, 
			EventId int null,
			[Value] nvarchar(64) null,
			AdditionalText nvarchar(256) null,
			FilePath nvarchar(128) null,
			Created datetime null,
			Updated datetime null
		)
	end
go


if exists (select 1 from sys.procedures where name = 'GetQuizzesForIndividual')
	drop procedure [dbo].[GetQuizzesForIndividual]
go

	create procedure [dbo].[GetQuizzesForIndividual]
	(
		@Id uniqueidentifier
	)
	as
	begin
		select E.EventName as [Name], IE.Value, IE.AdditionalText, E.DateConducted, E.ConductedBy, T.FilePath, T.Subject 
		from IndividualEvent IE 
		inner join [Event] E on IE.EventId = E.EventId
		inner join EventType T on E.EventTypeId = T.EventTypeId
		inner join [alburhan_user].Individual I on I.IndividualId = IE.IndividualId

		where I.Id = @Id
		order by E.DateConducted desc
	end
go


if exists (select 1 from sys.procedures where name = 'GetEventTypes')
	drop procedure [dbo].[GetEventTypes]
go

	create procedure [dbo].[GetEventTypes]
	as
	begin
		select * from EventType
		order by Created desc
	end
go


if exists (select 1 from sys.procedures where name = 'GetEventOccurences')
	drop procedure [dbo].[GetEventOccurences]
go

	create procedure [dbo].[GetEventOccurences]
	as
	begin
		select E.EventId, [Type], T.Subject, E.EventName, E.DateConducted, E.ConductedBy, E.AdditionalText 
		from [Event] E 
		INNER JOIN [EventType] T ON E.EventTypeId = T.EventTypeId
		order by DateConducted desc
	end
go


if exists (select 1 from sys.procedures where name = 'GetEventOccurencesForIndividual')
	drop procedure [dbo].[GetEventOccurencesForIndividual]
go

	create procedure [dbo].[GetEventOccurencesForIndividual]
	(
		@IndividualId int
	)
	as
	begin
		select ie.IndividualEventId, ie.IndividualId, t.Type, t.Subject, ie.Value, 
		ie.AdditionalText, e.EventName, e.DateConducted, e.ConductedBy, ie.FilePath
	
		from IndividualEvent ie
		inner join [Event] e on ie.EventId = e.EventId
		inner join [EventType] t on e.EventTypeId = t.EventTypeId 
		where ie.IndividualId = @IndividualId
		order by DateConducted desc
	end
go

if exists (select 1 from sys.procedures where name = 'DeleteIndividualEvent')
	drop procedure [dbo].[DeleteIndividualEvent]
go

	create procedure [dbo].[DeleteIndividualEvent]
	(
		@individualEventId int
	)
	as
	begin
		delete from individualevent where individualeventid = @individualeventid
	end
go

if exists (select 1 from sys.procedures where name = 'GetEventOccurencesNotForIndividual')
	drop procedure [dbo].[GetEventOccurencesNotForIndividual]
go

	create procedure [dbo].[GetEventOccurencesNotForIndividual]
	(
		@IndividualId int
	)
	as
	begin
		select e.EventId, e.EventName, t.Type, t.Subject, e.ConductedBy, e.DateConducted
		from [Event] e
		inner join EventType t on e.EventTypeId = t.EventTypeId
		where e.EventId not in
		(
			select ie.EventId from IndividualEvent ie
			where IndividualId = @IndividualId
		)
		order by DateConducted desc
	end
go


if exists (select 1 from sys.procedures where name = 'CreateAssessmentType')
	drop procedure [dbo].[CreateAssessmentType]
go

	create procedure [dbo].[CreateAssessmentType]
	(
		@Type nvarchar(128),
		@Subject nvarchar(64),
		@AdditionalText nvarchar(256),
		@FilePath nvarchar(128)
	)
	as
	begin
		insert into EventType values (@Type, @Subject, @AdditionalText, @FilePath, getdate(), null)
	end
go

if exists (select 1 from sys.procedures where name = 'CreateAssessment')
	drop procedure [dbo].[CreateAssessment]
go

	create procedure [dbo].[CreateAssessment]
	(
		@EventTypeId int,
		@EventName nvarchar(256),
		@DateConducted datetime,
		@ConductedBy nvarchar(64),
		@AdditionalText nvarchar(256)
	)
	as
	begin
		insert into Event values (@EventTypeId, @EventName, @DateConducted, @ConductedBy, @AdditionalText, getdate(), null)
	end
go


if exists (select 1 from sys.procedures where name = 'DeleteEventType')
	drop procedure [dbo].[DeleteEventType]
go

	create procedure [dbo].[DeleteEventType]
	(
		@EventTypeId int
	)
	as
	begin
		delete IE from IndividualEvent IE 
		inner join Event E ON IE.EventId = E.EventId 
		where E.EventTypeId = @EventTypeId

		delete from Event 
		where EventTypeId = @EventTypeId
		
		delete from EventType 
		where EventTypeId = @EventTypeId
	end
go


if exists (select 1 from sys.procedures where name = 'DeleteEvent')
	drop procedure [dbo].[DeleteEvent]
go

	create procedure [dbo].[DeleteEvent]
	(
		@EventId int
	)
	as
	begin
		delete IE from IndividualEvent IE 
		inner join Event E ON IE.EventId = E.EventId 
		where E.EventId = @EventId

		delete from Event 
		where EventId = @EventId
	end
go

if exists (select 1 from sys.procedures where name = 'CreateIndividualEvent')
	drop procedure [dbo].[CreateIndividualEvent]
go

	create procedure [dbo].[CreateIndividualEvent]
	(
		@IndividualId int,
		@EventId int,
		@Value nvarchar(64),
		@AdditionalText nvarchar(512),
		@FilePath nvarchar(256)
	)
	as
	begin
		insert into individualevent values (@IndividualId, @EventId, @Value, @AdditionalText, @FilePath, getdate(), null)
	end
go

