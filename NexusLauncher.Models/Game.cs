using System;

namespace NexusLauncher.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string InstallPath { get; set; }
        public bool IsInstalled { get; set; }

        public DateTime? PurchaseDate { get; set; }
        public bool Owned { get; set; }

        // Nuevos campos
        public decimal Price { get; set; } = 0m;
        public byte[] Image { get; set; }             // BLOB
        public string ImageFileName { get; set; }
        public string ImageContentType { get; set; }  // MIME type, e.g. "image/jpeg"
    }
}