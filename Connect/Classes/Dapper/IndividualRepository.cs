using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Connect.Classes.DataModels;
using Connect.Classes.Helpers;
using Connect.Controllers;
using Connect.Models.Admissions;
using Connect.Models.Charts;
using Connect.Models.Interview;
using Connect.Models.Payment;
using Connect.Models.Quiz;

namespace Connect.Classes.Dapper
{
	public class IndividualRepository : BaseRepository
	{
		#region Grading (ceo, panel)
		public void UpdateCeoGrading(CeoGradingViewModel model)
		{
			var conn = Connection();

			try
			{
				using (conn)
				{
					conn.Execute("UpdateCeoGrading", new
						{
							model.Id,
							model.Comments,
							model.Grade
						},
						commandType: CommandType.StoredProcedure);
				}
			}
			finally
			{
				FinaliseConnection(conn);
			}
		}

		public CeoGradingViewModel SelectCeoGrading(Guid id)
		{
			CeoGradingViewModel model;
			var conn = Connection();
			try
			{
				using (conn)
				{
					model = conn.Query<CeoGradingViewModel>("SelectCeoGrading", new { Id = id }, commandType: CommandType.StoredProcedure).FirstOrDefault();
				}
			}
			finally
			{
				FinaliseConnection(conn);
			}
			return model;
		}

		public void UpdatePanelGrading(PanelGradingViewModel model)
		{
			var conn = Connection();

			try
			{
				using (conn)
				{
					conn.Execute("UpdatePanelGrading", new
						{
							model.Id,

							model.IntroProfileDescription,
							model.IntroKeyAchievements,

							model.MotivationReasonForEnrollment,
							model.MotivationGoals,
							model.MotivationIfNotSelected,

							model.StrengthsTopTwo,
							model.StrengthsHobbies,

							model.CommittmentAnyCourseBefore,
							model.CommittmentFulfillPromises,
							model.CommittmentExample,
							model.CommittmentFailures,

							model.AdditionalSkillsAnyOther,
							model.AdditionalSkillsSocial,

							model.GradeCommitment,
							model.GradeStrengths,
							model.GradePassion,
							model.GradeFunctionalExpertise,
							model.GradeExistingAffiliation,
							Comments = model.PanelComments,
							Grade = model.PanelGrade,
					},
						commandType: CommandType.StoredProcedure);
				}
			}
			finally
			{
				FinaliseConnection(conn);
			}
		}

        public PanelGradingViewModel SelectPanelGrading(Guid id)
		{
			PanelGradingViewModel model;
			var conn = Connection();
			try
			{
				using (conn)
				{
					model = conn.Query<PanelGradingViewModel>("SelectPanelGrading", new { Id = id }, commandType: CommandType.StoredProcedure).FirstOrDefault();
				}
			}
			finally
			{
				FinaliseConnection(conn);
			}
			return model;
		}

		#endregion

		public bool IsAccountVerified(string id)
		{
			bool verified;
			var conn = Connection();
			try
			{
				using (conn)
				{
					verified = conn.QuerySingle<bool>("IsAccountVerified", new { Id = id },
						commandType: CommandType.StoredProcedure);
				}
			}
			finally
			{
				FinaliseConnection(conn);
			}
			return verified;
		}

		public bool DoesEmailExist(string email)
		{
			var exists = false;
			var conn = Connection();
			try
			{
				using (conn)
				{
					exists = conn.QuerySingle<bool>("DoesEmailExist", new { Email = email },
						commandType: CommandType.StoredProcedure);
				}
			}
			finally
			{
				FinaliseConnection(conn);
			}
			return exists;
		}

		public bool DoesPaymentCodeExist(string paymentCode)
		{
			var exists = false;
			var conn = Connection();
			try
			{
				using (conn)
				{
					exists = conn.QuerySingle<bool>("DoesPaymentCodeExist", new { PaymentCode = paymentCode },
						commandType: CommandType.StoredProcedure);
				}
			}
			finally
			{
				FinaliseConnection(conn);
			}
			return exists;
		}

