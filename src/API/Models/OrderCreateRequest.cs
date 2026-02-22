namespace API.Models
{
    public class OrderCreateRequest
    {
        public long OrderId { get; set; }
        public long CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
