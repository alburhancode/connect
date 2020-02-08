
/*
------ long script

----if exists (select 1 from sys.tables where name = 'InterviewSlot')
----begin 
----	delete from InterviewSlot
----	drop table InterviewSlot
----end
----go

----create table InterviewSlot
----(
----	InterviewSlotId int identity(1,1),
----	Slot NVARCHAR(512),
----	[State] NVARCHAR(32),
----	MinAge INT,
----	MaxAge INT
----)
----GO

----if exists (select 1 from sys.tables where name = 'Individual')
----begin 
----	delete from Individual
----	drop table Individual
----end
----go

----create table Individual
----(
----	IndividualId int identity(100000,1),
----	Id uniqueidentifier NOT NULL,
----	Name NVARCHAR(128) NOT NULL,
----	Email NVARCHAR(128) NOT NULL,
----	Password NVARCHAR(25),
----	Phone NVARCHAR(14) NULL,
----	Age INT NULL,
----	UserType VARCHAR(20) NOT NULL,
----	RegistrationStatus NVARCHAR(32) NOT NULL,
----	PaymentMethod NVARCHAR(32) NULL,
----	PaymentCode NVARCHAR(32) NULL,
----	PaymentStatus VARCHAR(32) NULL,
----	InterviewSlotId INT, 

----	NicNumber NVARCHAR(13) NULL,
----	OtherContact NVARCHAR(14) NULL,
----	MaritalStatus NVARCHAR(8) NULL,
----	ResidentialAddress NVARCHAR(1024) NULL,
----	PermanentAddress NVARCHAR(1024) NULL,

----	MatricGpa NVARCHAR(8) NULL,
----	MatricSubjects NVARCHAR(1024) NULL,
----	MatricInstitute NVARCHAR(512) NULL,

----	IntermediateGpa NVARCHAR(8) NULL,
----	IntermediateSubjects NVARCHAR(1024) NULL,
----	IntermediateInstitute NVARCHAR(512) NULL,

----	GraduationGpa NVARCHAR(8) NULL,
----	GraduationSubjects NVARCHAR(1024) NULL,
----	GraduationInstitute NVARCHAR(512) NULL,

----	MastersGpa NVARCHAR(10) NULL,
----	MastersSubjects NVARCHAR(1024) NULL,
----	MastersInstitution NVARCHAR(512) NULL,

----	MsGpa NVARCHAR(10) NULL,
----	MsSubjects NVARCHAR(1024) NULL,
----	MsInstitution NVARCHAR(512) NULL,

----	PhdSubjects NVARCHAR(1024) NULL,
----	PhdInstitution NVARCHAR(512) NULL,

----	OtherQualification NVARCHAR(512) NULL,

----	CurrentJob NVARCHAR(128) NULL,
----	CompanyName NVARCHAR(128) NULL,
----	Designation NVARCHAR(128) NULL,
----	TotalJobExperience NVARCHAR(64) NULL,

----	BusinessName NVARCHAR(128) NULL,
----	BusinessArea NVARCHAR(128) NULL,
----	BusinessNature NVARCHAR(128) NULL,

----	AppliedBefore BIT DEFAULT 0,
----	AnyReligiousEducation BIT DEFAULT 0,
----	ReligiousEducationSpecify NVARCHAR(128) NULL,
----	AnyOtherExpertise NVARCHAR(128) NULL,
----	CurrentStatus NVARCHAR(32) NULL,
----	Education NVARCHAR(32) NULL,
	
----	CONSTRAINT AK_Email UNIQUE(Email)
----)
----GO



----if exists (select 1 from sys.procedures where name = 'DoesEmailExist')
----drop procedure DoesEmailExist
----go

----create procedure DoesEmailExist
----(
----	@Email NVARCHAR(256)
----)
----as
----begin
----	declare @exists bit = 0
----	select @exists = 1 from Individual where Email = @Email and [PaymentStatus] <> 'Blocked'
----	select @exists
----end
----GO

----if exists (select 1 from sys.procedures where name = 'IsAccountVerified')
----drop procedure IsAccountVerified
----go

----create procedure IsAccountVerified
----(
----	@Id NVARCHAR(64)
----)
----as
----begin
----	declare @exists bit = 0
----	select @exists = 1 from Individual where Id = @Id and [RegistrationStatus] <> 'Blocked'
----	if @exists = 1
----		update Individual set [RegistrationStatus] = 'Registered' where Id = @Id and [RegistrationStatus] = 'UnRegistered'
		
----	select @exists
----end
----GO

----if exists (select 1 from sys.procedures where name = 'GetAvailableInterviewSlots')
----drop procedure GetAvailableInterviewSlots
----go

----create procedure GetAvailableInterviewSlots
----as
----begin
----	select s.InterviewSlotId, Slot from InterviewSlot s
----	left outer join Individual i on i.InterviewSlotId = s.InterviewSlotId and i.UserType = 'Applicant' 
----	where s.State = 'Active'
----	and s.InterviewSlotId 
----	NOT IN
----(
----65
----,66
----,67
----,68
----,69
----,70
----,71
----,72
----,73
----,74
----,75
----,76
----,77
----,78
----,79
----,80
----,81
----,82
----,83
----,84
----,85
----,86
----,87
----,88
----,89,90,91,92,93,94,95,96
----,97
----,98
----,99
----,100
----,101
----,102
----,103
----,104
----,105
----,106
----,107
----,108
----,109
----,110
----,111
----,112
----,113,114,115,116,117,118
----,119,120,
----127,128
----)

----	group by s.InterviewSlotId, Slot
----	having count(s.InterviewSlotId) < 6
----	union all
----	select 999, 'To be decided by Al Burhan Administration'
----	order by InterviewSlotId
----end
----GO

----if exists (select 1 from sys.procedures where name = 'DoesPaymentCodeExist')
----drop procedure DoesPaymentCodeExist
----go

----create procedure DoesPaymentCodeExist
----(
----	@PaymentCode NVARCHAR(12)
----)
----as
----begin
----	declare @exists bit = 0
----	select @exists = 1 from Individual where [PaymentCode] = @PaymentCode
----	select @exists
----end
----go

----if exists (select 1 from sys.procedures where name = 'UpdateIndividualPaymentVerifiedFlag')
----drop procedure UpdateIndividualPaymentVerifiedFlag
----go

----create procedure UpdateIndividualPaymentVerifiedFlag
----(
----	@Id uniqueidentifier,
----	@PaymentStatus VARCHAR(32)
----)
----as
----begin
----	update Individual set PaymentStatus = @PaymentStatus where Id = @Id
----end
----GO

----if exists (select 1 from sys.procedures where name = 'GetSelectedInterviewSlot')
----drop procedure GetSelectedInterviewSlot
----go

----create procedure GetSelectedInterviewSlot
----(
----	@Id uniqueidentifier
----)
----as
----begin
----	select ISNULL(InterviewSlotId,0) from Individual where Id = @Id
----end
----GO


----if exists (select 1 from sys.procedures where name = 'GetSelectedInterviewSlotById')
----drop procedure GetSelectedInterviewSlotById
----go

----create procedure GetSelectedInterviewSlotById
----(
----	@Id int
----)
----as
----begin
----	select Slot as [InterviewSlot] from InterviewSlot where InterviewSlotId = @Id
----end
----GO

----ALTER TABLE Individual
----ADD Created datetime
----GO

------ Interview Weekend 15
------insert into InterviewSlot(Slot,State,MinAge,MaxAge) values ('Saturday - 24th Nov - 9:00-9:30','Active',0,0)
------insert into InterviewSlot(Slot,State,MinAge,MaxAge) values ('Saturday - 24th Nov - 9:30-10:00','Active',0,0)
------insert into InterviewSlot(Slot,State,MinAge,MaxAge) values ('Saturday - 24th Nov - 10:00-10:30','Active',0,0)
------insert into InterviewSlot(Slot,State,MinAge,MaxAge) values ('Saturday - 24th Nov - 10:30-11:00','Active',0,0)
------insert into InterviewSlot(Slot,State,MinAge,MaxAge) values ('Saturday - 24th Nov - 11:00-11:30','Active',0,0)
------insert into InterviewSlot(Slot,State,MinAge,MaxAge) values ('Saturday - 24th Nov - 11:30-12:00','Active',0,0)
------insert into InterviewSlot(Slot,State,MinAge,MaxAge) values ('Saturday - 24th Nov - 14:00-14:30','Active',0,0)
------insert into InterviewSlot(Slot,State,MinAge,MaxAge) values ('Saturday - 24th Nov - 14:30-15:00','Active',0,0)

------insert into InterviewSlot(Slot,State,MinAge,MaxAge) values ('Sunday - 25th Nov - 9:00-9:30','Active',0,0)
------insert into InterviewSlot(Slot,State,MinAge,MaxAge) values ('Sunday - 25th Nov - 9:30-10:00','Active',0,0)
------insert into InterviewSlot(Slot,State,MinAge,MaxAge) values ('Sunday - 25th Nov - 10:00-10:30','Active',0,0)
------insert into InterviewSlot(Slot,State,MinAge,MaxAge) values ('Sunday - 25th Nov - 10:30-11:00','Active',0,0)
------insert into InterviewSlot(Slot,State,MinAge,MaxAge) values ('Sunday - 25th Nov - 11:00-11:30','Active',0,0)
------insert into InterviewSlot(Slot,State,MinAge,MaxAge) values ('Sunday - 25th Nov - 11:30-12:00','Active',0,0)
------insert into InterviewSlot(Slot,State,MinAge,MaxAge) values ('Sunday - 25th Nov - 14:00-14:30','Active',0,0)
------insert into InterviewSlot(Slot,State,MinAge,MaxAge) values ('Sunday - 25th Nov - 14:30-15:00','Active',0,0)

-------- Interview Weekend 2
------insert into InterviewSlot(Slot,State,MinAge,MaxAge) values ('Saturday - 1st Dec - 9:00-9:30','Active',0,0)
------insert into InterviewSlot(Slot,State,MinAge,MaxAge) values ('Saturday - 1st Dec - 9:30-10:00','Active',0,0)
------insert into InterviewSlot(Slot,State,MinAge,MaxAge) values ('Saturday - 1st Dec - 10:00-10:30','Active',0,0)
------insert into InterviewSlot(Slot,State,MinAge,MaxAge) values ('Saturday - 1st Dec - 10:30-11:00','Active',0,0)
------insert into InterviewSlot(Slot,State,MinAge,MaxAge) values ('Saturday - 1st Dec - 11:00-11:30','Active',0,0)
------insert into InterviewSlot(Slot,State,MinAge,MaxAge) values ('Saturday - 1st Dec - 11:30-12:00','Active',0,0)
------insert into InterviewSlot(Slot,State,MinAge,MaxAge) values ('Saturday - 1st Dec - 14:00-14:30','Active',0,0)
------insert into InterviewSlot(Slot,State,MinAge,MaxAge) values ('Saturday - 1st Dec - 14:30-15:00','Active',0,0)

------insert into InterviewSlot(Slot,State,MinAge,MaxAge) values ('Sunday - 2nd Nov - 9:00-9:30','Active',0,0)
------insert into InterviewSlot(Slot,State,MinAge,MaxAge) values ('Sunday - 2nd Nov - 9:30-10:00','Active',0,0)
------insert into InterviewSlot(Slot,State,MinAge,MaxAge) values ('Sunday - 2nd Nov - 10:00-10:30','Active',0,0)
------insert into InterviewSlot(Slot,State,MinAge,MaxAge) values ('Sunday - 2nd Nov - 10:30-11:00','Active',0,0)
------insert into InterviewSlot(Slot,State,MinAge,MaxAge) values ('Sunday - 2nd Nov - 11:00-11:30','Active',0,0)
------insert into InterviewSlot(Slot,State,MinAge,MaxAge) values ('Sunday - 2nd Nov - 11:30-12:00','Active',0,0)
------insert into InterviewSlot(Slot,State,MinAge,MaxAge) values ('Sunday - 2nd Nov - 14:00-14:30','Active',0,0)
------insert into InterviewSlot(Slot,State,MinAge,MaxAge) values ('Sunday - 2nd Nov - 14:30-15:00','Active',0,0)

-------- Interview Weekend 3
------insert into InterviewSlot(Slot,State,MinAge,MaxAge) values ('Saturday - 8th Dec - 9:00-9:30','Active',0,0)
------insert into InterviewSlot(Slot,State,MinAge,MaxAge) values ('Saturday - 8th Dec - 9:30-10:00','Active',0,0)
------insert into InterviewSlot(Slot,State,MinAge,MaxAge) values ('Saturday - 8th Dec - 10:00-10:30','Active',0,0)
------insert into InterviewSlot(Slot,State,MinAge,MaxAge) values ('Saturday - 8th Dec - 10:30-11:00','Active',0,0)
------insert into InterviewSlot(Slot,State,MinAge,MaxAge) values ('Saturday - 8th Dec - 11:00-11:30','Active',0,0)
------insert into InterviewSlot(Slot,State,MinAge,MaxAge) values ('Saturday - 8th Dec - 11:30-12:00','Active',0,0)
------insert into InterviewSlot(Slot,State,MinAge,MaxAge) values ('Saturday - 8th Dec - 14:00-14:30','Active',0,0)
------insert into InterviewSlot(Slot,State,MinAge,MaxAge) values ('Saturday - 8th Dec - 14:30-15:00','Active',0,0)

------insert into InterviewSlot(Slot,State,MinAge,MaxAge) values ('Sunday - 9th Nov - 9:00-9:30','Active',0,0)
------insert into InterviewSlot(Slot,State,MinAge,MaxAge) values ('Sunday - 9th Nov - 9:30-10:00','Active',0,0)
------insert into InterviewSlot(Slot,State,MinAge,MaxAge) values ('Sunday - 9th Nov - 10:00-10:30','Active',0,0)
------insert into InterviewSlot(Slot,State,MinAge,MaxAge) values ('Sunday - 9th Nov - 10:30-11:00','Active',0,0)
------insert into InterviewSlot(Slot,State,MinAge,MaxAge) values ('Sunday - 9th Nov - 11:00-11:30','Active',0,0)
------insert into InterviewSlot(Slot,State,MinAge,MaxAge) values ('Sunday - 9th Nov - 11:30-12:00','Active',0,0)
------insert into InterviewSlot(Slot,State,MinAge,MaxAge) values ('Sunday - 9th Nov - 14:00-14:30','Active',0,0)
------insert into InterviewSlot(Slot,State,MinAge,MaxAge) values ('Sunday - 9th Nov - 14:30-15:00','Active',0,0)

-------- Interview Weekend 4
------insert into InterviewSlot(Slot,State,MinAge,MaxAge) values ('Saturday - 15th Dec - 9:00-9:30','Active',0,0)
------insert into InterviewSlot(Slot,State,MinAge,MaxAge) values ('Saturday - 15th Dec - 9:30-10:00','Active',0,0)
------insert into InterviewSlot(Slot,State,MinAge,MaxAge) values ('Saturday - 15th Dec - 10:00-10:30','Active',0,0)
------insert into InterviewSlot(Slot,State,MinAge,MaxAge) values ('Saturday - 15th Dec - 10:30-11:00','Active',0,0)
------insert into InterviewSlot(Slot,State,MinAge,MaxAge) values ('Saturday - 15th Dec - 11:00-11:30','Active',0,0)
------insert into InterviewSlot(Slot,State,MinAge,MaxAge) values ('Saturday - 15th Dec - 11:30-12:00','Active',0,0)
------insert into InterviewSlot(Slot,State,MinAge,MaxAge) values ('Saturday - 15th Dec - 14:00-14:30','Active',0,0)
------insert into InterviewSlot(Slot,State,MinAge,MaxAge) values ('Saturday - 15th Dec - 14:30-15:00','Active',0,0)

------insert into InterviewSlot(Slot,State,MinAge,MaxAge) values ('Sunday - 16th Nov - 9:00-9:30','Active',0,0)
------insert into InterviewSlot(Slot,State,MinAge,MaxAge) values ('Sunday - 16th Nov - 9:30-10:00','Active',0,0)
------insert into InterviewSlot(Slot,State,MinAge,MaxAge) values ('Sunday - 16th Nov - 10:00-10:30','Active',0,0)
------insert into InterviewSlot(Slot,State,MinAge,MaxAge) values ('Sunday - 16th Nov - 10:30-11:00','Active',0,0)
------insert into InterviewSlot(Slot,State,MinAge,MaxAge) values ('Sunday - 16th Nov - 11:00-11:30','Active',0,0)
------insert into InterviewSlot(Slot,State,MinAge,MaxAge) values ('Sunday - 16th Nov - 11:30-12:00','Active',0,0)
------insert into InterviewSlot(Slot,State,MinAge,MaxAge) values ('Sunday - 16th Nov - 14:00-14:30','Active',0,0)
------insert into InterviewSlot(Slot,State,MinAge,MaxAge) values ('Sunday - 16th Nov - 14:30-15:00','Active',0,0)


------insert into individual (Id,Name,Email,Password,Phone,Age,UserType,RegistrationStatus) values (newid(),'Admin', 'admin@alburhanconnect.com','loveislam','00000000','35','Administrator','Registered')
------insert into individual (Id,Name,Email,Password,Phone,Age,UserType,RegistrationStatus) values (newid(),'PaymentsAdmin', 'payments@alburhanconnect.com','loveislam','00000000','35','PaymentAdministrator','Registered')

------insert into individual (Id,Name,Email,Password,Phone,Age,UserType,RegistrationStatus,PaymentStatus,PaymentCode,InterviewSlotId) values (newid(),'F1', 'ruv@alburhanconnect.com','123456','00000000','35','Applicant','Registered','UnVerified','1234567890',1)
------insert into individual (Id,Name,Email,Password,Phone,Age,UserType,RegistrationStatus,PaymentStatus,PaymentCode,InterviewSlotId) values (newid(),'F2', 'rd@alburhanconnect.com','123456','00000000','35','Applicant','Registered','Declined','1234567890',2)
------insert into individual (Id,Name,Email,Password,Phone,Age,UserType,RegistrationStatus,PaymentStatus,PaymentCode,InterviewSlotId) values (newid(),'F3', 'rv@alburhanconnect.com','123456','00000000','35','Applicant','Registered','Verified','1234567890',3)

------insert into individual (Id,Name,Email,Password,Phone,Age,UserType,RegistrationStatus,PaymentStatus,PaymentCode,InterviewSlotId) values (newid(),'F4', 'uruv@alburhanconnect.com','123456','00000000','35','Applicant','UnRegistered','UnVerified','1234567890',4)
------insert into individual (Id,Name,Email,Password,Phone,Age,UserType,RegistrationStatus,PaymentStatus,PaymentCode,InterviewSlotId) values (newid(),'F5', 'urd@alburhanconnect.com','123456','00000000','35','Applicant','UnRegistered','Declined','1234567890',5)
------insert into individual (Id,Name,Email,Password,Phone,Age,UserType,RegistrationStatus,PaymentStatus,PaymentCode,InterviewSlotId) values (newid(),'F6', 'urv@alburhanconnect.com','123456','00000000','35','Applicant','UnRegistered','Verified','1234567890',6)

------insert into individual (Id,Name,Email,Password,Phone,Age,UserType,RegistrationStatus,PaymentStatus,PaymentCode,InterviewSlotId) values (newid(),'F7', 'cv@alburhanconnect.com','123456','00000000','35','Applicant','Completed','Verified','1234567890',7)
------insert into individual (Id,Name,Email,Password,Phone,Age,UserType,RegistrationStatus,PaymentStatus,PaymentCode,InterviewSlotId) values (newid(),'F8', 'lv@alburhanconnect.com','123456','00000000','35','Applicant','Locked','Verified','1234567890',8)

------insert into individual (Id,Name,Email,Password,Phone,Age,UserType,RegistrationStatus,PaymentStatus,PaymentCode,InterviewSlotId) values (newid(),'F7', 'cuv@alburhanconnect.com','123456','00000000','35','Applicant','Completed','UnVerified','1234567890',7)

----insert into individual (Id,Name,Email,Password,Phone,Age,UserType,RegistrationStatus) values (newid(),'Panel', 'panel@alburhanconnect.com','loveislam','00000000','35','Panel','Registered')
----insert into individual (Id,Name,Email,Password,Phone,Age,UserType,RegistrationStatus) values (newid(),'Ceo', 'ceo@alburhanconnect.com','loveislam','00000000','35','Ceo','Registered')

----insert into individual (Id,Name,Email,Password,Phone,Age,UserType,RegistrationStatus, interviewslotid, created) values (newid(),'Test for Panel Candidate', 'testforpanel@alburhanconnect.com','loveislam','00000000','35','Applicant','PanelInterviewed',999, getdate())
----insert into individual (Id,Name,Email,Password,Phone,Age,UserType,RegistrationStatus, interviewslotid, created, PaymentStatus) values (newid(),'Test for Panel Interviewed, Awaiting CEO Candidate', 'testforceo@alburhanconnect.com','loveislam','00000000','35','Applicant','CeoInterviewed',999, getdate(), 'Verified')


----create procedure GetApplicationsWithRejectedStatus
----as
----begin
----	select Id, Name, Email, IndividualId, Phone, Age, RegistrationStatus, Created 
----	from individual
----	where RegistrationStatus = 'Rejected'
----	and UserType = 'Applicant' 
----	and InterviewSlotId <> 0
----	order by InterviewSlotId asc
----end
----go

----create procedure GetApplicationsWithSelectedStatus
----as
----begin
----	select Id, Name, Email, IndividualId, Phone, Age, RegistrationStatus, Created 
----	from individual
----	where RegistrationStatus = 'Selected'
----	and UserType = 'Applicant' 
----	and InterviewSlotId <> 0
----	order by InterviewSlotId asc
----end
----go


---- ######## GRADING (ceo, panel)

--if not exists(select 1 from sys.columns c inner join sys.tables t on c.object_id = t.object_id where t.name = 'Individual' and c.name = 'CeoComments')
--begin
--	alter table Individual
--	add CeoComments ntext null
--end
--go

--if not exists(select 1 from sys.columns c inner join sys.tables t on c.object_id = t.object_id where t.name = 'Individual' and c.name = 'CeoGrade')
--begin
--	alter table Individual
--	add CeoGrade int null
--end
--go

--if exists (select 1 from sys.procedures where name = 'SelectCeoGrading') drop procedure SelectCeoGrading
--go
--create procedure SelectCeoGrading(@id uniqueidentifier) as begin select Name, CeoComments as Comments, CeoGrade as Grade from individual where id = @id end
--go

--if exists (select 1 from sys.procedures where name = 'UpdateCeoGrading') drop procedure UpdateCeoGrading
--go
--create procedure UpdateCeoGrading(@Id uniqueidentifier, @comments ntext, @grade int) as 
--begin 
--	update individual set ceocomments = @comments, ceograde = @grade where id = @id 
--	update individual set RegistrationStatus = 'CeoInterviewed' where id = @id --and RegistrationStatus = 'PanelInterviewed'
--end
--go

----if exists (select 1 from sys.tables where name = 'PanelGrading')
----begin 
----	delete from PanelGrading
----	drop table PanelGrading
----end
----go

----create table PanelGrading
----(
----	PanelGradingId int identity(1,1),
----	IndividualId int,
----	IntroProfileDescription varchar(2048),
----	IntroKeyAchievements varchar(2048),
----	MotivationReasonForEnrollment varchar(2048),
----	MotivationGoals varchar(2048),
----	MotivationIfNotSelected varchar(2048),
----	StrengthsTopTwo varchar(2048),
----	StrengthsHobbies varchar(2048),
----	CommittmentAnyCourseBefore varchar(2048),
----	CommittmentFulfillPromises varchar(2048),
----	CommittmentExample varchar(2048),
----	CommittmentFailures varchar(2048),
----	AdditionalSkillsAnyOther varchar(2048),
----	AdditionalSkillsSocial varchar(2048),
----	GradeCommitment int,
----	GradeStrengths int,
----	GradePassion int,
----	GradeFunctionalExpertise int,
----	GradeExistingAffiliation int,

----	Comments nvarchar(2048),
----	Grade int
----)
----GO


----if exists (select 1 from sys.procedures where name = 'SelectPanelGrading') drop procedure SelectPanelGrading
----go

----create procedure SelectPanelGrading
----(@id uniqueidentifier) 
----as 
----begin 
----	select 
----	i.[Name],
----	i.IndividualId,
----	IntroProfileDescription,
----	IntroKeyAchievements,
----	MotivationReasonForEnrollment,
----	MotivationGoals,
----	MotivationIfNotSelected,
----	StrengthsTopTwo,
----	StrengthsHobbies,
----	CommittmentAnyCourseBefore,
----	CommittmentFulfillPromises,
----	CommittmentExample,
----	CommittmentFailures,
----	AdditionalSkillsAnyOther,
----	AdditionalSkillsSocial,
----	GradeCommitment,
----	GradeStrengths,
----	GradePassion,
----	GradeFunctionalExpertise,
----	GradeExistingAffiliation,
----	Comments as PanelComments,
----	Grade as PanelGrade
	
----	from PanelGrading pg 
----	right outer join Individual i on i.IndividualId = pg.IndividualId 
----	where i.id = @id
----end
----GO

--	if exists (select 1 from sys.procedures where name = 'UpdatePanelGrading') drop procedure UpdatePanelGrading
--	go
--	create procedure UpdatePanelGrading
--	(
--		@Id uniqueidentifier, 
--		@IntroProfileDescription nvarchar(2048),
--		@IntroKeyAchievements nvarchar(2048),
--		@MotivationReasonForEnrollment nvarchar(2048),
--		@MotivationGoals nvarchar(2048),
--		@MotivationIfNotSelected nvarchar(2048),
--		@StrengthsTopTwo nvarchar(2048),
--		@StrengthsHobbies nvarchar(2048),
--		@CommittmentAnyCourseBefore nvarchar(2048),
--		@CommittmentFulfillPromises nvarchar(2048),
--		@CommittmentExample nvarchar(2048),
--		@CommittmentFailures nvarchar(2048),
--		@AdditionalSkillsAnyOther nvarchar(2048),
--		@AdditionalSkillsSocial nvarchar(2048),
--		@GradeCommitment int,
--		@GradeStrengths int,
--		@GradePassion int,
--		@GradeFunctionalExpertise int,
--		@GradeExistingAffiliation int,
--		@comments varchar(2048), 
--		@grade int
--	) 
--		as 
--		begin 
--			declare @IndividualId int 
--			select @IndividualId = IndividualId from individual where id = @id

--			if exists (select 1 from panelgrading where individualid = @individualid)
--			begin
--				update panelgrading 
--				set 
--				IntroProfileDescription			=	@IntroProfileDescription ,
--				IntroKeyAchievements			=	@IntroKeyAchievements ,
--				MotivationReasonForEnrollment	=	@MotivationReasonForEnrollment ,
--				MotivationGoals					=	@MotivationGoals ,
--				MotivationIfNotSelected			=	@MotivationIfNotSelected ,
--				StrengthsTopTwo					=	@StrengthsTopTwo ,
--				StrengthsHobbies				=	@StrengthsHobbies ,
--				CommittmentAnyCourseBefore		=	@CommittmentAnyCourseBefore ,
--				CommittmentFulfillPromises		=	@CommittmentFulfillPromises ,
--				CommittmentExample				=	@CommittmentExample ,
--				CommittmentFailures				=	@CommittmentFailures ,
--				AdditionalSkillsAnyOther		=	@AdditionalSkillsAnyOther ,
--				AdditionalSkillsSocial			=	@AdditionalSkillsSocial ,
--				GradeCommitment					=	@GradeCommitment ,
--				GradeStrengths					=	@GradeStrengths ,
--				GradePassion					=	@GradePassion ,
--				GradeFunctionalExpertise		=	@GradeFunctionalExpertise ,
--				GradeExistingAffiliation		=	@GradeExistingAffiliation ,
--				comments						=	@comments, 
--				grade							=	@grade 
			
--				where Individualid = @IndividualId 
--			end
--			else
--			begin 
--				insert into panelgrading 
--				(
--					individualid, IntroProfileDescription ,
--					IntroKeyAchievements ,
--					MotivationReasonForEnrollment ,
--					MotivationGoals ,
--					MotivationIfNotSelected ,
--					StrengthsTopTwo ,
--					StrengthsHobbies ,
--					CommittmentAnyCourseBefore ,
--					CommittmentFulfillPromises ,
--					CommittmentExample ,
--					CommittmentFailures ,
--					AdditionalSkillsAnyOther ,
--					AdditionalSkillsSocial ,
--					GradeCommitment ,
--					GradeStrengths ,
--					GradePassion ,
--					GradeFunctionalExpertise ,
--					GradeExistingAffiliation ,
--					comments, 
--					grade 
--				) values 
--				(
--					@individualid, 
--					@IntroProfileDescription ,
--					@IntroKeyAchievements ,
--					@MotivationReasonForEnrollment ,
--					@MotivationGoals ,
--					@MotivationIfNotSelected ,
--					@StrengthsTopTwo ,
--					@StrengthsHobbies ,
--					@CommittmentAnyCourseBefore ,
--					@CommittmentFulfillPromises ,
--					@CommittmentExample ,
--					@CommittmentFailures ,
--					@AdditionalSkillsAnyOther ,
--					@AdditionalSkillsSocial ,
--					@GradeCommitment ,
--					@GradeStrengths ,
--					@GradePassion ,
--					@GradeFunctionalExpertise ,
--					@GradeExistingAffiliation ,
--					@comments, 
--					@grade 
--				)
--			end

--			if exists (select 1 from individual where id = @id and RegistrationStatus IN ('UnRegistered','Registered','Completed'))
--			begin 
--				update individual set RegistrationStatus = 'PanelInterviewed' where id = @Id 
--			end
--		end
--	go






--if exists (select 1 from sys.procedures where name = 'GetIndividualBasicDetailsById')
--drop procedure GetIndividualBasicDetailsById
--go

--CREATE procedure [dbo].[GetIndividualBasicDetailsById]
--(
--	@Id uniqueidentifier
--)
--as
--begin
--	select Id, [Name], [Password], RegistrationStatus, UserType, Age, PaymentStatus, PaymentCode, PaymentMethod, Name from individual where Id = @Id

--end
--go

--if exists (select 1 from sys.procedures where name = 'UpdatePaymentDetails')
--drop procedure UpdatePaymentDetails
--go

--create procedure UpdatePaymentDetails
--(
--	@Email NVARCHAR(128),
--	@PaymentMethod NVARCHAR(32),
--	@PaymentCode NVARCHAR(32),
--	@PaymentStatus VARCHAR(32),
--	@Id uniqueidentifier = null
--)
--as
--begin
--	if @Email <> ''
--	begin
--		update individual set PaymentMethod = @PaymentMethod, PaymentCode = @PaymentCode, PaymentStatus = @PaymentStatus
--		where Email = @Email
--	end

--	update individual set PaymentMethod = @PaymentMethod, PaymentCode = @PaymentCode, PaymentStatus = @PaymentStatus
--	where Id = @Id
--end
--go

--if exists (select 1 from sys.procedures where name = 'GetIndividualDetailsById')
--drop procedure GetIndividualDetailsById
--go

--create procedure GetIndividualDetailsById
--(
--	@Id uniqueidentifier
--)
--as
--begin
--	select i.*, g.Grade as PanelGrade from Individual i
--	left outer join PanelGrading g on i.individualid = g.individualid
--	where Id = @Id
--end
--GO





----alter table individual 
----add EmailSent bit null

----alter table individual 
----add Section nvarchar(64) null





--use alburhan_batch5
--go

--if exists (select 1 from sys.procedures where name = 'UpdateIndividualSection')
--drop procedure UpdateIndividualSection
--go

--create procedure UpdateIndividualSection
--(
--	@IndividualId int,
--	@Section nvarchar(64)
--)
--as
--begin
--	update individual set Section = @Section where individualId = @individualId
--end
--go





--use alburhan_batch5
--go

--if exists (select 1 from sys.procedures where name = 'UpdateIndividualEnrolmentEmailSentStatus')
--drop procedure UpdateIndividualEnrolmentEmailSentStatus
--go

--create procedure UpdateIndividualEnrolmentEmailSentStatus
--(
--	@IndividualId int
--)
--as
--begin
--	update individual set EmailSent = 1 where individualId = @individualId
--	select Email + ',' + isnull(Section,'') + ',' + convert(nvarchar(32), RollNumber) from Individual where IndividualId = @IndividualId
--end
--go





--use alburhan_batch5
--go








--use alburhan_batch5
--go

--if exists (select 1 from sys.procedures where name = 'UpdateIndividualSection')
--drop procedure UpdateIndividualSection
--go

--create procedure UpdateIndividualSection
--(
--	@IndividualId int,
--	@Section nvarchar(64)
--)
--as
--begin
--	update individual set Section = @Section where individualId = @individualId
--end
--go




-- New Stuff 2019 - Management etc plus more
-- New Stuff 2019 - Management etc plus more
-- New Stuff 2019 - Management etc plus more
-- New Stuff 2019 - Management etc plus more
-- New Stuff 2019 - Management etc plus more
-- New Stuff 2019 - Management etc plus more
-- New Stuff 2019 - Management etc plus more
-- New Stuff 2019 - Management etc plus more
-- New Stuff 2019 - Management etc plus more
-- New Stuff 2019 - Management etc plus more
-- New Stuff 2019 - Management etc plus more
-- New Stuff 2019 - Management etc plus more
-- New Stuff 2019 - Management etc plus more
-- New Stuff 2019 - Management etc plus more
-- New Stuff 2019 - Management etc plus more
-- New Stuff 2019 - Management etc plus more
-- New Stuff 2019 - Management etc plus more
-- New Stuff 2019 - Management etc plus more
-- New Stuff 2019 - Management etc plus more
-- New Stuff 2019 - Management etc plus more
-- New Stuff 2019 - Management etc plus more
-- New Stuff 2019 - Management etc plus more
-- New Stuff 2019 - Management etc plus more
-- New Stuff 2019 - Management etc plus more
-- New Stuff 2019 - Management etc plus more
-- New Stuff 2019 - Management etc plus more
-- New Stuff 2019 - Management etc plus more
-- New Stuff 2019 - Management etc plus more
-- New Stuff 2019 - Management etc plus more
-- New Stuff 2019 - Management etc plus more
-- New Stuff 2019 - Management etc plus more
-- New Stuff 2019 - Management etc plus more
-- New Stuff 2019 - Management etc plus more
-- New Stuff 2019 - Management etc plus more
-- New Stuff 2019 - Management etc plus more
-- New Stuff 2019 - Management etc plus more
-- New Stuff 2019 - Management etc plus more
-- New Stuff 2019 - Management etc plus more
-- New Stuff 2019 - Management etc plus more
-- New Stuff 2019 - Management etc plus more



-- create 8 users who can do things
-- a new usertype
--insert into individual (Id,Name,Email,Password,Phone,Age,UserType,RegistrationStatus) values (newid(),'Ameen Ahsan', 'ameen_ahsan@alburhanconnect.com','manager5a','00000','0','Manager','Registered')
--insert into individual (Id,Name,Email,Password,Phone,Age,UserType,RegistrationStatus) values (newid(),'Dr Rizwan', 'dr_rizwan@alburhanconnect.com','manager5b','00000','0','Manager','Registered')
--insert into individual (Id,Name,Email,Password,Phone,Age,UserType,RegistrationStatus) values (newid(),'Zahir Akhtar', 'zahir_akhtar@alburhanconnect.com','manager5c','00000','0','Manager','Registered')
--insert into individual (Id,Name,Email,Password,Phone,Age,UserType,RegistrationStatus) values (newid(),'Behzad Zahid', 'behzad_zahid@alburhanconnect.com','manager5d','00000','0','Manager','Registered')

--insert into individual (Id,Name,Email,Password,Phone,Age,UserType,RegistrationStatus) values (newid(),'Colonel Younis', 'colonel_younis@alburhanconnect.com','manager5e','00000','0','Manager','Registered')
--insert into individual (Id,Name,Email,Password,Phone,Age,UserType,RegistrationStatus) values (newid(),'Shoaib', 'shoaib_j@alburhanconnect.com','manager5f','00000','0','Manager','Registered')
--insert into individual (Id,Name,Email,Password,Phone,Age,UserType,RegistrationStatus) values (newid(),'Fawad', 'm_fawad@alburhanconnect.com','manager5j','00000','0','Manager','Registered')
--insert into individual (Id,Name,Email,Password,Phone,Age,UserType,RegistrationStatus) values (newid(),'Ameer Sahab', 'ameer@alburhanconnect.com','manager5h','00000','0','Manager','Registered')

---- Notes function
--use alburhan_batch5
--go


--if exists (select 1 from sys.tables where name = 'Note')
--begin
--	delete from Note
--	drop table Note
--end
--go

--create table Note
--(
--	NoteId INT IDENTITY(1,1),
--	IndividualId INT,
--	NoteText nvarchar(2048),
--	MarkedAsProcessed BIT,
--	Created DATETIME
--)
--GO


--if exists (select 1 from sys.procedures where name = 'SaveNoteForIndividual')
--drop procedure SaveNoteForIndividual
--go

--if exists (select 1 from sys.procedures where name = 'GetNotesForIndividual')
--drop procedure GetNotesForIndividual
--go

--create procedure GetNotesForIndividual
--(
--	@Id uniqueidentifier
--)
--AS
--BEGIN
--	select NoteId, NoteText, MarkedAsProcessed, i.IndividualId, n.Created, i.id 
--	from Note n 
--	inner join Individual i on i.IndividualId = n.IndividualId 
--	where i.id = @Id ORDER BY n.created desc
--END
--GO

--if exists (select 1 from sys.procedures where name = 'SaveNoteForIndividual')
--drop procedure SaveNoteForIndividual
--go

--create procedure SaveNoteForIndividual
--(
--	@IndividualId INT,
--	@NoteText nvarchar(2048)
--)
--AS
--BEGIN
--	insert into Note values (@IndividualId, @NoteText, 0, dateadd(hour,5, getutcdate()))
--END
--GO

--if exists (select 1 from sys.procedures where name = 'UpdateNoteForIndividual')
--drop procedure UpdateNoteForIndividual
--go

--create procedure UpdateNoteForIndividual
--(
--	@NoteId INT,
--	@NoteText nvarchar(2048),
--	@MarkAsProcessed BIT
--)
--AS
--BEGIN
--	update Note set NoteText = @NoteText, MarkedAsProcessed = @MarkAsProcessed where NoteId = @NoteId
--END
--GO


--if exists (select 1 from sys.procedures where name = 'DeleteNote')
--drop procedure DeleteNote
--go

--create procedure DeleteNote
--(
--	@NoteId INT
--)
--AS
--BEGIN
--	delete from Note where NoteId = @NoteId
--END
--GO


---- 15th January 2019
----------------------------------------------------------------------------------------------------------------------------------------------------------------------

--if exists (select 1 from sys.procedures where name = 'UpdateIndividualDetails')
--drop procedure UpdateIndividualDetails
--go

--CREATE PROCEDURE UpdateIndividualDetails
--(
--	@Id uniqueidentifier,
--	@NicNumber NVARCHAR(50) NULL,
--	@OtherContact NVARCHAR(20) NULL,
--	@MaritalStatus NVARCHAR(8) NULL,
--	@ResidentialAddress NVARCHAR(1024) NULL,
--	@PermanentAddress NVARCHAR(1024) NULL,

--	@MatricGpa INT NULL,
--	@MatricSubjects NVARCHAR(1024) NULL,
--	@MatricInstitute NVARCHAR(512) NULL,

--	@IntermediateGpa INT NULL,
--	@IntermediateSubjects NVARCHAR(1024) NULL,
--	@IntermediateInstitute NVARCHAR(512) NULL,

--	@GraduationGpa INT NULL,
--	@GraduationSubjects NVARCHAR(1024) NULL,
--	@GraduationInstitute NVARCHAR(512) NULL,

--	@MastersGpa INT NULL,
--	@MastersSubjects NVARCHAR(1024) NULL,
--	@MastersInstitution NVARCHAR(512) NULL,

--	@MsGpa INT NULL,
--	@MsSubjects NVARCHAR(1024) NULL,
--	@MsInstitution NVARCHAR(512) NULL,

--	@PhdSubjects NVARCHAR(1024) NULL,
--	@PhdInstitution NVARCHAR(512) NULL,

--	@OtherQualification NVARCHAR(512) NULL,

--	@CurrentJob NVARCHAR(128) NULL,
--	@CompanyName NVARCHAR(128) NULL,
--	@Designation NVARCHAR(128) NULL,
--	@TotalJobExperience NVARCHAR(64) NULL,

--	@BusinessName NVARCHAR(128) NULL,
--	@BusinessArea NVARCHAR(128) NULL,
--	@BusinessNature NVARCHAR(128) NULL,

--	@AppliedBefore BIT NULL,
--	@AnyReligiousEducation BIT NULL,
--	@ReligiousEducationSpecify NVARCHAR(128) NULL,
--	@AnyOtherExpertise NVARCHAR(128) NULL,
--	@CurrentStatus NVARCHAR(32) NULL,
--	@Education NVARCHAR(32) NULL,
--	@RegistrationStatus VARCHAR(32) NULL,
--	@InterviewSlotId INT NULL,
--	@Name NVARCHAR(128) NULL,
--	@Phone NVARCHAR(20) NULL
--)
--as
--begin
--	UPDATE Individual
--	SET 
--      [NicNumber] = @NicNumber
--      ,[OtherContact] = @OtherContact
--      ,[MaritalStatus] = @MaritalStatus
--      ,[ResidentialAddress] = @ResidentialAddress
--      ,[PermanentAddress] = @PermanentAddress
--      ,[MatricGpa] = @MatricGpa
--      ,[MatricSubjects] = @MatricSubjects
--      ,[MatricInstitute] = @MatricInstitute
--      ,[IntermediateGpa] = @IntermediateGpa
--      ,[IntermediateSubjects] = @IntermediateSubjects
--      ,[IntermediateInstitute] = @IntermediateInstitute
--      ,[GraduationGpa] = @GraduationGpa
--      ,[GraduationSubjects] = @GraduationSubjects
--      ,[GraduationInstitute] = @GraduationInstitute
--      ,[MastersGpa] = @MastersGpa
--      ,[MastersSubjects] = @MastersSubjects
--      ,[MastersInstitution] = @MastersInstitution
--      ,[MsGpa] = @MsGpa
--      ,[MsSubjects] = @MsSubjects
--      ,[MsInstitution] = @MsInstitution
--      ,[PhdSubjects] = @PhdSubjects
--      ,[PhdInstitution] = @PhdInstitution
--      ,[OtherQualification] = @OtherQualification
--      ,[CurrentJob] = @CurrentJob
--      ,[CompanyName] = @CompanyName
--      ,[Designation] = @Designation
--      ,[TotalJobExperience] = @TotalJobExperience
--      ,[BusinessName] = @BusinessName
--      ,[BusinessArea] = @BusinessArea
--      ,[BusinessNature] = @BusinessNature
--      ,[AppliedBefore] = @AppliedBefore
--      ,[AnyReligiousEducation] = @AnyReligiousEducation
--      ,[ReligiousEducationSpecify] = @ReligiousEducationSpecify
--      ,[AnyOtherExpertise] = @AnyOtherExpertise
--	  ,[CurrentStatus] = @CurrentStatus
--	  ,[Education] = @Education
--	  ,RegistrationStatus = @RegistrationStatus
--	  ,InterviewSlotId = @InterviewSlotId
--	  ,Name = @Name
--	  ,Phone = @Phone
-- WHERE Id = @Id

--end
--GO




*/

