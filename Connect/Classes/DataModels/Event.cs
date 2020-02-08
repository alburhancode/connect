using System;

namespace Connect.Classes.DataModels
{
    public class EventType
    {
        public int EventTypeId { get; set; }
        public string Type { get; set; }                // a Quiz or an assignment or an exam or etc
        public string Subject { get; set; }             // seerat
        public string AdditionalText { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string FilePath { get; set; }
        public string TypeSubjectCombined
        {
            get
            {
                return $"{Type} - {Subject}";
            }
        }
    }

    public class EventOccurence
    {
        public int EventId { get; set; }
        public int EventTypeId { get; set; }
        public string EventName { get; set; }           // seerat quiz 1
        public string AdditionalText { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string Type { get; set; }
        public string Subject { get; set; }
        public DateTime DateConducted { get; set; }
        public string ConductedBy { get; set; }
        public string AboutThisAssessment
        {
            get
            {
                return $"{Type} of {Subject} by {ConductedBy} on {DateConducted.ToString("dd-MM-yyyy")}";
            }
        }
    }

    public class IndividualEvent
    {
        public int IndividualEventId { get; set; }
        public int IndividualId { get; set; }
        public int EventId { get; set; }
        public string Value { get; set; }
        public string AdditionalText { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string FilePath { get; set; }
    }
}