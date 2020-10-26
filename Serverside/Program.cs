using System;
using System.Threading;

namespace Serverside
{
    static class Program
    {
        private static readonly Mutex mainMutex = new Mutex(true, "main");
        public static void Main()
        {
            if (!mainMutex.WaitOne(3000, false))
            {
                Console.WriteLine("Another Instance of this program ia already running");
                Console.WriteLine("Press any key to close ...\n");
                Console.ReadKey();
                return;
            }

            string ip = "127.0.0.1";
            int port = 80;

            /*Responses responses = RequestHandler.Responses;
            responses.Add("hello server", () => "hello world");
            responses.Add("test delay", () => { Task.Delay(TimeSpan.FromSeconds(20)).Wait(); return "test"; });
            RequestHandler.Responses = responses;*/

            Server server = new Server(ip, port);
            server.Run();

            Console.WriteLine("Press any key to close ...\n");
            Console.ReadKey();
            server.Stop();
        }
    }
}