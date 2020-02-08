USE [alburhan_batch5]
GO

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
