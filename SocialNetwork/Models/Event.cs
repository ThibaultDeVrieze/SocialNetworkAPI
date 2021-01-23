using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Models
{
    public class Event
    {
        #region Fields
        public int EventID { get; set; }
        public string EventName { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public Location Location { get; set; }
        public Image Image { get; set; }
        public User Founder { get; set; }
        public ICollection<UserEvent> UsersGoing { get; set; }
        public ICollection<Message> Messages { get; set; }
        #endregion

        #region Constructors
        protected Event()
        {
        }
        public Event(string name, DateTime date, User founder, Location location = null, Image img = null, string description = "")
        {
            EventName = name;
            Date = date;
            Founder = founder;
            Location = location;
            Image = img;
            Description = description;

            UsersGoing = new List<UserEvent>();
            UsersGoing.Add(new UserEvent(founder, this));
            Messages = new List<Message>();
        }
        #endregion

        #region Methods
        public void GoToEvent(User user)
        {
            UserEvent ue = new UserEvent(user, this);
            if (!UsersGoing.Contains(ue))
            {
                UsersGoing.Add(ue);
            }
        }
        public void DontGoToEvent(User user)
        {
            UserEvent ue = new UserEvent(user, this);
            if (UsersGoing.Contains(ue))
            {
                UsersGoing.Remove(UsersGoing.FirstOrDefault(u => u == ue));
            }
        }
        public void PostMessage(Message message)
        {
            Messages.Add(message);
        }
        public void DeleteMessage(Message message)
        {
            Messages.Remove(Messages.FirstOrDefault(i => i == message));
        }
        public bool UserGoesToEvent(User user)
        {
            UserEvent ue = new UserEvent(user, this);
            return UsersGoing.Contains(ue);
        }
        #endregion
    }
}
