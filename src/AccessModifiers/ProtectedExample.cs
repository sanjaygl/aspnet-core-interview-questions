namespace AccessModifiers
{
    /// <summary>
    /// Base class example demonstrating protected members
    /// Protected members are accessible within the class and derived classes
    /// </summary>
    public class ProtectedExample
    {
        // Protected field - accessible in derived classes
        protected string baseData = "Base Data";
        
        // Protected method
        protected void ProcessData()
        {
            Console.WriteLine($"Processing: {baseData}");
        }
        
        // Protected property
        protected int InternalCounter { get; set; }
        
        // Protected constructor - commonly used in abstract classes
        protected ProtectedExample()
        {
            InternalCounter = 0;
        }
        
        // Public method that uses protected members
        public void DisplayInfo()
        {
            Console.WriteLine($"Base Data: {baseData}");
            ProcessData();
        }
    }
    
    /// <summary>
    /// Derived class in same assembly accessing protected members
    /// </summary>
    public class DerivedInSameAssembly : ProtectedExample
    {
        public void AccessProtectedMembers()
        {
            // ✅ Can access protected members from base class
            Console.WriteLine($"Accessing from derived class: {baseData}");
            ProcessData();
            InternalCounter = 10;
            Console.WriteLine($"Internal Counter: {InternalCounter}");
        }
        
        public void ModifyBaseData(string newData)
        {
            baseData = newData; // ✅ Can modify protected field
            ProcessData();
        }
    }
}
