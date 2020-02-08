using System.Collections.Generic;

namespace Connect.Models.Grading.Api
{
    public class GradingPostBody
    {
        public string Email { get; set; }
        public string CreatedAt { get; set; }
        
        public List<GradingRequestLog> Grading { get; set; }

        public GradingPostBody()
        {
            Grading = new List<GradingRequestLog>();
        }
    }
}