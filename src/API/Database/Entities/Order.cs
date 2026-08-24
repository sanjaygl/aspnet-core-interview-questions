namespace API.Database.Entities;

public class Order
{
    public int Id { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = "Pending"; // Pending, Shipped, Completed, Cancelled
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Foreign Key linking the order directly to the User who purchased it
    public int UserId { get; set; }
    public User User { get; set; } = new User();
}
