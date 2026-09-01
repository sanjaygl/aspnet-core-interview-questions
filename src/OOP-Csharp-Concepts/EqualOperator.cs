using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_CSharp_Concepts
{
    /// <summary>
    /// A. For strings, both == and Equals() compare values (content) because string overrides Equals() 
    /// and overloads the == operator to perform value comparison instead of reference comparison.
    /// == is null-safe, while calling Equals() on a null object throws a NullReferenceException.
    /// For reference types (custom objects), both == and Equals() compare references by default, 
    /// unless Equals() and/or == are explicitly overridden.
    /// </summary>
    internal class EqualOperator
    {
        public static void Run()
        {
            // ==========================================
            // SECTION 1: REFERENCE TYPES (CUSTOM OBJECTS)
            // ==========================================

            // Creating two separate instances with identical property values
            var p1 = new Person { Name = "Sanjay" };
            var p2 = new Person { Name = "Sanjay" };

            // Returns FALSE because they point to different memory addresses (reference comparison)
            Console.WriteLine(p1 == p2);

            // Returns FALSE because default Equals() behaves like == for custom classes
            Console.WriteLine(p1.Equals(p2));

            // ==========================================
            // SECTION 2: STRINGS (SPECIAL CASE)
            // ==========================================

            string a = "Hello";
            string b = "Hello";
            string c = null;

            // Returns TRUE because the == operator is overloaded in Strings to compare values
            Console.WriteLine(a == b);

            // Returns TRUE because the Equals() method is overridden in Strings to compare values
            Console.WriteLine(a.Equals(b));

            // Returns FALSE because 'a' has a value and 'c' is null (== is completely null-safe)
            Console.WriteLine(a == c);

            // Returns TRUE because it successfully checks for null without crashing
            Console.WriteLine(c == null);

            // ==========================================
            // SECTION 3: NULL HANDLING DIFFERENCE
            // ==========================================

            try
            {
                // CRASHES: Calling an instance method (.Equals) on a null reference throws an error
                //Console.WriteLine(c.Equals(a));
            }
            catch (NullReferenceException ex)
            {
                // Caught exception successfully demonstrated
                Console.WriteLine($"❌ Equals() threw Exception: {ex.Message}");
            }
        }
    }

    // Simple custom class to demonstrate reference type comparison behavior
    class Person
    {
        public string Name { get; set; }
    }

}
