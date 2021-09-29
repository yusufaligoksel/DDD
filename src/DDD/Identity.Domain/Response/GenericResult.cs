using System.Net;

namespace Identity.Domain.Response
{
    public class GenericResult<T>
    {
        public string Version => "1.0.0";
        public bool Status { get; set; }
        public int StatusCode { get; set; } = (int)HttpStatusCode.OK;
        public string Message { get; set; }
        public T Data { get; set; }
        public ErrorResult Errors { get; set; }
    }
}