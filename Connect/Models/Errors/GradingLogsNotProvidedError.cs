namespace Connect.Controllers
{
    internal class GradingLogsNotProvidedError : ErrorBase
    {
        public GradingLogsNotProvidedError()
        {
            Error = "logs upload failed. No logs were provided in the request.";
            Errorcode = 400;
        }
    }
}