-- 18th JAN 2019 
-- need the new column
---- need new logins
--use alburhan_batch5
--go

--alter table individual
--add Batch nvarchar(128)
--go

--alter table individual
--add FatherOrHusbandName nvarchar(128)
--go

--alter table individual
--add ClassSelection nvarchar(256)
--go

--alter table individual
--add WhatsAppNumber nvarchar(32)
--go

--alter table individual
--add EducationDetails nvarchar(512)
--go

--alter table individual
--add Responsibilities nvarchar(32)
--go

--alter table individual
--add ProfessionStudyBusinessDetails nvarchar(512)
--go

--alter table individual
--add CityDetails nvarchar(512)
--go

---- one off
--update individual set Batch = 'ISB-2019-B005' 
--go

--if exists (select 1 from sys.procedures where name = 'GetPaymentsPendingVerificationAndDeclined')
--drop procedure GetPaymentsPendingVerificationAndDeclined
--go

--create procedure GetPaymentsPendingVerificationAndDeclined
--(
--	@batch nvarchar(128)
--)
--as
--begin
--	select Id, Name, Email, IndividualId, PaymentCode, PaymentMethod, Phone, Password, PaymentStatus, Age, CurrentStatus, RegistrationStatus, Created,
--	s.Slot 
--	from individual i
--	left outer join InterviewSlot s on s.InterviewSlotId = i.InterviewslotId
--	where PaymentStatus IN ('UnVerified','Declined','Verified','Blocked')
--	and PaymentCode IS NOT NULL
--	and UserType = 'Applicant' 
--	and batch = @batch
-- order by Created desc
--end
--go

