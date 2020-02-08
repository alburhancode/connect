namespace Connect.Controllers
{
    public abstract class ErrorBase
    {
        public string Error { get; set; }
        public int Errorcode { get; set; }

        protected ErrorBase()
        {
            Error = "Unknown error";
            Errorcode = -1;
        }
    }
}