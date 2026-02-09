namespace ThreadingPractice
{
    public class BankAccount
    {
        public int Balance;
        public object LockObj = new object();

        public BankAccount(int balance)
        {
            Balance = balance;
        }

        public void Withdraw(int amount)
        {
            if (Balance >= amount)
            {
                // Simulate processing delay
                Task.Delay(1).Wait();

                Balance -= amount;
                Console.WriteLine($"Withdrawn: {amount}, Remaining Balance: {Balance}");
            }
            else
            {
                Console.WriteLine($"Insufficient balance. Available: {Balance}, Requested: {amount}");
            }
        }

        public void WithdrawFixed(int amount)
        {
            lock (LockObj)
            {
                if (Balance >= amount)
                {
                    Task.Delay(1).Wait();
                    Balance -= amount;
                    Console.WriteLine($"Withdrawn: {amount}, Remaining Balance: {Balance}");
                }
                else
                {
                    Console.WriteLine($"Insufficient balance. Available: {Balance}, Requested: {amount}");
                }
            }
        }

        public void Run()
        {
            Task t1 = Task.Run(() => Withdraw(800));
            Task t2 = Task.Run(() => Withdraw(800));

            Task.WaitAll(t1, t2);

            Console.WriteLine($"Final Balance: {Balance}");
        }
    }

    public class DeadlockExample
    {
        public void Run()
        {
            var accountA = new BankAccount(1000);
            var accountB = new BankAccount(1000);

            Task t1 = Task.Run(() => Transfer(accountA, accountB, 100));
            Task t2 = Task.Run(() => Transfer(accountB, accountA, 200));

            Task.WaitAll(t1, t2);
        }

        public void Transfer(BankAccount from, BankAccount to, int amount)
        {
            lock (from.LockObj)
            {
                Console.WriteLine($"Locked FROM account");

                Thread.Sleep(100); // Simulate delay

                lock (to.LockObj)
                {
                    Console.WriteLine($"Locked TO account");

                    from.Balance -= amount;
                    to.Balance += amount;
                }
            }
        }
    }

    public class ThreadExample
    {
        static int counter = 0;

        // Runs on the calling thread - blocks it for 10 seconds
        public void ProcessData()
        {
            Console.WriteLine("ProcessData " + DateTime.Now);
            Thread.Sleep(10000); // Blocks the current thread
            Console.WriteLine("Done");
            Console.WriteLine("ProcessData " + DateTime.Now);
        }

        // Runs on a ThreadPool thread - does not block the caller
        public void ProcessDataInBackground()
        {
            Task.Run(() =>
            {
                Console.WriteLine("ProcessDataInBackground " + DateTime.Now);
                Thread.Sleep(10000); // Blocks the worker thread only
                Console.WriteLine("Done");
                Console.WriteLine("ProcessDataInBackground " + DateTime.Now);
            });

            Console.WriteLine("Caller continues immediately " + DateTime.Now);
        }

        public void CuncurrancyExample()
        {
            Parallel.For(0, 10000, i =>
            {
                counter++;   // Not thread-safe
            });

            Console.WriteLine($"Counter: {counter}");
        }
    }
}
