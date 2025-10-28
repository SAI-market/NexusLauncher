namespace NexusLauncher.Models
{
    public class User
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; } 
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; } 
    }
}