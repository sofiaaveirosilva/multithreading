namespace multithreading
{
    //https://www.pluralsight.com/guides/lock-statement-best-practices
    // Threads and Multithreading:
    // The process of multi-threading allows multiple tasks to execute concurrently, and to manipulate other tasks.
    // We use threads to run something which does not need user interaction
    // and which runs in parallel with the normal operations by the user.

    using System;
    using System.Threading;

    class Program
    {
        static void Main()
        {
            // Declare a SocialNetwork
            SocialNetwork socialNetwork = new SocialNetwork();

            // initialize 3 threads class object 
            Thread thread1 = new Thread(() => socialNetwork.Print1("Twitter!"));  // sends a parameter
            Thread thread2 = new Thread(() => socialNetwork.Print2());            // doesn't send any parameter
            Thread thread3 = new Thread(() => socialNetwork.Print3("Linkedin!")); // sends a parameter

            // we can explicitly assign priority to a thread
            // that doesn't guarantee us that the thread will the first or the last to be executed
            // setting the thread priority to highest will not correspond to real-time execution
            thread2.Priority = ThreadPriority.Highest;
            thread1.Priority = ThreadPriority.Lowest;
            thread3.Priority = ThreadPriority.Lowest;

            // start running our threads  
            thread1.Start();
            thread2.Start();
            thread3.Start();

            // this code is read at the same time as the threads are being executed
            Console.WriteLine("threads are being executed!");

            // wait until all threads are done with its execution.
            thread1.Join();
            thread2.Join();
            thread3.Join();

            // after executing the threads display message
            Console.WriteLine("Press Enter to terminate!");
            Console.ReadLine();
        }
    }
}
