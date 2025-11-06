namespace FirstAPI.Models
{
    public class Order
    {
        public int OrderNumber { get; set; }
        public int CustomerId { get; set; }
        public DateTime DateOfOrder { get; set; }
        public decimal Total { get; set; }

      
        public Customer? Customer { get; set; }
        public ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}
