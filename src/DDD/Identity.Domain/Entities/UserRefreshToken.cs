using System;

namespace Identity.Domain.Entities
{
    public class UserRefreshToken:BaseEntity
    {
        public int UserId { get; set; }
        public string RefreshToken { get; set; }
        public DateTime Expiration { get; set; }
    }
}