		public void CreateOrUpdateBasicIndividual(Individual individual)
		{
			var conn = Connection();

			try
			{
				using (conn)
				{
					conn.Execute("CreateOrUpdateBasicIndividual", new
						{
							individual.Id,
							individual.Name,
							individual.Email,
							individual.Password,
							Phone = individual.Phone.ToString(),
							individual.Age,
							UserType = individual.UserType.ToString(),
							RegistrationStatus = individual.RegistrationStatus.ToString(),
							individual.PaymentMethod,
							individual.PaymentCode,
							PaymentStatus = individual.PaymentStatus.ToString(),
							individual.InterviewSlotId,
                            individual.Batch,
                            CurrentStatus = individual.CurrentStatus.ToString(),
                            individual.Campus,
                            individual.BatchTimingId
						},
						commandType: CommandType.StoredProcedure);
				}
			}
			finally
			{
				FinaliseConnection(conn);
			}
		}

		public void UpdatePaymentDetails(Individual individual)
		{
			var conn = Connection();

			try
			{
				using (conn)
				{
					conn.Execute("UpdatePaymentDetails", new
						{
							individual.Email,
							individual.PaymentMethod,
							individual.PaymentCode,
							PaymentStatus = individual.PaymentStatus.ToString(),
                            individual.Id
						},
						commandType: CommandType.StoredProcedure);
				}
			}
			finally
			{
				FinaliseConnection(conn);
			}
		}

		public void UpdateIndividualDetails(Individual individualFromDb)
		{
			var conn = Connection();

			try
			{
				using (conn)
				{
					conn.Execute("UpdateIndividualDetails", new
						{
							individualFromDb.Id,
							//State = individualFromDb.State.ToString(),
							//individualFromDb.FatherName,
							individualFromDb.NicNumber,
							individualFromDb.OtherContact,
							individualFromDb.MaritalStatus,
							individualFromDb.ResidentialAddress,
							individualFromDb.PermanentAddress,
							individualFromDb.MatricGpa,
							individualFromDb.MatricSubjects,
							individualFromDb.MatricInstitute,
							individualFromDb.IntermediateGpa,
							individualFromDb.IntermediateSubjects,
							individualFromDb.IntermediateInstitute,
							individualFromDb.GraduationGpa,
							individualFromDb.GraduationSubjects,
							individualFromDb.GraduationInstitute,
							individualFromDb.MastersGpa,
							individualFromDb.MastersSubjects,
							individualFromDb.MastersInstitution,
							individualFromDb.MsGpa,
							individualFromDb.MsSubjects,
							individualFromDb.MsInstitution,
							individualFromDb.PhdSubjects,
							individualFromDb.PhdInstitution,
							individualFromDb.OtherQualification,

							individualFromDb.CurrentJob,
							individualFromDb.CompanyName,
							individualFromDb.Designation,
							individualFromDb.TotalJobExperience,

							individualFromDb.BusinessName,
							individualFromDb.BusinessArea,
							individualFromDb.BusinessNature,

							individualFromDb.AppliedBefore,
							individualFromDb.AnyReligiousEducation,
							individualFromDb.ReligiousEducationSpecify,
							individualFromDb.AnyOtherExpertise,
							CurrentStatus = individualFromDb.CurrentStatus.ToString(),
							Education = individualFromDb.Education.ToString(),
							RegistrationStatus = individualFromDb.RegistrationStatus.ToString(),
							individualFromDb.InterviewSlotId,
                            individualFromDb.Name,
                            individualFromDb.Phone,
                        individualFromDb.FatherOrHusbandName,
                        individualFromDb.WhatsAppNumber,
                        individualFromDb.EducationDetails,
                        individualFromDb.ProfessionStudyBusinessDetails,
                        individualFromDb.BatchTimingId,
                        individualFromDb.AdvancedSelection

                        //individualFromDb.PaymentCode,
                        //individualFromDb.PaymentMethod,
                        //individualFromDb.PanelGrade,
                        //individualFromDb.PanelComments,
                        //individualFromDb.CeoGrade,
                        //individualFromDb.CeoComments
                    },
						commandType: CommandType.StoredProcedure);
				}
			}
			finally
			{
				FinaliseConnection(conn);
			}
		}

        public bool IsAuthentic(string email, string password)
		{
			var exists = false;
			var conn = Connection();
			try
			{
				using (conn)
				{
					exists = conn.QuerySingle<bool>("IsAuthentic", new { Email = email, Password = password },
						commandType: CommandType.StoredProcedure);
				}
			}
			finally
			{
				FinaliseConnection(conn);
			}
			return exists;
		}

