namespace AccessModifiers
{
    /// <summary>
    /// Example demonstrating private protected members (C# 7.2+)
    /// Private protected = protected AND internal (intersection - more restrictive)
    /// Accessible: derived classes in SAME assembly ONLY
    /// </summary>
    public class PrivateProtectedExample
    {
        // Private protected field
        // Accessible: derived classes in same assembly ONLY
        private protected string RestrictedData = "Restricted Data";
        
        // Private protected method
        private protected void RestrictedMethod()
        {
            Console.WriteLine("Private protected method called");
            Console.WriteLine($"RestrictedData: {RestrictedData}");
        }
        
        // Private protected property
        private protected int RestrictedCounter { get; set; }
        
        // Private protected constructor
        private protected PrivateProtectedExample()
        {
            RestrictedCounter = 0;
        }
        
        // Public method using private protected members
        public void DisplayInfo()
        {
            RestrictedMethod();
            Console.WriteLine($"Counter: {RestrictedCounter}");
        }
    }
    
    /// <summary>
    /// Derived class in SAME assembly
    /// ✅ Can access private protected members
    /// </summary>
    public class SameAssemblyDerivedPrivateProtected : PrivateProtectedExample
    {
        public void AccessPrivateProtectedMembers()
        {
            // ✅ Can access private protected members
            // (derived class in same assembly)
            Console.WriteLine($"Accessing RestrictedData: {RestrictedData}");
            RestrictedMethod();
            RestrictedCounter = 42;
        }
        
        public void ModifyRestrictedData(string newData)
        {
            RestrictedData = newData; // ✅ Can modify private protected field
            RestrictedMethod();
        }
    }
    
    /// <summary>
    /// Non-derived class in SAME assembly
    /// ❌ Cannot access private protected members (not derived)
    /// </summary>
    public class SameAssemblyNonDerivedPrivateProtected
    {
        public void TestAccess()
        {
            var obj = new SameAssemblyDerivedPrivateProtected();
            
            // ❌ Cannot access private protected members
            // Even though we're in the same assembly, we're not a derived class
            // Console.WriteLine(obj.RestrictedData);     // Compilation error
            // obj.RestrictedMethod();                    // Compilation error
            // obj.RestrictedCounter = 10;                // Compilation error
            
            // ✅ Can only call public methods
            obj.DisplayInfo();
        }
    }
}

/* 
 * KEY DIFFERENCE: private protected vs protected
 * 
 * protected:
 * - Accessible by derived classes in ANY assembly
 * - External assemblies CAN inherit and access protected members
 * 
 * private protected:
 * - Accessible by derived classes in SAME assembly ONLY
 * - External assemblies CANNOT access even if they inherit
 * 
 * Use private protected when you want:
 * - Internal extensibility (inheritance within your library)
 * - Prevent external assemblies from overriding sensitive methods
 * - More control over your class hierarchy
 */
