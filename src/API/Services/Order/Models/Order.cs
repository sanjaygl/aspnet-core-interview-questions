namespace API.Services.Order.Models
{
    public class Order
    {
        public long OrderId { get; set; }
        public long CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string? OrderStatus { get; set; }
    }
}
