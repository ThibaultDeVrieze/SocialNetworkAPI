using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Models
{
    public class Image
    {
        #region Fields
        public int ImageID { get; set; }
        public string ImagePath { get; set; }
        public User User { get; set; }
        #endregion

        #region Constructors
        protected Image()
        {
        }
        public Image(string imagepath, User user)
        {
            ImagePath = imagepath;
            User = user;
        }
        #endregion
    }
}
