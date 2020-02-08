--if exists (select 1 from sys.tables where name = 'InterviewSlot')
--begin 
--	delete from InterviewSlot
--	drop table InterviewSlot
--end
--go

--create table InterviewSlot
--(
--	InterviewSlotId int identity(1,1),
--	Slot NVARCHAR(512),
--	[State] NVARCHAR(32),
--	MinAge INT,
--	MaxAge INT
--)
--GO

--insert into InterviewSlot(Slot,MinAge,MaxAge) values ('Sunday 22-04-2018 from 09:00 AM to 09:30 AM ',0,0)
--insert into InterviewSlot(Slot,MinAge,MaxAge) values ('Sunday 22-04-2018 from 09:30 AM to 10:00 AM ',0,0)
--insert into InterviewSlot(Slot,MinAge,MaxAge) values ('Sunday 22-04-2018 from 10:00 AM to 10:30 AM ',0,0)
--insert into InterviewSlot(Slot,MinAge,MaxAge) values ('Sunday 22-04-2018 from 10:30 AM to 11:00 AM ',0,0)
--insert into InterviewSlot(Slot,MinAge,MaxAge) values ('Sunday 22-04-2018 from 11:00 AM to 11:30 AM ',0,0)
--insert into InterviewSlot(Slot,MinAge,MaxAge) values ('Sunday 22-04-2018 from 11:30 AM to 12:00 PM ',0,0)
--insert into InterviewSlot(Slot,MinAge,MaxAge) values ('Sunday 22-04-2018 from 12:00 PM to 12:30 PM ',0,0)
--insert into InterviewSlot(Slot,MinAge,MaxAge) values ('Sunday 22-04-2018 from 12:30 PM to 01:00 PM ',0,0)
--insert into InterviewSlot(Slot,MinAge,MaxAge) values ('Sunday 22-04-2018 from 02:00 PM to 02:30 PM ',0,0)
--insert into InterviewSlot(Slot,MinAge,MaxAge) values ('Sunday 22-04-2018 from 02:30 PM to 03:00 PM ',0,0)
--insert into InterviewSlot(Slot,MinAge,MaxAge) values ('Sunday 22-04-2018 from 03:00 PM to 03:30 PM ',0,0)
--insert into InterviewSlot(Slot,MinAge,MaxAge) values ('Sunday 22-04-2018 from 03:30 PM to 04:00 PM ',0,0)
--insert into InterviewSlot(Slot,MinAge,MaxAge) values ('Sunday 22-04-2018 from 04:00 PM to 04:30 PM ',0,0)
--insert into InterviewSlot(Slot,MinAge,MaxAge) values ('Sunday 22-04-2018 from 04:30 PM to 05:00 PM ',0,0)

--insert into InterviewSlot(Slot,MinAge,MaxAge) values ('Saturday 28-04-2018 from 09:00 AM to 09:30 AM ',0,0)
--insert into InterviewSlot(Slot,MinAge,MaxAge) values ('Saturday 28-04-2018 from 09:30 AM to 10:00 AM ',0,0)
--insert into InterviewSlot(Slot,MinAge,MaxAge) values ('Saturday 28-04-2018 from 10:00 AM to 10:30 AM ',0,0)
--insert into InterviewSlot(Slot,MinAge,MaxAge) values ('Saturday 28-04-2018 from 10:30 AM to 11:00 AM ',0,0)
--insert into InterviewSlot(Slot,MinAge,MaxAge) values ('Saturday 28-04-2018 from 11:00 AM to 11:30 AM ',0,0)
--insert into InterviewSlot(Slot,MinAge,MaxAge) values ('Saturday 28-04-2018 from 11:30 AM to 12:00 PM ',0,0)
--insert into InterviewSlot(Slot,MinAge,MaxAge) values ('Saturday 28-04-2018 from 12:00 PM to 12:30 PM ',0,0)
--insert into InterviewSlot(Slot,MinAge,MaxAge) values ('Saturday 28-04-2018 from 12:30 PM to 01:00 PM ',0,0)
--insert into InterviewSlot(Slot,MinAge,MaxAge) values ('Saturday 28-04-2018 from 02:00 PM to 02:30 PM ',0,0)
--insert into InterviewSlot(Slot,MinAge,MaxAge) values ('Saturday 28-04-2018 from 02:30 PM to 03:00 PM ',0,0)
--insert into InterviewSlot(Slot,MinAge,MaxAge) values ('Saturday 28-04-2018 from 03:00 PM to 03:30 PM ',0,0)
--insert into InterviewSlot(Slot,MinAge,MaxAge) values ('Saturday 28-04-2018 from 03:30 PM to 04:00 PM ',0,0)
--insert into InterviewSlot(Slot,MinAge,MaxAge) values ('Saturday 28-04-2018 from 04:00 PM to 04:30 PM ',0,0)
--insert into InterviewSlot(Slot,MinAge,MaxAge) values ('Saturday 28-04-2018 from 04:30 PM to 05:00 PM ',0,0)

