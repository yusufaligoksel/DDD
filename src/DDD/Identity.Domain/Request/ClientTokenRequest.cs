namespace Identity.Domain.Request
{
    public class ClientTokenRequest
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}