--use alburhan_batch5
--go

--if exists (select 1 from sys.procedures where name = 'GetIndividualBasicDetailsByEmail')
--drop procedure GetIndividualBasicDetailsByEmail
--go

--create procedure GetIndividualBasicDetailsByEmail
--(
--	@Email NVARCHAR(256)
--)
--as
--begin
--	select Id, [Password], RegistrationStatus, UserType, Age, PaymentStatus, PaymentCode, PaymentMethod, Name, Batch from individual where Email = @Email
--end
--GO


--if exists (select 1 from sys.procedures where name = 'GetCompletedApplicationForIntervieweesForCeo')
--drop procedure GetCompletedApplicationForIntervieweesForCeo
--go

--create procedure GetCompletedApplicationForIntervieweesForCeo
--(
--	@batch nvarchar(128)
--)
--as
--begin
--	select Id, Name, Email, i.IndividualId, Phone, Age, RegistrationStatus, Created, s.slot as InterviewDateAndTime,
--	pg.Grade as PanelGrade, i.CeoGrade as CeoGrade
--	from individual i
--	left outer join interviewslot s on i.InterviewSlotId = s.InterviewSlotId
--	left outer join panelgrading pg on pg.individualid = i.individualid
--	where PaymentStatus = 'Verified'

----	where RegistrationStatus IN ('PanelInterviewed', 'CeoInterviewed')
--	and UserType = 'Applicant' 
----	and i.InterviewSlotId <> 0
--	and batch = @batch
--	order by i.InterviewSlotId asc
--end
--go


