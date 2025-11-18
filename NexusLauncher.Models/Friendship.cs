using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexusLauncher.Models
{
    public class Friendship
    {
        public int Id { get; set; }
        public int UserId1 { get; set; }
        public int UserId2 { get; set; }
        public string Status { get; set; } // "Pending", "Accepted", "Rejected"
        public int RequestedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
