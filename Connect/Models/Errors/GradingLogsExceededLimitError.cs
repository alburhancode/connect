namespace Connect.Controllers
{
    internal class GradingLogsExceededLimitError : ErrorBase
    {
        public GradingLogsExceededLimitError()
        {
            Error = "Too many records.";
            Errorcode = 400;
        }
    }
}