--if exists (select 1 from sys.procedures where name = 'GetCompletedApplicationForIntervieweesForPanel')
--drop procedure GetCompletedApplicationForIntervieweesForPanel
--go

--create procedure GetCompletedApplicationForIntervieweesForPanel
--(@batch nvarchar(128))
--as
--begin
--	select Id, Name, RegistrationStatus, Email, i.IndividualId, Phone, Age, NicNumber, RegistrationStatus, Created, s.slot as InterviewDateAndTime, PaymentStatus,
--	pg.Grade as PanelGrade, i.CeoGrade as CeoGrade
--	from individual i
--	left outer join interviewslot s on i.InterviewSlotId = s.InterviewSlotId
--	left outer join panelgrading pg on pg.individualid = i.individualid
--	where PaymentStatus = 'Verified'
--	and RegistrationStatus IN ('UnRegistered','Registered','Completed','PanelInterviewed','CeoInterviewed')
--	and UserType = 'Applicant' 
--	and i.InterviewSlotId <> 0
--	and i.InterviewSlotId is not null
--	and batch = @batch
--	order by i.InterviewSlotId asc
--end
--go


--if exists (select 1 from sys.procedures where name = 'UpdateIndividualDetails')
--drop procedure UpdateIndividualDetails
--go

--CREATE PROCEDURE UpdateIndividualDetails
--(
--	@Id uniqueidentifier,
--	@NicNumber NVARCHAR(50) NULL,
--	@OtherContact NVARCHAR(20) NULL,
--	@MaritalStatus NVARCHAR(8) NULL,
--	@ResidentialAddress NVARCHAR(1024) NULL,
--	@PermanentAddress NVARCHAR(1024) NULL,

--	@MatricGpa INT NULL,
--	@MatricSubjects NVARCHAR(1024) NULL,
--	@MatricInstitute NVARCHAR(512) NULL,

--	@IntermediateGpa INT NULL,
--	@IntermediateSubjects NVARCHAR(1024) NULL,
--	@IntermediateInstitute NVARCHAR(512) NULL,

--	@GraduationGpa INT NULL,
--	@GraduationSubjects NVARCHAR(1024) NULL,
--	@GraduationInstitute NVARCHAR(512) NULL,

--	@MastersGpa INT NULL,
--	@MastersSubjects NVARCHAR(1024) NULL,
--	@MastersInstitution NVARCHAR(512) NULL,

--	@MsGpa INT NULL,
--	@MsSubjects NVARCHAR(1024) NULL,
--	@MsInstitution NVARCHAR(512) NULL,

--	@PhdSubjects NVARCHAR(1024) NULL,
--	@PhdInstitution NVARCHAR(512) NULL,

--	@OtherQualification NVARCHAR(512) NULL,

--	@CurrentJob NVARCHAR(128) NULL,
--	@CompanyName NVARCHAR(128) NULL,
--	@Designation NVARCHAR(128) NULL,
--	@TotalJobExperience NVARCHAR(64) NULL,

--	@BusinessName NVARCHAR(128) NULL,
--	@BusinessArea NVARCHAR(128) NULL,
--	@BusinessNature NVARCHAR(128) NULL,

--	@AppliedBefore BIT NULL,
--	@AnyReligiousEducation BIT NULL,
--	@ReligiousEducationSpecify NVARCHAR(128) NULL,
--	@AnyOtherExpertise NVARCHAR(128) NULL,
--	@CurrentStatus NVARCHAR(32) NULL,
--	@Education NVARCHAR(32) NULL,
--	@RegistrationStatus VARCHAR(32) NULL,
--	@InterviewSlotId INT NULL,
--	@Name NVARCHAR(128) NULL,
--	@Phone NVARCHAR(20) NULL,
--    @FatherOrHusbandName nvarchar(32) null,
--    @WhatsAppNumber NVARCHAR(20) NULL,
--    @EducationDetails NVARCHAR(512) NULL,
--    @ProfessionStudyBusinessDetails NVARCHAR(52) NULL
--)
--as
--begin
--	UPDATE Individual
--	SET 
--      [NicNumber] = @NicNumber
--      ,[OtherContact] = @OtherContact
--      ,[MaritalStatus] = @MaritalStatus
--      ,[ResidentialAddress] = @ResidentialAddress
--      ,[PermanentAddress] = @PermanentAddress
--      ,[MatricGpa] = @MatricGpa
--      ,[MatricSubjects] = @MatricSubjects
--      ,[MatricInstitute] = @MatricInstitute
--      ,[IntermediateGpa] = @IntermediateGpa
--      ,[IntermediateSubjects] = @IntermediateSubjects
--      ,[IntermediateInstitute] = @IntermediateInstitute
--      ,[GraduationGpa] = @GraduationGpa
--      ,[GraduationSubjects] = @GraduationSubjects
--      ,[GraduationInstitute] = @GraduationInstitute
--      ,[MastersGpa] = @MastersGpa
--      ,[MastersSubjects] = @MastersSubjects
--      ,[MastersInstitution] = @MastersInstitution
--      ,[MsGpa] = @MsGpa
--      ,[MsSubjects] = @MsSubjects
--      ,[MsInstitution] = @MsInstitution
--      ,[PhdSubjects] = @PhdSubjects
--      ,[PhdInstitution] = @PhdInstitution
--      ,[OtherQualification] = @OtherQualification
--      ,[CurrentJob] = @CurrentJob
--      ,[CompanyName] = @CompanyName
--      ,[Designation] = @Designation
--      ,[TotalJobExperience] = @TotalJobExperience
--      ,[BusinessName] = @BusinessName
--      ,[BusinessArea] = @BusinessArea
--      ,[BusinessNature] = @BusinessNature
--      ,[AppliedBefore] = @AppliedBefore
--      ,[AnyReligiousEducation] = @AnyReligiousEducation
--      ,[ReligiousEducationSpecify] = @ReligiousEducationSpecify
--      ,[AnyOtherExpertise] = @AnyOtherExpertise
--	  ,[CurrentStatus] = @CurrentStatus
--	  ,[Education] = @Education
--	  ,RegistrationStatus = @RegistrationStatus
--	  ,InterviewSlotId = @InterviewSlotId
--	  ,Name = @Name
--	  ,Phone = @Phone
--		,FatherOrHusbandName = @FatherOrHusbandName
--		,WhatsAppNumber = @WhatsAppNumber
--		,EducationDetails = @EducationDetails
--		,ProfessionStudyBusinessDetails = @ProfessionStudyBusinessDetails

