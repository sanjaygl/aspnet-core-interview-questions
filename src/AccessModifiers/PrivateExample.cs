namespace AccessModifiers
{
    /// <summary>
    /// Example demonstrating private members
    /// Private members are accessible only within the same class
    /// </summary>
    public class PrivateExample
    {
        // Private field - only accessible within this class
        private string secretKey = "MySecretKey123";
        
        // Private method
        private void InternalProcess()
        {
            Console.WriteLine($"Processing with secret key: {secretKey}");
        }
        
        // Private property
        private int InternalCounter { get; set; }
        
        // Public method that uses private members
        public void Execute()
        {
            InternalCounter++;
            Console.WriteLine($"Execution count: {InternalCounter}");
            InternalProcess(); // ✅ Can access private method within same class
        }
        
        // Private nested class - only accessible within PrivateExample
        private class InternalHelper
        {
            public void Help()
            {
                Console.WriteLine("Internal helper working");
            }
        }
        
        // Public method using private nested class
        public void UseHelper()
        {
            var helper = new InternalHelper(); // ✅ Can create private nested class instance
            helper.Help();
        }
    }
}
