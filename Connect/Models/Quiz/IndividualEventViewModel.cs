using System;

namespace Connect.Models.Quiz
{
    public class IndividualEventViewModel
    {
        public int IndividualEventId { get; set; }
        public int IndividualId { get; set; }
        public string Type { get; set; }
        public string Subject { get; set; }
        public string Value { get; set; }
        public string AdditionalText { get; set; }
        public string EventName { get; set; }
        public DateTime DateConducted { get; set; }
        public string DateConductedShort {
            get
            {
                return DateConducted.ToShortDateString();
            }
        }
        public string ConductedBy { get; set; }
        public string FilePath { get; set; }
        public int MarksObtained { get; set; }
        public int MaxMarks { get; set; }
    }
}