-- WHERE Id = @Id

--end
--GO

--delete from interviewslot where Slot in ('26th January 2019', '27th January 2019','None')
--update interviewslot set State = 'InActive'
--insert into interviewslot values ('26th January 2019', 'Active',0,0)
--insert into interviewslot values ('27th January 2019', 'Active',0,0)
--insert into interviewslot values ('None', 'Active',0,0)

--if exists (select 1 from sys.procedures where name = 'GetAvailableInterviewSlots')
--drop procedure GetAvailableInterviewSlots
--go

--create procedure GetAvailableInterviewSlots
--as
--begin
--	select s.InterviewSlotId, Slot from InterviewSlot s
--	left outer join Individual i on i.InterviewSlotId = s.InterviewSlotId and i.UserType = 'Applicant' 
--	where s.State = 'Active'
--	group by s.InterviewSlotId, Slot
--	having count(s.InterviewSlotId) < 25
--	order by InterviewSlotId
--end
--GO

--insert into individual (Id, Name, Email,Password,UserType,RegistrationStatus,Batch)
--values (newid(),'Payment Admin W','payments_w@alburhanconnect.com','skip3rdexit','PaymentAdministrator','Registered','ISB-2019-B001-Females')

--insert into individual (Id, Name, Email,Password,UserType,RegistrationStatus,Batch)
--values (newid(),'Ceo W','ceo_w@alburhanconnect.com','loveislam','Ceo','Registered','ISB-2019-B001-Females')