--insert into InterviewSlot(Slot,MinAge,MaxAge) values ('Sunday 29-04-2018 from 09:00 AM to 09:30 AM ',0,0)
--insert into InterviewSlot(Slot,MinAge,MaxAge) values ('Sunday 29-04-2018 from 09:30 AM to 10:00 AM ',0,0)
--insert into InterviewSlot(Slot,MinAge,MaxAge) values ('Sunday 29-04-2018 from 10:00 AM to 10:30 AM ',0,0)
--insert into InterviewSlot(Slot,MinAge,MaxAge) values ('Sunday 29-04-2018 from 10:30 AM to 11:00 AM ',0,0)
--insert into InterviewSlot(Slot,MinAge,MaxAge) values ('Sunday 29-04-2018 from 11:00 AM to 11:30 AM ',0,0)
--insert into InterviewSlot(Slot,MinAge,MaxAge) values ('Sunday 29-04-2018 from 11:30 AM to 12:00 PM ',0,0)
--insert into InterviewSlot(Slot,MinAge,MaxAge) values ('Sunday 29-04-2018 from 12:00 PM to 12:30 PM ',0,0)
--insert into InterviewSlot(Slot,MinAge,MaxAge) values ('Sunday 29-04-2018 from 12:30 PM to 01:00 PM ',0,0)
--insert into InterviewSlot(Slot,MinAge,MaxAge) values ('Sunday 29-04-2018 from 02:00 PM to 02:30 PM ',0,0)
--insert into InterviewSlot(Slot,MinAge,MaxAge) values ('Sunday 29-04-2018 from 02:30 PM to 03:00 PM ',0,0)
--insert into InterviewSlot(Slot,MinAge,MaxAge) values ('Sunday 29-04-2018 from 03:00 PM to 03:30 PM ',0,0)
--insert into InterviewSlot(Slot,MinAge,MaxAge) values ('Sunday 29-04-2018 from 03:30 PM to 04:00 PM ',0,0)
--insert into InterviewSlot(Slot,MinAge,MaxAge) values ('Sunday 29-04-2018 from 04:00 PM to 04:30 PM ',0,0)
--insert into InterviewSlot(Slot,MinAge,MaxAge) values ('Sunday 29-04-2018 from 04:30 PM to 05:00 PM ',0,0)

--if exists (select 1 from sys.tables where name = 'Individual')
--begin 
--	delete from Individual
--	drop table Individual
--end
--go

--if exists (select 1 from sys.tables where name = 'audit_login')
--begin 
--	delete from audit_login
--	drop table audit_login
--end
--go

--create table audit_login
--(
--	email nvarchar(256),
--	created datetime
--)
--go

--if exists (select 1 from sys.tables where name = 'Individual')
--begin 
--	delete from Individual
--	drop table Individual
--end
--go

