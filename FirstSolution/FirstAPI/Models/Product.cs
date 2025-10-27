namespace FirstAPI.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        
        public float Price { get; set; }
        public bool IsDiscontinued { get; set; }
        public int Stock { get; set; }
    }
}
