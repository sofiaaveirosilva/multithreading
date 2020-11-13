namespace multithreading
{
    //https://www.pluralsight.com/guides/lock-statement-best-practices
    using System;
    using System.Threading;

    class Program
    {
        static void Main()
        {
            // construct two threads for our demonstration;  
            Thread thread1 = new Thread(() => Print1("Twitter!"));
            Thread thread2 = new Thread(() => Print2());
            Thread thread3 = new Thread(() => Print3("Linkedin!"));

            // start them  
            thread1.Start();
            thread2.Start();
            thread3.Start();
        }

        private static void Print1(string socialNetwork)
        {
            lock (socialNetworkLock)
            {
                if (socialNetwork.Equals("Twitter!"))
                {
                    Console.WriteLine("Hello I received a message from " + socialNetwork);
                }
            }
        }

        private static void Print2()
        {
            if (!isInitialized)
            {
                // we should avoid locking on any public or even a variable that it's in use,
                // we should only lock in a private variable of type object, for example, that it
                // is meant to serve only this purpose
                lock (socialNetworkLock)
                {
                    //checks the state of the application
                    if (!isInitialized)
                    {
                        foreach (string network in socialNetworkList)
                        {
                            Thread.Sleep(1000);
                            Console.WriteLine("Hello from " + network);
                        }

                        isInitialized = true;
                    }
                }
            }
        }

        private static void Print3(string socialNetwork)
        {
            lock (socialNetworkLock)
            {
                if (socialNetwork.Equals("Linkedin!"))
                {
                    Console.WriteLine("Hello I received a message from " + socialNetwork);
                }
            }
        }

        private static bool isInitialized = false;
        // we should lock always in a provate readonly instance of object:
        private static readonly object socialNetworkLock = new object();
        private static string[] socialNetworkList = new string[5] { "Snapchat", "Instagram", "Slack", "Linkedin", "WhatsApp" };
    }
}
