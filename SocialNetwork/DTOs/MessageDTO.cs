using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.DTOs
{
    public class MessageDTO
    {
        public DateTime DateTime { get; set; }
        public int UserID { get; set; }
        public string MessageText { get; set; }
        public ImageDTO Image { get; set; }
    }
}