--create table Individual
--(
--	IndividualId int identity(100000,1),
--	Id uniqueidentifier NOT NULL,
--	Name NVARCHAR(128) NOT NULL,
--	Email NVARCHAR(128) NOT NULL,
--	Password NVARCHAR(25),
--	UserType VARCHAR(20) NOT NULL,
--	Phone NVARCHAR(14) NOT NULL,
--	Age INT NOT NULL,
--	State VARCHAR(60) NULL,
--	PaymentCode NVARCHAR(2048) NULL,
--	InterviewSlotId INT,
--	ManualMessage NVARCHAR(2048) NULL,
--	AutoMessage NVARCHAR(2048) NULL,
--	IsActive BIT DEFAULT 1, 
--	FatherName NVARCHAR(128) NULL,
--	NicNumber NVARCHAR(13) NULL,
--	OtherContact NVARCHAR(14) NULL,
--	MaritalStatus NVARCHAR(8) NULL,
--	ResidentialAddress NVARCHAR(1024) NULL,
--	PermanentAddress NVARCHAR(1024) NULL,

--	MatricGpa NVARCHAR(8) NULL,
--	MatricSubjects NVARCHAR(1024) NULL,
--	MatricInstitute NVARCHAR(512) NULL,

--	IntermediateGpa NVARCHAR(8) NULL,
--	IntermediateSubjects NVARCHAR(1024) NULL,
--	IntermediateInstitute NVARCHAR(512) NULL,

--	GraduationGpa NVARCHAR(8) NULL,
--	GraduationSubjects NVARCHAR(1024) NULL,
--	GraduationInstitute NVARCHAR(512) NULL,

--	MastersGpa NVARCHAR(10) NULL,
--	MastersSubjects NVARCHAR(1024) NULL,
--	MastersInstitution NVARCHAR(512) NULL,

--	MsGpa NVARCHAR(10) NULL,
--	MsSubjects NVARCHAR(1024) NULL,
--	MsInstitution NVARCHAR(512) NULL,

--	PhdSubjects NVARCHAR(1024) NULL,
--	PhdInstitution NVARCHAR(512) NULL,

--	OtherQualification NVARCHAR(512) NULL,

--	CurrentJob NVARCHAR(128) NULL,
--	CompanyName NVARCHAR(128) NULL,
--	Designation NVARCHAR(128) NULL,
--	TotalJobExperience NVARCHAR(64) NULL,

--	BusinessName NVARCHAR(128) NULL,
--	BusinessArea NVARCHAR(128) NULL,
--	BusinessNature NVARCHAR(128) NULL,

--	AppliedBefore BIT DEFAULT 0,
--	AnyReligiousEducation BIT DEFAULT 0,
--	ReligiousEducationSpecify NVARCHAR(128) NULL,
--	AnyOtherExpertise NVARCHAR(128) NULL,
--	CurrentStatus NVARCHAR(32) NULL,
--	PaymentMethod NVARCHAR(32) NULL,
--	Education NVARCHAR(32) NULL,
--	LastName NVARCHAR(128) NULL,
--	ApplyingFor NVARCHAR(64) NULL,
--	CurrentStatus NVARCHAR(32) NULL,
--	PaymentVerified BIT default 0,
--	CONSTRAINT AK_Email UNIQUE(Email)
--)
--GO

--alter table individual
--add PanelGrade nvarchar(4)
--go

--alter table individual
--add PanelComments nvarchar(512)
--go

--alter table individual
--add CeoGrade nvarchar(4)
--go

--alter table individual
--add CeoComments nvarchar(512)
--go

--if exists (select 1 from sys.procedures where name = 'CreateBasicIndividual')
--drop procedure CreateBasicIndividual
--go

