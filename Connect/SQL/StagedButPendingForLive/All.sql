
if exists (select 1 from sys.procedures where name = 'UpdateIndividualStatus')
drop procedure UpdateIndividualStatus
go

create procedure UpdateIndividualStatus
(
	@IndividualId int,
	@Status nvarchar(128),
	@Batch nvarchar(32)
)
as
begin
	declare @rollnumber int
	update alburhan_user.individual set RegistrationStatus = @Status where individualId = @individualId
	select Email from alburhan_user.Individual where IndividualId = @IndividualId
	update alburhan_user.individual set Section = NULL, EmailSent = 0 where @status NOT IN ('Enrolled','Dropped') and individualid = @Individualid
	
	if exists (select 1 from alburhan_user.individual where individualid = @individualid and rollnumber is null)
	begin
		select @rollnumber = max(isnull(RollNumber,1000))+1 from individual where batch = @batch
		update alburhan_user.individual set RollNumber = @rollnumber where individualId = @individualId and @status = 'Enrolled' 
	end
end
go


if exists (select 1 from sys.procedures where name = 'GetCeoInterviewedFemaleCandidatesForFinalDecision')
drop procedure GetCeoInterviewedFemaleCandidatesForFinalDecision
go

create procedure [dbo].[GetCeoInterviewedFemaleCandidatesForFinalDecision]
(
	@batch nvarchar(128)
)
as
begin
	select Id, Name, Email, I.IndividualId, Phone, Age, RegistrationStatus, Created, CeoComments, CeoGrade, 
	EmailSent, 
	ISNULL(Section, '') as Section, 
	RollNumber,
	P.Grade as PanelGrade
	from alburhan_user.individual I
	left outer join [alburhan_user].PanelGrading P on I.IndividualId = P.IndividualId
	where RegistrationStatus IN ('CeoInterviewed','Enrolled','Rejected','Completed','PanelInterviewed','Dropped')
	and UserType = 'Applicant' 
	--and CeoGrade in (1,2,3,4,5,6,7,8)
	AND Batch = @batch
	order by 
	[Name] asc
end
GO

---- Alter Assessments table, Add column 'TotalMarks'
--ALTER TABLE [dbo].[]

