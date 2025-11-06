namespace FirstAPI.Models
{
    public class SpLoginReturn
    {
        public required byte[] Password { get; set; }
        public required byte[] HashKey { get; set; }
        public string? Role { get; set; }
    }
}
