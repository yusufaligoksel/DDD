using System;

namespace Identity.Domain.Entities
{
    public class User:BaseEntity
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string OldPasswordHash { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public byte UserStatus { get; set; }
        public byte LoginStatus { get; set; }
        public int IncorrectLoginCount { get; set; }
        public DateTime? BirthDay { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}