--create procedure CreateBasicIndividual
--(
--	@Id uniqueidentifier,
--	@Name NVARCHAR(128),
--	@Email NVARCHAR(128),
--	@Password NVARCHAR(8),
--	@Phone NVARCHAR(20),
--	@Age INT,
--	@FatherName NVARCHAR(128) NULL,
--	@LastName NVARCHAR(128) NULL
--)
--as
--begin
--	if exists (select 1 from individual where email = @email)
--	begin
--		update individual set Id = @Id, Name = @Name, Phone = @Phone, Age = @Age, CurrentStatus = 'Professional', State = 'StepOneCompleted', FatherName = @FatherName, LastName = @LastName
--		where Email = @Email
--	end
--	else
--	begin
--		insert into Individual (Id, Name, Email, Password, UserType, Phone, Age, CurrentStatus, [State], FatherName, LastName)
--		values (@Id, @Name, @Email, @Password, 'Applicant', @Phone, @Age, 'Professional', 'StepOneCompleted', @FatherName, @LastName)
--	end
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
--	select * from Individual where Id = @Id
--end
--GO

--if exists (select 1 from sys.procedures where name = 'GetState')
--drop procedure GetState
--go

--create procedure GetState
--(
--	@Id uniqueidentifier
--)
--as
--begin
--	select [State] from Individual where Id = @Id
--end
--GO

--if exists (select 1 from sys.procedures where name = 'GetStateAndInterviewStatus')
--drop procedure GetStateAndInterviewStatus
--go

--create procedure GetStateAndInterviewStatus
--(
--	@Id uniqueidentifier
--)
--as
--begin
--	select [State], CASE WHEN InterviewSlotId IS NULL THEN 0 ELSE 1 END as [InterviewSlotBooked] from Individual where Id = @Id
--end
--GO


--if exists (select 1 from sys.procedures where name = 'UpdateIndividualDetails')
--drop procedure UpdateIndividualDetails
--go

--create procedure UpdateIndividualDetails
--(
--	@Id uniqueidentifier,
--	@State VARCHAR(60) NULL,
--	@FatherName NVARCHAR(128) NULL,
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
--	@PaymentCode NVARCHAR(10) NULL,
--	@PaymentMethod NVARCHAR(32) NULL,
--	@PanelGrade NVARCHAR(4),
--	@PanelComments NVARCHAR(512),
--	@CeoGrade NVARCHAR(4),
--	@CeoComments NVARCHAR(512)
--)
--as
--begin
--	UPDATE Individual
--	SET 
--      [State] = @State
--      ,[FatherName] = @FatherName
--      ,[NicNumber] = @NicNumber
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
--	  ,[PaymentCode] = @PaymentCode
--	  ,[PaymentMethod] = @PaymentMethod
--	  ,[PanelGrade] = @PanelGrade
--	  ,[PanelComments] = @PanelComments
--	  ,[CeoGrade] = @CeoGrade
--	  ,[CeoComments] = @CeoComments
-- WHERE Id = @Id

-- INSERT INTO [alburhan_admissions].[alburhan_user].[IndividualAudit]
--           ([IndividualId]
--           ,[Email]
--           ,[Password]
--           ,[Phone]
--           ,[State]
--           ,[PaymentCode]
--           ,[InterviewSlotId]
--           ,[NicNumber]
--           ,[MaritalStatus]
--           ,[PaymentMethod]
--           ,[LastName]
--           ,[created]
--           ,[PaymentVerified])
--	select IndividualId, Email, Password, Phone, State, PaymentCode, InterviewSlotId, NicNumber, MaritalStatus, PaymentMethod, LastName, getdate(), PaymentVerified
--	from Individual where Id = @Id

--end
--GO

--if exists (select 1 from sys.procedures where name = 'GetPaymentsPendingVerification')
--drop procedure GetPaymentsPendingVerification
--go

--create procedure GetPaymentsPendingVerification
--as
--begin
--	select Id, NicNumber, Name, LastName, Email, IndividualId, PaymentCode, PaymentMethod, Phone, FatherName, Password 
--	from individual 
--	where State = 'StepThreeCompleted' 
--	and PaymentCode IS NOT NULL
--	AND PaymentVerified <> 1 
--	order by IndividualId
--end
--go

