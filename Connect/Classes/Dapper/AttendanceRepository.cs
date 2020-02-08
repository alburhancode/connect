using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Connect.Classes.DataModels;
using Connect.Models.Quiz;

namespace Connect.Classes.Dapper
{
	public class AttendanceRepository : BaseRepository
	{
        internal void SaveAttendance(IAttendanceLog attendance)
        {
            var conn = Connection();

            try
            {
                using (conn)
                {
                    conn.Execute("SaveAttendance", new
                    {
                        attendance.Batch,
                        attendance.Course,
                        attendance.CreatedAt,
                        attendance.Email,
                        attendance.RollNumber,
                        attendance.Section,
                        attendance.Semester,
                        attendance.Status,
                        attendance.Subject,
                    },
                        commandType: CommandType.StoredProcedure);
                }
            }
            finally
            {
                FinaliseConnection(conn);
            }
        }

        internal IList<AttendanceHistory> GetAttendanceHistory(string rollNumber)
        {
            List<AttendanceHistory> history;
            var conn = Connection();
            try
            {
                using (conn)
                {
                    history = conn.Query<AttendanceHistory>("GetAttendanceHistory", new { rollNumber },
                        commandType: CommandType.StoredProcedure).ToList();
                }
            }
            finally
            {
                FinaliseConnection(conn);
            }
            return history;
        }

        public List<IndividualBasics> GetIndividualsBySection(string batch, string section, int eventId)
        {
            List<IndividualBasics> individuals;
            var conn = Connection();
            try
            {
                using (conn)
                {
                    individuals = conn.Query<IndividualBasics>("GetIndividualsBySection", new { batch, section, eventId }, 
                        commandType: CommandType.StoredProcedure).ToList();
                }
            }
            finally
            {
                FinaliseConnection(conn);
            }
            return individuals;
        }

        internal void CreateCallDetailsForIndividual(int individualid, string calloutcome, string callnotes, string caller, string kefiyat)
        {
            var conn = Connection();
            try
            {
                using (conn)
                {
                    conn.Execute("CreateCallDetailsForIndividual", new { individualid, calloutcome, callnotes, caller, kefiyat },
                        commandType: CommandType.StoredProcedure);
                }
            }
            finally
            {
                FinaliseConnection(conn);
            }
        }

        public List<AttendanceFullSummary> GetIndividualAttendanceBySection(string batch, string section)
        {
            List<AttendanceFullSummary> attendance;
            var conn = Connection();
            try
            {
                using (conn)
                {
                    attendance = conn.Query<AttendanceFullSummary>("GetAttendanceFullSummaryBySection", new { batch, section },
                        commandType: CommandType.StoredProcedure).ToList();

                    if(attendance != null)
                    {
                        List<AttendanceHistory> individualAtt = null; 
                        foreach (var att in attendance)
                        {
                            individualAtt = conn.Query<AttendanceHistory>("GetAttendanceHistoryByIndividual", new { rollnumber = att.RollNumber, batch, section },
                                commandType: CommandType.StoredProcedure).ToList();

                            if(individualAtt != null)
                            {
                                att.History = individualAtt;
                            }
                        }
                    }

                }
            }
            finally
            {
                FinaliseConnection(conn);
            }

            return attendance;
        }

        public List<AttendanceFullSummary> GetIndividualAttendanceBySubject(string batch, string subject)
        {
            List<AttendanceFullSummary> attendance;
            var conn = Connection();
            try
            {
                using (conn)
                {
                    attendance = conn.Query<AttendanceFullSummary>("GetAttendanceSummaryBySubject", new { batch, subject },
                        commandType: CommandType.StoredProcedure).ToList();

                    if (attendance != null)
                    {
                        List<AttendanceHistory> individualAtt = null;
                        foreach (var att in attendance)
                        {
                            individualAtt = conn.Query<AttendanceHistory>("GetAttendanceHistoryByIndividual", new { rollnumber = att.RollNumber, batch },
                                commandType: CommandType.StoredProcedure).ToList();

                            if (individualAtt != null)
                            {
                                att.History = individualAtt;
                            }
                        }
                    }
                }
            }
            finally
            {
                FinaliseConnection(conn);
            }
            return attendance;
        }

        public List<IndividualBasicsForAttendance> GetIndividualsBySectionForAttendance(string batch, string section, int eventId)
        {
            List<IndividualBasicsForAttendance> individuals;
            var conn = Connection();
            try
            {
                using (conn)
                {
                    individuals = conn.Query<IndividualBasicsForAttendance>("GetIndividualsBySection", new { batch, section, eventId },
                        commandType: CommandType.StoredProcedure).ToList();
                }
            }
            finally
            {
                FinaliseConnection(conn);
            }
            return individuals;
        }

        internal IList<OverallAttendance> GetOverallAttendance(string batch)
        {
            var overallAttendance = new List<OverallAttendance>();
            var conn = Connection();
            try
            {
                using (conn)
                {
                    overallAttendance = conn.Query<OverallAttendance>("GetOverallAttendance", new { batch },
                        commandType: CommandType.StoredProcedure).ToList();
                }
            }
            finally
            {
                FinaliseConnection(conn);
            }
            return overallAttendance;
        }
    }
}