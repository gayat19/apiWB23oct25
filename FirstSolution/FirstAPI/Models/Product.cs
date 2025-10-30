using System.ComponentModel.DataAnnotations.Schema;

namespace FirstAPI.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        
        public float Price { get; set; }
        public bool IsDiscontinued { get; set; }
        public int Stock { get; set; }
        public int? CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }
    }
}
