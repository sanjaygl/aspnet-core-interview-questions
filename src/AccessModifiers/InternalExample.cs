namespace AccessModifiers
{
    /// <summary>
    /// Internal class example - only accessible within ClassLibraryA assembly
    /// Perfect for implementation details that should not be exposed to external consumers
    /// </summary>
    internal class InternalExample
    {
        // Internal field
        internal string InternalData = "Internal Data";
        
        // Internal method
        internal void InternalOperation()
        {
            Console.WriteLine("Internal operation in ClassLibraryA");
            Console.WriteLine($"Data: {InternalData}");
        }
        
        // Internal property
        internal int InternalCounter { get; set; }
        
        // Internal constructor
        internal InternalExample()
        {
            InternalCounter = 0;
        }
        
        internal InternalExample(string data)
        {
            InternalData = data;
            InternalCounter = 0;
        }
    }
    
    /// <summary>
    /// Public class with internal members
    /// Shows how to hide implementation details while exposing a public API
    /// </summary>
    public class PublicClassWithInternalMembers
    {
        // Internal field - accessible only within same assembly
        internal ILogger? Logger;
        
        // Internal method - accessible only within same assembly
        internal void InternalHelper()
        {
            Console.WriteLine("Internal helper method");
        }
        
        // Public method that uses internal members
        public void PublicMethod()
        {
            InternalHelper(); // ✅ Can call internal method from same assembly
            Logger?.Log("Public method called");
        }
        
        // Internal static helper
        internal static class StringHelpers
        {
            internal static string FormatData(string data)
            {
                return $"[{data}]";
            }
        }
    }
    
    /// <summary>
    /// Another internal class that uses InternalExample
    /// Demonstrates internal collaboration within the same assembly
    /// </summary>
    internal class InternalProcessor
    {
        public void ProcessData()
        {
            // ✅ Can create and use InternalExample within same assembly
            var internalObj = new InternalExample("Processor Data");
            internalObj.InternalOperation();
            
            // ✅ Can use internal static helper
            var formatted = PublicClassWithInternalMembers.StringHelpers.FormatData("Test");
            Console.WriteLine(formatted);
        }
    }
    
    // Internal interface - only visible within assembly
    internal interface ILogger
    {
        void Log(string message);
    }
}
