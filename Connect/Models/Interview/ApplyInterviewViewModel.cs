using System.Collections.Generic;
using Connect.Classes.DataModels;

namespace Connect.Models.Interview
{
	public class ApplyInterviewViewModel
	{
        public List<InterviewSlot> AvailableInterviewSlots { get; set; }
        public int SelectedInterviewSlotId { get; set; }
    }
}