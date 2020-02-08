namespace Connect.Controllers
{
    internal class AttendanceLogsNotProvidedError : ErrorBase
    {
        public AttendanceLogsNotProvidedError()
        {
            Error = "Client log upload failed. No client logs were provided in the request.";
            Errorcode = 400;
        }
    }
}