using OOP_CSharp_Concepts.Abstraction;

namespace OOP_CSharp_Concepts
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //ImplementationExample example = new ImplementationExample();
            //Console.WriteLine(example.Print("Hello Interface"));

            //DerivedImplementationExample derivedImplementationExample = new DerivedImplementationExample();
            //Console.WriteLine(derivedImplementationExample.Print("Hello Abstract"));

            //PolymorphismRunner.RunFullDemonstration();
            PolyRunner.Run();

            // YieldRunner.Run();

            //DelegateRunner.Run();
            //EqualOperator.Run();

            Console.ReadLine();
        }
    }
}