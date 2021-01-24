using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialNetwork.Models
{
    public class User
    {

        #region Fields
        public int UserID { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string LinkedInURL { get; set; }
        public Location Location { get; set; }
        public string Description { get; set; }
        #endregion

        #region Constructors
        protected User()
        {
        }
        public User(string fname, string lname, DateTime birth, string email, Location location, string linkedin = "", string description = "")
        {
            Firstname = fname;
            Lastname = lname;
            DateOfBirth = birth;
            Email = email;
            Location = location;
            LinkedInURL = linkedin;
            Description = description;
        }
        #endregion

        #region Methods
        #endregion
    }
}
