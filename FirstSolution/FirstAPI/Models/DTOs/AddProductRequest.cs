namespace FirstAPI.Models.DTOs
{
    public class AddProductRequest
    {
        public string Title { get; set; } = string.Empty;
        public float Price { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }
    }
}
