using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Models
{
    public class Message
    {
        #region Fields
        public int MessageID { get; set; }
        public DateTime DateTime { get; set; }
        public User User { get; set; }
        public string MessageText { get; set; }
        public Image Image { get; set; }
        public bool Editted { get; set; }
        #endregion

        #region Constructors
        protected Message()
        {
        }
        public Message(string text, User user, Image img = null)
        {
            DateTime = DateTime.Now;
            User = user;
            MessageText = text;
            Image = img;
            Editted = false;
        }
        #endregion

        #region Methods
        public void EditMessage(string text)
        {
            MessageText = text;
            DateTime = DateTime.Now;
            Editted = true;
        }

        public void EditImage(string imgpath)
        {
            Image.ImagePath = imgpath;
            Editted = true;
            DateTime = DateTime.Now;
        }
        public bool CanEditMessage(int user)
        {
            return User.UserID == user;
        }
        #endregion
    }
}
