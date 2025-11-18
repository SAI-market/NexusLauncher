using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexusLauncher.Models
{
    public class FriendViewModel
    {
        public int Id { get; set; } // User.Id
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string FriendStatus { get; set; } // "Friend", "NotFriend", "PendingSent", "PendingReceived"
        public string Presence { get; set; } // "Online", "Offline", "Jugando"
    }
}
