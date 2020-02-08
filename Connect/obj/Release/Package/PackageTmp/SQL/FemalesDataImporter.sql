
-- BE CAREFUL HERE. THIS IS ONE OFF ONLY

USE [alburhan_batch5]
GO

ALTER TABLE [dbo].[Individual] DROP CONSTRAINT [AK_Email]
GO

SET ANSI_PADDING ON
GO

ALTER TABLE [dbo].[Individual] ADD  CONSTRAINT [AK_Email] UNIQUE NONCLUSTERED 
(
	[Email] ASC,
	[Batch]
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO


IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[v_RandomNewID]'))
DROP VIEW [dbo].[v_RandomNewID]
GO

CREATE VIEW [dbo].[v_RandomNewID]
AS
	SELECT NEWID() AS [NewID]
GO


IF object_id(N'f_GenerateRandomPassword', N'FN') IS NOT NULL
    DROP FUNCTION dbo.f_GenerateRandomPassword
GO

CREATE FUNCTION [dbo].[f_GenerateRandomPassword]()
RETURNS VARCHAR(6)
AS
BEGIN
	DECLARE @Password VARCHAR(6)
	DECLARE @ValidCharacters VARCHAR(100)
	DECLARE @PasswordIndex INT
	DECLARE @CharacterLocation INT

	SET @ValidCharacters = '1234567890'
	SET @PasswordIndex = 1
	SET @Password = ''

	WHILE @PasswordIndex <= 6
	BEGIN
		SELECT @CharacterLocation = ABS(CAST(CAST([NewID] AS VARBINARY) AS INT)) % LEN(@ValidCharacters) + 1
		FROM [dbo].[v_RandomNewID]

		SET @Password = @Password + SUBSTRING(@ValidCharacters, @CharacterLocation, 1)
		SET @PasswordIndex = @PasswordIndex + 1
	END
	
	RETURN @Password
END
GO


if not exists (select 1 from sys.tables where name = 'Imported')
begin 
	create table Imported
	(
		[Timestamp] datetime null,
		[Name] nvarchar(256) null,
		[CNIC Number] CHAR(256) null,
		[Email Address] nvarchar(256) null,
		[Father/Husband Name] nvarchar(256) null,
		[Contact Number] nvarchar(256) null,
		[Class Selection] nvarchar(256) null,
		[WhatsApp Number] nvarchar(256) null,
		[Contact Number (Husband/Father/Son)] nvarchar(max) null,
		[Marital Status] nvarchar(16) null,
		[Age (Years)] nvarchar(3) null,
		Education nvarchar(1024) null,
		Responsibilities nvarchar(512) null,
		[Profession/Studies/Business Details] nvarchar(256) null,
		[Address] nvarchar(1024) null,
		City nvarchar(256) null
	)
end
go

delete from Imported

BULK INSERT dbo.Imported
FROM 'C:\ME\AL BURHAN\Admissions\ABW_Final.csv' 
WITH ( FIELDTERMINATOR ='¬', FIRSTROW = 2 )

-- Fix All the records where an email is not provided
update Imported set [Email Address] = 'missing_email_for_contact_' + [Contact Number] + '@gmail.com' where [Email Address] is null
-- Fix duplicate email address
update imported set [Email Address] = 'duplicate_email.Tasleemakbar449@gmail.com' where [Email Address] = 'Tasleemakbar449@gmail.com' and Name = 'Ayesha ahmad' 
-- Fix duplicate email address
update imported set [Email Address] = 'duplicate_email.saminashafi.1@gmail.com' where [Email Address] = 'saminashafi.1@gmail.com' and [Age (Years)] = 51 
-- Fix duplicate email address
update imported set [Email Address] = 'duplicate_email.kfkhan94@gmail.com' where [Email Address] = 'kfkhan94@gmail.com' and Name = 'Sajda Bibi'
-- Fix duplicate email address
update imported set [Email Address] = 'duplicate_email.arehman350@ymail.com' where [Email Address] = 'arehman350@ymail.com' and Name = 'Jawairia Rahman'

-- Fix duplicate email address
update imported set [Email Address] = 'duplicate_email.Hamnaasghar10@gmail.com' where [Email Address] = 'Hamnaasghar10@gmail.com' and Name = 'Farzana Ashghr'
update imported set [Email Address] = 'duplicate_email.Kakakhel.ambreen@gmail.com' where [Email Address] = 'Kakakhel.ambreen@gmail.com' and Name = 'Ayesha Kakakhel'
update imported set [Email Address] = 'duplicate_email.nimra.albaaz@yahoo.com' where [Email Address] = 'nimra.albaaz@yahoo.com' and Name = 'Naheed Shoukat'
update imported set [Email Address] = 'duplicate_email.mariya.anwaar@yahoo.com' where [Email Address] = 'mariya.anwaar@yahoo.com' and Name = 'Ghazala Tabassum'

update imported set [Email Address] = 'duplicate1_email.ahteshamkhan616@gmail.com' where [Email Address] = 'ahteshamkhan616@gmail.com' and Name = 'Nabila bibi'
update imported set [Email Address] = 'duplicate2_email.ahteshamkhan616@gmail.com' where [Email Address] = 'ahteshamkhan616@gmail.com' and Name = 'Farat bibi'
update imported set [Email Address] = 'duplicate3_email.ahteshamkhan616@gmail.com' where [Email Address] = 'ahteshamkhan616@gmail.com' and Name = 'Maryam bibi'

update imported set [Email Address] = 'duplicate_email.missing_email_for_contact_3206031950@gmail.com' where [Email Address] = 'missing_email_for_contact_3206031950@gmail.com' and Name = 'Areesha tul Fatima'
update imported set [Email Address] = 'duplicate_email.mscbiology70@gmail.com' where [Email Address] = 'mscbiology70@gmail.com' and Name = 'Sobia Mushtaq'
update imported set [Email Address] = 'duplicate_email.Sufyan.kakakhel@gmail.com ' where [Email Address] = 'Sufyan.kakakhel@gmail.com ' and Name = 'Binte Sufyan Kakakhel'


-- Delete existing records from Individual table for females
delete I from individual I inner join Imported M on I.Email = M.[Email Address] AND I.Batch = 'ISB-2019-B001-Females'
delete from individual where UserType = 'Applicant' and batch = 'ISB-2019-B001-Females'

-- Insert into the Individual table
insert into individual(Id,Name,NicNumber,Email,FatherOrHusbandName,Phone,ClassSelection,WhatsAppNumber,
MaritalStatus,Age,EducationDetails,Responsibilities,ProfessionStudyBusinessDetails,ResidentialAddress, PermanentAddress,
CityDetails,Password,UserType,RegistrationStatus,PaymentStatus,InterviewSlotId,Batch,
PaymentCode, PaymentMethod)

select newid(), Name, [CNIC Number], [Email Address], [Father/Husband Name], [Contact Number], [Class Selection], 
[WhatsApp Number], [Marital Status], [Age (Years)], Education, Responsibilities, 
[Profession/Studies/Business Details], [Address], [Address], City, [dbo].[f_GenerateRandomPassword](), 'Applicant', 
'Completed', 'UnVerified',0, 'ISB-2019-B001-Females',
'NotApplicable','NotApplicable'
from Imported

update individual set Password = 'p4ym3nt54dminf3m4l35' where Email = 'payments_w@alburhanconnect.com'
update individual set Password = 'c30f3m4l354lburh4n' where Email = 'ceo_w@alburhanconnect.com'
update individual set Password = 'p4n3lf3m4l354lburh4n' where Email = 'panel_w@alburhanconnect.com'

select * from individual where batch = 'ISB-2019-B001-Females'

