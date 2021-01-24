using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.DTOs
{
    public class EventDTO
    {
        public string EventName { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public LocationDTO Location { get; set; }
        public ImageDTO Image { get; set; }
        public UserDTO Founder { get; set; }
    }
}
