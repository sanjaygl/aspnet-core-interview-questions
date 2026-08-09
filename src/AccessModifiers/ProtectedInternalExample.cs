namespace AccessModifiers
{
    /// <summary>
    /// Example demonstrating protected internal members
    /// Protected internal = protected OR internal (union - more permissive)
    /// Accessible: same assembly OR derived classes (any assembly)
    /// </summary>
    public class ProtectedInternalExample
    {
        // Protected internal field
        // Accessible from: same assembly OR derived classes in any assembly
        protected internal string SharedData = "Shared Data";
        
        // Protected internal method
        protected internal void SharedMethod()
        {
            Console.WriteLine("Protected internal method called");
            Console.WriteLine($"SharedData: {SharedData}");
        }
        
        // Protected internal property
        protected internal int SharedCounter { get; set; }
        
        // Protected internal constructor
        protected internal ProtectedInternalExample()
        {
            SharedCounter = 0;
        }
        
        // Public method using protected internal members
        public void DisplayInfo()
        {
            SharedMethod();
            Console.WriteLine($"Counter: {SharedCounter}");
        }
    }
    
    /// <summary>
    /// Derived class in SAME assembly accessing protected internal members
    /// </summary>
    public class SameAssemblyDerived : ProtectedInternalExample
    {
        public void AccessProtectedInternalMembers()
        {
            // ✅ Can access protected internal members (derived class)
            Console.WriteLine($"Accessing SharedData: {SharedData}");
            SharedMethod();
            SharedCounter = 100;
        }
    }
    
    /// <summary>
    /// Non-derived class in SAME assembly accessing protected internal members
    /// </summary>
    public class SameAssemblyNonDerived
    {
        public void TestAccess()
        {
            var obj = new ProtectedInternalExample();
            
            // ✅ Can access protected internal members (same assembly - internal part)
            Console.WriteLine(obj.SharedData);
            obj.SharedMethod();
            obj.SharedCounter = 50;
        }
    }
}
