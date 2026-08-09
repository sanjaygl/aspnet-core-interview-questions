namespace AccessModifiers
{
    /// <summary>
    /// Public Class Example - Accessible from any assembly
    /// Example: public class can be used anywhere
    /// </summary>
    public class PublicExample
    {
        // Public field - accessible from anywhere
        public string Name = "Public Field";
        
        // Public property
        public int Count { get; set; }
        
        // Public method
        public void DisplayMessage()
        {
            Console.WriteLine("Hello from PublicExample in ClassLibraryA");
            Console.WriteLine($"Name: {Name}, Count: {Count}");
        }
        
        // Public constructor
        public PublicExample()
        {
            Name = "Default Name";
            Count = 0;
        }
        
        public PublicExample(string name, int count)
        {
            Name = name;
            Count = count;
        }
    }
}