--insert into individual (Id, Name, Email,Password,UserType,RegistrationStatus,Batch)
--values (newid(),'Panel W','panel_w@alburhanconnect.com','loveislam','Panel','Registered','ISB-2019-B001-Females')





--use alburhan_batch5
--go


--if exists (select 1 from sys.procedures where name = 'CreateOrUpdateBasicIndividual')
--drop procedure CreateOrUpdateBasicIndividual
--go

--create procedure CreateOrUpdateBasicIndividual
--(
--	@Id uniqueidentifier,
--	@Name NVARCHAR(128),
--	@Email NVARCHAR(128),
--	@Password NVARCHAR(12),
--	@Phone NVARCHAR(20),
--	@Age INT,
--	@UserType NVARCHAR(30),
--	@RegistrationStatus NVARCHAR(32),
--	@PaymentMethod NVARCHAR(32),
--	@PaymentCode NVARCHAR(32),
--	@PaymentStatus VARCHAR(32),
--	@InterviewSlotId INT,
--	@Batch NVARCHAR(64)
--)
--as
--begin
--	if @InterviewSlotId = 999
--	begin 
--		set @InterviewSlotId = 1030
--	end

--	if exists (select 1 from individual where email = @email)
--	begin
--		update individual set Id = @Id, Name = @Name, [Password] = @Password, Phone = @Phone, Age = @Age, UserType = @UserType,
--		RegistrationStatus = @RegistrationStatus, PaymentMethod = @PaymentMethod, PaymentCode = @PaymentCode, PaymentStatus = @PaymentStatus,
--		InterviewSlotId = @InterviewSlotId, Batch = @Batch
--		where Email = @Email
--	end
--	else
--	begin
--		insert into Individual (Id, Name, Email, [Password], Phone, Age, UserType, RegistrationStatus, PaymentMethod, PaymentCode, PaymentStatus, InterviewSlotId, Created, Batch)
--		values (@Id, @Name, @Email, @Password, @Phone, @Age, @UserType, @RegistrationStatus, @PaymentMethod, @PaymentCode, @PaymentStatus, @InterviewSlotId, dateadd(hour,5,getutcdate()), @Batch)
--	end
--end
--go









