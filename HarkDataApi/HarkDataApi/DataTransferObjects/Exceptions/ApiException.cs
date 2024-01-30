namespace HarkDataApi.DataTransferObjects.Exceptions
{
    public class ApiException : Exception
    {
        public int Status { get; set; }

        public ApiException(int status, string message, Exception? innerException = null)
            : base(message, innerException)
        {
            Status = status;
        }
    }
}
