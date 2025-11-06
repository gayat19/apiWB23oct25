namespace FirstAPI.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public float Age { get; set; }
        public ICollection<Order>? Orders { get; set; }

        public User? User { get; set; }
    }
}
