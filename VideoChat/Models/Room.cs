using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VideoChat.Models
{
    public class Room
    {
        public int RoomId { get; set; }
        public string SessionId { get; set; }
        public string RoomName { get; set; }
        public string Token { get; set; }
    }
}
