using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Connect.Classes.DataModels;
using Connect.Classes.Helpers;
using Connect.Models.Quiz;

namespace Connect.Classes.Dapper
{
	public class EventRepository : BaseRepository
	{
        internal List<EventType> GetEventTypes()
        {
            List<EventType> history;
            var conn = Connection();
            try
            {
                using (conn)
                {
                    history = conn.Query<EventType>("GetEventTypes", commandType: CommandType.StoredProcedure).ToList();
                }
            }
            finally
            {
                FinaliseConnection(conn);
            }
            return history;
        }

        internal List<EventsOccurencesViewModel> GetEventOccurences(string batch)
        {
            List<EventsOccurencesViewModel> history;
            var conn = Connection();
            try
            {
                using (conn)
                {
                    history = conn.Query<EventsOccurencesViewModel>("GetEventOccurences", new { batch }, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            finally
            {
                FinaliseConnection(conn);
            }
            return history;
        }

        internal List<IndividualEventViewModel> GetEventOccurencesForIndividual(int individualId)
        {
            List<IndividualEventViewModel> records;
            var conn = Connection();
            try
            {
                using (conn)
                {
                    records = conn.Query<IndividualEventViewModel>("GetEventOccurencesForIndividual", new { individualId }, 
                        commandType: CommandType.StoredProcedure).ToList();
                }
            }
            finally
            {
                FinaliseConnection(conn);
            }
            return records;
        }

        internal List<EventOccurence> GetEventOccurencesNotForIndividual(int individualId)
        {
            List<EventOccurence> records;
            var conn = Connection();
            try
            {
                using (conn)
                {
                    records = conn.Query<EventOccurence>("GetEventOccurencesNotForIndividual", new { individualId },
                        commandType: CommandType.StoredProcedure).ToList();
                }
            }
            finally
            {
                FinaliseConnection(conn);
            }
            return records;
        }

        internal void CreateIndividualEvent(CreateIndividualEventViewModel individualEvent)
        {
            var conn = Connection();
            try
            {
                using (conn)
                {
                    conn.Execute("CreateIndividualEvent", new { individualEvent.IndividualId, individualEvent.EventId, individualEvent.Value, individualEvent.AdditionalText, individualEvent.FilePath },
                        commandType: CommandType.StoredProcedure);
                }
            }
            finally
            {
                FinaliseConnection(conn);
            }
        }

        internal void CreateUpdateOrDeleteIndividualQuizMarks(int individualid, int eventid, int marks)
        {
            var conn = Connection();
            try
            {
                using (conn)
                {
                    conn.Execute("CreateUpdateOrDeleteIndividualQuizMarks", new { individualid, eventid, marks },
                        commandType: CommandType.StoredProcedure);
                }
            }
            finally
            {
                FinaliseConnection(conn);
            }
        }

        internal void CreateAssessmentType(CreateAssessmentTypeViewModel model)
        {
            var conn = Connection();
            try
            {
                using (conn)
                {
                    conn.Execute("CreateAssessmentType", new { model.Type, model.Subject, model.AdditionalText, model.FilePath },
                        commandType: CommandType.StoredProcedure);
                }
            }
            finally
            {
                FinaliseConnection(conn);
            }
        }

        internal void CreateAssessment(CreateEventOccurenceViewModel model)
        {
            var conn = Connection();
            try
            {
                using (conn)
                {
                    conn.Execute("CreateAssessment", new { model.EventTypeId, model.EventName, model.DateConducted, model.ConductedBy, model.AdditionalText, model.MaxMarks, SessionHelper.LoggedInUser.Batch },
                        commandType: CommandType.StoredProcedure);
                }
            }
            finally
            {
                FinaliseConnection(conn);
            }
        }

        #region Delete

        internal void DeleteIndividualEvent(int individualEventId)
        {
            var conn = Connection();
            try
            {
                using (conn)
                {
                    conn.Execute("DeleteIndividualEvent", new { individualEventId },
                        commandType: CommandType.StoredProcedure);
                }
            }
            finally
            {
                FinaliseConnection(conn);
            }
        }

        internal void DeleteEvent(int eventId)
        {
            var conn = Connection();
            try
            {
                using (conn)
                {
                    conn.Execute("DeleteEvent", new { eventId },
                        commandType: CommandType.StoredProcedure);
                }
            }
            finally
            {
                FinaliseConnection(conn);
            }
        }

        internal void DeleteEventType(int eventTypeId)
        {
            var conn = Connection();
            try
            {
                using (conn)
                {
                    conn.Execute("DeleteEventType", new { eventTypeId },
                        commandType: CommandType.StoredProcedure);
                }
            }
            finally
            {
                FinaliseConnection(conn);
            }
        }

        #endregion

    }
}