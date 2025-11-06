namespace FirstAPI.Models
{
    public class User
    {
        public string Username { get; set; } = string.Empty;
        public required byte[] Password { get; set; }
        public required byte[] HashKey { get; set; }
        public string? Role { get; set; }
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

    }
}
