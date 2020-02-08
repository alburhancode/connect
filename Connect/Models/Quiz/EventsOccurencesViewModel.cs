using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Connect.Models.Quiz
{
    public class EventsOccurencesViewModel
    {
        public int EventId { get; set; }
        public string Type { get; set; }                // a Quiz or an assignment or an exam or etc
        public string Subject { get; set; }             // seerat
        public string EventName { get; set; }           // seerat quiz 1
        [DefaultValue(100)]
        public int MaxMarks { get; set; }           // seerat quiz 1

        [DisplayName("Conducted on")]
        public DateTime DateConducted { get; set; }
        public string DateConductedShort
        {
            get
            {
                return DateConducted.ToString("dd-MM-yyyy");
            }
        }
        public string ConductedBy { get; set; }
        [Required]
        [DisplayName("Free Text")]
        public string AdditionalText { get; set; }
    }
}