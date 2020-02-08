
if not exists (select 1 from sys.tables where name = 'Attendance')
begin 
	create table [dbo].[Attendance]
	(
		AttendanceId INT IDENTITY(1,1),
		Batch nvarchar(32),
		Course nvarchar(32),
		CreatedAt datetime,
		Email nvarchar(128),
		RollNumber nvarchar(64),
		Section nvarchar(32),
		Semester nvarchar(8),
		[Status] nvarchar(8),
		[Subject] nvarchar(64),
		Created datetime,
		UpdatedAt datetime
	)
end
go


if exists (select 1 from sys.procedures where name = 'GetAttendanceHistory')
drop procedure GetAttendanceHistory
go

CREATE procedure [dbo].[GetAttendanceHistory]
(
	@RollNumber nvarchar(32)
)
as
begin
	WITH AttendanceRowSet
	AS
	(
		SELECT [Subject]
		  ,CreatedAt
		  ,[Status]
		  ,UpdatedAt
		,ROW_NUMBER() OVER (PARTITION BY [Subject] ORDER BY [Subject], CreatedAt DESC) AS RowNum
		FROM dbo.Attendance 
		WHERE RollNumber = @RollNumber
	)
	SELECT [Subject], CreatedAt, [Status], UpdatedAt 
	FROM AttendanceRowSet 
	WHERE RowNum <= 6
end
go


if exists (select 1 from sys.procedures where name = 'SaveAttendance')
drop procedure SaveAttendance
go

create procedure SaveAttendance
(
	@Batch nvarchar(64),
	@Course nvarchar(32),
	@CreatedAt nvarchar(64),
	@Email nvarchar(128),
	@RollNumber nvarchar(64),
	@Section nvarchar(32),
	@Semester nvarchar(8),
	@Status nvarchar(8),
	@Subject nvarchar(64)
)
as
begin
	set @CreatedAt = dateadd(hour,5,@CreatedAt)
	declare @existingAttendanceId int
	select @existingAttendanceId = AttendanceId 
		from Attendance 
			where RollNumber = @RollNumber 
			and [subject] = @Subject
			and datepart(day, @CreatedAt) = datepart(day, CreatedAt)

	if(@existingAttendanceId > 0)
	begin
		update Attendance set [Status] = @Status, updatedAt = @CreatedAt 
		where AttendanceId = @existingAttendanceId
	end
	else
	begin
		insert into Attendance
		select 	@Batch, @Course, @CreatedAt, @Email, @RollNumber, @Section, @Semester, @Status, @Subject, dateadd(hour,5,getutcdate()), null
	end
end
GO

if exists (select 1 from sys.procedures where name = 'GetAttendanceForIndividual')
drop procedure dbo.GetAttendanceForIndividual
go

create procedure [dbo].[GetAttendanceForIndividual]
(
	@Id uniqueidentifier
)
AS
BEGIN
	declare @rollnumber nvarchar(64)
	select @rollnumber = batch + '-' + convert(nvarchar,rollnumber) from alburhan_user.individual where id = @id
	select AttendanceId, [Status], [Subject], CreatedAt as [AttendanceTakenOn], ISNULL(UpdatedAt, '') as [AttendanceUpdatedOn], @Id as [Id]
	from Attendance
	where RollNumber = @rollnumber
	ORDER BY Created desc
END
GO

if exists (select 1 from sys.procedures where name = 'UpdateAttendanceStatus')
drop procedure dbo.UpdateAttendanceStatus
go

create procedure [dbo].[UpdateAttendanceStatus]
(
	@AttendanceId int,
	@Status int
)
AS
BEGIN
	update attendance set [status] = @Status, UpdatedAt = dateadd(hour,5,getutcdate()) where attendanceid = @attendanceid
END
GO


