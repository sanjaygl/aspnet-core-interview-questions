using OOP_Concepts.Abstraction;
using OOP_Concepts.PolymorphismMastery;

namespace OOP_Concepts
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ImplementationExample example = new ImplementationExample();
            Console.WriteLine(example.Print("Hello Interface"));

            DerivedImplementationExample derivedImplementationExample = new DerivedImplementationExample();
            Console.WriteLine(derivedImplementationExample.Print("Hello Abstract"));

            PolymorphismRunner.RunFullDemonstration();
            PolyRunner.Run();

            Console.ReadLine();
        }
    }
}