using System.Collections.Generic;

namespace Identity.Domain.Response
{
    public class ErrorResult
    {
        public Dictionary<string, List<string>> Validation { get; set; }
        public List<string> System { get; set; }
    }
}