namespace FirstAPI.Models
{
    public class OrderDetail
    {
        public int SNo { get; set; }
        public int ProductId { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
        public Product? Product { get; set; }
        public int OrderNumber { get; set; }
        public Order? Order { get; set; }
    }
}
