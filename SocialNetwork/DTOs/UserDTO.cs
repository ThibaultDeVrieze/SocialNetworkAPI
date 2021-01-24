using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.DTOs
{
    public class UserDTO
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string LinkedInURL { get; set; }
        public LocationDTO Location { get; set; }
        public string Description { get; set; }
    }
}
