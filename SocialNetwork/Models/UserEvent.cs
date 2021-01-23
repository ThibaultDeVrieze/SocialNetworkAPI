using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Models
{
    public class UserEvent
    {
        public int UserID { get; set; }
        public User User { get; set; }
        public int EventID { get; set; }
        public Event Event { get; set; }

        protected UserEvent()
        {

        }
        public UserEvent(User user, Event ev)
        {
            User = user;
            UserID = user.UserID;
            Event = ev;
            EventID = ev.EventID;
        }
    }
}
