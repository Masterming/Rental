using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace Serverside
{
    static class Program
    {
        private static Mutex mainMutex = new Mutex(true, "main");
        public static void Main()
        {
            //start only one instance of this application
            if (!mainMutex.WaitOne(3000, false))
            {
                Console.WriteLine("Another Instance of this program is already running");
                Console.WriteLine("Press any key to close ...\n");
                Console.ReadKey();
                return;
            }

            string ip = "127.0.0.1";
            int port = 80;

            Server server = new Server(ip, port);
            server.Run();

            Console.WriteLine("Press any key to close ...\n");
            Console.ReadKey();
            server.Stop();
        }
    }
}