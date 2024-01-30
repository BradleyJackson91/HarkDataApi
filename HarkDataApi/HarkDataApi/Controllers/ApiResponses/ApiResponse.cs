using HarkDataApi.DataTransferObjects.Exceptions;
using Microsoft.OpenApi.Models;

namespace HarkDataApi.Controllers.ApiResponses
{
    public class ApiResponse
    {
        public int Status { get; set; }
        public object? Data { get; set; }
        public string? Message { get; set; }

        public static ApiResponse Success(object? data)
        {
            return new ApiResponse()
            {
                Status = 200,
                Data = data
            };
        }

        public static ApiResponse Error(int status, string? message)
        {
            return new ApiResponse()
            {
                Status = status,
                Message = message
            };
        }

        public static ApiResponse NotFound()
        {
            return new ApiResponse()
            {
                Status = 404,
                Message = "Data not found for specified parameters."
            };
        }
    }
}
