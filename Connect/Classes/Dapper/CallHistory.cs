using Connect.Models.Quiz;

namespace Connect.Classes.Dapper
{
    public class CallHistory
    {
        public int IndividualId { get; set; }
        public CallOutcome CallOutcome { get; set; }
        public string CallNotes { get; set; }
        public string Caller { get; set; }
        public CallerKefiyat Kefiyat { get; set; }
    }
}