--if exists (select 1 from sys.procedures where name = 'UpdateIndividualPaymentVerifiedFlag')
--drop procedure UpdateIndividualPaymentVerifiedFlag
--go

--create procedure UpdateIndividualPaymentVerifiedFlag
--(
--	@Id uniqueidentifier,
--	@PaymentVerified BIT
--)
--as
--begin
--	if @PaymentVerified = 1
--	begin 
--		update Individual set PaymentVerified = 1 where Id = @Id
--	end
--	else
--	begin
--		update Individual set PaymentVerified = 0, PaymentCode = NULL where Id = @Id
--	end
--end
--GO

--if exists (select 1 from sys.procedures where name = 'UpdateIndividualState')
--drop procedure UpdateIndividualState
--go

--create procedure UpdateIndividualState
--(
--	@Id uniqueidentifier,
--	@State VARCHAR(128),
--	@Message NVARCHAR(2048) = NULL
--)
--as
--begin
--	if @Message is null
--		update Individual set State = @State where Id = @Id
--	else
--		update Individual set State = @State, ManualMessage = @Message where Id = @Id
--end
--GO

--if exists (select 1 from sys.procedures where name = 'GetIndividualBasicDetailsByEmail')
--drop procedure GetIndividualBasicDetailsByEmail
--go

--create procedure GetIndividualBasicDetailsByEmail
--(
--	@Email NVARCHAR(128)
--)
--as
--begin
--	select Id, Password, State, UserType, Age, PaymentVerified, PaymentCode from individual where Email = @Email

--	-- this should be driven by code -- not bothered right now to add apply code changes
--	insert into audit_login values (@Email, dateadd(hour,5, getutcdate()))
--end
--GO

--if exists (select 1 from sys.procedures where name = 'GetIndividualBasicDetailsById')
--drop procedure GetIndividualBasicDetailsById
--go

--create procedure GetIndividualBasicDetailsById
--(
--	@Id uniqueidentifier
--)
--as
--begin
--	select Password, Email from individual where Id = @Id
--end
--GO

----if exists (select 1 from sys.procedures where name = 'DeleteIndividual')
----drop procedure DeleteIndividual
----go

----create procedure DeleteIndividual
----(
----	@Id uniqueidentifier
----)
----as
----begin
----	delete from Individual where Id = @Id
----end
----GO

--if exists (select 1 from sys.procedures where name = 'DoesEmailExist')
--drop procedure DoesEmailExist
--go

--create procedure DoesEmailExist
--(
--	@Email NVARCHAR(128)
--)
--as
--begin
--	declare @exists bit = 0
--	select @exists = 1 from Individual where Email = @Email and [State] <> 'PaymentVerificationFailed'
--	select @exists
--end
--GO

--if exists (select 1 from sys.procedures where name = 'UpdateIndividualInterviewSlot')
--drop procedure UpdateIndividualInterviewSlot
--go

--create procedure UpdateIndividualInterviewSlot
--(
--	@Id uniqueidentifier,
--	@InterviewSlotId INT
--)
--as
--begin
--	declare @state nvarchar(50)
--	select @state = State from individual where Id = @Id
--	if(@state = 'StepTwoCompleted')
--	begin
--		set @state = 'StepThreeCompleted'
--	end

--	update Individual set InterviewSlotId = @InterviewSlotId, State = @state where Id = @Id
--end
--GO

--if exists (select 1 from sys.procedures where name = 'GetSelectedInterviewSlot')
--drop procedure GetSelectedInterviewSlot
--go

--create procedure GetSelectedInterviewSlot
--(
--	@Id uniqueidentifier
--)
--as
--begin
--	select ISNULL(InterviewSlotId,0) from Individual where Id = @Id
--end
--GO

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
--	having count(i.InterviewSlotId) < 11
--	order by InterviewSlotId
--end
--GO


