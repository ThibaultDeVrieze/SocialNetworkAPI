using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.DTOs
{
    public class LocationDTO
    {
        public string Street { get; set; }
        public int HouseNr { get; set; }
        public int Postalcode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
