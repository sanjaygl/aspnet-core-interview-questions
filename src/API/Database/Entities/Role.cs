namespace API.Database.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // One-to-Many navigation relationship property to the Session table
        public ICollection<User> Users { get; set; } = [];
    }
}