		public List<Individual> GetIndividuals()
		{
			List<Individual> individuals;
			var conn = Connection();
			try
			{
				using (conn)
				{
					individuals = conn.Query<Individual>("GetIndividuals", commandType: CommandType.StoredProcedure).ToList();
				}
			}
			finally
			{
				FinaliseConnection(conn);
			}
			return individuals;
		}

		public void DeleteIndividual(Guid id)
		{
			var conn = Connection();
			try
			{
				using (conn)
				{
					conn.Execute("DeleteIndividual", new { Id = id },
						commandType: CommandType.StoredProcedure);
				}
			}
			finally
			{
				FinaliseConnection(conn);
			}
		}

		public string GetState(Guid id)
		{
			string state;
			var conn = Connection();
			try
			{
				using (conn)
				{
					state = conn.Query<string>("GetState", new { Id = id }, commandType: CommandType.StoredProcedure).FirstOrDefault();
				}
			}
			finally
			{
				FinaliseConnection(conn);
			}
			return state;
		}

		public Individual GetIndividualDetailsById(Guid id)
		{
			Individual individual;
			var conn = Connection();
			try
			{
				using (conn)
				{
					individual = conn.Query<Individual>("GetIndividualDetailsById", new { Id = id}, commandType: CommandType.StoredProcedure).FirstOrDefault();
				}
			}
			finally
			{
				FinaliseConnection(conn);
			}
			return individual;
		}

		public Individual GetIndividualBasicDetailsByEmail(string email)
		{
			Individual individual;
			var conn = Connection();
			try
			{
				using (conn)
				{
					var result = conn.QueryMultiple("GetIndividualBasicDetailsByEmail", new { Email = email }, commandType: CommandType.StoredProcedure);
                    individual = result.Read<Individual>().FirstOrDefault();

                    if(individual != null)
                    {
                        var sections = result.Read<string>();
                        var subjects = result.Read<string>();
                        individual.Sections = sections.ToList();
                        individual.Subject = subjects.ToList();
                    }
                }
            }
			finally
			{
				FinaliseConnection(conn);
			}
			return individual;
		}

        public IndividualByCnic GetIndividualDetailsByCnic(string cnic)
        {
            IndividualByCnic individual = null;
            var conn = Connection();
            try
            {
                using (conn)
                {
                    var result = conn.Query<IndividualByCnic>("GetIndividualDetailsByCnic", new { cnic }, commandType: CommandType.StoredProcedure).FirstOrDefault();

                    if (result != null)
                    {
                        individual = result;
                    }
                }
            }
            finally
            {
                FinaliseConnection(conn);
            }

            return individual;
        }

        public Individual GetIndividualIdsByEmail(string email)
        {
            Individual individual = null;
            var conn = Connection();
            try
            {
                using (conn)
                {
                    var result = conn.Query<Individual>("GetIndividualIdsByEmail", new { Email = email }, commandType: CommandType.StoredProcedure).FirstOrDefault();

                    if (result != null)
                    {
                        individual = result;
                    }
                }
            }
            finally
            {
                FinaliseConnection(conn);
            }

            return individual;
        }

        public Individual GetIndividualBasicDetailsById(Guid id)
		{
			Individual individual;
			var conn = Connection();
			try
			{
				using (conn)
				{
					individual = conn.Query<Individual>("GetIndividualBasicDetailsById", new { Id = id }, commandType: CommandType.StoredProcedure).FirstOrDefault();
				}
			}
			finally
			{
				FinaliseConnection(conn);
			}
			return individual;
		}

		public void UpdateIndividualState(Guid id, string state, string message = null)
		{
			var conn = Connection();
			try
			{
				using (conn)
				{
					conn.Execute("UpdateIndividualState", new { Id = id, State = state, Message = message },
						commandType: CommandType.StoredProcedure);
				}
			}
			finally
			{
				FinaliseConnection(conn);
			}
		}

        public void UpdateIndividualPaymentVerifiedFlag(Guid id, string paymentStatus)
		{
			var conn = Connection();
			try
			{
				using (conn)
				{
					conn.Execute("UpdateIndividualPaymentVerifiedFlag", new { Id = id, PaymentStatus = paymentStatus },
						commandType: CommandType.StoredProcedure);
				}
			}
			finally
			{
				FinaliseConnection(conn);
			}
		}

