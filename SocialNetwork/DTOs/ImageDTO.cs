using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.DTOs
{
    public class ImageDTO
    {
        public string ImagePath { get; set; }
        public UserDTO User { get; set; }
    }
}