--========================= APPLY TO LIVE



--USE [alburhan_batch5]
--GO

--if exists (select 1 from sys.procedures where name = 'GetCeoInterviewedCandidatesForFinalDecision')
--drop procedure GetCeoInterviewedCandidatesForFinalDecision
--go

--create  procedure [dbo].[GetCeoInterviewedCandidatesForFinalDecision]
--(
--	@batch nvarchar(128)
--)
--as
--begin
--	select Id, Name, Email, I.IndividualId, Phone, Age, RegistrationStatus, Created, CeoComments, CeoGrade, 
--	EmailSent, 
--	ISNULL(Section, '') as Section, 
--	RollNumber,
--	P.Grade as PanelGrade
--	from alburhan_user.Individual I
--	left outer join PanelGrading P on I.IndividualId = P.IndividualId
--	where RegistrationStatus IN ('CeoInterviewed','Enrolled','Rejected','Completed','PanelInterviewed','Dropped')
--	and UserType = 'Applicant' 
--	and CeoGrade in (1,2,3,4,5,6,7,8)
--	AND Batch = @batch
--	order by case section 
--					when 'Morning-5A' then 1
--					when 'Morning-5B' then 2
--					when 'Morning-5C' then 3
--					when 'Morning-5D' then 4
--					when 'Morning-5E' then 5
--					when 'Morning-5F' then 6
--					when 'Morning-5G' then 7
--					when 'Morning-5H' then 8
--					when 'Evening-5I' then 9
--					when 'Evening-5J' then 10
--					when 'Evening-5K' then 11
--					when 'Evening-5L' then 12
--					else 13
--			end
--	, [Name] asc
--end
--GO


