namespace FirstAPI.Models.DTOs
{
    public class CustomerRegisterRequest 
    {
        public string Name { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public float Age { get; set; }

    }
}
