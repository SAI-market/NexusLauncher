using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexusLauncher.Models
{
    public class FriendRequestDto
    {
        public int FriendshipId { get; set; }
        public int FromUserId { get; set; }
        public string FromDisplayName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
