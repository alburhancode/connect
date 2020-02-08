using Connect.Classes.DataModels;
using System.Collections.Generic;

namespace Connect.Models
{
    public class ApplyTimingViewModel
    {
        public List<BatchTiming> AvailableTimings { get; set; }
        public int SelectedBatchTimingId { get; set; }
    }
}