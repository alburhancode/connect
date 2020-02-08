using Connect.Classes.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Connect.Models.Quiz
{
    public class CreateIndividualEventViewModel
    {
        public int IndividualId { get; set; }
        public int EventId { get; set; }
        public string Value { get; set; }
        public string AdditionalText { get; set; }
        public string FilePath { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public List<EventOccurence> EventOccurences { get; set; }
    }

    public class CreateAssessmentTypeViewModel
    {
        public int EventTypeId { get; set; }
        public string Type { get; set; }
        public string Subject { get; set; }
        public string AdditionalText { get; set; }
        public string FilePath { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }

    public class CreateAssessmentViewModel
    {
        public int EventId { get; set; }
        public int EventTypeId { get; set; }
        public string EventName { get; set; }
        public int MaxMarks { get; set; }
        public DateTime? DateConducted { get; set; }
        [Required]
        public string ConductedBy { get; set; }
        [Required]
        [DisplayName("Free Text-Total Marks, Date etc")]
        public string AdditionalText { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public List<EventType> EventTypes { get; set; }
    }

    public class CreateEventOccurenceViewModel
    {
        public int EventId { get; set; }
        public int EventTypeId { get; set; }
        public int MaxMarks { get; set; }
        public string EventName { get; set; }
        public DateTime DateConducted { get; set; }
        [Required]
        public string ConductedBy { get; set; }
        [Required]
        [DisplayName("Free Text-Total Marks, Date etc")]
        public string AdditionalText { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}