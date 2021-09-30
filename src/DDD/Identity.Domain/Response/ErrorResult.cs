using System.Collections.Generic;

namespace Identity.Domain.Response
{
    public class ErrorResult
    {
        public Dictionary<string, string> Validation { get; set; }
        public string Message { get; set; }

        public ErrorResult(string error)
        {
            this.Message = error;
        }

        public ErrorResult(Dictionary<string,string> errors, string message="Validation Fail")
        {
            this.Validation = errors;
            this.Message = message;
        }
    }
}