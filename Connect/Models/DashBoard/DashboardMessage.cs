using System;
using System.Collections.Generic;

namespace Connect.Models.DashBoard
{
    public class DashboardMessage
    {
        public int MessageId { get; set; }
        public string Text { get; set; }
        public string Status { get; set; }
        public List<string> Replies { get; set; }
        public string Reply { get; set; }
        public DateTime Created { get; set; }
    }
}