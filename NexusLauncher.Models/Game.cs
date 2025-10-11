using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexusLauncher.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string InstallPath { get; set; }
        public bool IsInstalled { get; set; }
    }
}