		public IEnumerable<Payment> GetPaymentsPendingVerificationAndDeclined()
		{
			IList<Payment> payments;
			var conn = Connection();
			try
			{
				using (conn)
				{
					payments = conn.Query<Payment>("GetPaymentsPendingVerificationAndDeclined", new { batch = SessionHelper.LoggedInUser.Batch }, 
                        commandType: CommandType.StoredProcedure).ToList();
				}
			}
			finally
			{
				FinaliseConnection(conn);
			}
			return payments;
		}

        public IEnumerable<Admission> GetCeoInterviewedCandidatesForFinalDecision()
        {
            IList<Admission> interviewes;
            var conn = Connection();
            try
            {
                using (conn)
                {
                    interviewes = conn.Query<Admission>("GetCeoInterviewedCandidatesForFinalDecision", new { batch = SessionHelper.LoggedInUser.Batch },
                        commandType: CommandType.StoredProcedure).ToList();
                }
            }
            finally
            {
                FinaliseConnection(conn);
            }
            return interviewes;
        }

        public IEnumerable<Admission> GetCeoInterviewedFemaleCandidatesForFinalDecision()
        {
            IList<Admission> interviewes;
            var conn = Connection();
            try
            {
                using (conn)
                {
                    interviewes = conn.Query<Admission>("GetCeoInterviewedFemaleCandidatesForFinalDecision", new { batch = SessionHelper.LoggedInUser.Batch },
                        commandType: CommandType.StoredProcedure).ToList();
                }
            }
            finally
            {
                FinaliseConnection(conn);
            }
            return interviewes;
        }

