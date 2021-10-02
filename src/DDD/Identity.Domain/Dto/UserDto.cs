using System.Collections.Generic;

namespace Identity.Domain.Dto
{
    public class UserDto
    {
        public UserDto()
        {
            this.Roles = new List<string>();
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public List<string> Roles { get; set; }
    }
}