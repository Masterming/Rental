using System;

namespace Serverside
{
    class Program
    {
        public static void Main()
        {
            string ip = "127.0.0.1";
            int port = 80;

            Responses responses = RequestHandler.Responses;
            responses.Add("hello server", () => "hello world");
            responses.Add("test", () => "test");
            RequestHandler.Responses = responses;

            Server server = new Server(ip, port);
            server.Run();

            Console.WriteLine("Press any key to close ...\n");
            Console.ReadKey();
            server.Stop();
        }
    }
}