        public IEnumerable<Interviewee> GetCompletedApplicationForIntervieweesForPanel()
        {
            IList<Interviewee> interviewes;
            var conn = Connection();
            try
            {
                using (conn)
                {
                    interviewes = conn.Query<Interviewee>("GetCompletedApplicationForIntervieweesForPanel", new { batch = SessionHelper.LoggedInUser.Batch }, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            finally
            {
                FinaliseConnection(conn);
            }
            return interviewes;
        }

        public IEnumerable<Interviewee> GetCompletedApplicationForIntervieweesForCeo()
		{
			IList<Interviewee> interviewes;
			var conn = Connection();
			try
			{
				using (conn)
				{
					interviewes = conn.Query<Interviewee>("GetCompletedApplicationForIntervieweesForCeo", new { batch = SessionHelper.LoggedInUser.Batch }, commandType: CommandType.StoredProcedure).ToList();
				}
			}
			finally
			{
				FinaliseConnection(conn);
			}
			return interviewes;
		}

        public string UpdateIndividualStatus(int individualId, RegistrationStatus status)
        {
            object email;
            var conn = Connection();
            try
            {
                using (conn)
                {
                    email = conn.ExecuteScalar("UpdateIndividualStatus", new { IndividualId = individualId, Status = status.ToString() },
                        commandType: CommandType.StoredProcedure);
                }
            }
            finally
            {
                FinaliseConnection(conn);
            }

            if (email == null)
                email = "";

            return email.ToString();
        }

        public string UpdateIndividualEnrolmentEmailSentStatus(int individualId)
        {
            object email;
            var conn = Connection();
            try
            {
                using (conn)
                {
                    email = conn.ExecuteScalar("UpdateIndividualEnrolmentEmailSentStatus", new { IndividualId = individualId },
                        commandType: CommandType.StoredProcedure);
                }
            }
            finally
            {
                FinaliseConnection(conn);
            }

            if (email == null)
                email = "";

            return email.ToString();
        }

        public void UpdateIndividualSection(int individualId, string section)
        {
            var conn = Connection();
            try
            {
                using (conn)
                {
                    conn.Execute("UpdateIndividualSection", new { IndividualId = individualId, Section = section }, 
                        commandType: CommandType.StoredProcedure);
                }
            }
            finally
            {
                FinaliseConnection(conn);
            }
        }

        public IEnumerable<CallHistory> GetCallHistoryForIndividual(Guid id)
        {
            IEnumerable<CallHistory> callHistory;
            var conn = Connection();
            try
            {
                using (conn)
                {
                    callHistory = conn.Query<CallHistory>("GetCallHistoryForIndividual", new { Id = id }, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            finally
            {
                FinaliseConnection(conn);
            }
            return callHistory;
        }

        #region Notes

        public IEnumerable<Note> GetNotesForIndividual(Guid id)
        {
            IEnumerable<Note> notes;
            var conn = Connection();
            try
            {
                using (conn)
                {
                    notes = conn.Query<Note>("GetNotesForIndividual", new { Id = id }, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            finally
            {
                FinaliseConnection(conn);
            }
            return notes;
        }

        public IEnumerable<IndividualEventViewModel> GetQuizzesForIndividual(Guid id)
        {
            IEnumerable<IndividualEventViewModel> records;
            var conn = Connection();
            try
            {
                using (conn)
                {
                    records = conn.Query<IndividualEventViewModel>("GetQuizzesForIndividual", new { Id = id }, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            finally
            {
                FinaliseConnection(conn);
            }
            return records;
        }

        public IEnumerable<IndividualAttendance> GetAttendanceForIndividual(Guid id)
        {
            IEnumerable<IndividualAttendance> notes;
            var conn = Connection();
            try
            {
                using (conn)
                {
                    notes = conn.Query<IndividualAttendance>("GetAttendanceForIndividual", new { Id = id }, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            finally
            {
                FinaliseConnection(conn);
            }
            return notes;
        }

        public IEnumerable<Note> GetNotesForIndividualById(int individualId)
        {
            IEnumerable<Note> notes;
            var conn = Connection();
            try
            {
                using (conn)
                {
                    notes = conn.Query<Note>("GetNotesForIndividualById", new { IndividualId = individualId }, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            finally
            {
                FinaliseConnection(conn);
            }
            return notes;
        }

        //public void DeleteNote(int noteId)
        //{
        //    var conn = Connection();

        //    try
        //    {
        //        using (conn)
        //        {
        //            conn.Execute("DeleteNote", new { noteId }, commandType: CommandType.StoredProcedure);
        //        }
        //    }
        //    finally
        //    {
        //        FinaliseConnection(conn);
        //    }
        //}

        public void SaveNoteForIndividual(int individualId, string noteText)
        {
            var conn = Connection();

            try
            {
                using (conn)
                {
                    conn.Execute("SaveNoteForIndividual", new { individualId, noteText }, commandType: CommandType.StoredProcedure);
                }
            }
            finally
            {
                FinaliseConnection(conn);
            }
        }

        #endregion

        #region Attendance
        public void UpdateAttendanceStatus(int attendanceId, int status)
        {
            var conn = Connection();

            try
            {
                using (conn)
                {
                    conn.Execute("UpdateAttendanceStatus", new { attendanceId, status }, commandType: CommandType.StoredProcedure);
                }
            }
            finally
            {
                FinaliseConnection(conn);
            }
        }

        #endregion

        #region

        internal void AddLeave(int individualId, DateTime dateOfLeave, string reason)
        {
            var conn = Connection();

            try
            {
                using (conn)
                {
                    conn.Execute("SaveLeaveForIndividual", new { individualId, dateOfLeave, reason }, commandType: CommandType.StoredProcedure);
                }
            }
            finally
            {
                FinaliseConnection(conn);
            }
        }

        internal IEnumerable<IndividualLeave> GetLeavesForIndividual(Guid id)
        {
            IEnumerable<IndividualLeave> notes;
            var conn = Connection();
            try
            {
                using (conn)
                {
                    notes = conn.Query<IndividualLeave>("GetLeavesForIndividual", new { Id = id }, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            finally
            {
                FinaliseConnection(conn);
            }
            return notes;
        }

        internal void DeleteLeave(int leaveId)
        {
            var conn = Connection();
            try
            {
                using (conn)
                {
                    conn.Execute("DeleteLeave", new { leaveId }, commandType: CommandType.StoredProcedure);
                }
            }
            finally
            {
                FinaliseConnection(conn);
            }
        }

        internal int GetApplicationChartData(string batch)
        {
            int count = 0;
            var conn = Connection();
            try
            {
                using (conn)
                {
                    count = conn.ExecuteScalar<int>("GetApplicationChartData", new { batch }, commandType: CommandType.StoredProcedure);
                }
            }
            finally
            {
                FinaliseConnection(conn);
            }

            return count;
        }

        #endregion
    }
}