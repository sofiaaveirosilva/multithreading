namespace multithreading
{
    using System;
    using System.Threading;

    public class SocialNetwork
    {

        public void Print1(string socialNetwork)
        {
            lock (socialNetworkLock)
            {
                if (socialNetwork.Equals("Twitter!"))
                {
                    Console.WriteLine("Hello I received a message from " + socialNetwork);
                }
            }
        }

        public void Print2()
        {
            if (!isInitialized)
            {
                // While a lock is held, the thread that holds the lock can again acquire and release the lock.
                // Any other thread is blocked from acquiring the lock and waits until the lock is released.
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
                            Thread.Sleep(500);
                            Console.WriteLine("Hello from " + network);
                        }

                        isInitialized = true;
                    }
                }
            }
        }

        public void Print3(string socialNetwork)
        {
            lock (socialNetworkLock)
            {
                if (socialNetwork.Equals("Linkedin!"))
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("Hello I received a message from " + socialNetwork);
                }
            }
        }

        private bool isInitialized = false;
        // we should lock always in a private readonly instance of object:
        private readonly object socialNetworkLock = new object();
        private readonly string[] socialNetworkList = new string[5] { "Snapchat", "Instagram", "Slack", "Linkedin", "WhatsApp" };

    }
}
