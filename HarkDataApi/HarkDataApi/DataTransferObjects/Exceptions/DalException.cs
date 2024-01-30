namespace HarkDataApi.DataTransferObjects.Exceptions
{
    public class DalException : Exception
    {
        public DalException(string message, Exception? innerException = null) 
            : base(message, innerException) { }
    }
}
