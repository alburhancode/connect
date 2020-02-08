namespace Connect.Controllers
{
    internal class ClientLogsExceededLimitError : ErrorBase
    {
        public ClientLogsExceededLimitError()
        {
            Error = "Too many records.";
            Errorcode = 400;
        }
    }
}