using System.Net;

namespace Identity.Domain.Response
{
    public class GenericResult<T>
    {
        public string Version => "1.0.0";
        public bool Success { get; set; }
        public int StatusCode { get; set; } = (int)HttpStatusCode.OK;
        public string Message { get; set; }
        public T Data { get; set; }
        public ErrorResult Errors { get; set; }
        
        public static GenericResult<T> SuccessResponse(T data, int statusCode, string message = "OK")
        {
            return new GenericResult<T> { Success = true, Data = data, StatusCode = statusCode, Message = message };
        }

        public static GenericResult<T> ErrorResponse(ErrorResult error, int statusCode, string message = "Fail")
        {
            return new GenericResult<T> { Success = false, Errors = error, StatusCode = statusCode, Message = message };
        }
    }
}