--use alburhan_batch5
--go


--if exists (select 1 from sys.procedures where name = 'GetNotesForIndividualById')
--drop procedure dbo.GetNotesForIndividualById
--go

--create procedure [dbo].[GetNotesForIndividualById]
--(
--	@IndividualId int
--)
--AS
--BEGIN
--	select NoteId, NoteText, MarkedAsProcessed, IndividualId, n.Created 
--	from alburhan_user.Note n 
--	where individualId = @IndividualId ORDER BY n.created desc
--END
--GO








--if exists (select 1 from sys.procedures where name = 'GetIndividualsBySection')
--drop procedure GetIndividualsBySection
--go

--CREATE procedure [dbo].[GetIndividualsBySection]
--(
--	@Batch nvarchar(16),
--	@Section nvarchar(4)
--)
--as
--begin
--	select IndividualId, [dbo].[fProperCase]([Name],'|APT|HWY|BOX|',NULL) as [Name],Email, Batch + '-' + convert(nvarchar, RollNumber) as RollNumber
--	from alburhan_user.individual
--	where Batch = @Batch
--	and substring(Section,LEN(Section),LEN(Section)-1) = @Section
--	and UserType = 'Applicant'
--	and RegistrationStatus = 'Enrolled'
--	order by [Name] asc
--end
--go



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