--if exists (select 1 from sys.procedures where name = 'GetSelectedInterviewSlotById')
--drop procedure GetSelectedInterviewSlotById
--go

--create procedure GetSelectedInterviewSlotById
--(
--	@Id int
--)
--as
--begin
--	select Slot as [InterviewSlot] from InterviewSlot where InterviewSlotId = @Id
--end
--GO

----if exists (select 1 from sys.procedures where name = 'SlotHasCapacity')
----drop procedure SlotHasCapacity
----go

----create procedure SlotHasCapacity
----(
----	@Id uniqueidentifier,
----	@InterviewSlotId int
----)
----as
----begin
----	declare @isOccupied bit = 0
----	select @isOccupied = 1 from Individual where Id <> @Id and InterviewSlotId = @InterviewSlotId
----	select @isOccupied
----end
----GO

---- INSERT ALL DATA
--insert into individual (Id,Name,Email,Password,UserType,Phone,Age,IsActive,State) values (newid(),'Admin', 'admin@alburhanconnect.com','loveislam','Administrator','00000000','35',1,'FormLocked')
--insert into individual (Id,Name,Email,Password,UserType,Phone,Age,IsActive,State) values (newid(),'Accountant', 'accountant@alburhanconnect.com','loveislam','Administrator','00000000','35',1,'FormLocked')

	
--	-- if either of the Avatar, Nic or Payment Code are null or blank, then the state should be 'DataRequired' and the AutoMessage should be 'Please note that a valid payment code, scanned copies of Photograph and Nic are required.'
--		-- At this stage, user can enter all the data.
--	-- if all three are provided, then the state should be 'DataBeingVerified', the AutoMessage becomes 'Please note that your photograph, Nic and Payment codes are being verified'
--		-- At this stage, the 3 data fields become read-only/locked and the user can change their other details.
--	-- If all three are verified and are WRONG, the state become 'DataVerificationFailed', the auto message becomes 'Please note that there is a problem with the details you provided'. Panel can enter a manual message when changing the status
--		-- If the ManualMessage exists, then the AutoMessage will not be displayed
--		-- At this stage, user can enter all the data.
--	-- If all three are verified and are OK, the state become 'DataVerifiedInterviewDataRequired' and the ManualMessage become NULL and the AutoMessage becomes 'Please note that your data is verified, now select an interview slot (day and time) to complete the application'
--		-- The user after login is taken to the InterviewDetails View. 
--		-- The message is displayed on this screen
--		-- The user can go back to the main form and can only change other data except for the 3 DataFields
--		-- On sign up screen, the user will see the AutoMessage
--		-- A button will be enabled based on this status on the main form to get to the interview screen
--	-- If interview details are complete, the the state becomes 'FormCompleted' and the AutomMessage becomes 'Your form is complete. You can still change your data and interview slot within the next few days'
--		-- when the user logs in, the rules are same as 'DataVerifiedInterviewDataRequired' but the user lands onto the signup page and can change some details, can go to the interview page
--		-- BUT the user cannot unselect an interview slot. 

--		--The difference here is, when the user is in 'DataVerifiedInterviewDataRequired' state. It can leave the interview slot empty, BUT if the user is in 'FormCompleted' mode, then the user
--		-- cannot make it blank but can only change or stick to it.

--	-- The interview slots selection can only be finalised once it is submitted. It may be available in the dropdown but someone else may also have selected it while it is being processed.
--		-- This validation will be applied on submit. The user's list will refresh based on the remaining available slots.
		



----if exists (select 1 from sys.procedures where name = 'GetIndividualDetailsByEmail')
----drop procedure GetIndividualDetailsByEmail
----go

----create procedure GetIndividualDetailsByEmail
----(
----	@Email NVARCHAR(128)
----)
----as
----begin
----	select * from Individual where Email = @Email
----end
----GO

