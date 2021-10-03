using System.Collections.Generic;

namespace Identity.Domain.Entities
{
    public class Client
    {
        public Client()
        {
            this.Audiences = new List<string>();
        }
        public string Id { get; set; }
        public string Secret { get; set; }
        public List<string> Audiences { get; set; }
    }
}