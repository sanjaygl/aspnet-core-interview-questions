namespace CodingProblem
{
    // Sealed prevents inheritance, ensuring no subclass can break the Singleton pattern
    // by creating multiple instances through derived classes.
    public sealed class Singleton
    {
        // Volatile ensures proper memory barriers for double-checked locking pattern.
        // Without volatile, compiler/CPU reordering could allow threads to see a 
        // non-null _instance before the constructor completes, causing access to
        // a partially constructed object.
        private static volatile Singleton _instance;

        // Lock object for thread synchronization in double-checked locking pattern.
        // A dedicated object is used as a lock rather than locking on 'this' or the type itself
        // to prevent external code from causing deadlocks. This object's sole purpose is to 
        // serve as a mutex - only one thread can hold this lock at a time.
        private static object _instanceLock = new object();

        private Singleton()
        {
            // Private constructor to prevent instantiation from outside
        }

        public static Singleton GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    // Lock ensures thread safety by preventing multiple threads from creating 
                    // separate instances simultaneously. Without this lock, in a multi-threaded 
                    // scenario, two threads could both pass the null check above at the same time,
                    // and both would create their own instance, violating the Singleton pattern.
                    // The lock creates a "critical section" - only one thread can enter at a time.
                    // Other threads must wait until the lock is released before they can enter.
                    lock (_instanceLock)
                    {
                        // Double-check after acquiring lock because another thread might have 
                        // created the instance while this thread was waiting for the lock.
                        // This is called "double-checked locking" pattern.
                        if (_instance == null)
                        {
                            _instance = new Singleton();
                        }
                    }
                }
                return _instance;
            }
        }

        public void DoWork()
        {
            Console.WriteLine("Doing some work...");
        }
    }

    public sealed class Logger
    {
        private static Lazy<Logger> _instance = new Lazy<Logger>(() => new Logger());
        private Logger()
        {
        }

        public static Logger GetInstance => _instance.Value;

        public void Log(string message)
        {
            Console.WriteLine($"[{DateTime.Now}] {message}");
        }
    }
}
