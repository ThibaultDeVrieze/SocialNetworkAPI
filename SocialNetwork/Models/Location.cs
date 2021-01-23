using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Models
{
    public class Location
    {
        #region Fields
        public int LocationID { get; set; }
        public string Street { get; set; }
        public int HouseNr { get; set; }
        public int Postalcode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        #endregion

        #region Constructors
        protected Location()
        {
        }
        public Location(string street = "", int housenr = 0, int postalcode = 9000, string city = "Gent", string country = "Belgium")
        {
            Street = street;
            HouseNr = housenr;
            Postalcode = postalcode;
            City = city;
            Country = country;
        }
        #endregion
    }
}