--if exists (select 1 from sys.procedures where name = 'GetIndividuals')
--drop procedure GetIndividuals
--go

--create procedure GetIndividuals
--as
--begin
--	select Id, Name, LastName, Email, Phone, State, NicNumber, PaymentCode, paymentverified, PanelGrade, CeoGrade, 
--	((PanelGrade + CeoGrade * 3) * 100) / 24 as [Grade] 
--	from Individual
--	where UserType NOT IN ('Administrator','Accountant','Panel','ceo')
--	--and State IN ('Enrolled','StepThreeCompleted')
--	--and PaymentVerified = 1
--	order by 
--	case 
--		when State = 'StepThreeCompleted' then 1
--		when State = 'StepTwoCompleted' then 2
--		when State = 'StepOneCompleted' then 3
--		end, 
--	PaymentVerified desc, 
--	((PanelGrade + CeoGrade * 3) * 100) / 24 desc,
--	PaymentCode desc,
--	Name asc
--end
--GO

--if exists (select 1 from sys.procedures where name = 'GetIndividualDetailsByEmail')
--drop procedure GetIndividualDetailsByEmail
--go

--create procedure GetIndividualDetailsByEmail
--(
--	@Email NVARCHAR(128)
--)
--as
--begin
--	select * from Individual where Email = @Email
--end
--GO

--grant execute on GetIndividualDetailsByEmail to fquser
--go

--IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[v_RandomNewID]'))
--DROP VIEW [dbo].[v_RandomNewID]
--GO

--CREATE VIEW [dbo].[v_RandomNewID]
--AS
--	SELECT NEWID() AS [NewID]
--GO

--IF object_id(N'f_GenerateRandomPassword', N'FN') IS NOT NULL
--    DROP FUNCTION dbo.f_GenerateRandomPassword
--GO

--CREATE FUNCTION [dbo].[f_GenerateRandomPassword]()
--RETURNS VARCHAR(8)
--AS
--BEGIN
--	DECLARE @Password VARCHAR(8)
--	DECLARE @ValidCharacters VARCHAR(100)
--	DECLARE @PasswordIndex INT
--	DECLARE @CharacterLocation INT

--	SET @ValidCharacters = '1234567890'
--	SET @PasswordIndex = 1
--	SET @Password = ''

--	WHILE @PasswordIndex <= 8
--	BEGIN
--		SELECT @CharacterLocation = ABS(CAST(CAST([NewID] AS VARBINARY) AS INT)) % LEN(@ValidCharacters) + 1
--		FROM [dbo].[v_RandomNewID]

--		SET @Password = @Password + SUBSTRING(@ValidCharacters, @CharacterLocation, 1)
--		SET @PasswordIndex = @PasswordIndex + 1
--	END
	
--	RETURN @Password
--END
--GO

--if exists (select 1 from sys.procedures where name = 'IsAuthentic')
--drop procedure IsAuthentic
--go

--create procedure IsAuthentic
--(
--	@Email nvarchar(128),
--	@Password nvarchar(128)
--)
--as
--begin
--	declare @isAuthentic bit = 0
--	select @isAuthentic = 1 from individual where Email = @Email and [Password] = @Password and [State] = 'Registered'
--	select @isAuthentic
--end
--GO

----grant execute on GetIndividualDetails to fquser
----go

--------------------
--/*
--exec DoesEmailExist 'fqureshi@medicines.org.uk'
--exec SaveIndividualDetails '022CAC90-B468-4C42-911F-A54DC8543EBE', 'Farhan', 'fqureshi@medicines.org.uk'
--exec GetIndividualDetails 'fqureshi@medicines.org.uk'
--exec DoesEmailExist 'fqureshi@medicines.org.uk'

--select * from Individual
--delete from Individual

--343552fe-f562-4450-bd74-91e7d5b3900b
--*/
