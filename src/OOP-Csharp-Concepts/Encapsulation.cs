namespace OOP_CSharp_Concepts
{
    // =========================================================================
    // PART A: ENCAPSULATION
    // =========================================================================
    // Encapsulation means bundling data and the methods that operate on that
    // data inside a class and controlling how the internal state is accessed.
    //
    // The main purpose is to protect the object's state and prevent invalid
    // or unwanted changes from outside the class.
    // =========================================================================

    public class BankAccount
    {
        // ---------------------------------------------------------------------
        // PRIVATE FIELD
        // ---------------------------------------------------------------------
        // 'private' prevents outside code from directly accessing the field.
        // The field represents the internal state of the BankAccount object.
        // ---------------------------------------------------------------------

        private decimal balance;

        // ---------------------------------------------------------------------
        // PUBLIC PROPERTY WITH PRIVATE SET
        // ---------------------------------------------------------------------
        // Anyone can read the balance, but only this class can change it.
        // This provides controlled access to the internal state.
        // ---------------------------------------------------------------------

        public decimal Balance
        {
            get
            {
                return balance;
            }
            private set
            {
                balance = value;
            }
        }

        // ---------------------------------------------------------------------
        // CONSTRUCTOR
        // ---------------------------------------------------------------------
        // The constructor controls how the object is initially created.
        // Invalid initial values can be rejected before the object is created.
        // ---------------------------------------------------------------------

        public BankAccount(decimal initialBalance)
        {
            if (initialBalance < 0)
            {
                throw new ArgumentException("Initial balance cannot be negative.");
            }

            balance = initialBalance;
        }

        // ---------------------------------------------------------------------
        // DEPOSIT METHOD
        // ---------------------------------------------------------------------
        // External code cannot directly change 'balance'.
        // It must use Deposit(), where validation can be performed.
        // ---------------------------------------------------------------------

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Deposit amount must be greater than zero.");
            }

            balance += amount;
        }

        // ---------------------------------------------------------------------
        // WITHDRAW METHOD
        // ---------------------------------------------------------------------
        // The class controls how the balance can be reduced.
        // This prevents the object from entering an invalid state.
        // ---------------------------------------------------------------------

        public bool Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                return false;
            }

            if (amount > balance)
            {
                return false;
            }

            balance -= amount;
            return true;
        }
    }


    // =========================================================================
    // PART B: ENCAPSULATION USING PRIVATE DATA AND CONTROLLED METHODS
    // =========================================================================

    public class Employee
    {
        // ---------------------------------------------------------------------
        // PRIVATE FIELD
        // ---------------------------------------------------------------------
        // External code cannot directly change the employee's age.
        // ---------------------------------------------------------------------

        private int age;

        // ---------------------------------------------------------------------
        // PROPERTY WITH VALIDATION
        // ---------------------------------------------------------------------
        // The setter controls what value is allowed.
        // ---------------------------------------------------------------------

        public int Age
        {
            get
            {
                return age;
            }
            private set
            {
                if (value >= 18)
                {
                    age = value;
                }
            }
        }

        // ---------------------------------------------------------------------
        // METHOD TO CHANGE STATE
        // ---------------------------------------------------------------------
        // Business rules can be applied before modifying the private field.
        // ---------------------------------------------------------------------

        public void SetAge(int newAge)
        {
            if (newAge < 18)
            {
                throw new ArgumentException("Employee must be at least 18 years old.");
            }

            Age = newAge;
        }
    }


    // =========================================================================
    // PART C: READ-ONLY DATA
    // =========================================================================

    public class EmployeeProfile
    {
        // ---------------------------------------------------------------------
        // GET-ONLY PROPERTY
        // ---------------------------------------------------------------------
        // The value can be assigned during construction but cannot be changed
        // through the public interface afterward.
        // ---------------------------------------------------------------------

        public string EmployeeId { get; }

        public string Name { get; private set; }

        public EmployeeProfile(string employeeId, string name)
        {
            EmployeeId = employeeId;
            Name = name;
        }

        // ---------------------------------------------------------------------
        // CONTROLLED UPDATE
        // ---------------------------------------------------------------------
        // Instead of exposing a public setter, the class controls how Name
        // can be changed.
        // ---------------------------------------------------------------------

        public void ChangeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be empty.");
            }

            Name = name;
        }
    }


    // =========================================================================
    // PART D: EXECUTION PLATFORM
    // =========================================================================

    public class EncapsulationRunner
    {
        public static void Run()
        {
            // =================================================================
            // 1. OBJECT CREATION
            // =================================================================

            BankAccount account = new BankAccount(1000);

            // =================================================================
            // 2. READING CONTROLLED DATA
            // =================================================================

            Console.WriteLine($"Initial Balance: {account.Balance}");

            // =================================================================
            // 3. CONTROLLED STATE CHANGE
            // =================================================================

            account.Deposit(500);

            Console.WriteLine($"After Deposit: {account.Balance}");

            // =================================================================
            // 4. CONTROLLED WITHDRAWAL
            // =================================================================

            bool success = account.Withdraw(300);

            Console.WriteLine($"Withdrawal Successful: {success}");
            Console.WriteLine($"Final Balance: {account.Balance}");

            // =================================================================
            // 5. DIRECT FIELD ACCESS IS NOT ALLOWED
            // =================================================================

            // account.balance = -500;
            // ❌ Compile error because 'balance' is private.

            // =================================================================
            // 6. PRIVATE SETTER
            // =================================================================

            // account.Balance = 5000;
            // ❌ Compile error because Balance has a private setter.
        }
    }
}

// =========================================================================
// PART E: QUICK INTERVIEW QUESTIONS
// =========================================================================
//
// 1. What is encapsulation, and why do we use it?
// 2. Why should fields usually be private?
// 3. What is the difference between a field and a property?
// 4. What is the purpose of a private setter?
// 5. Can we achieve encapsulation without using properties?
// 6. How does encapsulation help prevent invalid object state?
// 7. What is the difference between data hiding and encapsulation?
// 8. What is the difference between abstraction and encapsulation?
// 9. Why should we prefer methods over public setters when business validation
//    is required?
// 10. Can a private field be accessed directly from a derived class?
// 11. What is the difference between private and protected members?
// 12. Can a property contain validation logic?
// 13. Why is exposing public fields generally considered poor encapsulation?
// 14. Can encapsulation be achieved using only access modifiers?
// 15. How does encapsulation help with maintainability and loose coupling?
//
// =========================================================================