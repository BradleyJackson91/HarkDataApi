namespace HarkDataApi.DataTransferObjects.Exceptions
{
    public class BusinessException : Exception
    { 

        public BusinessException(string? message, Exception innerException)
            : base(message, innerException) { }
    }
}
